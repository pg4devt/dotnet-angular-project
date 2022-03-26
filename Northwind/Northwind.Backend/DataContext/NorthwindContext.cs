using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.DataContext
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;
    }
}
