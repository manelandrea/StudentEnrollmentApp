using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Sea.Core;

namespace Sea.Web.Models
{
    public class StudentModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }

        [DisplayFormat(DataFormatString="{0:#.####}")]
        public double? GPA { get; set; }

        public int? ClassId { get; set; }

        [Display(Name = "Class Name")]
        public string ClassName { get; set; }

        public IEnumerable<ClassDetail> ClassDetails { get; set; }
    }
}