using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinaGoes.Models;
using LinaModels;

namespace LinaGoes.ViewModels
{
    public class SchoolAssignViewModel
    {
        public string UserId { get; set; }
        public int SchoolId { get; set; }
        public List<School> SchoolList { get; set; }
        public List<ApplicationUser> UserList { get; set; }
    }
}