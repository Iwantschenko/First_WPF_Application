using DbContextClasses;
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

namespace Task8.UserControlls
{
    /// <summary>
    /// Interaction logic for CourseTabControlls.xaml
    /// </summary>
    public partial class CourseTabControlls : UserControl
    {
        public CourseTabControlls()
        {
            InitializeComponent();
        }
        public TabItem CreateTabItem(List<Course> courses)
        {

            courseList.ItemsSource = courses;
            return new TabItem()
            {
                Header = "Courses",
                Content = Panel
            };
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            if (courseList.SelectedItem != null)
            {
                Course course = courseList.SelectedItem as Course;
                MessageBox.Show($"Are you sure you want to delete? Course {course.Course_Name}","Delete",MessageBoxButton.YesNo);
            }
            else
                MessageBox.Show("Course not selected");
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(courseList.SelectedItem.ToString());
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(courseList.SelectedItem.ToString());
        }
    }
}
