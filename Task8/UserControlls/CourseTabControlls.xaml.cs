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
    /// Interaction logic for CourseTabControlls.xaml
    /// </summary>
    public partial class CourseTabControlls : UserControl
    {
        private  ServiceDb<Course> _courseService;

        private  ResourceManager _resources;
        private  ObservableCollection<Course> _courseListView;
        
        public CourseTabControlls(IServiceScope scope)
        {
            InitializeComponent();
            _resources = new ResourceManager("Task8.Resource1", Assembly.GetExecutingAssembly());
            _courseListView = new ObservableCollection<Course>();

            _courseService = scope.ServiceProvider.GetRequiredService<ServiceDb<Course>>();
        }
        public TabItem CreateTabItem(ObservableCollection<Course> courses)
        {
            _courseListView = courses;
            courseListUI.ItemsSource = _courseListView;
            return new TabItem()
            {
                Header = "Courses",
                Content = Panel
            };
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            if (courseListUI.SelectedItem != null && courseListUI.SelectedItem is Course)
            {
                Course course = (Course) courseListUI.SelectedItem;
                var resultMessege = MessageBox.Show(_resources.GetString("Delete"), "Delete", MessageBoxButton.YesNo);
                if (resultMessege == MessageBoxResult.Yes)
                {
                    _courseListView.Remove(course);
                    _courseService.Remove(course);
                    _courseService.Save();
                }
            }
            else
                return;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(courseListUI.SelectedItem.ToString());
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(courseListUI.SelectedItem.ToString());
        }
    }
}
