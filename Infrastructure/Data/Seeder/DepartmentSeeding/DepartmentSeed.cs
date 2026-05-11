using Domain.Entities.Departments;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Seeder;

public static class DepartmentSeed
{
    public static async Task SeedAsync(
        ApplicationDbContext context)
    {
        if (await context.departments.AnyAsync())
            return;

        var departments = new List<Department>
        {
            new(
                "Human Resources",
                "الموارد البشرية",
                "HR Department",
                "قسم الموارد البشرية"
            ),

            new(
                "Information Technology",
                "تكنولوجيا المعلومات",
                "IT Department",
                "قسم تكنولوجيا المعلومات"
            ),

            new(
                "Finance",
                "المالية",
                "Finance Department",
                "قسم المالية"
            ),

            new(
                "Marketing",
                "التسويق",
                "Marketing Department",
                "قسم التسويق"
            ),

            new(
                "Operations",
                "العمليات",
                "Operations Department",
                "قسم العمليات"
            )
        };

        await context.departments.AddRangeAsync(
            departments);

        await context.SaveChangesAsync();
    }
}