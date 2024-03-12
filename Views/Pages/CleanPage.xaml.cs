using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sparkle.Views.Pages
{
    /// <summary>
    /// Interaction logic for CleanPage.xaml
    /// </summary>
    public partial class CleanPage : Page
    {
        public CleanPage()
        {
            InitializeComponent();
        }

        private void Clean_Click(object sender, RoutedEventArgs e)
        {
            Clean();
        }

        private void Clean()
        {
            string url = "https://raw.githubusercontent.com/Parcoil/files/main/clean.bat"; // Updated with the provided URL
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
                    MessageBox.Show("Batch file does not exist after download.");
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
            static void TrimWorkingSet()
            {
                try
                {
                    // Get the current process
                    Process currentProcess = Process.GetCurrentProcess();

                    // Display the current working set size before trimming
                    Console.WriteLine("Working Set Size (Before Trim): " + currentProcess.WorkingSet64 / 1024 + " KB");

                    // Trim the working set
                    bool success = NativeMethods.SetProcessWorkingSetSize(currentProcess.Handle, -1, -1);

                    if (success)
                    {
                        Console.WriteLine("Working set trimmed successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to trim working set.");
                    }

                    // Display the working set size after trimming
                    Console.WriteLine("Working Set Size (After Trim): " + currentProcess.WorkingSet64 / 1024 + " KB");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error trimming working set: " + ex.Message);
                }
                while (true)
                {
                    // Trim the process's working set
                    TrimWorkingSet();

                    // Wait for 5 minutes before repeating
                    Thread.Sleep(TimeSpan.FromMinutes(5));
                }
            }
        }
        class NativeMethods
        {
            [System.Runtime.InteropServices.DllImport("kernel32.dll")]
            public static extern bool SetProcessWorkingSetSize(IntPtr proc, int min, int max);
        }

    }
}

