using System.Collections.Generic;

namespace Automat.Domain.Product.Dtos
{
    public class CategoryDomainDto
    {
        public string Name { get; set; }
        public IEnumerable<CategoryDomainDto> RelatedCategories { get; set; }
        public CategoryDomainDto SubCategory { get; set; }
    }
}