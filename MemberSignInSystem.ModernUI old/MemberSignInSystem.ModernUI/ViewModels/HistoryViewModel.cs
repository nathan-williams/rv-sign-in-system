using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;
using MemberSignInSystem.ModernUI.ViewModels.Helpers;
using MemberSignInSystem.ModernUI.ViewModels.Models;
using System.Windows;

namespace MemberSignInSystem.ModernUI.ViewModels
{
    class HistoryViewModel : NotifyPropertyChanged
    {
        public HistoryViewModel() : base() { }

        private DateTime displayDate = DateTime.Now;
        public DateTime DisplayDate
        {
            get { return displayDate; }
            set
            {
                if (displayDate != value)
                {
                    displayDate = value;
                    OnPropertyChanged("DisplayDate");

                    if (displayDate.Date == DateTime.Today)
                    {
                        LoginViewModel loginViewModel = Application.Current.Resources["LoginViewModel"] as LoginViewModel;
                        displayedHistory = loginViewModel.LoginHistory;
                    }
                    else
                    {
                        string targetUri = "SignIn Records/" + displayDate.ToString("yyyy-MM-dd") + ".xml";
                        displayedHistory = FileStorageHelper.RetrieveFamilyList(targetUri);
                    }

                    OnPropertyChanged("DisplayedHistory");
                }
            }
        }
        private List<Family> displayedHistory = (Application.Current.Resources["LoginViewModel"] as LoginViewModel).LoginHistory;
        public List<Family> DisplayedHistory
        {
            get { return displayedHistory; }
            set
            {
                if (displayedHistory != value)
                {
                    displayedHistory = value;
                    OnPropertyChanged("DisplayedHistory");
                }
            }
        }
    }
}
