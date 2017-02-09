using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using LinaGoes.Models;
using LinaHelpers.Statics;
using LinaModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LinaGoes.Controllers
{
    [Authorize(Roles = RoleName.Admin + "," + RoleName.SchoolAdmin)]
    public class ImportController : Controller
    {
        private ApplicationDbContext _db;

        public ImportController()
        {
            _db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }

        public ActionResult Teacher()
        {
            School sc = _db.Schools.SingleOrDefault(t => t.UserId == User.Identity.Name);
            //string roleId = _db.Roles.SingleOrDefault(t => t.Name == RoleName.Teacher).Id;
            //List<ApplicationUser> UsersInTeacherRole = _db.Users
            //    .Where(t => t.Roles.Select(z => z.RoleId).Contains(roleId))

            //    .ToList();
            return View("Teacher");
        }


        public ActionResult TeacherSave(List<ApplicationUser> apUserList)
        {
            School sc = _db.Schools.SingleOrDefault(t => t.UserId == User.Identity.Name);
            var userStore = new UserStore<ApplicationUser>(_db);
            var userManager = new UserManager<ApplicationUser>(userStore);

            

            apUserList.RemoveAt(apUserList.Count-1);
            foreach (ApplicationUser au in apUserList)
            {
                au.EmailConfirmed = false;
                string password = "abc.123Q";
                au.SchoolId = sc.Id;
                au.UserName = au.Email;
                au.TwoFactorEnabled = false;
                au.Id = Guid.NewGuid().ToString();
                try
                {
                    var result = userManager.Create(au, password);
                    if (result.Succeeded)
                    {
                        userManager.AddToRole(au.Id, RoleName.Teacher);
                    }
                }
                catch (DbEntityValidationException da)
                {
                    var a = da.EntityValidationErrors.ToString();
                }
                
            }
            return View("Teacher");
        }
    }
}