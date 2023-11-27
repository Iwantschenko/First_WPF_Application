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
    /// Interaction logic for GroupTabControll.xaml
    /// </summary>
    public partial class GroupTabControll : UserControl
    {
        public GroupTabControll()
        {
            InitializeComponent();
        }
        public TabItem CreateTabItem(TreeViewControll item)
        {

            GroupList.ItemsSource = item.Groups;
            return new TabItem()
            {
                Header = $"{item.Courses.Course_Name} course",
                Content = Panel
            };
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Course course = GroupList.SelectedItem as Course;
            if (course != null)
            {
                MessageBox.Show(GroupList.SelectedItem.ToString());
            }
            else MessageBox.Show("Select item, please!");

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(GroupList.SelectedItem.ToString());
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(GroupList.SelectedItem.ToString());
        }
    }
}
