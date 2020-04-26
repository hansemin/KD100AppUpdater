﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KD100AppUpdater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var fileNames = System.IO.Directory.GetFiles(basePath);

            foreach (var fileName in fileNames)
            {
                File.Copy(fileName, App.AppPath + System.IO.Path.GetFileName(fileName), true);
            }

            Process.Start(App.AppPath + App.NewAppName);
            Environment.Exit(0);
        }
    }
}