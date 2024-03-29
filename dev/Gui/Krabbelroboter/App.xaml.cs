﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Krabbelroboter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new Views.MainView()
            {
                DataContext = new ViewModels.MainViewModel()
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
