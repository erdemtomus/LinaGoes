using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinaHelpers.Statics
{
    public static class RoleName
    {

        //manage whole program
        public const string Admin = "Admin";
        //can only manage his own school
        public const string SchoolAdmin = "SchoolAdmin";
        //can only manage his own class
        public const string Teacher = "Teacher";
        //can only view user
        public const string Parent = "Parent";

    }
}
