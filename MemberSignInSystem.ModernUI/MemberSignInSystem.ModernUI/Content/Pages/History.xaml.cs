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

using FirstFloor.ModernUI.Windows;
using MemberSignInSystem.ModernUI.ViewModels;
using System.IO;
using MemberSignInSystem.ModernUI.ViewModels.Helpers;

namespace MemberSignInSystem.ModernUI.Content.Pages
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : UserControl, IContent
    {
        static DateTime startDate, endDate; // Injected into HistoryDisplayDatePicker in validateBlackoutDates method
        public History()
        {
            InitializeComponent();

            validateBlackoutDates();
        }

        public void validateBlackoutDates()
        {
            this.HistoryDisplayDatePicker.BlackoutDates.Clear();
            foreach (CalendarDateRange range in GetBlackoutDates())
            {
                try
                {
                    this.HistoryDisplayDatePicker.BlackoutDates.Add(range);
                }
                catch(Exception e)
                {
                    Console.Write(e.ToString());
                }
            }

            this.HistoryDisplayDatePicker.DisplayDateStart = startDate;
            this.HistoryDisplayDatePicker.DisplayDateEnd = endDate;
        }

        private static List<CalendarDateRange> GetBlackoutDates()
        // Checks what dates have a file availble in the SignInRecords directory.
        {
            string directoryUri = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "SignIn Records\\");

            // Create SignIn Records folder if it doesn't already exist
            bool folderExists = Directory.Exists(directoryUri);
            if (!folderExists) Directory.CreateDirectory(directoryUri);

            List<DateTime> available = getAvailableDates(directoryUri);
            List<CalendarDateRange> unavailable = getUnavailableDates(available);

            startDate = new DateTime(available.FirstOrDefault().Year, 1, 1);
            endDate = new DateTime(available.LastOrDefault().Year, 12, 31);

            return unavailable;
        }
        private static List<DateTime> getAvailableDates(string directoryUri)
        {
            List<DateTime> available = new List<DateTime>();

            foreach (string filename in Directory.GetFiles(directoryUri, "*.xml"))
            {
                string simpleFileName = Path.GetFileNameWithoutExtension(filename);
                DateTime currentFileDateTime = Convert.ToDateTime(simpleFileName);
                available.Add(currentFileDateTime);
            }
            
            available.Add(DateTime.Today);
            available.Sort();

            return available;
        }
        private static List<CalendarDateRange> getUnavailableDates(List<DateTime> available)
        {
            List<CalendarDateRange> unavailable = new List<CalendarDateRange>()
            {
                new CalendarDateRange(DateTime.MinValue,available.FirstOrDefault().AddDays(-1)),
                new CalendarDateRange(available.LastOrDefault().AddDays(1),DateTime.MaxValue)
            };

            DateTime nullHolder = new DateTime(7); // 7 is an arbitrary number, it just can't be a whole number of days
            DateTime previousAvailableDate = nullHolder;
            foreach (DateTime availableDate in available)
            {
                if (previousAvailableDate != nullHolder)
                {
                    DateTime start = previousAvailableDate.AddDays(1);
                    DateTime end = availableDate.AddDays(-1);
                    if (start > end)
                        continue;
                    if (start == end)
                        unavailable.Add(new CalendarDateRange(start));
                    if (start < end)
                        unavailable.Add(new CalendarDateRange(start, end));
                }

                previousAvailableDate = availableDate;
            }

            return unavailable;
        }

        // For IContent implementation
        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            // Refresh binding
            loginHistoryList.Items.Refresh();

            // Resize columns
            GridView gv = loginHistoryList.View as GridView;
            if (gv != null)
            {
                foreach (var c in gv.Columns)
                {
                    // Same code that is executed when gripper is double clicked
                    if (double.IsNaN(c.Width))
                    {
                        c.Width = c.ActualWidth;
                    }
                    c.Width = double.NaN;
                }
            }

            // Set DisplayDate to current day each time user navigates here
            (this.DataContext as HistoryViewModel).DisplayDate = DateTime.Today;

            return;
        }
        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e) { return; }
        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e) { return; }
        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (e.Source.OriginalString == "SyncDatabaseDumbyURI")
            {
                e.Cancel = true;
                SkyDriveHelper.Sync();
            }
            return; 
        }
    }
}
