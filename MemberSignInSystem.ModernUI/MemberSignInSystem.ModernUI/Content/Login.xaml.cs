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

using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using MemberSignInSystem.ModernUI.ViewModels;
using MemberSignInSystem.ModernUI.ViewModels.Helpers;
using MemberSignInSystem.ModernUI.ViewModels.Models;

using System.Diagnostics; // Stopwatch
using System.Resources;
using System.Speech.Synthesis;
using System.Data;

namespace MemberSignInSystem.ModernUI.Content
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        // Create a resource manager to retrieve resources.
        static ResourceManager rm = MemberSignInSystem.ModernUI.Properties.Resources.ResourceManager;
        static String textboxDefaultText = rm.GetString("SearchBoxDefaultText");
        private System.Speech.Synthesis.SpeechSynthesizer speaker;
        private static LoginViewModel viewModel;

        public Login()
        {
            // Get view model
            viewModel = Application.Current.Resources["LoginViewModel"] as LoginViewModel;
            this.DataContext = viewModel;

            InitializeComponent();

            // Save reference to SearchMemberTextBox for scanner functionality
            Application.Current.Resources["TextBoxDefaultStateCommand"] = new RelayCommand((o) => this.Dispatcher.BeginInvoke(new Action(textboxSetDefaultState)));
            Application.Current.Resources["SearchMemberTextBox"] = SearchMemberTextBox;
            Application.Current.Resources["SubitCommand"] = new RelayCommand((o) => submit(o as String));

            /*Loaded += delegate
            {
                // Lock in SearchMemberTextBox width after initialization
                double w = SearchMemberTextBox.ActualWidth;
                SearchMemberTextBox.Width = w * 1.1;
            };*/
            // Set member search textbox to default state
            textboxSetDefaultState();

            // Submit if <Enter> key pressed
            SearchMemberTextBox.InputBindings.Add(new InputBinding(new RelayCommand(o =>
            {
                string q = SearchMemberTextBox.Text;
                if (q != "")
                {
                    if (submit(q) == 1)
                        SearchMemberTextBox.Clear();
                    return;
                }
                if (q == "")
                {
                    LoginViewModel.DoDisplayError = true;
                    LoginViewModel.ErrorMessage = "Must contain text";
                    LoginViewModel.ErrorDisplayRule = "TextChanged";
                }

                viewModel.ForceValidateCommand.Execute(null);
            }), new KeyGesture(Key.Enter)));

            // Declare voice synthesizer
            speaker = new SpeechSynthesizer();
        }

        //      <----------------textbox functions---------------->

        public void ResetForeground()
        {
            // Textbox is in default state
            if (SearchMemberTextBox.Text == textboxDefaultText)
                SearchMemberTextBox.SetResourceReference(TextBox.ForegroundProperty, "InputTextWatermark");
            // Textbox is in input state
            else
                SearchMemberTextBox.SetResourceReference(TextBox.ForegroundProperty, "InputText");
        }

        private void textboxSetDefaultState()
        {
            SearchMemberTextBox.Text = textboxDefaultText;

            // Set textbox foreground to theme's watermark brush
            SearchMemberTextBox.Foreground = FindResource("InputTextWatermark") as SolidColorBrush;

            SearchMemberTextBox.CaretBrush = SearchMemberTextBox.Background;
            System.Windows.Forms.Cursor.Show();
        }
        private void textboxSetInputState()
        {
            SearchMemberTextBox.Clear();

            // Set textbox foreground to theme's regular InputText brush
            SearchMemberTextBox.Foreground = FindResource("InputText") as SolidColorBrush;

            SearchMemberTextBox.CaretBrush = SearchMemberTextBox.Foreground;
        }
        private void gotMouseCapture(object sender, RoutedEventArgs e)
        {
            // Put textbox in input state
            if (SearchMemberTextBox.Text == textboxDefaultText)
                textboxSetInputState();
        }
        private void lostFocus(object sender, RoutedEventArgs e)
        {
            // Put textbox back in default state
            if (SearchMemberTextBox.Text == "")
                textboxSetDefaultState();
        }
        // If textbox reset by Esc key, and input afterwards, then do this
        private void previewKeyDown(object sender, KeyEventArgs e)
        {
            // Put textbox in input state
            if (SearchMemberTextBox.Text == textboxDefaultText)
                textboxSetInputState();
            // Check for reset key (Esc)
            if (e.Key == Key.Escape && SearchMemberTextBox.Text == "")
                textboxSetDefaultState();
        }

        //      <----------------database comms---------------->

        private int submit(String query)
        {
            // 2 = individual id ; 1 = family id ; 0 = other, should be name
            int queryType = DbHelper.classify(query);

            // Get things from the database, which
            // Stores result in the SearchHelper Data Table of DbHelper class.
            /*if (queryType == 2) // Query is an individual id
            {
                DbHelper.searchByIdForIndividuals(query.Replace(" ", "").Replace("%", ""));
            }*/
            if (queryType == 1) // Query is a family id
            {
                DbHelper.searchByIdForFamilies(query.Replace(" ", ""));
            }
            if (queryType == 0) // Query is a name
            {
                String[] queries = query.Split(' ');
                DataTable result = null;
                foreach (String q in queries)
                {
                    if (q == "") continue;
                    DataTable toAdd = DbHelper.searchByName(q);
                    if (result == null) result = toAdd;
                    else result.Merge(toAdd);
                }
                result = DbHelper.RemoveDupes(result);
                if (result == null) result = new DataTable();
                DbHelper.copyToSearchHelper(result);
            }

            DataTable table = DbHelper.SearchHelper;

            if (table == null) return -1;

            int numberOfRowsInQueryResult = table.Rows.Count;
            Family selectedMember = getMemberSelection(numberOfRowsInQueryResult);

            if (selectedMember != null)
            {
                selectedMember.LoginTime = DateTime.Now;

                viewModel.Login(selectedMember);
                viewModel.ForceUpdateHistoryReportTextCommand.Execute(null);

                // show installed voices
                foreach (var v in speaker.GetInstalledVoices().Select(v => v.VoiceInfo))
                {
                    Console.WriteLine("Name:{0}, Gender:{1}, Age:{2}",
                      v.Description, v.Gender, v.Age);
                }

                speaker.SpeakAsync(selectedMember.Greeting);

                return 1;
            }
            if (selectedMember == null)
            {
                /*if (numberOfRowsInQueryResult > 0) // User cancelled
                {
                    db.DoDisplayError = true;
                    db.ErrorMessage = "Cancelled";
                    db.ErrorDisplayRule = "TextChanged";

                    db.ForceValidateCommand.Execute(null);
                }*/
                if (numberOfRowsInQueryResult == 0) // Search returned no results
                {
                    if (LoginViewModel.DoDisplayError != true) // if Error not already set in DbHelper due to unavailability of database file
                    {
                        LoginViewModel.DoDisplayError = true;
                        LoginViewModel.ErrorMessage = "No members match the criteria";
                        LoginViewModel.ErrorDisplayRule = "TextChanged";

                        viewModel.ForceValidateCommand.Execute(null);
                    }
                }
            }
            return -1;
        }
        private Family getMemberSelection(int numRows)
        {
            ModernDialogForMemberSearch searchResultModernDialog = null;
            if (numRows == 0) // Search returned no results
            {
                return null;
            }
            if (numRows == 1) // Search returned one result, we have our selectedMember
            {
                return DbHelper.SelectedMember;
            }
            if (numRows > 1) // Search returned several results, user must choose
            {
                searchResultModernDialog = new ModernDialogForMemberSearch()
                {
                    Title = rm.GetString("SearchResultDialogTitle"),
                    Content = new Dialogs.SearchResultDialog()
                };
                searchResultModernDialog.ShowDialog();

                if (searchResultModernDialog.Result == MessageBoxResult.OK) // User clicked Ok or Double Clicked an item
                {
                    return DbHelper.SelectedMember;
                }
                // User clicked Cancel
                return null;
            }
            return null;
        }

        private void OpenLoginHistoryDialog(object sender, RoutedEventArgs e)
        {
            ModernDialogForLoginHistory loginHistoryDialog = new ModernDialogForLoginHistory()
            {
                Title = rm.GetString("LoginHistoryDialogTitle"),
                Content = new Dialogs.LoginHistoryDialog()
            };
            loginHistoryDialog.ShowDialog();
        }

        //      <----------------button listeners---------------->
        
    }
}
