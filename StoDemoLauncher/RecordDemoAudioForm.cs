using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StoDemoLauncher.Model;

namespace StoDemoLauncher
{
    /// <summary>
    /// Querries audio recording settings and can start audio recording
    /// </summary>
    public partial class RecordDemoAudioForm : Form
    {
        /// <summary>
        /// The demo for which to record audio
        /// </summary>
        DemoInfo demo;

        /// <summary>
        /// The client to use for recording
        /// </summary>
        GameClient gameClient;

        /// <summary>
        /// Creates a new RecordDemoAudioForm
        /// </summary>
        /// <param name="demo">The demo for which to record audio</param>
        /// <param name="gameClient">The gameclient to record audio with</param>
        public RecordDemoAudioForm(DemoInfo demo, GameClient gameClient, string initialFileName)
        {
            InitializeComponent();
            this.demo = demo;
            this.gameClient = gameClient;
            this.fileNameTextBox.Text = initialFileName;
            this.fileNameTextBox.SelectAll();
            this.outputFolderComboBox.SelectedIndex = 1;
        }

        /// <summary>
        /// Closes the dialog without consequence
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void cancel_Event(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Starts rendering
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void ok_Event(object sender, EventArgs e)
        {
            this.StartRecording();
        }

        /// <summary>
        /// Starts recording audio with "Star Trek Online"
        /// </summary>
        private void StartRecording()
        {
            string filename = fileNameTextBox.Text;
            if (!filename.EndsWith(".wav")) filename += ".wav";
            if(this.outputFolderComboBox.SelectedIndex == 0)
                this.gameClient.RecordDemoAudio30(demo, this.gameClient.GetDemosPath(this.demo.Server) + "\\" + fileNameTextBox.Text);
            else
                this.gameClient.RecordDemoAudio30(demo, this.gameClient.GetScreenshotsPath(this.demo.Server) + "\\" + fileNameTextBox.Text);
            this.Close();
        }

        /// <summary>
        /// Callback for "TextChanged" event from fileNameTextBox
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void fileNameTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) != -1)
            {
                textBox.Undo();
                MessageBox.Show(
                     this,
                     "You may not use the following charactes in a file name:\n" +
                     "\\ / : * ? \" < > |",
                     "Invalid Characters",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error);
            }
        }
    }
}
