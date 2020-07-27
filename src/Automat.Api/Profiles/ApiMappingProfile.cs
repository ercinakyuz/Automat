using AutoMapper;
using Automat.Api.Models.Response;
using Automat.Application.CommandHandlers.CompleteOrderWithCash.Models;
using Automat.Application.CommandHandlers.CompleteOrderWithCreditCard.Models;

namespace Automat.Api.Profiles
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<CompleteOrderWithCashCommandResult, CompleteOrderWithCashResponse>()
                .ForMember(destination => destination.Result, memberOptions => memberOptions.MapFrom(from => from.Order));
            CreateMap<CompleteOrderWithCreditCardCommandResult, CompleteOrderWithCreditCardResponse>()
                .ForMember(destination => destination.Result, memberOptions => memberOptions.MapFrom(from => from.Order));
        }
    }
}
