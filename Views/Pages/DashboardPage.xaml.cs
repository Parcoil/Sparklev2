using System;
using System.Diagnostics;
using System.Management;
using System.Timers;
using Sparkle.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Sparkle.Views.Pages
{
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        private readonly System.Timers.Timer timer;
        private PerformanceCounter memoryCounter;

        public DashboardViewModel ViewModel { get; }

        public DashboardPage(DashboardViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
            DisplayHardwareInfo();

            // Initialize the timer
            timer = new System.Timers.Timer();
            timer.Interval = 1000; // 1000 milliseconds = 1 second
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            // Initialize the performance counter for memory usage
            memoryCounter = new PerformanceCounter("Memory", "Available MBytes");

            // Call the method to update memory usage when the page is initialized
            UpdateMemoryUsage();
        }
        private void DisplayHardwareInfo()
        {
            // Get GPU Name
            string gpuName = GetHardwareInfo("Win32_VideoController", "Name");

            // Get CPU Name
            string cpuName = GetHardwareInfo("Win32_Processor", "Name");

            // Get Motherboard Name
            string motherboardName = GetHardwareInfo("Win32_BaseBoard", "Product");

            // Get Installed RAM
            string installedRAM = GetInstalledRAM();

            // Update TextBlocks with retrieved information
            gpuTextBlock.Text = gpuName;
            cpuTextBlock.Text = cpuName;
            motherboardTextBlock.Text = motherboardName;
            ramTextBlock.Text = installedRAM;
        }
        private string GetHardwareInfo(string wmiClass, string property)
        {
            string result = "Information not available";

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM " + wmiClass);
                foreach (ManagementObject obj in searcher.Get())
                {
                    result = obj[property].ToString();
                    break; // We only need the first result
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions or errors
                Console.WriteLine("Error retrieving hardware information: " + ex.Message);
            }

            return result;
        }

        private string GetInstalledRAM()
        {
            string result = "Information not available";

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
                foreach (ManagementObject obj in searcher.Get())
                {
                    ulong ram = Convert.ToUInt64(obj["TotalPhysicalMemory"]);
                    result = (ram / (1024 * 1024)).ToString() + " MB";
                    break; // We only need the first result
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions or errors
                Console.WriteLine("Error retrieving RAM information: " + ex.Message);
            }

            return result;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Update memory usage every second
            UpdateMemoryUsage();
        }

        private void UpdateMemoryUsage()
        {
            // Get the available physical memory (in gigabytes) using the performance counter
            double availableMemoryGB = memoryCounter.NextValue() / 1024.0; // Convert from megabytes to gigabytes

            // Display available memory in gigabytes in the first text block (memoryUsageTextBlock)
            // Ensure this method is called from the UI thread if you're updating UI controls
            Dispatcher.Invoke(() =>
            {
                memoryUsageTextBlock.Text = $"Available Memory: {availableMemoryGB:F2} GB";
            });
        }

        private ulong GetTotalPhysicalMemory()
        {
            ulong totalPhysicalMemory = 0;
            ObjectQuery objectQuery = new ObjectQuery("SELECT * FROM Win32_ComputerSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(objectQuery);
            foreach (ManagementObject managementObject in searcher.Get())
            {
                totalPhysicalMemory = (ulong)managementObject["TotalPhysicalMemory"];
                break; // Assuming there's only one result
            }
            return totalPhysicalMemory;
        }
    }
}
