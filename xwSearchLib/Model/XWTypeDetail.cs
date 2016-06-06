using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xwSearchLib.Utility;

namespace xwSearchLib.Model
{
    public class XWTypeDetail : Meta
    {
        private const string CONDITIONAL_TRIGGER = "w/";
        private const string CONDITIONAL_NUMBER_TRIGGER = "or";

        public XW_TYPE slotType { get; set; }
        public int slotCount { get; set; }
        public bool hasConditional { get; set; }
        public string conditional { get; set; }

        public XWTypeDetail() { }

        public XWTypeDetail(string[] row,  
            EXCEL_SHEET_PILOT column, 
            XW_TYPE type)
        {
            setConditional(row[Convert.ToInt32(column)]);
            setSlotType(row[Convert.ToInt32(column)],type);
        }

        private void setConditional(string input)
        {
            if (input.Contains(CONDITIONAL_TRIGGER))
            {
                this.hasConditional = true;
                this.conditional = input;

                if (input.Contains(CONDITIONAL_NUMBER_TRIGGER)) 
                {
                    int indexOfLeftParen = input.IndexOf("(");
                    string numberExtract = input.Substring(0, indexOfLeftParen);
                    this.slotCount = xwFormatter.fixSpreadSheetNumber(numberExtract);
                }
                else
                {
                    // looks to be the default
                    this.slotCount = 1;
                }
            }
        }

        private void setSlotType(string input, XW_TYPE type)
        {
        
            if (hasConditional)
            {
                this.slotType = type;
            }
            else
            {
                int slotCount = xwFormatter.fixSpreadSheetNumber(input);

                if (slotCount != 0)
                {
                    this.slotType = type;
                    this.slotCount = slotCount;
                }
                else
                {
                    this.slotType = XW_TYPE.NONE;
                    this.slotCount = 0;
                }
            }
        }


    }
}
