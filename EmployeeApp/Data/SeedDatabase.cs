using EmployeeApp.Models;

namespace EmployeeApp.Data
{
    public static class SeedDatabase
    {
        public static void AddData(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetService<EmployeeDbContext>();
            List<Department> departments = new List<Department>()
            {
                new Department { Id = 1, Name = "IT" },
                new Department { Id = 2, Name = "Finance" },
                new Department { Id = 3, Name = "Sales" }
            };

            db.Departments.AddRange(departments);
            db.SaveChanges();

            Employee employee = new Employee { Id = 1, Name = "Jack", DepartmentId = 1, Email = "jack12@gmai.com", Salary = 100000, Phone = "1234578", Address = "New Town, NY" };
            db.Employees.Add(employee);
            db.SaveChanges();

        }

    }
}
