using System;
using System.IO;

namespace AgsLauncherV2.Services
{
    internal class LogSVC
    {
        public static void CreateLogFile()
        {
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt"))
            {
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "Log at " + DateTime.Now);
            }
            else if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt"))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt");
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGINIT]: Log service started at " + DateTime.Now + Environment.NewLine);
            }
        }
        public static void LogWindowInit()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: MainWindow initialized" + Environment.NewLine);
        }
        public static void LogRPC()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGRPC]: RPC initialized" + Environment.NewLine);
        }
        public static void LogJSEvent()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGJS]: Started JS logging worker" + Environment.NewLine);
        }
        public static void LogJSDownload()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGJS]: Downloaded JSON data from GitHub repository" + Environment.NewLine);
        }
        public static void LogJSRead()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGJS]: Read JSON strings from " + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\fuck.json" + Environment.NewLine);
        }
        public static void LogJSSet()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGJS]: Set version strings with json information" + Environment.NewLine);
        }
        public static void LogJSDelete()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGJS]: Deleted JSON strings at " + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\fuck.json" + Environment.NewLine);
        }
        public static void LogCMenuState()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: Wrote collapsed menu setting to JSON" + Environment.NewLine);
        }
        public static void LogFatalErr()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: Fatal error occurred, logging..." + Environment.NewLine);
        }
        public static void LogUpdaterDownload()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: Launcher is out of date, downloading updater" + Environment.NewLine);
        }
        public static void LogUpdaterExtract()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: Extracted launcher updater" + Environment.NewLine);
        }
        public static void LogUpdaterStart()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: Starting launcher updater" + Environment.NewLine);
        }
        public static void LogDrag()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: Window drag bar clicked" + Environment.NewLine);
        }
        public static void LogLauncherShutdown()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: Launcher shutting down" + Environment.NewLine);
        }
        public static void LogToastNotif()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: Showed toast notification" + Environment.NewLine);
        }
        public static void LogCorruptZipDelete()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: Corrupt game zip deleted" + Environment.NewLine);
        }
        internal class BtnLogic
        {
            public static void LogHomeBTNClick()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN]: Home button clicked" + Environment.NewLine);
            }
            public static void LogChangelogBTNClick()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN]: Changelog button clicked" + Environment.NewLine);
            }
            public static void LogBugBTNClick()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN]: Bugs button clicked" + Environment.NewLine);
            }
            public static void LogNewsBTNClick()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN]: News button clicked" + Environment.NewLine);
            }
            public static void LogAccountBTNClick()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN]: Account button clicked" + Environment.NewLine);
            }
            public static void LogAccountPageNotReady()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: Account page not ready" + Environment.NewLine + "[LOGLAUNCHER]: Replaced account button logic with settings button logic" + Environment.NewLine);
            }
            public static void LogSettingBTNClick()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN]: Settings button clicked" + Environment.NewLine);
            }
            public static void LogMinimizeBTNClick()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN]: Minimize window button clicked" + Environment.NewLine);
            }
            public static void LogCloseBTNClick()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN]: Close window button clicked" + Environment.NewLine);
            }
            public static void LogColElements()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN]: Set collapsed menu elements" + Environment.NewLine);
            }
            public static void LogUncolElements()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN]: Set uncollapsed menu elements" + Environment.NewLine);
            }
            public static void LogOpacityChange()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN]: Set object opacities" + Environment.NewLine);
            }
            public class CloseBTNEvents
            {
                public static void LogUserConfirm()
                {
                    File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: User confirmed to interrupt download" + Environment.NewLine);
                }
                public static void LogUserDeny()
                {
                    File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: User confirmed to interrupt download" + Environment.NewLine);
                }
                public static void LogCloseInterrupt()
                {
                    File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: User confirmed to interrupt download" + Environment.NewLine);
                }
            }
            public class PlayBTNEvents
            {
                public static void LogBTNClick()
                {
                    File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN]: Play button clicked" + Environment.NewLine);
                }
                public static void LogGameNonexistent()
                {
                    File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: AveryGame not found on system" + Environment.NewLine);
                }
                public static void LogUserConfirm()
                {
                    File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: User confirmed to download AveryGame" + Environment.NewLine);
                }
                public static void LogUserDeny()
                {
                    File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: User denied to download AveryGame" + Environment.NewLine);
                }
                public static void LogButtonContentChange()
                {
                    File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: Play button content changed" + Environment.NewLine);
                }
                public static void LogDownloadWorker()
                {
                    File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: Starting AveryGame download worker" + Environment.NewLine);
                }
                public static void LogGameStart()
                {
                    File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: Game is up to date, launching game" + Environment.NewLine);
                }
                public class Update
                {
                    public static void LogUpdateNotice()
                    {
                        File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: Game is not up to date, asked user if they would like to update" + Environment.NewLine);
                    }
                    public static void LogUserConfirm()
                    {
                        File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: User confirmed game update" + Environment.NewLine);
                    }
                    public static void LogUserDeny()
                    {
                        File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: User denied to update game, launching" + Environment.NewLine);
                    }
                }
                public class Download
                {
                    public static void LogDownloadStart()
                    {
                        File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER]: Set download progress percent opacity, set webclient download callback and progress changed" + Environment.NewLine);
                    }
                    public class DownloadCallbackEvents
                    {
                        public static void LogDownloadComplete()
                        {
                            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGEVENT]: Game completed, running extraction + completion code..." + Environment.NewLine);
                        }
                        public static void LogExtract()
                        {
                            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGEVENT]: Extracting game to folder" + Environment.NewLine);
                        }
                        public static void LogZipDelete()
                        {
                            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGEVENT]: Delete unneeded game zip" + Environment.NewLine);
                        }
                        public static void LogDownloadCleanup()
                        {
                            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGEVENT]: Set progress bar opacity and launch button text" + Environment.NewLine);
                        }
                        public static void LogWriteVersionString()
                        {

                        }
                    }
                }
            }
        }
        internal class TitaniumSVC
        {
            public static void what()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGEVENT]: what" + Environment.NewLine);
            }
        }
    }
}
