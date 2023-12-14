using DbContextClasses;
using FunctionRepository;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
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
using Task8.UserControlls;

namespace Task8
{
   
    public partial class MainWindow : Window
    {
        private ServiceDb<Course> _courseServise;
        private ServiceDb<GroupStudent> groupService;
        private ServiceDb<Teacher> _teacherService;
        private ServiceDb<Student> _studentService;
        private IServiceScope _Scope;
        public MainWindow()
        {
            InitializeComponent();

            #region SetServices
            var collectionService = new ServiceCollection()
                .AddDbContext<DataBaseContext>()
                .AddScoped<IRepository<Course>, CourseRepository>()
                .AddScoped<IRepository<GroupStudent>, GroupRepository>()
                .AddScoped<IRepository<Teacher>, TeacherRepository>()
                .AddScoped<IRepository<Student>, StudentRepository>()
                .AddScoped<ServiceDb<Course>>()
                .AddScoped<ServiceDb<GroupStudent>>()
                .AddScoped<ServiceDb<Teacher>>()
                .AddScoped<ServiceDb<Student>>()
                .BuildServiceProvider();

            _Scope = collectionService.CreateScope();
            _courseServise = _Scope.ServiceProvider.GetRequiredService<ServiceDb<Course>>();
            groupService = _Scope.ServiceProvider.GetRequiredService<ServiceDb<GroupStudent>>();
            _teacherService = _Scope.ServiceProvider.GetRequiredService<ServiceDb<Teacher>>();
            _studentService = _Scope.ServiceProvider.GetRequiredService<ServiceDb<Student>>();
            #endregion

            #region Fill_TreeView
            List<CourseHierarchicaTree> treeViewList = new List<CourseHierarchicaTree>();
            foreach (var dbCourse in _courseServise.GetAll())
            {
                CourseHierarchicaTree branch = new CourseHierarchicaTree();
                branch.Courses = dbCourse;

                ObservableCollection<GroupHierarchicalLowTree> lowTreeList = new ObservableCollection<GroupHierarchicalLowTree>();
                foreach (var group in groupService.GetAll().Where(x => x.CourseId == dbCourse.Course_ID))
                {
                    GroupHierarchicalLowTree lowTree = new GroupHierarchicalLowTree();
                    lowTree.Group = group;
                    foreach (var students in _studentService.GetAll().Where(x => x.GroupId == group.Group_Id))
                    {
                        lowTree.Students.Add(students);
                    }
                    lowTreeList.Add(lowTree);
                }
                branch.Groups = lowTreeList;
                treeViewList.Add(branch);
            }
            Tree.ItemsSource = treeViewList;
            #endregion

        }

        private void Tree_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedTreeItem = Tree.SelectedItem;
            if (selectedTreeItem is CourseHierarchicaTree)
            {
                ClickOnCourse((CourseHierarchicaTree)selectedTreeItem);
            }
            else if (selectedTreeItem is GroupHierarchicalLowTree)
            {
                ClickOnGroup((GroupHierarchicalLowTree)selectedTreeItem);
            }
            else
                MessageBox.Show($"Error with ->  {selectedTreeItem.ToString()}");
        }

        private void CourseButton_Click(object sender, RoutedEventArgs e)
        {

            ObservableCollection<Course> courseCollection = new ObservableCollection<Course>();
            foreach (var course in _courseServise.GetAll())
            {
                courseCollection.Add(course);
            }
            CoursesTabControll courseTab = new CoursesTabControll(_Scope);
            TabItem tabItem = courseTab.CreateTabItem(courseCollection);
            Tabs.Items.Add(tabItem);
            Tabs.SelectedItem = tabItem;
        }
        
        private void TeacherButton_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Teacher> teacherCollection = new ObservableCollection<Teacher>();
            foreach (var teacher in _teacherService.GetAll())
            {
                teacherCollection.Add(teacher);
            }
            TeachersTabController teacherTab = new TeachersTabController(_Scope);
            TabItem tabItem = teacherTab.CreateTabItem(teacherCollection);
            Tabs.Items.Add(tabItem);
            Tabs.SelectedItem = tabItem;
        }

        private void ClickOnCourse(CourseHierarchicaTree item)
        {
            GroupTabControll groupTabControll = new GroupTabControll(_Scope);
            TabItem tabItem = groupTabControll.CreateTabItem(item);
            Tabs.Items.Add(tabItem);
            Tabs.SelectedItem = tabItem;
        }
        private void ClickOnGroup(GroupHierarchicalLowTree item)
        {
            StudentsTabControll studentsTabControll = new StudentsTabControll(_Scope);
            TabItem tabItem = studentsTabControll.CreateTabItem(item);
            Tabs.Items.Add(tabItem);
            Tabs.SelectedItem = tabItem;

        }
        
    }
}
 