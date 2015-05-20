using FirstFloor.ModernUI.Windows.Controls;
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
using System.Globalization;
using MemberSignInSystem.ModernUI.ViewModels.Helpers;
using System.Timers;

namespace MemberSignInSystem.ModernUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        private void closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DbHelper.closeConnection();
        }

        public MainWindow()
        {
            ApplicationManager.Current.InitializeSettings();

            InitializeComponent();

            DbHelper.verifyConnectionOpen();
            this.Closing += closing;

            AppearanceManager.Current.AccentColor = Color.FromRgb(0x1b, 0xa1, 0xe2);

            /*
            foreach (FontFamily f in System.Windows.Media.Fonts.SystemFontFamilies)
            {
                Console.Write(f.ToString());
            }
            */

            FormattedText logoText = new FormattedText (
                MemberSignInSystem.ModernUI.Properties.Resources.LogoText,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                new Typeface (
                    new FontFamily("Magneto"),
                    FontStyles.Normal,
                    FontWeights.ExtraBold,
                    FontStretches.Normal
                    ),
                50,
                System.Windows.Media.Brushes.Black
            ); // Font size (50) and brush color (black) are arbitrary.
            this.LogoData = logoText.BuildGeometry(new System.Windows.Point(0, 0));

            // Declare keyboard listener
            Keyboard.AddPreviewKeyDownHandler(this, previewKeyEventHandler);

            keyListenerResetTimer.AutoReset = false;
            keyListenerResetTimer.Elapsed += delegate
            {
                if (listen)
                {
                    (Application.Current.Resources["SubitCommand"] as RelayCommand).Execute(query);
                }

                g = false;
                ctrl = false;
                listen = false;
                query = "";
                keyListenerResetTimer.Stop();
            };
        }
        
        //      <----------------keyboard functions---------------->

        Timer keyListenerResetTimer = new Timer(50);
        bool g, ctrl, listen;
        private string query = "";
        private void previewKeyEventHandler(object sender, KeyEventArgs e)
        {
            if (listen)
            {
                query += (new KeyConverter()).ConvertToString(e.Key);
            }
            // Check for when ctrl and g are pressed within <intervalCtrlAndG> ticks of one another.
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                keyListenerResetTimer.Start();
                ctrl = true;
            }
            if (e.Key == Key.G)
            {
                keyListenerResetTimer.Start();
                g = true;
            }
            if (g && ctrl) // Scanner inputting data, not user
            {
                listen = true;
            }
        }
        
    }
}

/*
 
            if (listen)
            {
                query += (new KeyConverter()).ConvertToString(e.Key);
            }
            // Check for when ctrl and g are pressed within <intervalCtrlAndG> ticks of one another.
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                ctrl = true;
            }
            if (e.Key == Key.G)
            {
                g = true;
            }
            if (ctrl && g)
            {
                listen = true;
            }
            if (listen && !g && !ctrl)
            {
                (Application.Current.Resources["SubitCommand"] as RelayCommand).Execute(query);
                listen = false;
            }
 
 */