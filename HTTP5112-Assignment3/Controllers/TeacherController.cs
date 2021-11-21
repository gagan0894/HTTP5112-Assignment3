using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTTP5112_Assignment3.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListTeacher(int? salary)
        {
            List<string> Teachers = new List<string>();
            TeacherDataController controller = new TeacherDataController();
                if(salary == null)
                Teachers=controller.ListTeachers();
                else
                Teachers =controller.ListTeachersWithSalary(salary);

                ViewBag.TeachersList = Teachers;
                return View();
        }

    }
}