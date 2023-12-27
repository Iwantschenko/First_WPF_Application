using DbContextClasses;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for StudentTabControll.xaml
    /// </summary>
    public partial class StudentTabControll : UserControl
    {
        private ResourceManager _resources;
        private ServiceDb<Student> _studentService;

        private GroupStudent _thisGroup;
        private ObservableCollection<Student> _studentsListView;
        public StudentTabControll(IServiceScope scope)
        {
            _resources = new ResourceManager("Task.Resource2", Assembly.GetExecutingAssembly());
            _studentService = scope.ServiceProvider.GetRequiredService<ServiceDb<Student>>();
            _thisGroup = new GroupStudent();
            _studentsListView = new ObservableCollection<Student>();
            InitializeComponent();
        }

        public TabItem CreateTabItem(GroupHierarchicalLowTree item )
        {
            _thisGroup = item.Group;
            _studentsListView = item.Students;

            studentListUI.ItemsSource = _studentsListView;

            return new TabItem()
            {
                Header = $"{item.Group.Group_Name} group",
                Content = Panel
            };
        }
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            IdBox.Text = Guid.NewGuid().ToString();
            IdGroupBox.Text = _thisGroup.Group_Name;
            NameBox.Text = String.Empty;
            SurnameBox.Text = String.Empty;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (studentListUI.SelectedItem != null)
            {
                try
                {
                    Student item = (Student)studentListUI.SelectedItem;

                    IdBox.Text = item.Student_Id.ToString();
                    IdGroupBox.Text = _thisGroup.Group_Name;
                    NameBox.Text = item.First_Name;
                    SurnameBox.Text = item.Last_Name;
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
            if (studentListUI.SelectedItem != null && studentListUI.SelectedItem is Student)
            {
                Student student = (Student)studentListUI.SelectedItem;
                var resultMessege = MessageBox.Show(_resources.GetString("Delete"), "Delete", MessageBoxButton.YesNo);
                if (resultMessege == MessageBoxResult.Yes)
                {
                    _studentService.Remove(student);
                    _studentsListView.Remove(student);
                    _studentService.Save();
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Student student = new Student()
                {
                    Student_Id = Guid.Parse(IdBox.Text),
                    GroupId = _thisGroup.Group_Id,
                    First_Name = NameBox.Text,
                    Last_Name = SurnameBox.Text
                };
                if (MessageBox.Show(_resources.GetString("CreateChangeYes"), "GroupYesNo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (_studentService.GetId(student.Student_Id) == null)
                    {
                        _studentService.Add(student);   
                        _studentsListView.Add(student);
                        _studentService.Save();
                    }
                    if (_studentService.GetId(student.Student_Id) != null)
                    {
                        _studentService.Update(student);

                        int index = _studentsListView.IndexOf(_studentsListView.FirstOrDefault(x => x.Student_Id == student.Student_Id));

                        _studentsListView[index] = student;

                        _studentService.Save();
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
