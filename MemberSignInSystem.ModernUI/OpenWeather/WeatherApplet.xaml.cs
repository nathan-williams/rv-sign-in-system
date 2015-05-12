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

using System.Timers;
using OpenWeather.ViewModel;
using FirstFloor.ModernUI.Presentation;

namespace OpenWeather
{
    /// <summary>
    /// Interaction logic for WeatherApplet.xaml
    /// </summary>
    
    public partial class WeatherApplet : UserControl
    {
        private static int RefreshInterval = 60000; // 1 minute
        private static Timer timer;
        private WeatherViewModel viewModel;

        public WeatherApplet()
        {
            InitializeComponent();

            viewModel = this.DataContext as WeatherViewModel;
            
            // Workaround to attach location and units properties in viewModel to dynamic resources of application.
            //this.SetResourceReference(WeatherApplet.LocationProperty, "Location");
            //this.SetResourceReference(WeatherApplet.UnitsProperty, "Units");
            
            this.Loaded += async delegate { await viewModel.GetWeather(); };

            Application.Current.Resources["FirstLoadCommand"] = new RelayCommand(o => {
                WeatherDisplay.Visibility = System.Windows.Visibility.Visible;
                WeatherAppletContent.Children.Remove(InitialProgressRing);
                RefreshWeatherButton.IsEnabled = true;
                Start();
            });
        }

        private void Start()
        {
            timer = new Timer(RefreshInterval);
            timer.Elapsed += async delegate
            {
                try
                {
                    await Dispatcher.InvokeAsync(new Action(() =>
                    {
                        viewModel.GetWeather();
                    }));
                }
                catch (TaskCanceledException e)
                {
                    Console.Write(e.Message);
                }
            };
            timer.Enabled = true;
        }
        
        /*
        public static readonly DependencyProperty LocationProperty = DependencyProperty.Register("Location", typeof(List<String>), typeof(WeatherApplet), new PropertyMetadata(new List<String> { "Springfield,VA" }));
        public static readonly DependencyProperty UnitsProperty = DependencyProperty.Register("Units", typeof(String), typeof(WeatherApplet), new PropertyMetadata("imperial"));
        public List<String> Location
        {
            get { return (List<String>)GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); viewModel.Location = this.Location; }
        }
        public String Units
        {
            get { return (String)GetValue(UnitsProperty); }
            set { SetValue(UnitsProperty, value); viewModel.Units = this.Units; }
        }
        */
    }
}
