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
    /// Interaction logic for StudentsTabControll.xaml
    /// </summary>
    public partial class StudentsTabControll : UserControl
    {
        public StudentsTabControll()
        {
            InitializeComponent();
        }
        public TabItem CreateTabItem(LowTree item)
        {
            StudentList.ItemsSource = item.Students;
            return new TabItem()
            {
                Header = $"{item.Group.Group_Name} Group",
                Content = Panel
            };
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (StudentList.SelectedItem == null) 
            {
                MessageBox.Show("Select item");
                return;
            }

            try 
            {
                if (MessageBoxResult.Yes == MessageBox.Show($"You want to delete student {StudentList.SelectedItem} ", "Delete", MessageBoxButton.YesNo))
                {
                    
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(StudentList.SelectedItem.ToString());
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(StudentList.SelectedItem.ToString());
        }
    }
}
