using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Automat.Domain.Product.Db;
using Automat.Domain.Product.Services.Requests;
using Automat.Domain.Product.Services.Responses;

namespace Automat.Domain.Product.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<GetProductsBySkuListResponseDto> GetProductsBySkuListAsync(GetProductsBySkuListRequestDto requestDto, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductsBySkuListAsync(requestDto.SkuList, cancellationToken);
            return new GetProductsBySkuListResponseDto
            {
                Products = products
            };
        }

        public async Task<GetAvailableProductsResponseDto> GetAvailableProducts(GetAvailableProductsRequestDto requestDto, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAsync(product => product.AvailableQuantity > 0 && !product.Category.RelatedCategories.Any());
            return new GetAvailableProductsResponseDto
            {
                Products = products
            };
        }

        public async Task<DecreaseStockResponseDto> DecreaseStockAsync(DecreaseStockRequestDto requestDto, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductsBySkuListAsync(requestDto.DecreaseItems.Select(item => item.Sku), cancellationToken);
            foreach (var product in products)
            {
                var decreaseItem = requestDto.DecreaseItems.FirstOrDefault(item => item.Sku == product.Sku);
                if (decreaseItem != null)
                {
                    var availableQuantity = product.AvailableQuantity - decreaseItem.Quantity;
                    await _productRepository.UpdateAsync(product.SetAvailableQuantity(availableQuantity > 0 ? availableQuantity : 0));
                }
            }

            return new DecreaseStockResponseDto();
        }
    }
}
