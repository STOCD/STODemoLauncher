using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StoDemoLauncher.Filters;
using StoDemoLauncher.Parser;

namespace StoDemoLauncher.Model
{
    /// <summary>
    /// The information displayed for a demo file
    /// </summary>
    public class DemoInfo
    {
        /// <summary>
        /// Short file name ("something.demo")
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// STO internal map name
        /// </summary>
        public string MapName { get; set; }

        /// <summary>
        /// Beginning of the recording in s
        /// </summary>
        public long StartTime { get; set; }

        /// <summary>
        /// Ending of the recording in s
        /// </summary>
        public long EndTime { get; set; }

        /// <summary>
        /// Short player character name
        /// </summary>
        public string Character { get; set; }

        /// <summary>
        /// System time created
        /// </summary>
        public DateTime Create { get; set; }

        /// <summary>
        /// System time of last modification
        /// </summary>
        public DateTime Modify { get; set; }

        /// <summary>
        /// On which server was this demo recorded?
        /// </summary>
        public GameServer Server { get; set; }

        /// <summary>
        /// Retrieves a DemoInfoFile object from a 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        /// public static GetFileInfo(string filename)
        public static DemoInfo GetDemoFileInfo(string filename)
        {
            DemoInfoParser parserPlugin = new DemoInfoParser();
            ParserEngine.Parse(filename, parserPlugin);
            return parserPlugin.GetResult();
        }
        
        /// <summary>
        /// Human readable message
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GameClient.GetServerName(this.Server) + " " + this.FileName + " " + this.MapName + " " + this.Character;
        }

    }
}
