using Domain.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.EmployeeConfiguration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
                 .ValueGeneratedNever();
        builder.HasOne(x => x.User)
            .WithOne(x => x.Employee)
            .HasForeignKey<Employee>(x => x.Id);

        builder.Property(x => x.JobTitle)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Status)
        .HasConversion<string>()
        .HasMaxLength(50)
        .IsRequired();
    }
}