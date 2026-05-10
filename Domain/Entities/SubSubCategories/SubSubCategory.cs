using Domain.Entities.Categories;
using Domain.Entities.SubCategories;

namespace Domain.Entities.SubSubCategories
{

    public class SubSubCategory : BaseEntity
    {
        public Guid Id { get; private set; }
        public Guid SubCategoryId { get; private set; }

        public LocalizedString Name { get; private set; } = default!;
        public LocalizedString ?Description { get; private set; } = default!;
        public SubCategory SubCategory { get; private set; } = default!;

        private SubSubCategory() { }

        public SubSubCategory(string? nameEn, string? nameAr, Guid subCategoryId,string ?desEn,string ?desAr)
        {
            Id = Guid.NewGuid();
            SubCategoryId = subCategoryId;
            Name = LocalizedString.Create(nameEn, nameAr);
            SetDescription(desEn, desAr);
        }

        public Result<bool> Update(string? nameEn, string? nameAr, string? descEn, string? descAr, Guid? subCategoryId)
        {
            Name = LocalizedString.Create(nameEn, nameAr);
            SetDescription(descEn, descAr);
            if (subCategoryId.HasValue)
                SubCategoryId = subCategoryId.Value;

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
