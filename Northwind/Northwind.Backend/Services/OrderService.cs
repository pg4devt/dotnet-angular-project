using Microsoft.EntityFrameworkCore;
using Northwind.Backend.DataContext;
using Northwind.Backend.Models;

namespace Northwind.Backend.Services
{
    public class OrderService : IOrderService
    {
        private readonly int DefaultPageSize = 10;
        private readonly int MaxPageSize = 100;
        
        private readonly NorthwindContext _context;

        public OrderService(NorthwindContext context)
        {
            _context = context;
        }

        public async Task<OrderListResult> GetOrdersAsync(OrderListRequest request)
        {            
            IEnumerable<Order> ordersQuery = await _context.Orders.ToListAsync();            

            int top = Math.Min(request?.Top ?? DefaultPageSize, MaxPageSize);
            int skip = request?.Skip ?? 0;

            var orders = ordersQuery
                .OrderBy(a => a.OrderId)
                .Skip(skip)
                .Take(top)
                .ToList();

            var totalCount = orders.Count;

            var result = new OrderListResult
            {
                TotalCount = totalCount,
                Items = orders,
            };

            return result;
        }
    }
}
