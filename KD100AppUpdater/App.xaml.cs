using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KD100AppUpdater
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static public string NewAppName { get; set; } = "TestProjectApp.exe";
        static public string AppPath { get; set; }
        static public string AppName { get; set; }

        protected override void OnStartup(StartupEventArgs args)
        {
            base.OnStartup(args);

            //foreach (var arg in args.Args)
            //{
            //    MessageBox.Show(arg);
            //}

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
            var excuteFileName = NewAppName;
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var fileNames = Directory.GetFiles(basePath, excuteFileName);
            if (fileNames.Length == 0)
            {
                throw new Exception($"Not exist Update file : {excuteFileName}");
            }


            Process[] p = Process.GetProcessesByName(AppName);
            if (p.Length == 0)
            {
                throw new Exception($"Not Find Exit Process : {AppName}");
            }
            p[0].Kill();

            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}
