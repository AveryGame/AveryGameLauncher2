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
using static AgsLauncherV2.HomePage;

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
				State = "Testing RPC",
				Assets = new Assets()
				{
					LargeImageKey = "image_large",
					LargeImageText = "fuck off",
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

                if (client.CurrentUser.Username == "12᲼")
                {
                    WelcomeRPCLabel.Content = "Hi, kianna!";
                }
                if (client.CurrentUser.Username == "artemis_terrain")
                {
                    WelcomeRPCLabel.Content = "Hi, avery!";
                }
                if (client.CurrentUser.Username == "Crunnie")
                {
                    WelcomeRPCLabel.Content = "Hi, crunchy!";
                }
            }
            catch
            {
                WelcomeRPCLabel.Content = "Welcome to the AGS Launcher!";
            }
            pfp.Source = new BitmapImage(new Uri(client.CurrentUser.GetAvatarURL(User.AvatarFormat.PNG)));
        }
        private void Home(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("home button clicked");
            
        }
        private void Changelog(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("changelog button clicked");
        }
        private void DragBar(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void Bugs(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("bugs button clicked");
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
            this.Close();
        }
    }
}
