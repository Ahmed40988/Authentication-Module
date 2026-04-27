using Application.Common.Abstractions.Consts;
using Domain.Entities.RoleModule;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seeder.IdentitySeed
{
    public static class PermissionSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (context.Permissions.Any()) return;

            var permissions = typeof(Permissions)
                .GetFields()
                .Select(x => new Permission
                {
                    PermissionId = Guid.NewGuid(),
                    PermissionName = x.GetValue(null)!.ToString()!
                }).ToList();

            await context.Permissions.AddRangeAsync(permissions);
            await context.SaveChangesAsync();
        }
    }
}
