using Domain.Entities.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.DepartmentConfiguration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");

            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Name, b =>
            {
                b.Property(p => p.En)
                    .HasColumnName("NameEn")
                    .HasMaxLength(200)
                    .IsRequired();

                b.Property(p => p.Ar)
                    .HasColumnName("NameAr")
                    .HasMaxLength(200)
                    .IsRequired();
                b.HasIndex(p => p.En);
                b.HasIndex(p => p.Ar);
            });


            builder.OwnsOne(x => x.Description, b =>
            {
                b.Property(p => p.En)
                    .HasColumnName("DescriptionEn")
                    .HasMaxLength(1000);

                b.Property(p => p.Ar)
                    .HasColumnName("DescriptionAr")
                    .HasMaxLength(1000);
            });
            builder.HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
