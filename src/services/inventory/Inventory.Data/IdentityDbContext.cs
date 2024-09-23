using ECommerce.Inventory.Domain.AggregatesModel.Identity;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using ECommerce.Shared.Data.Extensions;
using ECommerce.Inventory.Data.EntityConfigurations.IdentityEntities;

namespace ECommerce.Inventory.Data;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : IdentityDbContext<User, Role, Guid,
            UserClaim, UserRole, UserLogin,
            RoleClaim, UserToken>(options), IDataProtectionKeyContext
{
    public DbSet<DataProtectionKey> DataProtectionKeys => Set<DataProtectionKey>();
    public DbSet<PermissionGroup> PermissionGroups => Set<PermissionGroup>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<SecurityEvent> SecurityEvents => Set<SecurityEvent>();
    public DbSet<Application> Applications => Set<Application>();
    public DbSet<Authorization> Authorizations => Set<Authorization>();
    public DbSet<Scope> Scopes => Set<Scope>();
    public DbSet<Token> Tokens => Set<Token>();
    public DbSet<ClientRole> ClientRoles => Set<ClientRole>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureExtensions();
        modelBuilder.UseCustomDbFunctions();
        modelBuilder.UseCustomPostgreSQLDbFunctions();

        #region OpnIddict

        modelBuilder.ApplyConfiguration(new ApplicationConfiguration());
        modelBuilder.ApplyConfiguration(new AuthorizationConfiguration());
        modelBuilder.ApplyConfiguration(new ScopeConfiguration());
        modelBuilder.ApplyConfiguration(new TokenConfiguration());
        modelBuilder.ApplyConfiguration(new ClientRoleConfiguration());

        #endregion

        #region Identity

        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new RoleClaimConfiguration());

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserClaimConfiguration());
        modelBuilder.ApplyConfiguration(new UserLoginConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserTokenConfiguration());

        #endregion

        #region Security

        modelBuilder.ApplyConfiguration(new PermissionGroupConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        modelBuilder.ApplyConfiguration(new SecurityEventConfiguration());

        #endregion
    }
}
