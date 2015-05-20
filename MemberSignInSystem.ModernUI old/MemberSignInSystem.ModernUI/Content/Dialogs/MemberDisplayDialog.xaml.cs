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

using MemberSignInSystem.ModernUI.ViewModels.Models;

namespace MemberSignInSystem.ModernUI.Content.Dialogs
{
    /// <summary>
    /// Interaction logic for MemberDisplayDialog.xaml
    /// </summary>
    public partial class MemberDisplayDialog : UserControl
    {
        public MemberDisplayDialog(Family i)
        {
            this.DataContext = i;
            InitializeComponent();
        }
    }
}
