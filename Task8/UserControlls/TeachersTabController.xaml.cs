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

namespace Task8.UserControlls
{
    /// <summary>
    /// Interaction logic for TeachersTabController.xaml
    /// </summary>
    public partial class TeachersTabController : UserControl
    {
        private ResourceManager _resources;
        private ObservableCollection<Teacher> _teachersListView;
        private ServiceDb<Teacher> _teachersService;
        private ServiceDb<GroupStudent> _groupService;
        public TeachersTabController(IServiceScope scope)
        {
            _resources = new ResourceManager("Task8.Resource1", Assembly.GetExecutingAssembly());
            _teachersListView = new ObservableCollection<Teacher>();
            _teachersService = scope.ServiceProvider.GetRequiredService<ServiceDb<Teacher>>();
            _groupService = scope.ServiceProvider.GetRequiredService<ServiceDb<GroupStudent>>();

            InitializeComponent();
        }
    
        public TabItem CreateTabItem(ObservableCollection<Teacher> teachers)
        {
            _teachersListView = teachers;
            teachersListUI.ItemsSource = _teachersListView;
            return new TabItem()
            {
                Header = "Teachers",
                Content = Panel
            };
        }
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            IdBox.Text = Guid.NewGuid().ToString();
            NameBox.Text = String.Empty;
            SurnameBox.Text = String.Empty;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (teachersListUI.SelectedItem != null)
            {
                try
                {
                    Teacher teacher = (Teacher)teachersListUI.SelectedItem;
                    IdBox.Text = teacher.Teacher_Id.ToString();
                    NameBox.Text = teacher.Teacher_Name;
                    SurnameBox.Text = teacher.Teacher_Surname;
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
            if (teachersListUI.SelectedItem != null && teachersListUI.SelectedItem is Teacher)
            {
                Teacher teacher = (Teacher)teachersListUI.SelectedItem;
                if (_groupService.GetAll().FirstOrDefault(x => x.TeacherId == teacher.Teacher_Id) != null)
                {
                    MessageBox.Show(_resources.GetString("!DeleteTeacher"));
                    return;
                }
                DeleteTeacher(teacher);
            }
        }

        private void DeleteTeacher(Teacher teacher)
        {
            var resultMessege = MessageBox.Show(_resources.GetString("Delete"), "Delete", MessageBoxButton.YesNo);
            if (resultMessege == MessageBoxResult.Yes)
            {
                _teachersListView.Remove(teacher);
                _teachersService.Remove(teacher);
                _teachersService.Save();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Teacher teacher = new Teacher()
                {
                    Teacher_Id = Guid.Parse(IdBox.Text),
                    Teacher_Name = NameBox.Text,
                    Teacher_Surname = SurnameBox.Text,
                };

                if (MessageBox.Show(_resources.GetString("CreateChangeYes"), "Create Course", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (_teachersService.GetId(teacher.Teacher_Id) == null)
                    {
                        _teachersService.Add(teacher);
                        _teachersListView.Add(teacher);
                        _teachersService.Save();
                    }
                    if (_teachersService.GetId(teacher.Teacher_Id) != null)
                    {
                        _teachersService.Update(teacher);

                        int index = _teachersListView.IndexOf(_teachersListView.FirstOrDefault(x => x.Teacher_Id == teacher.Teacher_Id));
                        _teachersListView[index] = teacher;

                        _teachersService.Save();
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
