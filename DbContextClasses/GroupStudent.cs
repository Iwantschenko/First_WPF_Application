using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbContextClasses
{
    public class GroupStudent
    {
        [Key]
        public Guid Group_Id { get; set; }
        public string Group_Name {get ;set ;}
        public Guid CourseId { get; set; }
        public Course? Course { get; set; }
        public Guid TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

    }
}
