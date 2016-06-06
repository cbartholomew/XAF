using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.IO;
namespace xwSearchLib.Utility
{
    public class xwExcel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Workbook createWorkbook()
        {
            Application xlApp = new Application();

            Workbook xlWorkBook = xlApp.Workbooks.Add();

            return xlWorkBook;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Workbook getWorkbook(string path)
        {
            Workbook xwWorkBook = null;
            try
            {
                if (String.IsNullOrEmpty(path))
                {
                    throw new ArgumentNullException("No XLS Path Found");
                }
                Application xwApp = new Application();

                // init new appliction
                xwWorkBook = xwApp.Workbooks.Open(path);
            }
            catch (Exception e)
            {
                throw e;
            }

            return xwWorkBook;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xwWorkBook"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static Worksheet getWorksheet(Workbook xwWorkBook, string sheetName = "Sheet1")
        {
            Worksheet xwWorksheet = null;
            try
            {
                // create new work sheet
                xwWorksheet = (Worksheet)xwWorkBook.Sheets[sheetName];
            }
            catch (Exception e)
            {
                throw e;
            }

            return xwWorksheet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="range"></param>
        /// <param name="customerWorksheet"></param>
        /// <returns></returns>
        public static string[] GetRangeValue(string range, Worksheet customerWorksheet)
        {
            Range workingRange = customerWorksheet.get_Range(range);

            System.Array arr = (System.Array)workingRange.Cells.Value2;

            string[] output = ConvertToStringArray(arr);

            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private static string[] ConvertToStringArray(System.Array values)
        {
            string[] theArray = new string[values.Length];
            for (int i = 1; i <= values.Length; i++)
            {
                if (values.GetValue(1, i) == null)
                    theArray[i - 1] = "";
                else
                    theArray[i - 1] = (string)values.GetValue(1, i).ToString();
            }
            return theArray;
        }

        /// <summary>
        /// This is the default setting.
        /// </summary>
        /// <param name="path"></param>
        public static List<string[]> createFromExcel(string path, string startColumn, string endColumn, string worksheet = "Sheet1")
        {
            Workbook customerWorkbook = xwExcel.getWorkbook(path);
            Worksheet customerWorksheet = xwExcel.getWorksheet(customerWorkbook, worksheet);
            Range xlsRange = customerWorksheet.UsedRange;

            List<string[]> rows = new List<string[]>();


            // iterate through the rows to create work items
            foreach (Range row in xlsRange.Rows)
            {
                int rowNumber = row.Row;
                // skip header
                if (rowNumber == 1)
                    continue;
                // the range here is column A (1) will always be the title and column B (2) will be the description
                string[] range = xwExcel.GetRangeValue(startColumn + rowNumber + ":" + endColumn + rowNumber + "", customerWorksheet);

                rows.Add(range);
            }
            customerWorkbook.Close();

            return rows;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="assignedTo"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="iterationPath"></param>
        public static void createTestWorkItemToFile(string assignedTo, string title, string description, string iterationPath = @"CIP\V2 Shim AD Testing")
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine(assignedTo);
            output.AppendLine(iterationPath);
            output.AppendLine(title);
            output.AppendLine(description);

            File.AppendAllText("testOutput.txt", output.ToString());
        }
    }

}


