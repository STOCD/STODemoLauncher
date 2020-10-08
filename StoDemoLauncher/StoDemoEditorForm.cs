using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StoDemoLauncher.Filters;
using StoDemoLauncher.Model;
using StoDemoLauncher.Parser;

namespace StoDemoLauncher
{
    /// <summary>
    /// Editor for *.demo files
    /// </summary>
    public partial class StoDemoEditorForm : Form
    {
        /// <summary>
        /// The currently edited file
        /// </summary>
        string fileName;

        /// <summary>
        /// Our model
        /// </summary>
        DemoInfo demo;

        /// <summary>
        /// The Star Trek Online game client
        /// </summary>
        GameClient gameClient;

        /// <summary>
        /// The manager of demo backups
        /// </summary>
        BackupManager backupManager;

        /// <summary>
        /// The contents of the demo file
        /// </summary>
        List<string> fileContents;

        /// <summary>
        /// Flag indicates if the demo has been changed since last saving
        /// </summary>
        bool dirty = false;

        /// <summary>
        /// Constructor
        /// </summary>
        public StoDemoEditorForm(string filename, GameClient gameClient)
        {
            InitializeComponent();
            this.fileName = filename;
            this.demo = DemoInfo.GetDemoFileInfo(this.fileName);
            this.gameClient = gameClient;
            this.backupManager = new BackupManager(gameClient);
//            this.fileContents = GetFileContents(filename);
            this.dirty = false;
        }

        private List<string> GetFileContents(string filename)
        {
            List<string> result;
            BusyForm.ShowBusyScreen(this);
            result = this.gameClient.GetFileContents(filename);
            BusyForm.HideBusyScreen();
            return result;
        }

// begin callbacks

        /// <summary>
        /// Form load callback (this code is executed after the 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StoDemoEditorForm_Shown(object sender, EventArgs e)
        {
            this.fileContents = GetFileContents(this.fileName);
            RefreshFileInfo();
        }

        /// <summary>
        /// Opens a new demo file in the demo Editor
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        public void openDemo_Event(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = this.gameClient.GetDemosPath(this.demo.Server);
            dialog.FileName = this.GetSimpleFileName();
            dialog.Filter = "Demo files (*.demo)|*.demo|All files (*.*)|*.*";

            DialogResult result = dialog.ShowDialog();

            if (result.Equals(DialogResult.OK))
            {
                this.fileName = dialog.FileName;
                this.demo = DemoInfo.GetDemoFileInfo(this.fileName);
                this.RefreshFileInfo();
                if (this.demo != null)
                {
                    this.fileContents = this.GetFileContents(this.fileName); //this.gameClient.GetDemoContents(this.demo);
                    this.dirty = false;
                }
            }
        }

        /// <summary>
        /// Callback for "Close" event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_Event(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Saves the current demo file
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        public void save_Event(object sender, EventArgs e)
        {
            if (this.SaveFile(this.fileName, this.fileContents))
            {
                this.dirty = false;
                this.RefreshFileInfo();
                this.statusStripLabel.Text = "Ready";
            }
        }

        /// <summary>
        /// Play the demo in Star Trek Online
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        public void saveAs_Event(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = this.gameClient.GetDemosPath(this.demo.Server);
            dialog.FileName = this.GetSimpleFileName();
            dialog.Filter = "Demo files (*.demo)|*.demo|All files (*.*)|*.*";

            DialogResult result = dialog.ShowDialog();

            if (result.Equals(DialogResult.OK))
            {
                if (this.SaveFile(dialog.FileName, this.fileContents))
                {
                    this.fileName = dialog.FileName;
                    this.dirty = false;
                    this.RefreshFileInfo();
                    this.statusStripLabel.Text = "Ready";
                }
                else
                {
                    this.statusStripLabel.Text = "Error saving file";
                }
            }
        }

        /// <summary>
        /// Play the demo in Star Trek Online
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        public void playPreview_Event(object sender, EventArgs e)
        {
            this.gameClient.PlayPreview(this.fileContents, this.demo.Server);
        }

        /// <summary>
        /// Renders the demo to individual files
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        public void renderDemo_Event(object sender, EventArgs e)
        {
            string previewFileName = gameClient.GetDemosPath(this.demo.Server) + "\\StoDemoLauncherPreview.demo";
            System.IO.File.WriteAllLines(previewFileName, this.fileContents.ToArray());
            if (System.IO.File.Exists(previewFileName))
            {
                string prefix = this.fileName.Substring(0, this.fileName.LastIndexOf("."));
                new RenderDemoForm(DemoInfo.GetDemoFileInfo(previewFileName), this.gameClient, prefix.Substring(prefix.LastIndexOf("\\") + 1)).ShowDialog(this);
            }
        }

        /// <summary>
        /// Records the audio of a demo to ia wave file
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        public void recordDemoAudio_Event(object sender, EventArgs e)
        {
            string previewFileName = gameClient.GetDemosPath(this.demo.Server) + "\\StoDemoLauncherPreview.demo";
            System.IO.File.WriteAllLines(previewFileName, this.fileContents.ToArray());
            if (System.IO.File.Exists(previewFileName))
            {
                string wavFileName = this.fileName.Substring(0, this.fileName.LastIndexOf(".")) + ".wav";
                new RecordDemoAudioForm(DemoInfo.GetDemoFileInfo(previewFileName), this.gameClient, wavFileName.Substring(wavFileName.LastIndexOf("\\") + 1)).ShowDialog(this);
            }
        }

        /// <summary>
        /// Callback for "Replace Costumes" event
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void replaceCostumes_Event(object sender, EventArgs e)
        {
            new ReplaceCostumesForm(this.fileContents, this.gameClient, this.demo.Server).ShowDialog(this);
        }

        /// <summary>
        /// Starts up the "Remove Notification" filter dialog
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void removeNotifications_Event(object sender, EventArgs e)
        {
            RemoveMessagesFileFilter filter = new RemoveMessagesFileFilter(this.fileContents, "Remove Notifications", "NotifySend");
            DialogResult dialogResult = new FilterFileForm(this.fileContents, this.gameClient, this.demo.Server, filter).ShowDialog(this);

            if (dialogResult == DialogResult.OK)
            {
                this.statusStripLabel.Text = "Notifications removed";
                this.dirty = true;
                this.RefreshFileInfo();
            }
        }

        /// <summary>
        /// Starts up the "Remove Internal Game Command Errors" filter dialog
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void removeInternalGameCommandErrors_Event(object sender, EventArgs e)
        {
            RemoveMessagesFileFilter filter = new RemoveMessagesFileFilter(this.fileContents, "Remove Internal Game Command Errors", "STRUCT(0)");
            DialogResult dialogResult = new FilterFileForm(this.fileContents, this.gameClient, this.demo.Server, filter).ShowDialog(this);

            if (dialogResult == DialogResult.OK)
            {
                this.statusStripLabel.Text = "Internal game command errors removed";
                this.dirty = true;
                this.RefreshFileInfo();
            }
        }

        /// <summary>
        /// Starts up the "Remove Waveform Minigame" filter dialog
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void removeWaveformMinigame_Event(object sender, EventArgs e)
        {
            RemoveMessagesFileFilter filter = new RemoveMessagesFileFilter(this.fileContents, "Remove Waveform Minigames", "gclWaveformReceiveData");
            DialogResult dialogResult = new FilterFileForm(this.fileContents, this.gameClient, this.demo.Server, filter).ShowDialog(this);

            if (dialogResult == DialogResult.OK)
            {
                this.statusStripLabel.Text = "Waveform Minigames removed";
                this.dirty = true;
                this.RefreshFileInfo();
            }
        }

        /// <summary>
        /// Starts up the "Remove Scan Cone FX" filter dialog
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void removeScanConeFx_Event(object sender, EventArgs e)
        {
            RemoveResourcesFileFilter filter = new RemoveResourcesFileFilter(this.fileContents, "Remove Scan Cone FX", "Fx_Ship_Scan_Cone");
            DialogResult dialogResult = new FilterFileForm(this.fileContents, this.gameClient, this.demo.Server, filter).ShowDialog(this);

            if (dialogResult == DialogResult.OK)
            {
                this.statusStripLabel.Text = "Scan Cone FX removed";
                this.dirty = true;
                this.RefreshFileInfo();
            }
        }

        /// <summary>
        /// Starts up the "Remove FX" filter dialog
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void removeFx_Event(object sender, EventArgs e)
        {
            RemoveResourcesFileFilter filter = new RemoveResourcesFileFilter(this.fileContents, "Remove FX", "FxName");
            DialogResult dialogResult = new FilterFileForm(this.fileContents, this.gameClient, this.demo.Server, filter).ShowDialog(this);

            if (dialogResult == DialogResult.OK)
            {
                this.statusStripLabel.Text = "FX removed";
                this.dirty = true;
                this.RefreshFileInfo();
            }
        }


        /// <summary>
        /// Callback for Editor close. Checks for unsaved changes.
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void StoDemoEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.dirty)
            {
                DialogResult result = MessageBox.Show(this,
                    "Do you want to save the changes to " + GetSimpleFileName() + "?",
                    Application.ProductName,
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                if(result.Equals(DialogResult.Yes))
                {
                    if(!this.SaveFile(this.fileName, this.fileContents))
                    {
                        e.Cancel = true;
                    }
                }
                else if(result.Equals(DialogResult.Cancel))
                {
                    e.Cancel = true;
                }
            }
        }

// end callbacks

        /// <summary>
        /// Initializes the Demo Editor UI with file information from the demo
        /// </summary>
        private void RefreshFileInfo()
        {
            this.demo = DemoInfo.GetDemoFileInfo(this.fileName);
            if (this.demo != null)
            {
                this.Text = this.GetSimpleFileName().Substring(0, demo.FileName.LastIndexOf(".demo")) + " - Demo Editor";
                this.nameDisplayLabel.Text = demo.FileName;
                this.mapDisplayLabel.Text = demo.MapName;
                this.durationDisplayLabel.Text = StoDemoLauncherForm.CustomDurationConvert(demo.StartTime, demo.EndTime);
                this.characterDisplayLabel.Text = demo.Character;
                this.createdDisplayLabel.Text = StoDemoLauncherForm.CustomTimeConvert(demo.Create);
                this.modifiedDisplayLabel.Text = StoDemoLauncherForm.CustomTimeConvert(demo.Modify);
                this.serverDisplayLabel.Text = GameClient.GetServerName(demo.Server);

                this.saveDemoToolStripMenuItem.Enabled = this.dirty;
                this.saveDemoMenuItem.Enabled = this.dirty;
            }
            else
            {
                MessageBox.Show(this,
                    "This is not a valid demo file. The editor will now close,\n" +
                    "to prevent crashes or file corruption.",
                    "Invalid File",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.Close();
            }
        }

        /// <summary>
        /// Writes a list of strings to a text file
        /// </summary>
        /// <param name="fileName">The absolute path to the file to be written</param>
        /// <param name="fileContents">The contents to be written</param>
        /// <returns>true if operation was successful, false otherwise</returns>
        private bool SaveFile(string fileName, List<string> fileContents)
        {
            bool success = false;
            BusyForm.ShowBusyScreen(this);
            try
            {
                System.IO.File.WriteAllLines(fileName, fileContents.ToArray());
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    "An error occured while saving " + this.GetSimpleFileName() + ".\n" +
                    "Please report the following error:\n" +
                    ex.Message + "\n\n" +
                    "The demo file is still loaded in memory. You can try to\n" +
                    "save it under another name or at a different loacation\n" +
                    "using \"Save As\".",
                    "Error Saving File",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            BusyForm.HideBusyScreen();
            return success;
        }

        /// <summary>
        /// Returns the current file name with extension
        /// </summary>
        /// <returns>The filename with extension, but without path</returns>
        private string GetSimpleFileName()
        {
            return this.fileName.Substring(this.fileName.LastIndexOf("\\") + 1);
        }
    }
}
