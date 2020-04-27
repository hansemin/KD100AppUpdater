using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace KD100AppUpdater
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static public string NewAppName { get; set; } = "KD100App.exe";
        static public string AppPath { get; set; }
        static public string AppName { get; set; }

        static public string UpdatePath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;

        protected override void OnStartup(StartupEventArgs args)
        {
            base.OnStartup(args);

            try
            {
                if (args.Args.Length != 2)
                    throw new Exception($"Error Args Length : {args.Args.Length}");

                AppPath = args.Args[0];
                AppName = args.Args[1];

                StartUpdateWindow();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Failed Update", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
        }

        private void StartUpdateWindow()
        {
            var fileNames = Directory.GetFiles(UpdatePath, NewAppName);
            if (fileNames.Length == 0)
            {
                throw new Exception($"Not exist Update file : {NewAppName}");
            }

            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}
