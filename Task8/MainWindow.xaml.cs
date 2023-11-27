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
        public static ServiceDb<Course> courseServise {  get; private set; } 
        public static ServiceDb<GroupStudent> groupService {  get; private set; }
        public static ServiceDb<Teacher> teacherService {  get; private set; }
        public static ServiceDb<Student> studentService {  get; private set; }
        public MainWindow()
        {
            InitializeComponent();

            #region//SetServices
            var collectionService = new ServiceCollection()
                .AddDbContext<DataBaseContextClass>()
                .AddScoped<IRepository<Course>, CourseRepository>()
                .AddScoped<IRepository<GroupStudent>, GroupRepository>()
                .AddScoped<IRepository<Teacher>, TeacherRepository>()
                .AddScoped<IRepository<Student>, StudentRepository>()
                .AddScoped<ServiceDb<Course>>()
                .AddScoped<ServiceDb<GroupStudent>>()
                .AddScoped<ServiceDb<Teacher>>()
                .AddScoped<ServiceDb<Student>>()
                .BuildServiceProvider();

            var scope = collectionService.CreateScope();
            courseServise = scope.ServiceProvider.GetRequiredService<ServiceDb<Course>>();
            groupService = scope.ServiceProvider.GetRequiredService<ServiceDb<GroupStudent>>();
            teacherService = scope.ServiceProvider.GetRequiredService<ServiceDb<Teacher>>();
            studentService = scope.ServiceProvider.GetRequiredService<ServiceDb<Student>>();
            #endregion

            #region//Fill_TreeView
            List<TreeViewControll> treeViewList = new List<TreeViewControll>();
            foreach (var dbCourse in courseServise.GetAll())
            {
                TreeViewControll branch = new TreeViewControll();
                branch.Courses = dbCourse;

                ObservableCollection<LowTree> lowTreeList = new ObservableCollection<LowTree>();
                foreach (var group in groupService.GetAll().Where(x => x.CourseId == dbCourse.Course_ID))
                {
                    LowTree lowTree = new LowTree();
                    lowTree.Group = group;
                    foreach (var students in studentService.GetAll().Where(x => x.GroupId == group.Group_Id))
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
            if (selectedTreeItem is TreeViewControll)
            {
                ClickOnCourse((TreeViewControll)selectedTreeItem);
            }
            else if (selectedTreeItem is LowTree)
            {
                ClickOnGroup((LowTree)selectedTreeItem);
            }
            else
                MessageBox.Show($"Error with ->  {selectedTreeItem.ToString()}");
        }

        private void ClickOnCourse(TreeViewControll item)
        {
            GroupTabControll groupTabControll = new GroupTabControll();
            TabItem tabItem = groupTabControll.CreateTabItem(item);
            Tabs.Items.Add(tabItem);
            Tabs.SelectedItem = tabItem;
        }
        private void ClickOnGroup(LowTree item)
        {
            StudentsTabControll studentsTabControll = new StudentsTabControll();
            TabItem tabItem = studentsTabControll.CreateTabItem(item);
            Tabs.Items.Add(tabItem);
            Tabs.SelectedItem = tabItem;

        }
        private void CourseButton_Click(object sender, RoutedEventArgs e)
        { 
            CourseTabControlls courseTabControll = new CourseTabControlls();
            TabItem item = courseTabControll.CreateTabItem(courseServise.GetAll());
            Tabs.Items.Add(item);
            Tabs.SelectedItem = item;
        }
    }
}
 