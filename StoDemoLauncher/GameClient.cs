using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using StoDemoLauncher.Model;
using System.IO;
using StoDemoLauncher.Helper;
using Microsoft.Win32;

namespace StoDemoLauncher
{
    /// <summary>
    /// STO servers
    /// </summary>
    public enum GameServer
    {
        HOLODECK,
        TRIBBLE,
        REDSHIRT,
        EXTERN
    }

    /// <summary>
    /// The valid file types for STO screen shots
    /// </summary>
    public enum ImageFileExtension
    {
        JPG,
        TGA
    }

    /// <summary>
    /// Record for render command-line options
    /// </summary>
    public class RenderSettings
    {
        public string Prefix { get; set; }
        public ImageFileExtension ImageFileFormat { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int PostProcessing { get; set; }
        public int HighQualityDepthOfField { get; set; }
        public int ReduceMipMaps { get; set; }
        public int DontReduceTextures { get; set; }
        public int VisScale { get; set; }
        public int Shadows { get; set; }
        public int BloomQuality { get; set; }
        public int ShadowMapSize { get; set; }
        public int RenderScale { get; set; }
        public int ScreenshotAfterRenderScale { get; set; }
        public int UiToggleHud { get; set; }
        public int Fov { get; set; }
        public int ShowChatBubbles { get; set; }
        public int ShowFloatingText { get; set; }
        public int ShowEntityUi { get; set; }
        public int ShowFps { get; set; }
        public int ShowLessAnnoyingAccessLevelWarning { get; set; }
        public int ChangeSize { get; set; }
    }

    /// <summary>
    /// Abstraction of the STO installation on the user's machine. Contains a
    /// lot of convineance methods.
    /// </summary>
    public class GameClient
    {
        // begin config constants
        public const string GameClientIniGroup = "GameClient";
        public const string PathIniKey = "path";
        public const string HolodeckPathIniKey = "holodeckPath";
        public const string TribblePathIniKey = "tribblePath";
        public const string RedshirtPathIniKey = "redshirtPath";
        public const string StoDemoLauncherIniGroup = "StoDemoLauncher";
        public const string VersionIniKey = "version";

        // end config constants

        /// <summary>
        /// The absolute path to the directory containing "Star Trek Online.exe"
        /// </summary>
        private string installLocation;

        /// <summary>
        /// The absolute path to the directory containing "GameClient.exe" for Holodeck
        /// </summary>
        private string holodeckPath;

        /// <summary>
        /// The absolute path to the directory containing "GameClient.exe" for Tribble
        /// </summary>
        private string tribblePath;

        /// <summary>
        /// The absolute path to the directory containing "GameClient.exe" for Redshirt
        /// </summary>
        private string redshirtPath;

        /// <summary>
        /// Indicates, if there is a playtest directory
        /// </summary>
        private bool tribbleExists = false;

        /// <summary>
        /// Indicates, if there is a playtest directory
        /// </summary>
        private bool redshirtExists = false;

        // begin properties

        /// <summary>
        /// The absolute path to the directory containing "Star Trek Online.exe"
        /// </summary>
        public string InstallLocation
        {
            get
            {
                return this.installLocation;
            }
        }

        /// <summary>
        /// The absolute path to the directory containing "GameClient.exe" for Holodeck
        /// </summary>
        public string HolodeckPath
        {
            get
            {
                return this.holodeckPath;
            }
        }

        /// <summary>
        /// The absolute path to the directory containing "GameClient.exe" for Tribble
        /// </summary>
        public string TribblePath
        {
            get
            {
                return this.tribblePath;
            }
        }

        /// <summary>
        /// The absolute path to the directory containing "GameClient.exe" for Redshirt
        /// </summary>
        public string RedshirtPath
        {
            get
            {
                return this.redshirtPath;
            }
        }

        /// <summary>
        /// Indicates, if there is a playtest directory
        /// </summary>
        public bool TribbleExists
        {
            get
            {
                return this.tribbleExists;
            }
        }

        /// <summary>
        /// Indicates, if there is a playtest directory
        /// </summary>
        public bool RedshirtExists
        {
            get
            {
                return this.redshirtExists;
            }
        }
        // end properties

// begin constructor
        /// <summary>
        /// Creates a new game client object
        /// </summary>
        /// <param name="installLocation">The absolute path to the directory
        /// containing "Star Trek Online.exe"</param>
        public GameClient(string installLocation)
        {
            this.UpdatePaths(installLocation);
        }
// end constructor

        /// <summary>
        /// Looks for the Star Trek Online installation location (the
        /// directory containing "Star Trek Online.exe"). If the location
        /// cannot automatically be retrieved from the tools config.ini file 
        /// we will continue searching the registry. If we can't find it there
        /// either the user is asked to browse and select
        /// "Star Trek Online.exe". If the user does not provide the correct
        /// file or cancels the dialog, the empty string is returned.
        /// </summary>
        /// <returns>The absolut path to the Star Trek Online installation
        /// location or the empty string, if all attemts to find it have
        /// failed.</returns>
        static public string FindStoDirectory()
        {
            string stoInstallLocation = "";

            // try to fetch path from config file
            ConfigurationFile config = ConfigurationFile.GetInstance();

            if (config.Contains(GameClientIniGroup, PathIniKey))
            {
                stoInstallLocation = config.GetStringValue(GameClientIniGroup, PathIniKey);
                if (GameClient.ValidateStoInstallationPath(stoInstallLocation))
                {
                    return stoInstallLocation;
                }
                else
                {
                    MessageBox.Show(
                    "The Star Trek Online installation path from the config.ini\n" +
                    "is invalid. Trying to read installation directory from\n" +
                    "registry.",
                    "Invalid Path In config.ini",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                    config.Remove(GameClientIniGroup, PathIniKey);
                    config.Remove(GameClientIniGroup, HolodeckPathIniKey);
                    config.Remove(GameClientIniGroup, TribblePathIniKey);
                    config.Remove(GameClientIniGroup, RedshirtPathIniKey);
                    config.Save();
                }
            }

            // next try to get info from registry
            RegistryKey pathKey = Registry.CurrentUser;

            if (pathKey != null)
            {
                pathKey = pathKey.OpenSubKey("SOFTWARE");
            }
            if (pathKey != null)
            {
                pathKey = pathKey.OpenSubKey("Cryptic");
            }
            if (pathKey != null)
            {
                pathKey = pathKey.OpenSubKey("Star Trek Online");
            }
            if (pathKey != null && pathKey.GetValue("InstallLocation") != null)
            {
                stoInstallLocation = pathKey.GetValue("InstallLocation").ToString();
                pathKey.Close();
            }
            else
            {
                DialogResult warningResult = MessageBox.Show(
                    "Star Trek Online could not be found automatically.\n" +
                    "This problem may occur, if the game was not installed\n" +
                    "using the setup, or the game was installed using a\n" +
                    "different account.\n" +
                    "If you want to continue, please select the\n" +
                    "\"Star Trek Online.exe\" from your installation directory\n" +
                    "in the following dialog.",
                    "Could Not Find Star Trek Online",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);
                if (warningResult == DialogResult.OK)
                {
                    bool correctPath = false;
                    while (!correctPath)
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Filter = "Star Trek Online.exe|Star Trek Online.exe";
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            stoInstallLocation = openFileDialog.FileName.Substring(0, openFileDialog.FileName.LastIndexOf("\\"));
                            if (GameClient.ValidateStoInstallationPath(stoInstallLocation))
                            {
                                correctPath = true;
                                pathKey = Registry.CurrentUser.CreateSubKey("SOFTWARE").CreateSubKey("Cryptic").CreateSubKey("Star Trek Online");
                                pathKey.SetValue("InstallLocation", stoInstallLocation);
                            }
                        }
                        if (!correctPath)
                        {
                            DialogResult preLaunchExitResult = MessageBox.Show(
                                "No valid installation directory.\n" +
                                "You have not selected a valid installation\n" +
                                "directory for Star Trek Online.\n" +
                                "Try again or exit the Demo Launcher.",
                                "Could Not Find Star Trek Online",
                                MessageBoxButtons.RetryCancel,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1);
                            if (preLaunchExitResult == DialogResult.Cancel)
                            {
                                return "";
                            }
                        }
                    }
                }
            }

            // Save config file
            config.PutValue(GameClientIniGroup, PathIniKey, stoInstallLocation);
            config.Save();

            return stoInstallLocation;
        }

        /// <summary>
        /// Checks the given directory for a "Star Trek Online.exe" and a
        /// Holodeck server installation.
        /// </summary>
        /// <param name="path">Path to validate</param>
        /// <returns>True if a Star Trek Online.exe and Holodeck installation
        /// were found in the given folder</returns>
        static public bool ValidateStoInstallationPath(string path)
        {
            return System.IO.File.Exists(path + "\\Star Trek Online.exe") && GameClient.ValidateServerPath(path + "\\Star Trek Online\\Live\\x64");
        }

        /// <summary>
        /// Checks the given directory for a GameClient.exe
        /// </summary>
        /// <param name="path">Path to be validated as server installation</param>
        /// <returns>True if a GameCleint.exe is found in the installation
        /// directory, else otherwise</returns>
        static public bool ValidateServerPath(string path)
        {
            return System.IO.File.Exists(path + "\\GameClient.exe");
        }

        /// <summary>
        /// Forces game client to check for changed paths
        /// </summary>
        public void UpdatePaths(string stoInstallationPath)
        {
            this.installLocation = stoInstallationPath;

            // fetch holodeck directory from config file
            ConfigurationFile config = ConfigurationFile.GetInstance();
            if (!config.Contains(GameClientIniGroup, HolodeckPathIniKey))
            {
                config.PutValue(GameClientIniGroup, HolodeckPathIniKey, installLocation + "\\Star Trek Online\\Live");
            }
            this.holodeckPath = config.GetStringValue(GameClientIniGroup, HolodeckPathIniKey);

            // fetch tribble directory from tribble file
            if (!config.Contains(GameClientIniGroup, TribblePathIniKey))
            {
                config.PutValue(GameClientIniGroup, TribblePathIniKey, installLocation + "\\Star Trek Online\\Playtest");
            }
            this.tribblePath = config.GetStringValue(GameClientIniGroup, TribblePathIniKey);

            this.tribbleExists = System.IO.Directory.Exists(this.tribblePath);

            // fetch redshirt directory from tribble file
            if (!config.Contains(GameClientIniGroup, RedshirtPathIniKey))
            {
                config.PutValue(GameClientIniGroup, RedshirtPathIniKey, installLocation + "\\Star Trek Online\\Beta");
            }
            this.redshirtPath = config.GetStringValue(GameClientIniGroup, RedshirtPathIniKey);

            this.redshirtExists = System.IO.Directory.Exists(this.redshirtPath);
        }

        /// <summary>
        /// Tests if directories have changed
        /// </summary>
        public void Refresh()
        {
            this.tribbleExists = System.IO.Directory.Exists(this.tribblePath);
            this.redshirtExists = System.IO.Directory.Exists(this.redshirtPath);
        }

        /// <summary>
        /// Opens a folder in the OS file browser
        /// </summary>
        /// <param name="path">the absolute path to open</param>
        /// <returns>True if path exists and the file browser could be started, false otherwise</returns>
        private bool OpenFolder(string path)
        {
            if (System.IO.Directory.Exists(path))
            {
                System.Diagnostics.Process.Start(path);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the drive letter of the STO installation
        /// </summary>
        /// <returns>The STO installation drive, e.g., "C:\"</returns>
        public string GetDrive()
        {
            return this.holodeckPath.Substring(0, this.holodeckPath.IndexOf("\\") + 1);
        }
        
        /// <summary>
        /// Returns the path to the directory containing the "GameClient.exe"
        /// for a given target server
        /// </summary>
        /// <param name="server">The target server</param>
        /// <returns></returns>
        public string GetPath(GameServer server)
        {
            if (server == GameServer.HOLODECK) return this.HolodeckPath;
            else if (server == GameServer.TRIBBLE) return this.TribblePath;
            else if (server == GameServer.REDSHIRT) return this.RedshirtPath;
            else if (server == GameServer.EXTERN) return this.HolodeckPath;
            return "";
        }

        /// <summary>
        /// Returns the user friendly name of a given target server
        /// </summary>
        /// <param name="server">The target server</param>
        /// <returns></returns>
        public static string GetServerName(GameServer server)
        {
            if (server == GameServer.HOLODECK) return "Holodeck";
            else if (server == GameServer.TRIBBLE) return "Tribble";
            else if (server == GameServer.REDSHIRT) return "Redshirt";
            else if (server == GameServer.EXTERN) return "Unknown (will run on Holodeck)";
            return "";
        }

        /// <summary>
        /// Returns a human friendly string of an image file extension
        /// </summary>
        /// <param name="ext">The extension to convert to a string</param>
        /// <returns>The extension string of the given extension</returns>
        public static string GetImageFileExtension(ImageFileExtension ext)
        {
            if (ext == ImageFileExtension.TGA) return "tga";
            return "jpg";
        }

        /// <summary>
        /// Returns the path to the "demos" folder of a given installation
        /// </summary>
        /// <param name="server">Server of the installation to fetch the demos
        /// folder for</param>
        /// <returns>Absolute path to "demos" folder</returns>
        public string GetDemosPath(GameServer server)
        {
            return this.GetPath(server) + "\\demos";
        }

        /// <summary>
        /// Returns the path to the "screenshots" folder of a given installation
        /// </summary>
        /// <param name="server">Server of the installation to fetch the screenshots
        /// folder for</param>
        /// <returns>Absolute path to "screenshots" folder</returns>
        public string GetScreenshotsPath(GameServer server)
        {
            return this.GetPath(server) + "\\screenshots";
        }

        /// <summary>
        /// Tries to open the folder containing the demos for a given server.
        /// If there is no folder, a warning is displayed.
        /// </summary>
        /// <param name="server">Installation for which to open folder</param>
        public void OpenDemosFolder(GameServer server)
        {
            if (!this.OpenFolder(this.GetDemosPath(server)))
            {
                MessageBox.Show("There is no demos folder for " + GameClient.GetServerName(server) + ".",
                    "No Demos Folder Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Tries to open the folder containing the demos for a given server.
        /// If there is no folder, a warning is displayed.
        /// </summary>
        /// <param name="server">Installation for which to open folder</param>
        public void OpenScreenshotsFolder(GameServer server)
        {
            if (!this.OpenFolder(this.GetScreenshotsPath(server)))
            {
                MessageBox.Show("There is no screen shots folder for " + GameClient.GetServerName(server) + ".",
                    "No Screen Shots Folder Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Returns the absolute path to the GameClient.exe of a given
        /// installation
        /// </summary>
        /// <param name="server">The installation for which to get the client
        /// path</param>
        /// <returns>The absolute path to the GameClient.exe of that
        /// installation</returns>
        private string GetGameClientPath(GameServer server)
        {
            return this.GetPath(server) + "\\x64\\GameClient.exe";
        }

        /// <summary>
        /// Opens "Star Trek Online" in "demo_play" mode with the given demo.
        /// </summary>
        /// <param name="demo">The demo to be played</param>
        public void PlayDemo(DemoInfo demo)
        {
            // Intercept missing demo file
            if (this.DemoFileExists(demo))
            {
                this.RunDemo(demo.Server, "-demo_play " + demo.FileName);
            }
            // demo file does not exist --> stop and warn the user
            else
            {
                ShowMissingDemoFileMessage();
            }
        }


        public void PlayDemoNoUI(DemoInfo demo)
        {
            // Intercept missing demo file
            if (this.DemoFileExists(demo))
            {
                this.RunDemo(demo.Server, "-demo_play " + demo.FileName + " -demo_hide_ui");
            }
            // demo file does not exist --> stop and warn the user
            else
            {
                ShowMissingDemoFileMessage();
            }
        }

        /// <summary>
        /// Opens "Star Trek Online" in "demo_save_images" mode with the given
        /// demo.
        /// </summary>
        /// <param name="demo">The demo to be rendered.</param>
        /// <param name="prefix">The image sequence file prefix</param>
        /// <param name="ext">Imgae file extension</param>
        public void RenderDemo(DemoInfo demo, RenderSettings settings)
        {
            // Intercept missing demo file
            if (this.DemoFileExists(demo))
            {
                this.RunDemo(demo.Server, "-demo_save_images " + demo.FileName + " " + settings.Prefix + " " + GameClient.GetImageFileExtension(settings.ImageFileFormat) +
                    (settings.ChangeSize == 1 ?
                    " -windowedsize " + settings.Width + " " + settings.Height +
                    " -rendersize " + settings.Width + " " + settings.Height :
                    "") +
                    " -renderscale " + settings.RenderScale +
//                    " -fov " + settings.Fov +
                    " -screenshotAfterRenderscale " + settings.ScreenshotAfterRenderScale +
                    " -disableTexReduce " + settings.DontReduceTextures +
                    " -visscale " + settings.VisScale +
                    " -reduce_mip " + settings.ReduceMipMaps +
                    " –highQualityDOF " + settings.HighQualityDepthOfField +
                    " -postprocessing " + settings.PostProcessing +
                    " -shadows " + settings.Shadows +
//                    " -ui_ToggleHUD " + settings.UiToggleHud +
//                    " -ShowChatBubbles " + settings.ShowChatBubbles +
//                    " -ShowFloatingText " + settings.ShowFloatingText +
//                    " -ShowEntityUI " + settings.ShowEntityUi +
                    " -pssmShadowMapSize " + settings.ShadowMapSize +
//                    " -pssmNearShadowRes 64" + // What is that?
                    " -bloomquality " + settings.BloomQuality +
                    " -showfps " + settings.ShowFps +
                    " -ShowLessAnnoyingAccessLevelWarning " + settings.ShowLessAnnoyingAccessLevelWarning
                );
            }
            // demo file does not exist --> stop and warn the user
            else
            {
                ShowMissingDemoFileMessage();
            }
        }

        /// <summary>
        /// Opens "Star Trek Online" in "demo_play_30fps" mode with the given
        /// demo.
        /// </summary>
        /// <param name="demo">The demo for which to record audio.</param>
        /// <param name="fileName">Filename of the created wave.</param>
        public void RecordDemoAudio30(DemoInfo demo, string fileName)
        {
            // Intercept missing demo file
            if (this.DemoFileExists(demo))
            {
                this.RunDemo(demo.Server, "-demo_play_30fps " + demo.FileName + " -audiooutput \"" + fileName + "\" -noInactive 1");
            }
            // demo file does not exist --> stop and warn the user
            else
            {
                ShowMissingDemoFileMessage();
            }
        }

        /// <summary>
        /// Displays a warning about the dem file being missing.
        /// </summary>
        private static void ShowMissingDemoFileMessage()
        {
            MessageBox.Show("The demo file does not exist.\n" +
                    "This might happen, if you deleted or renamed the demo\n" +
                    "file since the last refresh.\n" +
                    "Click \"Refresh\" and try again.\n" +
                    "If the problem persist, report an error under\n" +
                    "\"Updates and Help\".",
                    "Demo File Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button2);
        }

        /// <summary>
        /// Runs a selected demo file. If STO is running already, the user
        /// will be promped to either cancel or force an additional instance
        /// to run.
        /// </summary>
        private void RunDemo(GameServer server, string arguments)
        {
            // By default, run the demo
            DialogResult proceed = DialogResult.Yes;

            // Check is STO is running
            if (this.IsProcessRunning("GameClient", "Star Trek Online"))
            {
                // Ask user if she wants to run an additional instance
                proceed = MessageBox.Show("Star Trek Online is already running.\n" +
                    "Running multiple instances of STO might cause significant performance issues.\n" +
                    "Do you really want to run the demo?",
                    "STO Already Running",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);
            }

            // Run demo
            if (proceed.Equals(DialogResult.Yes))
            {
                Process gameClient = new Process();

                // fetch correct exe and working dir
                gameClient.StartInfo.FileName = this.GetGameClientPath(server);
                gameClient.StartInfo.WorkingDirectory = this.GetPath(server);

                // append general arguments and launch
                gameClient.StartInfo.Arguments = " " + arguments;
                gameClient.Start();
            }
        }

        /// <summary>
        /// Checks is a process is currently running
        /// </summary>
        /// <param name="exePrefix">Name of the process (no directory, no
        /// extension!)</param>
        /// <param name="windowName">Name of the window (distinguish multiple
        /// instances of the same process)</param>
        /// <returns></returns>
        private bool IsProcessRunning(string exePrefix, string windowName)
        {
            // runn through processes and return true if you find a match
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(exePrefix) && clsProcess.MainWindowTitle.Contains(windowName))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true, if the given demo file exists
        /// </summary>
        /// <param name="demo">The demo file to check</param>
        /// <returns>True if the file exists, false otherwise</returns>
        private bool DemoFileExists(DemoInfo demo)
        {
            return System.IO.File.Exists(this.GetDemosPath(demo.Server) + "\\" + demo.FileName);
        }

        /// <summary>
        /// Creates a list which contains all *.demo filenames from one
        /// installation
        /// </summary>
        /// <param name="server">The installation for which to parse the demos
        /// directory</param>
        /// <returns>A list of the absolute filenames with in that directory</returns>
        public List<string> GetDemoFileList(GameServer server)
        {
            List<string> result = new List<string>();

            // attempt to read in all demo files
            try
            {
                string[] files = System.IO.Directory.GetFiles(this.GetDemosPath(server));

                // collect all files with the ".demo" extension
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].EndsWith(".demo"))
                    {
                        result.Add(files[i]);
                    }
                }
            }
            // If an error occured, show it in a message box
            catch(Exception e)
            {
                MessageBox.Show("Your demo directory for " + GameClient.GetServerName(server) + " could not be read.\n" + 
                    "Please report the following error under \"Updates & Help\"\n" +
                    e.Message, "Error Parsing Demo Directory",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            return result;
        }

        /// <summary>
        /// Returns the absolute path to a demo file
        /// </summary>
        /// <param name="demo">DemoFileInfo of the demo for which to get the
        /// absolute path</param>
        /// <returns>The absolute path to the demo file</returns>
        public string GetDemoPath(DemoInfo demo)
        {
            return this.GetDemosPath(demo.Server) + "\\" + demo.FileName;
        }

        /// <summary>
        /// Feches the contents of a demo file
        /// </summary>
        /// <param name="demo">The demo to read</param>
        /// <returns>The contents of the file</returns>
        public List<string> GetDemoContents(DemoInfo demo)
        {
            return this.GetFileContents(this.GetDemoPath(demo));
        }

        /// <summary>
        /// Fetches the contents of a demo file
        /// </summary>
        /// <param name="fileName">Absolute path to the demo to read</param>
        /// <returns>The contents of the file</returns>
        public List<string> GetFileContents(string fileName)
        {
            List<string> result = new List<string>();
            StreamReader file = new StreamReader(fileName);
            string line = "";
            // parse as long as we don't have all information and there are still lines to read
            while ((line = file.ReadLine()) != null)
            {
                result.Add(line);
            }
            file.Close();
            return result;
        }

        /// <summary>
        /// Previews the contents of demo file
        /// </summary>
        /// <param name="fileName">Absolute path to the demo to read</param>
        /// <returns>The contents of the file</returns>
        public void PlayPreview(List<string> contents, GameServer server)
        {
            string previewFileName = this.GetDemosPath(server) + "\\StoDemoLauncherPreview.demo";
            System.IO.File.WriteAllLines(previewFileName, contents.ToArray());
            if (System.IO.File.Exists(previewFileName))
            {
                this.PlayDemo(DemoInfo.GetDemoFileInfo(previewFileName));
            }
        }

        /// <summary>
        /// Writes a demo file to HDD from a string list
        /// </summary>
        /// <param name="info">The DemoInfo of the file to be written</param>
        /// <param name="fileContents">The contents of the file to be written</param>
        public void WriteDemoContents(DemoInfo info, List<string> fileContents)
        {
            System.IO.File.WriteAllLines(this.GetDemoPath(info), fileContents.ToArray());
        }


    }
}
