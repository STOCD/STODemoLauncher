using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StoDemoLauncher.Helper
{
    /// <summary>
    /// A file entry in the MRU list
    /// </summary>
    class MruFile
    {
        public string FileName { get; set; }

        public MruFile(String fileName)
        {
            this.FileName = fileName;
        }

        public override string ToString()
        {
            return FileName.Substring(FileName.LastIndexOf("\\") + 1);
        }

        public override bool Equals(object other)
        {
            return other is MruFile && ((MruFile)other).FileName.Equals(this.FileName);
        }

        public override int GetHashCode()
        {
            return FileName.GetHashCode();
        }
    }


    /// <summary>
    /// A list of the most recently used files
    /// </summary>
    class MruFileList
    {
// begin config constants
        public const string MruFileListIniGroup = "MruFileList";
        public const string EnableMruFileListIniKey = "enableMruFileList";
        public const string MaxEntryCountIniKey = "maxEntryCount";
        public const string MruFileEntryIniKeyPrefix = "mruFileEntry";

        public const bool EnableMruFileListDefault = true;
        public const int MaxEntryCountDefault = 4;
// end config constants

        /// <summary>
        /// The references for the MRU files
        /// </summary>
        private List<MruFile> fileList = new List<MruFile>();

        /// <summary>
        /// The number of entries to manage
        /// </summary>
        private int maxEntryCount;

        /// <summary>
        /// Creates a new MruFileList
        /// </summary>
        public MruFileList()
        {
            LoadFromConfig();
        }

        /// <summary>
        /// Replaces the contents of the list with the entries in the config file
        /// </summary>
        public void LoadFromConfig()
        {
            ConfigurationFile config = ConfigurationFile.GetInstance();

            this.fileList.Clear();
            if (config.Contains(MruFileListIniGroup, EnableMruFileListIniKey))
            {
                if(config.GetBoolValue(MruFileListIniGroup, EnableMruFileListIniKey))
                {
                    for (int i = 0; i < config.GetIntValue(MruFileListIniGroup, MaxEntryCountIniKey); i++)
                    {
                        if (config.Contains(MruFileListIniGroup, MruFileEntryIniKeyPrefix + i))
                        {
                            this.fileList.Add(new MruFile(config.GetStringValue(MruFileListIniGroup, MruFileEntryIniKeyPrefix + i)));
                        }
                    }
                    this.maxEntryCount = config.GetIntValue(MruFileListIniGroup, MaxEntryCountIniKey);
                }
            }
            else
            {
                config.PutValue(MruFileListIniGroup, EnableMruFileListIniKey, EnableMruFileListDefault);
                config.PutValue(MruFileListIniGroup, MaxEntryCountIniKey, MaxEntryCountDefault);
            }
        }

        /// <summary>
        /// Writes MRU file list to config file
        /// </summary>
        public void SaveToConfig()
        {
            ConfigurationFile config = ConfigurationFile.GetInstance();

            if (config.GetBoolValue(MruFileListIniGroup, EnableMruFileListIniKey))
            {
                for (int i = 0; i < Math.Min(this.fileList.Count,config.GetIntValue(MruFileListIniGroup, MaxEntryCountIniKey)); i++)
                {
                    config.PutValue(MruFileListIniGroup, MruFileEntryIniKeyPrefix + i, this.fileList.ElementAt(i).FileName);
                }
            }

            this.CleanUpConfig();
        }

        /// <summary>
        /// Removes unused entries from config file
        /// </summary>
        private void CleanUpConfig()
        {
            ConfigurationFile config = ConfigurationFile.GetInstance();

            int firstUnusedIndex = 0;

            if (config.GetBoolValue(MruFileListIniGroup, EnableMruFileListIniKey))
                firstUnusedIndex = Math.Min(this.fileList.Count, config.GetIntValue(MruFileListIniGroup, MaxEntryCountIniKey));

            for(int i = firstUnusedIndex; i < 10; i++)
            {
                config.Remove(MruFileListIniGroup, MruFileEntryIniKeyPrefix + i);
            }
        }

        /// <summary>
        /// Creates a list of ToolStripMenuItems and registers an EventHandler
        /// with each.
        /// </summary>
        /// <param name="handler">The EventHandler to notify when an item is
        /// clicked</param>
        /// <returns>A list of ToolStripMenuItems</returns>
        public List<ToolStripItem> ToToolStripItems(EventHandler handler)
        {
            List<ToolStripItem> result = new List<ToolStripItem>();
            foreach(MruFile entry in this.fileList)
            {
                ToolStripMenuItem menuItem = this.CreateToolStripMenuItem(entry);
                menuItem.Click += handler;
                result.Add(menuItem);
            }
            return result;
        }

        /// <summary>
        /// Creates a ToolStripMenuItem from a single MruFile entry
        /// </summary>
        /// <param name="entry">The MruFile entry for which to construct a
        /// ToolStripMenuItem</param>
        /// <returns>A ToolStripMenuItem, that reports the full filename as
        /// event argument when clicked</returns>
        private ToolStripMenuItem CreateToolStripMenuItem(MruFile entry)
        {
            ToolStripMenuItem result = new ToolStripMenuItem();

            result.Tag = entry.FileName;
            result.Text = entry.ToString();

            return result;
        }

        /// <summary>
        /// Places a given file at the top of the MRU list
        /// </summary>
        /// <param name="fileName">The name of the file to move to the top</param>
        public void PromoteFile(string fileName)
        {
            this.PromoteFile(new MruFile(fileName));
        }

        /// <summary>
        /// Places a given file at the top of the MRU list
        /// </summary>
        /// <param name="file">The file to move to the top</param>
        public void PromoteFile(MruFile file)
        {
            if (this.fileList.Contains(file))
            {
                this.fileList.Remove(file);
                this.fileList.Insert(0, file);
            }
            else
            {
                this.fileList.Insert(0, file);
                if (this.fileList.Count > this.maxEntryCount)
                {
                    this.fileList.Remove(this.fileList.Last());
                }
            }
        }

        /// <summary>
        /// Removes a given file from the MRU list
        /// </summary>
        /// <param name="fileName">The name of the file to move to the top</param>
        public void RemoveFile(string fileName)
        {
            this.RemoveFile(new MruFile(fileName));
        }

        /// <summary>
        /// Places a given file at the top of the MRU list
        /// </summary>
        /// <param name="file">The file to move to the top</param>
        public void RemoveFile(MruFile file)
        {
            if (this.fileList.Contains(file))
            {
                this.fileList.Remove(file);
            }
        }


        /// <summary>
        /// Removes all entries from the MRU list
        /// </summary>
        public void Clear()
        {
            this.fileList.Clear();
        }
    }
}
