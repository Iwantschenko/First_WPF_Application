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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task8.UserControlls
{
    /// <summary>
    /// Interaction logic for TempUser.xaml
    /// </summary>
    public partial class TempUser : UserControl
    {
        private ServiceDb<Course> _courseService;

        private ResourceManager _resources;
        private ObservableCollection<Course> _courseListView;
        public TempUser(IServiceScope scope)
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

        

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (courseListUI.SelectedItem != null)
            try
            {
                Course course = courseListUI.SelectedItem as Course;
                IdBox.Text = course.Course_ID.ToString();
                NameBox.Text = course.Course_Name;
                DescriptionBox.Text = course.Course_Description;    
            }
            catch(Exception ex ) 
            {
                MessageBox.Show(ex.Message);
                return;
            }
            else
            {
                MessageBox.Show(_resources.GetString("EditSelect")) ;
            }


        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            IdBox.Text = Guid.NewGuid().ToString();
            NameBox.Text = String.Empty;
            DescriptionBox.Text = String.Empty;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (courseListUI.SelectedItem != null && courseListUI.SelectedItem is Course)
            {
                Course course = (Course)courseListUI.SelectedItem;
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Course course = new Course()
                {
                    Course_ID = Guid.Parse(IdBox.Text),
                    Course_Name = NameBox.Text,
                    Course_Description = DescriptionBox.Text
                };

                if (MessageBox.Show(_resources.GetString("CreateYes"),"Create Course" , MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (_courseService.GetId(course.Course_ID) == null)
                    {
                        _courseService.Add(course);
                        _courseListView.Add(course);
                    }
                    else
                    {
                        _courseService.Update(course);
                         int index = _courseListView.IndexOf( _courseListView.FirstOrDefault(x => x.Course_ID == course.Course_ID));
                        _courseListView[index] = course;
                        
                    }
                    _courseService.Save();
                }
                else 
                    return;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
