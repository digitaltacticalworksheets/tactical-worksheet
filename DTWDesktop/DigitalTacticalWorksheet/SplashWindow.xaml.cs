using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Animation;

namespace DigitalTacticalWorksheet
{
    public partial class SplashWindow : Window
    {
        public SplashWindow()
        {
            InitializeComponent();
            SetVersionLabel();
        }

        private void SetVersionLabel()
        {
            try
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                if (version != null)
                {
                    VersionLabel.Text = $"v{version.Major}.{version.Minor}.{version.Build}";
                }
            }
            catch
            {
                // If anything weird happens, fall back to a static label
                VersionLabel.Text = "v1.0.0";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Total splash time: ~2.2s (0.6s fade-in + 1.0s visible + 0.6s fade-out)
            var storyboard = new Storyboard();

            // Fade IN: 0 -> 1 over 0.6 sec
            var fadeIn = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(0.6),
                BeginTime = TimeSpan.FromSeconds(0.0),
                FillBehavior = FillBehavior.HoldEnd
            };

            // Fade OUT: 1 -> 0 over 0.6 sec, starts after 1.6 sec total
            var fadeOut = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = TimeSpan.FromSeconds(0.6),
                BeginTime = TimeSpan.FromSeconds(1.6),
                FillBehavior = FillBehavior.Stop
            };

            Storyboard.SetTarget(fadeIn, this);
            Storyboard.SetTargetProperty(fadeIn, new PropertyPath(Window.OpacityProperty));

            Storyboard.SetTarget(fadeOut, this);
            Storyboard.SetTargetProperty(fadeOut, new PropertyPath(Window.OpacityProperty));

            storyboard.Children.Add(fadeIn);
            storyboard.Children.Add(fadeOut);

            storyboard.Completed += (s, _) =>
            {
                var main = new MainWindow();
                main.Show();
                this.Close();
            };

            storyboard.Begin();
        }
    }
}
