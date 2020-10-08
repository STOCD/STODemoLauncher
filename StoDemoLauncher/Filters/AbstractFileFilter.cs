using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StoDemoLauncher.Filters
{
    /// <summary>
    /// All filters must extend this class. Provide a InterfaceFilterSettings
    /// implementation to store settings. Provide a UI to edit a single file.
    /// The user control obtains the settings through the constructor.
    /// </summary>
    public abstract class AbstractFileFilter
    {
        /// <summary>
        /// Human readable identifier of the filter
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Form to modify filter settings
        /// </summary>
        protected UserControl filterControl;

        /// <summary>
        /// Progress bar owned by the filter form UI
        /// </summary>
        public ToolStripProgressBar ProgressBar { get; set; }

        /// <summary>
        /// The settings of the filer
        /// </summary>
        protected InterfaceFilterSettings settings;

        /// <summary>
        /// The contents of the file to be modified
        /// </summary>
        public List<string> FileContents { get; set; }

        /// <summary>
        /// The control element to change settings
        /// </summary>
        /// <returns>The UI control to specify filter settings</returns>
        public UserControl FilterControl
        {
            get
            {
                return this.filterControl;
            }
        }

        /// <summary>
        /// The settings for this filter
        /// </summary>
        /// <returns>The current setting for this filter</returns>
        public InterfaceFilterSettings Settings
        {
            get
            {
                return this.settings;
            }
        }

        /// <summary>
        /// Constructer for filtering an individual file
        /// </summary>
        /// <param name="fileContents">The file to filter, line by line</param>
        public AbstractFileFilter(List<string> fileContents)
        {
            this.filterControl = null;
            this.FileContents = fileContents;
        }

        /// <summary>
        /// Run the filter on current file with the current settings
        /// </summary>
        /// <returns>The modified file contents</returns>
        public abstract List<string> ApplyFilter();
    }
}
