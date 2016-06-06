using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwSearchLib.Utility
{
    public class xwFormatter
    {
        public static int fixSpreadSheetNumber(string input)
        {
            int output = 0;

            if (input == "-")
            {
                output = -2;
            }
            else if (input == "*") 
            {
                output = -1;
            }
            else
            {
                output = (String.IsNullOrEmpty(input)) ? 0 
                    : Convert.ToInt32(input);
            }

            return output;
        }

        public static bool fixSpreadSheetBoolean(string input)
        {
            return (String.IsNullOrEmpty(input) || input == "FALSE")
                ? false : true;
        }
    }
}
