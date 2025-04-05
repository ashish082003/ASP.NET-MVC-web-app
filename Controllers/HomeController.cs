using System.Diagnostics;
using EmployeeData.Models;
using EmployeeData.Data;
using Microsoft.AspNetCore.Mvc;


namespace EmployeeData.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmployeContext _context;
        private const int PageSize = 5; 

        public HomeController(ILogger<HomeController> logger, EmployeContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult Employee(string searchTerm, int pageNumber = 1)
        {
            var employeesQuery = _context.Employees.AsQueryable();

            
            if (!string.IsNullOrEmpty(searchTerm))
            {
                employeesQuery = employeesQuery.Where(e => e.Name.Contains(searchTerm));
            }

          
            int totalEmployees = employeesQuery.Count();
            var employees = employeesQuery
                .OrderBy(e => e.EmployeeId)
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();

           
            ViewBag.SearchTerm = searchTerm;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalEmployees / PageSize);

            return View(employees);
        }

        public IActionResult CreateEditEmployee(int? id)
        {
            if (id != null)
            {
                var employee = _context.Employees.SingleOrDefault(x => x.EmployeeId == id);
                return View(employee);
            }
            return View();
        }

        public IActionResult DeleteEmploye(int id)
        {
            var employee = _context.Employees.SingleOrDefault(x => x.EmployeeId == id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return RedirectToAction("Employee");
        }

        public IActionResult CreateEditEmployeeForm(Employee model)
        {
            if (model.EmployeeId == 0)
            {
                _context.Employees.Add(model);
            }
            else
            {
                _context.Employees.Update(model);
            }

            _context.SaveChanges();
            return RedirectToAction("Employee");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
