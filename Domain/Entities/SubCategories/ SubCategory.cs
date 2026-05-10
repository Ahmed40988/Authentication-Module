using Domain.Entities.Categories;
using Domain.Entities.SubSubCategories;

namespace Domain.Entities.SubCategories
{

    public class SubCategory : BaseEntity
    {
        public Guid Id { get; private set; }
        public Guid CategoryId { get; private set; }
        public LocalizedString Name { get; private set; } = default!;
        public LocalizedString ?Description { get; private set; } = default!;
        public Category Category { get; private set; } = default!;

        private readonly List<SubSubCategory> _subSubCategories = new();
        public IReadOnlyCollection<SubSubCategory> SubSubCategories => _subSubCategories.AsReadOnly();
        private SubCategory() { }

        public SubCategory(string? nameEn, string? nameAr, Guid categoryId,string ?desEn,string ?desAr)
        {
            Id = Guid.NewGuid();
            CategoryId = categoryId;
            Name = LocalizedString.Create(nameEn, nameAr);
            SetDescription(desEn, desAr);
        }
        public Result<bool> Update(string? nameEn, string? nameAr, string? descEn, string? descAr,Guid ?categorId)
        {
            Name = LocalizedString.Create(nameEn, nameAr);
            SetDescription(descEn, descAr);
                if (categorId.HasValue)
                    CategoryId = categorId.Value;

            return Result<bool>.Success(true);
        }
        public void ToggleStatus()
        {
            if (IsActive)
                Deactivate();
            else
                Activate();
        }
        public Result<bool> Delete()
        {
            if (_subSubCategories.Any())
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
    }
}
