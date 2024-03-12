using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sparkle.Views.Pages
{
    /// <summary>
    /// Interaction logic for DebloatPage.xaml
    /// </summary>
    public partial class DebloatPage : Page
    {
        public DebloatPage()
        {
            InitializeComponent();
        }

        private void Debloat_Click(object sender, RoutedEventArgs e)
        {
            Debloat();
        }

        private void Debloat()
        {
            string url = "https://raw.githubusercontent.com/Parcoil/files/main/debloat.bat"; // Updated with the provided URL
            string downloadPath = @"C:\Program Files (x86)\Parcoil\Sparkle\clean.bat";

            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(url, downloadPath);
                }

                if (File.Exists(downloadPath))
                {
                    // Prompting for admin rights when executing the batch file
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.FileName = downloadPath;
                    startInfo.Verb = "runas"; // Prompts for admin rights
                    System.Diagnostics.Process.Start(startInfo);
                }
                else
                {
                    MessageBox.Show("There was an error when debloating");
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show("Web exception occurred: " + ex.Message);
                // Log the exception details for further investigation
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                // Log the exception details for further investigation
            }
        }
    }
}
