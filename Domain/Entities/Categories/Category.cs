
using Domain.Entities.BrandCategories;
using Domain.Entities.SubSubCategories;

namespace Domain.Entities.Categories
{

    public class Category : BaseEntity
    {
        public Guid Id { get; private set; }

        public LocalizedString Name { get; private set; } = default!;
        public LocalizedString ?Description { get; private set; } = default!;
        private readonly List<SubCategory> _subCategories = new();
        public IReadOnlyCollection<SubCategory> SubCategories => _subCategories.AsReadOnly();

        private readonly List<BrandCategory> _brandCategories = new();
        public IReadOnlyCollection<BrandCategory> BrandCategories => _brandCategories.AsReadOnly();
        private Category() { }

        public Category(string? nameEn, string? nameAr, string? descEn, string? descAr)
        {
            Id = Guid.NewGuid();
            Name = LocalizedString.Create(nameEn, nameAr);
            SetDescription(descEn, descAr);
        }
        public Result<bool> Update(string? nameEn, string? nameAr, string? descEn, string? descAr)
        {
            Name = LocalizedString.Create(nameEn, nameAr);
            SetDescription(descEn, descAr);

            return Result<bool>.Success(true);
        }

        public Result<bool> Delete()
        {
            if (_subCategories.Any())
                return Result<bool>.Failure(LocalizationKeys.CannotDelete);

            Deactivate();
            return Result<bool>.Success(true);
        }

        public void ToggleStatus()
        {
            if (IsActive)
                Deactivate();
            else
                Activate();
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
    }
}
