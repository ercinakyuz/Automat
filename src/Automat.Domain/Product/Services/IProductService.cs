using System.Threading;
using System.Threading.Tasks;
using Automat.Domain.Product.Services.Requests;
using Automat.Domain.Product.Services.Responses;

namespace Automat.Domain.Product.Services
{
    public interface IProductService
    {
        Task<GetProductsBySkuListResponseDto> GetProductsBySkuListAsync(GetProductsBySkuListRequestDto requestDto, CancellationToken cancellationToken);
        Task<GetAvailableProductsResponseDto> GetAvailableProducts(GetAvailableProductsRequestDto requestDto, CancellationToken cancellationToken);

        Task<DecreaseStockResponseDto> DecreaseStockAsync(DecreaseStockRequestDto requestDto, CancellationToken cancellationToken);
    }
}