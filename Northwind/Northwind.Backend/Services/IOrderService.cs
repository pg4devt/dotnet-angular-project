using Northwind.Backend.Models;

namespace Northwind.Backend.Services
{
    public interface IOrderService
    {
        Task<OrderListResult> GetOrdersAsync(OrderListRequest request);
    }
}
