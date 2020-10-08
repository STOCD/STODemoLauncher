using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoDemoLauncher.Helper
{
    class StringFilter
    {
        /// <summary>
        /// Compares two logical values
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public delegate bool LogicalOperation (bool value1, bool value2);

        private static bool AndOperation(bool value1, bool value2)
        {
            return value1 && value2;
        }

        private static bool OrOperation(bool value1, bool value2)
        {
            return value1 || value2;
        }

        /// <summary>
        /// Class that performs logical operations
        /// </summary>
        public class LogicalOperator
        {
            private LogicalOperation operation;
            private bool defaultResult;

            public LogicalOperator(LogicalOperation operation, bool defaultResult)
            {
                this.operation = operation;
                this.defaultResult = defaultResult;
            }

            public bool DefaultResult {
                get
                {
                    return this.defaultResult;
                }
            }

            public bool Compute(bool value1, bool value2)
            {
                return this.operation(value1, value2);
            }

        }

        public static LogicalOperator And = new LogicalOperator(new LogicalOperation(AndOperation), true);
        public static LogicalOperator Or = new LogicalOperator(new LogicalOperation(OrOperation), false);

        /// <summary>
        /// Converts a space separated list of search terms into a list of
        /// strings
        /// </summary>
        /// <param name="searchTerms">A space separated search term list</param>
        /// <returns>A list of search terms</returns>
        private static List<string> ExtractTerms(string searchTerms)
        {
            List<string> terms = new List<string>();

            string searchTerm = searchTerms.Trim();
            while (searchTerm.Length > 0)
            {
                int spaceIndex = searchTerm.IndexOf(" ");
                if (spaceIndex != -1)
                {
                    terms.Add(searchTerm.Substring(0, spaceIndex));
                    searchTerm = searchTerm.Substring(spaceIndex + 1);
                }
                else
                {
                    terms.Add(searchTerm);
                    searchTerm = "";
                }
            }
            return terms;
        }

        /// <summary>
        /// Tests, if a given string contains all given searchTerms. The test
        /// is case-sensitive.
        /// </summary>
        /// <param name="searchTerms">A space separated search term list</param>
        /// <param name="testSubject">The string to test</param>
        /// <returns>True if all terms are contained, or no search terms were
        /// given</returns>
        public static bool TestAll(string testString, string searchTerms, LogicalOperator defaultOperator)
        {
            bool result = defaultOperator.DefaultResult;

            List<string> terms = ExtractTerms(searchTerms);

            foreach (string term in terms)
            {
                result = defaultOperator.Compute(result, testString.Contains(term));
            }
            return result;
        }

        /// <summary>
        /// Tests, if a given string contains all given searchTerms. The test
        /// is not case-sensitive.
        /// </summary>
        /// <param name="searchTerms">A space separated search term list</param>
        /// <param name="testSubject">The string to test</param>
        /// <returns>True if all terms are contained, or no search terms were
        /// given</returns>
        public static bool TestAllCaseInvariant(string testString, string searchTerms, LogicalOperator defaultOperator)
        {
            return StringFilter.TestAll(testString.ToLowerInvariant(), searchTerms.ToLowerInvariant(), defaultOperator);
        }
    }
}
