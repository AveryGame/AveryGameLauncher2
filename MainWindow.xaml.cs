using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Diagnostics;
using DiscordRPC;
using DiscordRPC.Logging;
using System.IO;
using System.Net;
using Newtonsoft.Json;
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
            try
            {
                NavigationService ns = NavigationService.GetNavigationService(this);
                /*
                Create a Discord client
                NOTE: If you are using Unity3D, you must use the full constructor and define
                 the pipe connection.
                */
                client = new DiscordRpcClient("939285353355935774");

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
                    State = "In the launcher",
                    Assets = new Assets()
                    {
                        LargeImageKey = "agsgrey",
                        LargeImageText = "Release 2.0",
                        SmallImageKey = ""
                    }
                });
                WebClient webclient = new WebClient();
                string DATA = webclient.DownloadString("https://raw.githubusercontent.com/imstillamazedbyit/1q29dks43895r794/main/launcherinfo.json");
                LauncherCloud json = JsonConvert.DeserializeObject<LauncherCloud>(DATA);
                File.WriteAllText(@"fuck.json", DATA);
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
                    if (client.CurrentUser.Username == json.KiannaUN)
                    {
                        WelcomeRPCLabel.Content = "kys";
                    }
                    if (client.CurrentUser.Username == json.AveryUN)
                    {
                        WelcomeRPCLabel.Content = "Hi, avery!";
                    }
                    if (client.CurrentUser.Username == json.CrunnieUN)
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
                if (File.Exists("1i9qQNqWOlQcdrZ0qD3NU7WzHKW4h54U.zip"))
                {
                    File.Delete("1i9qQNqWOlQcdrZ0qD3NU7WzHKW4h54U.zip");
                }
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
            catch
            {
                MessageBox.Show("A fatal error occurred while opening the Avery Game launcher. Please make sure you are connected to the internet and try again. If the problem persists, report the issue and when it is occurring in #bug-reports in the Avery Game discord server.", "Fatal error!", MessageBoxButton.OK, MessageBoxImage.Stop);
                this.Close();
            }
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
            public string KiannaUN { get; set; }
            public string AveryUN { get; set; }
            public string CrunnieUN { get; set; }
            public string VersionFolderInt { get; set; }
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
                MessageBox.Show("A fatal error occurred while opening the Avery Game launcher. Please make sure you are connected to the internet and try again. If the problem persists, report the issue and when it is occurring in #bug-reports in the Avery Game discord server.", "Fatal error!", MessageBoxButton.OK, MessageBoxImage.Stop);
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
                MessageBox.Show("A fatal error occurred while opening the Avery Game launcher. Please make sure you are connected to the internet and try again. If the problem persists, report the issue and when it is occurring in #bug-reports in the Avery Game discord server.", "Fatal error!", MessageBoxButton.OK, MessageBoxImage.Stop);
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
                ChangelogBugsTitle.Opacity = 1;
                ChangelogBugsTitle.Text = "Bugs";
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
                MessageBox.Show("A fatal error occurred while opening the Avery Game launcher. Please make sure you are connected to the internet and try again. If the problem persists, report the issue and when it is occurring in #bug-reports in the Avery Game discord server.", "Fatal error!", MessageBoxButton.OK, MessageBoxImage.Stop);
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
                MessageBoxResult result = MessageBox.Show("AveryGame is not done downloading, and exiting will corrupt the download. Are you sure?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result == MessageBoxResult.Yes)
                {
                    this.Close();
                }
                else
                {

                }
            }
            else
            {
                File.Delete(@"fuck.json");
                this.Close();
            }
        }

        private void testbutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WebClient webclient = new WebClient();
                string DATA = webclient.DownloadString("https://raw.githubusercontent.com/imstillamazedbyit/1q29dks43895r794/main/launcherinfo.json");
                LauncherCloud json = JsonConvert.DeserializeObject<LauncherCloud>(DATA);
                if (!File.Exists(System.IO.Path.Combine(json.VersionFolderInt +  "/4.2/WindowsNoEditor/AveryGame.exe")))
                {
                    MessageBoxResult result;
                    result = MessageBox.Show("Avery Game was not found! Would you like to install?", "Error", MessageBoxButton.YesNo, MessageBoxImage.Error);
                    if (result == MessageBoxResult.Yes)
                    {
                        LaunchButton.Content = "Downloading AGS...";
                        dprog.Opacity = 1;
                        kys();
                    }
                    else
                    {
                        MessageBox.Show("Avery Game was not found on your system. Please restart the launcher or select a custom path.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    string latest = File.ReadAllText(@"gclient.json");
                    if (latest.Contains(json.VersionInt))
                    {
                        try
                        {
                            Process.Start(System.IO.Path.Combine(json.VersionInt + "/WindowsNoEditor/AveryGame.exe"));
                            new ToastContentBuilder()
                                .AddArgument("action", "viewConversation")
                                .AddArgument("conversationId", 9813)
                                .AddText("Avery Game")
                                .AddText("Game launched")
                            .Show();
                        }
                        catch
                        {
                            Process.Start(System.IO.Path.Combine(json.VersionInt + "/4.2/WindowsNoEditor/AveryGame.exe"));
                            new ToastContentBuilder()
                                .AddArgument("action", "viewConversation")
                                .AddArgument("conversationId", 9813)
                                .AddText("Avery Game")
                                .AddText("Game launched")
                            .Show();
                        }
                    }
                    if (!latest.Contains(json.VersionInt))
                    {
                        MessageBoxResult updateAvail = MessageBox.Show("AveryGame has an update. Would you like to install it now?", "Notice", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (updateAvail == MessageBoxResult.Yes)
                        {
                            LaunchButton.Content = "Downloading AGS...";
                            dprog.Opacity = 1;
                            kys();
                        }
                        if (updateAvail == MessageBoxResult.No)
                        {
                            try
                            {
                                Process.Start(System.IO.Path.Combine(json.VersionInt + "/WindowsNoEditor/AveryGame.exe"));
                            }
                            catch
                            {
                                Process.Start(System.IO.Path.Combine(json.VersionInt + "/4.2/WindowsNoEditor/AveryGame.exe"));
                            }
                        }
                    }

                }
            }
            catch
            {
                MessageBox.Show("A fatal error occurred while opening the Avery Game launcher. Please make sure you are connected to the internet and try again. If the problem persists, report the issue and when it is occurring in #bug-reports in the Avery Game discord server.", "Fatal error!", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
        private void DownloadGameCompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            try
            {
                WebClient webclient = new WebClient();
                string DATA = webclient.DownloadString("https://raw.githubusercontent.com/imstillamazedbyit/1q29dks43895r794/main/launcherinfo.json");
                LauncherCloud json = JsonConvert.DeserializeObject<LauncherCloud>(DATA);
                string gzip = "1i9qQNqWOlQcdrZ0qD3NU7WzHKW4h54U.zip";
                ZipFile.ExtractToDirectory(gzip, json.VersionInt);
                File.Delete(gzip);
                dprog.Opacity = 0;
                LaunchButton.Content = "Launch Avery Game";
                File.WriteAllText(@"gclient.json", json.VersionInt);
                new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText("Avery Game downloading status")
                .AddText("Avery Game has finished downloading!")
                .Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error finishing download: {ex}");
            }
        }
        private void kys()
        {
            try
            {


                string gzip = "1i9qQNqWOlQcdrZ0qD3NU7WzHKW4h54U.zip";
                WebClient webclient = new WebClient();
                string DATA = webclient.DownloadString("https://raw.githubusercontent.com/imstillamazedbyit/1q29dks43895r794/main/launcherinfo.json");
                LauncherCloud json = JsonConvert.DeserializeObject<LauncherCloud>(DATA);
                string dpath = json.VersionInt;
                new ToastContentBuilder()
                    .AddArgument("action", "viewConversation")
                    .AddArgument("conversationId", 9813)
                    .AddText("Avery Game download status")
                    .AddText("Download started...")
                    .Show();
                webclient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadGameCompletedCallback);
                webclient.DownloadFileAsync(new Uri("https://www.googleapis.com/drive/v3/files/1i9qQNqWOlQcdrZ0qD3NU7WzHKW4h54U_?alt=media&key=AIzaSyD3hsuSxEFnxZkgadbUSPt_iyx8qJ4lwWQ"), gzip);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error finishing download: {ex}");
            }
        }
    }
}