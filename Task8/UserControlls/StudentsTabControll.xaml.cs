using DbContextClasses;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
using static System.Formats.Asn1.AsnWriter;

namespace Task8.UserControlls
{
    /// <summary>
    /// Interaction logic for StudentsTabControll.xaml
    /// </summary>
    public partial class StudentsTabControll : UserControl
    {
        private ServiceDb<GroupStudent> _groupService;
        private ServiceDb<Student> _studentService;
        private ResourceManager _resources;

        private ObservableCollection<Student> _studentListView;
        public StudentsTabControll(IServiceScope scope)
        {
            InitializeComponent();
             _resources = new ResourceManager("Task8.Resource1" , Assembly.GetExecutingAssembly());
            _studentListView = new ObservableCollection<Student>();

            _groupService = scope.ServiceProvider.GetRequiredService<ServiceDb<GroupStudent>>();
            _studentService = scope.ServiceProvider.GetRequiredService<ServiceDb<Student>>();
        }
        public TabItem CreateTabItem(GroupHierarchicalLowTree item)
        {
            _studentListView = item.Students;
            StudentListUI.ItemsSource = _studentListView;
            return new TabItem()
            {
                Header = $"{item.Group.Group_Name} Group",
                Content = Panel
            };
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (StudentListUI.SelectedItem != null && StudentListUI.SelectedItem is Student)
            {
                Student student = StudentListUI.SelectedItem as Student;
                var resultMessege = MessageBox.Show(_resources.GetString("Delete"), "Delete", MessageBoxButton.YesNo);
                if (resultMessege == MessageBoxResult.Yes)
                {
                    _studentListView.Remove(student);
                    _studentService.Remove(student);
                    _studentService.Save();
                }
            }
            else
                return;
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(StudentListUI.SelectedItem.ToString());
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(StudentListUI.SelectedItem.ToString());
        }
    }
}
