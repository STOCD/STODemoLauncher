using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StoDemoLauncher.Model;
using System.Threading;

namespace StoDemoLauncher.Filters
{
    /// <summary>
    /// Filter form to modify a 
    /// </summary>
    public partial class FilterFileForm : Form
    {
        /// <summary>
        /// The demo file contents to modify
        /// </summary>
        private List<string> fileContents;

        /// <summary>
        /// Star Trek Online game client
        /// </summary>
        private GameClient gameClient;

        /// <summary>
        /// The installation to use for preview
        /// </summary>
        private GameServer server;

        /// <summary>
        /// The filter to use
        /// </summary>
        private AbstractFileFilter filter = null;

//begin callbacks

        /// <summary>
        /// Writes a preview file to disk and runs the client
        /// </summary>
        /// <param name="sender">Triggering UI component</param>
        /// <param name="e">Event arguments</param>
        private void preview_Event(object sender, EventArgs e)
        {
            List<string> filterResult = this.filter.ApplyFilter();
            this.gameClient.PlayPreview(filterResult, this.server);

            //string previewFileName = gameClient.GetDemosPath(this.server) + "\\StoDemoLauncherPreview.demo";
            //System.IO.File.WriteAllLines(previewFileName, filterResult.ToArray());
            //if(System.IO.File.Exists(previewFileName))
            //{
            //    this.gameClient.PlayDemo(DemoInfo.GetDemoFileInfo(previewFileName));
            //}
        }

        /// <summary>
        /// Applies the changes of the filter to the file contents and closes
        /// the window.
        /// </summary>
        /// <param name="sender">Triggering UI component</param>
        /// <param name="e">Event arguments</param>
        private void ok_Event(object sender, EventArgs e)
        {
            List<string> filteredFile = filter.ApplyFilter();
            this.fileContents.Clear();
            this.fileContents.AddRange(filteredFile);
            this.Close();
        }

        /// <summary>
        /// Shown callback for FilterFileForm
        /// </summary>
        /// <param name="sender">Triggering UI component</param>
        /// <param name="e">Event arguments</param>
        private void FilterFileForm_Shown(object sender, EventArgs e)
        {
            BusyForm.ShowBusyScreen(this);
            this.Text = filter.Text;
            this.filterPlaceholder.Controls.Add(this.filter.FilterControl);
            this.filter.FilterControl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            BusyForm.HideBusyScreen();
        }

//end callbacks

        /// <summary>
        /// Creates a new filter dialog form
        /// </summary>
        /// <param name="filter"></param>
        public FilterFileForm(List<string> fileContents, GameClient gameClient, GameServer server, AbstractFileFilter filter)
        {
            InitializeComponent();
            this.fileContents = fileContents;
            this.gameClient = gameClient;
            this.server = server;
            this.filter = filter;
        }
    }
}
