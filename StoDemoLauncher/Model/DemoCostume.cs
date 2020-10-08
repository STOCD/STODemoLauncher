using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoDemoLauncher.Model
{
    /// <summary>
    /// A "Costumev5" section from a demo file
    /// </summary>
    class DemoCostume : DemoSection
    {
        /// <summary>
        /// hReferencedCostume, pSubstituteCostume, pStoredCostume, or pcDestructibleObjectCostume
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Name of the Costume
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Human readable message
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = this.Name;
            if (this.Type.Equals("HreferencedCostume")) result += " (stock Costume reference)";
            if (this.Type.Equals("PsubstituteCostume")) result += " (overridden stock Costume)";
            if (this.Type.Equals("PstoredCostume")) result += " (player Costume)";
            if (this.Type.Equals("PcdestructibleobjectCostume")) result = "(destructable object)";
            return result;
        }
    }
}
