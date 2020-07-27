using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Automat.Domain.Basket.Dtos;
using Automat.Domain.Basket.Models;
using Automat.Domain.Product.Dtos;
using Automat.Domain.Product.Models;
using Automat.Domain.Product.Services;
using Automat.Domain.Product.Services.Requests;

namespace Automat.Domain.Basket.Services
{
    public interface IBasketService
    {
        Task<AddProductsToBasketResponseDto> AddProductsToBasketAsync(AddProductsToBasketRequestDto requestDto, CancellationToken cancellationToken);
    }
    public class BasketService : IBasketService
    {
        private readonly IProductService _productService;

        public BasketService(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<AddProductsToBasketResponseDto> AddProductsToBasketAsync(AddProductsToBasketRequestDto requestDto, CancellationToken cancellationToken)
        {
            Models.Basket basket = null;
            var getProductsBySkuListRequest = new GetProductsBySkuListRequestDto
            {
                SkuList = requestDto.BasketItems.Select(basketItem => basketItem.Sku).Union(requestDto.BasketItems.Select(basketItem => basketItem.RelatedItem?.Sku).Where(sku => !string.IsNullOrWhiteSpace(sku)))
            };

            var getProductsBySkuListResponse = await _productService.GetProductsBySkuListAsync(getProductsBySkuListRequest, cancellationToken);
            if (getProductsBySkuListResponse?.Products != null)
            {
                basket = Models.Basket.Load(new BasketDomainDto());

                foreach (var basketItemDto in requestDto.BasketItems)
                {
                    var product = getProductsBySkuListResponse.Products.FirstOrDefault(p => p.Sku == basketItemDto.Sku);

                    if (product != null && product.AvailableQuantity >= basketItemDto.Quantity)
                    {
                        var basketItem = BasketItem.Load(new BasketItemDomainDto
                        {
                            Quantity = basketItemDto.Quantity,
                            Product = new ProductDomainDto
                            {
                                Sku = product.Sku,
                                AvailableQuantity = product.AvailableQuantity,
                                Name = product.Name,
                                Price = product.Price,
                                Category = new CategoryDomainDto
                                {
                                    Name = product.Category.Name,
                                    SubCategory = product.Category.SubCategory != null ? new CategoryDomainDto
                                    {
                                        Name = product.Category.SubCategory.Name
                                    } : null
                                },
                            }
                        });

                        if (basketItemDto.RelatedItem != null)
                        {
                            var relatedItemDto = basketItemDto.RelatedItem;
                            var relatedProduct = getProductsBySkuListResponse.Products.FirstOrDefault(p => p.Sku == relatedItemDto.Sku);
                            if (relatedProduct != null && relatedProduct.AvailableQuantity >= relatedItemDto.Quantity)
                            {
                                if (AreCategoriesRelated(product.Category, relatedProduct.Category))
                                {
                                    basketItem.SetRelatedItem(new BasketItemDomainDto
                                    {
                                        Quantity = relatedItemDto.Quantity,
                                        Product = new ProductDomainDto
                                        {
                                            Sku = relatedProduct.Sku,
                                            AvailableQuantity = relatedProduct.AvailableQuantity,
                                            Name = relatedProduct.Name,
                                            Price = relatedProduct.Price,
                                            Category = new CategoryDomainDto
                                            {
                                                Name = relatedProduct.Category.Name,
                                                RelatedCategories = relatedProduct.Category.RelatedCategories.Select(rc => new CategoryDomainDto
                                                {
                                                    Name = rc.Name
                                                }),
                                            }
                                        }
                                    });
                                }
                            }
                        }

                        basket.AddBasketItem(basketItem);
                    }

                }
            }

            return new AddProductsToBasketResponseDto
            {
                Basket = basket
            };
        }

        private bool AreCategoriesRelated(Category mainCategory, Category relatableCategory)
        {
            if (relatableCategory?.RelatedCategories != null && relatableCategory.RelatedCategories.Select(rc => rc.Name).Contains(mainCategory.Name))
            {
                return true;
            }
            else if (mainCategory.SubCategory != null)
            {
                return AreCategoriesRelated(mainCategory.SubCategory, relatableCategory);
            }
            else
            {
                return false;
            }

        }
    }
    public class AddProductsToBasketResponseDto
    {
        public Models.Basket Basket { get; set; }
    }

    public class AddProductsToBasketRequestDto
    {
        public IEnumerable<BasketItemDto> BasketItems { get; set; }
    }

    public class BasketItemDto
    {
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public BasketItemDto RelatedItem { get; set; }
    }
}
