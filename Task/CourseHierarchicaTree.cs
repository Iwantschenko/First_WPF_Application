using DbContextClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public class CourseHierarchicaTree
    {
        public Course Courses { get; set; }
        public ObservableCollection<GroupHierarchicalLowTree> Groups { get; set; }
        public CourseHierarchicaTree()
        {
            Groups = new ObservableCollection<GroupHierarchicalLowTree>();
        }
        public event ProgressChangedEventHandler? ProgressChanged;

    }
}
