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
    /// Interaction logic for GroupTabControll.xaml
    /// </summary>
    public partial class GroupTabControll : UserControl
    {
        private ServiceDb<GroupStudent> _groupService;
        private ResourceManager _resources;
        private ObservableCollection<GroupHierarchicalLowTree> _groupListView;
        public GroupTabControll(IServiceScope scope )
        {
            _resources = new ResourceManager("Task8.Resource1", Assembly.GetExecutingAssembly());
            
            _groupListView = new ObservableCollection<GroupHierarchicalLowTree>();

            _groupService = scope.ServiceProvider.GetRequiredService<ServiceDb<GroupStudent>>();
            InitializeComponent();
        }
        public TabItem CreateTabItem(CourseHierarchicaTree item)
        {
            _groupListView = item.Groups;
            groupListUI.ItemsSource = _groupListView;
            return new TabItem()
            {
                Header = $"{item.Courses.Course_Name} course",
                Content = Panel
            };
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (groupListUI.SelectedItem != null && groupListUI.SelectedItem is GroupHierarchicalLowTree)
            {
                GroupHierarchicalLowTree groupBranch = (GroupHierarchicalLowTree)groupListUI.SelectedItem;
                if (MessageBox.Show(_resources.GetString("Delete"), "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _groupListView.Remove(groupBranch);
                    _groupService.Remove(groupBranch.Group);
                    _groupService.Save();
                }
            }
            else
                return;

        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(groupListUI.SelectedItem.ToString());
        }//none

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(groupListUI.SelectedItem.ToString());
        }//none
    }
}
