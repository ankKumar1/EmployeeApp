using EmployeeApp.Models;

namespace EmployeeApp.ViewModel
{
    public class EmployeeVM
    {
        public Employee Employee { get; set; }
        public List<Department> Departments { get; set; }
    }
}
