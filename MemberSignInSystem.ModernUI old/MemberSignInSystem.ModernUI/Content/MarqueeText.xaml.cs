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
using System.Windows.Media.Animation;
using System.Reflection;
using System.Windows.Media.Effects;

using System.IO;

namespace MemberSignInSystem.ModernUI.Content
{
    /// <summary>
    /// Interaction logic for MarqueeText.xaml
    /// </summary>
    public partial class MarqueeText : UserControl
    {
        MarqueeType _marqueeType;
        DoubleAnimation _doubleAnimation;
        Random rand = new Random();
        static List<String> announcements = new List<String>() { "ANNOUNCEMENT 1", "ANNOUNCEMENT 2", "ANNOUNCEMENT 3", "ANNOUNCEMENT 4", "ANNOUNCEMENT 5" };
        int annCount = 0;
        int desiredFrameRate = 30;

        public MarqueeType MarqueeType
        {
            get { return _marqueeType; }
            set { _marqueeType = value; }
        }

        public String MarqueeContent
        {
            get { return tbmarquee.Text; }
            set { tbmarquee.Text = value; }
        }

        private double _marqueeTimeInSeconds;

        public double MarqueeTimeInSeconds
        {
            get { return _marqueeTimeInSeconds; }
            set { _marqueeTimeInSeconds = value; }
        }


        public MarqueeText()
        {
            InitializeComponent();
            LoadAnnouncements();
            ResetForeground();
            this.Loaded += delegate { MarqueeText_Loaded(); };
            this.SizeChanged += delegate { MarqueeText_SizeChanged(); };
        }

        public static void LoadAnnouncements()
        {
            string directoryUri = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "Membership Records\\");
            string announcementsPath = directoryUri + "Announcements.txt";
            if (File.Exists(announcementsPath))
            {
                String[] lines = File.ReadAllLines(announcementsPath);
                announcements = new List<String>();
                foreach (String ann in lines)
                {
                    if (ann != "" && ann != "\n")
                    {
                        announcements.Add(ann);
                    }
                }
            }
        }

        async void _doubleAnimation_Completed(object sender, EventArgs e)
        {
            FormattedText oldText = new FormattedText(this.MarqueeContent, System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface(tbmarquee.FontFamily, tbmarquee.FontStyle, tbmarquee.FontWeight, tbmarquee.FontStretch), tbmarquee.FontSize, tbmarquee.Foreground);
            FormattedText newText = new FormattedText(GetAnnouncement(), System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface(tbmarquee.FontFamily, tbmarquee.FontStyle, tbmarquee.FontWeight, tbmarquee.FontStretch), tbmarquee.FontSize, tbmarquee.Foreground);
            tbmarquee.Text = newText.Text;
            if (oldText.Width != newText.Width)
                await EventAsync.FromEvent<SizeChangedEventArgs>(tbmarquee, "SizeChanged");
            MarqueeText_SizeChanged();
            ResetForeground();
            BeginAnimation(_marqueeType);
        }
        private void ResetForeground()
        {
            Color c = RandomNeonColor();
            this.Foreground = new SolidColorBrush(c);
            (tbmarquee.Effect as DropShadowEffect).Color = c;
        }

        private Color RandomNeonColor()
        {
            Color[] neonColors = { GetColor("#97ff00"), GetColor("#ea00ff"), GetColor("#00c5ff"), GetColor("#ff009c"), GetColor("#ff0000"), GetColor("#ff9933") };
            return neonColors[rand.Next(neonColors.Length)];
        }
        private Color GetColor(String hex)
        {
            return (Color)ColorConverter.ConvertFromString(hex);
        }
        private String GetAnnouncement()
        {
            String ret = announcements[annCount % announcements.Count];
            annCount++;
            return ret;
        }

        private void MarqueeText_SizeChanged()
        {
            if (_doubleAnimation != null)
            {
                BeginAnimation(_marqueeType, null);
            }

            // Not updating canMain dimensions on restore (demaximize/shrink)
            canMain.Height = this.ActualHeight;
            canMain.Width = this.ActualWidth;

            double newSize = 0;
            if (this._marqueeType == MarqueeType.LeftToRight || this._marqueeType == MarqueeType.RightToLeft)
            {
                newSize = this.ActualWidth + tbmarquee.ActualWidth;
            }
            else if (this._marqueeType == MarqueeType.TopToBottom || this._marqueeType == MarqueeType.BottomToTop)
            {
                newSize = this.ActualHeight + tbmarquee.ActualHeight;
            }
            UpdateAnimationDuration(newSize);
            StartMarqueeing(_marqueeType);
        }

        void UpdateAnimationDuration(Double newMarqueeSize)
        {
            // 1.4 inches per second
            // Widths given in device-independent units (1/96th inch per unit)
            // Use 600 as average marqueesize
            double newTime = (newMarqueeSize) * (1.0 / 96.0) / 1.4;
            this._marqueeTimeInSeconds = newTime;// +  5.0 * ((newMarqueeSize - 600.0) / 600.0);
        }

        private void MarqueeText_Loaded()
        {
            this.Foreground = new SolidColorBrush(RandomNeonColor());

            StartMarqueeing(_marqueeType);
        }

        public void StartMarqueeing(MarqueeType marqueeType)
        {
            if (marqueeType == MarqueeType.LeftToRight)
            {
                LeftToRightMarquee();
            }
            else if (marqueeType == MarqueeType.RightToLeft)
            {
                RightToLeftMarquee();
            }
            else if (marqueeType == MarqueeType.TopToBottom)
            {
                TopToBottomMarquee();
            }
            else if (marqueeType == MarqueeType.BottomToTop)
            {
                BottomToTopMarquee();
            }
        }

        private void LeftToRightMarquee()
        {
            double height = canMain.ActualHeight - tbmarquee.ActualHeight;
            tbmarquee.Margin = new Thickness(0, height / 2, 0, 0);
            _doubleAnimation = new DoubleAnimation();
            _doubleAnimation.Completed += _doubleAnimation_Completed;
            _doubleAnimation.From = -tbmarquee.ActualWidth;
            _doubleAnimation.To = this.ActualWidth; // using this instead of canMain to workaround bug where canMain dimensions not updated
            _doubleAnimation.RepeatBehavior = new RepeatBehavior(1);
            _doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(_marqueeTimeInSeconds));
            Timeline.SetDesiredFrameRate(_doubleAnimation, desiredFrameRate);
            BeginAnimation(_marqueeType);
        }
        private void RightToLeftMarquee()
        {
            double height = canMain.ActualHeight - tbmarquee.ActualHeight;
            tbmarquee.Margin = new Thickness(0, height / 2, 0, 0);
            _doubleAnimation = new DoubleAnimation();
            _doubleAnimation.Completed += _doubleAnimation_Completed;
            _doubleAnimation.From = -tbmarquee.ActualWidth;
            _doubleAnimation.To = this.ActualWidth; // using this instead of canMain to workaround bug where canMain dimensions not updated
            _doubleAnimation.RepeatBehavior = new RepeatBehavior(1);
            _doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(_marqueeTimeInSeconds));
            Timeline.SetDesiredFrameRate(_doubleAnimation, desiredFrameRate);
            BeginAnimation(_marqueeType);
        }
        private void TopToBottomMarquee()
        {
            double width = canMain.ActualWidth - tbmarquee.ActualWidth;
            tbmarquee.Margin = new Thickness(width / 2, 0, 0, 0);
            _doubleAnimation = new DoubleAnimation();
            _doubleAnimation.Completed += _doubleAnimation_Completed;
            _doubleAnimation.From = -tbmarquee.ActualHeight;
            _doubleAnimation.To = canMain.ActualHeight;
            _doubleAnimation.RepeatBehavior = new RepeatBehavior(1);
            _doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(_marqueeTimeInSeconds));
            Timeline.SetDesiredFrameRate(_doubleAnimation, desiredFrameRate);
            BeginAnimation(_marqueeType);
        }
        private void BottomToTopMarquee()
        {
            double width = canMain.ActualWidth - tbmarquee.ActualWidth;
            tbmarquee.Margin = new Thickness(width / 2, 0, 0, 0);
            _doubleAnimation = new DoubleAnimation();
            _doubleAnimation.Completed += _doubleAnimation_Completed;
            _doubleAnimation.From = -tbmarquee.ActualHeight;
            _doubleAnimation.To = canMain.ActualHeight;
            _doubleAnimation.RepeatBehavior = new RepeatBehavior(1);
            _doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(_marqueeTimeInSeconds));
            Timeline.SetDesiredFrameRate(_doubleAnimation, desiredFrameRate);
            BeginAnimation(_marqueeType);
        }
        private void BeginAnimation(MarqueeType marqueeType)
        {
            if (marqueeType == MarqueeType.LeftToRight)
            {
                tbmarquee.BeginAnimation(Canvas.LeftProperty, _doubleAnimation);
            }
            else if (marqueeType == MarqueeType.RightToLeft)
            {
                tbmarquee.BeginAnimation(Canvas.RightProperty, _doubleAnimation);
            }
            else if (marqueeType == MarqueeType.TopToBottom)
            {
                tbmarquee.BeginAnimation(Canvas.TopProperty, _doubleAnimation);
            }
            else if (marqueeType == MarqueeType.BottomToTop)
            {
                tbmarquee.BeginAnimation(Canvas.BottomProperty, _doubleAnimation);
            }
        }
        private void BeginAnimation(MarqueeType marqueeType, DoubleAnimation dAnim)
        {
            if (marqueeType == MarqueeType.LeftToRight)
            {
                tbmarquee.BeginAnimation(Canvas.LeftProperty, dAnim);
            }
            else if (marqueeType == MarqueeType.RightToLeft)
            {
                tbmarquee.BeginAnimation(Canvas.RightProperty, dAnim);
            }
            else if (marqueeType == MarqueeType.TopToBottom)
            {
                tbmarquee.BeginAnimation(Canvas.TopProperty, dAnim);
            }
            else if (marqueeType == MarqueeType.BottomToTop)
            {
                tbmarquee.BeginAnimation(Canvas.BottomProperty, dAnim);
            }
        }
    }
    public enum MarqueeType
    {
        LeftToRight,
        RightToLeft,
        TopToBottom,
        BottomToTop
    }


    //await EventAsync.FromEvent<EventArgs>(EVENT OWNER, "EVENT NAME");
    public static class EventAsync
    {
        // TODO: We're skipping a *lot* of error checking here.
        private sealed class EventHandlerTask<TEventArgs>
        {
            private readonly TaskCompletionSource<TEventArgs> tcs;
            private readonly Delegate subscription;
            private readonly object target;
            private readonly EventInfo eventInfo;

            public EventHandlerTask(object target, string eventName)
            {
                this.tcs = new TaskCompletionSource<TEventArgs>();
                this.target = target;
                this.eventInfo = target.GetType().GetEvent(eventName);
                this.subscription = Delegate.CreateDelegate(this.eventInfo.EventHandlerType, this, "EventCompleted");
                this.eventInfo.AddEventHandler(target, this.subscription);
            }

            public Task<TEventArgs> Task
            {
                get { return tcs.Task; }
            }

            private void EventCompleted(object sender, TEventArgs args)
            {
                this.eventInfo.RemoveEventHandler(this.target, this.subscription);
                this.tcs.SetResult(args);
            }
        }

        public static Task<TEventArgs> FromEvent<TEventArgs>(object target, string eventName) where TEventArgs : EventArgs
        {
            return new EventHandlerTask<TEventArgs>(target, eventName).Task;
        }
    }
}


/*


    

*/