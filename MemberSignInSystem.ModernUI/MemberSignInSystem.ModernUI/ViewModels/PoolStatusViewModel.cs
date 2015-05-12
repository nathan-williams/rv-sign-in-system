using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

using System.Windows.Input;
using System.Windows;
using System.Timers;

namespace MemberSignInSystem.ModernUI.ViewModels
{
    class PoolStatusViewModel : NotifyPropertyChanged
    {
        #region Declaration of bidning variables

        private List<Pool> poolStatuses = new List<Pool>();
        private bool isInEditMode;
        private ICommand toggleEditModeCommand;
        private Geometry editIconData = Geometry.Parse("F1 M 42.7499,25.3335L 50.6666,33.2501L 31.6667,52.25L 23.75,44.3334L 42.7499,25.3335 Z M 52.1704,31.6664L 44.3333,23.8293L 47.6921,20.4706C 48.9288,19.2339 50.9338,19.2339 52.1705,20.4706L 55.5292,23.8293C 56.7659,25.066 56.7659,27.071 55.5292,28.3077L 52.1704,31.6664 Z M 21.7709,55.0207L 20.9792,54.2291L 23.0573,47.5988L 28.4011,52.9426L 21.7709,55.0207 Z ");
        private Geometry saveIconData = Geometry.Parse("F1 M 20.5833,20.5833L 55.4167,20.5833L 55.4167,55.4167L 45.9167,55.4167L 45.9167,44.3333L 30.0833,44.3333L 30.0833,55.4167L 20.5833,55.4167L 20.5833,20.5833 Z M 33.25,55.4167L 33.25,50.6667L 39.5833,50.6667L 39.5833,55.4167L 33.25,55.4167 Z M 26.9167,23.75L 26.9167,33.25L 49.0833,33.25L 49.0833,23.75L 26.9167,23.75 Z ");
        private Geometry leaveIconData = Geometry.Parse("F1 M 51.0071,19.0027L 51.0071,27.0038L 48.0067,27.0038L 48.0067,22.0031L 25.0035,22.0031L 25.0035,54.0075L 48.0067,54.0075L 48.0067,49.0068L 51.0071,49.0068L 51.0071,57.008L 22.1698,57.008L 22.0031,19.0027L 51.0071,19.0027 Z M 30.0042,35.0049L 45.2563,35.0049L 37.005,28.0039L 47.0066,28.0039L 58.5082,38.0053L 47.0066,48.0067L 37.005,48.0067L 45.2563,41.0057L 30.0042,41.0057L 30.0042,35.0049 Z ");
        private string leaveEditModeToolTip = MemberSignInSystem.ModernUI.Properties.Resources.LeaveEditModeToolTip;
        private string enterEditModeToolTip = MemberSignInSystem.ModernUI.Properties.Resources.EnterEditModeToolTip;
        private static DateTime nullHolder = new DateTime(7); // 7 is an arbitrary number, it just can't be a whole number of days
        private DateTime timeOfLastUpdate = nullHolder; // Units past hour (and minute) do not matter (i.e. ticks, seconds).
        private bool isPoolClosed = false;
        private DateTime timeOfReopen;
        private Timer poolClosedTimerUpdater = new Timer(1000); // Updates timer by the second.
        private List<PoolClosureDuration> poolClosureDurationOptions; // In minutes
        private double selectedPoolClosureDuration; // In minutes

        #endregion

        #region Binding methods

        public bool IsPoolClosed
        {
            get { return isPoolClosed; }
            set 
            { 
                if (isPoolClosed != value)
                {
                    isPoolClosed = value;
                    OnPropertyChanged("IsPoolClosed");
                    OnPropertyChanged("IsPoolOpen");
                }
            }
        }
        public bool IsPoolOpen
        {
            get { return !isPoolClosed; }
            set
            {
                if (isPoolClosed != !value)
                {
                    isPoolClosed = !value;
                    OnPropertyChanged("IsPoolClosed");
                    OnPropertyChanged("IsPoolOpen");
                }
            }
        }
        public Visibility IsPoolClosedSignVisible
        {
            get
            {
                if (isPoolClosed == true && !isInEditMode)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }
        public Visibility IsOpenClosedVisible
        {
            get
            {
                if (isInEditMode == true)
                    return Visibility.Visible;
                if (isInEditMode == false)
                    return Visibility.Hidden;
                return Visibility.Collapsed;
            }
        }
        public string PoolClosedTimerString
        {
            get
            {
                TimeSpan timeLeft = timeOfReopen - DateTime.Now;
                string minutes = timeLeft.Minutes < 10 ? "0" + timeLeft.Minutes.ToString() : timeLeft.Minutes.ToString();
                string seconds = timeLeft.Seconds < 10 ? "0" + timeLeft.Seconds.ToString() : timeLeft.Seconds.ToString();
                return minutes + ":" + seconds;
            }
        }
        public Visibility IsTimerVisible
        {
            get
            {
                if (selectedPoolClosureDuration == double.MaxValue)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }
        public double SelectedPoolClosureDuration
        {
            get
            {
                return selectedPoolClosureDuration;
            }
            set
            {
                if (selectedPoolClosureDuration != value)
                {
                    selectedPoolClosureDuration = value;
                }
            }
        }
        public List<PoolClosureDuration> PoolClosureDurationOptions
        {
            get
            {
                return poolClosureDurationOptions;
            }
        }

        public string TimeOfLastUpdateString
        {
            get
            {
                if(timeOfLastUpdate == nullHolder)
                    return "No available data";
                return "Last Updated " + timeOfLastUpdate.ToString("h tt");
            }
        }

        public List<Pool> PoolStatuses
        {
            get { return poolStatuses; }
        }

        public bool IsInEditMode
        {
            get { return isInEditMode; }
            set
            {
                if (isInEditMode != value)
                {
                    isInEditMode = value;

                    if (isInEditMode == false) // Leaving edit mode
                    {
                        if (isPoolClosed == true && selectedPoolClosureDuration != double.MaxValue)
                        {
                            timeOfReopen = DateTime.Now.AddMinutes(selectedPoolClosureDuration);
                            poolClosedTimerUpdater.Elapsed += delegate
                            {
                                if (timeOfReopen.CompareTo(DateTime.Now) < 0) // Pool should be open
                                {
                                    poolClosedTimerUpdater.Stop();
                                    IsPoolClosed = false;
                                }
                                OnPropertyChanged("PoolClosedTimerString");
                                OnPropertyChanged("IsPoolClosedSignVisible");
                                OnPropertyChanged("IsTimerVisible");
                                OnPropertyChanged("PoolClosedTimerString");
                            };
                            poolClosedTimerUpdater.Start();
                        }
                    }
                    foreach (Pool pool in poolStatuses)
                    {
                        foreach (PoolVariable var in pool.PoolVars)
                        {
                            if (var.Value == "" || var.Value == "-") // Variable value is one of the holders
                            {
                                if (isInEditMode == false) // Leaving edit mode
                                {
                                    var.Value = "-";
                                }
                                else if (isInEditMode == true) // Entering edit mode
                                {
                                    var.Value = "";
                                }
                            }
                        }
                    }

                    OnPropertyChanged("TimeOfLastUpdateString");
                    OnPropertyChanged("IsInEditMode");
                    OnPropertyChanged("IsNotInEditMode");
                    OnPropertyChanged("IsOpenClosedVisible");
                    OnPropertyChanged("IsPoolClosedSignVisible");
                    OnPropertyChanged("IsTimerVisible");
                    OnPropertyChanged("PoolClosedTimerString");
                    OnPropertyChanged("EditButtonToolTip");
                    OnPropertyChanged("IconData");
                    OnPropertyChanged("TimeUntilReopen");
                }
            }
        }
        public bool IsNotInEditMode
        {
            get { return !isInEditMode; }
        }

        private void toggleIsInEditMode()
        {
            if (isInEditMode == true) // Changing from edit mode to display mode.
            {
                timeOfLastUpdate = DateTime.Now;
                if (timeOfLastUpdate.Minute > 40) // Round to next hour if within 20 minutes.
                {
                    timeOfLastUpdate = timeOfLastUpdate.AddHours(1);
                }
            }
            IsInEditMode = !isInEditMode;

        }

        public ICommand ToggleEditModeCommand
        {
            get { return toggleEditModeCommand; }
        }

        public string EditButtonToolTip
        {
            get
            {
                if (isInEditMode == false)
                    return enterEditModeToolTip;
                if (isInEditMode == true)
                    return leaveEditModeToolTip;
                return "";
            }
        }

        public Geometry IconData
        {
            get
            {
                if (isInEditMode == true)
                    return leaveIconData;
                if (isInEditMode == false)
                    return editIconData;
                return null;
            }
        }

        #endregion

        public PoolStatusViewModel() : base()
        {
            Application.Current.Resources["PoolStatusViewModel"] = this;

            toggleEditModeCommand = new RelayCommand((o) => toggleIsInEditMode());

            // Test
            this.poolStatuses = MemberSignInSystem.ModernUI.ViewModels.Helpers.ApplicationManager.Current.PoolStatuses;
            this.poolClosureDurationOptions = new List<PoolClosureDuration>()
            {
                new PoolClosureDuration(1),
                new PoolClosureDuration(15),
                new PoolClosureDuration(30),
                new PoolClosureDuration(45),
                new PoolClosureDuration(double.MaxValue)
            };
            selectedPoolClosureDuration = 45; // Default pool closure duration

            IsInEditMode = false; // Sets varValues to correct values.
            // Test
        }
    }

    [Serializable]
    public class Pool : NotifyPropertyChanged
    {
        public Pool(string name, List<PoolVariable> vars)
        {
            poolName = name;
            poolVars = vars;
        }
        public Pool()
        {
            poolName = "";
            PoolVars = new List<PoolVariable>();
        }

        private string poolName;
        public string PoolName
        {
            get { return poolName; }
            set { poolName = value; OnPropertyChanged("PoolName"); }
        }
        private List<PoolVariable> poolVars = new List<PoolVariable>();
        public List<PoolVariable> PoolVars
        {
            get { return poolVars; }
            set
            {
                poolVars = value;
                OnPropertyChanged("PoolVars");
            }
        }
    }

    [Serializable]
    public class PoolVariable : NotifyPropertyChanged
    {
        public PoolVariable(string name, string value)
        {
            this.name = name;
            if (value == "" && Application.Current.Resources["PoolStatusViewModel"] as PoolStatusViewModel != null && !(Application.Current.Resources["PoolStatusViewModel"] as PoolStatusViewModel).IsInEditMode)
                this.value = "-";
            this.value = value;
        }
        public PoolVariable(string n)
        {
            this.name = n;
            if (Application.Current.Resources["PoolStatusViewModel"] != null)
                if (!(Application.Current.Resources["PoolStatusViewModel"] as PoolStatusViewModel).IsInEditMode)
                    this.value = "-";
                else
                    this.value = "";
            else
                this.value = "-";
        }
        public PoolVariable()
        {
            if (Application.Current.Resources["PoolStatusViewModel"] != null)
                if (!(Application.Current.Resources["PoolStatusViewModel"] as PoolStatusViewModel).IsInEditMode)
                {
                    this.name = "-";
                    this.value = "-";
                }
                else
                {
                    this.name = "";
                    this.value = "";
                }
            else
            {
                this.name = "-";
                this.value = "-";
            }
        }
        
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        private string value;
        public string Value
        {
            get
            {
                return value;
            }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    OnPropertyChanged("Value");
                }
            }
        }
    }
    class PoolClosureDuration
    {
        private double closureDuration;
        public double ClosureDuration
        {
            get
            {
                return closureDuration;
            }
        }
        public string OptionString
        {
            get
            {
                if (closureDuration == double.MaxValue)
                {
                    return "∞";
                }
                return Convert.ToInt32(closureDuration).ToString();
            }
        }
        public PoolClosureDuration(double duration)
        {
            closureDuration = duration;
        }
    }
}
