using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StoDemoLauncher.Parser;
using StoDemoLauncher.Model;
using StoDemoLauncher.Helper;

namespace StoDemoLauncher.Filters
{
    /// <summary>
    /// UI control to determine RemoveNotifiactionSettings for a single file
    /// </summary>
    public partial class RemoveSectionsFileControl : UserControl
    {
        /// <summary>
        /// Filter settings
        /// </summary>
        RemoveSectionsSettings settings;

        /// <summary>
        /// Demo file contents
        /// </summary>
        List<string> fileContents;

        /// <summary>
        /// The parser to identify sections
        /// </summary>
        AbstractSectionsParser parser;

        /// <summary>
        /// Model data
        /// </summary>
        List<DemoSection> parserResults;

// begin callbacks

        /// <summary>
        /// Callback for "Select All" event
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void selectAll_Event(object sender, EventArgs e)
        {
            SelectAllNotifications();
        }

        /// <summary>
        /// Callback for "Select None" event
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event arguments</param>
        private void selectNone_Event(object sender, EventArgs e)
        {
            SelectNoneNotifications();
        }

        /// <summary>
        /// Callback for selection change in notification list box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notificationsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.settings.Sections = this.GetSelectedSections();
        }

        /// <summary>
        /// "Load" callback for RemoveMessagesFileControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveMessagesFileControl_Load(object sender, EventArgs e)
        {
            InitializeSectionsListBox();
        }

        /// <summary>
        /// callback for "TextChanged" event of filterListTextBox
        /// </summary>
        /// <param name="sender">Triggering UI element</param>
        /// <param name="e">Event Arguments</param>
        private void filterListTextBox_TextChanged(object sender, EventArgs e)
        {
            this.FilterListBox();
        }

// end callbacks

        /// <summary>
        /// Creates a new RemoveNotificationsFileControl
        /// </summary>
        /// <param name="settings">The settings of the owning filter</param>
        /// <param name="fileContents">The file contents to modify</param>
        /// <param name="parser">The parser to use for initializing the file
        /// and populating the settings</param>
        public RemoveSectionsFileControl(RemoveSectionsSettings settings, List<string> fileContents, AbstractSectionsParser parser)
        {
            InitializeComponent();
            this.settings = settings;
            this.fileContents = fileContents;
            this.parser = parser;
        }

        /// <summary>
        /// Populates the list box by parsing sections
        /// </summary>
        private void InitializeSectionsListBox()
        {
            ParserEngine.Parse(this.fileContents, parser);
            this.parserResults = parser.GetResult();
            this.FilterListBox();
        }

        /// <summary>
        /// Filters section list box
        /// </summary>
        private void FilterListBox()
        {
            this.sectionsListBox.BeginUpdate();
            this.sectionsListBox.Items.Clear();
            this.sectionsListBox.Items.AddRange(this.parserResults.FindAll(new Predicate<DemoSection>(FilterSection)).ToArray());
            this.SelectAllNotifications();
            this.sectionsListBox.EndUpdate();
        }


        /// <summary>
        /// Predicate method to match section with filter string
        /// </summary>
        /// <param name="section">section to be tested</param>
        /// <returns>True if section matches filter terms, false otherwise</returns>
        private bool FilterSection(DemoSection section)
        {
            return StringFilter.TestAllCaseInvariant(section.ToString(), this.filterListTextBox.Text, StringFilter.And);
        }

        /// <summary>
        /// Selects all notifications
        /// </summary>
        private void SelectAllNotifications()
        {
            for (int i = 0; i < this.sectionsListBox.Items.Count; i++)
            {
                this.sectionsListBox.SetSelected(i, true);
            }
        }

        /// <summary>
        /// Deselect all notifications
        /// </summary>
        private void SelectNoneNotifications()
        {
            this.sectionsListBox.ClearSelected();
        }

        /// <summary>
        /// Returns only selected notifications
        /// </summary>
        /// <returns></returns>
        private List<DemoSection> GetSelectedSections()
        {
            List<DemoSection> result = new List<DemoSection>();
            foreach (object message in this.sectionsListBox.SelectedItems)
            {
                result.Add((DemoSection)message);
            }
            return result;
        }
    }
}
