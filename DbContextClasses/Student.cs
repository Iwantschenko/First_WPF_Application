using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbContextClasses
{
    public  class Student
    {
        [Key]
        public Guid Student_Id { get; set; }
        public string First_Name { get;set; }
        public string Last_Name { get;set; }
        public Guid GroupId { get; set; }
        public GroupStudent? Group { get; set; }
    }
}
