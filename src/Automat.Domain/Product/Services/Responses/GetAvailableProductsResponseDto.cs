using System.Collections.Generic;

namespace Automat.Domain.Product.Services.Responses
{
    public class GetAvailableProductsResponseDto
    {
        public IEnumerable<Models.Product> Products { get; set; }
    }
}