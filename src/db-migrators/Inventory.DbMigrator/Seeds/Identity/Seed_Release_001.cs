using System.Globalization;
using System.Security.Cryptography;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

using ECommerce.Inventory.Data;

using OpenIddict.Abstractions;

using static OpenIddict.Abstractions.OpenIddictConstants;
using ECommerce.Inventory.Domain.AggregatesModel.Identity;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.DbMigrator.Seeds.Identity
{
    public sealed class Seed_Release_001
    {
        public static async Task SeedAsync(
            IServiceProvider services,
            IdentityDbContext dbContext,
            ILogger<IdentityDbContext> logger)
        {
            await InitRoles(services);
            await InitUsers(services);
            await InitApplications(services);
            await InitResources(services);
            await InitScopes(services);
            await InitClientRoles(services);
        }

        private static async Task InitClientRoles(IServiceProvider serviceProvider)
        {
            var clientRoleInfos = new ClientRoleInfo[] {
                new("admin-web", "Administrator"),
                new("admin-portal", "Administrator"),
                new("store-front-mobile", "Customer"),
                new("store-front-web", "Customer"),
            };

            await InitClientRoles(serviceProvider, clientRoleInfos);
        }

        private static async Task InitClientRoles(IServiceProvider serviceProvider, params ClientRoleInfo[] clientRoleInfos)
        {
            var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();

            foreach (var clientRoleInfo in clientRoleInfos)
            {
                if (await dbContext.ClientRoles.AnyAsync(t => t.RoleName == clientRoleInfo.RoleName &&
                                                        t.ClientId == clientRoleInfo.ClientId))
                    continue;

                var clientRole = new ClientRole(clientRoleInfo.RoleName, clientRoleInfo.ClientId);

                dbContext.ClientRoles.Add(clientRole);

                var result = await dbContext.SaveChangesAsync();
            }
        }

        private static async Task InitRoles(IServiceProvider serviceProvider)
        {
            var roleInfos = new RoleInfo[] {
                new("Administrator", "Administrator", true, true),
                new("Customer", "Customer", true, true),
                new("Staff", "Staff", true, true),
            };

            await InitRoles(serviceProvider, roleInfos);
        }

        private static async Task InitRoles(IServiceProvider serviceProvider, params RoleInfo[] roleInfos)
        {
            var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

            foreach (var roleInfo in roleInfos)
            {
                if (await roleManager.Roles.AnyAsync(t => t.NormalizedName == roleInfo.Name.ToUpper()))
                    continue;

                var role = new Role(roleInfo.Name, roleInfo.Description, roleInfo.Predefined);
                role.Enable();

                role.AuditCreation(null);

                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded)
                    continue;
            }
        }

        private static async Task InitUsers(IServiceProvider serviceProvider)
        {
            var usersInfo = new UserInfo[] {
                new("administrator", "P#ssw0rd", "Administrator", "System", "Admin", "admin@vklink.vn", "0123456789"),
            };

            await InitUsers(serviceProvider, usersInfo);
        }

        private static async Task InitUsers(IServiceProvider serviceProvider, params UserInfo[] usersInfo)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            foreach (var userInfo in usersInfo)
            {
                var user = await userManager.FindByNameAsync(userInfo.UserName);

                if (user != null) return;

                user = new User(
                    userInfo.UserName,
                    userInfo.FirstName,
                    userInfo.LastName
                );

                user.SetEmail(userInfo.Email);
                user.SetPhoneNumber(userInfo.PhoneNumber);

                user.ConfirmEmail();
                user.ConfirmPhoneNumber();

                user.AuditCreation(null);

                user.TryToChangeStatus(UserStatus.ACTIVE, string.Empty);

                var result = await userManager.CreateAsync(user, userInfo.Password);

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = await userManager.AddToRoleAsync(user, userInfo.RoleName);

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }

        private static async Task InitApplications(IServiceProvider serviceProvider)
        {
            var manager = serviceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await manager.FindByClientIdAsync("native-api-docs") is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor()
                {
                    ApplicationType = ApplicationTypes.Native,
                    ClientType = ClientTypes.Public,
                    ClientId = "native-api-docs",
                    DisplayName = "Native Api Docs",
                    ConsentType = ConsentTypes.Systematic,
                    RedirectUris = {
                        new Uri("https://oauth.pstmn.io/v1/browser-callback"),
                        new Uri("https://oauth.pstmn.io/v1/callback")
                    },
                    Permissions = {
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.Token,
                        Permissions.Endpoints.Revocation,
                        Permissions.Endpoints.Logout,

                        Permissions.GrantTypes.Password,
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.GrantTypes.RefreshToken,

                        Permissions.ResponseTypes.Code,

                        Permissions.Prefixes.Scope + Scopes.OpenId,
                        Permissions.Prefixes.Scope + Scopes.OfflineAccess,

                        Permissions.Scopes.Email,
                        Permissions.Scopes.Phone,
                        Permissions.Scopes.Roles,
                        Permissions.Scopes.Profile,

                        Permissions.Prefixes.Scope + "api-gateway.all",

                        Permissions.Prefixes.Scope + "blog.all",
                        Permissions.Prefixes.Scope + "catalog.all",
                        Permissions.Prefixes.Scope + "cms.all",
                        Permissions.Prefixes.Scope + "customer.all",
                        Permissions.Prefixes.Scope + "identity.all",
                        Permissions.Prefixes.Scope + "notification.all",
                        Permissions.Prefixes.Scope + "object-storage.all",
                        Permissions.Prefixes.Scope + "ordering.all",
                        Permissions.Prefixes.Scope + "store.all",
                    },

                    Settings =
                    {
                        // Use a shorter access token lifetime for tokens issued to the Api Docs application.
                        [OpenIddictConstants.Settings.TokenLifetimes.AccessToken] = TimeSpan.FromMinutes(20).ToString("c", CultureInfo.InvariantCulture)
                    },

                    Requirements = {
                        Requirements.Features.ProofKeyForCodeExchange
                    }
                });
            }

            if (await manager.FindByClientIdAsync("admin-web") is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor()
                {
                    ApplicationType = ApplicationTypes.Web,
                    ClientType = ClientTypes.Public,
                    ClientId = "admin-web",
                    DisplayName = "Admin Web",
                    ConsentType = ConsentTypes.Implicit,
                    RedirectUris = {
                        new Uri("http://localhost:11000/signin-callback"),
                    },
                    PostLogoutRedirectUris = {
                        new Uri("http://localhost:11000"),
                    },
                    Permissions = {
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.Token,
                        Permissions.Endpoints.Revocation,
                        Permissions.Endpoints.Logout,

                        Permissions.GrantTypes.Password,
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.GrantTypes.RefreshToken,

                        Permissions.ResponseTypes.Code,

                        Permissions.Prefixes.Scope + Scopes.OpenId,
                        Permissions.Prefixes.Scope + Scopes.OfflineAccess,

                        Permissions.Scopes.Email,
                        Permissions.Scopes.Phone,
                        Permissions.Scopes.Roles,
                        Permissions.Scopes.Profile,

                        Permissions.Prefixes.Scope + "api-gateway.all",

                        Permissions.Prefixes.Scope + "blog.all",
                        Permissions.Prefixes.Scope + "catalog.all",
                        Permissions.Prefixes.Scope + "cms.all",
                        Permissions.Prefixes.Scope + "customer.all",
                        Permissions.Prefixes.Scope + "identity.all",
                        Permissions.Prefixes.Scope + "notification.all",
                        Permissions.Prefixes.Scope + "object-storage.all",
                        Permissions.Prefixes.Scope + "ordering.all",
                        Permissions.Prefixes.Scope + "store.all",
                    },
                    Requirements = {
                        Requirements.Features.ProofKeyForCodeExchange
                    },
                });
            }

            if (await manager.FindByClientIdAsync("partner-web") is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor()
                {
                    ApplicationType = ApplicationTypes.Web,
                    ClientType = ClientTypes.Public,
                    ClientId = "partner-web",
                    DisplayName = "Partner Web",
                    ConsentType = ConsentTypes.Explicit,
                    RedirectUris = {
                        new Uri("http://localhost:11000/signin-callback"),
                    },
                    PostLogoutRedirectUris = {
                        new Uri("http://localhost:11000"),
                    },
                    Permissions = {
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.Token,
                        Permissions.Endpoints.Revocation,
                        Permissions.Endpoints.Logout,

                        Permissions.GrantTypes.Password,
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.GrantTypes.RefreshToken,

                        Permissions.ResponseTypes.Code,

                        Permissions.Prefixes.Scope + Scopes.OpenId,
                        Permissions.Prefixes.Scope + Scopes.OfflineAccess,

                        Permissions.Scopes.Email,
                        Permissions.Scopes.Phone,
                        Permissions.Scopes.Roles,
                        Permissions.Scopes.Profile,

                        Permissions.Prefixes.Scope + "api-gateway.all",

                        Permissions.Prefixes.Scope + "blog.all",
                        Permissions.Prefixes.Scope + "catalog.all",
                        Permissions.Prefixes.Scope + "cms.all",
                        Permissions.Prefixes.Scope + "customer.all",
                        Permissions.Prefixes.Scope + "identity.all",
                        Permissions.Prefixes.Scope + "notification.all",
                        Permissions.Prefixes.Scope + "object-storage.all",
                        Permissions.Prefixes.Scope + "ordering.all",
                        Permissions.Prefixes.Scope + "store.all",
                    },
                    Requirements = {
                        Requirements.Features.ProofKeyForCodeExchange
                    },
                });
            }

            if (await manager.FindByClientIdAsync("store-front-web") is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor()
                {
                    ApplicationType = ApplicationTypes.Web,
                    ClientType = ClientTypes.Public,
                    ClientId = "store-front-web",
                    DisplayName = "Store Front Web",
                    ConsentType = ConsentTypes.Explicit,
                    RedirectUris = {
                        new Uri("http://localhost:11010/signin-callback"),
                    },
                    PostLogoutRedirectUris = {
                        new Uri("http://localhost:11010"),
                    },
                    Permissions = {
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.Token,
                        Permissions.Endpoints.Revocation,
                        Permissions.Endpoints.Logout,

                        Permissions.GrantTypes.Password,
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.GrantTypes.RefreshToken,

                        Permissions.ResponseTypes.Code,

                        Permissions.Prefixes.Scope + Scopes.OpenId,
                        Permissions.Prefixes.Scope + Scopes.OfflineAccess,

                        Permissions.Scopes.Email,
                        Permissions.Scopes.Phone,
                        Permissions.Scopes.Roles,
                        Permissions.Scopes.Profile,

                        Permissions.Prefixes.Scope + "api-gateway.all",

                        Permissions.Prefixes.Scope + "blog.all",
                        Permissions.Prefixes.Scope + "catalog.all",
                        Permissions.Prefixes.Scope + "cms.all",
                        Permissions.Prefixes.Scope + "customer.all",
                        Permissions.Prefixes.Scope + "identity.all",
                        Permissions.Prefixes.Scope + "notification.all",
                        Permissions.Prefixes.Scope + "object-storage.all",
                        Permissions.Prefixes.Scope + "ordering.all",
                        Permissions.Prefixes.Scope + "store.all",
                    }
                });
            }

            if (await manager.FindByClientIdAsync("store-front-mobile") is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor()
                {
                    ApplicationType = ApplicationTypes.Native,
                    ClientType = ClientTypes.Public,
                    ClientId = "store-front-mobile",
                    DisplayName = "Store Front Mobile",
                    ConsentType = ConsentTypes.Explicit,
                    RedirectUris = {
                        new Uri("http://localhost:11800/signin-callback"),
                    },
                    PostLogoutRedirectUris = {
                        new Uri("http://localhost:11800"),
                    },
                    Permissions = {
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.Token,
                        Permissions.Endpoints.Revocation,
                        Permissions.Endpoints.Logout,

                        Permissions.GrantTypes.Password,
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.GrantTypes.RefreshToken,

                        Permissions.ResponseTypes.Code,

                        Permissions.Prefixes.Scope + Scopes.OpenId,
                        Permissions.Prefixes.Scope + Scopes.OfflineAccess,

                        Permissions.Scopes.Email,
                        Permissions.Scopes.Phone,
                        Permissions.Scopes.Roles,
                        Permissions.Scopes.Profile,

                        Permissions.Prefixes.Scope + "api-gateway.all",

                        Permissions.Prefixes.Scope + "blog.all",
                        Permissions.Prefixes.Scope + "catalog.all",
                        Permissions.Prefixes.Scope + "cms.all",
                        Permissions.Prefixes.Scope + "customer.all",
                        Permissions.Prefixes.Scope + "identity.all",
                        Permissions.Prefixes.Scope + "notification.all",
                        Permissions.Prefixes.Scope + "object-storage.all",
                        Permissions.Prefixes.Scope + "ordering.all",
                        Permissions.Prefixes.Scope + "store.all",
                    },
                });
            }

            if (await manager.FindByClientIdAsync("admin-portal") is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor()
                {
                    ApplicationType = ApplicationTypes.Web,
                    ClientType = ClientTypes.Public,
                    ClientId = "admin-portal",
                    DisplayName = "Admin Portal",
                    ConsentType = ConsentTypes.Explicit,
                    RedirectUris = {
                        new Uri("http://localhost:8000/signin-callback"),
                        new Uri("https://admin-laundry-dev.vklink.vn/signin-callback"),
                        new Uri("https://admin-new-laundry-dev.vklink.vn/signin-callback"),
                    },
                    PostLogoutRedirectUris = {
                        new Uri("http://localhost:11800"),
                        new Uri("https://admin-laundry-dev.vklink.vn"),
                        new Uri("https://admin-new-laundry-dev.vklink.vn"),
                    },
                    Permissions = {
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.Token,
                        Permissions.Endpoints.Revocation,
                        Permissions.Endpoints.Logout,

                        Permissions.GrantTypes.Password,
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.GrantTypes.RefreshToken,

                        Permissions.ResponseTypes.Code,

                        Permissions.Prefixes.Scope + Scopes.OpenId,
                        Permissions.Prefixes.Scope + Scopes.OfflineAccess,

                        Permissions.Scopes.Email,
                        Permissions.Scopes.Phone,
                        Permissions.Scopes.Roles,
                        Permissions.Scopes.Profile,

                        Permissions.Prefixes.Scope + "api-gateway.all",

                        Permissions.Prefixes.Scope + "blog.all",
                        Permissions.Prefixes.Scope + "catalog.all",
                        Permissions.Prefixes.Scope + "cms.all",
                        Permissions.Prefixes.Scope + "customer.all",
                        Permissions.Prefixes.Scope + "identity.all",
                        Permissions.Prefixes.Scope + "notification.all",
                        Permissions.Prefixes.Scope + "object-storage.all",
                        Permissions.Prefixes.Scope + "ordering.all",
                        Permissions.Prefixes.Scope + "store.all",
                        Permissions.Prefixes.Scope + "reproting.all",
                    },
                });
            }
        }

        private static async Task InitResources(IServiceProvider serviceProvider)
        {
            var manager = serviceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await manager.FindByClientIdAsync("api-gateway") is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor()
                {
                    ClientId = "api-gateway",
                    ClientSecret = "80377ab9-9e6d-4dd8-998c-dd41916f7017",
                    ClientType = ClientTypes.Confidential,
                    DisplayName = "Api Gateway Api",
                    Permissions = {
                        Permissions.GrantTypes.ClientCredentials,
                    },
                });
            }

            if (await manager.FindByClientIdAsync("identity") is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor()
                {
                    ClientId = "identity",
                    ClientSecret = "4973c8e7-a771-42ce-9b25-4fe86c67a989",
                    ClientType = ClientTypes.Confidential,
                    DisplayName = "Identity Api",
                    Permissions = {
                        Permissions.Endpoints.Introspection
                    },
                });
            }
        }

        private static async Task InitScopes(IServiceProvider serviceProvider)
        {
            var manager = serviceProvider.GetRequiredService<IOpenIddictScopeManager>();

            if (await manager.FindByNameAsync("api-gateway.all") is null)
            {
                await manager.CreateAsync(new OpenIddictScopeDescriptor()
                {
                    Name = "api-gateway.all",
                    DisplayName = "Fully permission on Api-gateway Api",
                    Resources = {
                        "api-gateway"
                    },
                });
            }

            if (await manager.FindByNameAsync("identity.all") is null)
            {
                await manager.CreateAsync(new OpenIddictScopeDescriptor()
                {
                    Name = "identity.all",
                    DisplayName = "Fully permission on Identity Api",
                    Resources = {
                        "identity"
                    },
                });
            }
        }

        internal class RoleInfo
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Predefined { get; set; }
            public bool Enabled { get; set; }

            public RoleInfo(string name, string description, bool predefined, bool enabled = true)
            {
                Name = name;
                Description = description;
                Predefined = predefined;
                Enabled = enabled;
            }
        }

        internal class UserInfo
        {
            public string UserName { get; set; }
            public string RoleName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Password { get; set; }

            public UserInfo(
                string userName, string password, string roleName,
                string firstName, string lastName,
                string email, string phoneNumber
                )
            {
                UserName = userName;
                Password = password;
                RoleName = roleName;

                FirstName = firstName;
                LastName = lastName;

                Email = email;
                PhoneNumber = phoneNumber;
            }
        }

        static ECDsaSecurityKey GetECDsaSigningKey(ReadOnlySpan<char> key)
        {
            var algorithm = ECDsa.Create();
            algorithm.ImportFromPem(key);

            return new ECDsaSecurityKey(algorithm);
        }

        internal class ClientRoleInfo
        {
            public string ClientId { get; set; }
            public string RoleName { get; set; }

            public ClientRoleInfo(string clientId, string roleName)
            {
                ClientId = clientId;
                RoleName = roleName;
            }
        }
    }
}
