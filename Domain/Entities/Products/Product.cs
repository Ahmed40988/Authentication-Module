using Domain.Common.ValueObjects;
using Domain.Entities.Cataloges;
using Domain.Entities.Categories;
using Domain.Entities.SubCategories;
using Domain.Entities.SubSubCategories;

namespace Domain.Entities.Products
{
    public class Product : BaseEntity
    {
        public Guid Id { get; private set; }
        public LocalizedString Name { get; private set; } = default!;
        public LocalizedString ?Description { get; private set; } = default!;
        public Guid BrandId { get; private set; }
        public Brand Brand { get; private set; } = default!;
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; } = default!;
        public Guid? SubCategoryId { get; private set; }
        public SubCategory? SubCategory { get; private set; }
        public Guid? SubSubCategoryId { get; private set; }
        public SubSubCategory? SubSubCategory { get; private set; }
        public string SKU { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public string ?imageUrl { get; private set; } = string.Empty;
        public int StockQuantity { get; private set; }

        private Product() { }

        public Product(
            string? nameEn,
            string? nameAr,
            string? descEn,
            string? descAr,
            string sku,
            decimal price,
            int stock,
            string ?ImageUrl,
            Guid brandId,
            Guid categoryId,
            Guid? subCategoryId,
            Guid? subSubCategoryId)
        {
            Id = Guid.NewGuid();

            Name = LocalizedString.Create(nameEn, nameAr);
            SetDescription(descEn, descAr);
            SetSku(sku);
            SetPrice(price);
            SetStock(stock);
            ImageUrl= imageUrl;
            BrandId = brandId;
            CategoryId = categoryId;
            SubCategoryId = subCategoryId;
            SubSubCategoryId = subSubCategoryId;
        }

        public Result<bool> Update(string? nameEn, string? nameAr, decimal price, int stock,string ?desEn,string ?desAr)
        {
            Name = LocalizedString.Create(nameEn, nameAr);
            SetPrice(price);
            SetStock(stock);
            SetDescription(desEn, desAr);
            return Result<bool>.Success(true);
        }

        public Result<bool> ReduceStock(int quantity)
        {
            if (quantity <= 0)
                return Result<bool>.Failure(LocalizationKeys.InvalidQuantity);

            if (StockQuantity < quantity)
                return Result<bool>.Failure(LocalizationKeys.NotEnoughStock);

            StockQuantity -= quantity;
            return Result<bool>.Success(true);
        }

        private void SetSku(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new ArgumentException(LocalizationKeys.Required);

            SKU = sku.Trim();
        }

        private void SetPrice(decimal price)
        {
            if (price < 0)
                throw new ArgumentException(LocalizationKeys.Invalid);

            Price = price;
        }

        private void SetStock(int stock)
        {
            if (stock < 0)
                throw new ArgumentException(LocalizationKeys.Invalid);

            StockQuantity = stock;
        }
        private void SetDescription(string? en, string? ar)
        {
            if (string.IsNullOrWhiteSpace(en) && string.IsNullOrWhiteSpace(ar))
            {
                Description = null;
                return;
            }

            Description = LocalizedString.Create(en, ar);
        }
        public Result<bool> Delete()
        {
            Deactivate();
            return Result<bool>.Success(true);
        }
    }
    }
