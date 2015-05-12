using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using MemberSignInSystem.ModernUI.ViewModels.Helpers;

namespace MemberSignInSystem.ModernUI.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl, IContent
    {
        public Home()
        {
            // Save common view model for relevant children.
            Application.Current.Resources["LoginViewModel"] = new ViewModels.LoginViewModel();

            InitializeComponent();
        }
        
        //      <----------------keyboard functions---------------->

        // For IContent implementation
        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            // Reset SearchMemberTextBox Foreground in case a theme change occurred
            Login.ResetForeground();
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





// Logo testing
/*foreach (FontFamily f in System.Windows.Media.Fonts.SystemFontFamilies)
{
    Console.Write(f.ToString());
    FormattedText logoText = new FormattedText(
        f.ToString() + ": rvstc",
        System.Globalization.CultureInfo.GetCultureInfo("en-us"),
        FlowDirection.LeftToRight,
        new Typeface(
            f,
            FontStyles.Normal,
            FontWeights.ExtraBold,
            FontStretches.Normal
            ),
        20,
        System.Windows.Media.Brushes.Black
    ); // new FontFamily("Arial")
    Border b = new Border() {
        Background = new SolidColorBrush(AppearanceManager.Current.AccentColor),
        Child = new Path() {
            Data = logoText.BuildGeometry(new Point(0,0)),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Stretch = Stretch.None, Fill = new SolidColorBrush(Colors.White)
        }
                    
    };
    LogoTesting.Items.Add(b);
}*/