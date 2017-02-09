using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LinaGoes.Models;
using LinaGoes.ViewModels;
using LinaHelpers.Statics;
using LinaModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LinaGoes.Controllers
{
    [Authorize(Roles = RoleName.Admin)]
    public class SchoolController : Controller
    {
        private ApplicationDbContext _db;

        public SchoolController()
        {
            _db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }


        public ActionResult Index()
        {
            List<School> schoolList = _db.Schools.ToList();
            return View(schoolList);
        }

        public ActionResult New()
        {
            School sc = new School
            {
                Active = true
            };

            return View("SchoolForm",sc);
        }

        public ActionResult Edit(int id)
        {

            School sc = _db.Schools.SingleOrDefault(t => t.Id == id);
            if (sc == null)
                return HttpNotFound();
            return View("SchoolForm", sc);
        }

        [HttpPost]
        public ActionResult Save(School sch)
        {
            if (!ModelState.IsValid)
            {
                return View("SchoolForm",sch);
            }

            if (sch.Id == 0)
            {
                sch.EditedDate = DateTime.Now;
                sch.EditedBy = User.Identity.Name;
                _db.Schools.Add(sch);
            }
            else
            {
                var schoolInDb = _db.Schools.Single(t => t.Id == sch.Id);
                schoolInDb.Name = sch.Name;
                schoolInDb.LogoUrl = sch.LogoUrl;
                schoolInDb.Phone = sch.Phone;
                schoolInDb.Website = sch.Website;
                schoolInDb.Address = sch.Address;
                schoolInDb.Active = sch.Active;
                schoolInDb.EditedDate = DateTime.Now;
                schoolInDb.EditedBy = User.Identity.Name;

            }

            _db.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult Assign()
        {
            List<School> schoolList = _db.Schools.ToList();
            List<ApplicationUser> usersList = _db.Users.ToList();

            SchoolAssignViewModel sa = new SchoolAssignViewModel
            {
                SchoolList  = schoolList,
                UserList = usersList
            };

            return View("Assign", sa);
        }


        /// <summary>
        /// Okula Yönetici Atama Fonksiyonu
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Assign(SchoolAssignViewModel mod)
        {
            
            //var roleStore = new RoleStore<IdentityRole>(_db);
            //var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<ApplicationUser>(_db);
            var userManager = new UserManager<ApplicationUser>(userStore);
            IdentityResult res = userManager.AddToRole(mod.UserId.ToString(), RoleName.SchoolAdmin);

            if (!res.Succeeded)
            {
                string mes = "";
                foreach (string er in res.Errors)
                {
                    mes += er + "/";
                }
                return Content(mes);
            }
            else
            {
                try
                {
                    ApplicationUser u = _db.Users.FirstOrDefault(t => t.Id == mod.UserId);
                    if (u != null) u.SchoolId = mod.SchoolId;
                    _db.SaveChanges();
                }
                catch (Exception)
                {
                }
            }
            

            //_db.Roles.

            //UserManagerExtensions.AddToRole(this, mod.UserId, RoleName.SchoolAdmin);
            //var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            //var roleManager = new RoleManager<IdentityRole>(roleStore);
            //await roleManager.CreateAsync(new IdentityRole("Teacher"));
            //await UserManager.AddToRoleAsync(user.Id, RoleName.Parent);
            //var store = new UserStore<IdentityUser>(_db);
            //UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>();

            //userManager.AddToRole(mod.UserId, RoleName.SchoolAdmin);

            //UserManagerExtensions.AddToRole();

            //AddUserToRole(mod.UserId, RoleName.SchoolAdmin);
            //UserManager.AddToRoleAsync(mod.UserId, RoleName.SchoolAdmin);
            return RedirectToAction("Assign");

        }


    }
}