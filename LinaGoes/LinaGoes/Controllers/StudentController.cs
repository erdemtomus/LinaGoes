using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinaGoes.Models;
using LinaGoes.ViewModels;
using LinaHelpers.Statics;
using LinaModels;

namespace LinaGoes.Controllers
{
    [Authorize(Roles = RoleName.Admin + "," + RoleName.SchoolAdmin + "," + RoleName.Teacher)]
    public class StudentController : Controller
    {
        private ApplicationDbContext _db;

        public StudentController()
        {
            _db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }


        public ActionResult Index()
        {
            List<Student> studentList = _db.Students.Include(m => m.Classroom).ToList();
            return View(studentList);
        }

        public ActionResult New()
        {
            int schoolId = 0;
            

            //schoolId = kullanıcının okulunu bilmemiz lazım burada...


            StudentFormViewModel sfvm = new StudentFormViewModel
            {
                Student =  new Student{Active = true},
                ClassroomList = _db.Classrooms.Where(t=>t.Active==true && t.SchoolId==schoolId).ToList()
            };
            

            return View("StudentForm", sfvm);
        }

    }
}