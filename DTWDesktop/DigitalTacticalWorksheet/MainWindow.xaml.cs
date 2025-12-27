using Microsoft.Web.WebView2.Core;
using System;
using System.Diagnostics;
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
            // Ensure WebView2 is ready
            await Browser.EnsureCoreWebView2Async();

            // Allowed root domain (host only)
            string allowedHost = "www.digitaltacticalworksheets.com";

            // Navigate AFTER WebView2 is initialized
            Browser.Source = new Uri("https://www.digitaltacticalworksheets.com/");

            // Navigation control
            Browser.CoreWebView2.NavigationStarting += (s, args) =>
            {
                try
                {
                    var uri = new Uri(args.Uri);

                    // Allow our domain + subdomains
                    if (!uri.Host.EndsWith(allowedHost, StringComparison.OrdinalIgnoreCase))
                    {
                        // Block in-app navigation
                        args.Cancel = true;

                        // Open externally instead
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = args.Uri,
                            UseShellExecute = true
                        });
                    }
                }
                catch
                {
                    // Block anything malformed
                    args.Cancel = true;
                }
            };
        }
    }
}
