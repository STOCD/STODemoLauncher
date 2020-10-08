using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoDemoLauncher.Model;
using System.Windows.Forms;
using StoDemoLauncher.Parser;

namespace StoDemoLauncher.Filters
{
    /// <summary>
    /// Filter to remove notification messages from demo files
    /// </summary>
    public class RemoveMessagesFileFilter : AbstractRemoveSectionsFileFilter
    {
        /// <summary>
        /// Creates a new RemoveNotificationsFilter for a given file
        /// </summary>
        /// <param name="fileContents">The contents of the demo file to be modified</param>
        public RemoveMessagesFileFilter(List<string> fileContents, string text, string messageSearchString) : base(fileContents)
        {
            this.Text = text;
            this.filterControl = new RemoveSectionsFileControl((RemoveSectionsSettings)this.Settings, this.FileContents, new MessagesParser(messageSearchString));
        }

    }
}
