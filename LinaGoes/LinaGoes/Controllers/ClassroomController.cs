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
using Microsoft.AspNet.Identity;

namespace LinaGoes.Controllers
{
    [Authorize(Roles = RoleName.Admin + "," + RoleName.SchoolAdmin)]
    public class ClassroomController : Controller
    {
        private ApplicationDbContext _db;

        public ClassroomController()
        {
            _db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }


        public ActionResult Index()
        {
            List<Classroom> croomList = _db.Classrooms.Include(m=>m.School).ToList();
            return View(croomList);
        }

        public ActionResult New()
        {
            Classroom cr = new Classroom
            {
                Active = true
            };

            TempData["schoolList"] = _db.Schools.Where(t => t.Active ==true).ToList();

            return View("ClassroomForm", cr);
        }

        public ActionResult Edit(int id)
        {
            TempData["schoolList"] = _db.Schools.Where(t => t.Active == true).ToList();

            Classroom sc = _db.Classrooms.SingleOrDefault(t => t.Id == id);
            if (sc == null)
                return HttpNotFound();
            return View("ClassroomForm", sc);
        }

        [HttpPost]
        public ActionResult Save(Classroom cs)
        {
            if (!ModelState.IsValid)
            {
                TempData["schoolList"] = _db.Schools.Where(t => t.Active == true).ToList();

                return View("ClassroomForm", cs);
            }

            if (cs.Id == 0)
            {
                cs.EditedDate = DateTime.Now;
                cs.EditedBy = User.Identity.Name;
                _db.Classrooms.Add(cs);
            }
            else
            {
                var classRoomInDb = _db.Classrooms.Single(t => t.Id == cs.Id);
                classRoomInDb.Name = cs.Name;
                classRoomInDb.Definition = cs.Definition;
                classRoomInDb.SchoolId = cs.SchoolId;
                classRoomInDb.Active = cs.Active;
                classRoomInDb.EditedDate = DateTime.Now;
                classRoomInDb.EditedBy = User.Identity.Name;
            }

            _db.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult Teacher(int id)
        {

            List<ClassroomTeacher> clist = _db.ClassroomTeachers.Where(t => t.ClassroomId == id).ToList();
            string roleId = _db.Roles.SingleOrDefault(t => t.Name == RoleName.Teacher).Id;
            ClassroomTeacherViewModel c = new ClassroomTeacherViewModel
            {
                CList = clist,
                ClassRoomTeacher = new ClassroomTeacher
                {
                    ClassroomId =  id
                },
                UsersInTeacherRole = _db.Users.Where(t=>t.Roles.Select(z=>z.RoleId).Contains(roleId)).ToList(),
                CurrentClassInfo = _db.Classrooms.Single(t=>t.Id==id)
            };
            return View("Teacher",c);

        }


        [HttpPost]
        public ActionResult Teacher(ClassroomTeacherViewModel ct)
        {

            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            _db.ClassroomTeachers.Add(ct.ClassRoomTeacher);
            _db.SaveChanges();

            return RedirectToAction("Teacher",new {id= ct.ClassRoomTeacher.Id});

        }
        


    }
}