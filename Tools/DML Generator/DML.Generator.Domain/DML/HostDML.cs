using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DML.Generator.Domain.Abstract;
using System.Data;
using DML.Generator.Domain.Extensions;
using System.Configuration;

namespace DML.Generator.Domain.DML
{
    public class HostDML:AbstractDML
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HostDML"/> class.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        /// <param name="modeuleName">Name of the modeule.</param>
        /// <param name="sheetName">Name of the sheet.</param>
        public HostDML(DataTable dataTable, string modeuleName, string sheetName) 
            : base(dataTable, modeuleName, sheetName) 
        {
            this.DateFormat = ConfigurationManager.AppSettings.Get("DateFormat"); //"dd.MM.yyyy";
        }

        /// <summary>
        /// Gets the delete statement.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        /// <returns></returns>
        protected override string GetDeleteStatement()
        {
            StringBuilder dmlString = new StringBuilder();
            foreach (DataRow row in dataTable.Rows)
            {
                dmlString.Append(string.Format("DELETE FROM {0}", dataTable.TableName));
                dmlString.Append("\r\nWHERE\r\n");
                foreach (DataColumn column in dataTable.PrimaryKey)
                {
                    if (column.DataType == typeof(System.Int32) || column.DataType == typeof(System.Decimal) || (column.DataType == typeof(System.String) && row[column].ToString().IndexOf("CURRENT TIME") > -1))
                    {
                        dmlString.Append(string.Format("{0} = {1}\r\nAND ", column.ColumnName.Trim(), row[column]));
                    }
                    else if (column.DataType == typeof(System.DateTime))
                    {
                        string formatedDate = DateTime.Parse(row[column].ToString()).ToString(DateFormat);
                        
                        if (string.Equals(formatedDate, "11.11.1111"))
                        {
                            dmlString.Append(string.Format("{0} = ''\r\nAND ", column.ColumnName.Trim()));
                        }
                        else if (string.Equals(formatedDate, "11.11.2222"))
                        {
                            dmlString.Append(string.Format("{0} = NULL\r\nAND ", column.ColumnName.Trim()));
                        }
                        else if (string.Equals(formatedDate, "11.11.3333"))
                        {
                            dmlString.Append(string.Format("{0} = CURRENT DATE\r\nAND ", column.ColumnName.Trim()));
                        }
                        else
                        {
                            dmlString.Append(string.Format("{0} = '{1}'\r\nAND ", column.ColumnName.Trim(), formatedDate));
                        }
                    }
                    else
                    {
                        string inputString = string.Empty;
                        string tempString = dmlString.ToString();
                        if (string.Equals(tempString.Substring(tempString.Length - 4, 4), "AND "))
                        {
                            inputString = string.Format("AND {0} = '{1}'", column.ColumnName.Trim(), row[column]);
                            dmlString.Remove(dmlString.Length - 4, 4);
                        }
                        else
                        {
                            inputString = string.Format("{0} = '{1}'", column.ColumnName.Trim(), row[column]);
                        }
                        if (inputString.Length > 72)
                        {
                            foreach (string str in inputString.SplitByLength(72))
                            {
                                dmlString.Append(string.Format("{0}\r\n", str));
                            }

                            dmlString.Append("AND ");
                        }
                        else
                        {
                            dmlString.Append(string.Format("{0}\r\nAND ", inputString));
                        }
                    }
                }

                dmlString.Remove(dmlString.Length - 4, 4);
                dmlString.Append(";\r\n\r\n");
            }

            return dmlString.ToString();
        }

        /// <summary>
        /// Gets the host insert DML string.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        /// <returns></returns>
        protected override string GetInsertStatement()
        {
            StringBuilder dmlString = new StringBuilder();
            foreach (DataRow row in dataTable.Rows)
            {
                if (string.Equals(row["Action"].ToString().ToLower(), "add"))
                {
                    dmlString.Append(string.Format("INSERT INTO {0}", dataTable.TableName));
                    dmlString.Append("\r\n(");
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        if (!string.Equals(column.ColumnName, "Action"))
                        {
                            dmlString.Append(string.Format("{0} ,\r\n", column.ColumnName.Trim()));
                        }
                    }
                    dmlString.Remove(dmlString.Length - 4, 4);
                    dmlString.Append(")\r\nVALUES(\r\n");
                    foreach (object column in row.ItemArray.Skip(1))
                    {
                        if (column.GetType() == typeof(System.DBNull))
                        {
                            dmlString.Append(string.Format("{0} ,\r\n", "NULL"));
                        }
                        else if (column.GetType() == typeof(System.Int32) || column.GetType() == typeof(System.Decimal) || (column.GetType() == typeof(System.String) && column.ToString().IndexOf("CURRENT TIME") > -1))
                        {
                            dmlString.Append(string.Format("{0} ,\r\n", column));
                        }
                        else if (column.GetType() == typeof(System.DateTime))
                        {
                                string formatedDate = DateTime.Parse(column.ToString()).ToString(DateFormat);
                                if (string.Equals(formatedDate, "11.11.1111"))
                                {
                                    dmlString.Append("'' ,\r\n");
                                }
                                else if (string.Equals(formatedDate, "11.11.2222"))
                                {
                                    dmlString.Append("NULL ,\r\n");
                                }
                                else if (string.Equals(formatedDate, "11.11.3333"))
                                {
                                    dmlString.Append("CURRENT DATE ,\r\n");
                                }
                                else
                                {
                                    dmlString.Append(string.Format("'{0}' ,\r\n", formatedDate));
                                }
                        }
                        else
                        {
                            string inputString = string.Format("'{0}' ,", column);
                            if (inputString.Length > 72)
                            {
                                foreach (string str in inputString.SplitByLength(72))
                                {
                                    dmlString.Append(string.Format("{0}\r\n", str));
                                }
                            }
                            else
                            {
                                dmlString.Append(string.Format("{0}\r\n", inputString));
                            }
                        }
                    }

                    dmlString.Remove(dmlString.Length - 4, 4);
                    dmlString.Append(");\r\n\r\n");
                }
            }

            return dmlString.ToString();
        }

        /// <summary>
        /// Gets the header information.
        /// </summary>
        /// <returns></returns>
        protected override string GetHeaderInformation()
        {
            StringBuilder headerInformation = new StringBuilder();
            headerInformation.Append("-- RELEASEID<xx>\r\n");
            headerInformation.Append("-- CHANGEID <xxxxxx>\r\n");
            headerInformation.Append("--*******************************************************************\r\n");
            headerInformation.Append(string.Format("--* TABLE NAME  :  {0}\r\n", this.dataTable.TableName));
            headerInformation.Append(string.Format("--* MODULE NAME : {0}\r\n", this.moduleName));
            headerInformation.Append("--* MODULE TYPE :  DBL\r\n");
            headerInformation.Append("--* PLATFORM    :  HOST / MVS\r\n");
            headerInformation.Append("--* GENERATED BY:  DML Automation tool v0.3\r\n");
            headerInformation.Append(string.Format("--* TIME        :  {0}\r\n", DateTime.Now.ToString()));
            headerInformation.Append(string.Format("--* SOURCE      :  {0}\r\n", this.sheetName));
            headerInformation.Append("--* \r\n--*******************************************************************\r\n");
            int insertCount = this.getActionCount("add");
            int deleteCount = this.getActionCount("delete") + insertCount;
            headerInformation.Append("--* TABLE NAME                          DELETES      INSERTS \r\n");
            headerInformation.Append(string.Format("--* {0}                             {1}           {2}\r\n",this.dataTable.TableName, deleteCount, insertCount));
            headerInformation.Append("--* \r\n--*******************************************************************\r\n");
            headerInformation.Append("--* \r\n--*******************************************************************\r\n");
            headerInformation.Append(string.Format("--*  MAINTENANCE LOG FOR {0}\r\n", this.dataTable.TableName));
            headerInformation.Append("--* \r\n");
            headerInformation.Append("--*  LOG #      DATE       EMP ID            DESCRIPTION\r\n");
            headerInformation.Append("--* -------  -----------  --------  ------------------------------- \r\n");
            headerInformation.Append(string.Format("--* <xxxxx> {0}    XXXXXXX   GENERATED.\r\n", DateTime.Now.ToShortDateString()));
            headerInformation.Append("--*******************************************************************\r\n");
            return headerInformation.ToString();
        }
    }
}
