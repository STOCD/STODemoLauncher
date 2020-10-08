using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoDemoLauncher.Model
{
    /// <summary>
    /// Resources section from a demo file
    /// </summary>
    class DemoResource : DemoSection
    {
        public long Handle { get; set; }
        public long Id { get; set; }
        public string Constant { get; set; }
        public List<DemoState> States { get; set; }

        /// <summary>
        /// Human readable resource
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = this.Constant;
            result = result.Substring(result.IndexOf("{") + 1);
            result = result.Substring(0, result.LastIndexOf("}"));
            int endLine = result.IndexOf("\\r");
            result = result.Replace("\\r", "\r");
            result = result.Replace("\\n", "\n");

            if (result.Contains("FxName"))
            {
                result = result.Substring(result.IndexOf("FxName") + 7);
                result = result.Substring(0, result.IndexOfAny(new char[] { ' ', '\r', '\n', '\t'}));
            }

            return result;
        }
    }
}
