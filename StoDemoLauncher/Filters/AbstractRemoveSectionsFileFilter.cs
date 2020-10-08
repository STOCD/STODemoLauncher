using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoDemoLauncher.Model;

namespace StoDemoLauncher.Filters
{
    /// <summary>
    /// An abstract filter to model filters that remove whole sections of text
    /// lines from a demo file. Settings must be RemoveSectionsSettings
    /// </summary>
    public abstract class AbstractRemoveSectionsFileFilter : AbstractFileFilter
    {
        public AbstractRemoveSectionsFileFilter(List<string> fileContents)
            : base(fileContents)
        {
            this.settings = new RemoveSectionsSettings();
        }

        /// <summary>
        /// Runs the filter with given settings on a file
        /// </summary>
        /// <returns>The modified file contents</returns>
        public override List<string> ApplyFilter()
        {
            List<string> result = new List<string>();
            List<DemoSection> sections = new List<DemoSection>();
            sections.AddRange(((RemoveSectionsSettings)this.settings).Sections);

            // placeholders
            long lineNumber = 0;

            // modify file
            foreach (string line in this.FileContents)
            {
                if (sections.Count == 0 || lineNumber < sections[0].StartLine)
                {
                    result.Add(line);
                }
                else if (lineNumber == sections[0].EndLine)
                {
                    sections.Remove(sections[0]);
                }
                lineNumber++;
            }

            // return modified file
            return result;
        }
    }
}
