using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FirstFloor.ModernUI.Windows.Controls
{
    /// <summary>
    /// Represents a Modern UI styled dialog window.
    /// </summary>
    public class ModernDialogForLoginHistory
        : ModernDialog
    {
        private ICommand closeCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModernDialog"/> class.
        /// </summary>
        public ModernDialogForLoginHistory()
        {
            this.DefaultStyleKey = typeof(ModernDialog);
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            this.closeCommand = new RelayCommand(o => {
                var result = o as MessageBoxResult?;
                if (result.HasValue) {
                    this.Result = result.Value;
                }
                Close();
            });

            this.Buttons = CustomButtonList();

            // set the default owner to the app main window (if possible)
            if (Application.Current != null && Application.Current.MainWindow != this) {
                this.Owner = Application.Current.MainWindow;
            }
        }
        private List<Button> CustomButtonList()
        {
            List<Button> buttons = new List<Button>();
            
            Button button1 = new Button
            {
                Content = FirstFloor.ModernUI.Resources.Close,
                Command = this.CloseCommand,
                CommandParameter = MessageBoxResult.Cancel,
                IsDefault = false,
                IsCancel = true,
                MinHeight = 21,
                MinWidth = 65,
                Margin = new Thickness(4, 0, 0, 0)
            };
            buttons.Add(button1);
            
            return buttons;
        }

        private Button CreateCloseDialogButton(string content, bool isDefault, bool isCancel, MessageBoxResult result)
        {
            return new Button {
                Content = content,
                Command = this.CloseCommand,
                CommandParameter = result,
                IsDefault = isDefault,
                IsCancel = isCancel,
                MinHeight = 21,
                MinWidth = 65,
                Margin = new Thickness(4, 0, 0, 0)
            };
        }

        // My additions; pragmas necessary since xml commments not possible
        #pragma warning disable 1591
        public MessageBoxResult Result
        {
            get { return dialogRes; }
            set { dialogRes = value; }
        }
        #pragma warning restore 1591
    }
}
