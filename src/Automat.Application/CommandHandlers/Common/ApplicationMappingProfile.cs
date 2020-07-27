using AutoMapper;
using Automat.Application.CommandHandlers.Common.Contracts;
using Automat.Domain.Basket.Models;
using Automat.Domain.Product.Models;

namespace Automat.Application.CommandHandlers.Common
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<BasketItem, OrderItemContract>();
            CreateMap<Product, OrderProductContract>();
        }
    }
}
