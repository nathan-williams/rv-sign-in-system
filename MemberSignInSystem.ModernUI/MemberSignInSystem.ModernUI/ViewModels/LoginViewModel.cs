using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Resources;
using System.Windows.Input;
using System.IO;
using MemberSignInSystem.ModernUI.ViewModels.Helpers;
using MemberSignInSystem.ModernUI.ViewModels.Models;
using System.Windows;
using System.Timers;

namespace MemberSignInSystem.ModernUI.ViewModels
{
    class LoginViewModel : NotifyPropertyChanged, IDataErrorInfo
    {
        static String textboxDefaultText = MemberSignInSystem.ModernUI.Properties.Resources.SearchBoxDefaultText;
        private static string query = textboxDefaultText;

        public LoginViewModel() : base()
        {
            // If a sign in list for today already exists, import it.
            string targetUri = "SignIn Records/" + DateTime.Today.ToString("yyyy-MM-dd") + ".xml";
            List<Family> ExistingSignInListForToday = FileStorageHelper.RetrieveFamilyList(targetUri);
            if (ExistingSignInListForToday != null)
            {
                loginHistory = ExistingSignInListForToday;
            }
            else
            {
                loginHistory = new List<Family>();
            }

            memberDisplayVisibilityTimer = new Timer(7000);
            memberDisplayVisibilityTimer.Elapsed += delegate
            {
                MemberDisplayIsVisible = Visibility.Collapsed;
                memberDisplayVisibilityTimer.Stop();
            };
        }

        public string Query
        {
            get { return query; }
            set
            {
                if (query != value)
                {
                    if (ErrorDisplayRule == "TextChanged")
                    {
                        DoDisplayError = false;
                    }

                    query = value;
                    //OnPropertyChanged("Query");
                }
            }
        }

        public ICommand ForceValidateCommand
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    OnPropertyChanged("Query");
                });
            }
        }
        static string errorMessage;
        public static string ErrorMessage { get { return errorMessage; } set { errorMessage = value; } }
        static bool doDisplayError;
        public static bool DoDisplayError { get { return doDisplayError; } set { doDisplayError = value; } }
        static string errorDisplayRule;
        public static string ErrorDisplayRule { get { return errorDisplayRule; } set { errorDisplayRule = value; } }
        
        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Query")
                {
                    if (DoDisplayError == true)
                        return ErrorMessage;
                }
                return null;
            }
        }

        // History view model

        static List<Family> loginHistory;
        // Binding doesn't work if static method below
        public List<Family> LoginHistory
        {
            get { return loginHistory; }
            set
            {
                if (loginHistory != value)
                {
                    loginHistory = value;
                    OnPropertyChanged("LoginHistory");
                }
            }
        }
        public String LoginHistoryReportText
        {
            get { return LoginCount.ToString() + " members logged in today"; }
        }
        private static int LoginCount // Does not count each family more than once
        {
            get
            {
                Dictionary<Int32, Object> dict = new Dictionary<Int32, Object>();
                foreach (Family m in loginHistory)
                {
                    dict[m.Id] = null;
                }
                return dict.Count;
            }
        }
        public bool LoginCountGreaterThanZero // Does not count each family more than once
        {
            get
            {
                return LoginCount > 0;
            }
        }

        public void Login(Family m)
        {
            DisplayedMember = m;

            // Remove all that aren't on this day.
            loginHistory.RemoveAll(delegate(Family fam) { return fam.LoginTime.Day != m.LoginTime.Day; });
            // Only distinguish logins that are 15 minutes apart or more.
            loginHistory.RemoveAll(delegate(Family fam) { return fam.Id == m.Id && (m.LoginTime - fam.LoginTime).Minutes < 15; });
            loginHistory.Add(m);

            // Save loginHistory for today in case of application restart. Then we can retrieve it again later.
            string savePath = "SignIn Records/" + DateTime.Today.ToString("yyyy-MM-dd") + ".xml";
            FileStorageHelper.Save(loginHistory, typeof(List<Family>), savePath);

            return;
        }
        public ICommand ForceUpdateHistoryReportTextCommand
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    OnPropertyChanged("LoginHistoryReportText");
                    OnPropertyChanged("LoginCountGreaterThanZero");
                });
            }
        }
        public ICommand ForceUpdateHistoryCommand
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    OnPropertyChanged("LoginHistory");
                });
            }
        }


        private Timer memberDisplayVisibilityTimer;
        public Timer MemberDisplayVisibilityTimer
        {
            get { return memberDisplayVisibilityTimer; }
        }
        private static Family displayedMember;
        public Family DisplayedMember
        {
            get { return displayedMember; }
            set
            {
                if (displayedMember != value)
                {
                    displayedMember = value;
                    OnPropertyChanged("DisplayedMember");

                    MemberDisplayIsVisible = Visibility.Visible;
                    memberDisplayVisibilityTimer.Stop();
                    memberDisplayVisibilityTimer.Start();
                }
            }
        }
        public ICommand ForceUpdateDisplayedMemberCommand
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    OnPropertyChanged("LoginHistory");
                });
            }
        }

        private Visibility memberDisplayIsVisible = Visibility.Collapsed;
        public Visibility MemberDisplayIsVisible
        {
            get { return memberDisplayIsVisible; }
            set
            {
                memberDisplayIsVisible = value;
                OnPropertyChanged("MemberDisplayIsVisible");
            }
        }
    }
}
