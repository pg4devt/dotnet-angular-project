namespace Northwind.Backend.Models
{
    public class CategoryListRequest : ListRequest
    {
        public Category Category { get; set; } = null!;
    }
}
