using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoDemoLauncher.Model;

namespace StoDemoLauncher.Filters
{
    /// <summary>
    /// Settings object for a filter that removes complete section from a demo
    /// file
    /// </summary>
    public class RemoveSectionsSettings : InterfaceFilterSettings
    {
        /// <summary>
        /// The sections to be removed
        /// </summary>
        public List<DemoSection> Sections { get; set; }

        /// <summary>
        /// Creates a new settings object
        /// </summary>
        public RemoveSectionsSettings()
        {
            this.Sections = new List<DemoSection>();
        }
    }
}
