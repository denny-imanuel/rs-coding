using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace RohdeSchwarz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TreeViewModel viewModel;
        private List<string> checkedItems;

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new TreeViewModel();
            DataContext = viewModel;
            checkedItems = new List<string>();

        }

        

        private void btnCollape_Click(object sender, RoutedEventArgs e)
        {
            // collapsed all item
            foreach (var item in myTreeView.Items)
            {
                var treeItem = myTreeView.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                treeItem.IsExpanded = false;
            }
        }

        private void btnExpand_Click(object sender, RoutedEventArgs e)
        {
            // expand all item
            foreach(var item in myTreeView.Items)
            {
                var treeItem = myTreeView.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                treeItem.IsExpanded = true;
            }
        }

        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            // get all checked item
            checkedItems = new List<string>();
            getCheckedItem(viewModel.TreeModels);
            if (checkedItems.Count > 0 )
            {
                btnBack.IsEnabled = true;
                btnStart.IsEnabled = true;
            }
            else
            {
                btnBack.IsEnabled = false;
                btnStart.IsEnabled = false;
            }
        }

        private void getCheckedItem(ObservableCollection<TreeModel> treeItems)
        {
            // recursively get checked item
            foreach (var item in treeItems)
            {
                if (item.IsChecked == true)
                {
                    checkedItems.Add(item.Name);
                }
                getCheckedItem(item.Children);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            // reset all item
            DataContext = null;
            viewModel = new TreeViewModel();
            DataContext = viewModel;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            // get all checked item
            checkedItems = new List<string>();
            getCheckedItem(viewModel.TreeModels);
            // display all checked item
            var sbuilder = new StringBuilder();
            foreach(var str in checkedItems)
            {
                sbuilder.AppendLine(str);
            }
            MessageBox.Show(sbuilder.ToString());
        }
    }
}
