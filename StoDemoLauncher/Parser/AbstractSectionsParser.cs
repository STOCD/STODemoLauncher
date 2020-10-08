using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoDemoLauncher.Model;

namespace StoDemoLauncher.Parser
{
    /// <summary>
    /// Provides a method to return a list of sections
    /// </summary>
    abstract public class AbstractSectionsParser : AbstractParser
    {
        /// <summary>
        /// Returns the list with the found resources.
        /// </summary>
        /// <returns></returns>
        abstract public List<DemoSection> GetResult();

    }
}
