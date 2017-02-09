using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinaModels
{
    public class Classroom
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "İsim Zorunludur")]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Definition { get; set; }

        [Display(Name = "Okul")]
        [Required]
        public int SchoolId { get; set; }
        public School School { get; set; }


        /// <summary>
        /// Standart Properties
        /// </summary>
        public bool Active { get; set; }
        public DateTime EditedDate { get; set; }
        public string EditedBy { get; set; }

        public List<ClassroomTeacher> ClassroomTeachers { get; set; }


    }
}
