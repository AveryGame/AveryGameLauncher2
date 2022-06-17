using System;
using System.IO;
using DiscordRPC;
using System.Net;
using System.Windows;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using DiscordRPC.Logging;
using DiscordRPC.Message;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Input;
using System.IO.Compression;
using System.Windows.Media.Imaging;
using System.Collections.Specialized;
using Microsoft.Toolkit.Uwp.Notifications;

//Pragma warning disabling
#pragma warning disable IDE0079 //1984
#pragma warning disable IDE1006 //FUCK YOUR NAMING VIOLATION BITCH
#pragma warning disable IDE0017 //I simply do not care.
#pragma warning disable IDE0066 //mald
#pragma warning disable IDE0063 //using my fucking nuts
#pragma warning disable CS0252 //cast some bitches

namespace AgsLauncherV2
{
    public partial class MainWindow : Window
    {
        public DiscordRpcClient client;
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                Services.LogSVC.CreateLogFile();
                Services.LogSVC.LogJSEvent();
                WebClient webclient = new WebClient();
                //public strings: https://raw.githubusercontent.com/AyeItsAxi/ags-launcher-strings/main/launcherinfo.json
                //tester strings: https://raw.githubusercontent.com/AyeItsAxi/ags-launcher-strings/dev/launcherinfo.json
                //bdlock strings: https://raw.githubusercontent.com/AyeItsAxi/ags-launcher-strings/kamo/launcherinfo.json
                //dvlper strings: https://raw.githubusercontent.com/AyeItsAxi/ags-launcher-strings/void/launcherinfo.json
                //kschec strings: https://raw.githubusercontent.com/AyeItsAxi/ags-launcher-strings/killswitches/launcherinfo.json
                webclient.DownloadFile("https://raw.githubusercontent.com/AyeItsAxi/ags-launcher-strings/killswitches/launcherinfo.json", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\KSCheck.json");
                string KSJson = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\KSCheck.json");
                Services.AGCloud KS = JsonConvert.DeserializeObject<Services.AGCloud>(KSJson);
                if (KS.bIs2724KillSwitched.ToString().ToLower().Equals("true"))
                {
                    mald();
                }
                else if (!KS.bIs2724KillSwitched.ToString().ToLower().Equals("true"))
                {
                    Continue();
                }
            }
            //if something causes the launcher to crash it will log the error and show the user a messagebox
            catch (Exception ex)
            {
                Services.LogSVC.LogFatalErr();
                ErrorLogging(ex);
                MessageBox.Show(ex.Message, "Fatal error!", MessageBoxButton.OK, MessageBoxImage.Stop);
                this.Close();
            }
        }
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
        }

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
        private void Home(object sender, RoutedEventArgs e)
        {
            //home button logic
            try
            {
                string DATA = File.ReadAllText(filepath + "\\AveryGame Launcher\\fuck.json");
                Services.AGCloud AGLCloud = JsonConvert.DeserializeObject<Services.AGCloud>(DATA);
                Services.LogSVC.LogJSRead();
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
                    ColAccount.Visibility = Visibility.Hidden;
                    AccountButton.Visibility = Visibility.Visible;
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
                    LaunchButton.Margin = new Thickness(356, 371, 0, 0);
                    UncolHome.Opacity = 1;
                    UncolChangelog.Opacity = 1;
                    UncolBugs.Opacity = 1;
                    UncolSettingsImg.Opacity = 1;
                    if (AGLCloud.AccountPageReady.ToLower() == "false")
                    {
                        Services.LogSVC.BtnLogic.LogAccountPageNotReady();
                        AccountButton.Visibility = Visibility.Hidden;
                        ColAccount.Visibility = Visibility.Hidden;
                        AccountIcon.Visibility = Visibility.Hidden;
                        SettingsButton.Margin = new Thickness(6, 170, 0, 0);
                        ColSettings.Margin = new Thickness(10, 170, 0, 0);
                        UncolSettingsImg.Margin = new Thickness(10, 170, 0, 0);
                    }
                    Services.LogSVC.BtnLogic.LogOpacityChange();
                }
                Services.LogSVC.BtnLogic.LogHomeBTNClick();
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
                NewsDate.Opacity = 0;
                NewsHeader.Opacity = 0;
                NewsSubheader.Opacity = 0;
                NewsImageBorder.Opacity = 0;
                Services.LogSVC.BtnLogic.LogOpacityChange();
                if ((bool)CollapseCB.IsChecked)
                {
                    AveryGame.Opacity = 0;
                    ChangelogBugsTitle.Opacity = 0;
                    Collapsed.Opacity = 1;
                    ColChangelogBugsTitle.Opacity = 1;
                    ColChangelogBugsTitle.Text = "Home";
                    ColVerSTR.Opacity = 0;
                    VerSTR.Opacity = 0;
                    ColVerSTR.Opacity = 0;
                    ChangelogBugsTitle.Text = "Home";
                    Uncollapsed.Opacity = 0;
                    HomeButton.Visibility = Visibility.Hidden;
                    ChangelogButton.Visibility = Visibility.Hidden;
                    BugsButton.Visibility = Visibility.Hidden;
                    ColSettings.Visibility = Visibility.Visible;
                    SettingsButton.Visibility = Visibility.Hidden;
                    AccountButton.Visibility = Visibility.Hidden;
                    ColAccount.Visibility = Visibility.Visible;
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
                    NewsImageBorder.Margin = new Thickness(109, 26, 79, 193);
                    NewsHeader.Margin = new Thickness(109, 262, 79, 156);
                    NewsSubheader.Margin = new Thickness(109, 294, 79, 113);
                    NewsDate.Margin = new Thickness(100, 269, 79, 0);
                    ColHomeButton.IsEnabled = false;
                    ColChangelogButton.IsEnabled = true;
                    ColBugsButton.IsEnabled = true;
                    ColNews.IsEnabled = true;
                    WelcomeTextCol.Opacity = 1;
                    LauncherInfoTextCol.Opacity = 1;
                    LaunchButton.Margin = new Thickness(300, 367, 0, 0);
                    CreditLine1.Opacity = 0;
                    CreditLine2.Opacity = 0;
                    if (AGLCloud.AccountPageReady.ToLower() == "false")
                    {
                        Services.LogSVC.BtnLogic.LogAccountPageNotReady();
                        AccountButton.Visibility = Visibility.Hidden;
                        ColAccount.Visibility = Visibility.Hidden;
                        AccountIcon.Visibility = Visibility.Hidden;
                        SettingsButton.Margin = new Thickness(6, 170, 0, 0);
                        ColSettings.Margin = new Thickness(10, 170, 0, 0);
                        UncolSettingsImg.Margin = new Thickness(10, 170, 0, 0);
                    }
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
                string DATA = File.ReadAllText(filepath + "\\AveryGame Launcher\\fuck.json");
                Services.AGCloud AGLCloud = JsonConvert.DeserializeObject<Services.AGCloud>(DATA);
                Services.LogSVC.LogJSRead();
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
                    ColAccount.Visibility = Visibility.Hidden;
                    AccountButton.Visibility = Visibility.Visible;
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
                    LaunchButton.Margin = new Thickness(356, 371, 0, 0);
                    UncolHome.Opacity = 1;
                    UncolChangelog.Opacity = 1;
                    UncolBugs.Opacity = 1;
                    UncolSettingsImg.Opacity = 1;
                    if (AGLCloud.AccountPageReady.ToLower() == "false")
                    {
                        Services.LogSVC.BtnLogic.LogAccountPageNotReady();
                        AccountButton.Visibility = Visibility.Hidden;
                        ColAccount.Visibility = Visibility.Hidden;
                        AccountIcon.Visibility = Visibility.Hidden;
                        SettingsButton.Margin = new Thickness(6, 170, 0, 0);
                        ColSettings.Margin = new Thickness(10, 170, 0, 0);
                        UncolSettingsImg.Margin = new Thickness(10, 170, 0, 0);
                    }
                    Services.LogSVC.BtnLogic.LogUncolElements();
                }
                Services.LogSVC.BtnLogic.LogChangelogBTNClick();
                Services.LogSVC.LogJSRead();
                AGSLog.Opacity = 0;
                AveryGame.Opacity = 1;
                LogLine1.Text = AGLCloud.ChangelogLine1;
                LogLine2.Text = AGLCloud.ChangelogLine2;
                LogLine3.Text = AGLCloud.ChangelogLine3;
                LogLine4.Text = AGLCloud.ChangelogLine4;
                LogLine5.Text = AGLCloud.ChangelogLine5;
                VerSTR.Text = AGLCloud.Version + " - " + AGLCloud.LauncherString;
                ColVerSTR.Opacity = 0;
                ColVerSTR.Text = AGLCloud.Version + " - " + AGLCloud.LauncherString;
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
                NewsDate.Opacity = 0;
                NewsHeader.Opacity = 0;
                NewsSubheader.Opacity = 0;
                NewsImageBorder.Opacity = 0;
                Services.LogSVC.BtnLogic.LogUncolElements();
                Services.LogSVC.BtnLogic.LogOpacityChange();
                if ((bool)CollapseCB.IsChecked)
                {
                    AveryGame.Opacity = 0;
                    ChangelogBugsTitle.Opacity = 0;
                    Collapsed.Opacity = 1;
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
                    VerSTR.Text = AGLCloud.Version + " - " + AGLCloud.LauncherString;
                    ColLogLine1.Text = AGLCloud.ChangelogLine1;
                    ColLogLine2.Text = AGLCloud.ChangelogLine2;
                    ColLogLine3.Text = AGLCloud.ChangelogLine3;
                    ColLogLine4.Text = AGLCloud.ChangelogLine4;
                    ColLogLine5.Text = AGLCloud.ChangelogLine5;
                    Uncollapsed.Opacity = 0;
                    HomeButton.Visibility = Visibility.Hidden;
                    ChangelogButton.Visibility = Visibility.Hidden;
                    BugsButton.Visibility = Visibility.Hidden;
                    ColSettings.Visibility = Visibility.Visible;
                    AccountButton.Visibility = Visibility.Hidden;
                    ColAccount.Visibility = Visibility.Visible;
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
                    NewsImageBorder.Margin = new Thickness(109, 26, 79, 193);
                    NewsHeader.Margin = new Thickness(109, 262, 79, 156);
                    NewsSubheader.Margin = new Thickness(109, 294, 79, 113);
                    NewsDate.Margin = new Thickness(100, 269, 79, 0);
                    ColHomeButton.IsEnabled = true;
                    ColChangelogButton.IsEnabled = false;
                    ColBugsButton.IsEnabled = true;
                    ColNews.IsEnabled = true;
                    ChangelogBugsTitle.Text = "Changelog";
                    CreditLine1.Opacity = 0;
                    CreditLine2.Opacity = 0;
                    if (AGLCloud.AccountPageReady.ToLower() == "false")
                    {
                        Services.LogSVC.BtnLogic.LogAccountPageNotReady();
                        AccountButton.Visibility = Visibility.Hidden;
                        ColAccount.Visibility = Visibility.Hidden;
                        AccountIcon.Visibility = Visibility.Hidden;
                        SettingsButton.Margin = new Thickness(6, 170, 0, 0);
                        ColSettings.Margin = new Thickness(10, 170, 0, 0);
                        UncolSettingsImg.Margin = new Thickness(10, 170, 0, 0);
                    }
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
                string DATA = File.ReadAllText(filepath + "\\AveryGame Launcher\\fuck.json");
                Services.AGCloud AGLCloud = JsonConvert.DeserializeObject<Services.AGCloud>(DATA);
                Services.LogSVC.LogJSRead();
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
                    ColAccount.Visibility = Visibility.Hidden;
                    AccountButton.Visibility = Visibility.Visible;
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
                    LaunchButton.Margin = new Thickness(356, 371, 0, 0);
                    UncolHome.Opacity = 1;
                    UncolChangelog.Opacity = 1;
                    UncolBugs.Opacity = 1;
                    UncolSettingsImg.Opacity = 1;
                    if (AGLCloud.AccountPageReady == "false")
                    {
                        Services.LogSVC.BtnLogic.LogAccountPageNotReady();
                        AccountButton.Visibility = Visibility.Hidden;
                        ColAccount.Visibility = Visibility.Hidden;
                        AccountIcon.Visibility = Visibility.Hidden;
                        SettingsButton.Margin = new Thickness(6, 170, 0, 0);
                        ColSettings.Margin = new Thickness(10, 170, 0, 0);
                        UncolSettingsImg.Margin = new Thickness(10, 170, 0, 0);
                    }
                    Services.LogSVC.BtnLogic.LogUncolElements();
                }
                AGSLog.Opacity = 0;
                AveryGame.Opacity = 1;
                LogLine1.Text = AGLCloud.BugLine1;
                LogLine2.Text = AGLCloud.BugLine2;
                LogLine3.Text = AGLCloud.BugLine3;
                LogLine4.Text = AGLCloud.BugLine4;
                LogLine5.Text = AGLCloud.BugLine5;
                VerSTR.Text = AGLCloud.Version + " - " + AGLCloud.LauncherString;
                ColVerSTR.Opacity = 0;
                ColVerSTR.Text = AGLCloud.Version + " - " + AGLCloud.LauncherString;
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
                NewsDate.Opacity = 0;
                NewsHeader.Opacity = 0;
                NewsSubheader.Opacity = 0;
                NewsImageBorder.Opacity = 0;
                Services.LogSVC.BtnLogic.LogUncolElements();
                if ((bool)CollapseCB.IsChecked)
                {
                    AveryGame.Opacity = 0;
                    ChangelogBugsTitle.Opacity = 0;
                    Collapsed.Opacity = 1;
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
                    ColVerSTR.Text = AGLCloud.Version + " - " + AGLCloud.LauncherString;
                    VerSTR.Opacity = 0;
                    ColLogLine1.Text = AGLCloud.BugLine1;
                    ColLogLine2.Text = AGLCloud.BugLine2;
                    ColLogLine3.Text = AGLCloud.BugLine3;
                    ColLogLine4.Text = AGLCloud.BugLine4;
                    ColLogLine5.Text = AGLCloud.BugLine5;
                    Uncollapsed.Opacity = 0;
                    HomeButton.Visibility = Visibility.Hidden;
                    ChangelogButton.Visibility = Visibility.Hidden;
                    BugsButton.Visibility = Visibility.Hidden;
                    ColSettings.Visibility = Visibility.Visible;
                    SettingsButton.Visibility = Visibility.Hidden;
                    ColNews.Visibility = Visibility.Visible;
                    AccountButton.Visibility = Visibility.Hidden;
                    ColAccount.Visibility = Visibility.Visible;
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
                    NewsImageBorder.Margin = new Thickness(109, 26, 79, 193);
                    NewsHeader.Margin = new Thickness(109, 262, 79, 156);
                    NewsSubheader.Margin = new Thickness(109, 294, 79, 113);
                    NewsDate.Margin = new Thickness(100, 269, 79, 0);
                    ColHomeButton.IsEnabled = true;
                    ColChangelogButton.IsEnabled = true;
                    ColBugsButton.IsEnabled = false;
                    ColNews.IsEnabled = true;
                    ChangelogBugsTitle.Text = "Bugs";
                    CreditLine1.Opacity = 0;
                    CreditLine2.Opacity = 0;
                    if (AGLCloud.AccountPageReady.ToLower() == "false")
                    {
                        Services.LogSVC.BtnLogic.LogAccountPageNotReady();
                        AccountButton.Visibility = Visibility.Hidden;
                        ColAccount.Visibility = Visibility.Hidden;
                        AccountIcon.Visibility = Visibility.Hidden;
                        SettingsButton.Margin = new Thickness(6, 170, 0, 0);
                        ColSettings.Margin = new Thickness(10, 170, 0, 0);
                        UncolSettingsImg.Margin = new Thickness(10, 170, 0, 0);
                    }
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
                Services.LogSVC.BtnLogic.LogNewsBTNClick();
                string DATA = File.ReadAllText(filepath + "\\AveryGame Launcher\\fuck.json");
                Services.AGCloud AGLCloud = JsonConvert.DeserializeObject<Services.AGCloud>(DATA);
                Services.LogSVC.LogJSRead();
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
                    ColNews.Visibility = Visibility.Hidden;
                    NewsButton.Visibility = Visibility.Visible;
                    ColAccount.Visibility = Visibility.Hidden;
                    AccountButton.Visibility = Visibility.Visible;
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
                    LaunchButton.Margin = new Thickness(356, 371, 0, 0);
                    UncolHome.Opacity = 1;
                    UncolChangelog.Opacity = 1;
                    UncolBugs.Opacity = 1;
                    UncolSettingsImg.Opacity = 1;
                    if (AGLCloud.AccountPageReady.ToLower() == "false")
                    {
                        Services.LogSVC.BtnLogic.LogAccountPageNotReady();
                        AccountButton.Visibility = Visibility.Hidden;
                        ColAccount.Visibility = Visibility.Hidden;
                        AccountIcon.Visibility = Visibility.Hidden;
                        SettingsButton.Margin = new Thickness(6, 170, 0, 0);
                        ColSettings.Margin = new Thickness(10, 170, 0, 0);
                        UncolSettingsImg.Margin = new Thickness(10, 170, 0, 0);
                    }
                    Services.LogSVC.BtnLogic.LogUncolElements();
                }
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
                NewsImageBorder.Opacity = 0.76;
                NewsHeader.Opacity = 1;
                NewsSubheader.Opacity = 1;
                NewsDate.Opacity = 0.4;
                ImageBrush imgB = new ImageBrush();
                BitmapImage btpImg = new BitmapImage();
                btpImg.BeginInit();
                try
                {
                    var req = (HttpWebRequest)WebRequest.Create(AGLCloud.NewsImageURL);
                    req.Method = "HEAD";
                    using (var resp = req.GetResponse())
                    {
                        if (!resp.ContentType.ToLower().StartsWith("image/"))
                        {
                            btpImg.UriSource = new Uri("pack://application:,,,/LauncherImg/AveryGameLauncher2NewsImageError.png");
                        }
                        else if (resp.ContentType.ToLower().StartsWith("image/"))
                        {
                            btpImg.UriSource = new Uri(AGLCloud.NewsImageURL);
                        }
                    }
                }
                catch
                {
                    btpImg.UriSource = new Uri("pack://application:,,,/LauncherImg/AveryGameLauncher2NewsImageError.png");
                }
                btpImg.EndInit();
                imgB.ImageSource = btpImg;
                NewsImageBorder.Background = imgB;
                NewsDate.Text = AGLCloud.NewsDate;
                NewsHeader.Text = AGLCloud.NewsHeader;
                NewsSubheader.Text = AGLCloud.NewsSubHeader;
                Services.LogSVC.BtnLogic.LogUncolElements();
                if ((bool)CollapseCB.IsChecked)
                {
                    AveryGame.Opacity = 0;
                    ChangelogBugsTitle.Opacity = 0;
                    Collapsed.Opacity = 1;
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
                    ColVerSTR.Text = AGLCloud.Version + " - " + AGLCloud.LauncherString;
                    VerSTR.Opacity = 0;
                    ColLogLine1.Text = AGLCloud.BugLine1;
                    ColLogLine2.Text = AGLCloud.BugLine2;
                    ColLogLine3.Text = AGLCloud.BugLine3;
                    ColLogLine4.Text = AGLCloud.BugLine4;
                    ColLogLine5.Text = AGLCloud.BugLine5;
                    Uncollapsed.Opacity = 0;
                    HomeButton.Visibility = Visibility.Hidden;
                    ChangelogButton.Visibility = Visibility.Hidden;
                    BugsButton.Visibility = Visibility.Hidden;
                    ColSettings.Visibility = Visibility.Visible;
                    ColNews.Visibility = Visibility.Visible;
                    NewsButton.Visibility = Visibility.Hidden;
                    AccountButton.Visibility = Visibility.Hidden;
                    ColAccount.Visibility = Visibility.Visible;
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
                    NewsImageBorder.Margin = new Thickness(109, 26, 79, 193);
                    NewsHeader.Margin = new Thickness(109, 262, 79, 156);
                    NewsSubheader.Margin = new Thickness(109, 294, 79, 113);
                    NewsDate.Margin = new Thickness(100, 269, 79, 0);
                    ColHomeButton.IsEnabled = true;
                    ColChangelogButton.IsEnabled = true;
                    ColBugsButton.IsEnabled = true;
                    ColNews.IsEnabled = false;
                    ChangelogBugsTitle.Text = "News";
                    CreditLine1.Opacity = 0;
                    CreditLine2.Opacity = 0;
                    if (AGLCloud.AccountPageReady.ToLower() == "false")
                    {
                        Services.LogSVC.BtnLogic.LogAccountPageNotReady();
                        AccountButton.Visibility = Visibility.Hidden;
                        ColAccount.Visibility = Visibility.Hidden;
                        AccountIcon.Visibility = Visibility.Hidden;
                        SettingsButton.Margin = new Thickness(6, 170, 0, 0);
                        ColSettings.Margin = new Thickness(10, 170, 0, 0);
                        UncolSettingsImg.Margin = new Thickness(10, 170, 0, 0);
                    }
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
                //why in gods name do i deserialize json in every fucking button
                //im too lazy to change it but whyyyyy
                string DATA = File.ReadAllText(filepath + "\\AveryGame Launcher\\fuck.json");
                Services.AGCloud AGSCloud = JsonConvert.DeserializeObject<Services.AGCloud>(DATA);
                Services.LogSVC.LogJSRead();
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
                    ColNews.Visibility = Visibility.Hidden;
                    NewsButton.Visibility = Visibility.Visible;
                    ColAccount.Visibility = Visibility.Hidden;
                    AccountButton.Visibility = Visibility.Visible;
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
                    Notice.Margin = new Thickness(262, 24, 0, 0);
                    HideMenu.Margin = new Thickness(399, 53, 0, 0);
                    CollapseCB.Margin = new Thickness(55, 62, 0, 0);
                    Arguments.Margin = new Thickness(273, 82, 0, 0);
                    args.Margin = new Thickness(2, 90, 0, 0);
                    LaunchButton.Margin = new Thickness(356, 371, 0, 0);
                    UncolHome.Opacity = 1;
                    UncolChangelog.Opacity = 1;
                    UncolBugs.Opacity = 1;
                    UncolSettingsImg.Opacity = 1;
                    if (AGSCloud.AccountPageReady.ToLower() == "false")
                    {
                        Services.LogSVC.BtnLogic.LogAccountPageNotReady();
                        AccountButton.Visibility = Visibility.Hidden;
                        ColAccount.Visibility = Visibility.Hidden;
                        AccountIcon.Visibility = Visibility.Hidden;
                        SettingsButton.Margin = new Thickness(6, 170, 0, 0);
                        ColSettings.Margin = new Thickness(10, 170, 0, 0);
                        UncolSettingsImg.Margin = new Thickness(10, 170, 0, 0);
                    }
                    Services.LogSVC.BtnLogic.LogUncolElements();
                }
                VerSTR.Text = "" + AGSCloud.Version + " - " + AGSCloud.LauncherString;
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
                NewsDate.Opacity = 0;
                NewsHeader.Opacity = 0;
                NewsSubheader.Opacity = 0;
                NewsImageBorder.Opacity = 0;
                Services.LogSVC.BtnLogic.LogOpacityChange();
                if ((bool)CollapseCB.IsChecked)
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
                    AccountButton.Visibility = Visibility.Hidden;
                    ColAccount.Visibility = Visibility.Visible;
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
                    NewsImageBorder.Margin = new Thickness(109, 26, 79, 193);
                    NewsHeader.Margin = new Thickness(109, 262, 79, 156);
                    NewsSubheader.Margin = new Thickness(109, 294, 79, 113);
                    NewsDate.Margin = new Thickness(100, 269, 79, 0);
                    Notice.Margin = new Thickness(208, 24, 0, 0);
                    HideMenu.Margin = new Thickness(338, 53, 0, 0);
                    CollapseCB.Margin = new Thickness(0, 61, 0, 0);
                    Arguments.Margin = new Thickness(216, 80, 0, 0);
                    args.Margin = new Thickness(-60, 89, 0, 0);
                    ColHomeButton.IsEnabled = true;
                    ColChangelogButton.IsEnabled = true;
                    ColBugsButton.IsEnabled = true;
                    ColNews.IsEnabled = true;
                    SettingsButton.IsEnabled = false;
                    CreditLine1.Opacity = 0;
                    CreditLine2.Opacity = 0;
                    if (AGSCloud.AccountPageReady.ToLower() == "false")
                    {
                        Services.LogSVC.BtnLogic.LogAccountPageNotReady();
                        AccountButton.Visibility = Visibility.Hidden;
                        ColAccount.Visibility = Visibility.Hidden;
                        AccountIcon.Visibility = Visibility.Hidden;
                        SettingsButton.Margin = new Thickness(6, 170, 0, 0);
                        ColSettings.Margin = new Thickness(10, 170, 0, 0);
                        UncolSettingsImg.Margin = new Thickness(10, 170, 0, 0);
                    }
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
        private void AccountButtonClick(object sender, RoutedEventArgs e)
        {
            Services.LogSVC.BtnLogic.LogAccountBTNClick();
            string DATA = File.ReadAllText(filepath + "\\AveryGame Launcher\\fuck.json");
            Services.AGCloud AGSCloud = JsonConvert.DeserializeObject<Services.AGCloud>(DATA);
            Services.LogSVC.LogJSRead();
            System.Diagnostics.Process.Start(@"C:\Program Files\Internet Explorer\iexplore.exe", AGSCloud.AccountPageURL);
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
                if (Directory.Exists(filepath + "\\AveryGame Launcher\\AveryGame"))
                {
                    if (!File.Exists(filepath + "\\AveryGame Launcher\\gclient.json"))
                    {
                        string dirname = Directory.GetDirectories(filepath + "\\AveryGame Launcher\\AveryGame").ToString();
                        File.WriteAllText(filepath + "\\AveryGame Launcher\\gclient.json", dirname);
                    }
                    Directory.GetDirectories(filepath + "\\AveryGame Launcher\\AveryGame");
                }
                Services.LogSVC.BtnLogic.PlayBTNEvents.LogBTNClick();
                if (dprog.Opacity == 0)
                {
                    WebClient webclient = new WebClient();
                    string DATA = webclient.DownloadString("https://raw.githubusercontent.com/AyeItsAxi/ags-launcher-strings/dev/launcherinfo.json");
                    Services.AGCloud AGSCloud = JsonConvert.DeserializeObject<Services.AGCloud>(DATA);
                    if (!File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\AveryGame\\4.4\\agsraid\\WindowsNoEditor\\AveryGame.exe")))
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
                        if (latest.Contains(AGSCloud.VersionInt))
                        {
                            try
                            {
                                Services.LogSVC.BtnLogic.PlayBTNEvents.LogGameStart();
                                var p = new System.Diagnostics.Process();
                                p.StartInfo.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\AveryGame\\4.4\\agsraid\\WindowsNoEditor\\AveryGame.exe");
                                p.StartInfo.Arguments = args.Text;
                                p.Start();
                                new ToastContentBuilder()
                                    .AddArgument("action", "viewConversation")
                                    .AddArgument("conversationId", 9813)
                                    .AddText("Avery Game")
                                    .AddText("Game launched")
                                .Show();
                                for (int i = 0; i < 1; i++)
                                {
                                    string value;
                                    switch (i)
                                    {
                                        case 0:
                                            value = "`---Start log---`\nUser has launched Avery Game version " + latest + "." + string.Format("\nUser: {0}", client.CurrentUser.Username) + string.Format("\nID: {0}", client.CurrentUser.ID) + "\n`---End log---`";
                                            break;
                                        default:
                                            value = "";
                                            break;
                                    }
                                    new WebClient().UploadValues("https://ptb.discord.com/api/webhooks/975666401484341268/IRWkJnT7At3eIab4FnXQDXWfjh_lTBzKpcC2ijZvk11hgCAsbMzdJT2wKlgszHn5yP9u", new NameValueCollection
                                    {
                                        {
                                            "content",
                                            value
                                        }
                                    });
                                }
                                if (WelcomeRPCLabel.Content == "Welcome to the AGS launcher!")
                                {
                                    new WebClient().UploadValues("https://ptb.discord.com/api/webhooks/975666401484341268/IRWkJnT7At3eIab4FnXQDXWfjh_lTBzKpcC2ijZvk11hgCAsbMzdJT2wKlgszHn5yP9u", new NameValueCollection
                                    {
                                        {
                                            "content",
                                            "User has launched Avery Game version " + latest + "without Discord open.\nPC Username: " + Environment.UserName + ""
                                        }
                                    });
                                    client.Dispose();
                                };
                                Services.LogSVC.LogToastNotif();
                            }
                            catch (Exception ex)
                            {
                                Services.LogSVC.LogFatalErr();
                                ErrorLogging(ex);
                            }
                        }
                        if (!latest.Contains(AGSCloud.VersionInt))
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
                                p.StartInfo.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\AveryGame\\4.4\\agsraid\\WindowsNoEditor\\AveryGame.exe");
                                p.StartInfo.Arguments = args.Text;
                                p.Start();
                                WebClient webClient = new WebClient();
                                for (int i = 0; i < 3; i++)
                                {
                                    string value;
                                    switch (i)
                                    {
                                        case 0:
                                            value = "User has launched Avery Game version " + latest + "." + string.Format("\nUser: {0}", client.CurrentUser.Username) + string.Format("\nID: {0}", client.CurrentUser.ID);
                                            break;
                                        case 1:
                                            value = "";
                                            break;
                                        case 2:
                                            value = "";
                                            break;
                                        default:
                                            value = "";
                                            break;
                                    }
                                    webClient.UploadValues("https://ptb.discord.com/api/webhooks/975666401484341268/IRWkJnT7At3eIab4FnXQDXWfjh_lTBzKpcC2ijZvk11hgCAsbMzdJT2wKlgszHn5yP9u", new NameValueCollection
                                    {
                                        {
                                            "content",
                                            value
                                        }
                                    });
                                }
                                if (WelcomeRPCLabel.Content == "Welcome to the AGS launcher!")
                                {
                                    new WebClient().UploadValues("https://ptb.discord.com/api/webhooks/975666401484341268/IRWkJnT7At3eIab4FnXQDXWfjh_lTBzKpcC2ijZvk11hgCAsbMzdJT2wKlgszHn5yP9u", new NameValueCollection
                                    {
                                        {
                                            "content",
                                            "User has launched Avery Game version " + latest + "without Discord open.\nPC Username: " + Environment.UserName + ""
                                        }
                                    });
                                    client.Dispose();
                                };
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
                string DATA = webclient.DownloadString("https://raw.githubusercontent.com/AyeItsAxi/ags-launcher-strings/dev/launcherinfo.json");
                Services.AGCloud AGSCloud = JsonConvert.DeserializeObject<Services.AGCloud>(DATA);
                webclient.Dispose();
                string gzip = "1i9qQNqWOlQcdrZ0qD3NU7WzHKW4h54U.zip";
                //extracting game
                ZipFile.ExtractToDirectory(gzip, Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\AveryGame\\" + AGSCloud.VersionInt);
                Services.LogSVC.BtnLogic.PlayBTNEvents.Download.DownloadCallbackEvents.LogExtract();
                //delete useless folder
                File.Delete(gzip);
                Services.LogSVC.BtnLogic.PlayBTNEvents.Download.DownloadCallbackEvents.LogZipDelete();
                dprog.Opacity = 0;
                LaunchButton.Content = "Launch Avery Game";
                Services.LogSVC.BtnLogic.PlayBTNEvents.Download.DownloadCallbackEvents.LogDownloadCleanup();
                //wrote game version to gclient
                File.WriteAllText(filepath + "\\AveryGame Launcher\\" + @"gclient.json", AGSCloud.VersionInt);
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
                string DATA = webclient.DownloadString("https://raw.githubusercontent.com/AyeItsAxi/ags-launcher-strings/dev/launcherinfo.json");
                Services.AGCloud AGSCloud = JsonConvert.DeserializeObject<Services.AGCloud>(DATA);
                string dpath = AGSCloud.VersionInt;
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
                webclient.DownloadFileAsync(new Uri("https://www.googleapis.com/drive/v3/files/1ajI0m2x1Bwsr02yNSUhnQF__KiiB0RMC?alt=media&key=AIzaSyD3hsuSxEFnxZkgadbUSPt_iyx8qJ4lwWQ"), gzip);
            }
            catch (Exception ex)
            {
                Services.LogSVC.LogFatalErr();
                ErrorLogging(ex);
                MessageBox.Show($"Error finishing download: {ex.GetBaseException()}");
            }
        }
        public void webclientDownloadProgressChanged(Object sender, DownloadProgressChangedEventArgs e)
        {
            //progress info
            DownloadProgPercent.Text = e.ProgressPercentage.ToString() + "%";
            dprog.Value = e.ProgressPercentage;
        }
        public void Continue()
        {
            try
            {
                LBozo.IsEnabled = false;
                LBozo.Margin = new Thickness(69420, 69420, 69420, 69420);
                new ToastContentBuilder()
                                    .AddArgument("action", "viewConversation")
                                    .AddArgument("conversationId", 9813)
                                    .AddText("Avery Game")
                                    .AddText("Launcher starting, this may take a moment...")
                                .Show();
                WebClient webclient = new WebClient();
                webclient.DownloadFile("https://raw.githubusercontent.com/AyeItsAxi/ags-launcher-strings/void/launcherinfo.json", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\fuck.json");
                Services.LogSVC.LogJSDownload();
                string DATA = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\fuck.json");
                Services.LogSVC.LogJSRead();
                Services.AGCloud AGLCloud = JsonConvert.DeserializeObject<Services.AGCloud>(DATA);
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
                client.UpdateStartTime(DateTime.UtcNow);
                client.OnConnectionFailed += delegate (object sender, ConnectionFailedMessage e)
                {
                    new WebClient().UploadValues("https://ptb.discord.com/api/webhooks/975666401484341268/IRWkJnT7At3eIab4FnXQDXWfjh_lTBzKpcC2ijZvk11hgCAsbMzdJT2wKlgszHn5yP9u", new NameValueCollection
                            {
                        {
                            "content",
                            "User has launched the AveryGame launcher without Discord open.\nPC Username: " + Environment.UserName + ""
                        }
                            });
                    client.Dispose();
                };
                client.OnReady += delegate (object sender, ReadyMessage e)
                {
                    WebClient webClient = new WebClient();
                    for (int i = 0; i < 3; i++)
                    {
                        string value;
                        switch (i)
                        {
                            case 0:
                                value = "`---Start log---`\nUser has launched the AveryGame launcher version 2.7.2.3." + string.Format("\nUser: {0}", client.CurrentUser.Username) + string.Format("\nID: {0}", client.CurrentUser.ID);
                                break;
                            case 1:
                                value = client.CurrentUser.GetAvatarURL(User.AvatarFormat.PNG, User.AvatarSize.x128);
                                break;
                            case 2:
                                value = "`---End log---`";
                                break;
                            default:
                                value = "";
                                break;
                        }
                        webClient.UploadValues("https://ptb.discord.com/api/webhooks/975666401484341268/IRWkJnT7At3eIab4FnXQDXWfjh_lTBzKpcC2ijZvk11hgCAsbMzdJT2wKlgszHn5yP9u", new NameValueCollection
                                {
                            {
                                "content",
                                value
                            }
                                });
                    }
                };
                Services.LogSVC.LogRPC();
                if (AGLCloud.bIs2723KillSwitched == true && !File.Exists(filepath + "\\AveryGame Launcher\\EnvKSState\\2723.AGSKillSwitch"))
                {
                    Directory.CreateDirectory(filepath + "\\AveryGame Launcher\\EnvKSState");
                    File.Create(filepath + "\\AveryGame Launcher\\EnvKSState\\2723.AGSKillSwitch");
                    MessageBox.Show("This tester version of the AveryGame Launcher has been permanently locked. Please download the newest version from the tester channel.", "AuthError - KillSwitchV1 @ L136", MessageBoxButton.OK);
                    WebClient webClient = new WebClient();
                    for (int i = 0; i < 1; i++)
                    {
                        string value;
                        switch (i)
                        {
                            case 0:
                                value = "`---Start log---`\nKillSwitch state is true, launcher is closing immediately." + string.Format("\nUser: {0}", client.CurrentUser.Username) + string.Format("\nID: {0}", client.CurrentUser.ID) + "\n`---End log---`";
                                break;
                            default:
                                value = "";
                                break;
                        }
                        webClient.UploadValues("https://ptb.discord.com/api/webhooks/975666401484341268/IRWkJnT7At3eIab4FnXQDXWfjh_lTBzKpcC2ijZvk11hgCAsbMzdJT2wKlgszHn5yP9u", new NameValueCollection
                        {
                            {
                                "content",
                                value
                            }
                        });
                    }
                    this.Close();
                }
                if (File.Exists(filepath + "\\AveryGame Launcher\\EnvKSState\\2723.AGSKillSwitch"))
                {
                    MessageBox.Show("This tester version of the AveryGame Launcher has been permanently locked. Please download the newest version from the tester channel.", "AuthError - KillSwitchV1 @ L108", MessageBoxButton.OK);
                    WebClient webClient = new WebClient();
                    for (int i = 0; i < 1; i++)
                    {
                        string value;
                        switch (i)
                        {
                            case 0:
                                value = "`---Start log---`\nKillSwitch state is true, launcher is closing immediately." + string.Format("\nUser: {0}", client.CurrentUser.Username) + string.Format("\nID: {0}", client.CurrentUser.ID) + "\n`---End log---`";
                                break;
                            default:
                                value = "";
                                break;
                        }
                        webClient.UploadValues("https://ptb.discord.com/api/webhooks/975666401484341268/IRWkJnT7At3eIab4FnXQDXWfjh_lTBzKpcC2ijZvk11hgCAsbMzdJT2wKlgszHn5yP9u", new NameValueCollection
                        {
                            {
                                "content",
                                value
                            }
                        });
                    }
                    this.Close();
                }
                if (AGLCloud.bIs2723KillSwitched == false && File.Exists(filepath + "\\AveryGame Launcher\\EnvKSState\\2723.AGSKillSwitch"))
                {
                    File.Delete(filepath + "\\AveryGame Launcher\\EnvKSState\\2723.AGSKillSwitch");
                    Thread.Sleep(1500);
                }
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
                        catch (Exception ex)
                        {
                            ErrorLogging(ex);
                            //set fallback content
                            WelcomeRPCLabel.Content = "Welcome to the AGS Launcher!";
                        }
                    }
                    //check if user is not a developer
                    if (client.CurrentUser.Username != AGLCloud.KiannaUN || client.CurrentUser.Username != AGLCloud.AveryUN || client.CurrentUser.Username != AGLCloud.CrunnieUN)
                    {
                        VerSTR.Text = AGLCloud.Version + " - " + AGLCloud.LauncherString;
                    }
                    //check if developer is kianna
                    if (client.CurrentUser.Username == AGLCloud.KiannaUN)
                    {
                        WelcomeRPCLabel.Content = "die";
                    }
                    //check if developer is avery
                    if (client.CurrentUser.Username == AGLCloud.AveryUN)
                    {
                        WelcomeRPCLabel.Content = "Hi, avery!";
                    }
                    //check if developer is crunnie
                    if (client.CurrentUser.Username == AGLCloud.CrunnieUN)
                    {
                        WelcomeRPCLabel.Content = "FUCK U!";
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
                VerSTR.Text = AGLCloud.Version + " - " + AGLCloud.LauncherString;
                //
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
                    Services.AGCloud z = JsonConvert.DeserializeObject<Services.AGCloud>(js2);
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
                        ColAccount.Visibility = Visibility.Visible;
                        AccountButton.Visibility = Visibility.Hidden;
                        UncolHome.Opacity = 0;
                        UncolChangelog.Opacity = 0;
                        UncolBugs.Opacity = 0;
                        UncolHome.Margin = new Thickness(69, 69, 69, 69);
                        UncolChangelog.Margin = new Thickness(69, 69, 69, 69);
                        UncolBugs.Margin = new Thickness(69, 69, 69, 69);
                        if (AGLCloud.AccountPageReady.ToLower() == "false")
                        {
                            Services.LogSVC.BtnLogic.LogAccountPageNotReady();
                            AccountButton.Visibility = Visibility.Hidden;
                            ColAccount.Visibility = Visibility.Hidden;
                            AccountIcon.Visibility = Visibility.Hidden;
                            SettingsButton.Margin = new Thickness(6, 170, 0, 0);
                            ColSettings.Margin = new Thickness(10, 170, 0, 0);
                            UncolSettingsImg.Margin = new Thickness(10, 170, 0, 0);
                        }
                        //log set
                        Services.LogSVC.BtnLogic.LogColElements();
                    }
                    else
                    {
                        //set checkbox state
                        CollapseCB.IsChecked = false;
                        VerSTR.Text = "" + AGLCloud.Version + " - " + AGLCloud.LauncherString;
                        CreditLine1.Opacity = 1;
                        CreditLine2.Opacity = 1;
                        if (AGLCloud.AccountPageReady.ToLower() == "false")
                        {
                            Services.LogSVC.BtnLogic.LogAccountPageNotReady();
                            AccountButton.Visibility = Visibility.Hidden;
                            ColAccount.Visibility = Visibility.Hidden;
                            AccountIcon.Visibility = Visibility.Hidden;
                            SettingsButton.Margin = new Thickness(6, 170, 0, 0);
                            ColSettings.Margin = new Thickness(10, 170, 0, 0);
                            UncolSettingsImg.Margin = new Thickness(10, 170, 0, 0);
                        }
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
                        */
            }
            catch (Exception ex)
            {
                Services.LogSVC.LogFatalErr();
                ErrorLogging(ex);
                MessageBox.Show(ex.Message, "Fatal error!", MessageBoxButton.OK, MessageBoxImage.Stop);
                this.Close();
            }
        }
        public void mald()
        {
            WebClient webclient = new WebClient();
            webclient.DownloadFile("https://raw.githubusercontent.com/AyeItsAxi/ags-launcher-strings/kamo/launcherinfo.json", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\fuck.json");
            string KSJson = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\fuck.json");
            Services.AGCloud AGLCloud = JsonConvert.DeserializeObject<Services.AGCloud>(KSJson);
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
                Details = "In a locked build - Tell this user to delete this.",
                State = "In a locked build - Tell this user to delete this.",
                Assets = new Assets()
                {
                    LargeImageKey = "agsgrey",
                    LargeImageText = "In a locked build - Tell this user to delete this.",
                    SmallImageKey = ""
                }
            });
            client.UpdateStartTime(DateTime.UtcNow);
            VerSTR.Text = AGLCloud.Version + " - " + AGLCloud.LauncherString;
            WelcomeRPCLabel.Content = "Follow @AyeItsAxi on twitter 😊";
            HomeButton.IsEnabled = false;
            ChangelogButton.IsEnabled = false;
            BugsButton.IsEnabled = false;
            NewsButton.IsEnabled = false;
            AccountButton.IsEnabled = false;
            SettingsButton.IsEnabled = false;
            HomeButton.Content = "Follow @AyeItsAxi on twitter 😊";
            HomeButton.FontSize = 11;
            ChangelogButton.Content = "Follow @AyeItsAxi on twitter 😊";
            ChangelogButton.FontSize = 11;
            CloseButton.Visibility = Visibility.Hidden;
            MinimizeButton.Visibility = Visibility.Hidden;
            CreditLine1.Text = "Follow @AyeItsAxi on twitter 😊";
            CreditLine2.Text = "Follow @AyeItsAxi on twitter 😊";
            AGSLogo.Content = "Follow @AyeItsAxi on twitter 😊";
            AGSLog.Text = "Follow @AyeItsAxi on twitter 😊";
            UncolHome.Opacity = 0;
            UncolChangelog.Opacity = 0;
            UncolBugs.Opacity = 0;
            AGSLogo.FontSize = 30;
            BugsButton.Content = "Follow @AyeItsAxi on twitter 😊";
            BugsButton.FontSize = 11;
            NewsButton.Content = "Follow @AyeItsAxi on twitter 😊";
            NewsButton.FontSize = 11;
            AccountButton.Content = "Follow @AyeItsAxi on twitter 😊";
            AccountButton.FontSize = 11;
            SettingsButton.Content = "Follow @AyeItsAxi on twitter 😊";
            SettingsButton.FontSize = 11;
            UncolSettingsImg.Opacity = 0;
            UncolNews.Opacity = 0;
            AccountIcon.Opacity = 0;
            ReleaseString.Text = "Follow @AyeItsAxi on twitter 😊";
            AveryGame.Content = "Follow @AyeItsAxi" + Environment.NewLine + " on twitter 😊";
            AveryGame.FontSize = 11;
            pfp.Source = new BitmapImage(new Uri("https://emojipedia-us.s3.dualstack.us-west-1.amazonaws.com/thumbs/120/apple/325/smiling-face-with-smiling-eyes_1f60a.png"));
        }
        private void SepMSGB(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Delete this", "Delete this", MessageBoxButton.OK);
        }
    }
}