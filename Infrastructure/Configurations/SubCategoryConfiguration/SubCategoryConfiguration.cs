using Domain.Entities.SubCategories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.SubCategoryConfiguration;

public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
{
    public void Configure(EntityTypeBuilder<SubCategory> builder)
    {
        builder.ToTable("SubCategories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CategoryId).IsRequired();

        builder.HasOne(sc => sc.Category)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(sc => sc.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(sc => sc.SubSubCategories)
            .WithOne(ssc => ssc.SubCategory)
            .HasForeignKey(ssc => ssc.SubCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsOne(x => x.Name, b =>
        {
            b.Property(p => p.En).HasColumnName("NameEn").HasMaxLength(200).IsRequired();
            b.Property(p => p.Ar).HasColumnName("NameAr").HasMaxLength(200).IsRequired();
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


    }
}