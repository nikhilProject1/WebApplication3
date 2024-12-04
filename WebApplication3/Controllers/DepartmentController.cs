using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext context;
        public DepartmentController(ApplicationDbContext context) { 
            this.context = context;
        }
        public IActionResult Index()
        {
            var AllDept = context.Departments.ToList();
            return View(AllDept);
        }
        public ActionResult CreateDepartment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDepartment(Departments departments)
        {
            if (departments == null)
            {
                return NotFound();
            }
            if (departments != null && departments.DeptName != null)
            {
                context.Departments.Add(departments);
                context.SaveChanges();
                return RedirectToAction("Index", "Department");
            }
            return View();
        }

        
        public ActionResult EditDepartment(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            Departments? department = context.Departments.FirstOrDefault(u => u.Id == Id);
            if (department == null) { return NotFound(); }
            return View(department);
        }
        [HttpPost]
        public ActionResult EditDepartment(Departments departments)
        {
            if (departments != null)
            {
                context.Departments.Update(departments);
                context.SaveChanges();
                return RedirectToAction("Index", "Department");
            }
            return View();
        }

        public ActionResult DeleteDepartment(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Departments? employeeDepartment = context.Departments.FirstOrDefault(u => u.Id == Id);
            if (employeeDepartment == null) { return NotFound(); }
            return View(employeeDepartment);
        }

        [HttpPost]
        [ActionName("DeleteDepartment")]
        public ActionResult DeleteDepartmentPOST(int? Id)
        {
            Departments? employeeDepartment = context.Departments.FirstOrDefault(u => u.Id == Id);
            if (employeeDepartment == null) { return NotFound(); };
            context.Departments.Remove(employeeDepartment);
            context.SaveChanges();
            return RedirectToAction("Index", "Department");

        }

        [HttpGet]
        public ActionResult ViewDepartment(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Departments? employeeDepartment = context.Departments.FirstOrDefault(u => u.Id == Id);
            if (employeeDepartment == null) { return NotFound(); }
            return View(employeeDepartment);
        }
    }
}
