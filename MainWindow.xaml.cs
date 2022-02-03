using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Diagnostics;
using System.Diagnostics;
using DiscordRPC;
using DiscordRPC.Logging;
using Windows.ApplicationModel.DataTransfer;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO.Compression;
using Microsoft.Toolkit.Uwp.Notifications; 

namespace AgsLauncherV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {	
		public DiscordRpcClient client;
        public MainWindow()
        {
            
            InitializeComponent();
            NavigationService ns = NavigationService.GetNavigationService(this);
			/*
			Create a Discord client
			NOTE: If you are using Unity3D, you must use the full constructor and define
			 the pipe connection.
			*/
			client = new DiscordRpcClient("917581646302183554");

			//Set the logger
			client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

			//Subscribe to events
			client.OnReady += (sender, e) =>
			{
				Console.WriteLine("Received Ready from user {0}", e.User.Username);
			};

			client.OnPresenceUpdate += (sender, e) =>
			{
				Console.WriteLine("Received Update! {0}", e.Presence);
			};

			//Connect to the RPC
			client.Initialize();

			//Set the rich presence
			//Call this as many times as you want and anywhere in your code.
			client.SetPresence(new RichPresence()
			{
				Details = "V2 beta launcher",
				State = "Editing JSON data...",
				Assets = new Assets()
				{
					LargeImageKey = "image_large",
					LargeImageText = "well uh,,, hi?",
					SmallImageKey = ""
				}
			});
            try
            {
                Thread.Sleep(2500);
                {
                    try
                    {
                        WelcomeRPCLabel.Content = "Welcome, " + client.CurrentUser.Username + "!";
                    }
                    catch
                    {
                        WelcomeRPCLabel.Content = "Welcome to the AGS Launcher!";
                    }
                }
                if (client.CurrentUser.Username == "kianna")
                {
                    WelcomeRPCLabel.Content = "kys";
                }
                if (client.CurrentUser.Username == "AveryMadness")
                {
                    WelcomeRPCLabel.Content = "Hi, avery!";
                }
                if (client.CurrentUser.Username == "Crunnie")
                {
                    WelcomeRPCLabel.Content = "FUCK U!";
                }
            }
            catch
            {
                WelcomeRPCLabel.Content = "Welcome to the AGS Launcher!";
            }  
            try
            {
                pfp.Source = new BitmapImage(new Uri(client.CurrentUser.GetAvatarURL(User.AvatarFormat.PNG)));
            }
            catch
            {
                pfp.Source = new BitmapImage(new Uri("https://cdn.discordapp.com/icons/907015974669131786/7db27e5f7ea4bd86cb35847c469b70e9.webp?size=96"));
            }
            WebClient webclient = new WebClient();
            webclient.Headers.Add("user-agent", "AGSLauncher-WebClient");
            string DATA = webclient.DownloadString("https://raw.githubusercontent.com/imstillamazedbyit/1q29dks43895r794/main/launcherinfo.json");
            LauncherCloud json = JsonConvert.DeserializeObject<LauncherCloud>(DATA);
            File.WriteAllText(@"fuck.json", DATA);
            VerSTR.Text = json.Version;
            ChangelogLine1.Text = json.ChangelogLine1;
            ChangelogLine2.Text = json.ChangelogLine2;
            ChangelogLine3.Text = json.ChangelogLine3;
            ChangelogLine4.Text = json.ChangelogLine4;
            ChangelogLine5.Text = json.ChangelogLine5;
            BugsLine1.Text = json.BugLine1;
            BugsLine2.Text = json.BugLine2;
            BugsLine3.Text = json.BugLine3;
            BugsLine4.Text = json.BugLine4;
            BugsLine5.Text = json.BugLine5;
        }
        public class LauncherCloud
        {
            public string Version {  get; set; }
            public string VersionInt { get; set; }
            public string ChangelogLine1 { get; set; }
            public string ChangelogLine2 { get; set; }
            public string ChangelogLine3 { get; set; }
            public string ChangelogLine4 { get; set; }
            public string ChangelogLine5 { get; set; }
            public string BugLine1 { get; set; }
            public string BugLine2 { get; set; }
            public string BugLine3 { get; set; }
            public string BugLine4 { get; set; }
            public string BugLine5 { get; set; }
        }
        private void Home(object sender, RoutedEventArgs e)
        {
            try
            {
                HomeButton.IsEnabled = false;
                LaunchButton.IsEnabled = true;
                BugsButton.IsEnabled = true;
                ChangelogButton.IsEnabled = true;
                LaunchButton.Opacity = 1;
                AGSLogo.Opacity = 0;
                ChangelogBugsTitle.Text = "Home";
                VerSTR.Opacity = 0;
                WelcomeText.Opacity = 1;
                LauncherInfoText.Opacity = 1;
                newpath.Visibility = Visibility.Visible;
                LaunchButton.Visibility = Visibility.Visible;
                ChangelogLine1.Opacity = 0;
                ChangelogLine2.Opacity = 0;
                ChangelogLine3.Opacity = 0;
                ChangelogLine4.Opacity = 0;
                ChangelogLine5.Opacity = 0;
                BugsLine1.Opacity = 0;
                BugsLine2.Opacity = 0;
                BugsLine3.Opacity = 0;
                BugsLine4.Opacity = 0;
                BugsLine5.Opacity = 0;
            }
            catch
            {
                MessageBox.Show("A fatal error occured and the AGS Launcher needs to be restarted.", "Fatal error", MessageBoxButton.OK, MessageBoxImage.Error);
                Process.Start("AgsLauncherV2.exe");
                this.Close();
            }
        }
        private void Changelog(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangelogButton.IsEnabled = false;
                HomeButton.IsEnabled = true;
                LaunchButton.IsEnabled = false;
                BugsButton.IsEnabled = true;
                LaunchButton.Opacity = 0;
                AGSLogo.Opacity = 0;
                ChangelogBugsTitle.Text = "Changelog";
                VerSTR.Opacity = 1;
                WelcomeText.Opacity = 0;
                LauncherInfoText.Opacity = 0;
                newpath.Visibility = Visibility.Hidden;
                LaunchButton.Visibility = Visibility.Hidden;
                ChangelogLine1.Opacity = 1;
                ChangelogLine2.Opacity = 1;
                ChangelogLine3.Opacity = 1;
                ChangelogLine4.Opacity = 1;
                ChangelogLine5.Opacity = 1;
                BugsLine1.Opacity = 0;
                BugsLine2.Opacity = 0;
                BugsLine3.Opacity = 0;
                BugsLine4.Opacity = 0;
                BugsLine5.Opacity = 0;
            }
            catch
            {
                MessageBox.Show("A fatal error occured and the AGS Launcher needs to be restarted.", "Fatal error", MessageBoxButton.OK, MessageBoxImage.Error);
                Process.Start("AgsLauncherV2.exe");
                this.Close();
            }
        }
        private void DragBar(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void Bugs(object sender, RoutedEventArgs e)
        {
            try
            {
                BugsButton.IsEnabled = false;
                HomeButton.IsEnabled = true;
                ChangelogButton.IsEnabled = true;
                LaunchButton.IsEnabled = false;
                LaunchButton.Opacity = 0;
                AGSLogo.Opacity = 0;
                ChangelogBugsTitle.Text = "Bugs";
                VerSTR.Opacity = 0;
                WelcomeText.Opacity = 0;
                LauncherInfoText.Opacity = 0;
                newpath.Visibility = Visibility.Hidden;
                LaunchButton.Visibility = Visibility.Hidden;
                ChangelogLine1.Opacity = 0;
                ChangelogLine2.Opacity = 0;
                ChangelogLine3.Opacity = 0;
                ChangelogLine4.Opacity = 0;
                ChangelogLine5.Opacity = 0;
                BugsLine1.Opacity = 1;
                BugsLine2.Opacity = 1;
                BugsLine3.Opacity = 1;
                BugsLine4.Opacity = 1;
                BugsLine5.Opacity = 1;
            }
            catch
            {
                MessageBox.Show("A fatal error occured and the AGS Launcher needs to be restarted.", "Fatal error", MessageBoxButton.OK, MessageBoxImage.Error);
                Process.Start("AgsLauncherV2.exe");
                this.Close();
            }
        }
        private void RpcNameCopy(object sender, RoutedEventArgs e)
        {
            WelcomeRPCLabel.Content = "astolfo hentai not done yet ";
        }
        private void Minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            if(dprog.Opacity == 1)
            {
                MessageBox.Show("AveryGame is not done downloading, and will instead minimize to the system tray. To exit, you can end the 'AGSLauncherV2.exe' process, or alternatively ALT + F4 out of it. (WARNING: THIS ACTION WILL CORRUPT THE DOWNLOAD, AND YOU WILL HAVE TO START OVER", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.WindowState = WindowState.Minimized;
            }
            else
            {
                File.Delete(@"fuck.json");
                this.Close();
            }
        }
        private void addPath(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("path button clicked");
        }

        private void testbutton_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists("/1i9qQNqWOlQcdrZ0qD3NU7WzHKW4h54U/WindowsNoEditor/Binaries/Win64/AveryGame.exe"))
            {
                MessageBoxResult result;
                result = MessageBox.Show("Avery Game was not found! Would you like to install?", "Error", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (result == MessageBoxResult.Yes)
                {
                    MessageBoxResult downconfirm;
                    downconfirm = MessageBox.Show("The Avery Game launcher will minimize and will not be usable until the download is finished, are you sure?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (downconfirm == MessageBoxResult.Yes)
                    {
                        LaunchButton.Content = "Downloading AGS...";
                        dprog.Opacity = 1;
                        kys();
                    }
                    else
                    {

                    }
                }
                else
                {
                    MessageBox.Show("AveryGame was not found on your system. Please restart the launcher or select a custom path.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Process.Start("/1i9qQNqWOlQcdrZ0qD3NU7WzHKW4h54U/WindowsNoEditor/Binaries/Win64/AveryGame.exe");
            }
            
        }
        private void kys()
        {
            string gzip = "1i9qQNqWOlQcdrZ0qD3NU7WzHKW4h54U.zip";
            WebClient webclient = new WebClient();
            string DATA = webclient.DownloadString("https://raw.githubusercontent.com/imstillamazedbyit/1q29dks43895r794/main/launcherinfo.json");
            LauncherCloud json = JsonConvert.DeserializeObject<LauncherCloud>(DATA);
            string dpath = json.VersionInt;
            new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText("Downloading AveryGame...")
                .AddText("Launcher has been minimized.")
                .Show();
            webclient.DownloadFile("https://www.googleapis.com/drive/v3/files/1i9qQNqWOlQcdrZ0qD3NU7WzHKW4h54U_?alt=media&key=AIzaSyD3hsuSxEFnxZkgadbUSPt_iyx8qJ4lwWQ", @"./" + json.VersionInt);
        }
    }
}