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

            // Get the total physical memory (in gigabytes)
            double totalPhysicalMemoryGB = GetTotalPhysicalMemory() / 1024.0; // Convert from kilobytes to gigabytes

            // Calculate the amount of RAM free (in gigabytes)
            double freeMemoryGB = totalPhysicalMemoryGB - availableMemoryGB;

            // Display free memory in gigabytes in the second text block (freeMemoryTextBlock)
            Dispatcher.Invoke(() =>
            {
                freeMemoryTextBlock.Text = $"Free Memory: {freeMemoryGB:F2} GB";
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
