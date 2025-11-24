using Microsoft.Web.WebView2.Core;
using System;
using System.Windows;

namespace DigitalTacticalWorksheet
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await Browser.EnsureCoreWebView2Async();

            // Allowed URL + host
            string allowedUrl = "https://digitaltacticalworksheets.github.io/tactical-worksheet";
            string allowedHost = "digitaltacticalworksheets.github.io";

            // Initial page
            Browser.Source = new Uri(allowedUrl);

            // Lock navigation
            Browser.CoreWebView2.NavigationStarting += (s, args) =>
            {
                try
                {
                    var uri = new Uri(args.Uri);

                    // Only allow our host
                    if (!uri.Host.Equals(allowedHost, StringComparison.OrdinalIgnoreCase))
                    {
                        args.Cancel = true;
                    }
                }
                catch
                {
                    // If it can't parse the URL, block it
                    args.Cancel = true;
                }
            };
        }
    }
}

