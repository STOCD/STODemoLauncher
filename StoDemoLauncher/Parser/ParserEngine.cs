using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using StoDemoLauncher.Filters;

namespace StoDemoLauncher.Parser
{
    public class ParserEngine
    {
        /// <summary>
        /// Parses a demo file
        /// </summary>
        /// <param name="filename">The file to be parsed</param>
        /// <param name="plugin">The ParserPlugin to use</param>
        /// <returns>true if parsing was successful</returns>
        public static bool Parse(string filename, AbstractParser plugin)
        {
            // here we read the contents of the demo file line-by-line
            string line = "";
            int lineNumber = 0;

            // only run if the file actually exists
            if (File.Exists(filename))
            {
                plugin.StartParsing(filename);

                // start reading file line by line
                StreamReader file = null;
                try
                {
                    file = new StreamReader(filename);
                    // parse as long as we don't have all information and there are still lines to read
                    while (!plugin.HasAllInformation() && (line = file.ReadLine()) != null)
                    {
                        plugin.NewLine(line, lineNumber);
                        lineNumber++;
                    }
                    plugin.StopParsing(lineNumber);
                }
                // report exception
                catch (Exception e)
                {
                    MessageBox.Show("The demo file " + filename + " could not be parsed for meta information.\n" +
                        "Existence is pain. Sorry for the inconvenience.\n" +
                        e.Message, "Error Parsing Demo Directory",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                // close file, no matter what
                finally
                {
                    if (file != null)
                    {
                        file.Close();
                    }
                }

                // abort parsing, if exception occured
                if (file == null)
                {
                    return false;
                }
            }
            // report error, if file does not exist
            else
            {
                MessageBox.Show("The demo file " + filename + " does not exist.\n" +
                    "Please report the this error under \"Updates & Help\"\n",
                    "File Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Parses a demo file in memory
        /// </summary>
        /// <param name="fileContents">The contents of a demo file to be parsed</param>
        /// <param name="plugin">The ParserPlugin to use</param>
        /// <returns>true if parsing was successful</returns>
        public static bool Parse(List<string> fileContents, AbstractParser plugin)//, ToolStripProgressBar progressBar)
        {
            if(fileContents != null)
            {
                // here we read the contents of the demo file line-by-line
                int lineNumber = 0;
                //progressBar.Maximum = fileContents.Count();
                //progressBar.Step = 1;
                List<string>.Enumerator enumerator = fileContents.GetEnumerator();
                
                plugin.StartParsing("no file");

                // parse as long as we don't have all information and there are still lines to read
                while (!plugin.HasAllInformation() && enumerator.MoveNext())
                {
                    plugin.NewLine(enumerator.Current, lineNumber);
                    lineNumber++;
                    //progressBar.PerformStep();
                }
                plugin.StopParsing(lineNumber);
            }
            // report error, if file does not exist
            else
            {
                MessageBox.Show("The demo file buffer was null.\n" +
                    "Please report the this error under \"Updates & Help\"\n",
                    "Demo File Buffer Null",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

    }
}
