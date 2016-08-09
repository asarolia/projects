using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DML.Generator.Domain.Abstract
{
    public abstract class AbstractDML
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractDML"/> class.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        /// <param name="modeuleName">Name of the modeule.</param>
        /// <param name="sheetName">Name of the sheet.</param>
        protected AbstractDML(DataTable dataTable, string modeuleName, string sheetName)
        {
            this.dataTable = dataTable;
            this.moduleName = modeuleName;
            this.sheetName = sheetName;
        }

        /// <summary>
        /// Modeule Name.
        /// </summary>
        protected string moduleName;

        /// <summary>
        /// Gets or sets the date format.
        /// </summary>
        /// <value>
        /// The date format.
        /// </value>
        protected string DateFormat { get; set; }

        /// <summary>
        /// Sheet Name.
        /// </summary>
        protected string sheetName;

        /// <summary>
        /// Gets or sets the data table.
        /// </summary>
        /// <value>
        /// The data table.
        /// </value>
        protected DataTable dataTable { get; set; }

        /// <summary>
        /// Gets or sets the delete statement.
        /// </summary>
        /// <value>
        /// The delete statement.
        /// </value>
        public string DeleteStatement
        {
            get { return this.GetDeleteStatement(); }
        }

        /// <summary>
        /// Gets or sets the insert statement.
        /// </summary>
        /// <value>
        /// The insert statement.
        /// </value>
        public string InsertStatement
        {
            get { return this.GetInsertStatement(); }
        }

        /// <summary>
        /// Gets the header information.
        /// </summary>
        public string HeaderInformation
        {
            get { return this.GetHeaderInformation(); }
        }

        /// <summary>
        /// Gets the insert statement.
        /// </summary>
        /// <returns></returns>
        protected abstract string GetInsertStatement();

        /// <summary>
        /// Gets the delete statement.
        /// </summary>
        /// <returns></returns>
        protected abstract string GetDeleteStatement();

        /// <summary>
        /// Gets the header information.
        /// </summary>
        /// <returns></returns>
        protected abstract string GetHeaderInformation();

        /// <summary>
        /// Gets the insert count.
        /// </summary>
        /// <returns></returns>
        protected int getActionCount(string action)
        {
            return (from row in this.dataTable.AsEnumerable()
                    where (string.Equals(row.Field<string>("Action"), action))
                    select row)
                   .Count();
        }
    }
}
