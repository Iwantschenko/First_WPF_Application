using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbContextClasses
{
    public class Course
    {
        [Key]
        public Guid Course_ID { get; set; }
        public string Course_Name { get; set;}
        public string Course_Description { get; set;}
        public List<GroupStudent> Groups { get; set; }
        public override string ToString()
        {
            return this.Course_Name;
        }
    }
}
