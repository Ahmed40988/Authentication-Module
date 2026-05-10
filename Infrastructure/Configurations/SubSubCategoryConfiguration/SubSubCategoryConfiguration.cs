using Domain.Entities.SubCategories;
using Domain.Entities.SubSubCategories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceStack;
namespace Infrastructure.Configurations.SubSubCategoryConfiguration
{ 

public class SubSubCategoryConfiguration : IEntityTypeConfiguration<SubSubCategory>
{
    public void Configure(EntityTypeBuilder<SubSubCategory> builder)
    {
        builder.ToTable("SubSubCategories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.SubCategoryId).IsRequired();

        builder.HasOne<SubCategory>()
            .WithMany()
            .HasForeignKey(x => x.SubCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsOne(x => x.Name, b =>
        {
            b.Property(p => p.En).HasColumnName("NameEn").HasMaxLength(200).IsRequired();
            b.Property(p => p.Ar).HasColumnName("NameAr").HasMaxLength(200).IsRequired();

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


        }

    }

    }