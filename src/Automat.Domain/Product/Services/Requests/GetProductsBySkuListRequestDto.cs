using System.Collections.Generic;

namespace Automat.Domain.Product.Services.Requests
{
    public class GetProductsBySkuListRequestDto
    {
        public IEnumerable<string> SkuList { get; set; }
    }
}