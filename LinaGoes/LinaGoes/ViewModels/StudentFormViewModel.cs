using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinaGoes.Models;
using LinaModels;

namespace LinaGoes.ViewModels
{
    public class StudentFormViewModel
    {
        public Student Student { get; set; }
        public List<ApplicationUser> Parents { get; set; }
        public List<Classroom> ClassroomList { get; set; }
        
    }
}