using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoDemoLauncher.Parser
{
    class IniParser : AbstractParser
    {
        private Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>();

        // placeholders
        string activeGroup = "";

        /// <summary>
        /// Is called by the parser when it has fetched a new line
        /// </summary>
        /// <param name="line">Content of the line</param>
        /// <param name="lineNumber">Current line number (0 index)</param>
        public override void NewLine(string line, int lineNumber)
        {
            string trimmedLine = line.Trim();

            // ignore comments
            if (!(trimmedLine.StartsWith("/") || trimmedLine.StartsWith("#") || trimmedLine.StartsWith(";")))
            {
                // Add group
                if(trimmedLine.StartsWith("[") && trimmedLine.Contains("]"))
                {
                    string groupName = trimmedLine.Substring(1);
                    groupName = groupName.Substring(0, groupName.LastIndexOf("]"));
                    if(!this.result.Keys.Contains(groupName)) result.Add(groupName, new Dictionary<string, string>());
                    this.activeGroup = groupName;
                }
                // Add key-value pair
                if (trimmedLine.Contains("="))
                {
                    string key = trimmedLine.Substring(0, trimmedLine.IndexOf("=")).Trim();
                    string value = trimmedLine.Substring(trimmedLine.IndexOf("=") + 1).Trim();
                    // Override existing key-value pair
                    if (this.result[this.activeGroup].Keys.Contains(key)) this.result[this.activeGroup][key] = value;
                    // Add non-existing key-value pair
                    else this.result[this.activeGroup].Add(key, value);
                }
            }
        }

        /// <summary>
        /// Returns the list with the found resources.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Dictionary<string, string>> GetResult()
        {
            return this.result;
        }
    }
}
