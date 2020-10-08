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
    /// Querries render settings and can start rendering
    /// </summary>
    public partial class RenderDemoForm : Form
    {
        /// <summary>
        /// The demo to be rendered
        /// </summary>
        DemoInfo demo;

        /// <summary>
        /// The client to use for rendering
        /// </summary>
        GameClient gameClient;

        /// <summary>
        /// Creates a new "Render Demo" dialog
        /// </summary>
        /// <param name="demo">The DemoFileInfo with the information on the
        /// demo to render</param>
        /// <param name="gameClient">The StoGameClient to use for rendering</param>
        public RenderDemoForm(DemoInfo demo, GameClient gameClient, string prefix)
        {
            InitializeComponent();
            this.demo = demo;
            this.gameClient = gameClient;
            this.screenshotPrefixTextBox.Text = prefix;
            this.screenshotPrefixTextBox.SelectAll();
            this.screenshotTypeComboBox.SelectedIndex = 0;
            this.resolutionPresetsComboBox.SelectedIndex = 3;
            this.shadowMapSizeComboBox.SelectedIndex = 2;
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
            string preferencesPath = gameClient.GetPath(this.demo.Server) + "/localdata/gameprefs.pref";
            //MessageBox.Show(
            //    this,
            //    "Your in-game video settings are overridden by the render\n" +
            //    "settings. The demo launcher will create a copy of your\n" +
            //    "settings, so you can restore them after rendering has\n" +
            //    "finished. Click \"OK\" to create the backup and start\n" +
            //    "rendering.",
            //    "Creating Backup",
            //    MessageBoxButtons.OK,
            //    MessageBoxIcon.Information);
            List<string> preferencesContents = this.gameClient.GetFileContents(preferencesPath);
            this.StartRendering();
            System.Threading.Thread.Sleep(10000);
            MessageBox.Show(
                this,
                "Please wait for \"Star Trek Online\" to finish rendering the\n"+
                "demo. When rendering has completed, close \"Star Trek Online\"\n" +
                "and click \"OK\" to restore your in-game video settings.",
                "Creating Backup",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            System.IO.File.WriteAllLines(preferencesPath, preferencesContents.ToArray());
        }

        /// <summary>
        /// Rund the game client in -demo_save_images mode with the given
        /// settings
        /// </summary>
        private void StartRendering()
        {
            RenderSettings settings = new RenderSettings();
            // Image sequence prefix
            settings.Prefix = this.screenshotPrefixTextBox.Text;

            // Image format
            if (this.screenshotTypeComboBox.SelectedIndex == 1) settings.ImageFileFormat = ImageFileExtension.TGA;
            else settings.ImageFileFormat = ImageFileExtension.JPG;

            // Image dimensions
            settings.ChangeSize = System.Convert.ToInt32(!this.useGameResolutionCheckBox.Checked);
            settings.Width = System.Convert.ToInt32(this.widthNumericUpDown.Value);
            settings.Height = System.Convert.ToInt32(this.heightNumericUpDown.Value);
            settings.RenderScale = this.doubleResolutionCheckBox.Checked ? 2 : 1;
            settings.ScreenshotAfterRenderScale = System.Convert.ToInt32(this.doubleResolutionCheckBox.Checked);
            settings.Fov = System.Convert.ToInt32(this.fovNumericUpDown.Value);

            // Quality
            settings.PostProcessing = System.Convert.ToInt32(this.PostProcessingCheckBox.Checked);
            settings.HighQualityDepthOfField = System.Convert.ToInt32(this.highQualityDofCheckBox.Checked);
            settings.ReduceMipMaps = System.Convert.ToInt32(this.reduceMipMapsCheckBox.Checked);
            settings.DontReduceTextures = System.Convert.ToInt32(!this.reduceTexturesCheckBox.Checked);
            settings.BloomQuality = System.Convert.ToInt32(this.bloomQualityNumericUpDown.Value);
            settings.VisScale = System.Convert.ToInt32(this.visScaleNumericUpDown.Value);
            settings.Shadows = System.Convert.ToInt32(this.shadowsCheckBox.Checked);
            settings.ShadowMapSize = System.Convert.ToInt32(this.shadowMapSizeComboBox.SelectedItem.ToString());

            // HUD
            settings.UiToggleHud = System.Convert.ToInt32(this.hudCheckBox.Checked);
            settings.ShowChatBubbles = System.Convert.ToInt32(this.chatBubblesCheckBox.Checked);
            settings.ShowFloatingText = System.Convert.ToInt32(this.floatingTextCheckBox.Checked);
            settings.ShowEntityUi = System.Convert.ToInt32(this.userInteraceCheckBox.Checked);
            settings.ShowFps = System.Convert.ToInt32(this.fpsCounterCheckBox.Checked);
            settings.ShowLessAnnoyingAccessLevelWarning = System.Convert.ToInt32(!this.warningsCheckBox.Checked);

            // Run
            this.gameClient.RenderDemo(demo, settings);
            this.Close();
        }

        /// <summary>
        /// Callback for "TextChanged" event from screenshotPrefixTextBox
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void screenshotPrefixTextBox_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) != -1)
            {
                ((TextBox)sender).Undo();
                MessageBox.Show(
                     this,
                     "You may not use the following charactes in a file name:\n" +
                     "\\ / : * ? \" < > |",
                     "Invalid Characters",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Synchronizes NumericUpDowns with ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resolutionPresetsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 240p (320 x 240)
            if (this.resolutionPresetsComboBox.SelectedIndex == 0)
            {
                this.widthNumericUpDown.Value = 320;
                this.heightNumericUpDown.Value = 240;
            }
            // 360p (640 x 360)
            else if (this.resolutionPresetsComboBox.SelectedIndex == 1)
            {
                this.widthNumericUpDown.Value = 640;
                this.heightNumericUpDown.Value = 360;
            }
            // 480p (640 x 480)
            else if (this.resolutionPresetsComboBox.SelectedIndex == 2)
            {
                this.widthNumericUpDown.Value = 640;
                this.heightNumericUpDown.Value = 480;
            }
            // 720p (1280 x 720)
            else if (this.resolutionPresetsComboBox.SelectedIndex == 3)
            {
                this.widthNumericUpDown.Value = 1280;
                this.heightNumericUpDown.Value = 720;
            }
            // 1080p (1920 x 1080)
            else if (this.resolutionPresetsComboBox.SelectedIndex == 4)
            {
                this.widthNumericUpDown.Value = 1920;
                this.heightNumericUpDown.Value = 1080;
            }
        }

        private void useGameResolutionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.resolutionPresetsComboBox.Enabled = !this.useGameResolutionCheckBox.Checked;
            this.widthNumericUpDown.Enabled = !this.useGameResolutionCheckBox.Checked;
            this.heightNumericUpDown.Enabled = !this.useGameResolutionCheckBox.Checked;
        }

        private void resolutionChanged_Event(object sender, EventArgs e)
        {
            if (this.widthNumericUpDown.Value == 320 && this.heightNumericUpDown.Value == 240)
            {
                this.resolutionPresetsComboBox.SelectedIndex = 0;
            }
            else if (this.widthNumericUpDown.Value == 640 && this.heightNumericUpDown.Value == 360)
            {
                this.resolutionPresetsComboBox.SelectedIndex = 1;
            }
            else if (this.widthNumericUpDown.Value == 640 && this.heightNumericUpDown.Value == 480)
            {
                this.resolutionPresetsComboBox.SelectedIndex = 2;
            }
            else if (this.widthNumericUpDown.Value == 1280 && this.heightNumericUpDown.Value == 720)
            {
                this.resolutionPresetsComboBox.SelectedIndex = 3;
            }
            else if (this.widthNumericUpDown.Value == 1920 && this.heightNumericUpDown.Value == 1080)
            {
                this.resolutionPresetsComboBox.SelectedIndex = 4;
            }
            else this.resolutionPresetsComboBox.SelectedIndex = 5;
        }
    }
}
