using Domain.Entities.AuthModule;
using Domain.Entities.BrandCategories;
using Domain.Entities.Cataloges;
using Domain.Entities.Categories;
using Domain.Entities.Departments;
using Domain.Entities.Employees;
using Domain.Entities.Products;
using Domain.Entities.RoleModule;
using Domain.Entities.SubCategories;
using Domain.Entities.SubSubCategories;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
       
        DbSet<User> ApplicationUsers { get; }
        DbSet<IdentityRole> Roles { get; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        DbSet<IdentityUserRole<string>> UserRoles { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
        DbSet<Brand> Brands { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<BrandCategory>  BrandCategories { get; set; }
        DbSet<SubCategory> SubCategories { get; set; }
        DbSet<SubSubCategory> SubSubCategories { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<Department> departments { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}