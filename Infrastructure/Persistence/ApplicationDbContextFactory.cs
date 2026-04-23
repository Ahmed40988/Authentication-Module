using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContextFactory
       : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

                optionsBuilder.UseSqlServer(
                    "Server=.;Database=AuthDb;Trusted_Connection=True;TrustServerCertificate=True"
                );

                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }
    
}
