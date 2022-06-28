using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AgsLauncherV2.Services;

namespace AgsLauncherV2
{
    /// <summary>
    /// Interaction logic for FTSWindow.xaml
    /// </summary>
    public partial class FTSWindow : Window
    {
        public FTSWindow()
        {
            InitializeComponent();
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher"))
            {
                L1.Opacity = 0;
                L3.Opacity = 0;
                Title.Text = "How did you get here?";
                L2.Text = "All of your files are here... 🤔";
            }
        }
        private void Minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void DragMove(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void SpawnWindowViewport()
        {
            //environment.exit will kill the whole process, just hide window and dont spawn it on next run is the only fix
            this.Hide();
            var AppWindow = new MainWindow();
            AppWindow.ShowDialog();
        }

        private void ContinueBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher");
                LogSVC.CreateLogFile();
                LogSVC.LogWindowInit();
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\EnvPath.txt", Environment.CurrentDirectory + "\\AGSLauncherV2.exe");
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Dir.txt", Environment.CurrentDirectory);
                L1.Opacity = 0;
                L3.Opacity = 0;
                Title.Text = "All done!";
                L2.Text = "Click proceed to enter the launcher!";
            } else if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher"))
            {
                SpawnWindowViewport();
            }
        }
    }
}
