using DbContextClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Task8
{
    public class TreeViewControll 
    {
        public Course Courses { get; set; }
        public ObservableCollection<LowTree> Groups { get; set; }
        public TreeViewControll()
        {
            Groups = new ObservableCollection<LowTree>();
        }
        public event ProgressChangedEventHandler? ProgressChanged;

    }
}
