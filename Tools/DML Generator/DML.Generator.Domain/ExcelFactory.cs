using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Xml.Linq;

namespace DML.Generator.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class ExcelFactory
    {
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        private string Path { get; set; }

        /// <summary>
        /// Gets or sets the data set.
        /// </summary>
        /// <value>
        /// The data set.
        /// </value>
        private DataSet dataSet { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelFactory"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public ExcelFactory(string path)
        {
            this.Path = path;
        }

        /// <summary>
        /// Gets the sheet data.
        /// </summary>
        /// <returns>Excel sheet data.</returns>
        public List<DMLInfo> GetSheetData()
        {
            List<DMLInfo> DMLInfo = new List<DMLInfo>();
            Application application = new Application();
            Workbook workbook = application.Workbooks.Open(this.Path);
            try
            {
                foreach (_Worksheet xlWorksheet in workbook.Sheets)
                {
                    if (!string.Equals(xlWorksheet.Name, "Example"))
                    {
                        DMLInfo.Add(new DMLInfo(new SheetData(xlWorksheet.Name, xlWorksheet.UsedRange)));
                    }
                }
                DMLInfo.RemoveAll(x => x.SheetData.StartRowIndex == 0);
            }
            finally
            {
                workbook.Close();
            }
            return DMLInfo;
        }
    }
}