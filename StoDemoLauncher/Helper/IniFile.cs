using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoDemoLauncher.Parser;

namespace StoDemoLauncher.Helper
{
    /// <summary>
    /// A configuration text file organizing key-value pairs in groups. A group
    /// is declared with "[GroupName]", key-value pair is declared with
    /// "key = value". Empty lines and lines starting with /, #, or ; are
    /// ignored as comments
    /// </summary>
    public class IniFile
    {
        /// <summary>
        /// Configuration organized in groups of key-value pairs
        /// </summary>
        protected Dictionary<string, Dictionary<string, string>> groups = null;

        /// <summary>
        /// Returns the group with the given name. If the group does not
        /// exist, an empty group is created
        /// </summary>
        /// <param name="groupName">Name of the group to return</param>
        /// <returns>A dictionary of all key-value pairs in the group</returns>
        public Dictionary<string, string> GetGroup(string groupName)
        {
            Dictionary<string, string> result = null;
            if(this.groups.Keys.Contains(groupName)) result = this.groups[groupName];
            else
            {
                this.groups.Add(groupName, new Dictionary<string, string>());
                result = this.groups[groupName];
            }
            return result;
        }

        /// <summary>
        /// Checks the existence of a group
        /// </summary>
        /// <param name="group">Name of the group to check</param>
        /// <returns>True if the group exists, false otherwise</returns>
        public bool Contains(string group)
        {
            return this.groups.Keys.Contains(group) && this.groups[group].Count > 0;
        }

        /// <summary>
        /// Checks the existence of a key-value pair
        /// </summary>
        /// <param name="group">Name of the group of the pair</param>
        /// <param name="key">Key of the pair</param>
        /// <returns>true if the group and the pair exists</returns>
        public bool Contains(string group, string key)
        {
            return this.Contains(group) && this.groups[group].Keys.Contains(key);
        }

        /// <summary>
        /// Creates a new key-value pair. Will create group as needed, and
        /// override existing value.
        /// </summary>
        /// <param name="group">Group in which to store the pair</param>
        /// <param name="key">Pair key</param>
        /// <param name="value">Pair value</param>
        public void PutValue(string group, string key, string value)
        {
            Dictionary<string, string> groupDictionary = this.GetGroup(group);
            if (groupDictionary.Keys.Contains(key)) groupDictionary[key] = value;
            else groupDictionary.Add(key, value);
        }

        /// <summary>
        /// Creates a new key-value pair. Will create group as needed, and
        /// override existing value.
        /// </summary>
        /// <param name="group">Group in which to store the pair</param>
        /// <param name="key">Pair key</param>
        /// <param name="value">Pair value</param>
        public void PutValue(string group, string key, bool value)
        {
            this.PutValue(group, key, value.ToString());
        }

        /// <summary>
        /// Creates a new key-value pair. Will create group as needed, and
        /// override existing value.
        /// </summary>
        /// <param name="group">Group in which to store the pair</param>
        /// <param name="key">Pair key</param>
        /// <param name="value">Pair value</param>
        public void PutValue(string group, string key, int value)
        {
            this.PutValue(group, key, value.ToString());
        }

        /// <summary>
        /// Returns a value for a given group and key
        /// </summary>
        /// <param name="group">Name of the group</param>
        /// <param name="key">Name of the key</param>
        /// <returns>The value if it exists or ""</returns>
        public string GetStringValue(string group, string key)
        {
            Dictionary<string, string> groupDictionary = this.GetGroup(group);
            if (groupDictionary.Keys.Contains(key)) return groupDictionary[key];
            return "";
        }

        /// <summary>
        /// Returns a value for a given group and key
        /// </summary>
        /// <param name="group">Name of the group</param>
        /// <param name="key">Name of the key</param>
        /// <returns>The value if it exists or 0</returns>
        public int GetIntValue(string group, string key)
        {
            Dictionary<string, string> groupDictionary = this.GetGroup(group);
            if (groupDictionary.Keys.Contains(key)) return System.Convert.ToInt32(groupDictionary[key]);
            return 0;
        }

        /// <summary>
        /// Returns a value for a given group and key
        /// </summary>
        /// <param name="group">Name of the group</param>
        /// <param name="key">Name of the key</param>
        /// <returns>The value if it exists or false</returns>
        public bool GetBoolValue(string group, string key)
        {
            Dictionary<string, string> groupDictionary = this.GetGroup(group);
            if (groupDictionary.Keys.Contains(key)) return System.Convert.ToBoolean(groupDictionary[key]);
            return false;
        }

        /// <summary>
        /// Empties the ini file
        /// </summary>
        public void Clear()
        {
            this.groups.Clear();
        }

        /// <summary>
        /// Removes a group from the demo file
        /// </summary>
        /// <param name="group">Name of the group to remove</param>
        public void Remove(string group)
        {
            if (this.Contains(group)) this.groups.Remove(group);
        }

        /// <summary>
        /// Removes a key-value pair from the ine file
        /// </summary>
        /// <param name="group">Group from which to remove the pair</param>
        /// <param name="key">Key of the pair to remove</param>
        public void Remove(string group, string key)
        {
            if (this.Contains(group, key)) this.groups[group].Remove(key);
        }

        /// <summary>
        /// Dumps the contents of the configuration to a list of strings
        /// formatted like an ini file.
        /// </summary>
        /// <returns>Ini file contents</returns>
        public List<string> GetFileContents()
        {
            List<string> result = new List<string>();

            foreach (string group in this.groups.Keys)
            {
                if(this.groups[group].Count > 0)
                {
                    result.Add("[" + group + "]");
                    foreach (string key in this.groups[group].Keys)
                    {
                        result.Add(key + " = " + this.groups[group][key]);
                    }
                    result.Add("");
                }
            }

            return result;
        }

        /// <summary>
        /// Read contents from a file and replace the current contents
        /// </summary>
        /// <param name="fileName">The file to be loaded</param>
        public void LoadIniFile(string fileName)
        {
            IniParser iniParser = new IniParser();
            ParserEngine.Parse(fileName, iniParser);
            Dictionary<string, Dictionary<string, string>> result = iniParser.GetResult();
            if (result != null) this.groups = result;
        }

    }
}
