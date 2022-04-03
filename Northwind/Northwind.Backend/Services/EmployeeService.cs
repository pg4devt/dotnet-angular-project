#nullable disable
using Microsoft.EntityFrameworkCore;
using Northwind.Backend.DataContext;
using Northwind.Backend.Models;
using System.Drawing;

namespace Northwind.Backend.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly int DefaultPageSize = 10;
        private readonly int MaxPageSize = 100;

        private readonly NorthwindContext _context;

        public EmployeeService(NorthwindContext context)
        {
            _context = context;
        }

        public async Task<EmployeeListResult> GetEmployeesAsync(EmployeeListRequest request)
        {
            IEnumerable<Employee> employeesQuery = await _context.Employees.ToListAsync();

            int top = Math.Min(request?.Top ?? DefaultPageSize, MaxPageSize);
            int skip = request?.Skip ?? 0;

            var employees = employeesQuery
                .OrderBy(a => a.EmployeeId)
                .Skip(skip)
                .Take(top)
                .ToList();

            var totalCount = employees.Count;

            foreach(var employee in employeesQuery)
            {
                if (employee.Photo != null)
                {
                    employee.PhotoPath = @"data:image/jpeg;base64," + Convert.ToBase64String(employee.Photo);
                }
            }

            var result = new EmployeeListResult
            {
                TotalCount = totalCount,
                Items = employees,
            };

            return result;
        }
        
        public async Task<Employee> GetEmployeeAsync(int id)
        {
            var result = await _context.Employees.FindAsync(id);
            return result;
        }

        private byte[] ImageToByteArray(Image imageIn)
        {
            if (imageIn is null)
            {
                throw new ArgumentNullException(nameof(imageIn));
            }

            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);

                return ms.ToArray();
            }
        }

        private Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                var returnImage = Image.FromStream(ms);

                return returnImage;
            }
        }
    }
}
