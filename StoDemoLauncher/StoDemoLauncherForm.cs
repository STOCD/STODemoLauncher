using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;
using StoDemoLauncher.Filters;
using System.Threading;
using StoDemoLauncher.Model;
using StoDemoLauncher.Helper;
using System.Net;

namespace StoDemoLauncher
{
    /// <summary>
    /// Main application window
    /// </summary>
    public partial class StoDemoLauncherForm : Form
    {
// begin config constants
        public const string DemoLauncherIniGroup = "DemoLauncher";
        public const string MaximizedIniKey = "maximized";
        public const string LocationXIniKey = "locationX";
        public const string LocationYIniKey = "locationY";
        public const string WidthIniKey = "width";
        public const string HeightIniKey = "height";
        public const string ShowToolbarIniKey = "showToolbar";
        public const string ShowFindToolbarIniKey = "showFindToolbar";
        public const string ShowStatusBarIniKey = "showStatusBar";
        public const string AutoUpdateIntervalMsIniKey = "autoUpdateIntervalMs";
        public const string EnableAutoUpdateIniKey = "enableAutoUpdate";

        public const string DemosListViewIniGroup = "DemosListView";
        public const string DoubleClickActionIniKey = "doubleClickAction";
        public const string EnterPressActionIniKey = "enterPressAction";
        public const string ConfirmDeleteIniKey = "confirmDelete";

// end config constants

        /// <summary>
        /// Link to STO forum thread
        /// </summary>
        private static string forumThreadUrl = "http://forums.startrekonline.com/showthread.php?t=198721";

        /// <summary>
        /// Link to STO sourceforge project
        /// </summary>
        private static string sourceforgeUrl = "https://sourceforge.net/projects/stodemolauncher/";

        /// <summary>
        /// Link to STO sourceforge project
        /// </summary>
        private static string wikiUrl = "http://sourceforge.net/apps/mediawiki/stodemolauncher/";

        /// <summary>
        /// Link to auto-update page
        /// </summary>
        private static string updateUrl = "http://stodemolauncher.sourceforge.net/autoupdate.html";

        /// <summary>
        /// Silent background update thread
        /// </summary>
        private Thread autoUpdateThread = null;

        /// <summary>
        /// The game client
        /// </summary>
        private GameClient gameClient;

        /// <summary>
        /// Config.ini
        /// </summary>
        private ConfigurationFile config;

        /// <summary>
        /// The model that holds all parsed demo infos
        /// We use "[ServerName][Filename]" as unique key for all demos.
        /// </summary>
        private SortedList<string, DemoInfo> demoInfos;

        /// <summary>
        /// Manager to handle backup and restore of demo files
        /// </summary>
        private BackupManager backupManager;

        /// <summary>
        /// List of MRU meu items
        /// </summary>
        private List<ToolStripItem> mruMenuItems = new List<ToolStripItem>();

        /// <summary>
        /// Constructor. Creates a new Star Trek Online Demo Launcher form
        /// </summary>
        /// <param name="installLocation">The absolute path to the directory
        /// containing "Star Trek Online.exe"</param>
        public StoDemoLauncherForm(GameClient gameClient)
        {
            InitializeComponent();
            this.InitializeListViewDemos();
            this.config = ConfigurationFile.GetInstance();
            this.gameClient = gameClient;
            this.demoInfos = new SortedList<string, DemoInfo>();
            this.backupManager = new BackupManager(this.gameClient);
            this.RefreshList();
        }

// begin callbacks

        /// <summary>
        /// Restore cofiguration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void load_Event(object sender, EventArgs e)
        {
            UpdateMruFileList();

            // Restore location and size
            if (config.Contains(DemoLauncherIniGroup, MaximizedIniKey) && config.GetBoolValue(DemoLauncherIniGroup, MaximizedIniKey))
            {
                this.WindowState = FormWindowState.Maximized;
            }
            if (config.Contains(DemoLauncherIniGroup, LocationXIniKey) && config.Contains(DemoLauncherIniGroup, LocationYIniKey))
            {
                this.Location = new Point(config.GetIntValue(DemoLauncherIniGroup, LocationXIniKey), config.GetIntValue(DemoLauncherIniGroup, LocationYIniKey));
            }
            if (config.Contains(DemoLauncherIniGroup, WidthIniKey) && config.Contains(DemoLauncherIniGroup, HeightIniKey))
            {
                this.Size = new Size(config.GetIntValue(DemoLauncherIniGroup, WidthIniKey), config.GetIntValue(DemoLauncherIniGroup, HeightIniKey));
            }

            // Make sure window is visible on screen
            if (this.Location.X + this.Size.Width > Screen.PrimaryScreen.WorkingArea.Width)
            {
                this.Location = new Point(Math.Max(0, Screen.PrimaryScreen.WorkingArea.Width - this.Size.Width), this.Location.Y);
            }
            if (this.Location.Y + this.Size.Height > Screen.PrimaryScreen.WorkingArea.Height)
            {
                this.Location = new Point(this.Location.X, Math.Max(0, Screen.PrimaryScreen.WorkingArea.Height - this.Size.Height));
            }
            this.Size = new Size(Math.Min(this.Size.Width, Screen.PrimaryScreen.WorkingArea.Width), Math.Min(this.Size.Height, Screen.PrimaryScreen.WorkingArea.Height));
            
            // Restore toolbars
            if (config.Contains(DemoLauncherIniGroup, ShowToolbarIniKey)) this.ShowToolbar(config.GetBoolValue(DemoLauncherIniGroup, ShowToolbarIniKey));
            if (config.Contains(DemoLauncherIniGroup, ShowFindToolbarIniKey)) this.ShowFindToolbar(config.GetBoolValue(DemoLauncherIniGroup, ShowFindToolbarIniKey));
            if (config.Contains(DemoLauncherIniGroup, ShowStatusBarIniKey)) this.ShowStatusBar(config.GetBoolValue(DemoLauncherIniGroup, ShowStatusBarIniKey));

            // Write default demos list view actions to config file if it's empty
            if (!config.Contains(DemosListViewIniGroup, DoubleClickActionIniKey)) config.PutValue(DemosListViewIniGroup, DoubleClickActionIniKey, 1);
            if (!config.Contains(DemosListViewIniGroup, EnterPressActionIniKey)) config.PutValue(DemosListViewIniGroup, EnterPressActionIniKey, 1);
            if (!config.Contains(DemosListViewIniGroup, ConfirmDeleteIniKey)) config.PutValue(DemosListViewIniGroup, ConfirmDeleteIniKey, true);

            // Start update thread
            if (!config.Contains(DemoLauncherIniGroup, EnableAutoUpdateIniKey)) config.PutValue(DemoLauncherIniGroup, EnableAutoUpdateIniKey, true);
            if (!config.Contains(DemoLauncherIniGroup, AutoUpdateIntervalMsIniKey)) config.PutValue(DemoLauncherIniGroup, AutoUpdateIntervalMsIniKey, 28800000);
            UpdateAutoUpdateThread();
            this.Activate();
        }

        /// <summary>
        /// Callback for closing event
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void closing_Event(object sender, FormClosingEventArgs e)
        {
            // Kill auto-update thread
            AbortAutoUpdateThread();
            // Update form location and size
            config.PutValue(DemoLauncherIniGroup, MaximizedIniKey, this.WindowState == FormWindowState.Maximized);
            if (this.WindowState == FormWindowState.Maximized)
            {
                config.PutValue(DemoLauncherIniGroup, LocationXIniKey, this.RestoreBounds.Location.X);
                config.PutValue(DemoLauncherIniGroup, LocationYIniKey, this.RestoreBounds.Location.Y);
                config.PutValue(DemoLauncherIniGroup, WidthIniKey, this.RestoreBounds.Size.Width);
                config.PutValue(DemoLauncherIniGroup, HeightIniKey, this.RestoreBounds.Size.Height);
            }
            else
            {
                config.PutValue(DemoLauncherIniGroup, LocationXIniKey, this.Location.X);
                config.PutValue(DemoLauncherIniGroup, LocationYIniKey, this.Location.Y);
                config.PutValue(DemoLauncherIniGroup, WidthIniKey, this.Size.Width);
                config.PutValue(DemoLauncherIniGroup, HeightIniKey, this.Size.Height);
            }
            // Save toolbar states
            config.PutValue(DemoLauncherIniGroup, ShowToolbarIniKey, this.toolStrip.Visible);
            config.PutValue(DemoLauncherIniGroup, ShowFindToolbarIniKey, this.findToolStrip.Visible);
            config.PutValue(DemoLauncherIniGroup, ShowStatusBarIniKey, this.statusStrip.Visible);

            // Save configuration
            config.Save();
        }

        /// <summary>
        /// "Open" event
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void open_Event(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = this.gameClient.GetDemosPath(GameServer.HOLODECK);
            dialog.Filter = "Demo files (*.demo)|*.demo|All files (*.*)|*.*";

            DialogResult result = dialog.ShowDialog();

            if (result.Equals(DialogResult.OK))
            {
                new StoDemoEditorForm(dialog.FileName, this.gameClient).ShowDialog(this);
            }
        }

        /// <summary>
        /// A MRU file item has been clicked
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void mruFileOpen_Event(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                string demoFileName = (string)((ToolStripMenuItem)sender).Tag;
                if (System.IO.File.Exists(demoFileName))
                {
                    this.PromoteMruFile(demoFileName);
                    new StoDemoEditorForm(demoFileName, this.gameClient).ShowDialog(this);
                }
                else
                {
                    MessageBox.Show(this,
                        "The demo could not be found. It may have been moved,\n" +
                        "renamed, or deleted.",
                        "File Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    this.RemoveMruFile(demoFileName);
                }
            }
        }

        /// <summary>
        /// Exit Event
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void exit_Event(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Callback for "Refresh" event. Clears and reinitializes the list of demos.
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void refresh_Event(object sender, EventArgs e)
        {
            this.RefreshListBusy();
        }

        /// <summary>
        /// Callback for "Render Demo" event. Renders the currently selected demo.
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void renderDemo_Event(object sender, EventArgs e)
        {
            ShowRenderDemoDialogForSelectedDemo();
        }

        /// <summary>
        /// Callback for "Play Demo" event. Runs the currently selected demo.
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void playDemo_Event(object sender, EventArgs e)
        {
            if (this.CheckSelectedFile())
            {
                this.PromoteMruFile(this.gameClient.GetDemoPath(GetSelectedDemo()));
                this.gameClient.PlayDemo(GetSelectedDemo());
            }
        }

        /// <summary>
        /// Callback for "Record Demo Audio" event. Records the audio of the
        /// currently selected demo.
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void recordDemoAudio_Event(object sender, EventArgs e)
        {
            ShowRecordDemoAudioDialogForSelectedDemo();
        }

        /// <summary>
        /// Callback for key press event of the demos list. Runs the currently
        /// selected demo when pressing the enter or return keys.
        /// </summary>
        /// <param name="sender">The demos list</param>
        /// <param name="e">Event arguments</param>
        private void listViewDemos_KeyDown(object sender, KeyEventArgs e)
        {
            // only do something on enter/return
            if (e.KeyCode == Keys.Enter)
            {
                PerformActionOnSelectedDemo(config.GetIntValue(DemosListViewIniGroup, EnterPressActionIniKey));
                e.Handled = true; // no other ui component needs to worry about the key press
            }
        }

        /// <summary>
        /// Callback for double click events
        /// </summary>
        /// <param name="sender">The demos list</param>
        /// <param name="e">Event arguments</param>
        private void listViewDemos_DoubleClick(object sender, EventArgs e)
        {
            PerformActionOnSelectedDemo(config.GetIntValue(DemosListViewIniGroup, DoubleClickActionIniKey));
        }

        /// <summary>
        /// Callback for click on column of the demos list. Sorts the list by
        /// the selected column.
        /// </summary>
        /// <param name="sender">The demos list</param>
        /// <param name="e">Event arguments</param>
        private void listViewDemos_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Create an instance of the ColHeader class.
            ColHeader clickedCol = (ColHeader)this.listViewDemos.Columns[e.Column];

            // Set the ascending property to sort in the opposite order.
            clickedCol.ascending = !clickedCol.ascending;

            // Get the number of items in the list.
            int numItems = this.listViewDemos.Items.Count;

            // Turn off display while data is repoplulated.
            this.listViewDemos.BeginUpdate();

            // Populate an ArrayList with a SortWrapper of each list item.
            ArrayList SortArray = new ArrayList();
            for (int i = 0; i < numItems; i++)
            {
                SortArray.Add(new SortWrapper(this.listViewDemos.Items[i], e.Column));
            }

            // Sort the elements in the ArrayList using a new instance of the SortComparer
            // class. The parameters are the starting index, the length of the range to sort,
            // and the IComparer implementation to use for comparing elements. Note that
            // the IComparer implementation (SortComparer) requires the sort
            // direction for its constructor; true if ascending, othwise false.
            SortArray.Sort(0, SortArray.Count, new SortWrapper.SortComparer(clickedCol.ascending));

            // Clear the list, and repopulate with the sorted items.
            this.listViewDemos.Items.Clear();
            for (int i = 0; i < numItems; i++)
                this.listViewDemos.Items.Add(((SortWrapper)SortArray[i]).sortItem);

            // Turn display back on.
            this.listViewDemos.EndUpdate();
        }

        /// <summary>
        /// Callback for "SelectedIndexChanged" event from demos ListView
        /// </summary>
        /// <param name="sender">The ListView</param>
        /// <param name="e">Event arguments</param>
        private void listViewDemos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listViewDemos.SelectedItems.Count == 1)
            {
                this.SetDemoCommandsEnabled(true);
                if (this.backupManager.hasBackup(this.GetSelectedDemo()))
                {
                    this.restoreDemoToolStripMenuItem.Enabled = true;
                    this.restoreDemoMenuItem.Enabled = true;
                }
                else
                {
                    this.restoreDemoToolStripMenuItem.Enabled = false;
                    this.restoreDemoMenuItem.Enabled = false;
                }
            }
            else
            {
                this.SetDemoCommandsEnabled(false);
                this.restoreDemoToolStripMenuItem.Enabled = false;
                this.restoreDemoMenuItem.Enabled = false;
            }
        }

        /// <summary>
        /// Callback for the "Open Holodeck Demos Folder" tool strip menu item
        /// </summary>
        /// <param name="sender">The "Open Holodeck Demos Folder" tool strip menu item</param>
        /// <param name="e">Event parameters</param>
        private void openHolodeckDemosFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.gameClient.OpenDemosFolder(GameServer.HOLODECK);
        }

        /// <summary>
        /// Callback for the "Open Tribble Demos Folder" tool strip menu item
        /// </summary>
        /// <param name="sender">The "Open Tribble Demos Folder" tool strip menu item</param>
        /// <param name="e">Event parameters</param>
        private void openTribbleDemosFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.gameClient.OpenDemosFolder(GameServer.TRIBBLE);
        }

        /// <summary>
        /// Callback for the "Open Redshirt Demos Folder" tool strip menu item
        /// </summary>
        /// <param name="sender">The "Open Redshirt Demos Folder" tool strip menu item</param>
        /// <param name="e">Event parameters</param>
        private void openRedshirtDemosFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.gameClient.OpenDemosFolder(GameServer.REDSHIRT);
        }

        /// <summary>
        /// Callback for the "Open Holodeck Screen Shots Folder" tool strip menu item
        /// </summary>
        /// <param name="sender">The "Open Holodeck Screen Shots Folder" tool strip menu item</param>
        /// <param name="e">Event parameters</param>
        private void openHolodeckScreenshotsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.gameClient.OpenScreenshotsFolder(GameServer.HOLODECK);
        }

        /// <summary>
        /// Callback for the "Open Tribble Screen Shots Folder" tool strip menu item
        /// </summary>
        /// <param name="sender">The "Open Tribble Screen Shots Folder" tool strip menu item</param>
        /// <param name="e">Event parameters</param>
        private void openTribbleScreenshotsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.gameClient.OpenScreenshotsFolder(GameServer.TRIBBLE);
        }

        /// <summary>
        /// Callback for the "Open Redshirt Screen Shots Folder" tool strip menu item
        /// </summary>
        /// <param name="sender">The "Open Redshirt Screen Shots Folder" tool strip menu item</param>
        /// <param name="e">Event parameters</param>
        private void openRedshirtScreenshotsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.gameClient.OpenScreenshotsFolder(GameServer.REDSHIRT);
        }

        /// <summary>
        /// Callback for "Help" event. Visits the StoDemoLauncher wiki.
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void help_Event(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(StoDemoLauncherForm.wikiUrl);
        }

        /// <summary>
        /// Callback for "Visit Homepage" event. Visits the StoDemoLauncher homepage.
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void visitHomepage_Event(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(StoDemoLauncherForm.sourceforgeUrl);
        }
        
        /// <summary>
        /// Callback for "Forum Thread" event. Visits the StoDemoLauncher
        /// thread in the STO forums.
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void forumThread_Event(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(StoDemoLauncherForm.forumThreadUrl);
        }

        /// <summary>
        /// Callback for "Check for Updates" event
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void checkForUpdates_Event(object sender, EventArgs e)
        {
            CheckForOnlineUpdate(false);
        }

        /// <summary>
        /// Callback for "About" event. Shows version number, credits, and disclaimer.
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void about_Event(object sender, EventArgs e)
        {
            MessageBox.Show("Version " + Application.ProductVersion + "\n" +
                "by RachelGarrett\n\n" +
                "Special thanks to:\n" +
                "- h2orat\n" +
                "- Chris Fisher\n" +
                "- Jeremy Randall\n" +
                "- CaptNeo\n" +
                "- JohoCrol\n" +
                "- gaius\n" +
                " -Leviathan99\n" +
                " -Etherghost\n" +
                " -Soriedem\n" +
                " -maquis\n\n" +
                "This tool is provided \"as-is\". Use at your own risk.\n\n" +
                "This tool is not endorsed or supported by Cryptic Studios or\n" +
                "Atari and is not a product licensed from CBS or Paramount.\n\n" +
                "\"Star Trek\" and all other trademarks are the property of\n" +
                "their respective owners.\n",
                Application.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// Callback for "Edit Demo" event. Opens selected demo in new StoDemoEditor.
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void editDemo_Event(object sender, EventArgs e)
        {
            ShowDemoEditorForSelectedDemo();
        }

        /// <summary>
        /// Callback for "Backup Demo" event. Opens selected demo in new StoDemoEditor.
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void backupDemo_Event(object sender, EventArgs e)
        {
            if (this.CheckSelectedFile())
            {
                bool successful = this.backupManager.BackupDemo(this.GetSelectedDemo());
                if (successful)
                {
                    this.restoreDemoMenuItem.Enabled = true;
                    this.restoreDemoToolStripMenuItem.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Callback for "Refresh Backup" event. Opens selected demo in new StoDemoEditor.
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void restoreBackup_Event(object sender, EventArgs e)
        {
            if (this.CheckSelectedFile())
            {
                bool successful = this.backupManager.RestoreBackup(this.GetSelectedDemo());
                if (successful) this.RefreshSelectedDemo();
            }
        }

        /// <summary>
        /// Callback for "Replace Costumes" event.
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void replaceCostumes_Event(object Sender, EventArgs e)
        {
            this.ShowReplaceCostumesForm();
        }

        /// <summary>
        /// Callback for "Remove Notifications" event.
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void removeNotifications_Event(object Sender, EventArgs e)
        {
            this.ShowMessagesFilter("Remove Notifications", "NotifySend", "Notifications removed");
        }

        /// <summary>
        /// Callback for "Remove UGCProjectJobStatus Errors" event.
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void removeInternalGameCommandErrors_Event(object Sender, EventArgs e)
        {
            this.ShowMessagesFilter("Remove Internal Game Command Errors", "STRUCT(0)", "Internal game command errors removed");
        }

        /// <summary>
        /// Callback for "Remove Waveform Minigame" event.
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void removeWaveformMinigame_Event(object Sender, EventArgs e)
        {
            this.ShowMessagesFilter("Remove Waveform Modulation UI", "gclWaveformReceiveData", "Waveform Modulation UI removed");
        }

        /// <summary>
        /// Callback for "Remove Scan Cone FX" event.
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void removeScanConeFX_Event(object Sender, EventArgs e)
        {
            this.ShowResourcesFilter("Remove Scan Cone FX", "FxName Fx_Ship_Scan_Cone", "Scan Cone FX removed");
        }

        /// <summary>
        /// Callback for "Remove FX" event.
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void removeFX_Event(object Sender, EventArgs e)
        {
            this.ShowResourcesFilter("Remove FX", "FxName ", "FX removed");
        }

        /// <summary>
        /// Callback for "Options" event.
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void options_Event(object sender, EventArgs e)
        {
            DialogResult result = new OptionsForm(this.gameClient).ShowDialog(this);
            if (result == DialogResult.OK)
            {
                this.RefreshListBusy();
                if(this.config.GetBoolValue(StoDemoLauncherForm.DemoLauncherIniGroup, StoDemoLauncherForm.EnableAutoUpdateIniKey))
                {
                    this.StartAutoUpdateThread(this.config.GetIntValue(StoDemoLauncherForm.DemoLauncherIniGroup, StoDemoLauncherForm.AutoUpdateIntervalMsIniKey));
                }
                UpdateMruFileList();
            }
        }

        /// <summary>
        /// Callback for "Toolbars" checker in menu bar
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void toolbarMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowToolbar(!this.toolStrip.Visible);
        }

        /// <summary>
        /// Callback for "Status Bar" checker in menu bar
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void statusBarMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowStatusBar(!this.statusStrip.Visible);
        }

        /// <summary>
        /// Callback for "Open Folder in File Browser" event
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void openFolder_Event(object sender, EventArgs e)
        {
            ShowFolderInFileBrowserForSelectedDemo();
        }

        /// <summary>
        /// Callback for "Delete" event
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void delete_Event(object sender, EventArgs e)
        {
            if (this.CheckSelectedFile()) this.DeleteSelectedDemo();
        }

        /// <summary>
        /// Callback for "Rename" event
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void rename_Event(object sender, EventArgs e)
        {
            if (this.CheckSelectedFile()) this.RenameSelectedDemo();
        }

        /// <summary>
        /// Callback for "Find" command
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void find_Event(object sender, EventArgs e)
        {
            this.ShowFindToolbar(!this.findToolStrip.Visible);
        }

        /// <summary>
        /// Callback for search change
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void findSettingsChanged_Event(object sender, EventArgs e)
        {
            this.FilterDemosListView();
        }

        /// <summary>
        /// Changes the search engine deafult operator to AND
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void matchAllTermsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.matchAllTermsToolStripMenuItem.Checked = true;
            this.matchAtLeastOneTermToolStripMenuItem.Checked = false;
            this.FilterDemosListView();
        }

        /// <summary>
        /// Changes the search engine deafult operator to OR
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void matchAtLeastOneTermToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.matchAllTermsToolStripMenuItem.Checked = false;
            this.matchAtLeastOneTermToolStripMenuItem.Checked = true;
            this.FilterDemosListView();
        }


// end callbacks

        /// <summary>
        /// Checks whether to display tribble and redshirt entries or not
        /// </summary>
        private void InitializeToolStrip()
        {
            // if there is a tribble installation, add missing tribble entries
            if (this.gameClient.TribbleExists)
            {
                if (!this.openDemosFolderToolStripDropDownButton.DropDownItems.Contains(this.openTribbleDemosFolderToolStripMenuItem))
                {
                    this.openDemosFolderToolStripDropDownButton.DropDownItems.Add(this.openTribbleDemosFolderToolStripMenuItem);
                }
                if (!this.openScreenshotsFolderToolStripDropDownButton.DropDownItems.Contains(this.openTribbleScreenshotsFolderToolStripMenuItem))
                {
                    this.openScreenshotsFolderToolStripDropDownButton.DropDownItems.Add(this.openTribbleScreenshotsFolderToolStripMenuItem);
                }
            }
            // if there is no tribble installation, and the tribble entry is in the list, remove it.
            else
            {
                if (this.openDemosFolderToolStripDropDownButton.DropDownItems.Contains(this.openTribbleDemosFolderToolStripMenuItem))
                {
                    this.openDemosFolderToolStripDropDownButton.DropDownItems.Remove(this.openTribbleDemosFolderToolStripMenuItem);
                }
                if (this.openScreenshotsFolderToolStripDropDownButton.DropDownItems.Contains(this.openTribbleScreenshotsFolderToolStripMenuItem))
                {
                    this.openScreenshotsFolderToolStripDropDownButton.DropDownItems.Remove(this.openTribbleScreenshotsFolderToolStripMenuItem);
                }
            }
            // if there is a redshirt installation, add missing redshirt entries
            if (this.gameClient.RedshirtExists)
            {
                if (!this.openDemosFolderToolStripDropDownButton.DropDownItems.Contains(this.openRedshirtDemosFolderToolStripMenuItem))
                {
                    this.openDemosFolderToolStripDropDownButton.DropDownItems.Add(this.openRedshirtDemosFolderToolStripMenuItem);
                }
                if (!this.openScreenshotsFolderToolStripDropDownButton.DropDownItems.Contains(this.openRedshirtScreenshotsFolderToolStripMenuItem))
                {
                    this.openScreenshotsFolderToolStripDropDownButton.DropDownItems.Add(this.openRedshirtScreenshotsFolderToolStripMenuItem);
                }
            }
            // if there is no redshirt installation, and the redshirt entry is in the list, remove it.
            else
            {
                if (this.openDemosFolderToolStripDropDownButton.DropDownItems.Contains(this.openRedshirtDemosFolderToolStripMenuItem))
                {
                    this.openDemosFolderToolStripDropDownButton.DropDownItems.Remove(this.openRedshirtDemosFolderToolStripMenuItem);
                }
                if (this.openScreenshotsFolderToolStripDropDownButton.DropDownItems.Contains(this.openRedshirtScreenshotsFolderToolStripMenuItem))
                {
                    this.openScreenshotsFolderToolStripDropDownButton.DropDownItems.Remove(this.openRedshirtScreenshotsFolderToolStripMenuItem);
                }
            }
        }

        /// <summary>
        /// Checks whether to display tribble entry or not
        /// </summary>
        private void InitializeMenuStrip()
        {
            // if there is a tribble installation, add missing tribble entries
            if (this.gameClient.TribbleExists)
            {
                if (!this.openDemosFolderMenuItem.DropDownItems.Contains(this.openTribbleDemosFolderMenuItem))
                {
                    this.openDemosFolderMenuItem.DropDownItems.Add(this.openTribbleDemosFolderMenuItem);
                }
                if (!this.openScreenshotsFolderMenuItem.DropDownItems.Contains(this.openTribbleScreenshotsFolderMenuItem))
                {
                    this.openScreenshotsFolderMenuItem.DropDownItems.Add(this.openTribbleScreenshotsFolderMenuItem);
                }
            }
            // if there is no tribble installation, and the tribble entry is in the list, remove it.
            else
            {
                if (this.openDemosFolderMenuItem.DropDownItems.Contains(this.openTribbleDemosFolderMenuItem))
                {
                    this.openDemosFolderMenuItem.DropDownItems.Remove(this.openTribbleDemosFolderMenuItem);
                }
                if (this.openScreenshotsFolderMenuItem.DropDownItems.Contains(this.openTribbleScreenshotsFolderMenuItem))
                {
                    this.openScreenshotsFolderMenuItem.DropDownItems.Remove(this.openTribbleScreenshotsFolderMenuItem);
                }
            }
            // if there is a redshirt installation, add missing redshirt entries
            if (this.gameClient.RedshirtExists)
            {
                if (!this.openDemosFolderMenuItem.DropDownItems.Contains(this.openRedshirtDemosFolderMenuItem))
                {
                    this.openDemosFolderMenuItem.DropDownItems.Add(this.openRedshirtDemosFolderMenuItem);
                }
                if (!this.openScreenshotsFolderMenuItem.DropDownItems.Contains(this.openRedshirtScreenshotsFolderMenuItem))
                {
                    this.openScreenshotsFolderMenuItem.DropDownItems.Add(this.openRedshirtScreenshotsFolderMenuItem);
                }
            }
            // if there is no redshirt installation, and the redshirt entry is in the list, remove it.
            else
            {
                if (this.openDemosFolderMenuItem.DropDownItems.Contains(this.openRedshirtDemosFolderMenuItem))
                {
                    this.openDemosFolderMenuItem.DropDownItems.Remove(this.openRedshirtDemosFolderMenuItem);
                }
                if (this.openScreenshotsFolderMenuItem.DropDownItems.Contains(this.openRedshirtScreenshotsFolderMenuItem))
                {
                    this.openScreenshotsFolderMenuItem.DropDownItems.Remove(this.openRedshirtScreenshotsFolderMenuItem);
                }
            }
        }


        /// <summary>
        /// Inserts columns in list view
        /// </summary>
        private void InitializeListViewDemos()
        {
            this.listViewDemos.Columns.Add(new ColHeader("Name", 120, HorizontalAlignment.Left, true));
            this.listViewDemos.Columns.Add(new ColHeader("Map", 140, HorizontalAlignment.Left, true));
            this.listViewDemos.Columns.Add(new ColHeader("Duration", 60, HorizontalAlignment.Right, true));
            this.listViewDemos.Columns.Add(new ColHeader("Character", 80, HorizontalAlignment.Left, true));
            this.listViewDemos.Columns.Add(new ColHeader("Created", 120, HorizontalAlignment.Left, true));
            this.listViewDemos.Columns.Add(new ColHeader("Modified", 120, HorizontalAlignment.Left, true));
            this.listViewDemos.Columns.Add(new ColHeader("Server", 60, HorizontalAlignment.Left, true));
        }

        /// <summary>
        /// Re-reads demo directories
        /// </summary>
        private void RefreshList()
        {
            this.ChangeStatus("Parsing demo directories...");

            this.gameClient.Refresh();
            this.InitializeMenuStrip();
            this.InitializeToolStrip();
            this.UpdateDemosListView();

            // activate/deactivate commands that need at least one demo file to
            // work with
            this.SetDemoCommandsEnabled(this.listViewDemos.Items.Count > 0);

            this.statusLabel.Text = "Ready";
        }

        /// <summary>
        /// Re-reads demo directories and shows marque busy screen in the
        /// interim
        /// </summary>
        private void RefreshListBusy()
        {
            BusyForm.ShowBusyScreen(this);
            this.RefreshList();
            BusyForm.HideBusyScreen();
        }

        /// <summary>
        /// Activates or deactivates all commands that rely on a ´demo file
        /// selection
        /// </summary>
        /// <param name="enabled">true if the commands should be enabled, false
        /// if the commands should be disabled</param>
        private void SetDemoCommandsEnabled(bool enabled)
        {
            this.playDemoToolStripButton.Enabled = enabled;
            this.playDemoToolStripMenuItem.Enabled = enabled;
            this.editDemoToolStripButton.Enabled = enabled;
            this.editDemoToolStripMenuItem.Enabled = enabled;
            this.renderDemoToolStripButton.Enabled = enabled;
            this.renderDemoToolStripMenuItem.Enabled = enabled;
            this.backupDemoMenuItem.Enabled = enabled;
            this.backupDemoToolStripMenuItem.Enabled = enabled;
            this.restoreDemoMenuItem.Enabled = enabled;
            this.restoreDemoToolStripMenuItem.Enabled = enabled;
            this.filtersMenuItem.Enabled = enabled;
            this.filtersToolStripMenuItem.Enabled = enabled;
            this.removeNotificationsMenuItem.Enabled = enabled;
            this.renderDemoMenuItem.Enabled = enabled;
            this.playDemoMenuItem.Enabled = enabled;
            this.editDemoMenuItem.Enabled = enabled;
            this.openFolderInFileBrowserToolStripMenuItem.Enabled = enabled;
            this.deleteToolStripMenuItem.Enabled = enabled;
            this.renameToolStripMenuItem.Enabled = enabled;
            this.deleteMenuItem.Enabled = enabled;
            this.renameMenuItem.Enabled = enabled;
            this.removeWaveformModulationUiMenuItem.Enabled = enabled;
            this.removeWaveformModulationUiToolStripMenuItem.Enabled = enabled;
            this.replaceCostumesMenuItem.Enabled = enabled;
            this.replaceCostumesToolStripMenuItem.Enabled = enabled;
            this.removeScanConeFxMenuItem.Enabled = enabled;
            this.removeScanConeFxToolStripMenuItem.Enabled = enabled;
            this.removeFXMenuItem.Enabled = enabled;
            this.removeFXToolStripMenuItem.Enabled = enabled;
            this.recordDemoAudioToolStripButton.Enabled = enabled;
            this.recordDemoAudioMenuItem.Enabled = enabled;
            this.recordDemoAudioToolStripMenuItem.Enabled = enabled;
        }

        /// <summary>
        /// Reads demo directories, parses file headers, and puts results in demo list view.
        /// </summary>
        private void UpdateDemosListView()
        {
            this.demoInfos.Clear();
            List<string> demoFiles = new List<string>();
            if(System.IO.Directory.Exists(this.gameClient.GetDemosPath(GameServer.HOLODECK)))
            {
                // Holodeck demos
               demoFiles.AddRange(gameClient.GetDemoFileList(GameServer.HOLODECK));
            }
            // Tribble demos
            if (this.gameClient.TribbleExists
                && System.IO.Directory.Exists(this.gameClient.GetDemosPath(GameServer.TRIBBLE)))
            {
                demoFiles.AddRange(gameClient.GetDemoFileList(GameServer.TRIBBLE));
            }
            // Redshirt demos
            if (this.gameClient.RedshirtExists
                && System.IO.Directory.Exists(this.gameClient.GetDemosPath(GameServer.REDSHIRT)))
            {
                demoFiles.AddRange(gameClient.GetDemoFileList(GameServer.REDSHIRT));
            }

            if (demoFiles.Count > 0)
            {
                foreach (string filename in demoFiles)
                {
                    DemoInfo info = DemoInfo.GetDemoFileInfo(filename);
                    if (info != null)
                    {
                        //this.listViewDemos.Items.Add(this.CreateListViewItem(info));
                        this.demoInfos.Add(GameClient.GetServerName(info.Server) + info.FileName, info);
                    }
                }
                FilterDemosListView();
            }
            else
            {
                MessageBox.Show(this,
                    "Not demo files could be found. Either you do not have any\n" +
                    "\"demos\" directories or you have no recorded demo files.\n" +
                    "Record a demo using the \"\\demorecord [filename]\" and\n" +
                    "\"\\demorecordstop\" commands in Star Trek Online.",
                    "No Demo Files Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Filters the list 
        /// </summary>
        private void FilterDemosListView()
        {
            this.listViewDemos.BeginUpdate();
            this.listViewDemos.Items.Clear();
            foreach (DemoInfo info in this.demoInfos.Values)
            {
                if (this.FilterInfo(info)) this.listViewDemos.Items.Add(this.CreateListViewItem(info));
            }
            if (this.listViewDemos.Items.Count > 0) this.listViewDemos.Items[0].Selected = true;
            else
            {
                this.listViewDemos.SelectedItems.Clear();
                this.SetDemoCommandsEnabled(false);
            }
            this.resultCountToolStripLabel.Text = this.listViewDemos.Items.Count.ToString() + (this.listViewDemos.Items.Count == 1 ? " result" : " results");
            this.listViewDemos.EndUpdate();
        }

        /// <summary>
        /// Decides, if a given DemoInfo matches the term in the findTextBox 
        /// </summary>
        /// <param name="info">The DemoInfo to be tested</param>
        /// <returns>True if the demo file matches the search, false otherwise</returns>
        private bool FilterInfo(DemoInfo info)
        {
            // determine correct logical operation
            StringFilter.LogicalOperator logOp;
            if (this.matchAllTermsToolStripMenuItem.Checked) logOp = StringFilter.And;
            else logOp = StringFilter.Or;

            // determine correct case matching
            if (this.matchCaseToolStripButton.Checked)
            {
                return StringFilter.TestAll(info.ToString(), this.findToolStripTextBox.Text, logOp);
            }
            else
            {
                return StringFilter.TestAllCaseInvariant(info.ToString(), this.findToolStripTextBox.Text, logOp);
            }
        }

        /// <summary>
        /// Opens a window indicating that the demos list needs to be refresed
        /// due to a missing file
        /// </summary>
        private void ShowRefreshMessage()
        {
            MessageBox.Show(this,
                "The selected demo file does not exist. It has been moved,\n" +
                "deleted, or renamed. To prevent further errors like this,\n" +
                "a refresh will be performed.",
                "File Not Found",
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop);
        }

        /// <summary>
        /// Prints something to the status bar
        /// </summary>
        /// <param name="message">The message to display</param>
        private void ChangeStatus(string message)
        {
            this.statusLabel.Text = message;
            this.statusStrip.Refresh();
        }

        /// <summary>
        /// Deletes the currently selected demo file
        /// </summary>
        private void DeleteSelectedDemo()
        {
            bool success = false;

            DemoInfo demo = this.GetSelectedDemo();

            DialogResult result;

            // Show confirmation dialog, unless disabled
            if (config.GetBoolValue(DemosListViewIniGroup, ConfirmDeleteIniKey))
            {
                result = MessageBox.Show(this,
                    "Are you sure you want to permanently delete " + demo.FileName + "?",
                    "Confirm File Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);
            }
            else
            {
                result = DialogResult.Yes;
            }

            if(result.Equals(DialogResult.Yes))
            {
                try
                {
                    System.IO.File.Delete(this.gameClient.GetDemoPath(demo));
                    success = true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(this,
                        demo.FileName + " cannot be deleted.\n" +
                        "Make sure the file is not opened in another\n" +
                        "application and you have permission to delete it.\n" +
                        ex.Message,
                        "Cannot Delete File",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

            if (success)
            {
                int selectedIndex = this.listViewDemos.SelectedIndices[0];
                this.demoInfos.Remove(GameClient.GetServerName(demo.Server) + demo.FileName);
                this.listViewDemos.Items.RemoveAt(selectedIndex);
                if (this.listViewDemos.Items.Count > 0)
                {
                    this.listViewDemos.Items[Math.Min(selectedIndex, this.listViewDemos.Items.Count - 1)].Selected = true;
                }
                else
                {
                    this.SetDemoCommandsEnabled(false);
                }
                this.statusLabel.Text = "Deleted " + demo.FileName;
            }
            else
            {
                this.statusLabel.Text = "Error deleting " + demo.FileName;
            }
        }

        /// <summary>
        /// Reads a demo file to RAM and returns the contents line by line
        /// </summary>
        /// <returns>List with the lines of the demo file</returns>
        private List<string> GetSelectedDemoFileContents()
        {
            BusyForm.ShowBusyScreen(this);
            List<string> fileContents = this.gameClient.GetFileContents(this.gameClient.GetDemoPath(this.GetSelectedDemo()));
            BusyForm.HideBusyScreen();
            return fileContents;
        }

        /// <summary>
        /// Renames the currently selected demo file
        /// </summary>
        private void RenameSelectedDemo()
        {
            bool success = false;

            DemoInfo demo = this.GetSelectedDemo();
            RenameDemoForm renameDialg = new RenameDemoForm(demo.FileName);
            DialogResult result = renameDialg.ShowDialog(this);

            string newDemoPath = "";

            if (result.Equals(DialogResult.OK))
            {
                newDemoPath = this.gameClient.GetDemosPath(demo.Server) + "\\" + renameDialg.UserText;
                if(!newDemoPath.EndsWith(".demo")) newDemoPath += ".demo";

                try
                {
                    System.IO.File.Move(this.gameClient.GetDemoPath(demo), newDemoPath);
                    success = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this,
                        demo.FileName + " cannot be moved.\n" +
                        "Make sure the file is not opened in another\n" +
                        "application and you have permissions to delete and\n" +
                        "files in the demos directory.\n" +
                        ex.Message,
                        "Cannot Rename File",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

            if (success)
            {
                int selectedIndex = this.listViewDemos.SelectedIndices[0];
                DemoInfo newDemo = DemoInfo.GetDemoFileInfo(newDemoPath);
                ListViewItem newItem = this.CreateListViewItem(newDemo);
                this.listViewDemos.Items.RemoveAt(selectedIndex);
                this.demoInfos.Remove(GameClient.GetServerName(demo.Server) + demo.FileName);
                this.demoInfos.Add(GameClient.GetServerName(newDemo.Server) + newDemo.FileName, newDemo);
                this.listViewDemos.Items.Add(newItem);
                int newIndex = this.listViewDemos.Items.IndexOf(newItem);
                this.listViewDemos.Items[newIndex].Selected = true;
                this.statusLabel.Text = "Renamed " + demo.FileName + " to " + newDemo.FileName;
            }
            else
            {
                this.statusLabel.Text = "Error renaming " + demo.FileName;
            }
        }

        /// <summary>
        /// Creates and shows the Demo Editor for the selected file
        /// </summary>
        private void ShowDemoEditorForSelectedDemo()
        {
            if (this.CheckSelectedFile())
            {
                this.PromoteMruFile(this.gameClient.GetDemoPath(this.GetSelectedDemo()));
                new StoDemoEditorForm(this.gameClient.GetDemoPath(this.GetSelectedDemo()), this.gameClient).ShowDialog(this);
                this.RefreshSelectedDemo();
            }
        }

        /// <summary>
        /// Creates and shows a RenderDemoForm for the selected file
        /// </summary>
        private void ShowRenderDemoDialogForSelectedDemo()
        {
            if (this.CheckSelectedFile())
            {
                this.PromoteMruFile(this.gameClient.GetDemoPath(this.GetSelectedDemo()));
                new RenderDemoForm(this.GetSelectedDemo(), this.gameClient, this.GetSelectedDemo().FileName.Substring(0, this.GetSelectedDemo().FileName.LastIndexOf("."))).ShowDialog(this);
            }
        }

        /// <summary>
        /// Creates and shows a RecordDemoAudioForm for the selected demo file
        /// </summary>
        private void ShowRecordDemoAudioDialogForSelectedDemo()
        {
            if (this.CheckSelectedFile())
            {
                this.PromoteMruFile(this.gameClient.GetDemoPath(this.GetSelectedDemo()));
                new RecordDemoAudioForm(this.GetSelectedDemo(), this.gameClient, this.GetSelectedDemo().FileName.Substring(0, this.GetSelectedDemo().FileName.LastIndexOf(".")) + ".wav").ShowDialog(this);
            }
        }

        /// <summary>
        /// Creates a new ReplaceCostumes Form
        /// </summary>
        /// 
        private void ShowReplaceCostumesForm()
        {
            if (this.CheckSelectedFile())
            {
                List<string> fileContents = GetSelectedDemoFileContents();
                DialogResult dialogResult = new ReplaceCostumesForm(fileContents, this.gameClient, this.GetSelectedDemo().Server).ShowDialog(this);
                if (dialogResult == DialogResult.OK)
                {
                    this.statusLabel.Text = "Characters replaced";
                    this.gameClient.WriteDemoContents(this.GetSelectedDemo(), fileContents);
                    this.RefreshSelectedDemo();
                }
            }
        }

        /// <summary>
        /// Creates a new FilterFileForm with a RemoveMessagesFileFilter.
        /// </summary>
        /// <param name="titlebar">The name of the filter in the titlebar</param>
        /// <param name="messagesSearchString">The message text to search for</param>
        /// <param name="successStatusMessage">The status bar message to
        /// display after successful filter application</param>
        private void ShowMessagesFilter(string titlebar, string messagesSearchString, string successStatusMessage)
        {
            if (this.CheckSelectedFile())
            {
                List<string> fileContents = GetSelectedDemoFileContents();
                this.ShowFilter(titlebar, new RemoveMessagesFileFilter(fileContents, titlebar, messagesSearchString), successStatusMessage, fileContents);
            }
        }

        /// <summary>
        /// Creates a new FilterFileForm with a RemoveResourcesFileFilter.
        /// </summary>
        /// <param name="titlebar">The name of the filter in the titlebar</param>
        /// <param name="messagesSearchString">The content text to search for</param>
        /// <param name="successStatusMessage">The status bar message to
        /// display after successful filter application</param>
        private void ShowResourcesFilter(string titlebar, string contentSearchString, string successStatusMessage)
        {
            if (this.CheckSelectedFile())
            {
                List<string> fileContents = GetSelectedDemoFileContents();
                this.ShowFilter(titlebar, new RemoveResourcesFileFilter(fileContents, titlebar, contentSearchString), successStatusMessage, fileContents);
            }
        }

        /// <summary>
        /// Creates a new FilterFileForm with a given filter
        /// </summary>
        /// <param name="titlebar">The name of the filter in the titlebar</param>
        /// <param name="messagesSearchString">The content text to search for</param>
        /// <param name="successStatusMessage">The status bar message to
        /// display after successful filter application</param>
        /// <param name="fileContents">The file contents to filter</param>
        private void ShowFilter(string titlebar, AbstractFileFilter filter, string successStatusMessage, List<string> fileContents)
        {
            FilterFileForm filterFileForm = new FilterFileForm(fileContents, this.gameClient, this.GetSelectedDemo().Server, filter);
            DialogResult dialogResult = filterFileForm.ShowDialog(this);
            filterFileForm.Dispose();

            if (dialogResult == DialogResult.OK)
            {
                this.statusLabel.Text = successStatusMessage;
                this.gameClient.WriteDemoContents(this.GetSelectedDemo(), fileContents);
                this.RefreshSelectedDemo();
            }
        }

        /// <summary>
        /// Sets find toolbar visibility
        /// </summary>
        /// <param name="visible">true if the toolbar should be shown, false
        /// when it should be hidden.</param>
        private void ShowFindToolbar(bool visible)
        {
            this.findToolStrip.Visible = visible;
            if (!visible)
            {
                this.matchAllTermsToolStripMenuItem.Checked = true;
                this.matchAtLeastOneTermToolStripMenuItem.Checked = false;
                this.findToolStripTextBox.Text = "";
                this.FilterDemosListView();
            }
            else
            {
                this.findToolStripTextBox.SelectAll();
                this.findToolStripTextBox.Focus();
            }
            this.findToolStripMenuItem.Checked = visible;
        }

        /// <summary>
        /// Sets toolbar visibility 
        /// </summary>
        /// <param name="visible">true if the toolbar should be shown, false
        /// when it should be hidden.</param>
        private void ShowToolbar(bool visible)
        {
            this.toolbarMenuItem.Checked = visible;
            this.toolStrip.Visible = visible;
        }

        /// <summary>
        /// Sets status bar visibility 
        /// </summary>
        /// <param name="visible">true if the toolbar should be shown, false
        /// when it should be hidden.</param>
        private void ShowStatusBar(bool visible)
        {
            this.statusBarMenuItem.Checked = visible;
            this.statusStrip.Visible = visible;
        }

        /// <summary>
        /// Checks the web for online updates of StoDemoLauncher
        /// </summary>
        private static void CheckForOnlineUpdate(bool silent)
        {
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(updateUrl);
                webRequest.Method = "GET";

                // download website
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                StreamReader webSource = new StreamReader(webResponse.GetResponseStream());
                string contents = webSource.ReadToEnd();
                webResponse.Close();
                webSource.Close();

                if (!silent) BusyForm.HideBusyScreen();

                // check for new version
                string version = contents.Substring(0, contents.IndexOf("\r"));
                int onlineMain = System.Convert.ToInt32(version.Substring(0, version.IndexOf(".")));
                version = version.Substring(version.IndexOf(".") + 1);
                int onlineSub = System.Convert.ToInt32(version.Substring(0, version.IndexOf(".")));
                version = version.Substring(version.IndexOf(".") + 1);
                int onlineSubsub = System.Convert.ToInt32(version.Substring(0, version.IndexOf(".")));
                version = version.Substring(version.IndexOf(".") + 1);
                int onlineSubsubsub = System.Convert.ToInt32(version);

                version = Application.ProductVersion;
                int offlineMain = System.Convert.ToInt32(version.Substring(0, version.IndexOf(".")));
                version = version.Substring(version.IndexOf(".") + 1);
                int offlineSub = System.Convert.ToInt32(version.Substring(0, version.IndexOf(".")));
                version = version.Substring(version.IndexOf(".") + 1);
                int offlineSubsub = System.Convert.ToInt32(version.Substring(0, version.IndexOf(".")));
                version = version.Substring(version.IndexOf(".") + 1);
                int offlineSubsubsub = System.Convert.ToInt32(version);

                if (onlineMain > offlineMain || onlineSub > offlineSub || onlineSubsub > offlineSubsub || onlineSubsubsub > offlineSubsubsub)
                {
                    string downloadUrl = contents.Substring(contents.IndexOf("http:"));
                    DialogResult dialogResult = MessageBox.Show(
                        "Version " + contents.Substring(0, contents.IndexOf("\r")) + " of the Star Trek Online Demo Launcher\n" +
                        "is available for downlaod. You are currently using version \n" +
                        Application.ProductVersion + ".\n" +
                        "Do you want to download the newer version?",
                        "New Version Available For Download",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
                    if(dialogResult == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(downloadUrl);
                    }
                }
                else if (!silent)
                {
                    MessageBox.Show(
                       "You are running the most recent version of Star Trek Online Demo Launcher.",
                       "You are Up-To-Date",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
                }
            }
            catch (Exception e)
            {
                if (!silent)
                {
                    BusyForm.HideBusyScreen();
                    MessageBox.Show(
                        "The auto-update site in the internet could not be\n" +
                        "contacted.\n" +
                        e.Message,
                        "Auto-Update Not Responding",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            if (!silent) BusyForm.HideBusyScreen();
        }

        /// <summary>
        /// Opens the windows explorer with the currently selected demo file highlighted
        /// </summary>
        private void ShowFolderInFileBrowserForSelectedDemo()
        {
            if (this.CheckSelectedFile()) System.Diagnostics.Process.Start("explorer.exe", "/select, " + this.gameClient.GetDemoPath(this.GetSelectedDemo()));
        }

        /// <summary>
        /// Runs one of the default actions on the currently selected demo file
        /// </summary>
        /// <param name="actionId">Id of the action to perform</param>
        private void PerformActionOnSelectedDemo(int actionId)
        {
            this.PromoteMruFile(this.gameClient.GetDemoPath(GetSelectedDemo()));
            switch (actionId)
            {
                // Play Demo
                case 1:
                    {
                        if (this.CheckSelectedFile()) this.gameClient.PlayDemo(GetSelectedDemo());
                        break;
                    }
                // Edit
                case 2:
                    {
                        this.ShowDemoEditorForSelectedDemo();
                        break;
                    }
                // Render
                case 3:
                    {
                        this.ShowRenderDemoDialogForSelectedDemo();
                        break;
                    }
                // Record
                case 4:
                    {
                        this.ShowRecordDemoAudioDialogForSelectedDemo();
                        break;
                    }
                // Open File in File Browser
                case 5:
                    {
                        if (this.CheckSelectedFile()) this.ShowFolderInFileBrowserForSelectedDemo();
                        break;
                    }
            }
        }

// begin model <--> view conversion
        /// <summary>
        /// Converts selected list view line back into a DemoFileInfo instance
        /// (carbon copy)
        /// </summary>
        /// <returns>A DemoFileInfo copy of the currently selected line.</returns>
        private DemoInfo GetSelectedDemo()
        {
            return (DemoInfo)this.demoInfos[this.listViewDemos.SelectedItems[0].SubItems[6].Text + this.listViewDemos.SelectedItems[0].SubItems[0].Text];
        }

        /// <summary>
        /// Converts a DemoFileInfo object into a ListViewItem
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public ListViewItem CreateListViewItem(DemoInfo demo)
        {
            return new ListViewItem(new string[]
            {
                demo.FileName,
                demo.MapName,
                StoDemoLauncherForm.CustomDurationConvert(demo.StartTime, demo.EndTime),
                demo.Character,
                StoDemoLauncherForm.CustomTimeConvert(demo.Create),
                StoDemoLauncherForm.CustomTimeConvert(demo.Modify),
                GameClient.GetServerName(demo.Server) });
        }

        /// <summary>
        /// Creates a date/time string usable for sorting
        /// </summary>
        /// <param name="date">The date to be converted</param>
        /// <returns>The date and time in "yyyy-mm-dd hh:mm" format</returns>
        public static string CustomTimeConvert(DateTime date)
        {
            string result = "";

            result = string.Format("{0:0000}", date.Year) + "-" + string.Format("{0:00}", date.Month) + "-" + string.Format("{0:00}", date.Day) + " " + string.Format("{0:00}", date.Hour) + ":" + string.Format("{0:00}", date.Minute);

            return result;
        }

        /// <summary>
        /// Creates a readable time duration string from two measurres in seconds
        /// </summary>
        /// <param name="StartTime">Start time in seconds</param>
        /// <param name="EndTime">End time in seconds</param>
        /// <returns>A time string of the difference in m:ss or h:mm:ss format</returns>
        public static string CustomDurationConvert(long StartTime, long EndTime)
        {
            // Decompose into hours, minutes, seconds
            long seconds = EndTime - StartTime;
            long minutes = seconds / 60;
            seconds -= minutes * 60;
            long hours = minutes / 60;
            minutes -= hours * 60;

            // build strings
            if (hours == 0)
            {
                return string.Format("{0:00}", minutes) + ":" + string.Format("{0:00}", seconds) + " min";
            }
            else
            {
                return hours + ":" + string.Format("{0:00}", minutes) + ":" + string.Format("{0:00}", seconds) + " h";
            }
        }

        /// <summary>
        /// Updates the selected list view entry
        /// </summary>
        private void RefreshSelectedDemo()
        {
            DemoInfo demo = this.GetSelectedDemo();
            demo = DemoInfo.GetDemoFileInfo(this.gameClient.GetDemoPath(demo));
            ListViewItem updatedItem = this.CreateListViewItem(demo);
            int selectedIndex = this.listViewDemos.SelectedIndices[0];
            this.listViewDemos.Items[this.listViewDemos.SelectedIndices[0]] = updatedItem;
            this.listViewDemos.Items[selectedIndex].Selected = true;
        }

        /// <summary>
        /// Checks if the currently selected file still exists and runs a
        /// refresh of the list otherwise.
        /// </summary>
        /// <returns>true, if the file exists, false otherwise</returns>
        private bool CheckSelectedFile()
        {
            if (this.listViewDemos.SelectedItems.Count == 1)
            {
                if (System.IO.File.Exists(this.gameClient.GetDemoPath(this.GetSelectedDemo())))
                {
                    return true;
                }
                else
                {
                    DemoInfo demo = this.GetSelectedDemo();
                    demo = DemoInfo.GetDemoFileInfo(this.gameClient.GetDemoPath(demo));
                    ListViewItem updatedItem = this.CreateListViewItem(demo);
                    int selectedIndex = this.listViewDemos.SelectedIndices[0];
                    this.listViewDemos.Items[this.listViewDemos.SelectedIndices[0]] = updatedItem;
                    this.listViewDemos.Items[selectedIndex].Selected = true;
                    this.RefreshListBusy();
                }
            }
            else
            {
                MessageBox.Show(
                    "Please select a demo from the list.",
                    "No Demo Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            return false;
        }
// end model <--> view conversion

        /// <summary>
        /// Checks for online updates every six hours
        /// </summary>
        /// <param name="interval">Integer time in ms to wait between checks</param>
        static void updateThreadStart(Object interval)
        {
            Int32 waitTime = (Int32)interval;
            while (true)
            {
                CheckForOnlineUpdate(true);
                System.Threading.Thread.Sleep(waitTime);
            }
        }

        /// <summary>
        /// Manages the Auto-Update thread
        /// </summary>
        private void UpdateAutoUpdateThread()
        {
            if (config.GetBoolValue(DemoLauncherIniGroup, EnableAutoUpdateIniKey))
            {
                this.StartAutoUpdateThread(config.GetIntValue(DemoLauncherIniGroup, AutoUpdateIntervalMsIniKey));
            }
            else
            {
                this.AbortAutoUpdateThread();
            }
        }

        /// <summary>
        /// Starts auto-update thread, or replaces running auto-update thread
        /// </summary>
        /// <param name="interval">Auto-update interval in ms</param>
        private void StartAutoUpdateThread(Int32 interval)
        {
            // cancel old thread
            this.AbortAutoUpdateThread();
            // Start update background thread
            ParameterizedThreadStart startDelegate = new ParameterizedThreadStart(updateThreadStart);
            autoUpdateThread = new Thread(startDelegate);
            autoUpdateThread.Priority = ThreadPriority.Lowest;
            autoUpdateThread.Start(interval);
        }

        /// <summary>
        /// Kill auto-update thread
        /// </summary>
        private void AbortAutoUpdateThread()
        {
            if (this.autoUpdateThread != null)
            {
                try { this.autoUpdateThread.Abort(); }
                finally { }
            }
        }

        /// <summary>
        /// Disposes old MRU file menu items and replaces them with up-to-date
        /// ones
        /// </summary>
        private void UpdateMruFileList()
        {
            // Remove old entries
            foreach (ToolStripItem menuItem in this.mruMenuItems)
            {
                this.fileToolStripMenuItem.DropDownItems.Remove(menuItem);
            }
            mruMenuItems.Clear();

            // Load MruList
            MruFileList mruList = new MruFileList();
            this.mruMenuItems.AddRange(mruList.ToToolStripItems(this.mruFileOpen_Event));

            // Add items to file menu
            if (mruMenuItems.Count > 0)
            {
                this.mruMenuItems.Insert(0, new ToolStripSeparator());
            }
            foreach (ToolStripItem menuItem in this.mruMenuItems)
            {
                this.fileToolStripMenuItem.DropDownItems.Insert(this.fileToolStripMenuItem.DropDownItems.Count - 2, menuItem);
            }
        }

        /// <summary>
        /// Moves a file at the top of the MRU files list
        /// </summary>
        /// <param name="fileName">Path to the file to promote</param>
        private void PromoteMruFile(string fileName)
        {
            MruFileList mruList = new MruFileList();
            mruList.PromoteFile(fileName);
            mruList.SaveToConfig();
            this.UpdateMruFileList();
        }

        /// <summary>
        /// Removes a file from the MRU files list
        /// </summary>
        /// <param name="fileName">Path to the file to remove</param>
        private void RemoveMruFile(string fileName)
        {
            MruFileList mruList = new MruFileList();
            mruList.RemoveFile(fileName);
            mruList.SaveToConfig();
            this.UpdateMruFileList();
        }
    }
}
