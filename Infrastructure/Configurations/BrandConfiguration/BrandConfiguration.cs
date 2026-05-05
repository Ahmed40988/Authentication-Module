using Domain.Entities.Cataloges;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.BrandConfiguration;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("Brands");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.LogoUrl)
            .HasMaxLength(500);
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

        
        builder.HasMany(b => b.Products)
            .WithOne(p => p.Brand)
            .HasForeignKey(p => p.BrandId)
            .OnDelete(DeleteBehavior.Restrict);


    }
}