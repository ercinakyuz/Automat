using System;

namespace Automat.Domain.Order.Service.Requests
{
    public class GetOrderRequest
    {
        public Guid OrderId { get; set; }
    }
}