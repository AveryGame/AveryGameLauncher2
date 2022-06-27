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
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher");
            Services.LogSVC.CreateLogFile();
            Services.LogSVC.LogWindowInit();
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\EnvPath.txt", Environment.CurrentDirectory + "\\AGSLauncherV2.exe");
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Dir.txt", Environment.CurrentDirectory);
            SpawnWindowViewport();
        }
        private void Minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void DragMove(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private static void SpawnWindowViewport()
        {
            var AppWindow = new MainWindow();
            AppWindow.ShowDialog();
        }
    }
}
