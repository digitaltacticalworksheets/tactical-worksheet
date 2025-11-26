using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace DigitalTacticalWorksheet
{
    public partial class SplashWindow : Window
    {
        public SplashWindow()
        {
            InitializeComponent();
            Loaded += SplashWindow_Loaded;
        }

        private async void SplashWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Fade in
            DoubleAnimation fadeIn = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromMilliseconds(400)));
            this.BeginAnimation(Window.OpacityProperty, fadeIn);

            // Keep splash visible for 1.8 seconds
            await Task.Delay(1800);

            // Fade out
            DoubleAnimation fadeOut = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromMilliseconds(400)));
            fadeOut.Completed += (s, a) =>
            {
                try
                {
                    // Show the main window AFTER fade-out
                    MainWindow main = new MainWindow();
                    Application.Current.MainWindow = main;
                    main.Show();
                }
                finally
                {
                    this.Close();
                }
            };

            this.BeginAnimation(Window.OpacityProperty, fadeOut);
        }
    }
}
git add