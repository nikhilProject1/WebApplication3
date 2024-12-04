using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Departments
    {
        [Key]
        public int Id { get; set; } 
        public string DeptName { get; set; }
        public string Description { get; set; }
            
    }
}
