using Domain.Entities.Cataloges;
using Domain.Entities.Categories;

namespace Domain.Entities.BrandCategories
{
    public class BrandCategory
    {
        public Guid BrandId { get; private set; }
        public Guid CategoryId { get; private set; }

        public Brand Brand { get; private set; } = default!;
        public Category Category { get; private set; } = default!;

        private BrandCategory() { }

        public BrandCategory(Guid brandId, Guid categoryId)
        {
            BrandId = brandId;
            CategoryId = categoryId;
        }
    }
}
