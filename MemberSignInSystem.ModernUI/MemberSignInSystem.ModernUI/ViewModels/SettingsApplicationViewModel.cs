using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FirstFloor.ModernUI.Presentation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xaml;
using MemberSignInSystem.ModernUI.ViewModels.Helpers;
using System.ComponentModel;
using System.Windows.Input;
using MemberSignInSystem.ModernUI.Content;
using Elysium.Controls;

namespace MemberSignInSystem.ModernUI.ViewModels
{
    class SettingsApplicationViewModel : NotifyPropertyChanged, IDataErrorInfo
    {
        private List<SettingGroup> settingGroups;
        public List<SettingGroup> SettingGroups
        {
            get { return settingGroups; }
            set
            {
                if (settingGroups != value)
                {
                    settingGroups = value;
                    OnPropertyChanged("SettingGroups");
                }
            }
        }

        private SettingGroup pool1StatsSettingGroup, pool2StatsSettingGroup;
        private Setting pool1NameSetting, pool2NameSetting;
        private Setting pool1NumVarsSetting, pool2NumVarsSetting;
        private Setting pool1VarNamesSetting, pool2VarNamesSetting;

        public SettingsApplicationViewModel()
        {
            SyncWithApplicationManager();

            #region Root Folder setting group

            SettingGroup rootFolderSettingGroup = new SettingGroup("Root Folder", new List<Setting>
            {
                new Setting("Location", ChooseRootFolderUIEl()),
            });

            #endregion

            #region FlipImageGrid setting group

            SettingGroup flipImageGridSettingGroup = new SettingGroup("Flip Image Grid", new List<Setting>
            {
                new Setting("Pictures Folder", ChooseDirectoryUIEl()),
                new Setting("Transition Type", TransitionTypeUIEl()),
                new Setting("Just One Image?", OneImageBoolUIEl()),
            });

            #endregion

            #region Location setting group

            SettingGroup weatherAppletSettingGroup = new SettingGroup("Weather Applet", new List<Setting>
            {
                new Setting("Location", LocationUIEl()),
                new Setting("Units", UnitsUIEl()),
            });

            #endregion

            #region Pool Statuses setting groups

            #region Default values

            pool1Name = "Main Pool";
            pool2Name = "Wading Pool";
            pool1NumVars = 3;
            pool2NumVars = 2;
            pool1VarNames = new List<PoolVariable> { new PoolVariable("pH"), new PoolVariable("Cl"), new PoolVariable("Water Temp") };
            pool2VarNames = new List<PoolVariable> { new PoolVariable("pH"), new PoolVariable("Cl") };

            #endregion

            pool1NameSetting = new Setting("Name", PoolNameUIEl(1));
            pool2NameSetting = new Setting("Name", PoolNameUIEl(2));
            pool1NumVarsSetting = new Setting("# of Variables", PoolNumVarsUIEl(1));
            pool2NumVarsSetting = new Setting("# of Variables", PoolNumVarsUIEl(2));
            pool1VarNamesSetting = new Setting("Variable Names", PoolVarNamesUIEl(1));
            pool2VarNamesSetting = new Setting("Variable Names", PoolVarNamesUIEl(2));

            pool1StatsSettingGroup = new SettingGroup(pool1Name + " Stats", new List<Setting>
            {
                pool1NameSetting,
                pool1NumVarsSetting,
                pool1VarNamesSetting,
            });
            pool2StatsSettingGroup = new SettingGroup(pool2Name + " Stats", new List<Setting>
            {
                pool2NameSetting,
                pool2NumVarsSetting,
                pool2VarNamesSetting,
            });

            #endregion

            settingGroups = new List<SettingGroup>
            {
                rootFolderSettingGroup,
                flipImageGridSettingGroup,
                weatherAppletSettingGroup,
                pool1StatsSettingGroup,
                pool2StatsSettingGroup,
            };

            ApplicationManager.Current.PropertyChanged += OnApplicationManagerPropertyChanged;
        }
        private void SyncWithApplicationManager()
        {
            SyncRootFolderSettings();
            SyncFlipImageGridSettings();
            SyncWeatherAppletSettings();
            SyncPoolSettings();
        }
        private void OnApplicationManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "RootFolder") { SyncRootFolderSettings(); }
            if (e.PropertyName == "PicturesDirectory")
            {
                SyncFlipImageGridSettings();
            }
            if (e.PropertyName == "PoolStatuses")
            {
                SyncPoolSettings();
            }
        }

        public ICommand SaveSettingsCommand
        {
            get
            {
                return new RelayCommand(o =>
                {
                    ApplicationManager.Current.SaveSettings();
                });
            }
        }

        #region Root Folder setting logic

        private string rootFolder;
        public string RootFolder
        {
            get { return rootFolder; }
            set
            {
                if (rootFolder != value)
                {
                    rootFolder = value;
                    OnPropertyChanged("RootFolder");

                    ApplicationManager.Current.RootFolder = rootFolder;
                }
            }
        }
        private UIElement ChooseRootFolderUIEl()
        {
            DockPanel dockPanel = new DockPanel();

            TextBox t = new TextBox()
            {
                Height = Double.NaN,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                IsReadOnly = true,
                Focusable = false,
                Text = rootFolder,
                Cursor = Cursors.Arrow,
                Margin = new Thickness(0, 0, 0, 4),
            };
            DockPanel.SetDock(t, Dock.Right);

            Button b = new Button { Margin = new Thickness(0, 0, 0, 4), Padding = new Thickness(0), Width = Double.NaN, Content = "..." };
            b.Click += new RoutedEventHandler((sender, args) =>
            {
                using (System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog())
                {
                    dlg.Description = "Select the folder containing the \"membership.mdb\" file and the \"Member Pictures\" directory.";
                    dlg.SelectedPath = rootFolder;
                    dlg.ShowNewFolderButton = false;
                    System.Windows.Forms.DialogResult result = dlg.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        t.Text = dlg.SelectedPath;
                        RootFolder = dlg.SelectedPath;
                    }
                }
            });
            DockPanel.SetDock(b, Dock.Right);

            dockPanel.Children.Add(b);
            dockPanel.Children.Add(t);

            return dockPanel;
        }

        private void SyncRootFolderSettings()
        {
            RootFolder = ApplicationManager.Current.RootFolder;
        }

        #endregion

        #region FlipImageGrid setting logic

        #region PicturesDirectory

        private string picturesDirectory;
        public string PicturesDirectory
        {
            get
            {
                return picturesDirectory;
            }
            set
            {
                if (picturesDirectory != value)
                {
                    picturesDirectory = value;
                    OnPropertyChanged("PicturesDirectory");

                    ApplicationManager.Current.PicturesDirectory = picturesDirectory;
                }
            }
        }

        private UIElement ChooseDirectoryUIEl()
        {
            DockPanel dockPanel = new DockPanel();

            TextBox t = new TextBox()
            {
                Height = Double.NaN,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                IsReadOnly = true,
                Focusable = false,
                Text = picturesDirectory,
                Cursor = Cursors.Arrow,
                Margin = new Thickness(0, 0, 0, 4),
            };
            DockPanel.SetDock(t, Dock.Right);

            Button b = new Button { Margin = new Thickness(0, 0, 0, 4), Padding = new Thickness(0), Width = Double.NaN, Content = "..." };
            b.Click += new RoutedEventHandler((sender, args) =>
            {
                using (System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog())
                {
                    dlg.Description = null;
                    dlg.SelectedPath = picturesDirectory;
                    dlg.ShowNewFolderButton = false;
                    System.Windows.Forms.DialogResult result = dlg.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        t.Text = dlg.SelectedPath;
                        PicturesDirectory = dlg.SelectedPath;
                    }
                }
            });
            DockPanel.SetDock(b, Dock.Right);

            dockPanel.Children.Add(b);
            dockPanel.Children.Add(t);

            return dockPanel;
        }

        #endregion

        #region TransitionType

        private string transitionType;
        public string TransitionType
        {
            get
            {
                return transitionType;
            }
            set
            {
                if (transitionType != value)
                {
                    transitionType = value;
                    OnPropertyChanged("TransitionType");

                    ApplicationManager.Current.TransitionType = transitionType;
                }
            }
        }

        private UIElement TransitionTypeUIEl()
        {
            StackPanel sp = new StackPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 4), };
            rotateButton = new RadioButton() { Content = "rotate", Margin = new Thickness(0, 0, 10, 0), };
            rotateButton.Checked += RotateChecked;
            fadeButton = new RadioButton() { Content = "fade", Margin = new Thickness(0, 0, 10, 0), };
            fadeButton.Checked += FadeChecked;
            fadeAsyncButton = new RadioButton() { Content = "fade-async", Margin = new Thickness(0, 0, 10, 0), };
            fadeAsyncButton.Checked += FadeAsyncChecked;
            noneButton = new RadioButton() { Content = "none", Margin = new Thickness(0, 0, 10, 0), };
            noneButton.Checked += NoneChecked;
            sp.Children.Add(rotateButton);
            sp.Children.Add(fadeButton);
            sp.Children.Add(fadeAsyncButton);
            sp.Children.Add(noneButton);

            TextBox tb = new TextBox() { DataContext = this, };
            tb.TextChanged += transitionTypeTb_TextChanged;
            Binding b = new Binding()
            {
                Path = new PropertyPath("TransitionType"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            };
            tb.SetBinding(TextBox.TextProperty, b);

            return sp;
        }
        RadioButton rotateButton, fadeButton, fadeAsyncButton, noneButton;
        void transitionTypeTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            string selection = (sender as TextBox).Text;
            if (selection == "rotate")
                rotateButton.IsChecked = true;
            if (selection == "fade")
                fadeButton.IsChecked = true;
            if (selection == "fade-async")
                fadeAsyncButton.IsChecked = true;
            if (selection == "none")
                noneButton.IsChecked = true;
        }
        void RotateChecked(object sender, RoutedEventArgs e)
        {
            TransitionType = "rotate";
        }
        void FadeChecked(object sender, RoutedEventArgs e)
        {
            TransitionType = "fade";
        }
        void FadeAsyncChecked(object sender, RoutedEventArgs e)
        {
            TransitionType = "fade-async";
        }
        void NoneChecked(object sender, RoutedEventArgs e)
        {
            TransitionType = "none";
        }

        #endregion

        #region OneImageBool

        private Boolean? oneImageBool;
        public Boolean? OneImageBool
        {
            get
            {
                return oneImageBool;
            }
            set
            {
                if (oneImageBool != value)
                {
                    oneImageBool = value;
                    OnPropertyChanged("OneImageBool");

                    ApplicationManager.Current.OneImageBool = oneImageBool;
                }
            }
        }

        private UIElement OneImageBoolUIEl()
        {
            StackPanel sp = new StackPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 4), };
            yesButton = new RadioButton() { Content = "yes", Margin = new Thickness(0, 0, 10, 0), };
            yesButton.Checked += YesChecked;
            noButton = new RadioButton() { Content = "no", Margin = new Thickness(0, 0, 10, 0), };
            noButton.Checked += NoChecked;
            sp.Children.Add(yesButton);
            sp.Children.Add(noButton);

            TextBox tb = new TextBox() { DataContext = this, };
            tb.TextChanged += oneImageBoolTb_TextChanged;
            Binding b = new Binding()
            {
                Path = new PropertyPath("OneImageBool"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            };
            tb.SetBinding(TextBox.TextProperty, b);

            return sp;
        }
        RadioButton yesButton, noButton;
        void oneImageBoolTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            string selection = (sender as TextBox).Text;
            if (selection == "True")
                yesButton.IsChecked = true;
            if (selection == "False")
                noButton.IsChecked = true;
        }
        void YesChecked(object sender, RoutedEventArgs e)
        {
            OneImageBool = true;
        }
        void NoChecked(object sender, RoutedEventArgs e)
        {
            OneImageBool = false;
        }

        private void SyncFlipImageGridSettings()
        {
            PicturesDirectory = ApplicationManager.Current.PicturesDirectory;
            TransitionType = ApplicationManager.Current.TransitionType;
            OneImageBool = ApplicationManager.Current.OneImageBool;
        }

        #endregion

        #endregion

        #region WeatherApplet settings logic

        #region Location

        private String location;
        public String Location
        {
            get { return location; }
            set
            {
                if (location != value)
                {
                    LocationsSaved = false;

                    if (DoDisplayLocationError)
                        DoDisplayLocationError = false;

                    location = value;
                }
            }
        }
        private async Task ValidateLocations(String locationsString)
        {
            if (LocationsSaved)
                return;

            IsValidatingLocations = true;

            char[] splitterChars = {';'};
            List<String> locs = locationsString.Replace(" ", "").Split(splitterChars).ToList();
            locs.Remove("");
            List<String> dummyLocs = new List<String>();
            foreach (String loc in locs)
            {
                if (await VerifyValidLocation(loc) == false)
                    dummyLocs.Add(loc);
            }

            if (locationsString != Location)
                return;

            if (locs.Count == dummyLocs.Count)
            {
                LocationErrorMessage = "No data available for these locations (separate different locations with ';')";
                DoDisplayLocationError = true;
                LocationErrorDisplayRule = "TextChanged";
            }
            else if (dummyLocs.Count != 0)
            {
                String msg = "No data available for ";
                for (int n = 0; n < dummyLocs.Count; n++)
                {
                    if (n == dummyLocs.Count - 1) // last dummyLoc
                    {
                        if (dummyLocs.Count > 1)
                            msg += "or \"" + dummyLocs[n] + "\"";
                        else
                            msg += "\"" + dummyLocs[n] + "\"";
                        msg += " (separate different locations with ';')";
                    }
                    else
                    {
                        msg += "\"" + dummyLocs[n] + "\"" + ", ";
                    }
                }
                LocationErrorMessage = msg;
                DoDisplayLocationError = true;
                LocationErrorDisplayRule = "TextChanged";
            }
            else
            {
                LocationsSaved = true;

                if (LocationErrorDisplayRule == "TextChanged")
                {
                    DoDisplayLocationError = false;
                }

                //foreach (String dummyLoc in dummyLocs)
                //    locs.Remove(dummyLoc);

                ApplicationManager.Current.Location = locs;
                //location = String.Join(";", locs);
            }
            OnPropertyChanged("Location");

            IsValidatingLocations = false;
        }
        private async Task<bool> VerifyValidLocation(String loc)
        {
            string url = string.Format
                    ("http://api.openweathermap.org/data/2.5/weather?q={0}&mode=xml",
                    loc);
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                try
                {
                    string response = await client.GetStringAsync(url);
                    if (response.Contains("message") && response.Contains("cod"))
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        private Boolean locationsSaved = true;
        public Boolean LocationsSaved
        {
            get { return locationsSaved; }
            set { locationsSaved = value; OnPropertyChanged("LocationsSaved"); }
        }
        private Boolean isValidatingLocations = false;
        public Boolean IsValidatingLocations
        {
            get { return isValidatingLocations; }
            set { isValidatingLocations = value; OnPropertyChanged("IsValidatingLocations"); }
        }

        static string locationErrorMessage;
        public static string LocationErrorMessage { get { return locationErrorMessage; } set { locationErrorMessage = value; } }
        static bool doDisplayLocationError;
        public static bool DoDisplayLocationError { get { return doDisplayLocationError; } set { doDisplayLocationError = value; } }
        static string locationErrorDisplayRule;
        public static string LocationErrorDisplayRule { get { return locationErrorDisplayRule; } set { locationErrorDisplayRule = value; } }
        
        public string Error
        {
            get { return null; }
        }
        
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Location")
                {
                    if (DoDisplayLocationError == true)
                        return LocationErrorMessage;
                }
                return null;
            }
        }

        private UIElement LocationUIEl()
        {
            StackPanel sp = new StackPanel() { Orientation = Orientation.Horizontal, };
            StackPanel innersp = new StackPanel() { Orientation = Orientation.Vertical, };
            TextBox t = new TextBox()
            {
                DataContext = this,
                Margin = new Thickness(0, 0, 0, 4),
                MinWidth = 200,
            };
            Binding bind = new Binding()
            {
                Path = new PropertyPath("Location"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                ValidatesOnDataErrors = true,
            };
            t.SetBinding(TextBox.TextProperty, bind);
            t.InputBindings.Add(new InputBinding(new RelayCommand(o => { ValidateLocations(location); }), new KeyGesture(Key.Enter)));

            Button directions = new Button()
            {
                Margin = new Thickness(0, 0, 0, 4),
                Content = "Press Enter key to validate locations",
                Style = Application.Current.Resources["SystemButtonLink"] as Style,
                IsEnabled = false,
            };

            innersp.Children.Add(t);
            innersp.Children.Add(directions);
            
            TextBlock checkMark = new TextBlock()
            {
                Text = "✔",
                DataContext = this,
                Foreground = System.Windows.Media.Brushes.Green,
                FontSize = 16,
                FontWeight = FontWeights.ExtraBold,
                Margin = new Thickness(10,0,0,4),
            };
            Binding bind2 = new Binding("LocationsSaved") { Converter = Application.Current.Resources["BooleanToVisibilityConverter"] as IValueConverter };
            checkMark.SetBinding(TextBlock.VisibilityProperty, bind2);

            ProgressRing loadingRing = new ProgressRing()
            {
                DataContext = this,
                Height = 18, Width = 18, Margin = new Thickness(10,3,0,4),
                State = ProgressState.Indeterminate,
                VerticalAlignment = VerticalAlignment.Top,
            };
            loadingRing.SetResourceReference(ProgressRing.ForegroundProperty, "Accent");
            Binding bind3 = new Binding("IsValidatingLocations") { Converter = Application.Current.Resources["BooleanToVisibilityConverter"] as IValueConverter };
            loadingRing.SetBinding(ProgressRing.VisibilityProperty, bind3);
            
            sp.Children.Add(innersp);
            sp.Children.Add(checkMark);
            sp.Children.Add(loadingRing);
            return sp;
        }
        
        #endregion
        
        #region Units

        private String units;
        public String Units
        {
            get { return units; }
            set
            {
                if (units != value)
                {
                    units = value;
                    OnPropertyChanged("Units");

                    ApplicationManager.Current.Units = units;
                }
            }
        }

        private UIElement UnitsUIEl()
        {
            StackPanel sp = new StackPanel() { Orientation = Orientation.Horizontal, };
            imperialButton = new RadioButton() { Content = "imperial", Margin = new Thickness(0, 0, 10, 0), };
            imperialButton.Checked += ImperialChecked;
            metricButton = new RadioButton() { Content = "metric", Margin = new Thickness(0, 0, 10, 0), };
            metricButton.Checked += MetricChecked;
            sp.Children.Add(imperialButton);
            sp.Children.Add(metricButton);
            
            TextBox tb = new TextBox() { DataContext = this, };
            tb.TextChanged += tb_TextChanged;
            Binding b = new Binding()
            {
                Path = new PropertyPath("Units"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            };
            tb.SetBinding(TextBox.TextProperty, b);
            
            return sp;
        }
        RadioButton imperialButton, metricButton;
        void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            string selection = (sender as TextBox).Text;
            if (selection == "imperial")
                imperialButton.IsChecked = true;
            if (selection == "metric")
                metricButton.IsChecked = true;
        }
        void ImperialChecked(object sender, RoutedEventArgs e)
        {
            Units = "imperial";
        }
        void MetricChecked(object sender, RoutedEventArgs e)
        {
            Units = "metric";
        }
        
        #endregion

        private void SyncWeatherAppletSettings()
        {
            String s = String.Empty;
            List<String> locs = ApplicationManager.Current.Location;
            foreach (String loc in locs)
                s += loc + ";";
            Location = s;

            Units = ApplicationManager.Current.Units;
        }

        #endregion

        #region PoolStatuses UserControl settings logic

        // poolStatuses should not contain more than 2 Pools.
        private void SyncPoolSettings()
        {
            List<Pool> poolStatuses = ApplicationManager.Current.PoolStatuses;
            Pool p;
            for (int x = 0; x < 2; x++)
            {
                p = poolStatuses[x];
                if (x == 0) // Pool 1
                {
                    // Does not use get set methods in case the init values have not been set yet.
                    pool1Name = p.PoolName;
                    pool1NumVars = p.PoolVars.Count;
                    pool1VarNames = Copy(p.PoolVars);
                    OnPropertyChanged("Pool1Name");
                    OnPropertyChanged("Pool1NumVars");
                    OnPropertyChanged("Pool1VarNames");
                }
                else if (x == 1) // Pool 2
                {
                    // Does not use get set methods in case the init values have not been set yet.
                    pool2Name = p.PoolName;
                    pool2NumVars = p.PoolVars.Count;
                    pool2VarNames = Copy(p.PoolVars);
                    OnPropertyChanged("Pool2Name");
                    OnPropertyChanged("Pool2NumVars");
                    OnPropertyChanged("Pool2VarNames");
                }
            }
        }
        public static List<PoolVariable> Copy(List<PoolVariable> l)
        {
            List<PoolVariable> retVal = new List<PoolVariable>();
            foreach (PoolVariable v in l)
            {
                retVal.Add(new PoolVariable(v.Name, v.Value));
            }
            return retVal;
        }
        public static List<Pool> Copy(List<Pool> l)
        {
            List<Pool> retVal = new List<Pool>();
            foreach (Pool p in l)
            {
                retVal.Add(new Pool(p.PoolName, Copy(p.PoolVars)));
            }
            return retVal;
        }
        private void UpdateApplicationManagerPoolVarList(int poolNumber)
        {
            List<Pool> poolStatuses = ApplicationManager.Current.PoolStatuses;
            int x = poolNumber - 1; // index adjustment
            List<PoolVariable> updatedList = new List<PoolVariable>();
            // if x == 0, do pool1; if x == 1, do pool2.
            foreach (PoolVariable pv in (x == 0 ? pool1VarNames : pool2VarNames))
            {
                PoolVariable match = GetMatch(poolStatuses[x].PoolVars, pv);
                if (match != null) // pv is in AppearanceManager PoolStatuses list
                {
                    updatedList.Add(new PoolVariable(match.Name, match.Value));
                }
                else // pv is not in AppearanceManager PoolStatuses list
                {
                    updatedList.Add(new PoolVariable(pv.Name));
                }
            }
            poolStatuses[x].PoolVars = updatedList;
            // poolStatuses[x] = new Pool(poolStatuses[x].PoolName, updatedList);
        }
        private PoolVariable GetMatch(List<PoolVariable> list, PoolVariable target)
        {
            foreach (PoolVariable pv in list)
            {
                if (pv.Name == target.Name)
                {
                    return pv;
                }
            }
            return null;
        }

        #region UIElement methods for setting content

        private UIElement PoolNameUIEl(int poolNumber)
        {
            Binding binding = new Binding()
            {
                Path = poolNumber == 1 ? new PropertyPath("Pool1Name") : new PropertyPath("Pool2Name"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            TextBox tb = new TextBox()
            {
                DataContext = this,
                Margin = new Thickness(0, 0, 0, 4),
            };
            tb.SetBinding(TextBox.TextProperty, binding);
            return tb;
        }
        private UIElement PoolNumVarsUIEl(int poolNumber)
        {
            Binding binding = new Binding()
            {
                Path = poolNumber == 1 ? new PropertyPath("Pool1NumVars") : new PropertyPath("Pool2NumVars"),
                Mode = BindingMode.TwoWay
            };
            ComboBox cb = new ComboBox()
            {
                DataContext = this,
                ItemsSource = new List<int> { 1, 2, 3, 4, 5 },
                Margin = new Thickness(0, 0, 0, 4),
            };
            cb.SetBinding(ComboBox.SelectedValueProperty, binding);
            return cb;
        }

        private UIElement PoolVarNamesUIEl(int poolNumber)
        {
            Binding binding = new Binding()
            {
                Path = poolNumber == 1 ? new PropertyPath("Pool1VarNames") : new PropertyPath("Pool2VarNames"),
                Mode = BindingMode.TwoWay
            };
            ItemsControl lb = new ItemsControl()
            {
                DataContext = this,
                ItemTemplate = GetVarNamesUIElDataTemplate(poolNumber),
                Margin = new Thickness(0,0,0,4),
            };
            lb.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            return lb;
        }
        private void ForceUpdatePool1VarList(object sender, TextChangedEventArgs e)
        {
            Pool1VarNames = pool1VarNames;
        }
        private void ForceUpdatePool2VarList(object sender, TextChangedEventArgs e)
        {
            Pool2VarNames = pool2VarNames;
        }
        private DataTemplate GetVarNamesUIElDataTemplate(int poolNumber)
        {
            DataTemplate template = new DataTemplate();
            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(TextBox));
            factory.SetBinding(TextBox.TextProperty, new Binding("Name"){ Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            
            // make lists update when var names change. without this, they won't because each textbox binds to the poolvariable and not the list as a whole
            TextChangedEventHandler pool1Handler = new TextChangedEventHandler(ForceUpdatePool1VarList);
            TextChangedEventHandler pool2Handler = new TextChangedEventHandler(ForceUpdatePool2VarList);
            factory.AddHandler(TextBox.TextChangedEvent, poolNumber == 1 ? pool1Handler : pool2Handler);

            factory.SetValue(TextBox.MarginProperty, new Thickness(0, 0, 0, 4));
            
            template.VisualTree = factory;

            return template;
        }

        #endregion

        #region get set methods and variable declartions
        private string pool1Name;
        private string pool2Name;
        private int pool1NumVars;
        private int pool2NumVars;
        private List<PoolVariable> pool1VarNames;
        private List<PoolVariable> pool2VarNames;

        public string Pool1Name
        {
            get { return pool1Name; }
            set
            {
                if (pool1Name != value)
                {
                    pool1Name = value;
                    OnPropertyChanged("Pool1Name");
                    pool1StatsSettingGroup.SettingGroupTitle = pool1Name + " Stats";

                    ApplicationManager.Current.PoolStatuses[0].PoolName = value;
                }
            }
        }
        public string Pool2Name
        {
            get { return pool2Name; }
            set
            {
                if (pool2Name != value)
                {
                    pool2Name = value;
                    OnPropertyChanged("Pool2Name");
                    pool2StatsSettingGroup.SettingGroupTitle = pool2Name + " Stats";

                    ApplicationManager.Current.PoolStatuses[1].PoolName = value;
                }
            }
        }
        public int Pool1NumVars
        {
            get { return pool1NumVars; }
            set
            {
                if (pool1NumVars != value)
                {
                    List<PoolVariable> newPool1VarNames = Copy(pool1VarNames); // Copy the var names list
                    bool needToAddVarSlots = value > pool1NumVars;
                    for (int x = 0; x < Math.Abs(value - pool1NumVars); x++)
                    {
                        if (needToAddVarSlots)
                        {
                            newPool1VarNames.Add(new PoolVariable("(Empty)"));
                        }
                        else // needToRemoveVarSlots
                        {
                            newPool1VarNames.RemoveAt(newPool1VarNames.Count - 1);
                        }
                    }
                    Pool1VarNames = newPool1VarNames;

                    pool1NumVars = value;
                    OnPropertyChanged("Pool1NumVars");
                }
            }
        }
        public int Pool2NumVars
        {
            get { return pool2NumVars; }
            set
            {
                if (pool2NumVars != value)
                {
                    List<PoolVariable> newPool2VarNames = Copy(pool2VarNames); // Copy the var names list
                    bool needToAddVarSlots = value > pool2NumVars;
                    for (int x = 0; x < Math.Abs(value - pool2NumVars); x++)
                    {
                        if (needToAddVarSlots)
                        {
                            newPool2VarNames.Add(new PoolVariable("(Empty)"));
                        }
                        else // needToRemoveVarSlots
                        {
                            newPool2VarNames.RemoveAt(newPool2VarNames.Count - 1);
                        }
                    }
                    Pool2VarNames = newPool2VarNames;

                    pool2NumVars = value;
                    OnPropertyChanged("Pool2NumVars");
                }
            }
        }
        public List<PoolVariable> Pool1VarNames
        {
            get { return pool1VarNames; }
            set
            {
                pool1VarNames = value;
                OnPropertyChanged("Pool1VarNames");

                UpdateApplicationManagerPoolVarList(1);
            }
        }
        public List<PoolVariable> Pool2VarNames
        {
            get { return pool2VarNames; }
            set
            {
                pool2VarNames = value;
                OnPropertyChanged("Pool2VarNames");

                UpdateApplicationManagerPoolVarList(2);
            }
        }
        #endregion

        #endregion
    }

    class SettingGroup : NotifyPropertyChanged
    {
        private string settingGroupTitle;
        public string SettingGroupTitle
        {
            get { return settingGroupTitle; }
            set
            {
                if (settingGroupTitle != value)
                {
                    settingGroupTitle = value;
                    OnPropertyChanged("SettingGroupTitle");
                }
            }
        }
        private List<Setting> settings;
        public List<Setting> Settings
        {
            get { return settings; }
            set
            {
                if (settings != value)
                {
                    settings = value;
                    OnPropertyChanged("Settings");
                }
            }
        }

        public void AddSetting(int n)
        {
            for (int x = 0; x < n; x++)
            {
                settings.Add(new Setting());
            }
        }
        public void AddSetting(Setting s)
        {
            settings.Add(s);
        }
        public void AddSettingRange(List<Setting> sl)
        {
            settings.AddRange(sl);
        }
        public void RemoveSetting(int n)
        {
            if (settings.Count < n)
            {
                settings.RemoveRange(0, settings.Count);
                return;
            }
            for (int x = 0; x < n; x++)
            {
                settings.RemoveAt(settings.Count - 1);
            }
        }
        public void RemoveSetting(Setting s)
        {
            settings.Remove(s);
        }

        public SettingGroup(string title, List<Setting> sl)
        {
            settingGroupTitle = title;
            settings = sl;
        }
        public SettingGroup(string title)
        {
            settingGroupTitle = title;
            settings = new List<Setting>();
        }
        public SettingGroup()
        {
            settingGroupTitle = "";
            settings = new List<Setting>();
        }
    }

    class Setting : NotifyPropertyChanged
    {
        private string label;
        public string Label
        {
            get { return label; }
        }
        private List<UIElement> content;
        public List<UIElement> Content
        {
            get { return content; }
            set
            {
                if (content != value)
                {
                    content = value;
                    OnPropertyChanged("Content");
                }
            }
        }

        public Setting(string l, UIElement c)
        {
            label = l;
            content = new List<UIElement>(){c};
        }
        public Setting(string l)
        {
            label = l;
            content = new List<UIElement>();
        }
        public Setting()
        {
            label = "";
            content = new List<UIElement>();
        }
    }
}
