using DbContextClasses;
using FunctionRepository;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static DataBaseContextClass db = new DataBaseContextClass();
        static CourseRepository courseRepository = new CourseRepository(db);
        static GroupRepository groupRepository = new GroupRepository(db);
        static ServicesForRepository<Course> courseServise = new ServicesForRepository<Course>(courseRepository);
        static ServicesForRepository<GroupStudent> groupService = new ServicesForRepository<GroupStudent>(groupRepository);
        public MainWindow()
        {
            InitializeComponent();   
        }

        

       
    }
}
