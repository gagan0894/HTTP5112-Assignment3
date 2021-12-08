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
        [HttpGet]
        public ActionResult ListTeacher()
        {
            List<string> Teachers = new List<string>();
            TeacherDataController controller = new TeacherDataController();
                Teachers=controller.ListTeachers();

                ViewBag.TeachersList = Teachers;
                return View();

        }

        [HttpGet]
        [Route("Teacher/Show/{id}")]
        public ActionResult Show(int id)
        {
            List<string> Teachers = new List<string>();
            TeacherDataController controller = new TeacherDataController();
            Teachers = controller.ListTeachersWithSalary(id);
            ViewBag.Salary = id;
            ViewBag.TeachersList = Teachers;
            return View();

        }
    }
}