using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace DigitalTacticalWorksheet
{
    public partial class SplashWindow : Window
    {
        public SplashWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // total visible time ~ 2 seconds (1.3s static + 0.7s fade)
            var storyboard = new Storyboard();

            var fade = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = TimeSpan.FromSeconds(0.7),
                BeginTime = TimeSpan.FromSeconds(1.3),
                FillBehavior = FillBehavior.Stop
            };

            Storyboard.SetTarget(fade, this);
            Storyboard.SetTargetProperty(fade, new PropertyPath(Window.OpacityProperty));

            storyboard.Children.Add(fade);

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
