//AveryGame Launcher Copyright (C) 2022, Avery Fiebig-Dorey & Tristan Gee

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
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs");
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGINIT] [" + DateTime.Now.ToString() + "]: Log service started" + Environment.NewLine);
            }
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt"))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt");
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGINIT] [" + DateTime.Now.ToString() + "]: Log service started" + Environment.NewLine);
            }
        }
        public static void LogWindowInit()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: MainWindow initialized" + Environment.NewLine);
        }
        public static void LogRPC()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGRPC] [" + DateTime.Now.ToString() + "]: RPC initialized" + Environment.NewLine);
        }
        public static void LogJSEvent()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGJS] [" + DateTime.Now.ToString() + "]: Started JS logging worker" + Environment.NewLine);
        }
        public static void LogJSDownload()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGJS] [" + DateTime.Now.ToString() + "]: Downloaded JSON data from GitHub repository" + Environment.NewLine);
        }
        public static void LogJSRead()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGJS] [" + DateTime.Now.ToString() + "]: Read JSON strings from " + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\fuck.json" + Environment.NewLine);
        }
        public static void LogJSSet()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGJS] [" + DateTime.Now.ToString() + "]: Set version strings with json information" + Environment.NewLine);
        }
        public static void LogJSDelete()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGJS] [" + DateTime.Now.ToString() + "]: Deleted JSON strings at " + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\fuck.json" + Environment.NewLine);
        }
        public static void LogCMenuState()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: Wrote collapsed menu setting to JSON" + Environment.NewLine);
        }
        public static void LogFatalErr()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: Fatal error occurred, logging..." + Environment.NewLine);
        }
        public static void LogUpdaterDownload()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: Launcher is out of date, downloading updater" + Environment.NewLine);
        }
        public static void LogUpdaterExtract()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: Extracted launcher updater" + Environment.NewLine);
        }
        public static void LogUpdaterStart()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: Starting launcher updater" + Environment.NewLine);
        }
        public static void LogDrag()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: Window drag bar clicked" + Environment.NewLine);
        }
        public static void LogLauncherShutdown()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: Launcher shutting down" + Environment.NewLine);
        }
        public static void LogToastNotif()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: Showed toast notification" + Environment.NewLine);
        }
        public static void LogCorruptZipDelete()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: Corrupt game zip deleted" + Environment.NewLine);
        }
        public static void LogFTSCheckFalse()
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: Not FTS. Running Killswitch check." + Environment.NewLine);
        }
        internal class BtnLogic
        {
            public static void LogHomeBTNClick()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN] [" + DateTime.Now.ToString() + "]: Home button clicked" + Environment.NewLine);
            }
            public static void LogChangelogBTNClick()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN] [" + DateTime.Now.ToString() + "]: Changelog button clicked" + Environment.NewLine);
            }
            public static void LogBugBTNClick()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN] [" + DateTime.Now.ToString() + "]: Bugs button clicked" + Environment.NewLine);
            }
            public static void LogNewsBTNClick()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN] [" + DateTime.Now.ToString() + "]: News button clicked" + Environment.NewLine);
            }
            public static void LogAccountBTNClick()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN] [" + DateTime.Now.ToString() + "]: Account button clicked" + Environment.NewLine);
            }
            public static void LogAccountPageNotReady()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: Account page not ready" + Environment.NewLine + "[LOGLAUNCHER]: Replaced account button logic with settings button logic" + Environment.NewLine);
            }
            public static void LogSettingBTNClick()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN] [" + DateTime.Now.ToString() + "]: Settings button clicked" + Environment.NewLine);
            }
            public static void LogMinimizeBTNClick()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN] [" + DateTime.Now.ToString() + "]: Minimize window button clicked" + Environment.NewLine);
            }
            public static void LogCloseBTNClick()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN] [" + DateTime.Now.ToString() + "]: Close window button clicked" + Environment.NewLine);
            }
            public static void LogColElements()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN] [" + DateTime.Now.ToString() + "]: Set collapsed menu elements" + Environment.NewLine);
            }
            public static void LogUncolElements()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN] [" + DateTime.Now.ToString() + "]: Set uncollapsed menu elements" + Environment.NewLine);
            }
            public static void LogOpacityChange()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN] [" + DateTime.Now.ToString() + "]: Set object opacities" + Environment.NewLine);
            }
            public class CloseBTNEvents
            {
                public static void LogUserConfirm()
                {
                    File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: User confirmed to interrupt download" + Environment.NewLine);
                }
                public static void LogUserDeny()
                {
                    File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: User confirmed to interrupt download" + Environment.NewLine);
                }
                public static void LogCloseInterrupt()
                {
                    File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: User confirmed to interrupt download" + Environment.NewLine);
                }
            }
            public class PlayBTNEvents
            {
                public static void LogBTNClick()
                {
                    File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGBTN] [" + DateTime.Now.ToString() + "]: Play button clicked" + Environment.NewLine);
                }
                public static void LogGameNonexistent()
                {
                    File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: AveryGame not found on system" + Environment.NewLine);
                }
                public static void LogUserConfirm()
                {
                    File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: User confirmed to download AveryGame" + Environment.NewLine);
                }
                public static void LogUserDeny()
                {
                    File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: User denied to download AveryGame" + Environment.NewLine);
                }
                public static void LogButtonContentChange()
                {
                    File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: Play button content changed" + Environment.NewLine);
                }
                public static void LogDownloadWorker()
                {
                    File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: Starting AveryGame download worker" + Environment.NewLine);
                }
                public static void LogGameStart()
                {
                    File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: Game is up to date, launching game" + Environment.NewLine);
                }
                public class Update
                {
                    public static void LogUpdateNotice()
                    {
                        File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: Game is not up to date, asked user if they would like to update" + Environment.NewLine);
                    }
                    public static void LogUserConfirm()
                    {
                        File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: User confirmed game update" + Environment.NewLine);
                    }
                    public static void LogUserDeny()
                    {
                        File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: User denied to update game, launching" + Environment.NewLine);
                    }
                }
                public class Download
                {
                    public static void LogDownloadStart()
                    {
                        File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGLAUNCHER] [" + DateTime.Now.ToString() + "]: Set download progress percent opacity, set webclient download callback and progress changed" + Environment.NewLine);
                    }
                    public class DownloadCallbackEvents
                    {
                        public static void LogDownloadComplete()
                        {
                            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGEVENT] [" + DateTime.Now.ToString() + "]: Game completed, running extraction + completion code..." + Environment.NewLine);
                        }
                        public static void LogExtract()
                        {
                            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGEVENT] [" + DateTime.Now.ToString() + "]: Extracting game to folder" + Environment.NewLine);
                        }
                        public static void LogZipDelete()
                        {
                            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGEVENT] [" + DateTime.Now.ToString() + "]: Delete unneeded game zip" + Environment.NewLine);
                        }
                        public static void LogDownloadCleanup()
                        {
                            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGEVENT] [" + DateTime.Now.ToString() + "]: Set progress bar opacity and launch button text" + Environment.NewLine);
                        }
                        public static void LogWriteVersionString()
                        {
                            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGEVENT] [" + DateTime.Now.ToString() + "]: Wrote version string" + Environment.NewLine);
                        }
                    }
                }
            }
        }
        internal class TitaniumSVC
        {
            public static void what()
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\Logs\\LogV2.txt", "[LOGEVENT] [" + DateTime.Now.ToString() + "]: what" + Environment.NewLine);
            }
        }
    }
}
