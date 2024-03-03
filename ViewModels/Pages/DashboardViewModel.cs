// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System;
using System.Diagnostics;
using System.Windows;


namespace Sparkle.ViewModels.Pages
{
    public partial class DashboardViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _counter = 0;

        [RelayCommand]
        private void OnCounterIncrement()
        {
            Counter++;
        }

        private string _memoryUsage;
        public string MemoryUsage
        {
            get => _memoryUsage;
            set
            {
                _memoryUsage = value;
                OnPropertyChanged();
            }
        }

        public DashboardViewModel()
        {
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

            // Set memory usage property
            MemoryUsage = $"Memory Usage: {memoryUsageMB:F2} MB";
        }
    }
}
