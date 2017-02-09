using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LinaModels
{
    public class School
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "İsim Zorunludur")]
        public string Name { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        public string LogoUrl { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        [StringLength(100)]
        public string Website { get; set; }


        /// <summary>
        /// Standart Properties
        /// </summary>
        public bool Active { get; set; }
        public DateTime EditedDate { get; set; }
        public string EditedBy { get; set; }

        public string UserId { get; set; }
    }
}
