using System;
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            try
            {
                while (true)
                {
                    Process[] p = Process.GetProcessesByName(App.AppName);
                    if (p.Length == 0)
                    {
                        break;
                    }
                    await Task.Delay(100);
                    p[0].Kill();
                }

               
                string currencyDBFilePath = System.IO.Path.Combine(App.AppPath, "Kisan.Cdmp.Currency.db");
                if (File.Exists(currencyDBFilePath))
                {                    
                    File.Delete(currencyDBFilePath);
                }

                Copy(App.UpdatePath, App.AppPath);

                MessageBox.Show("Update Done.", "KD-100 App", MessageBoxButton.OK, MessageBoxImage.Information);
                Process.Start(App.AppPath + App.NewAppName);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            Environment.Exit(0);
        }

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            var diSource = new DirectoryInfo(sourceDirectory);
            var diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            foreach (FileInfo fi in source.GetFiles())
            {
                if (fi.Extension.Contains("sys"))                                    
                    continue;                

                try
                {
                    fi.CopyTo(System.IO.Path.Combine(target.FullName, fi.Name), true);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }


            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}
