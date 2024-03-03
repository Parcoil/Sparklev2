// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Diagnostics;
using Sparkle.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Sparkle.Views.Pages
{
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        public DashboardViewModel ViewModel { get; }

        public DashboardPage(DashboardViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            // Call the method to update memory usage when the page is initialized
            UpdateMemoryUsage();
        }

        private void UpdateMemoryUsage()
        {
            // Get the current process
            Process currentProcess = Process.GetCurrentProcess();

            // Get the private memory usage of the process (in bytes)
            long memoryUsageBytes = currentProcess.PrivateMemorySize64;

            // Convert bytes to megabytes
            double memoryUsageMB = memoryUsageBytes / (1024.0 * 1024.0);

            // Display memory usage in the TextBox (assuming you have a TextBox named memoryUsageTextBox)
            memoryUsageTextBlock.Text = $"Memory Usage: {memoryUsageMB:F2} %";
        }
    }
}

