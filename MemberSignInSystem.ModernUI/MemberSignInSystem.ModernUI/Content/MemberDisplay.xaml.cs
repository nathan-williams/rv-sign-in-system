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

using MemberSignInSystem.ModernUI.ViewModels;

using System.Timers;

namespace MemberSignInSystem.ModernUI.Content
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class MemberDisplay : UserControl
    {
        LoginViewModel viewModel;

        public MemberDisplay()
        {
            // Get view model
            viewModel = Application.Current.Resources["LoginViewModel"] as LoginViewModel;
            this.DataContext = viewModel;

            InitializeComponent();
        }

    }
}
