using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sea.Web.Models
{
    public class ClassModel
    {
        public int ID { get; set; }

        [Display(Name = "Class Name")]
        public string Name { get; set; }

        public string Location { get; set; }

        [RegularExpression("(Mr|Ms|Mrs)\\s[A-Z][a-z]+$", ErrorMessage = "Teacher name is not valid")]
        public string Teacher { get; set; }

        public  ICollection<StudentModel> Students { get; set; }

        public ClassModel()
        {
            Students = new List<StudentModel>();
        }

    }
}