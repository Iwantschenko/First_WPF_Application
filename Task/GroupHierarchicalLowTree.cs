using DbContextClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public class GroupHierarchicalLowTree
    {
        public GroupStudent Group { get; set; }
        public ObservableCollection<Student> Students { get; set; }
        public GroupHierarchicalLowTree()
        {
            Students = new ObservableCollection<Student>();
        }
        public override string ToString()
        {
            return Group.Group_Name;
        }
    }
}


