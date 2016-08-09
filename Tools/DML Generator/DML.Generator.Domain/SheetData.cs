using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace DML.Generator.Domain
{
    /// <summary>
    /// Class containing excel sheet raw data
    /// </summary>
    public class SheetData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SheetData"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="range">The range.</param>
        public SheetData(string name, Range range)
        {
            this.Name = name.Trim();
            this.UsedRange = (object[,])range.Value2;
            BuildSheetData();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName { get; private set; }

        /// <summary>
        /// Gets or sets the end index of the row.
        /// </summary>
        /// <value>
        /// The end index of the row.
        /// </value>
        public int EndRowIndex { get; private set; }

        /// <summary>
        /// Gets or sets the start index of the row.
        /// </summary>
        /// <value>
        /// The start index of the row.
        /// </value>
        public int StartRowIndex { get; private set; }

        /// <summary>
        /// Gets or sets the index of the table row.
        /// </summary>
        /// <value>
        /// The index of the table row.
        /// </value>
        public int TableRowIndex { get; private set; }

        /// <summary>
        /// Gets or sets the index of the column row.
        /// </summary>
        /// <value>
        /// The index of the column row.
        /// </value>
        public int ColumnRowIndex { get; private set; }

        /// <summary>
        /// Gets or sets the index of the type row.
        /// </summary>
        /// <value>
        /// The index of the type row.
        /// </value>
        public int TypeRowIndex { get; private set; }

        /// <summary>
        /// Gets or sets the index of the primary row.
        /// </summary>
        /// <value>
        /// The index of the primary row.
        /// </value>
        public int PrimaryRowIndex { get; private set; }

        /// <summary>
        /// Gets or sets the column count.
        /// </summary>
        /// <value>
        /// The column count.
        /// </value>
        public int ColumnCount { get; private set; }
        
        /// <summary>
        /// Gets or sets the user range.
        /// </summary>
        /// <value>
        /// The user range.
        /// </value>
        public object[,] UsedRange { get; set; }

        /// <summary>
        /// Gets the sheet information.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <returns></returns>
        private void BuildSheetData()
        {
            string cellText = string.Empty;
            if (!Object.ReferenceEquals(null, this.UsedRange))
            {
                this.ColumnCount = this.UsedRange.GetUpperBound(1);

                for (int rowCount = 1; rowCount <= this.UsedRange.GetUpperBound(0); rowCount++)
                {
                    if (this.UsedRange[rowCount, 1] != null)
                    {
                        cellText = this.UsedRange[rowCount, 1].ToString();

                        switch (cellText.ToLower())
                        {
                            case "end":
                                this.EndRowIndex = rowCount;
                                break;
                            case "start":
                                this.StartRowIndex = rowCount;
                                break;
                            case "exceed table:":
                                this.TableRowIndex = rowCount;
                                this.TableName = this.UsedRange[rowCount, 2].ToString().Trim();
                                break;
                            case "column name:":
                                this.ColumnRowIndex = rowCount;
                                break;
                            case "type:":
                                this.TypeRowIndex = rowCount;
                                break;
                            case "primary key:":
                                this.PrimaryRowIndex = rowCount;
                                break;
                        }
                    }
                }
            }
        }
    }
}
