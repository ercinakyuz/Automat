using System.Collections.Generic;

namespace Automat.Domain.Product.Services.Requests
{
    public class DecreaseStockRequestDto
    {
        public IEnumerable<DecreaseItemDto> DecreaseItems { get; set; }
    }
}