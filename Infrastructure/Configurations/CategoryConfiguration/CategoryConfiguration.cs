using Domain.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.CategoryConfiguration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(x => x.Id);
        builder.OwnsOne(x => x.Name, b =>
        {
            b.Property(p => p.En).HasColumnName("NameEn").HasMaxLength(200).IsRequired();
            b.Property(p => p.Ar).HasColumnName("NameAr").HasMaxLength(200).IsRequired();
            b.HasIndex(p => p.En);
            b.HasIndex(p => p.Ar);
        });

        builder.OwnsOne(x => x.Description, b =>
        {
            b.Property(p => p.En).HasColumnName("DescriptionEn").HasMaxLength(1000);
            b.Property(p => p.Ar).HasColumnName("DescriptionAr").HasMaxLength(1000);
        });

        builder.HasMany(c => c.SubCategories)
            .WithOne(sc => sc.Category)
            .HasForeignKey(sc => sc.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}