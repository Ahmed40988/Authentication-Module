using Application.Interfaces;
using Domain.Entities.AuthModule;
using Domain.Entities.AuthModules;
using Domain.Entities.Base;
using Domain.Entities.BrandCategories;
using Domain.Entities.Cataloges;
using Domain.Entities.Categories;
using Domain.Entities.Departments;
using Domain.Entities.Employees;
using Domain.Entities.Products;
using Domain.Entities.RoleModule;
using Domain.Entities.SubCategories;
using Domain.Entities.SubSubCategories;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence
{

    public class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly TimeZoneInfo _appTimeZone;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
            : base(options)
        {
            _httpContext = httpContextAccessor;
            _appTimeZone = ResolveAppTimeZone(configuration);
        }

        // DbSets
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<User> ApplicationUsers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
       public  DbSet<BrandCategory> BrandCategories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<SubSubCategory> SubSubCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department>  departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<RefreshToken>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<IAuditableEntity>();
            var currentUserId = _httpContext.HttpContext?.User?.GetUserId();
            var isAuthenticated = _httpContext.HttpContext?.User?.Identity?.IsAuthenticated == true;

            var now = GetAppNow();

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property(x => x.CreatedAt).CurrentValue = now;

                    if (isAuthenticated && currentUserId != null)
                        entityEntry.Property(x => x.CreatedById).CurrentValue = currentUserId;
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property(x => x.CreatedById).IsModified = false;
                    entityEntry.Property(x => x.CreatedAt).IsModified = false;

                    entityEntry.Property(x => x.UpdatedAt).CurrentValue = now;

                    if (isAuthenticated && currentUserId != null)
                        entityEntry.Property(x => x.UpdatedById).CurrentValue = currentUserId;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        private static TimeZoneInfo ResolveAppTimeZone(IConfiguration configuration)
        {
            var tzId =
                configuration["App:TimeZoneId"]
                ?? configuration["TimeZoneId"]
                ?? Environment.GetEnvironmentVariable("APP_TIME_ZONE_ID")
                ?? Environment.GetEnvironmentVariable("TZ")
                ?? "Egypt Standard Time";

            try
            {
                return TimeZoneInfo.FindSystemTimeZoneById(tzId);
            }
            catch
            {
                return TimeZoneInfo.Local;
            }
        }

        private DateTime GetAppNow()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _appTimeZone);
        }
    }
}