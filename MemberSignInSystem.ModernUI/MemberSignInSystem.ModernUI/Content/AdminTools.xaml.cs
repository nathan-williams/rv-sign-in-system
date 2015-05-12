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
using FirstFloor.ModernUI.Windows.Navigation;

namespace MemberSignInSystem.ModernUI.Content
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class AdminTools : UserControl//, IContent
    {
        public AdminTools()
        {
            // Register command for BBCodeBlock
            this.MyCommand = new RelayCommand(o => RefreshDatabase(o));
            this.LinkNavigator = new DefaultLinkNavigator();
            this.LinkNavigator.Commands.Add(
                new Uri("cmd://refreshdatabase/", UriKind.Absolute), this.MyCommand);

            InitializeComponent();
        }

        private void RefreshDatabase(object o)
        {

            //var frame = NavigationHelper.FindFrame(null, this);
            //frame.ProgressBarOn();

            // See if sky drive
            
        }
        public RelayCommand MyCommand { get; private set; }
        public ILinkNavigator LinkNavigator { get; private set; }
    }
}