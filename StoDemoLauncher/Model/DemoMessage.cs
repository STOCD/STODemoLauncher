using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoDemoLauncher.Model
{
    /// <summary>
    /// Represents a message recorded in STO
    /// </summary>
    public class DemoMessage : DemoSection
    {
        /// <summary>
        /// Timeindex of the message
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// Command enum?
        /// Mostly 65538, NotifySent 65537
        /// </summary>
        public long Command { get; set; }

        /// <summary>
        /// Entity reference id
        /// </summary>
        public long EntityRef { get; set; }

        /// <summary>
        /// Message text string
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Human readable message
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = this.Message;
            // Waveform Modulation mini-game
            if (result.StartsWith("\"gclWaveformReceiveData"))
            {
                result = "Waveform Modulation mini game";
            }
            // NotifySend & NotifySendWithData
            else if(result.StartsWith("<&NotifySend"))
            {
                result = result.Substring(result.IndexOf("\"") + 1);
                result = result.Substring(0, result.IndexOf("\""));
                result = result.Replace("\\\\q", "\"");
                result = result.Replace("\\\\s", "\'");
            }
            return result;
        }
    }
}
