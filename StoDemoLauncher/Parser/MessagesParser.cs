using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoDemoLauncher.Model;

namespace StoDemoLauncher.Parser
{
    /// <summary>
    /// Parses a demo file for "Notifications"
    /// </summary>
    class MessagesParser : AbstractSectionsParser
    {
        /// <summary>
        /// The list in which the parser stores the pprocessed messages
        /// </summary>
        List<DemoSection> result = new List<DemoSection>();

        /// <summary>
        /// The string we are looking for in parsed messages
        /// </summary>
        string messageSearchString;

        /// <summary>
        /// Number of open braces ("{" increments, "}" decrements)
        /// </summary>
        int braceLevel = 0;

        /// <summary>
        /// The number of open braces at the last encounter of "Messages"
        /// </summary>
        int lastMessagesBraceLevel = 0;

        /// <summary>
        /// The line at which the last "Messages" section started
        /// </summary>
        int lastMessagesSectionStart = 0;

        /// <summary>
        /// Flag that indicates if the parser has currently entered a
        /// "Messages" section
        /// </summary>
        bool inMessagesSection = false;

        /// <summary>
        /// Flag that indicates if we are currently processing a Messages
        /// section that contains the messageSearchString
        /// </summary>
        bool inSearchSection = false;

        /// <summary>
        /// The last visited message test
        /// </summary>
        string messageText = "";

        /// <summary>
        /// The command identifier
        /// </summary>
        int command = -1;

        /// <summary>
        /// The time index of the message in seconds
        /// </summary>
        double time = -1;

        /// <summary>
        /// The entity ref for this message
        /// </summary>
        int EntityRef = -1;

        /// <summary>
        /// Creates a new MessageParser object
        /// </summary>
        /// <param name="messageSearchString">The string to look for in parsed
        /// messages</param>
        public MessagesParser(string messageSearchString)
        {
            this.messageSearchString = messageSearchString;
        }

        /// <summary>
        /// Is called by the parser when it has fetched a new line
        /// </summary>
        /// <param name="line">Content of the line</param>
        /// <param name="lineNumber">Current line number (0 index)</param>
        public override void NewLine(string line, int lineNumber)
        {
            if (line.Trim().StartsWith("Messages"))
            {
                lastMessagesSectionStart = lineNumber;
                lastMessagesBraceLevel = braceLevel;
                inMessagesSection = true;
            }
            if (inMessagesSection && line.Trim().StartsWith("Time"))
            {
                string timeString = line.Substring(line.IndexOf("Time ") + 5);
                this.time = Double.Parse(timeString, System.Globalization.CultureInfo.GetCultureInfo("en-US").NumberFormat);
            }
            if (inMessagesSection && line.Trim().StartsWith("Message ") && line.Contains(messageSearchString))
            {
                inSearchSection = true;
                messageText = line.Substring(line.IndexOf("Message ") + 8);
            }
            if (inMessagesSection && line.Trim().StartsWith("Command"))
            {
                this.command = Convert.ToInt32(line.Substring(line.IndexOf("Command ") + 8));
            }
            if (inMessagesSection && line.Trim().StartsWith("EntityRef"))
            {
                this.EntityRef = Convert.ToInt32(line.Substring(line.IndexOf("EntityRef ") + 10));
            }
            if (inSearchSection && line.Trim().StartsWith("}") && braceLevel - 1 == lastMessagesBraceLevel)
            {
                inMessagesSection = false;
                inSearchSection = false;
                DemoMessage message = new DemoMessage();
                message.StartLine = lastMessagesSectionStart;
                message.EndLine = lineNumber + 1;
                message.Message = messageText;
                message.Time = time;
                message.Command = command;
                message.EntityRef = EntityRef;
                result.Add(message);
            }
            if (line.Trim().StartsWith("{")) braceLevel++;
            if (line.Trim().StartsWith("}")) braceLevel--;
        }

        /// <summary>
        /// Returns the list of parsed messages
        /// </summary>
        /// <returns>Returns a list of parsed messages</returns>
        override public List<DemoSection> GetResult()
        {
            return result;
        }
    }
}
