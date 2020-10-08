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
    /// Filter to remove "Resources" sections from demo files
    /// </summary>
    public class RemoveResourcesFileFilter : AbstractRemoveSectionsFileFilter
    {
        /// <summary>
        /// Creates a new RemoveNotificationsFilter for a given file
        /// </summary>
        /// <param name="fileContents">The contents of the demo file to be modified</param>
        public RemoveResourcesFileFilter(List<string> fileContents, string text, string constantSearchString)
            : base(fileContents)
        {
            this.Text = text;
            this.filterControl = new RemoveSectionsFileControl((RemoveSectionsSettings)this.Settings, fileContents, new ResourcesParser(constantSearchString));
        }
    }
}
