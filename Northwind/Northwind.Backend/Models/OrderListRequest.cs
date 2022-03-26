namespace Northwind.Backend.Models
{
    public class OrderListRequest : ListRequest
    {
        public Order Order { get; set; } = null!;
    }
}
