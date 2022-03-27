namespace Northwind.Backend.Models

{
    public class EmployeeListRequest : ListRequest
    {
        public Employee Employee { get; set; } = null!;
    }
}
