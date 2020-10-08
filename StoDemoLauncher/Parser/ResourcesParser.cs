using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoDemoLauncher.Model;

namespace StoDemoLauncher.Parser
{
    /// <summary>
    /// Parses a demo file for "Resources" sections which "constant" field
    /// contains a given search string.
    /// </summary>
    class ResourcesParser : AbstractSectionsParser
    {
        /// <summary>
        /// The list of parsed resources
        /// </summary>
        List<DemoSection> result = new List<DemoSection>();

        /// <summary>
        /// The string to search for in the constant field
        /// </summary>
        string constantSearchString;

        // placeholders
        int braceLevel = 0;
        int lastResourcesBraceLevel = 0;
        int lastResourcesSectionStart = 0;
        bool inResourcesSection = false;
        bool inSearchSection = false;
        int handle = -1;
        int id = -1;
        string constant = "";
        List<DemoState> states = new List<DemoState>();

        /// <summary>
        /// Creates a new ResourceParser object
        /// </summary>
        /// <param name="constantSearchString">The parser will only collect
        /// those resource sections which constant field contains this string.</param>
        public ResourcesParser(string constantSearchString)
        {
            this.constantSearchString = constantSearchString;
        }

        /// <summary>
        /// Is called by the parser when it has fetched a new line
        /// </summary>
        /// <param name="line">Content of the line</param>
        /// <param name="lineNumber">Current line number (0 index)</param>
        public override void NewLine(string line, int lineNumber)
        {
            if (line.Trim().ToLowerInvariant().StartsWith("resources"))
            {
                lastResourcesSectionStart = lineNumber;
                lastResourcesBraceLevel = braceLevel;
                inResourcesSection = true;
            }
            if (inResourcesSection && line.Trim().StartsWith("Handle"))
            {
                this.handle = Convert.ToInt32(line.Substring(line.IndexOf("Handle ") + 7));
            }
            if (inResourcesSection && line.Trim().StartsWith("ID"))
            {
                this.id = Convert.ToInt32(line.Substring(line.IndexOf("ID ") + 3));
            }
            if (inResourcesSection && line.Contains(constantSearchString))
            {
                inSearchSection = true;
                constant = line.Substring(line.IndexOf("constant ") + 9);
            }
            if (inResourcesSection && line.Trim().StartsWith("States"))
            {
                // TODO be clever about this
            }
            if (inSearchSection && line.Trim().StartsWith("}") && braceLevel - 1 == lastResourcesBraceLevel)
            {
                inResourcesSection = false;
                inSearchSection = false;
                DemoResource resource = new DemoResource();
                resource.StartLine = lastResourcesSectionStart - 1;
                resource.EndLine = lineNumber;
                resource.Constant = constant;
                resource.Handle = handle;
                resource.Id = id;
                result.Add(resource);
            }
            if (line.EndsWith("{")) braceLevel++;
            if (line.EndsWith("}")) braceLevel--;
        }

        /// <summary>
        /// Returns the list with the found resources.
        /// </summary>
        /// <returns></returns>
        override public List<DemoSection> GetResult()
        {
            return result;
        }
    }
}
