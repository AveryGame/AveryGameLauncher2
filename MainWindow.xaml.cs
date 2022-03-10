﻿using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
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
                client.Initialize();
                client.SetPresence(new RichPresence()
                {
                    Details = "V2 launcher",
                    State = "In the launcher",
                    Assets = new Assets()
                    {
                        LargeImageKey = "agsgrey",
                        LargeImageText = "ags+release+2.2+pub",
                        SmallImageKey = ""
                    }
                });
                if (!Directory.Exists(filepath + "\\AveryGame Launcher"))
                {
                    Directory.CreateDirectory(filepath + "\\AveryGame Launcher");
                }
                WebClient webclient = new WebClient();
                string DATA = webclient.DownloadString("https://raw.githubusercontent.com/imstillamazedbyit/1q29dks43895r794/main/launcherinfo.json");
                LauncherCloud json = JsonConvert.DeserializeObject<LauncherCloud>(DATA);
                File.WriteAllText(filepath + "\\AveryGame Launcher\\" + @"fuck.json", DATA);
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
                    if (client.CurrentUser.Username != json.KiannaUN || client.CurrentUser.Username != json.AveryUN || client.CurrentUser.Username != json.CrunnieUN)
                    {
                        WelcomeRPCLabel.Content = "Welcome to the AGS Launcher!";
                        VerSTR.Text = json.Version + " - " + json.LauncherString;
                    }
                    if (client.CurrentUser.Username == json.KiannaUN)
                    {
                        WelcomeRPCLabel.Content = "kys";
                        VerSTR.Text = json.DevLauncherString;
                    }
                    if (client.CurrentUser.Username == json.AveryUN)
                    {
                        WelcomeRPCLabel.Content = "Hi, avery!";
                        VerSTR.Text = json.DevLauncherString;
                    }
                    if (client.CurrentUser.Username == json.CrunnieUN)
                    {
                        WelcomeRPCLabel.Content = "FUCK U!";
                        VerSTR.Text = json.DevLauncherString;
                    }
                }
                catch
                {

                }
                try
                {
                    pfp.Source = new BitmapImage(new Uri(client.CurrentUser.GetAvatarURL(User.AvatarFormat.PNG)));
                }
                catch
                {
                    pfp.Source = new BitmapImage(new Uri("https://cdn.discordapp.com/app-assets/939285353355935774/939285441323077632.png"));
                }
                if (File.Exists("1i9qQNqWOlQcdrZ0qD3NU7WzHKW4h54U.zip"))
                {
                    File.Delete("1i9qQNqWOlQcdrZ0qD3NU7WzHKW4h54U.zip");
                }
                VerSTR.Text = json.Version + " - " + json.LauncherString;
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
                HideMenu.Opacity = 0;
                CollapseCB.Opacity = 0;
                Notice.Opacity = 0;
                Arguments.Opacity = 0;
                args.Opacity = 0;
                if (File.Exists(filepath + "\\AveryGame Launcher\\Clientsettings.json"))
                {
                    string js2 = File.ReadAllText(filepath + "\\AveryGame Launcher\\Clientsettings.json");
                    LauncherCloud z = JsonConvert.DeserializeObject<LauncherCloud>(js2);
                    if (z.CollapseMenu == "True")
                    {
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
                        UncolHome.Opacity = 0;
                        UncolChangelog.Opacity = 0;
                        UncolBugs.Opacity = 0;
                        UncolHome.Margin = new Thickness(69, 69, 69, 69);
                        UncolChangelog.Margin = new Thickness(69, 69, 69, 69);
                        UncolBugs.Margin = new Thickness(69, 69, 69, 69);
                    }
                    else
                    {
                        CollapseCB.IsChecked = false;
                        ChangelogLine1.Text = "               " + json.ChangelogLine1;
                        ChangelogLine2.Text = "               " + json.ChangelogLine2;
                        ChangelogLine3.Text = "      " + json.ChangelogLine3;
                        ChangelogLine4.Text = "      " + json.ChangelogLine4;
                        ChangelogLine5.Text = "      " + json.ChangelogLine5;
                        BugsLine1.Text = "      " + json.BugLine1;
                        BugsLine2.Text = "      " + json.BugLine2;
                        BugsLine3.Text = "      " + json.BugLine3;
                        BugsLine4.Text = "      " + json.BugLine4;
                        BugsLine5.Text = "      " + json.BugLine5;
                        VerSTR.Text = "             " + json.Version + " - " + json.LauncherString;
                        CreditLine1.Opacity = 1;
                        CreditLine2.Opacity = 1;
                    }
                }
                else
                {
                    File.WriteAllText(@filepath + "\\AveryGame Launcher\\Clientsettings.json", "{ \"CollapseMenu\":\"" + CollapseCB.IsChecked + "\" }");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A fatal error occurred while opening the Avery Game launcher. Please make sure you are connected to the internet and try again. If the problem persists, report the issue and when it is occurring in #bug-reports in the Avery Game discord server. :" + ex.Message, "Fatal error!", MessageBoxButton.OK, MessageBoxImage.Stop);
                this.Close();
            }
        }
        private static readonly string filepath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public class LauncherCloud
        {
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
        }
        private void Home(object sender, RoutedEventArgs e)
        {
            try
            {
                string DATA = File.ReadAllText(filepath + "\\AveryGame Launcher\\fuck.json");
                LauncherCloud json = JsonConvert.DeserializeObject<LauncherCloud>(DATA);
                HomeButton.IsEnabled = false;
                LaunchButton.IsEnabled = true;
                BugsButton.IsEnabled = true;
                ChangelogButton.IsEnabled = true;
                ColSettings.IsEnabled = true;
                SettingsButton.IsEnabled = true;
                LaunchButton.Opacity = 1;
                AGSLogo.Opacity = 0;
                ChangelogBugsTitle.Opacity = 1;
                ChangelogBugsTitle.Text = "         Home";
                BugsButton.Content = "Bugs";
                HomeButton.Content = "• Home";
                ChangelogButton.Content = "Changelog";
                SettingsButton.Content = "Settings";
                VerSTR.Opacity = 0;
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
                ChangelogLine1.Text = json.BalanceCL1 + json.ChangelogLine1;
                ChangelogLine2.Text = json.BalanceCL2 + json.ChangelogLine2;
                ChangelogLine3.Text = json.BalanceCL3 + json.ChangelogLine3;
                ChangelogLine4.Text = json.BalanceCL4 + json.ChangelogLine4;
                ChangelogLine5.Text = json.BalanceCL5 + json.ChangelogLine5;
                BugsLine1.Text = json.BalanceBL1 + json.BugLine1;
                BugsLine2.Text = json.BalanceBL2 + json.BugLine2;
                BugsLine3.Text = json.BalanceBL3 + json.BugLine3;
                BugsLine4.Text = json.BalanceBL4 + json.BugLine4;
                BugsLine5.Text = json.BalanceBL5 + json.BugLine5;
                VerSTR.Text = "             " + json.Version + " - " + json.LauncherString;
                HideMenu.Opacity = 0;
                CollapseCB.Opacity = 0;
                Notice.Opacity = 0;
                Arguments.Opacity = 0;
                args.Opacity = 0;
                if ((bool)(CollapseCB.IsChecked))
                {
                    VerSTR.Text = json.Version + " - " + json.LauncherString;
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
                    UncolHome.Opacity = 0;
                    UncolChangelog.Opacity = 0;
                    UncolBugs.Opacity = 0;
                    UncolHome.Margin = new Thickness(69, 69, 69, 69);
                    UncolChangelog.Margin = new Thickness(69, 69, 69, 69);
                    UncolBugs.Margin = new Thickness(69, 69, 69, 69);
                    ColHomeButton.IsEnabled = false;
                    ColChangelogButton.IsEnabled = true;
                    ColBugsButton.IsEnabled = true;
                    WelcomeTextCol.Opacity = 1;
                    LauncherInfoTextCol.Opacity = 1;
                    LaunchButton.Margin = new Thickness(300, 367, 0, 0);
                }
                else
                {
                    WelcomeText.Opacity = 1;
                    LauncherInfoText.Opacity = 1;
                }
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
                string DATA = File.ReadAllText(filepath + "\\AveryGame Launcher\\fuck.json");
                LauncherCloud json = JsonConvert.DeserializeObject<LauncherCloud>(DATA);
                ChangelogLine1.Text = json.BalanceCL1 + json.ChangelogLine1;
                ChangelogLine2.Text = json.BalanceCL2 + json.ChangelogLine2;
                ChangelogLine3.Text = json.BalanceCL3 + json.ChangelogLine3;
                ChangelogLine4.Text = json.BalanceCL4 + json.ChangelogLine4;
                ChangelogLine5.Text = json.BalanceCL5 + json.ChangelogLine5;
                BugsLine1.Text = json.BalanceBL1 + json.BugLine1;
                BugsLine2.Text = json.BalanceBL2 + json.BugLine2;
                BugsLine3.Text = json.BalanceBL3 + json.BugLine3;
                BugsLine4.Text = json.BalanceBL4 + json.BugLine4;
                BugsLine5.Text = json.BalanceBL5 + json.BugLine5;
                VerSTR.Text = "             " + json.Version + " - " + json.LauncherString;
                ChangelogButton.IsEnabled = false;
                HomeButton.IsEnabled = true;
                LaunchButton.IsEnabled = false;
                BugsButton.IsEnabled = true;
                ColSettings.IsEnabled = true;
                SettingsButton.IsEnabled = true;
                LaunchButton.Opacity = 0;
                AGSLogo.Opacity = 0;
                ChangelogBugsTitle.Opacity = 1;
                ChangelogBugsTitle.Text = "      Changelog";
                BugsButton.Content = "Bugs";
                HomeButton.Content = "Home";
                ChangelogButton.Content = "• Changelog";
                SettingsButton.Content = "Settings";
                VerSTR.Opacity = 1;
                WelcomeTextCol.Opacity = 0;
                LauncherInfoTextCol.Opacity = 0;
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
                HideMenu.Opacity = 0;
                CollapseCB.Opacity = 0;
                Notice.Opacity = 0;
                Arguments.Opacity = 0;
                args.Opacity = 0;
                if ((bool)(CollapseCB.IsChecked))
                {
                    VerSTR.Text = json.Version + " - " + json.LauncherString;
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
                    UncolHome.Opacity = 0;
                    UncolChangelog.Opacity = 0;
                    UncolBugs.Opacity = 0;
                    UncolHome.Margin = new Thickness(69, 69, 69, 69);
                    UncolChangelog.Margin = new Thickness(69, 69, 69, 69);
                    UncolBugs.Margin = new Thickness(69, 69, 69, 69);
                    ColHomeButton.IsEnabled = true;
                    ColChangelogButton.IsEnabled = false;
                    ColBugsButton.IsEnabled = true;
                    ChangelogBugsTitle.Text = "Changelog";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A fatal error occurred while opening the Avery Game launcher. Please make sure you are connected to the internet and try again. If the problem persists, report the issue and when it is occurring in #bug-reports in the Avery Game discord server.:" + ex.Message, "Fatal error!", MessageBoxButton.OK, MessageBoxImage.Stop);
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
                string DATA = File.ReadAllText(filepath + "\\AveryGame Launcher\\fuck.json");
                LauncherCloud json = JsonConvert.DeserializeObject<LauncherCloud>(DATA);
                ChangelogLine1.Text = json.BalanceCL1 + json.ChangelogLine1;
                ChangelogLine2.Text = json.BalanceCL2 + json.ChangelogLine2;
                ChangelogLine3.Text = json.BalanceCL3 + json.ChangelogLine3;
                ChangelogLine4.Text = json.BalanceCL4 + json.ChangelogLine4;
                ChangelogLine5.Text = json.BalanceCL5 + json.ChangelogLine5;
                BugsLine1.Text = json.BalanceBL1 + json.BugLine1;
                BugsLine2.Text = json.BalanceBL2 + json.BugLine2;
                BugsLine3.Text = json.BalanceBL3 + json.BugLine3;
                BugsLine4.Text = json.BalanceBL4 + json.BugLine4;
                BugsLine5.Text = json.BalanceBL5 + json.BugLine5;
                VerSTR.Text = "             " + json.Version + " - " + json.LauncherString;
                ChangelogBugsTitle.Text = "       Bugs";
                ChangelogButton.IsEnabled = true;
                HomeButton.IsEnabled = true;
                LaunchButton.IsEnabled = false;
                BugsButton.IsEnabled = false;
                ColSettings.IsEnabled = true;
                SettingsButton.IsEnabled = true;
                LaunchButton.Opacity = 0;
                AGSLogo.Opacity = 0;
                ChangelogBugsTitle.Opacity = 1;
                ChangelogBugsTitle.Text = "      Bugs";
                BugsButton.Content = "• Bugs";
                HomeButton.Content = "Home";
                ChangelogButton.Content = "Changelog";
                SettingsButton.Content = "Settings";
                VerSTR.Opacity = 1;
                WelcomeTextCol.Opacity = 0;
                LauncherInfoTextCol.Opacity = 0;
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
                HideMenu.Opacity = 0;
                CollapseCB.Opacity = 0;
                Notice.Opacity = 0;
                Arguments.Opacity = 0;
                args.Opacity = 0;
                if ((bool)(CollapseCB.IsChecked))
                {
                    VerSTR.Text = json.Version + " - " + json.LauncherString;
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
                    UncolHome.Opacity = 0;
                    UncolChangelog.Opacity = 0;
                    UncolBugs.Opacity = 0;
                    UncolHome.Margin = new Thickness(69, 69, 69, 69);
                    UncolChangelog.Margin = new Thickness(69, 69, 69, 69);
                    UncolBugs.Margin = new Thickness(69, 69, 69, 69);
                    ColHomeButton.IsEnabled = true;
                    ColChangelogButton.IsEnabled = true;
                    ColBugsButton.IsEnabled = false;
                    ChangelogBugsTitle.Text = "Bugs";
                }
            }
            catch
            {
                MessageBox.Show("A fatal error occurred while opening the Avery Game launcher. Please make sure you are connected to the internet and try again. If the problem persists, report the issue and when it is occurring in #bug-reports in the Avery Game discord server.", "Fatal error!", MessageBoxButton.OK, MessageBoxImage.Stop);
                this.Close();
            }
        }
        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            string DATA = File.ReadAllText(filepath + "\\AveryGame Launcher\\fuck.json");
            LauncherCloud json = JsonConvert.DeserializeObject<LauncherCloud>(DATA);
            ChangelogLine1.Text = json.BalanceCL1 + json.ChangelogLine1;
            ChangelogLine2.Text = json.BalanceCL2 + json.ChangelogLine2;
            ChangelogLine3.Text = json.BalanceCL3 + json.ChangelogLine3;
            ChangelogLine4.Text = json.BalanceCL4 + json.ChangelogLine4;
            ChangelogLine5.Text = json.BalanceCL5 + json.ChangelogLine5;
            BugsLine1.Text = json.BalanceBL1 + json.BugLine1;
            BugsLine2.Text = json.BalanceBL2 + json.BugLine2;
            BugsLine3.Text = json.BalanceBL3 + json.BugLine3;
            BugsLine4.Text = json.BalanceBL4 + json.BugLine4;
            BugsLine5.Text = json.BalanceBL5 + json.BugLine5;
            VerSTR.Text = "             " + json.Version + " - " + json.LauncherString;
            ChangelogButton.IsEnabled = true;
            HomeButton.IsEnabled = true;
            LaunchButton.IsEnabled = false;
            BugsButton.IsEnabled = true;
            SettingsButton.IsEnabled = false;
            ColSettings.IsEnabled = false;
            LaunchButton.Opacity = 0;
            AGSLogo.Opacity = 0;
            ChangelogBugsTitle.Opacity = 0;
            ChangelogBugsTitle.Text = "";
            BugsButton.Content = "Bugs";
            HomeButton.Content = "Home";
            ChangelogButton.Content = "Changelog";
            SettingsButton.Content = "• Settings";
            VerSTR.Opacity = 0;
            WelcomeTextCol.Opacity = 0;
            LauncherInfoTextCol.Opacity = 0;
            WelcomeText.Opacity = 0;
            LauncherInfoText.Opacity = 0;
            LaunchButton.Visibility = Visibility.Hidden;
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
            HideMenu.Opacity = 1;
            CollapseCB.Opacity = 1;
            Notice.Opacity = 1;
            Arguments.Opacity = 1;
            args.Opacity = 1;
            if ((bool)(CollapseCB.IsChecked))
            {
                VerSTR.Text = json.Version + " - " + json.LauncherString;
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
                UncolHome.Opacity = 0;
                UncolChangelog.Opacity = 0;
                UncolBugs.Opacity = 0;
                UncolHome.Margin = new Thickness(69, 69, 69, 69);
                UncolChangelog.Margin = new Thickness(69, 69, 69, 69);
                UncolBugs.Margin = new Thickness(69, 69, 69, 69);
                ColHomeButton.IsEnabled = true;
                ColChangelogButton.IsEnabled = true;
                ColBugsButton.IsEnabled = true;
                SettingsButton.IsEnabled = false;
                ChangelogBugsTitle.Text = "Bugs";
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
            if (dprog.Opacity == 1)
            {
                MessageBoxResult result = MessageBox.Show("AveryGame is not done downloading, and exiting will corrupt the download. Are you sure?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    this.Close();
                }
                else
                {

                }
            }
            else
            {
                File.Delete(filepath + "\\AveryGame Launcher\\" + @"fuck.json");
                File.WriteAllText(@filepath + "\\AveryGame Launcher\\Clientsettings.json", "{ \"CollapseMenu\":\"" + CollapseCB.IsChecked + "\" }");
                this.Close();
            }
        }

        private void testbutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dprog.Opacity == 0)
                {
                    WebClient webclient = new WebClient();
                    string DATA = webclient.DownloadString("https://raw.githubusercontent.com/imstillamazedbyit/1q29dks43895r794/main/launcherinfo.json");
                    LauncherCloud json = JsonConvert.DeserializeObject<LauncherCloud>(DATA);
                    if (!File.Exists(System.IO.Path.Combine(json.VersionFolderInt + "/4.2/WindowsNoEditor/AveryGame.exe")))
                    {
                        MessageBoxResult result;
                        result = MessageBox.Show("Avery Game was not found! Would you like to install?", "Error", MessageBoxButton.YesNo, MessageBoxImage.Error);
                        if (result == MessageBoxResult.Yes)
                        {
                            LaunchButton.Content = "Downloading AGS...";
                            kys();
                        }
                        else
                        {
                            MessageBox.Show("Avery Game was not found on your system. Please restart the launcher or select a custom path.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        string latest = File.ReadAllText(filepath + "\\AveryGame Launcher\\" + @"gclient.json");
                        if (latest.Contains(json.VersionInt))
                        {
                            try
                            {
                                var p = new System.Diagnostics.Process();
                                p.StartInfo.FileName = Path.Combine(json.VersionInt + "/4.2/WindowsNoEditor/AveryGame.exe");
                                p.StartInfo.Arguments = args.Text;
                                p.Start();
                                new ToastContentBuilder()
                                    .AddArgument("action", "viewConversation")
                                    .AddArgument("conversationId", 9813)
                                    .AddText("Avery Game")
                                    .AddText("Game launched")
                                .Show();
                            }
                            catch
                            {
                                MessageBox.Show("A fatal error occurred while launching AveryGame!", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        if (!latest.Contains(json.VersionInt))
                        {
                            MessageBoxResult updateAvail = MessageBox.Show("AveryGame has an update. Would you like to install it now?", "Notice", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (updateAvail == MessageBoxResult.Yes)
                            {
                                LaunchButton.Content = "Downloading AGS...";
                                kys();
                            }
                            if (updateAvail == MessageBoxResult.No)
                            {
                                var p = new System.Diagnostics.Process();
                                p.StartInfo.FileName = Path.Combine(json.VersionInt + "/4.2/WindowsNoEditor/AveryGame.exe");
                                p.StartInfo.Arguments = args.Text;
                                p.Start();
                            }
                        }
                    }
                }
                else
                {

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
                File.WriteAllText(filepath + "\\AveryGame Launcher\\" + @"gclient.json", json.VersionInt);
                new ToastContentBuilder()
                    .AddArgument("action", "viewConversation")
                    .AddArgument("conversationId", 9813)
                    .AddText("Avery Game downloading status")
                    .AddText("Avery Game has finished downloading!")
                    .Show();
                DownloadProgPercent.Opacity = 0;
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
                dprog.Opacity = 1;
                webclient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadGameCompletedCallback);
                webclient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webclientDownloadProgressChanged);
                DownloadProgPercent.Opacity = 1;
                webclient.DownloadFileAsync(new Uri("https://www.googleapis.com/drive/v3/files/1i9qQNqWOlQcdrZ0qD3NU7WzHKW4h54U_?alt=media&key=AIzaSyD3hsuSxEFnxZkgadbUSPt_iyx8qJ4lwWQ"), gzip);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error finishing download: {ex}");
            }
        }
        public void webclientDownloadProgressChanged(Object sender, DownloadProgressChangedEventArgs e)
        {
            DownloadProgPercent.Text = e.ProgressPercentage.ToString() + "%";
            dprog.Value = e.ProgressPercentage;
        }
    }
}