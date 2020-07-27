using System.Collections.Generic;
using System.Linq;
using Automat.Domain.Product.Dtos;
using Automat.Infrastructure.Db.Models;
using Automat.Infrastructure.Domain.Models;

namespace Automat.Domain.Product.Models
{
    public class Category : ValueObjectBase
    {
        private Category(CategoryDomainDto categoryDomainDto)
        {
            Name = categoryDomainDto.Name;
            if (categoryDomainDto.RelatedCategories != null && categoryDomainDto.RelatedCategories.Any())
            {
                RelatedCategories = categoryDomainDto.RelatedCategories.Select(Load);
            }
            if (categoryDomainDto.SubCategory != null)
            {
                SubCategory = Load(categoryDomainDto.SubCategory);
            }

        }
        public string Name { get; private set; }
        public IEnumerable<Category> RelatedCategories { get; private set; }
        public Category SubCategory { get; private set; }

        public static Category Load(CategoryDomainDto categoryDomainDto)
        {
            return new Category(categoryDomainDto);
        }
    }
}
