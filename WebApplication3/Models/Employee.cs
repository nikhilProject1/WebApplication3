using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Employee
    {
        [Key]        
        public int EmployeeId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public int DeptId { get; set; }

    }

}
