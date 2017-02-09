﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinaModels
{
    public class ClassroomTeacher
    {
        public int Id { get; set; }
        [Required]
        public int ClassroomId { get; set; }
        public Classroom Classroom { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
