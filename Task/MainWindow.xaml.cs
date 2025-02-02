﻿using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Services;
using DbContextClasses;
using FunctionRepository;
using Microsoft.Extensions.DependencyInjection;
using Task.UserControll;
using System.Security.Permissions;
namespace Task
{
    
    public partial class MainWindow : Window
    {
        private ServiceDb<Course> _courseServise;
        private ServiceDb<GroupStudent> groupService;
        private ServiceDb<Teacher> _teacherService;
        private ServiceDb<Student> _studentService;
        private IServiceScope _Scope;
        private ObservableCollection<CourseHierarchicaTree> _treeViewList;

       
        
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

            _treeViewList = new ObservableCollection<CourseHierarchicaTree>();
            
            Fill_TreeViewList();
            Tree.ItemsSource = _treeViewList;
        }
        
        public void Fill_TreeViewList()
        {
            _treeViewList.Clear();
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
                _treeViewList.Add(branch);
            }
            
        }
               
        //UI event 
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

            CourseTabControll courseTab = new CourseTabControll(_Scope);
            
            TabItem tabItem = courseTab.CreateTabItem(courseCollection);
            tabItem.Header = CreateTabHeader("Courses");
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
            TeachersTabControll teacherTab = new TeachersTabControll(_Scope);
            TabItem tabItem = teacherTab.CreateTabItem(teacherCollection);
            tabItem.Header = CreateTabHeader("Teachers");
            Tabs.Items.Add(tabItem);
            Tabs.SelectedItem = tabItem;
        }

        private void ClickOnCourse(CourseHierarchicaTree item)
        {

            GroupTabControll groupTabControll = new GroupTabControll(_Scope);
            TabItem tabItem = groupTabControll.CreateTabItem(item);

            tabItem.Header = CreateTabHeader( $"{item.Courses.Course_Name} groups");
            Tabs.Items.Add(tabItem);
            Tabs.SelectedItem = tabItem;
        }
        private void ClickOnGroup(GroupHierarchicalLowTree item)
        {
            StudentTabControll studentsTabControll = new StudentTabControll(_Scope);
            TabItem tabItem = studentsTabControll.CreateTabItem(item);

            tabItem.Header = CreateTabHeader( item.Group.Group_Name );
            Tabs.Items.Add(tabItem);
            Tabs.SelectedItem = tabItem;

        }
        private StackPanel CreateTabHeader( string headText)
        {
            Button closeTab = new Button();
            closeTab.Content = "X";
            closeTab.Height = 20;
            closeTab.Width = 20;
            closeTab.Click += CloseTab_Click;
            StackPanel header = new StackPanel();
            header.Orientation = Orientation.Horizontal;
            header.Children.Add(new TextBlock { Text = $"{headText}   " });
            header.Children.Add(closeTab);

            return header;
        }

        private void CloseTab_Click(object sender, RoutedEventArgs e)
        {
            Tabs.Items.RemoveAt(Tabs.SelectedIndex);
        }

        private void RefreshTree_Click(object sender, RoutedEventArgs e)
        {
            Fill_TreeViewList();
            
        }
    }
}