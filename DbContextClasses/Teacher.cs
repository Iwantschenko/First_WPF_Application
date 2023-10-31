using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbContextClasses
{
    public class Teacher
    {
        [Key]
        public Guid Teacher_Id { get; set; }
        public string Teacher_Name { get; set; }
        public string Teacher_Description { get; set; }
        public List<GroupStudent> Groups { get; set; }

    }
}
