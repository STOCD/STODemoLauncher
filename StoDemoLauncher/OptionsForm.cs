using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StoDemoLauncher.Helper;

namespace StoDemoLauncher
{
    /// <summary>
    /// Options dialog for StoDemoLauncher
    /// </summary>
    public partial class OptionsForm : Form
    {
        GameClient gameClient;
        ConfigurationFile config;

// begin callbacks

        /// <summary>
        /// "Load" event
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void Load_Event(object sender, EventArgs e)
        {
            // General Tab
            this.demosListViewDoubleClickComboBox.SelectedIndex = config.GetIntValue(StoDemoLauncherForm.DemosListViewIniGroup, StoDemoLauncherForm.DoubleClickActionIniKey);
            this.demosListViewEnterComboBox.SelectedIndex = config.GetIntValue(StoDemoLauncherForm.DemosListViewIniGroup, StoDemoLauncherForm.EnterPressActionIniKey);
            this.confirmDeleteCheckBox.Checked = config.GetBoolValue(StoDemoLauncherForm.DemosListViewIniGroup, StoDemoLauncherForm.ConfirmDeleteIniKey);
            // Auto-Update group
            this.autoUpdateCheckBox.Checked = config.GetBoolValue(StoDemoLauncherForm.DemoLauncherIniGroup, StoDemoLauncherForm.EnableAutoUpdateIniKey);
            this.autoUpdateNumericUpDown.Value = config.GetIntValue(StoDemoLauncherForm.DemoLauncherIniGroup, StoDemoLauncherForm.AutoUpdateIntervalMsIniKey) / 3600000;
            // Recently Used File List group
            this.keepMruListCheckBox.Checked = config.GetBoolValue(MruFileList.MruFileListIniGroup, MruFileList.EnableMruFileListIniKey);
            this.mruCountNumericUpDown.Value = config.GetIntValue(MruFileList.MruFileListIniGroup, MruFileList.MaxEntryCountIniKey);

            // Advanced Tab
            // Game Client Group
            this.stoInstallationPathTextBox.Text = this.gameClient.InstallLocation;
            this.holodeckPathTextBox.Text = this.gameClient.HolodeckPath;
            this.tribblePathTextBox.Text = this.gameClient.TribblePath;
            this.redshirtPathTextBox.Text = this.gameClient.RedshirtPath;
        }

        /// <summary>
        /// "Clear List" Button callback
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void clearMruListButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this,
                "Do you really want to delete the most recently\n" +
                "used file list?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (result.Equals(DialogResult.Yes))
            {
                MruFileList fileList = new MruFileList();
                fileList.Clear();
                fileList.SaveToConfig();
            }
        }

        /// <summary>
        /// Validates path text boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox textBox = (TextBox)sender;
                if (textBox.Text.IndexOfAny(System.IO.Path.GetInvalidPathChars()) != -1)
                {
                    textBox.Undo();
                    MessageBox.Show(
                     this,
                     "You may not use the following charactes in a file name:\n" +
                     "\" < > | [space] [tab]",
                     "Invalid Characters",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// "Browse..."
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void browseStoInstallationPathBrowse_Event(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(this.stoInstallationPathTextBox.Text)) this.folderBrowserDialog.SelectedPath = this.stoInstallationPathTextBox.Text;
            DialogResult dialogResult = this.folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                this.stoInstallationPathTextBox.Text = folderBrowserDialog.SelectedPath;
                if (!GameClient.ValidateStoInstallationPath(this.stoInstallationPathTextBox.Text))
                {
                    MessageBox.Show(this,
                        "The folder you selected does not seem to be the folder\n" +
                        "of the game launcher. Please check, if it contains\n" +
                        "\"Star Trek Online.exe\" and a sub-folder named\n" +
                        "\"\\Star Trek Online\\Live\".",
                        "Invalid Game Launcher Folder",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// "Browse..."
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void browseHolodeckPathBrowse_Event(object sender, EventArgs e)
        {
            this.BrowseServerPath(this.holodeckPathTextBox);
        }

        /// <summary>
        /// "Browse..."
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void browseTribblePathBrowse_Event(object sender, EventArgs e)
        {
            this.BrowseServerPath(this.tribblePathTextBox);
        }

        /// <summary>
        /// "Browse..."
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void browseRedshirtPathBrowse_Event(object sender, EventArgs e)
        {
            this.BrowseServerPath(this.redshirtPathTextBox);
        }

        /// <summary>
        /// "Defaults" event
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void defaults_Event(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(this,
                "This will restore default settings for all options. Do you\n" +
                "want to continue?",
                "Restore Default Settings",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (dialogResult == DialogResult.Yes)
            {
                // General tab
                // Demos List View group
                this.demosListViewDoubleClickComboBox.SelectedIndex = 1;
                this.demosListViewEnterComboBox.SelectedIndex = 1;
                this.confirmDeleteCheckBox.Checked = true;
                // Auto-Update group
                this.autoUpdateCheckBox.Checked = true;
                this.autoUpdateNumericUpDown.Value = 6;
                // Recently Used File List group
                this.keepMruListCheckBox.Checked = MruFileList.EnableMruFileListDefault;
                this.mruCountNumericUpDown.Value = MruFileList.MaxEntryCountDefault;


                // Advanced tab
                // Game Client group
                string stoInstallDirectory = GameClient.FindStoDirectory();
                if (stoInstallDirectory != "")
                {
                    this.gameClient.UpdatePaths(stoInstallDirectory);
                }
                this.stoInstallationPathTextBox.Text = this.gameClient.InstallLocation;
                this.holodeckPathTextBox.Text = this.gameClient.HolodeckPath;
                this.tribblePathTextBox.Text = this.gameClient.TribblePath;
                this.redshirtPathTextBox.Text = this.gameClient.RedshirtPath;
            }
        }

        /// <summary>
        /// "OK" event
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void ok_Event(object sender, EventArgs e)
        {
            // General tab
            // Demos List View group
            config.PutValue(StoDemoLauncherForm.DemosListViewIniGroup, StoDemoLauncherForm.DoubleClickActionIniKey, this.demosListViewDoubleClickComboBox.SelectedIndex);
            config.PutValue(StoDemoLauncherForm.DemosListViewIniGroup, StoDemoLauncherForm.EnterPressActionIniKey, this.demosListViewEnterComboBox.SelectedIndex);
            config.PutValue(StoDemoLauncherForm.DemosListViewIniGroup, StoDemoLauncherForm.ConfirmDeleteIniKey, this.confirmDeleteCheckBox.Checked);
            // Auto-Update group
            config.PutValue(StoDemoLauncherForm.DemoLauncherIniGroup, StoDemoLauncherForm.EnableAutoUpdateIniKey, this.autoUpdateCheckBox.Checked);
            config.PutValue(StoDemoLauncherForm.DemoLauncherIniGroup, StoDemoLauncherForm.AutoUpdateIntervalMsIniKey, System.Convert.ToInt32(this.autoUpdateNumericUpDown.Value) * 3600000);
            // Recently Used File List group
            config.PutValue(MruFileList.MruFileListIniGroup, MruFileList.EnableMruFileListIniKey, this.keepMruListCheckBox.Checked);
            config.PutValue(MruFileList.MruFileListIniGroup, MruFileList.MaxEntryCountIniKey, System.Convert.ToInt32(this.mruCountNumericUpDown.Value));

            // Advanced tab
            // Game Client group
            // Game client path
            if (GameClient.ValidateStoInstallationPath(this.stoInstallationPathTextBox.Text))
            {
                this.config.PutValue(GameClient.GameClientIniGroup, GameClient.PathIniKey, this.stoInstallationPathTextBox.Text);
            }
            else
            {
                this.stoInstallationPathTextBox.Text = this.gameClient.InstallLocation;
                MessageBox.Show(this,
                    "The folder you selected does not seem to be the folder\n" +
                    "of the game launcher. Your changes to the game launcher\n" +
                    "path will be ignored.",
                    "Invalid Game Launcher Folder",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            // Holodeck path
            if (GameClient.ValidateServerPath(this.holodeckPathTextBox.Text))
            {
                this.config.PutValue(GameClient.GameClientIniGroup, GameClient.HolodeckPathIniKey, this.holodeckPathTextBox.Text);
            }
            else
            {
                this.holodeckPathTextBox.Text = this.gameClient.HolodeckPath;
                MessageBox.Show(this,
                    "The folder you selected does not seem to be a game client\n" +
                    "installation. Your changes to the Holodeck client path\n" +
                    "will be ignored.",
                    "Invalid Game Client Folder",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            // Tribble path
            if (GameClient.ValidateServerPath(this.tribblePathTextBox.Text))
            {
                this.config.PutValue(GameClient.GameClientIniGroup, GameClient.TribblePathIniKey, this.tribblePathTextBox.Text);
            }
            else
            {
                this.tribblePathTextBox.Text = this.gameClient.TribblePath;
                MessageBox.Show(this,
                    "The folder you selected does not seem to be a game client\n" +
                    "installation. Your changes to the Tribble client path\n" +
                    "will be ignored.",
                    "Invalid Game Client Folder",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            this.gameClient.UpdatePaths(this.stoInstallationPathTextBox.Text);
            // Redshirt path
            if (GameClient.ValidateServerPath(this.redshirtPathTextBox.Text))
            {
                this.config.PutValue(GameClient.GameClientIniGroup, GameClient.RedshirtPathIniKey, this.redshirtPathTextBox.Text);
            }
            else
            {
                this.redshirtPathTextBox.Text = this.gameClient.RedshirtPath;
                MessageBox.Show(this,
                    "The folder you selected does not seem to be a game client\n" +
                    "installation. Your changes to the Redshirt client path\n" +
                    "will be ignored.",
                    "Invalid Game Client Folder",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            this.gameClient.UpdatePaths(this.stoInstallationPathTextBox.Text);
        }

// end callbacks

        /// <summary>
        /// Created a new Options dialog form
        /// </summary>
        public OptionsForm(GameClient gameClient)
        {
            InitializeComponent();
            this.gameClient = gameClient;
            this.config = ConfigurationFile.GetInstance();
        }

        /// <summary>
        /// Opens a folder browser and checks for a valid installation path
        /// </summary>
        /// <param name="textBox">Textbox to hold the path</param>
        private void BrowseServerPath(TextBox textBox)
        {
            if (System.IO.Directory.Exists(textBox.Text)) this.folderBrowserDialog.SelectedPath = textBox.Text;
            DialogResult dialogResult = this.folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                textBox.Text = folderBrowserDialog.SelectedPath;
                if (!GameClient.ValidateServerPath(textBox.Text))
                {
                    MessageBox.Show(this,
                        "The folder you selected does not seem to be a\n" +
                        "valid game client installation. Please check, if\n" +
                        "it contains a \"GameClient.exe\".",
                        "Invalid Game Client Folder",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
        }
    }
}
