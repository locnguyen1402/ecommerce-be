// namespace ECommerce.Shared.Common.Infrastructure.Data;

// public abstract class BaseDbContext : DbContext
// {
//     public BaseDbContext(DbContextOptions opts) : base(opts)
//     {

//     }

//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         var entryAssembly = Assembly.GetCallingAssembly() ?? throw new NullReferenceException("entryAssembly");

//         modelBuilder.ApplyConfigurationsFromAssembly(entryAssembly);
//     }
// }