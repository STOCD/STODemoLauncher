using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoDemoLauncher.Model;

namespace StoDemoLauncher.Parser
{
    /// <summary>
    /// Fetches all 
    /// </summary>
    class CostumeParser : AbstractSectionsParser
    {
        /// <summary>
        /// The list of parsed resources
        /// </summary>
        List<DemoSection> result = new List<DemoSection>();

        // placeholders
        int braceLevel = 0;
        int lastCostumev5BraceLevel = 0;
        int lastCostumev5SectionStart = 0;
        bool inCostumev5Section = false;
        string Costumetype = "";
        string Costumename = "";

        /// <summary>
        /// Is called by the parser when it has fetched a new line
        /// </summary>
        /// <param name="line">Content of the line</param>
        /// <param name="lineNumber">Current line number (0 index)</param>
        public override void NewLine(string line, int lineNumber)
        {
            if (line.Trim().StartsWith("Costumev5"))
            {
                lastCostumev5SectionStart = lineNumber;
                lastCostumev5BraceLevel = braceLevel;
                inCostumev5Section = true;
            }
            if (inCostumev5Section && Costumetype.Equals("") && line.Trim().StartsWith("Hreferencedcostume"))
            {
                this.Costumetype = "Hreferencedcostume";
                this.Costumename = line.Trim().Substring(19);
            }
            if (inCostumev5Section && Costumetype.Equals("") && line.Trim().StartsWith("Psubstitutecostume"))
            {
                this.Costumetype = "Psubstitutecostume";
                this.Costumename = line.Trim().Substring(19);
            }
            if (inCostumev5Section && Costumetype.Equals("") && line.Trim().StartsWith("Pstoredcostume"))
            {   
                this.Costumetype = "Pstoredcostume";
                this.Costumename = line.Trim().Substring(15);
            }
            if (inCostumev5Section && Costumetype.Equals("") && line.Trim().StartsWith("Psubstitutecostume"))
            {
                this.Costumetype = "Psubstitutecostume";
                this.Costumename = line.Trim().Substring(15);
            }
            if (inCostumev5Section && Costumetype.Equals("") && line.Trim().StartsWith("Pcdestructibleobjectcostume"))
            {
                this.Costumetype = "Pcdestructibleobjectcostume";
                this.Costumename = line.Trim().Substring(28);
            }

            if (line.Trim().StartsWith("CostumeV5"))
            {
                lastCostumev5SectionStart = lineNumber;
                lastCostumev5BraceLevel = braceLevel;
                inCostumev5Section = true;
            }
            if (inCostumev5Section && Costumetype.Equals("") && line.Trim().StartsWith("hReferencedCostume"))
            {
                this.Costumetype = "hReferencedCostume";
                this.Costumename = line.Trim().Substring(19);
            }
            if (inCostumev5Section && Costumetype.Equals("") && line.Trim().StartsWith("pSubstituteCostume"))
            {
                this.Costumetype = "pSubstituteCostume";
                this.Costumename = line.Trim().Substring(19);
            }
            if (inCostumev5Section && Costumetype.Equals("") && line.Trim().StartsWith("pStoredCostume"))
            {
                this.Costumetype = "pStoredCostume";
                this.Costumename = line.Trim().Substring(15);
            }
            if (inCostumev5Section && Costumetype.Equals("") && line.Trim().StartsWith("pSubstituteCostume"))
            {
                this.Costumetype = "pSubstituteCostume";
                this.Costumename = line.Trim().Substring(15);
            }
            if (inCostumev5Section && Costumetype.Equals("") && line.Trim().StartsWith("pcDestructibleObjectCostume"))
            {
                this.Costumetype = "pcDestructibleObjectCostume";
                this.Costumename = line.Trim().Substring(28) + " (Ignore This)";
            }

            if (inCostumev5Section && line.Trim().StartsWith("}") && braceLevel - 1 == lastCostumev5BraceLevel)
            {
                inCostumev5Section = false;
                DemoCostume resource = new DemoCostume();
                resource.StartLine = lastCostumev5SectionStart;
                resource.EndLine = lineNumber;
                resource.Name = this.Costumename;
                resource.Type = this.Costumetype;
                result.Add(resource);
                Costumename = "";
                Costumetype = "";
            }
            if (line.EndsWith("{")) braceLevel++;
            if (line.EndsWith("}")) braceLevel--;
        }

        /// <summary>
        /// Returns the list with the found resources.
        /// </summary>
        /// <returns></returns>
        override public List<DemoSection> GetResult()
        {
            return result;
        }

    }
}
