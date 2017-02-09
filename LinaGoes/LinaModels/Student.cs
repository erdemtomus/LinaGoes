using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinaModels
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "İsim Zorunludur")]
        [StringLength(50)]
        [Display(Name = "İsim")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyisim Zorunludur")]
        [StringLength(50)]
        [Display(Name="Soyisim")]
        public string Surname { get; set; }

        public string Phone { get; set; }

        [Display(Name = "Sınıf")]
        [Required]
        public int ClassroomId { get; set; }
        public Classroom Classroom { get; set; }

        /// <summary>
        /// Standart Properties
        /// </summary>
        public bool Active { get; set; }
        public DateTime EditedDate { get; set; }
        public string EditedBy { get; set; }

    }
}
    