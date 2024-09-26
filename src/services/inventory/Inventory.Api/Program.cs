using System.Text;

using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;

using MassTransit;

using OpenIddict.Validation.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

using RabbitMQ.Client;

using ECommerce.Inventory.Api.Services;
using ECommerce.Inventory.Data;
using ECommerce.Inventory.Data.Repositories;
using ECommerce.Inventory.Data.Repositories.Identity;
using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Inventory.Domain.AggregatesModel.Identity;
using ECommerce.Inventory.Infrastructure.Settings;
using ECommerce.Inventory.Infrastructure.Services;

using ECommerce.Shared.Common.Constants;
using ECommerce.Shared.Common.Infrastructure.Services;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.EventBus.Data;
using ECommerce.Shared.Common.EventBus.Extensions;
using ECommerce.Shared.Common.Helper;
using ECommerce.Shared.Data.Extensions;
using ECommerce.Shared.Libs.Extensions;
using ECommerce.Shared.Common.DocumentProcessing;

var builder = WebApplication.CreateSlimBuilder(args);
builder.WebHost.UseKestrelHttpsConfiguration();
builder.WebHost.ConfigureKestrel(options => options.AllowAlternateSchemes = true);
var Configuration = builder.Configuration;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

builder.Services.AddMediator();

// Db context
var connectionString = Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));
var appSettings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>()!;

var eventBusConnectionString = builder.Configuration.GetConnectionString(SchemaConstants.EVENT_BUS_CONNECTION)!;

var certs = await CertificateHelper.GetCertificatesFromPathsAsync(appSettings.Certs.DataProtection, appSettings.Certs.EncryptSigning);
var dpCerts = certs[0];
var identityCerts = certs[1];

var sslCert = await CertificateHelper.GetCertificateFromPathAsync(appSettings.Certs.Ssl);

builder.Services.AddObjectStorageService(appSettings.AwsSettings);

builder.Services
    .AddOptions()
    .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddResponseCaching();
builder.Services.AddResponseCompression();

builder.Services.AddCors();

if (!builder.Environment.IsProduction())
{
    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders =
            ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    });
}

builder.Services.ConfigDbContext<InventoryDbContext>(connectionString, typeof(Program).Assembly.ToString());
builder.Services.ConfigDbContext<IdentityDbContext>(connectionString, typeof(Program).Assembly.ToString());

builder.ConfigPooledDbContext<EventBusDbContext, EventBusDbContextFactory>(SchemaConstants.EVENT_BUS_DB_CONNECTION);

builder.Services.AddDataProtectionContext<IdentityDbContext>(appSettings.AppInstance, 90, dpCerts);

#region Identity

builder.Services
    .AddIdentity<User, Role>(options =>
    {
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = false;

        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 1;
        options.Password.RequiredUniqueChars = 0;
    })
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
    o.TokenLifespan = TimeSpan.FromMinutes(3));

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
});

builder.Services.AddAuthorization();

#endregion Identity

#region OpenIddict

builder.Services.AddOpenIddict()
    // Register the OpenIddict core components.
    .AddCore(options =>
    {
        options.UseEntityFrameworkCore()
            .UseDbContext<IdentityDbContext>()
            .ReplaceDefaultEntities<Application, Authorization, Scope, Token, Guid>();
    })
    // Register the OpenIddict server components.
    .AddServer(options =>
    {
        options.SetAuthorizationEndpointUris("connect/authorize", "connect/context", "connect/consent")
            .SetTokenEndpointUris("connect/token")
            .SetIntrospectionEndpointUris("connect/introspect")
            .SetDeviceEndpointUris("connect/device")
            .SetVerificationEndpointUris("connect/verify")
            .SetLogoutEndpointUris("connect/endsession")
            .SetRevocationEndpointUris("connect/revocation")
            .SetUserinfoEndpointUris("connect/userinfo");

        options.SetIssuer(appSettings.Identity.Authority);

        // Enable the authorization code, implicit and the refresh token flows.
        options.AllowAuthorizationCodeFlow()
            .AllowPasswordFlow()
            .AllowDeviceCodeFlow()
            .AllowClientCredentialsFlow()
            .AllowRefreshTokenFlow();

        options.UseReferenceAccessTokens()
            .UseReferenceRefreshTokens();

        // Expose all the supported scopes in the discovery document.
        options.RegisterScopes(
            Scopes.OpenId,
            Scopes.Email,
            Scopes.Roles,
            Scopes.Phone,
            Scopes.Profile,
            Scopes.OfflineAccess
        );

        // Register the signing and encryption credentials used to protect
        // sensitive data like the state tokens produced by OpenIddict.
        options.AddEncryptionCertificate(identityCerts[0])
            .AddSigningCertificate(identityCerts[1]);

        // if (builder.Environment.IsDevelopment())
        options.DisableAccessTokenEncryption();

        // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
        //
        // Note: the pass-through mode is not enabled for the token endpoint
        // so that token requests are automatically handled by OpenIddict.

        var openIddictServerBuilder = options
            .UseAspNetCore()
            .EnableStatusCodePagesIntegration()
            .EnableAuthorizationEndpointPassthrough()
            .EnableTokenEndpointPassthrough()
            .EnableUserinfoEndpointPassthrough()
            .EnableLogoutEndpointPassthrough()
            .EnableAuthorizationRequestCaching()
            .EnableVerificationEndpointPassthrough();

        // if (builder.Environment.IsDevelopment())
        openIddictServerBuilder.DisableTransportSecurityRequirement();

        // options.SetAccessTokenLifetime(TimeSpan.FromMinutes(appSettings.Config.AccessTokenExpireMinute));
        // options.SetRefreshTokenLifetime(TimeSpan.FromMinutes(appSettings.Config.RefreshTokenExpireMinute));
        // Register the event handler responsible for populating userinfo responses.
        // options.AddEventHandler<HandleUserinfoRequestContext>(configuration =>
        //     configuration.UseSingletonHandler<PopulateUserInfoHandler>());
    });
#endregion OpenIddict

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
builder.Services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddScoped<IMerchantRepository, MerchantRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IShopCollectionRepository, ShopCollectionRepository>();
builder.Services.AddScoped<IProductPromotionRepository, ProductPromotionRepository>();
builder.Services.AddScoped<IOrderPromotionRepository, OrderPromotionRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();

builder.Services.AddScoped<IClientRoleRepository, ClientRoleRepository>();
builder.Services.AddScoped<IPermissionGroupRepository, PermissionGroupRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IImportHistoryRepository, ImportHistoryRepository>();
builder.Services.AddScoped<IObjectStorageRepository, ObjectStorageRepository>();

builder.Services.AddScoped<IMerchantService, MerchantService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IObjectService, ObjectService>();

builder.Services.AddTransient<IIdentityService, IdentityService>();

builder.Services.AddSingleton<IObjectStorageService, ObjectStorageService>();

#region MassTransit
var connectionFactory = new ConnectionFactory
{
    Uri = new Uri(eventBusConnectionString),
};

builder.Services.AddMassTransit(x =>
{
    x.AddEntityFrameworkOutbox<EventBusDbContext>(o =>
    {
        o.UsePostgres();
        o.UseBusOutbox(bo =>
        {
            bo.DisableDeliveryService();
        });

        o.DisableInboxCleanupService();
    });

    x.SetKebabCaseEndpointNameFormatter();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(connectionFactory.HostName, (ushort)connectionFactory.Port, connectionFactory.VirtualHost, h =>
        {
            // Check config password on rabbitmq
            h.Username(connectionFactory.UserName);
            h.Password(connectionFactory.Password);

            h.UseSsl(ssl =>
            {
                ssl.ServerName = "rabbitmq";
                ssl.Certificate = sslCert;
            });
        });

        // cfg.UseDelayedMessageScheduler();

        // TODO: add publish event in here
        // cfg.Publish<SendSmsIntegrationEvent>(p =>
        // {
        //     p.Durable = true;
        //     p.AutoDelete = false;
        //     p.ExchangeType = "fanout";
        // });

        // configure any remaining consumers, sagas, etc.
        cfg.ConfigureEndpoints(context);
    });

});

builder.Services.AddDocumentProcessing();

builder.Services.AddIntegrationEvents<EventBusDbContext>();
#endregion MassTransit

#region common dependencies
builder.Services
    .ConfigureJson()
    .AddAutoMapper()
    .AddValidation()
    .RegisterMediatR();

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerGen();
#endregion common dependencies

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseStatusCodePages();
app.UseStaticFiles();

// Enable CORS
app.UseRouting();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseResponseCaching();
app.UseResponseCompression();

app.AutoMapEndpoints(typeof(Program).Assembly);

await app.RunAsync();