using ECommerce.Domain.AggregatesModel.Identity;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using ECommerce.Shared.Data.Extensions;

namespace ECommerce.Data;

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

        ModelBuilderExtensions.ConfigureIdentityEntitiesExtensions(modelBuilder);
    }
}
