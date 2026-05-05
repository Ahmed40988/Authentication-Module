using Domain.Entities.Cataloges;
using Domain.Entities.Categories;
using Domain.Entities.Products;
using Domain.Entities.SubSubCategories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.ProductConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.SKU)
            .HasMaxLength(100)
            .IsRequired();


        builder.Property(x => x.Price)
            .HasPrecision(18, 2);

        builder.Property(x => x.StockQuantity)
            .IsRequired();

        builder.HasOne<Brand>()
            .WithMany()
            .HasForeignKey(x => x.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<SubCategory>()
            .WithMany()
            .HasForeignKey(x => x.SubCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<SubSubCategory>()
            .WithMany()
            .HasForeignKey(x => x.SubSubCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsOne(x => x.Name, b =>
        {
            b.Property(p => p.En).HasColumnName("NameEn").HasMaxLength(300).IsRequired();
            b.Property(p => p.Ar).HasColumnName("NameAr").HasMaxLength(300).IsRequired();
            b.HasIndex(p => p.En);
            b.HasIndex(p => p.Ar);
        });

        builder.OwnsOne(x => x.Description, b =>
        {
            b.Property(p => p.En).HasColumnName("DescriptionEn").HasMaxLength(2000);
            b.Property(p => p.Ar).HasColumnName("DescriptionAr").HasMaxLength(2000);
        });

        builder.HasIndex(x => x.SKU).IsUnique();
    }
}