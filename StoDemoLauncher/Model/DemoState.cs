using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoDemoLauncher.Model
{
    /// <summary>
    /// Element from States section of a demo file
    /// </summary>
    class DemoState
    {
        /// <summary>
        /// Type 3 terminates an FX
        /// </summary>
        public long Type { get; set; }

        /// <summary>
        /// Not sure. Communication process on game server?
        /// </summary>
        public long processCount { get; set; }
    }
}
