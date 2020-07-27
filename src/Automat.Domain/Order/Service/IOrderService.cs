using System.Threading;
using System.Threading.Tasks;
using Automat.Domain.Order.Service.Requests;
using Automat.Domain.Order.Service.Responses;

namespace Automat.Domain.Order.Service
{
    public interface IOrderService
    {
        Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken);
        Task<GetOrderResponse> GetOrderAsync(GetOrderRequest request, CancellationToken cancellationToken);
    }
}
