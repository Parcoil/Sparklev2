﻿// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace Sparkle.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "Sparkle (Beta)";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Home",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                TargetPageType = typeof(Views.Pages.DashboardPage)
            },
            //new NavigationViewItem()
            //{
            //    Content = "Data",
            //    Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
            //    TargetPageType = typeof(Views.Pages.DataPage)
            //},
              new NavigationViewItem()
            {
                Content = "Debloat",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Box24 },
                TargetPageType = typeof(Views.Pages.DebloatPage)
            },
             new NavigationViewItem()
            {
                Content = "Clean",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Broom24 },
                TargetPageType = typeof(Views.Pages.CleanPage)
            },
                        new NavigationViewItem()
            {
                Content = "Drivers",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Sparkle24 },
                TargetPageType = typeof(Views.Pages.DriverPage)
            },
            // new NavigationViewItem()
            //{
            //    Content = "Apps",
            //    Icon = new SymbolIcon { Symbol = SymbolRegular.Apps24 },
            //    TargetPageType = typeof(Views.Pages.TestPage)
            //}
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Settings",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };
    }
}
