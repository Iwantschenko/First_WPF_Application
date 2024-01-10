using DbContextClasses;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Resources;
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

namespace Task.UserControll
{
    /// <summary>
    /// Interaction logic for GroupTabControll.xaml
    /// </summary>

    
    public partial class GroupTabControll : UserControl
    {
        private ServiceDb<GroupStudent> _groupService;
        private ServiceDb<Teacher> _teacherService;
        private ResourceManager _resources;
        private ServiceDb<Student> _studentService;

        private Course _thisCourse;
        private ObservableCollection<GroupStudent> _groupListView;

        

        public GroupTabControll(IServiceScope scope)
        {
            _resources = new ResourceManager("Task.Resource2", Assembly.GetExecutingAssembly());
           
            _groupListView = new ObservableCollection<GroupStudent>();
            _groupService = scope.ServiceProvider.GetRequiredService<ServiceDb<GroupStudent>>();
            _teacherService = scope.ServiceProvider.GetRequiredService<ServiceDb<Teacher>>();
            _studentService = scope.ServiceProvider.GetRequiredService<ServiceDb<Student>>();
            _thisCourse = new Course();

            InitializeComponent();
        }
       
        public TabItem CreateTabItem(CourseHierarchicaTree item)
        {
            foreach(var i in _groupService.GetAll().Where(x => x.CourseId == item.Courses.Course_ID))
            {
                _groupListView.Add(i);
            }

            _thisCourse = item.Courses;

            groupListUI.ItemsSource = _groupListView;
            TeacherComboBox.ItemsSource = _teacherService.GetAll();

            return new TabItem()
            {
                
                Content = Panel
            };
             
        }
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            IdBox.Text = Guid.NewGuid().ToString();
            IdCourseBox.Text = _thisCourse.Course_Name;
            NameBox.Text = String.Empty;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (groupListUI.SelectedItem != null)
            {
                try
                {
                    GroupStudent item = (GroupStudent)groupListUI.SelectedItem;

                    IdBox.Text = item.Group_Id.ToString();
                    IdCourseBox.Text = _thisCourse.Course_Name;
                    NameBox.Text = item.Group_Name;
                    TeacherComboBox.SelectedItem = _teacherService.GetId(item.TeacherId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show(_resources.GetString("EditSelect"));
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            
            if (groupListUI.SelectedItem != null && groupListUI.SelectedItem is GroupStudent)
            {
                GroupStudent group = (GroupStudent)groupListUI.SelectedItem;
                if (_studentService.GetAll().FirstOrDefault(x=> x.GroupId == group.Group_Id) != null)
                {
                    MessageBox.Show(_resources.GetString("GroupNotNuLL"));
                    return;
                }
                var resultMessege = MessageBox.Show(_resources.GetString("Delete"), "Delete", MessageBoxButton.YesNo);
                if (resultMessege == MessageBoxResult.Yes)
                {
                    _groupListView.Remove(group);
                    _groupService.Remove(group);
                    _groupService.Save();
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Teacher teacher = (Teacher)TeacherComboBox.SelectedItem;
                GroupStudent group = new GroupStudent()
                {
                    Group_Id = Guid.Parse(IdBox.Text),
                    CourseId = _thisCourse.Course_ID,
                    Group_Name = NameBox.Text,
                    TeacherId = teacher.Teacher_Id

                };

                if (MessageBox.Show(_resources.GetString("CreateChangeYes"), "GroupYesNo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (_groupService.GetId(group.Group_Id) == null)
                    {
                        _groupService.Add(group);
                        _groupListView.Add(group);
                        _groupService.Save();
                    }
                    if (_groupService.GetId(group.Group_Id) != null)
                    {
                        _groupService.Update(group);

                        int index = _groupListView.IndexOf(_groupListView.FirstOrDefault(x => x.Group_Id == group.Group_Id));
                        
                        _groupListView[index] = group;

                        _groupService.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
    }
}
