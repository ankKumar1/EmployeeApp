using EmployeeApp.Data;
using EmployeeApp.Models;
using EmployeeApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext _context;
        public EmployeeController(EmployeeDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            EmployeeVM employeeVM = new EmployeeVM()
            {
                Employee = id == null && id == 0 ? new Employee() : _context.Employees.FirstOrDefault(x => x.Id == id),
                Departments = _context.Departments.ToList()
            };

            return View(employeeVM);
        }

        #region Api
        public ActionResult<IEnumerable<Employee>> GetAll()
        {
            var employees = _context.Employees.Include(x => x.Department);
            return Ok(employees);
        }

        public ActionResult<Employee> Get(int id)
        {
            var employee = _context.Employees.Include(x => x.Department).FirstOrDefault(x => x.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public ActionResult Create([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public ActionResult Update([FromRoute] int id, [FromBody] Employee employee)
        {
            var employeeInDb = _context.Employees.Include(x => x.Department).FirstOrDefault(x => x.Id == id);
            if (employeeInDb == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            employeeInDb.Name = employee.Name;
            employeeInDb.DepartmentId = employee.DepartmentId;
            employeeInDb.Salary = employee.Salary;
            employeeInDb.Address = employee.Address;
            employeeInDb.Phone = employee.Phone;
            employeeInDb.Email = employee.Email;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var employee = _context.Employees.Include(x => x.Department).FirstOrDefault(x => x.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return Ok();
        }
        #endregion
    }
}

