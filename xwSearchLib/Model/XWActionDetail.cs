using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xwSearchLib.Utility;

namespace xwSearchLib.Model
{
    public class XWActionDetail : Meta
    {
        private const string CONDITIONAL_TRIGGER = "w/";

        public XW_SHIP_ACTIONS action { get; set; }
        public string actionDesc { get; set; }
        public bool hasConditional { get; set; }
        public string conditional { get; set; }

        public XWActionDetail() { }

        public XWActionDetail(string[] row, 
            EXCEL_SHEET_PILOT column, 
            XW_SHIP_ACTIONS action)
        {
            setConditional(row[Convert.ToInt32(column)]);
            setAction(row[Convert.ToInt32(column)], action);
        }

        private void setConditional(string input)
        {
            if (input.Contains(CONDITIONAL_TRIGGER))
            {
                this.hasConditional = true;
                this.conditional = input;
            }
        }

        private void setAction(string input,XW_SHIP_ACTIONS action)
        {
            if (hasConditional)
            {
                this.action = action;
                this.actionDesc = action.ToString();
            }
            else
            {
                if(xwFormatter.fixSpreadSheetBoolean(input))
                {
                    this.action = action;
                    this.actionDesc = action.ToString();
                }
                else
                {
                    this.action = XW_SHIP_ACTIONS.NONE;
                    this.actionDesc = XW_SHIP_ACTIONS.NONE.ToString();
                }
            }
        }
    }
}
