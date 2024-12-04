using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;

        }        
        public ActionResult Index()
        {
            var employeewithdepartments = (from e in _context.Employee
                            join d in _context.Departments
                            on e.DeptId equals d.Id into DepartmentGroup
                            from dept in DepartmentGroup.DefaultIfEmpty()
                            orderby e.EmployeeId
                            select new EmployeeViewModel {
                                EmpId = e.EmployeeId,
                                EmployeeName = e.Name,
                                EmployeeEmail = e.Email,
                                DepartmentName = dept.DeptName
                            }).ToList();                          
            return View(employeewithdepartments);
        }

        //[HttpPost]
        //public IActionResult AddEmployee()
        //{
        //    var emp = new Employee()
        //    {
        //        Name = "test",
        //        Email = "test@gmail.com"
        //    };
        //    _context.Employee.Add(emp);
        //    _context.SaveChanges();
        //    return View(emp);

        //}

        public ActionResult CreateEmployee()
        {
            ViewData["DeptName"] = new SelectList(_context.Departments, "Id", "DeptName");
            return View();
        }

        [HttpPost]
        public ActionResult CreateEmployee(Employee employee)
        {
            ViewData["DeptName"] = new SelectList(_context.Departments, "DeptName", "DeptName", employee.EmployeeId);
            if (ModelState.IsValid)
            {
                _context.Employee.Add(employee);
                _context.SaveChanges();
                return RedirectToAction("Index", "Employee");
            }
            return View();
        }

        public ActionResult EditEmployee(int? Id) {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            ViewData["DeptName"] = new SelectList(_context.Departments, "Id", "DeptName");
            Employee? employee1 = _context.Employee.FirstOrDefault(u => u.EmployeeId == Id);
            if (employee1 == null)
            {
                return NotFound();
            }
            ViewBag.DepartmentName = new SelectList(_context.Departments, "Id", "DeptName", employee1.DeptId);

            return View(employee1);
        }

        [HttpPost]
        public ActionResult EditEmployee(Employee employee)
        {
            ViewData["DeptName"] = new SelectList(_context.Departments, "DeptName", "DeptName", employee.DeptId);
            if (ModelState.IsValid) {
                _context.Employee.Update(employee);
                _context.SaveChanges();
                return RedirectToAction("Index", "Employee");
            }
            return View();
        }
        public ActionResult DeleteEmployee(int? Id)
        {
            if (Id == null || Id == 0) { 
                return NotFound();
            }
            var employee = (from e in _context.Employee where e.EmployeeId == Id
                            select new EmployeeViewModel
                            {
                                EmpId = e.EmployeeId,
                                EmployeeName = e.Name,
                                EmployeeEmail = e.Email                                
                            }).FirstOrDefault();
            if (employee == null) { 
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ActionName("DeleteEmployee")]
        public ActionResult DeleteEmployeePost(int? Id)
        {
            Employee? employee = _context.Employee.FirstOrDefault(u => u.EmployeeId == Id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employee.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction("Index", "Employee");
        }

        [HttpGet]
        public ActionResult ViewEmployee(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var employeeWithDepartments = (from e in _context.Employee
                                           join d in _context.Departments
                                           on e.DeptId equals d.Id into departmentgroup
                                           from dept in departmentgroup.DefaultIfEmpty()
                                           where e.EmployeeId == Id
                                           orderby e.EmployeeId
                                           select new EmployeeViewModel
                                           {
                                               EmpId = e.EmployeeId,
                                               EmployeeName = e.Name,
                                               DepartmentName = dept.DeptName,
                                               EmployeeEmail = e.Email
                                           }).FirstOrDefault();

            if (employeeWithDepartments == null)
            {
                return NotFound();
            }
            return View(employeeWithDepartments);


        }



    }
}
