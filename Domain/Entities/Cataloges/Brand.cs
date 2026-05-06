
using Domain.Entities.BrandCategories;
using Domain.Entities.Products;

namespace Domain.Entities.Cataloges
{
    public class Brand : BaseEntity
    {
        public Guid Id { get; private set; }
        public LocalizedString Name { get; private set; } = default!;
        public LocalizedString ?Description { get; private set; } = default!;
        public string? LogoUrl { get; private set; }

        private readonly List<Product> _products = new();
        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        private readonly List<BrandCategory> _brandCategories = new();
        public IReadOnlyCollection<BrandCategory> BrandCategories => _brandCategories.AsReadOnly();


        private Brand() { }
        public Brand(string nameEn, string nameAr, string ?descEn, string ?descAr, string? logo)
        {
            Id = Guid.NewGuid();
            Name = LocalizedString.Create(nameEn, nameAr);
            SetDescription(descEn, descAr);
            LogoUrl = logo;
        }
        public Result<bool> Update(string nameEn, string nameAr, string ?descEn, string ?descAr, string? logo)
        {
            Name = LocalizedString.Create(nameEn, nameAr);
            SetDescription(descEn, descAr);
            LogoUrl = logo;

            return Result<bool>.Success(true);
        }
        public Result<bool> Delete()
        {
            if (_products.Any())
                return Result<bool>.Failure(LocalizationKeys.CannotDelete);

            Deactivate();
            return Result<bool>.Success(true);
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
        public void ToggleStatus()
        {
            if (IsActive)
                Deactivate();
            else
                Activate();
        }
    }
}