using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinaGoes.Models;
using LinaModels;

namespace LinaGoes.ViewModels
{
    public class ClassroomTeacherViewModel
    {
        public List<ClassroomTeacher> CList { get; set; }
        public ClassroomTeacher ClassRoomTeacher{ get; set; }
        public List<ApplicationUser> UsersInTeacherRole { get; set; }
        public Classroom CurrentClassInfo { get; set; }
    }
}