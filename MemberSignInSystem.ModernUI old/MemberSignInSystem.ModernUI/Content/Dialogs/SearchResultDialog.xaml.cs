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

using System.Data;
using FirstFloor.ModernUI.Windows.Controls;
using MemberSignInSystem.ModernUI.ViewModels.Helpers;
using MemberSignInSystem.ModernUI.ViewModels.Models;

namespace MemberSignInSystem.ModernUI.Content.Dialogs
{
    /// <summary>
    /// Interaction logic for SearchResultDialog.xaml
    /// </summary>
    public partial class SearchResultDialog : UserControl
    {
        ListViewItem selectedItem;

        public SearchResultDialog()
        {
            InitializeComponent();

            DataTable searchResultsTable = DbHelper.SearchHelper;

            List<ListViewItem> searchResultMembers = new List<ListViewItem>();
            foreach (DataRow row in searchResultsTable.Rows)
            {
                ListViewItem lvi = new ListViewItem()
                {
                    Content = new Family(row),
                };
                searchResultMembers.Add(lvi);
            }
            searchResultsListView.ItemsSource = searchResultMembers;
        }

        private void searchResultsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ListViewItem selectedItem = getSelectedListViewItem(sender as ListView);
            
            if (selectedItem == null) // No item selected
            {
                return;
            }

            ModernDialogForMemberSearch parentWindow = Window.GetWindow(this) as ModernDialogForMemberSearch;
            parentWindow.OkButton.Command.Execute(MessageBoxResult.OK);
        }

        private void searchResultsListView_SelectionChanged(object sender, RoutedEventArgs e)
        {
            selectedItem = (sender as ListView).SelectedItem as ListViewItem;

            Family selectedMember = selectedItem.Content as Family;
            DbHelper.SelectedMember = selectedMember; // make selectedMember variable avaliable in other controls

            ModernDialogForMemberSearch parentWindow = Window.GetWindow(this) as ModernDialogForMemberSearch;
            if (parentWindow.OkButton.IsEnabled == false)
            {
                parentWindow.OkButton.IsEnabled = true;
            }
        }
    }
}
/*
        private ListViewItem getSelectedListViewItem(ListView listView)
        {
            List<ListViewItem> items = listView.ItemsSource as List<ListViewItem>;
            foreach (ListViewItem listViewItem in items)
            {
                if (listViewItem.IsSelected)
                {
                    return listViewItem;
                }
            }
            return null;
        }
 */