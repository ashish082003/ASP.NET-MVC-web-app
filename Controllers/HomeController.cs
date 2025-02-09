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

        public HomeController(ILogger<HomeController> logger,EmployeContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateEditEmployee( int? id)

        {
            if (id != null)
            {
                var isPre = _context.Employees.SingleOrDefault(x => x.EmployeeId == id);
                return View(isPre);
            }
            return View();
        }
        public IActionResult DeleteEmploye(int id)
        {
            var isPre = _context.Employees.SingleOrDefault(x => x.EmployeeId == id);
     
            _context.Employees.Remove(isPre);
            _context.SaveChanges();
            return RedirectToAction("Employee");
        }

        public IActionResult Employee()
        {
            var allEmployes = _context.Employees.ToList();
            return View(allEmployes);
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
