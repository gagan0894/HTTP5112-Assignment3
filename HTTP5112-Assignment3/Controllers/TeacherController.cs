using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using HTTP5112_Assignment3.Models;

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
            List<Teacher> Teachers = new List<Teacher>();
            TeacherDataController controller = new TeacherDataController();
            Teachers = controller.ListTeachers();

            return View(Teachers);

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

        [HttpGet]
        [Route("Teacher/New")]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [Route("Teacher/Create")]
        public ActionResult Create(string Fname,string Lname,string Enum,string Salary)
        {
            Debug.WriteLine("Fname-" + Fname+",Lname-"+Lname + ",Enum-" + Enum);
            TeacherDataController controller = new TeacherDataController();
            Teacher newTeacher = new Teacher();
            newTeacher.Fname = Fname;
            newTeacher.Lname = Lname;
            newTeacher.Salary = Salary;
            newTeacher.Enum = Enum;
            controller.AddTeacher(newTeacher );
            return RedirectToAction("ListTeacher");
        }

        [HttpGet]
        [Route("/Author/DeleteConfirm/{id}")]
        public ActionResult DeleteConfirm(int id)
        {
            ViewBag.teacherId = id.ToString();

            return View();
        }


        //POST : /Author/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("ListTeacher");
        }
    }
}