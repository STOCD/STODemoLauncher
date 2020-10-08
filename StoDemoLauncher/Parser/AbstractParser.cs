using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoDemoLauncher.Parser
{
    /// <summary>
    /// Must be implemented by objects that want to use the DemoFileParser.
    /// Provides an empty implementation for conveniance
    /// </summary>
    abstract public class AbstractParser
    {
        /// <summary>
        /// Is called before the first line is parsed.
        /// </summary>
        /// <param name="filename">Absolute path to the file being parsed</param>
        public virtual void StartParsing(string filename)
        {
        }

        /// <summary>
        /// Is called by the parser when it has fetched a new line
        /// </summary>
        /// <param name="line">Content of the line</param>
        /// <param name="lineNumber">Current line number (0 index)</param>
        public abstract void NewLine(string line, int lineNumber);

        /// <summary>
        /// Is called before each line is read. When you return true, parsing
        /// will stop prematurely.
        /// </summary>
        /// <returns>true if this plugin does not want to  process any more
        /// lines</returns>
        public virtual bool HasAllInformation()
        {
            return false;
        }

        /// <summary>
        /// Is called, when all lines have been read by the parser.
        /// </summary>
        /// <param name="lineCount">Total number of lines in the demo file.</param>
        public virtual void StopParsing(long lineCount)
        {
        }
    }
}
