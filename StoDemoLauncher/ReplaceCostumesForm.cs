using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StoDemoLauncher.Parser;
using StoDemoLauncher.Model;
using StoDemoLauncher.Helper;

namespace StoDemoLauncher
{
    /// <summary>
    /// Dialog that allows users to replace an existing Costume from a demo
    /// file with a Costume from another demo file
    /// </summary>
    public partial class ReplaceCostumesForm : Form
    {
        /// <summary>
        /// The original demo file contents to modify
        /// </summary>
        private List<string> fileContents;

        /// <summary>
        /// The working copy of the demo file contents
        /// </summary>
        private List<string> targetContents;

        /// <summary>
        /// Star Trek Online game client
        /// </summary>
        private GameClient gameClient;

        /// <summary>
        /// The installation to use for preview
        /// </summary>
        private GameServer server;

        /// <summary>
        /// Second demo file from which to copy over Costumes
        /// </summary>
        private List<string> sourceContents;

        /// <summary>
        /// List of target file Costumes
        /// </summary>
        private List<DemoSection> targetCostumes;

        /// <summary>
        /// List of source file Costumes
        /// </summary>
        private List<DemoSection> sourceCostumes;

// begin callbacks

        /// <summary>
        /// Callback for form "Shown" event
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void ReplaceCostumesForm_Shown(object sender, EventArgs e)
        {
            BusyForm.ShowBusyScreen(this);
            UpdateTargetList();
            BusyForm.HideBusyScreen();
        }

        /// <summary>
        /// Callback for "Open" event
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void open_Event(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = this.gameClient.GetDemosPath(this.server);
            dialog.Filter = "Demo files (*.demo)|*.demo|All files (*.*)|*.*";

            DialogResult result = dialog.ShowDialog();

            if (result.Equals(DialogResult.OK))
            {
                BusyForm.ShowBusyScreen(this);

                // update UI
                this.sourceListBox.Items.Clear();

                // determine if it is a valid file
                string sourceFileName = dialog.FileName;
                DemoInfo sourceInfo = DemoInfo.GetDemoFileInfo(sourceFileName);
                if (sourceInfo != null)
                {
                    // parse the source demo for Costumes
                    this.sourceContents = this.gameClient.GetFileContents(sourceFileName);
                    this.UpdateSourceList();
                }
                else
                {
                    BusyForm.HideBusyScreen();
                    MessageBox.Show(this,
                        sourceFileName + " is no valid demo file.",
                        "Invalid File",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                BusyForm.HideBusyScreen();
            }
        }

        /// <summary>
        /// Callback for "Selected Index Changed" event of both list boxes
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void listBoxesSelectedIndexChanged_Event(object sender, EventArgs e)
        {
            this.replaceButton.Enabled = targetListBox.SelectedItems.Count == 1 && sourceListBox.SelectedItems.Count == 1; 
        }

        /// <summary>
        /// Callback for "Replace" event
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void replaceButton_Click(object sender, EventArgs e)
        {
            BusyForm.ShowBusyScreen(this);
            DemoCostume target = (DemoCostume)targetListBox.SelectedItem;
            DemoCostume source = (DemoCostume)sourceListBox.SelectedItem;
            this.targetContents.RemoveRange(target.StartLine, target.LineCount);
            this.targetContents.InsertRange(target.StartLine, sourceContents.GetRange(source.StartLine, source.LineCount));
            this.UpdateTargetList();
            BusyForm.HideBusyScreen();
        }

        /// <summary>
        /// Callback for "Preview" event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previewButton_Click(object sender, EventArgs e)
        {
            this.gameClient.PlayPreview(this.targetContents, this.server);
        }

        /// <summary>
        /// Callback for "OK" event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            this.fileContents.Clear();
            this.fileContents.AddRange(this.targetContents);
        }

        /// <summary>
        /// Callback for 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filterTargetListTextBox_TextChanged(object sender, EventArgs e)
        {
            this.FilterTargetList();
        }

        private void filterSourceListTextBox_TextChanged(object sender, EventArgs e)
        {
            this.FilterSourceList();
        }

// end callbacks

        /// <summary>
        /// Creates a new "Replace Costumes" dialog
        /// </summary>
        public ReplaceCostumesForm(List<string> fileContents, GameClient gameClient, GameServer server)
        {
            InitializeComponent();
            this.fileContents = fileContents;
            this.targetContents = new List<string>();
            this.targetContents.AddRange(fileContents);
            this.gameClient = gameClient;
            this.server = server;
            this.targetCostumes = new List<DemoSection>();
            this.sourceCostumes = new List<DemoSection>();
        }

        /// <summary>
        /// Updates the model and displays all target Costumes, that match the
        /// current filter
        /// </summary>
        private void UpdateTargetList()
        {
            this.targetCostumes.Clear();
            CostumeParser parser = new CostumeParser();
            ParserEngine.Parse(this.targetContents, parser);
            this.targetCostumes.AddRange(parser.GetResult().ToArray());
            this.FilterTargetList();
        }

        /// <summary>
        /// Updates the contents of the target list to match the filter text
        /// </summary>
        private void FilterTargetList()
        {
            this.targetListBox.BeginUpdate();
            this.targetListBox.Items.Clear();
            this.targetListBox.Items.AddRange(this.targetCostumes.FindAll(new Predicate<DemoSection>(ContainsTargetFilterText)).ToArray());
            this.targetListBox.EndUpdate();
        }

        private bool ContainsTargetFilterText(DemoSection section)
        {
            return StringFilter.TestAllCaseInvariant(section.ToString(), this.filterTargetListTextBox.Text, StringFilter.And);
        }

        /// Updates the model and displays all source Costumes, that match the
        /// current filter
        private void UpdateSourceList()
        {
            this.sourceCostumes.Clear();
            CostumeParser parser = new CostumeParser();
            ParserEngine.Parse(this.sourceContents, parser);
            this.sourceCostumes.AddRange(parser.GetResult().ToArray());
            this.FilterSourceList();
        }

        /// <summary>
        /// Updates the contents of the source list to match the filter text
        /// </summary>
        private void FilterSourceList()
        {
            this.sourceListBox.BeginUpdate();
            this.sourceListBox.Items.Clear();
            this.sourceListBox.Items.AddRange(this.sourceCostumes.FindAll(new Predicate<DemoSection>(ContainsSourceFilterText)).ToArray());
            this.sourceListBox.EndUpdate();
        }

        private bool ContainsSourceFilterText(DemoSection section)
        {
            return StringFilter.TestAllCaseInvariant(section.ToString(), this.filterSourceListTextBox.Text, StringFilter.And);
        }
    }
}
