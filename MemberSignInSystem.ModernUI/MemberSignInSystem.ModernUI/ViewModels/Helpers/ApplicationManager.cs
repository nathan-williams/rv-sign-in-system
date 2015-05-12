using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FirstFloor.ModernUI.Presentation;
using System.Windows.Input;
using System.Windows;

namespace MemberSignInSystem.ModernUI.ViewModels.Helpers
{
    class ApplicationManager :NotifyPropertyChanged
    {
        public void InitializeSettings()
        {
            InitializeRootFolderSettings();
            InitializeFlipImageGridSettings();
            InitializeWeatherAppletSettings();
            InitializePoolStatusesSettings();
        }
        private void InitializeRootFolderSettings()
        {
            String rootFolderImport = FileStorageHelper.RetrieveString(RootFolderSerialFileLocation);
            if (rootFolderImport != null)
                Application.Current.Resources[KeyRootFolder] = rootFolderImport;
            else
                Application.Current.Resources[KeyRootFolder] = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "Membership Records");;
        }
        private void InitializeFlipImageGridSettings()
        {
            String picturesDirectoryImport = FileStorageHelper.RetrieveString(PicturesDirectorySerialFileLocation);
            if (picturesDirectoryImport != null)
                Application.Current.Resources[KeyPicturesDirectory] = picturesDirectoryImport;
            else
                Application.Current.Resources[KeyPicturesDirectory] = "C:\\Users\\Public\\Pictures\\Sample Pictures";

            String transitionTypeImport = FileStorageHelper.RetrieveString(TransitionTypeSerialFileLocation);
            if (transitionTypeImport != null)
                Application.Current.Resources[KeyTransitionType] = transitionTypeImport;
            else
                Application.Current.Resources[KeyTransitionType] = "rotate";

            Boolean? oneImageBoolImport = FileStorageHelper.RetrieveNullableBoolean(OneImageBoolSerialFileLocation);
            if (oneImageBoolImport != null)
                Application.Current.Resources[KeyOneImageBool] = oneImageBoolImport;
            else
                Application.Current.Resources[KeyOneImageBool] = false;
        }
        private void InitializePoolStatusesSettings()
        {
            List<Pool> poolStatusesImport = FileStorageHelper.RetrievePoolList(PoolStatusesSerialFileLocation);
            if (poolStatusesImport != null)
                Application.Current.Resources[KeyPoolStatuses] = poolStatusesImport;
            else
                Application.Current.Resources[KeyPoolStatuses] = new List<Pool>
                {
                    new Pool("Main Pool", new List<PoolVariable>() { new PoolVariable("pH"), new PoolVariable("Cl"), new PoolVariable("Water temp") }),
                    new Pool("Baby Pool", new List<PoolVariable>() { new PoolVariable("pH"), new PoolVariable("Cl") }),
                };
        }
        private void InitializeWeatherAppletSettings()
        {
            List<String> locationImport = FileStorageHelper.RetrieveStringList(LocationSerialFileLocation);
            if (locationImport != null)
                Application.Current.Resources[KeyLocation] = locationImport;
            else
                Application.Current.Resources[KeyLocation] = new List<String> { "22153", "Springfield,VA" };

            String unitsImport = FileStorageHelper.RetrieveString(UnitsSerialFileLocation);
            if (unitsImport != null)
                Application.Current.Resources[KeyUnits] = unitsImport;
            else
                Application.Current.Resources[KeyUnits] = "imperial";
        }

         /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationManager"/> class.
        /// </summary>
        private ApplicationManager()
        {
            SetRootFolderCommand = new RelayCommand(o =>
            {
                if (o is String)
                {
                    RootFolder = o as String;
                }
            });
            SetPicturesDirectoryCommand = new RelayCommand(o =>
            {
                if (o is String)
                {
                    PicturesDirectory = o as String;
                }
            });
            SetTransitionTypeCommand = new RelayCommand(o =>
            {
                if (o is String)
                {
                    TransitionType = o as String;
                }
            });
            SetOneImageBoolCommand = new RelayCommand(o =>
            {
                if (o is Boolean?)
                {
                    OneImageBool = o as Boolean?;
                }
            });
            SetPoolStatusesCommand = new RelayCommand(o =>
            {
                if (o is List<Pool>)
                {
                    PoolStatuses = o as List<Pool>;
                }
            });
            SetLocationCommand = new RelayCommand(o =>
            {
                if (o is List<String>)
                {
                    Location = o as List<String>;
                }
            });
            SetUnitsCommand = new RelayCommand(o =>
            {
                if (o is String)
                {
                    Units = o as String;
                }
            });
        }

        private static ApplicationManager current = new ApplicationManager();

        /// <summary>
        /// Gets the current <see cref="ApplicationManager"/> instance.
        /// </summary>
        public static ApplicationManager Current
        {
            get { return current; }
        }

        public void SaveSettings()
        {
            SaveRootFolderSetting();
            SavePicturesDirectorySetting();
            SaveTransitionTypeSetting();
            SaveOneImageBoolSetting();
            SavePoolStatusesSettings();
            SaveLocationsSetting();
            SaveUnitsSetting();
        }

        // Possible known issue: Updates Application Setting for lists even if its the same list
        //                       Find out how to check lists against each other for content in
        //                       reasonable fashion [Edit: See SetLocation for possible soln???]

        #region RootFolder methods

        /// <summary>
        /// The file path for the serialized root folder uri string.
        /// </summary>
        public const string RootFolderSerialFileLocation = "Settings/RootFolder.xml";

        /// <summary>
        /// The resource key for the root folder uri.
        /// </summary>
        public const string KeyRootFolder = "RootFolder";

        private String GetRootFolder()
        {
            var defaultRootFolder = Application.Current.Resources[KeyRootFolder] as String;
            return defaultRootFolder;
        }

        private void SetRootFolder(String rootFolder)
        {
            if (rootFolder!= GetRootFolder())
            {
                Application.Current.Resources[KeyRootFolder] = rootFolder;
                OnPropertyChanged("RootFolder");
            }
        }

        public String RootFolder
        {
            get { return GetRootFolder(); }
            set { SetRootFolder(value); }
        }

        private void SaveRootFolderSetting()
        {
            FileStorageHelper.Save(
                RootFolder,
                typeof(String),
                RootFolderSerialFileLocation
            );
        }

        /// <summary>
        /// The command that sets the root folder uri string.
        /// </summary>
        public ICommand SetRootFolderCommand { get; private set; }


        #endregion

        #region FlipImageGrid methods

        #region PicturesDirectory methods

        /// <summary>
        /// The file path for the serialized pictures directory uri string.
        /// </summary>
        public const string PicturesDirectorySerialFileLocation = "Settings/PicturesDirectory.xml";

        /// <summary>
        /// The resource key for the pictures directory uri for display in the FlipImageGrid.
        /// </summary>
        public const string KeyPicturesDirectory = "PicturesDirectory";

        private String GetPicturesDirectory()
        {
            var defaultPicturesDirectory = Application.Current.Resources[KeyPicturesDirectory] as String;
            return defaultPicturesDirectory;
        }

        private void SetPicturesDirectory(String picturesDirectory)
        {
            if (picturesDirectory != GetPicturesDirectory())
            {
                Application.Current.Resources[KeyPicturesDirectory] = picturesDirectory;
                OnPropertyChanged("PicturesDirectory");
            }
        }

        public String PicturesDirectory
        {
            get { return GetPicturesDirectory(); }
            set { SetPicturesDirectory(value); }
        }

        private void SavePicturesDirectorySetting()
        {
            FileStorageHelper.Save(
                PicturesDirectory,
                typeof(String),
                PicturesDirectorySerialFileLocation
            );
        }

        /// <summary>
        /// The command that sets the pictures directory uri string.
        /// </summary>
        public ICommand SetPicturesDirectoryCommand { get; private set; }

        #endregion

        #region TransitionType methods

        /// <summary>
        /// The file path for the serialized pictures directory uri string.
        /// </summary>
        public const string TransitionTypeSerialFileLocation = "Settings/TransitionType.xml";

        /// <summary>
        /// The resource key for the pictures directory uri for display in the FlipImageGrid.
        /// </summary>
        public const string KeyTransitionType = "TransitionType";

        private String GetTransitionType()
        {
            var defaultPicturesDirectory = Application.Current.Resources[KeyTransitionType] as String;
            return defaultPicturesDirectory;
        }

        private void SetTransitionType(String transitionType)
        {
            if (transitionType != GetTransitionType())
            {
                Application.Current.Resources[KeyTransitionType] = transitionType;
                OnPropertyChanged("TransitionType");
            }
        }

        public String TransitionType
        {
            get { return GetTransitionType(); }
            set { SetTransitionType(value); }
        }

        private void SaveTransitionTypeSetting()
        {
            FileStorageHelper.Save(
                TransitionType,
                typeof(String),
                TransitionTypeSerialFileLocation
            );
        }

        /// <summary>
        /// The command that sets the pictures directory uri string.
        /// </summary>
        public ICommand SetTransitionTypeCommand { get; private set; }

        #endregion

        #region OneImageBool methods

        /// <summary>
        /// The file path for the serialized pictures directory uri string.
        /// </summary>
        public const string OneImageBoolSerialFileLocation = "Settings/OneImageBool.xml";

        /// <summary>
        /// The resource key for the pictures directory uri for display in the FlipImageGrid.
        /// </summary>
        public const string KeyOneImageBool = "OneImageBool";

        private Boolean? GetOneImageBool()
        {
            var defaultOneImageBool = Application.Current.Resources[KeyOneImageBool] as Boolean?;
            return defaultOneImageBool;
        }

        private void SetOneImageBool(Boolean? oneImageBool)
        {
            if (oneImageBool != GetOneImageBool())
            {
                Application.Current.Resources[KeyOneImageBool] = oneImageBool;
                OnPropertyChanged("OneImageBool");
            }
        }

        public Boolean? OneImageBool
        {
            get { return GetOneImageBool(); }
            set { SetOneImageBool(value); }
        }

        private void SaveOneImageBoolSetting()
        {
            FileStorageHelper.Save(
                OneImageBool,
                typeof(Boolean?),
                OneImageBoolSerialFileLocation
            );
        }

        /// <summary>
        /// The command that sets the pictures directory uri string.
        /// </summary>
        public ICommand SetOneImageBoolCommand { get; private set; }

        #endregion

        #endregion

        #region PoolStatuses methods

        /// <summary>
        /// The file path for the serialized pool statuses list.
        /// </summary>
        public const string PoolStatusesSerialFileLocation = "Settings/PoolStatuses.xml";

        /// <summary>
        /// The resource key for the pool statuses list.
        /// </summary>
        public const string KeyPoolStatuses = "PoolStatuses";

        private List<Pool> GetPoolStatuses()
        {
            var defaultPoolStatuses = Application.Current.Resources[KeyPoolStatuses] as List<Pool>;
            return defaultPoolStatuses;
        }

        private void SetPoolStatuses(List<Pool> poolStatuses)
        {
            Application.Current.Resources[KeyPoolStatuses] = poolStatuses;
            OnPropertyChanged("PoolStatuses");
        }

        /// <summary>
        /// Gets or sets the pool statuses list.
        /// </summary>
        public List<Pool> PoolStatuses
        {
            get { return GetPoolStatuses(); }
            set { SetPoolStatuses(value); }
        }

        private void SavePoolStatusesSettings()
        {
            FileStorageHelper.Save(
                PoolStatuses,
                typeof(List<Pool>),
                PoolStatusesSerialFileLocation
            );
        }

        /// <summary>
        /// The command that sets the pool statuses list.
        /// </summary>
        public ICommand SetPoolStatusesCommand { get; private set; }

        #endregion

        #region WeatherApplet methods

        #region Location methods

        /// <summary>
        /// The file path for the serialized location uri string.
        /// </summary>
        public const string LocationSerialFileLocation = "Settings/Location.xml";

        /// <summary>
        /// The resource key for the location uri for the WeatherApplet.
        /// </summary>
        public const string KeyLocation = "Location";

        private List<String> GetLocation()
        {
            var defaultLocation = Application.Current.Resources[KeyLocation] as List<String>;
            return defaultLocation;
        }

        private void SetLocation(List<String> location)
        {
            //if (!Enumerable.SequenceEqual<String>(GetLocation().OrderBy(q => q), location.OrderBy(q => q)))
            //{
                Application.Current.Resources[KeyLocation] = location;
                OnPropertyChanged("Location");
            //}
        }

        public List<String> Location
        {
            get { return GetLocation(); }
            set { SetLocation(value); }
        }

        private void SaveLocationsSetting()
        {
            FileStorageHelper.Save(
                Location,
                typeof(List<String>),
                LocationSerialFileLocation
            );
        }

        /// <summary>
        /// The command that sets the location uri string.
        /// </summary>
        public ICommand SetLocationCommand { get; private set; }

        #endregion

        #region Weather Units methods

        /// <summary>
        /// The file path for the serialized units uri string.
        /// </summary>
        public const string UnitsSerialFileLocation = "Settings/Units.xml";

        /// <summary>
        /// The resource key for the units uri for display in the FlipImageGrid.
        /// </summary>
        public const string KeyUnits = "Units";

        private String GetUnits()
        {
            var defaultUnits = Application.Current.Resources[KeyUnits] as String;
            return defaultUnits;
        }

        private void SetUnits(String units)
        {
            if (units != GetUnits())
            {
                Application.Current.Resources[KeyUnits] = units;
                OnPropertyChanged("Units");
            }
        }

        public String Units
        {
            get { return GetUnits(); }
            set { SetUnits(value); }
        }

        private void SaveUnitsSetting()
        {
            FileStorageHelper.Save(
                Units,
                typeof(String),
                UnitsSerialFileLocation
            );
        }

        /// <summary>
        /// The command that sets the units uri string.
        /// </summary>
        public ICommand SetUnitsCommand { get; private set; }

        #endregion

        #endregion
    }
}
