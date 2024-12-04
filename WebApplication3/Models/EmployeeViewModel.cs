namespace WebApplication3.Models
{
    public class EmployeeViewModel
    {
        public int EmpId { get; set; }
        public string EmployeeName { get; set; }

        public string EmployeeEmail { get; set; }
        public string DepartmentName { get; set; }

        // Property to hold the selected department ID
        public int EmpDepId { get; set; }
    }
}
