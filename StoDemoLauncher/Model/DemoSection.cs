using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoDemoLauncher.Model
{
    /// <summary>
    /// Some section from a Demo file
    /// </summary>
    public class DemoSection
    {
        /// <summary>
        /// Line number of section start
        /// </summary>
        public int StartLine { get; set; }

        /// <summary>
        /// Line number of section end
        /// </summary>
        public int EndLine { get; set; }

        /// <summary>
        /// Returns the number of lines thi section spans
        /// </summary>
        public int LineCount
        {
            get
            {
                return EndLine - StartLine;
            }
        }
    }
}
