using Northwind.Backend.Models;

namespace Northwind.Backend.Services
{
    public interface IEmployeeService
    {
        Task<Employee> GetEmployeeAsync(int id);
        Task<EmployeeListResult> GetEmployeesAsync(EmployeeListRequest request);
    }
}