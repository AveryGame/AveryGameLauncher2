using System;
using System.IO;
using DiscordRPC;
using System.Net;
using System.Windows;
using Newtonsoft.Json;
using System.Threading;
using DiscordRPC.Logging;
using System.Windows.Input;
using System.IO.Compression;
using System.Windows.Media.Imaging;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Windows.Media;

namespace AgsLauncherV2
{
    //wh test commit
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DiscordRpcClient client;
        public MainWindow()
        {
            InitializeComponent();
            //try
            //{
            Services.LogSVC.CreateLogFile();
            Services.LogSVC.LogJSEvent();
            WebClient webclient = new WebClient();
            webclient.DownloadFile("https://raw.githubusercontent.com/imstillamazedbyit/1q29dks43895r794/main/launcherinfo.json", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\fuck.json");
            Services.LogSVC.LogJSDownload();
            string DATA = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\fuck.json");
            Services.LogSVC.LogJSRead();
            LauncherCloud json = JsonConvert.DeserializeObject<LauncherCloud>(DATA);
            //set client id
            client = new DiscordRpcClient("939285353355935774");
            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };
            client.OnReady += (sender, e) =>
            {
                Console.WriteLine("Received Ready from user {0}", e.User.Username);
            };
            client.OnPresenceUpdate += (sender, e) =>
            {
                Console.WriteLine("Received Update! {0}", e.Presence);
            };
            //initialize discord rpc service
            client.Initialize();
            //set rpc
            client.SetPresence(new RichPresence()
            {
                Details = "V2 launcher",
                State = "In the launcher",
                Assets = new Assets()
                {
                    LargeImageKey = "agsgrey",
                    LargeImageText = ReleaseString.Text,
                    SmallImageKey = ""
                }
            });
            Services.LogSVC.LogRPC();
            //checking for program files directory
            if (!Directory.Exists(filepath + "\\AveryGame Launcher"))
            {
                //create program files directory if it doesnt exist
                Directory.CreateDirectory(filepath + "\\AveryGame Launcher");
            }
            //check if log file doesnt exist
            Services.LogSVC.CreateLogFile();
            //check if log file exists
            Services.LogSVC.LogWindowInit();
            //write needed launcher updater content
            File.WriteAllText(filepath + "\\AveryGame Launcher\\EnvPath.txt", Environment.CurrentDirectory + "\\AGSLauncherV2.exe");
            File.WriteAllText(filepath + "\\AveryGame Launcher\\Dir.txt", Environment.CurrentDirectory);
            try
            {
                Thread.Sleep(2500);
                {
                    try
                    {
                        //set welcome rpc content
                        WelcomeRPCLabel.Content = "Welcome, " + client.CurrentUser.Username + "!";
                    }
                    catch
                    {
                        //set fallback content
                        WelcomeRPCLabel.Content = "Welcome to the AGS Launcher!";
                    }
                }
                //check if user is a developer
                if (client.CurrentUser.Username != json.KiannaUN || client.CurrentUser.Username != json.AveryUN || client.CurrentUser.Username != json.CrunnieUN)
                {
                    VerSTR.Text = json.Version + " - " + json.LauncherString;
                }
                //check if developer is kianna
                if (client.CurrentUser.Username == json.KiannaUN)
                {
                    WelcomeRPCLabel.Content = "kys";
                    VerSTR.Text = json.DevLauncherString;
                }
                //check if developer is avery
                if (client.CurrentUser.Username == json.AveryUN)
                {
                    WelcomeRPCLabel.Content = "Hi, avery!";
                    VerSTR.Text = json.DevLauncherString;
                }
                //check if developer is crunnie
                if (client.CurrentUser.Username == json.CrunnieUN)
                {
                    WelcomeRPCLabel.Content = "FUCK U!";
                    VerSTR.Text = json.DevLauncherString;
                }
            }
            catch
            {
                //useless catch i guess lol
            }
            try
            {
                //set pfp source to user pfp 
                //this is grabbed from rpc
                pfp.Source = new BitmapImage(new Uri(client.CurrentUser.GetAvatarURL(User.AvatarFormat.PNG)));
            }
            catch
            {
                //if pfp grab from rpc fails this is the fallback
                pfp.Source = new BitmapImage(new Uri("https://cdn.discordapp.com/app-assets/939285353355935774/939285441323077632.png"));
            }
            //check if corrupted game zip exists
            if (File.Exists("1i9qQNqWOlQcdrZ0qD3NU7WzHKW4h54U.zip"))
            {
                //delete corrupted game zip
                File.Delete("1i9qQNqWOlQcdrZ0qD3NU7WzHKW4h54U.zip");
                //log corrupted zip deletion
                Services.LogSVC.LogCorruptZipDelete();
            }
            //check if unneeded download helper folder exists (temporary)
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\DownloadHelper"))
            {
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\DownloadHelper", true);
            }
            //set version descriptions
            VerSTR.Text = json.Version + " - " + json.LauncherString;
            //log
            Services.LogSVC.LogJSSet();
            //begin setting uncollapsed menu elements
            HideMenu.Opacity = 0;
            CollapseCB.Opacity = 0;
            Notice.Opacity = 0;
            Arguments.Opacity = 0;
            args.Opacity = 0;
            //check if client settings exists
            //this is only written if the user has collapsed the menu. 
            //if it does not exist the menu will fallback to its collapsed state
            if (File.Exists(filepath + "\\AveryGame Launcher\\Clientsettings.json"))
            {
                string js2 = File.ReadAllText(filepath + "\\AveryGame Launcher\\Clientsettings.json");
                LauncherCloud z = JsonConvert.DeserializeObject<LauncherCloud>(js2);
                //read json data
                //check if collapse menu option is set to true
                if (z.CollapseMenu == "True")
                {
                    //set collapsed menu elements
                    CollapseCB.IsChecked = true;
                    Uncollapsed.Opacity = 0;
                    AGSLogo.Margin = new Thickness(0, 0, 56, 11);
                    HomeButton.Visibility = Visibility.Hidden;
                    ChangelogButton.Visibility = Visibility.Hidden;
                    BugsButton.Visibility = Visibility.Hidden;
                    ColSettings.Visibility = Visibility.Visible;
                    SettingsButton.Visibility = Visibility.Hidden;
                    AveryGame.Opacity = 0;
                    AGSLog.Opacity = 1;
                    ColHomeButton.Visibility = Visibility.Visible;
                    ColChangelogButton.Visibility = Visibility.Visible;
                    ColBugsButton.Visibility = Visibility.Visible;
                    ColNews.Visibility = Visibility.Visible;
                    NewsButton.Visibility = Visibility.Hidden;
                    UncolHome.Opacity = 0;
                    UncolChangelog.Opacity = 0;
                    UncolBugs.Opacity = 0;
                    UncolHome.Margin = new Thickness(69, 69, 69, 69);
                    UncolChangelog.Margin = new Thickness(69, 69, 69, 69);
                    UncolBugs.Margin = new Thickness(69, 69, 69, 69);
                    //log set
                    Services.LogSVC.BtnLogic.LogColElements();
                }
                else
                {
                    //set checkbox state
                    CollapseCB.IsChecked = false;
                    VerSTR.Text = "" + json.Version + " - " + json.LauncherString;
                    CreditLine1.Opacity = 1;
                    CreditLine2.Opacity = 1;
                    //log data
                    Services.LogSVC.BtnLogic.LogUncolElements();
                }
            }
            else
            {
                //write client settings
                File.WriteAllText(@filepath + "\\AveryGame Launcher\\Clientsettings.json", "{ \"CollapseMenu\":\"" + CollapseCB.IsChecked + "\" }");
                //this is not possible in LogV2, sorry.
                File.AppendAllText(filepath + "\\AveryGame Launcher\\Logs\\LogV2.txt", "Wrote collapse menu checkbox information to json. New state: " + CollapseCB.IsChecked + Environment.NewLine);
            }
            /*
            if (json.LauncherVer != ReleaseString.Text)
            {
                //show toast notif
                new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText("Avery Game download status")
                .AddText("There is an update available for the launcher, installing...")
                .Show();
                Services.LogSVC.LogToastNotif();
                //log updater download
                Services.LogSVC.LogUpdaterDownload();
                //set callbacks
                webclient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(help);
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\DownloadHelper");
                //start downloading 
                webclient.DownloadFileAsync(new Uri("https://www.googleapis.com/drive/v3/files/1MBkHHCCuYiJO2Z19OT6djQmnPY0zc6Qv?alt=media&key=AIzaSyD3hsuSxEFnxZkgadbUSPt_iyx8qJ4lwWQ"), Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\DownloadHelper\\Release.zip");
            }
        }
        //if something causes the launher to crash it will log the error and show the user a messagebox
        catch (Exception ex)
        {
            Services.LogSVC.LogFatalErr();
            ErrorLogging(ex);
            MessageBox.Show(ex.Message, "Fatal error!", MessageBoxButton.OK, MessageBoxImage.Stop);
            this.Close();
        }*/
        }/*
        //im going mental
        //im about to get real
        private void help(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            try
            {
                //extract launcher updater
                ZipFile.ExtractToDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\DownloadHelper\\Release.zip", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\DownloadHelper");
                Services.LogSVC.LogUpdaterExtract();
                //start launcher updater
                System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\DownloadHelper\\AGSLauncherUpdater.exe");
                //log process start
                Services.LogSVC.LogUpdaterStart();
                //log launcher shutdown
                Services.LogSVC.LogLauncherShutdown();
                //close launcher
                //if i dont it will crash the updater
                this.Close();
            }
            catch (Exception ex)
            {
                //log error
                Services.LogSVC.LogLauncherShutdown();
                ErrorLogging(ex);
                MessageBox.Show($"Error starting launcher update");
            }
        }*/

        public static void ErrorLogging(Exception ex)
        {
            //this is not possible in LogV2, sorry
            using (StreamWriter sw = File.AppendText(filepath + "\\AveryGame Launcher\\Logs\\LogV2.txt"))
            {
                //logic for all error logging
                sw.WriteLine("=============Error Logging ===========");
                sw.WriteLine("===========Start============= " + DateTime.Now);
                sw.WriteLine("Error Message: " + ex.Message);
                sw.WriteLine("Stack Trace: " + ex.StackTrace);
                sw.WriteLine("===========End============= " + DateTime.Now);

            }
        }
        private static readonly string filepath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public class LauncherCloud
        {
            //all json strings
            public string Version { get; set; }
            public string VersionInt { get; set; }
            public string ChangelogLine1 { get; set; }
            public string ChangelogLine2 { get; set; }
            public string ChangelogLine3 { get; set; }
            public string ChangelogLine4 { get; set; }
            public string ChangelogLine5 { get; set; }
            public string BalanceCL1 { get; set; }
            public string BalanceCL2 { get; set; }
            public string BalanceCL3 { get; set; }
            public string BalanceCL4 { get; set; }
            public string BalanceCL5 { get; set; }
            public string BugLine1 { get; set; }
            public string BugLine2 { get; set; }
            public string BugLine3 { get; set; }
            public string BugLine4 { get; set; }
            public string BugLine5 { get; set; }
            public string BalanceBL1 { get; set; }
            public string BalanceBL2 { get; set; }
            public string BalanceBL3 { get; set; }
            public string BalanceBL4 { get; set; }
            public string BalanceBL5 { get; set; }
            public string KiannaUN { get; set; }
            public string AveryUN { get; set; }
            public string CrunnieUN { get; set; }
            public string VersionFolderInt { get; set; }
            public string LauncherString { get; set; }
            public string DevLauncherString { get; set; }
            public string CollapseMenu { get; set; }
            public string LauncherVer { get; set; }
            public string NewsHeader { get; set; }
            public string NewsSubHeader { get; set; }
            public string NewsDate { get; set; }
            public string NewsImageURL { get; set; }
        }
        private void Home(object sender, RoutedEventArgs e)
        {
            //home button logic
            try
            {
                if ((bool)!CollapseCB.IsChecked)
                {
                    Uncollapsed.Opacity = 1;
                    Collapsed.Opacity = 0;
                    AGSLogo.Margin = new Thickness(0, 0, 56, 11);
                    HomeButton.Visibility = Visibility.Visible;
                    ChangelogButton.Visibility = Visibility.Visible;
                    BugsButton.Visibility = Visibility.Visible;
                    ColSettings.Visibility = Visibility.Hidden;
                    SettingsButton.Visibility = Visibility.Visible;
                    AveryGame.Opacity = 0;
                    AGSLog.Opacity = 0;
                    ColHomeButton.Visibility = Visibility.Hidden;
                    ColChangelogButton.Visibility = Visibility.Hidden;
                    ColBugsButton.Visibility = Visibility.Hidden;
                    UncolHome.Opacity = 0;
                    UncolChangelog.Opacity = 0;
                    UncolBugs.Opacity = 0;
                    UncolHome.Margin = new Thickness(11, 50, 0, 0);
                    UncolChangelog.Margin = new Thickness(11, 80, 0, 0);
                    UncolBugs.Margin = new Thickness(11, 110, 0, 0);
                    UncolHome.Opacity = 1;
                    UncolChangelog.Opacity = 1;
                    UncolBugs.Opacity = 1;
                    UncolSettingsImg.Opacity = 1;
                    Services.LogSVC.BtnLogic.LogOpacityChange();
                }
                Services.LogSVC.BtnLogic.LogHomeBTNClick();
                string DATA = File.ReadAllText(filepath + "\\AveryGame Launcher\\fuck.json");
                LauncherCloud json = JsonConvert.DeserializeObject<LauncherCloud>(DATA);
                AGSLog.Opacity = 0;
                AveryGame.Opacity = 1;
                HomeButton.IsEnabled = false;
                LaunchButton.IsEnabled = true;
                BugsButton.IsEnabled = true;
                ChangelogButton.IsEnabled = true;
                ColSettings.IsEnabled = true;
                SettingsButton.IsEnabled = true;
                NewsButton.IsEnabled = true;
                LaunchButton.Opacity = 1;
                AGSLogo.Opacity = 0;
                ChangelogBugsTitle.Opacity = 1;
                ChangelogBugsTitle.Text = "Home";
                BugsButton.Content = "Bugs";
                HomeButton.Content = "• Home";
                NewsButton.Content = "News";
                ChangelogButton.Content = "Changelog";
                SettingsButton.Content = "Settings";
                VerSTR.Opacity = 0;
                LaunchButton.Visibility = Visibility.Visible;
                LogLine1.Opacity = 0;
                LogLine2.Opacity = 0;
                LogLine3.Opacity = 0;
                LogLine4.Opacity = 0;
                LogLine5.Opacity = 0;
                ColLogLine1.Opacity = 0;
                ColLogLine2.Opacity = 0;
                ColLogLine3.Opacity = 0;
                ColLogLine4.Opacity = 0;
                ColLogLine5.Opacity = 0;
                HideMenu.Opacity = 0;
                CollapseCB.Opacity = 0;
                Notice.Opacity = 0;
                Arguments.Opacity = 0;
                args.Opacity = 0;
                Notice2.Opacity = 0;
                NewsDate.Opacity = 0;
                NewsHeader.Opacity = 0;
                NewsSubheader.Opacity = 0;
                NewsImageBorder.Opacity = 0;
                Services.LogSVC.BtnLogic.LogOpacityChange();
                if ((bool)(CollapseCB.IsChecked))
                {
                    AveryGame.Opacity = 0;
                    ChangelogBugsTitle.Opacity = 0;
                    ColChangelogBugsTitle.Opacity = 1;
                    ColChangelogBugsTitle.Text = "Home";
                    ColVerSTR.Opacity = 1;
                    VerSTR.Opacity = 0;
                    ColVerSTR.Opacity = 1;
                    ChangelogBugsTitle.Text = "Home";
                    Uncollapsed.Opacity = 0;
                    HomeButton.Visibility = Visibility.Hidden;
                    ChangelogButton.Visibility = Visibility.Hidden;
                    BugsButton.Visibility = Visibility.Hidden;
                    ColSettings.Visibility = Visibility.Visible;
                    SettingsButton.Visibility = Visibility.Hidden;
                    AveryGame.Opacity = 0;
                    AGSLog.Opacity = 1;
                    ColHomeButton.Visibility = Visibility.Visible;
                    ColChangelogButton.Visibility = Visibility.Visible;
                    ColBugsButton.Visibility = Visibility.Visible;
                    ColNews.Visibility = Visibility.Visible;
                    NewsButton.Visibility = Visibility.Hidden;
                    UncolHome.Opacity = 0;
                    UncolChangelog.Opacity = 0;
                    UncolBugs.Opacity = 0;
                    UncolHome.Margin = new Thickness(69, 69, 69, 69);
                    UncolChangelog.Margin = new Thickness(69, 69, 69, 69);
                    UncolBugs.Margin = new Thickness(69, 69, 69, 69);
                    ColHomeButton.IsEnabled = false;
                    ColChangelogButton.IsEnabled = true;
                    ColBugsButton.IsEnabled = true;
                    ColNews.IsEnabled = true;
                    WelcomeTextCol.Opacity = 1;
                    LauncherInfoTextCol.Opacity = 1;
                    LaunchButton.Margin = new Thickness(300, 367, 0, 0);
                    CreditLine1.Opacity = 0;
                    CreditLine2.Opacity = 0;
                    Services.LogSVC.BtnLogic.LogColElements();
                }
                else
                {
                    WelcomeText.Opacity = 1;
                    LauncherInfoText.Opacity = 1;
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(filepath + "\\AveryGame Launcher\\Logs\\LogV2.txt", "Fatal error occurred, logging..." + Environment.NewLine);
                ErrorLogging(ex);
                MessageBox.Show("A fatal error occurred while opening the Avery Game launcher. Please make sure you are connected to the internet and try again. If the problem persists, report the issue and when it is occurring in #bug-reports in the Avery Game discord server.", "Fatal error!", MessageBoxButton.OK, MessageBoxImage.Stop);
                this.Close();
            }
        }
        private void Changelog(object sender, RoutedEventArgs e)
        {
            //changelog button logic
            try
            {
                if ((bool)!CollapseCB.IsChecked)
                {
                    Uncollapsed.Opacity = 1;
                    Collapsed.Opacity = 0;
                    AGSLogo.Margin = new Thickness(0, 0, 56, 11);
                    HomeButton.Visibility = Visibility.Visible;
                    ChangelogButton.Visibility = Visibility.Visible;
                    BugsButton.Visibility = Visibility.Visible;
                    ColSettings.Visibility = Visibility.Hidden;
                    SettingsButton.Visibility = Visibility.Visible;
                    AveryGame.Opacity = 0;
                    AGSLog.Opacity = 0;
                    ColHomeButton.Visibility = Visibility.Hidden;
                    ColChangelogButton.Visibility = Visibility.Hidden;
                    ColBugsButton.Visibility = Visibility.Hidden;
                    UncolHome.Opacity = 0;
                    UncolChangelog.Opacity = 0;
                    UncolBugs.Opacity = 0;
                    UncolHome.Margin = new Thickness(11, 50, 0, 0);
                    UncolChangelog.Margin = new Thickness(11, 80, 0, 0);
                    UncolBugs.Margin = new Thickness(11, 110, 0, 0);
                    UncolHome.Opacity = 1;
                    UncolChangelog.Opacity = 1;
                    UncolBugs.Opacity = 1;
                    UncolSettingsImg.Opacity = 1;
                    Services.LogSVC.BtnLogic.LogUncolElements();
                }
                Services.LogSVC.BtnLogic.LogChangelogBTNClick();
                string DATA = File.ReadAllText(filepath + "\\AveryGame Launcher\\fuck.json");
                LauncherCloud json = JsonConvert.DeserializeObject<LauncherCloud>(DATA);
                Services.LogSVC.LogJSRead();
                AGSLog.Opacity = 0;
                AveryGame.Opacity = 1;
                LogLine1.Text = json.ChangelogLine1;
                LogLine2.Text = json.ChangelogLine2;
                LogLine3.Text = json.ChangelogLine3;
                LogLine4.Text = json.ChangelogLine4;
                LogLine5.Text = json.ChangelogLine5;
                VerSTR.Text = json.Version + " - " + json.LauncherString;
                ColVerSTR.Opacity = 0;
                ColVerSTR.Text = json.Version + " - " + json.LauncherString;
                ChangelogButton.IsEnabled = false;
                HomeButton.IsEnabled = true;
                LaunchButton.IsEnabled = false;
                BugsButton.IsEnabled = true;
                ColSettings.IsEnabled = true;
                SettingsButton.IsEnabled = true;
                NewsButton.IsEnabled = true;
                LaunchButton.Opacity = 0;
                AGSLogo.Opacity = 0;
                ChangelogBugsTitle.Opacity = 1;
                ChangelogBugsTitle.Text = "Changelog";
                BugsButton.Content = "Bugs";
                HomeButton.Content = "Home";
                ChangelogButton.Content = "• Changelog";
                SettingsButton.Content = "Settings";
                NewsButton.Content = "News";
                VerSTR.Opacity = 1;
                WelcomeTextCol.Opacity = 0;
                LauncherInfoTextCol.Opacity = 0;
                WelcomeText.Opacity = 0;
                LauncherInfoText.Opacity = 0;
                LaunchButton.Visibility = Visibility.Hidden;
                LogLine1.Opacity = 1;
                LogLine2.Opacity = 1;
                LogLine3.Opacity = 1;
                LogLine4.Opacity = 1;
                LogLine5.Opacity = 1;
                ColLogLine1.Opacity = 0;
                ColLogLine2.Opacity = 0;
                ColLogLine3.Opacity = 0;
                ColLogLine4.Opacity = 0;
                ColLogLine5.Opacity = 0;
                HideMenu.Opacity = 0;
                CollapseCB.Opacity = 0;
                Notice.Opacity = 0;
                Arguments.Opacity = 0;
                args.Opacity = 0;
                Notice2.Opacity = 0;
                NewsDate.Opacity = 0;
                NewsHeader.Opacity = 0;
                NewsSubheader.Opacity = 0;
                NewsImageBorder.Opacity = 0;
                Services.LogSVC.BtnLogic.LogUncolElements();
                Services.LogSVC.BtnLogic.LogOpacityChange();
                if ((bool)(CollapseCB.IsChecked))
                {
                    AveryGame.Opacity = 0;
                    ChangelogBugsTitle.Opacity = 0;
                    ColChangelogBugsTitle.Opacity = 1;
                    ColChangelogBugsTitle.Text = "Changelog";
                    ColVerSTR.Opacity = 1;
                    VerSTR.Opacity = 0;
                    ColVerSTR.Opacity = 1;
                    LogLine1.Opacity = 0;
                    LogLine2.Opacity = 0;
                    LogLine3.Opacity = 0;
                    LogLine4.Opacity = 0;
                    LogLine5.Opacity = 0;
                    ColLogLine1.Opacity = 1;
                    ColLogLine2.Opacity = 1;
                    ColLogLine3.Opacity = 1;
                    ColLogLine4.Opacity = 1;
                    ColLogLine5.Opacity = 1;
                    VerSTR.Text = json.Version + " - " + json.LauncherString;
                    ColLogLine1.Text = json.ChangelogLine1;
                    ColLogLine2.Text = json.ChangelogLine2;
                    ColLogLine3.Text = json.ChangelogLine3;
                    ColLogLine4.Text = json.ChangelogLine4;
                    ColLogLine5.Text = json.ChangelogLine5;
                    Uncollapsed.Opacity = 0;
                    HomeButton.Visibility = Visibility.Hidden;
                    ChangelogButton.Visibility = Visibility.Hidden;
                    BugsButton.Visibility = Visibility.Hidden;
                    ColSettings.Visibility = Visibility.Visible;
                    SettingsButton.Visibility = Visibility.Hidden;
                    AveryGame.Opacity = 0;
                    AGSLog.Opacity = 1;
                    ColHomeButton.Visibility = Visibility.Visible;
                    ColChangelogButton.Visibility = Visibility.Visible;
                    ColBugsButton.Visibility = Visibility.Visible;
                    ColNews.Visibility = Visibility.Visible;
                    NewsButton.Visibility = Visibility.Hidden;
                    UncolHome.Opacity = 0;
                    UncolChangelog.Opacity = 0;
                    UncolBugs.Opacity = 0;
                    UncolHome.Margin = new Thickness(69, 69, 69, 69);
                    UncolChangelog.Margin = new Thickness(69, 69, 69, 69);
                    UncolBugs.Margin = new Thickness(69, 69, 69, 69);
                    ColHomeButton.IsEnabled = true;
                    ColChangelogButton.IsEnabled = false;
                    ColBugsButton.IsEnabled = true;
                    ColNews.IsEnabled = true;
                    ChangelogBugsTitle.Text = "Changelog";
                    CreditLine1.Opacity = 0;
                    CreditLine2.Opacity = 0;
                    Services.LogSVC.BtnLogic.LogColElements();
                }
            }
            catch (Exception ex)
            {
                Services.LogSVC.LogFatalErr();
                ErrorLogging(ex);
                MessageBox.Show("A fatal error occurred while navigating to the changelog tab in the Avery Game launcher. Please make sure you are connected to the internet and try again. If the problem persists, report the issue and when it is occurring in #bug-reports in the Avery Game discord server.", "Fatal error!", MessageBoxButton.OK, MessageBoxImage.Stop);
                this.Close();
            }
        }
        private void DragBar(object sender, MouseButtonEventArgs e)
        {
            //dragbar logging + logic
            Services.LogSVC.LogDrag();
            this.DragMove();
        }
        private void Bugs(object sender, RoutedEventArgs e)
        {
            //bug button logic
            try
            {
                Services.LogSVC.BtnLogic.LogBugBTNClick();
                if ((bool)!CollapseCB.IsChecked)
                {
                    Uncollapsed.Opacity = 1;
                    Collapsed.Opacity = 0;
                    AGSLogo.Margin = new Thickness(0, 0, 56, 11);
                    HomeButton.Visibility = Visibility.Visible;
                    ChangelogButton.Visibility = Visibility.Visible;
                    BugsButton.Visibility = Visibility.Visible;
                    ColSettings.Visibility = Visibility.Hidden;
                    SettingsButton.Visibility = Visibility.Visible;
                    AveryGame.Opacity = 0;
                    AGSLog.Opacity = 0;
                    ColHomeButton.Visibility = Visibility.Hidden;
                    ColChangelogButton.Visibility = Visibility.Hidden;
                    ColBugsButton.Visibility = Visibility.Hidden;
                    UncolHome.Opacity = 0;
                    UncolChangelog.Opacity = 0;
                    UncolBugs.Opacity = 0;
                    UncolHome.Margin = new Thickness(11, 50, 0, 0);
                    UncolChangelog.Margin = new Thickness(11, 80, 0, 0);
                    UncolBugs.Margin = new Thickness(11, 110, 0, 0);
                    UncolHome.Opacity = 1;
                    UncolChangelog.Opacity = 1;
                    UncolBugs.Opacity = 1;
                    UncolSettingsImg.Opacity = 1;
                    Services.LogSVC.BtnLogic.LogUncolElements();
                }
                string DATA = File.ReadAllText(filepath + "\\AveryGame Launcher\\fuck.json");
                LauncherCloud json = JsonConvert.DeserializeObject<LauncherCloud>(DATA);
                Services.LogSVC.LogJSRead();
                AGSLog.Opacity = 0;
                AveryGame.Opacity = 1;
                LogLine1.Text = json.BugLine1;
                LogLine2.Text = json.BugLine2;
                LogLine3.Text = json.BugLine3;
                LogLine4.Text = json.BugLine4;
                LogLine5.Text = json.BugLine5;
                VerSTR.Text = json.Version + " - " + json.LauncherString;
                ColVerSTR.Opacity = 0;
                ColVerSTR.Text = json.Version + " - " + json.LauncherString;
                ChangelogBugsTitle.Text = "Bugs";
                ColChangelogBugsTitle.Text = "Bugs";
                ChangelogButton.IsEnabled = true;
                ColChangelogBugsTitle.Opacity = 0;
                HomeButton.IsEnabled = true;
                LaunchButton.IsEnabled = false;
                BugsButton.IsEnabled = false;
                ColSettings.IsEnabled = true;
                SettingsButton.IsEnabled = true;
                NewsButton.IsEnabled = true;
                LaunchButton.Opacity = 0;
                AGSLogo.Opacity = 0;
                ChangelogBugsTitle.Opacity = 1;
                ChangelogBugsTitle.Text = "Bugs";
                BugsButton.Content = "• Bugs";
                HomeButton.Content = "Home";
                ChangelogButton.Content = "Changelog";
                SettingsButton.Content = "Settings";
                NewsButton.Content = "News";
                VerSTR.Opacity = 1;
                WelcomeTextCol.Opacity = 0;
                LauncherInfoTextCol.Opacity = 0;
                WelcomeText.Opacity = 0;
                LauncherInfoText.Opacity = 0;
                LaunchButton.Visibility = Visibility.Hidden;
                LogLine1.Opacity = 1;
                LogLine2.Opacity = 1;
                LogLine3.Opacity = 1;
                LogLine4.Opacity = 1;
                LogLine5.Opacity = 1;
                ColLogLine1.Opacity = 0;
                ColLogLine2.Opacity = 0;
                ColLogLine3.Opacity = 0;
                ColLogLine4.Opacity = 0;
                ColLogLine5.Opacity = 0;
                HideMenu.Opacity = 0;
                CollapseCB.Opacity = 0;
                Notice.Opacity = 0;
                Arguments.Opacity = 0;
                args.Opacity = 0;
                Notice2.Opacity = 0;
                NewsDate.Opacity = 0;
                NewsHeader.Opacity = 0;
                NewsSubheader.Opacity = 0;
                NewsImageBorder.Opacity = 0;
                Services.LogSVC.BtnLogic.LogUncolElements();
                if ((bool)(CollapseCB.IsChecked))
                {
                    AveryGame.Opacity = 0;
                    ChangelogBugsTitle.Opacity = 0;
                    ColChangelogBugsTitle.Opacity = 1;
                    ColChangelogBugsTitle.Text = "Bugs";
                    ChangelogBugsTitle.Text = "Bugs";
                    VerSTR.Opacity = 0;
                    ColVerSTR.Opacity = 1;
                    LogLine1.Opacity = 0;
                    LogLine2.Opacity = 0;
                    LogLine3.Opacity = 0;
                    LogLine4.Opacity = 0;
                    LogLine5.Opacity = 0;
                    ColLogLine1.Opacity = 1;
                    ColLogLine2.Opacity = 1;
                    ColLogLine3.Opacity = 1;
                    ColLogLine4.Opacity = 1;
                    ColLogLine5.Opacity = 1;
                    ColVerSTR.Text = json.Version + " - " + json.LauncherString;
                    VerSTR.Opacity = 0;
                    ColLogLine1.Text = json.BugLine1;
                    ColLogLine2.Text = json.BugLine2;
                    ColLogLine3.Text = json.BugLine3;
                    ColLogLine4.Text = json.BugLine4;
                    ColLogLine5.Text = json.BugLine5;
                    Uncollapsed.Opacity = 0;
                    HomeButton.Visibility = Visibility.Hidden;
                    ChangelogButton.Visibility = Visibility.Hidden;
                    BugsButton.Visibility = Visibility.Hidden;
                    ColSettings.Visibility = Visibility.Visible;
                    SettingsButton.Visibility = Visibility.Hidden;
                    ColNews.Visibility = Visibility.Visible;
                    NewsButton.Visibility = Visibility.Hidden;
                    AveryGame.Opacity = 0;
                    AGSLog.Opacity = 1;
                    ColHomeButton.Visibility = Visibility.Visible;
                    ColChangelogButton.Visibility = Visibility.Visible;
                    ColBugsButton.Visibility = Visibility.Visible;
                    ColChangelogBugsTitle.Text = "Bugs";
                    UncolHome.Opacity = 0;
                    UncolChangelog.Opacity = 0;
                    UncolBugs.Opacity = 0;
                    UncolHome.Margin = new Thickness(69, 69, 69, 69);
                    UncolChangelog.Margin = new Thickness(69, 69, 69, 69);
                    UncolBugs.Margin = new Thickness(69, 69, 69, 69);
                    ColHomeButton.IsEnabled = true;
                    ColChangelogButton.IsEnabled = true;
                    ColBugsButton.IsEnabled = false;
                    ColNews.IsEnabled = true;
                    ChangelogBugsTitle.Text = "Bugs";
                    CreditLine1.Opacity = 0;
                    CreditLine2.Opacity = 0;
                    Services.LogSVC.BtnLogic.LogColElements();
                }
            }
            catch (Exception ex)
            {
                Services.LogSVC.LogFatalErr();
                ErrorLogging(ex);
                MessageBox.Show("A fatal error occurred while navigating to the bugs tab in the Avery Game launcher. Please make sure you are connected to the internet and try again. If the problem persists, report the issue and when it is occurring in #bug-reports in the Avery Game discord server.", "Fatal error!", MessageBoxButton.OK, MessageBoxImage.Stop);
                this.Close();
            }
        }
        private void News(object sender, RoutedEventArgs e)
        {
            //news button logic
            try
            {
                Services.LogSVC.BtnLogic.LogBugBTNClick();
                if ((bool)!CollapseCB.IsChecked)
                {
                    Uncollapsed.Opacity = 1;
                    Collapsed.Opacity = 0;
                    AGSLogo.Margin = new Thickness(0, 0, 56, 11);
                    HomeButton.Visibility = Visibility.Visible;
                    ChangelogButton.Visibility = Visibility.Visible;
                    BugsButton.Visibility = Visibility.Visible;
                    ColSettings.Visibility = Visibility.Hidden;
                    SettingsButton.Visibility = Visibility.Visible;
                    ColNews.Visibility = Visibility.Hidden;
                    NewsButton.Visibility = Visibility.Visible;
                    AveryGame.Opacity = 0;
                    AGSLog.Opacity = 0;
                    ColHomeButton.Visibility = Visibility.Hidden;
                    ColChangelogButton.Visibility = Visibility.Hidden;
                    ColBugsButton.Visibility = Visibility.Hidden;
                    UncolHome.Opacity = 0;
                    UncolChangelog.Opacity = 0;
                    UncolBugs.Opacity = 0;
                    UncolHome.Margin = new Thickness(11, 50, 0, 0);
                    UncolChangelog.Margin = new Thickness(11, 80, 0, 0);
                    UncolBugs.Margin = new Thickness(11, 110, 0, 0);
                    NewsImageBorder.Margin = new Thickness(169, 26, 19, 193);
                    NewsHeader.Margin = new Thickness(169, 262, 19, 156);
                    NewsSubheader.Margin = new Thickness(169, 294, 19, 113);
                    NewsDate.Margin = new Thickness(153, 263, 26, 161);
                    UncolHome.Opacity = 1;
                    UncolChangelog.Opacity = 1;
                    UncolBugs.Opacity = 1;
                    UncolSettingsImg.Opacity = 1;
                    Services.LogSVC.BtnLogic.LogUncolElements();
                }
                string DATA = File.ReadAllText(filepath + "\\AveryGame Launcher\\fuck.json");
                LauncherCloud json = JsonConvert.DeserializeObject<LauncherCloud>(DATA);
                Services.LogSVC.LogJSRead();
                AGSLog.Opacity = 0;
                AveryGame.Opacity = 1;
                ColVerSTR.Opacity = 0;
                ChangelogBugsTitle.Text = "News";
                ColChangelogBugsTitle.Text = "News";
                ChangelogButton.IsEnabled = true;
                ColChangelogBugsTitle.Opacity = 0;
                HomeButton.IsEnabled = true;
                LaunchButton.IsEnabled = false;
                BugsButton.IsEnabled = true;
                NewsButton.IsEnabled = false;
                ColSettings.IsEnabled = true;
                SettingsButton.IsEnabled = true;
                LaunchButton.Opacity = 0;
                AGSLogo.Opacity = 0;
                ChangelogBugsTitle.Opacity = 0;
                BugsButton.Content = "Bugs";
                HomeButton.Content = "Home";
                ChangelogButton.Content = "Changelog";
                NewsButton.Content = "• News";
                SettingsButton.Content = "Settings";
                VerSTR.Opacity = 0;
                WelcomeTextCol.Opacity = 0;
                LauncherInfoTextCol.Opacity = 0;
                WelcomeText.Opacity = 0;
                LauncherInfoText.Opacity = 0;
                LaunchButton.Visibility = Visibility.Hidden;
                LogLine1.Opacity = 0;
                LogLine2.Opacity = 0;
                LogLine3.Opacity = 0;
                LogLine4.Opacity = 0;
                LogLine5.Opacity = 0;
                ColLogLine1.Opacity = 0;
                ColLogLine2.Opacity = 0;
                ColLogLine3.Opacity = 0;
                ColLogLine4.Opacity = 0;
                ColLogLine5.Opacity = 0;
                HideMenu.Opacity = 0;
                CollapseCB.Opacity = 0;
                Notice.Opacity = 0;
                Arguments.Opacity = 0;
                args.Opacity = 0;
                Notice2.Opacity = 0;
                NewsImageBorder.Opacity = 0.76;
                NewsHeader.Opacity = 1;
                NewsSubheader.Opacity = 1;
                NewsDate.Opacity = 0.4;
                ImageBrush imgB = new ImageBrush();
                BitmapImage btpImg = new BitmapImage();
                btpImg.BeginInit();
                btpImg.UriSource = new Uri(json.NewsImageURL);
                btpImg.EndInit();
                imgB.ImageSource = btpImg;
                NewsDate.Text = json.NewsDate;
                NewsHeader.Text = json.NewsHeader;
                NewsImageBorder.Background = imgB;
                NewsSubheader.Text = json.NewsSubHeader;
                Services.LogSVC.BtnLogic.LogUncolElements();
                if ((bool)(CollapseCB.IsChecked))
                {
                    AveryGame.Opacity = 0;
                    ChangelogBugsTitle.Opacity = 0;
                    ColChangelogBugsTitle.Opacity = 0;
                    ColChangelogBugsTitle.Text = "News";
                    ChangelogBugsTitle.Text = "News";
                    VerSTR.Opacity = 0;
                    ColVerSTR.Opacity = 0;
                    LogLine1.Opacity = 0;
                    LogLine2.Opacity = 0;
                    LogLine3.Opacity = 0;
                    LogLine4.Opacity = 0;
                    LogLine5.Opacity = 0;
                    ColLogLine1.Opacity = 0;
                    ColLogLine2.Opacity = 0;
                    ColLogLine3.Opacity = 0;
                    ColLogLine4.Opacity = 0;
                    ColLogLine5.Opacity = 0;
                    ColVerSTR.Text = json.Version + " - " + json.LauncherString;
                    VerSTR.Opacity = 0;
                    ColLogLine1.Text = json.BugLine1;
                    ColLogLine2.Text = json.BugLine2;
                    ColLogLine3.Text = json.BugLine3;
                    ColLogLine4.Text = json.BugLine4;
                    ColLogLine5.Text = json.BugLine5;
                    Uncollapsed.Opacity = 0;
                    HomeButton.Visibility = Visibility.Hidden;
                    ChangelogButton.Visibility = Visibility.Hidden;
                    BugsButton.Visibility = Visibility.Hidden;
                    ColSettings.Visibility = Visibility.Visible;
                    ColNews.Visibility = Visibility.Visible;
                    NewsButton.Visibility = Visibility.Hidden;
                    SettingsButton.Visibility = Visibility.Hidden;
                    AveryGame.Opacity = 0;
                    AGSLog.Opacity = 1;
                    ColHomeButton.Visibility = Visibility.Visible;
                    ColChangelogButton.Visibility = Visibility.Visible;
                    ColBugsButton.Visibility = Visibility.Visible;
                    ColChangelogBugsTitle.Text = "News";
                    UncolHome.Opacity = 0;
                    UncolChangelog.Opacity = 0;
                    UncolBugs.Opacity = 0;
                    UncolHome.Margin = new Thickness(69, 69, 69, 69);
                    UncolChangelog.Margin = new Thickness(69, 69, 69, 69);
                    UncolBugs.Margin = new Thickness(69, 69, 69, 69);
                    ColHomeButton.IsEnabled = true;
                    ColChangelogButton.IsEnabled = true;
                    ColBugsButton.IsEnabled = true;
                    ColNews.IsEnabled = false;
                    ChangelogBugsTitle.Text = "News";
                    CreditLine1.Opacity = 0;
                    CreditLine2.Opacity = 0;
                    Services.LogSVC.BtnLogic.LogColElements();
                }
            }
            catch (Exception ex)
            {
                Services.LogSVC.LogFatalErr();
                ErrorLogging(ex);
                MessageBox.Show("A fatal error occurred while navigating to the bugs tab in the Avery Game launcher. Please make sure you are connected to the internet and try again. If the problem persists, report the issue and when it is occurring in #bug-reports in the Avery Game discord server.", "Fatal error!", MessageBoxButton.OK, MessageBoxImage.Stop);
                this.Close();
            }
        }
        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            //setting button logic
            try
            {
                Services.LogSVC.BtnLogic.LogSettingBTNClick();
                if ((bool)!CollapseCB.IsChecked)
                {
                    Uncollapsed.Opacity = 1;
                    Collapsed.Opacity = 0;
                    AGSLogo.Margin = new Thickness(0, 0, 56, 11);
                    HomeButton.Visibility = Visibility.Visible;
                    ChangelogButton.Visibility = Visibility.Visible;
                    BugsButton.Visibility = Visibility.Visible;
                    ColSettings.Visibility = Visibility.Hidden;
                    SettingsButton.Visibility = Visibility.Visible;
                    AveryGame.Opacity = 0;
                    AGSLog.Opacity = 0;
                    ColHomeButton.Visibility = Visibility.Hidden;
                    ColChangelogButton.Visibility = Visibility.Hidden;
                    ColBugsButton.Visibility = Visibility.Hidden;
                    UncolHome.Opacity = 0;
                    UncolChangelog.Opacity = 0;
                    UncolBugs.Opacity = 0;
                    UncolHome.Margin = new Thickness(11, 50, 0, 0);
                    UncolChangelog.Margin = new Thickness(11, 80, 0, 0);
                    UncolBugs.Margin = new Thickness(11, 110, 0, 0);
                    UncolHome.Opacity = 1;
                    UncolChangelog.Opacity = 1;
                    UncolBugs.Opacity = 1;
                    UncolSettingsImg.Opacity = 1;
                    Services.LogSVC.BtnLogic.LogUncolElements();
                }
                //why in gods name do i deserialize json in every fucking button
                //im too lazy to change it but whyyyyy
                string DATA = File.ReadAllText(filepath + "\\AveryGame Launcher\\fuck.json");
                LauncherCloud json = JsonConvert.DeserializeObject<LauncherCloud>(DATA);
                Services.LogSVC.LogJSRead();
                VerSTR.Text = "" + json.Version + " - " + json.LauncherString;
                AGSLog.Opacity = 0;
                AveryGame.Opacity = 1;
                ColVerSTR.Opacity = 0;
                ChangelogButton.IsEnabled = true;
                ColChangelogBugsTitle.Opacity = 0;
                ChangelogBugsTitle.Opacity = 0;
                HomeButton.IsEnabled = true;
                LaunchButton.IsEnabled = false;
                BugsButton.IsEnabled = true;
                SettingsButton.IsEnabled = false;
                NewsButton.IsEnabled = true;
                ColSettings.IsEnabled = false;
                LaunchButton.Opacity = 0;
                AGSLogo.Opacity = 0;
                ChangelogBugsTitle.Opacity = 0;
                ChangelogBugsTitle.Text = "";
                BugsButton.Content = "Bugs";
                HomeButton.Content = "Home";
                ChangelogButton.Content = "Changelog";
                NewsButton.Content = "News";
                SettingsButton.Content = "• Settings";
                VerSTR.Opacity = 0;
                WelcomeTextCol.Opacity = 0;
                LauncherInfoTextCol.Opacity = 0;
                WelcomeText.Opacity = 0;
                LauncherInfoText.Opacity = 0;
                LaunchButton.Visibility = Visibility.Hidden;
                LogLine1.Opacity = 0;
                LogLine2.Opacity = 0;
                LogLine3.Opacity = 0;
                LogLine4.Opacity = 0;
                LogLine5.Opacity = 0;
                ColLogLine1.Opacity = 0;
                ColLogLine2.Opacity = 0;
                ColLogLine3.Opacity = 0;
                ColLogLine4.Opacity = 0;
                ColLogLine5.Opacity = 0;
                HideMenu.Opacity = 1;
                CollapseCB.Opacity = 1;
                Notice.Opacity = 1;
                Arguments.Opacity = 1;
                args.Opacity = 1;
                Notice2.Opacity = 1;
                NewsDate.Opacity = 0;
                NewsHeader.Opacity = 0;
                NewsSubheader.Opacity = 0;
                NewsImageBorder.Opacity = 0;
                Services.LogSVC.BtnLogic.LogOpacityChange();
                if ((bool)(CollapseCB.IsChecked))
                {
                    VerSTR.Opacity = 0;
                    ColVerSTR.Opacity = 0;
                    LogLine1.Opacity = 0;
                    LogLine2.Opacity = 0;
                    LogLine3.Opacity = 0;
                    LogLine4.Opacity = 0;
                    LogLine5.Opacity = 0;
                    ColLogLine1.Opacity = 0;
                    ColLogLine2.Opacity = 0;
                    ColLogLine3.Opacity = 0;
                    ColLogLine4.Opacity = 0;
                    ColLogLine5.Opacity = 0;
                    Uncollapsed.Opacity = 0;
                    HomeButton.Visibility = Visibility.Hidden;
                    ChangelogButton.Visibility = Visibility.Hidden;
                    BugsButton.Visibility = Visibility.Hidden;
                    ColSettings.Visibility = Visibility.Visible;
                    SettingsButton.Visibility = Visibility.Hidden;
                    ColNews.Visibility = Visibility.Visible;
                    NewsButton.Visibility = Visibility.Hidden;
                    AveryGame.Opacity = 0;
                    AGSLog.Opacity = 1;
                    ColHomeButton.Visibility = Visibility.Visible;
                    ColChangelogButton.Visibility = Visibility.Visible;
                    ColBugsButton.Visibility = Visibility.Visible;
                    UncolHome.Opacity = 0;
                    UncolChangelog.Opacity = 0;
                    UncolBugs.Opacity = 0;
                    UncolHome.Margin = new Thickness(69, 69, 69, 69);
                    UncolChangelog.Margin = new Thickness(69, 69, 69, 69);
                    UncolBugs.Margin = new Thickness(69, 69, 69, 69);
                    ColHomeButton.IsEnabled = true;
                    ColChangelogButton.IsEnabled = true;
                    ColBugsButton.IsEnabled = true;
                    ColNews.IsEnabled = true;
                    SettingsButton.IsEnabled = false;
                    ChangelogBugsTitle.Text = "Bugs";
                    CreditLine1.Opacity = 0;
                    CreditLine2.Opacity = 0;
                    Services.LogSVC.BtnLogic.LogColElements();
                }
            }
            catch (Exception ex)
            {
                Services.LogSVC.LogFatalErr();
                ErrorLogging(ex);
                MessageBox.Show("A fatal error occurred while navigating to the settings tab in the Avery Game launcher. Please make sure you are connected to the internet and try again. If the problem persists, report the issue and when it is occurring in #bug-reports in the Avery Game discord server.", "Fatal error!", MessageBoxButton.OK, MessageBoxImage.Stop);
                this.Close();

            }
        }
        private void RpcNameCopy(object sender, RoutedEventArgs e)
        {
            //IGNORE
            WelcomeRPCLabel.Content = "astolfo hentai not done yet ";
            Services.LogSVC.TitaniumSVC.what();
        }
        private void Minimize(object sender, RoutedEventArgs e)
        {
            //minimize button logic
            this.WindowState = WindowState.Minimized;
            Services.LogSVC.BtnLogic.LogMinimizeBTNClick();
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            //close button logic
            Services.LogSVC.BtnLogic.LogCloseBTNClick();
            //check if game is downloading
            if (dprog.Opacity == 1)
            {
                MessageBoxResult result = MessageBox.Show("AveryGame is not done downloading, and exiting will corrupt the download. Are you sure?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                Services.LogSVC.BtnLogic.CloseBTNEvents.LogCloseInterrupt();
                if (result == MessageBoxResult.Yes)
                {
                    //user forced to close the launcher
                    Services.LogSVC.BtnLogic.CloseBTNEvents.LogUserConfirm();
                    Services.LogSVC.LogLauncherShutdown();
                    this.Close();
                }
                else
                {
                    //user did not force close
                    Services.LogSVC.BtnLogic.CloseBTNEvents.LogUserDeny();
                }
            }
            else
            {
                //delete launcher strings and log all events
                File.Delete(filepath + "\\AveryGame Launcher\\" + @"fuck.json");
                Services.LogSVC.LogJSDelete();
                File.WriteAllText(@filepath + "\\AveryGame Launcher\\Clientsettings.json", "{ \"CollapseMenu\":\"" + CollapseCB.IsChecked + "\" }");
                Services.LogSVC.LogCMenuState();
                Services.LogSVC.LogLauncherShutdown();
                this.Close();
            }
        }

        private void testbutton_Click(object sender, RoutedEventArgs e)
        {
            //play button logic
            try
            {
                Services.LogSVC.BtnLogic.PlayBTNEvents.LogBTNClick();
                if (dprog.Opacity == 0)
                {
                    WebClient webclient = new WebClient();
                    string DATA = webclient.DownloadString("https://raw.githubusercontent.com/imstillamazedbyit/1q29dks43895r794/main/launcherinfo.json");
                    LauncherCloud json = JsonConvert.DeserializeObject<LauncherCloud>(DATA);
                    if (!File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\AveryGame\\4.2\\4.2\\WindowsNoEditor\\AveryGame.exe")))
                    {
                        MessageBoxResult result;
                        result = MessageBox.Show("Avery Game was not found! Would you like to install?", "Error", MessageBoxButton.YesNo, MessageBoxImage.Error);
                        Services.LogSVC.BtnLogic.PlayBTNEvents.LogGameNonexistent();
                        if (result == MessageBoxResult.Yes)
                        {
                            Services.LogSVC.BtnLogic.PlayBTNEvents.LogUserConfirm();
                            LaunchButton.Content = "Downloading AGS...";
                            Services.LogSVC.BtnLogic.PlayBTNEvents.LogButtonContentChange();
                            Services.LogSVC.BtnLogic.PlayBTNEvents.LogDownloadWorker();
                            kys();
                        }
                        else
                        {
                            Services.LogSVC.BtnLogic.PlayBTNEvents.LogUserDeny();
                            MessageBox.Show("Avery Game was not found on your system.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        string latest = File.ReadAllText(filepath + "\\AveryGame Launcher\\" + @"gclient.json");
                        if (latest.Contains(json.VersionInt))
                        {
                            try
                            {
                                Services.LogSVC.BtnLogic.PlayBTNEvents.LogGameStart();
                                var p = new System.Diagnostics.Process();
                                p.StartInfo.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\AveryGame\\4.2\\4.2\\WindowsNoEditor\\AveryGame.exe");
                                p.StartInfo.Arguments = args.Text;
                                p.Start();
                                new ToastContentBuilder()
                                    .AddArgument("action", "viewConversation")
                                    .AddArgument("conversationId", 9813)
                                    .AddText("Avery Game")
                                    .AddText("Game launched")
                                .Show();
                                Services.LogSVC.LogToastNotif();
                            }
                            catch (Exception ex)
                            {
                                Services.LogSVC.LogFatalErr();
                                ErrorLogging(ex);
                                MessageBox.Show("A fatal error occurred while launching AveryGame!", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        if (!latest.Contains(json.VersionInt))
                        {
                            MessageBoxResult updateAvail = MessageBox.Show("AveryGame has an update. Would you like to install it now?", "Notice", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            Services.LogSVC.BtnLogic.PlayBTNEvents.Update.LogUpdateNotice();
                            if (updateAvail == MessageBoxResult.Yes)
                            {
                                Services.LogSVC.BtnLogic.PlayBTNEvents.Update.LogUserConfirm();
                                LaunchButton.Content = "Downloading AGS...";
                                kys();
                            }
                            if (updateAvail == MessageBoxResult.No)
                            {
                                Services.LogSVC.BtnLogic.PlayBTNEvents.Update.LogUserDeny();
                                var p = new System.Diagnostics.Process();
                                p.StartInfo.FileName = Path.Combine(json.VersionInt + "/4.2/WindowsNoEditor/AveryGame.exe");
                                p.StartInfo.Arguments = args.Text;
                                p.Start();
                                Services.LogSVC.BtnLogic.PlayBTNEvents.LogGameStart();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Services.LogSVC.LogFatalErr();
                ErrorLogging(ex);
                MessageBox.Show("A fatal error occurred while opening the Avery Game launcher. Please make sure you are connected to the internet and try again. If the problem persists, report the issue and when it is occurring in #bug-reports in the Avery Game discord server.", "Fatal error!", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
        private void DownloadGameCompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            try
            {
                //game completed callback
                Services.LogSVC.BtnLogic.PlayBTNEvents.Download.DownloadCallbackEvents.LogDownloadComplete();
                WebClient webclient = new WebClient();
                string DATA = webclient.DownloadString("https://raw.githubusercontent.com/imstillamazedbyit/1q29dks43895r794/main/launcherinfo.json");
                LauncherCloud json = JsonConvert.DeserializeObject<LauncherCloud>(DATA);
                string gzip = "1i9qQNqWOlQcdrZ0qD3NU7WzHKW4h54U.zip";
                //extracting game
                ZipFile.ExtractToDirectory(gzip, Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\AveryGame\\" + json.VersionInt);
                Services.LogSVC.BtnLogic.PlayBTNEvents.Download.DownloadCallbackEvents.LogExtract();
                //delete useless folder
                File.Delete(gzip);
                Services.LogSVC.BtnLogic.PlayBTNEvents.Download.DownloadCallbackEvents.LogZipDelete();
                dprog.Opacity = 0;
                LaunchButton.Content = "Launch Avery Game";
                Services.LogSVC.BtnLogic.PlayBTNEvents.Download.DownloadCallbackEvents.LogDownloadCleanup();
                //wrote game version to gclient
                File.WriteAllText(filepath + "\\AveryGame Launcher\\" + @"gclient.json", json.VersionInt);
                Services.LogSVC.BtnLogic.PlayBTNEvents.Download.DownloadCallbackEvents.LogWriteVersionString();
                new ToastContentBuilder()
                    .AddArgument("action", "viewConversation")
                    .AddArgument("conversationId", 9813)
                    .AddText("Avery Game downloading status")
                    .AddText("Avery Game has finished downloading!")
                    .Show();
                DownloadProgPercent.Opacity = 0;
                Services.LogSVC.LogToastNotif();
            }
            catch (Exception ex)
            {
                Services.LogSVC.LogFatalErr();
                ErrorLogging(ex);
                MessageBox.Show($"Error finishing download: {ex}");
            }
        }
        private void kys()
        {
            try
            {
                //download code logic
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
                Services.LogSVC.LogToastNotif();
                dprog.Opacity = 1;
                webclient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadGameCompletedCallback);
                webclient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webclientDownloadProgressChanged);
                DownloadProgPercent.Opacity = 1;
                Services.LogSVC.BtnLogic.PlayBTNEvents.Download.LogDownloadStart();
                webclient.DownloadFileAsync(new Uri("https://www.googleapis.com/drive/v3/files/1rhEMfMl1Z56nngeb7Hou9brCDZ2h0Wzu?alt=media&key=AIzaSyD3hsuSxEFnxZkgadbUSPt_iyx8qJ4lwWQ"), gzip);
            }
            catch (Exception ex)
            {
                Services.LogSVC.LogFatalErr();
                ErrorLogging(ex);
                MessageBox.Show($"Error finishing download: {ex}");
            }
        }
        public void webclientDownloadProgressChanged(Object sender, DownloadProgressChangedEventArgs e)
        {
            //progress info
            DownloadProgPercent.Text = e.ProgressPercentage.ToString() + "%";
            dprog.Value = e.ProgressPercentage;
        }
    }
}