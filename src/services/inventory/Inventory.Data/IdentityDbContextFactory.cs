﻿using Microsoft.EntityFrameworkCore;

namespace ECommerce.Inventory.Data;

public class IdentityDbContextFactory(
    IDbContextFactory<IdentityDbContext> pooledFactory) : IDbContextFactory<IdentityDbContext>
{
    public IdentityDbContext CreateDbContext()
    {
        var context = pooledFactory.CreateDbContext();

        // context.ConfigureLogger(logger);

        return context;
    }
}
