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
using System.Windows.Threading;
using System.Windows.Media.Animation;

using Thriple.Controls;

namespace MemberSignInSystem.ModernUI.Content
{
    /// <summary>
    /// Interaction logic for FlipImage.xaml
    /// </summary>
    public partial class FlipImage : UserControl
    {
        private static Random rand = null;
        private DispatcherTimer flipTimer;
        private const int ticksPerSecond = 10000000;
        int desiredFrameRate = 15;

        public FlipImage()
        {
            // If not already done, instantiate the Random number gen rand,
            // which is shared by all FlipImage instances.
            if (rand == null) rand = new Random();

            InitializeComponent();

            flipTimer = new DispatcherTimer(randomFlipInterval(), DispatcherPriority.Normal, TimeElapsed, this.Dispatcher);
            flipTimer.Start();
        }

        private void TimeElapsed(object sender, EventArgs e)
        {
            flipTimer.Stop();
            ChangeImage();
            flipTimer.Interval = randomFlipInterval();
            flipTimer.Start();
        }

        public void ChangeImage()
        {
            List<String> imageList = ImageList;
            ImageSource = imageList.Last();
            // Move consumed image to back of list
            String imageToMove = imageList.First();
            imageList.Remove(imageToMove);
            imageList.Add(imageToMove);
        }

        private TimeSpan randomFlipInterval()
        {
            return TimeSpan.FromTicks(rand.Next(10 * ticksPerSecond, 30 * ticksPerSecond)); // random interval between 10 seconds and 30 seconds.
            // Note: Update this to change possible range based on number of displayed children in flipimagegrd
        }
        public void StopTimer()
        {
            flipTimer.Stop();
        }
        public void StartTimer()
        {
            flipTimer.Start();
        }

        public List<String> ImageList
        {
            get
            {
                return (List<String>)GetValue(ImageListProperty);
            }
            set
            {
                if (value != null)
                {
                    SetValue(ImageListProperty, value);
                    ChangeImage();
                }
            }
        }
        public static readonly DependencyProperty ImageListProperty = DependencyProperty.Register(
            "ImageList", typeof(List<String>), typeof(FlipImage), new PropertyMetadata(new List<String>()));

        public String TransitionType
        {
            get
            {
                return (String)GetValue(TransitionTypeProperty);
            }
            set
            {
                if (value != null)
                {
                    SetValue(TransitionTypeProperty, value);
                }
            }
        }
        public static readonly DependencyProperty TransitionTypeProperty = DependencyProperty.Register(
            "TransitionType", typeof(String), typeof(FlipImage), new PropertyMetadata("rotate"));

        private bool isOnFrontSide = true;
        public string ImageSource
        {
            get
            {
                return (string)GetValue(ImageSourceProperty);
            }
            set
            {
                String transitionType = TransitionType;
                if (transitionType == "rotate")
                {
                    if (isOnFrontSide == true) // Flipping from front side to back side
                        ImageContainer.BackContent = value;
                    else // Flipping from back side to front side
                        ImageContainer.Content = value;

                    isOnFrontSide = !isOnFrontSide;

                    // In case view port is still null, add a callback to rotate afterwards.
                    try
                    {
                        ImageContainer.Rotate();
                    }
                    catch (Exception e)
                    {
                        ImageContainer.ViewPortOpened += delegate { ImageContainer.Rotate(); };
                    }
                }
                else if (transitionType == "fade-async")
                {
                    DoubleAnimation fadeOut = new DoubleAnimation(1.0, 0.0, new Duration(TimeSpan.FromSeconds(0.8)), FillBehavior.Stop);
                    Timeline.SetDesiredFrameRate(fadeOut, desiredFrameRate);
                    DoubleAnimation fadeIn = new DoubleAnimation(0.0, 1.0, new Duration(TimeSpan.FromSeconds(0.8)), FillBehavior.Stop);
                    Timeline.SetDesiredFrameRate(fadeIn, desiredFrameRate);
                    fadeOut.Completed += delegate
                    {
                        if (isOnFrontSide == true) ImageContainer.Content = value;
                        else ImageContainer.BackContent = value;
                        
                        ImageContainer.BeginAnimation(ContentControl3D.OpacityProperty, fadeIn);
                        //this.BeginAnimation(FlipImage.OpacityProperty, fadeIn);
                    };
                    ImageContainer.BeginAnimation(ContentControl3D.OpacityProperty, fadeOut);
                    //this.BeginAnimation(FlipImage.OpacityProperty, fadeOut);
                }
                else if (transitionType == "fade")
                {
                    String oldImgSrc = isOnFrontSide ? ImageContainer.Content as String : ImageContainer.BackContent as String;
                    if (oldImgSrc == null || oldImgSrc == "")
                    {
                        if (isOnFrontSide == true) ImageContainer.Content = value;
                        else ImageContainer.BackContent = value;
                        return;
                    }

                    DoubleAnimation fadeOut = new DoubleAnimation(1.0, 0.0, new Duration(TimeSpan.FromSeconds(0.8)), FillBehavior.Stop);
                    Timeline.SetDesiredFrameRate(fadeOut, desiredFrameRate);

                    Grid oldImgGrid = new Grid()
                    {
                        Background = new ImageBrush(new BitmapImage(new Uri(oldImgSrc)))
                        {
                            AlignmentX = AlignmentX.Center,
                            AlignmentY = AlignmentY.Center,
                            Stretch = Stretch.UniformToFill,
                        },
                    };
                    
                    ContentGrid.Children.Add(oldImgGrid);

                    if (isOnFrontSide == true) ImageContainer.Content = value;
                    else ImageContainer.BackContent = value;

                    fadeOut.Completed += delegate { ContentGrid.Children.Remove(oldImgGrid); };
                    oldImgGrid.BeginAnimation(Grid.OpacityProperty, fadeOut);
                }
                else if (transitionType == "none")
                {
                    if (isOnFrontSide == true) ImageContainer.Content = value;
                    else ImageContainer.BackContent = value;
                }
            }
        }

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
            "ImageSource", typeof(string), typeof(FlipImage), new PropertyMetadata(string.Empty));
    }
}
