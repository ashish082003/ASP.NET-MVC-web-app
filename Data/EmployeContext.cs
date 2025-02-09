using Microsoft.EntityFrameworkCore;
using EmployeeData.Models;
namespace EmployeeData.Data
{
    public class EmployeContext:DbContext
    {
        public EmployeContext(DbContextOptions<EmployeContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
    }
}
