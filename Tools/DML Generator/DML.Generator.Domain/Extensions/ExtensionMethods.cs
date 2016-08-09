﻿using System;
using System.Collections.Generic;
using Data = System.Data;
using System.Diagnostics.Contracts;
using DML.Generator.Domain;
using Microsoft.Office.Interop.Excel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DML.Generator.Domain.Extensions
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Toes the data table.
        /// </summary>
        /// <param name="Info">The info.</param>
        /// <returns></returns>
        public static Data::DataTable ToDataTable(this DMLInfo Info)
        { 
            Contract.Requires(Info != null);
            Data::DataTable table = new Data::DataTable(Info.SheetData.TableName);
            BuildDataColumns(ref table,Info);
            return table;
        }

        /// <summary>
        /// Builds the data columns.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="Info">The info.</param>
        private static void BuildDataColumns(ref Data::DataTable table, DMLInfo Info)
        {
            List<Data::DataColumn> keyColumn = new List<Data::DataColumn>();
            
            Data::DataColumn actionColumn = new Data::DataColumn
                {
                    ColumnName = "Action",
                    DataType = typeof(System.String)
                };
            table.Columns.Add(actionColumn);

            for (int i = 2; i <= Info.SheetData.ColumnCount; i++)
            {
                try
                {
                    if (Info.SheetData.UsedRange[Info.SheetData.ColumnRowIndex, i] != null)
                    {
                        Data::DataColumn column = new Data::DataColumn
                        {
                            ColumnName = Info.SheetData.UsedRange[Info.SheetData.ColumnRowIndex, i].ToString(),
                            DataType = GetDataType(Info.SheetData.UsedRange[Info.SheetData.TypeRowIndex, i].ToString())
                        };

                        SetProperties(Info.SheetData.UsedRange[Info.SheetData.TypeRowIndex, i].ToString(), ref column);

                        if (Info.SheetData.UsedRange[Info.SheetData.PrimaryRowIndex, i] != null && string.Equals(Info.SheetData.UsedRange[Info.SheetData.PrimaryRowIndex, i].ToString().ToLower(), "yes"))
                        {
                            keyColumn.Add(column);
                        }
                        table.Columns.Add(column);
                    }
                }
                catch (Exception ex)
                {
                    Info.Errors.Add(ex.Message);
                }
            }

            if (keyColumn.Count == 0)
            {
                foreach (Data::DataColumn column in table.Columns)
                {
                    if (!string.Equals(column.ColumnName, "Action"))
                    {
                        keyColumn.Add(column);
                    }
                }
            }

            table.PrimaryKey = keyColumn.ToArray();
            PopulateDataTable(ref table, Info);
        }

        /// <summary>
        /// Populates the data table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="Info">The info.</param>
        private static void PopulateDataTable(ref Data::DataTable table, DMLInfo Info)
        {
            int rowCount = default(int);
            int colCount = default(int);
            for (rowCount = Info.SheetData.StartRowIndex + 1; rowCount < Info.SheetData.EndRowIndex; rowCount++)
            {
                try
                {
                    Data::DataRow dataRow = table.NewRow();
                    for (colCount = 1; colCount <= dataRow.Table.Columns.Count; colCount++)
                    {
                        if (Info.SheetData.UsedRange[rowCount, colCount] != null && !string.IsNullOrEmpty(Info.SheetData.UsedRange[rowCount, colCount].ToString()))
                        {
                            if (string.Equals(Info.SheetData.UsedRange[rowCount, colCount].ToString().Trim().ToLower(), "null"))
                            {
                                dataRow[colCount - 1] = DBNull.Value;
                            }
                            else if (Info.SheetData.UsedRange[Info.SheetData.TypeRowIndex, colCount].ToString().ToUpper().IndexOf("TIME") > -1)
                            {
                                Regex regex = new Regex("[0-9]{4}-[0-9]{2}-[0-9]{2}-[0-9]{2}.[0-9]{2}.[0-9]{2}.[0-9]{6}");

                                if (Info.SheetData.UsedRange[rowCount, colCount].ToString().ToUpper().IndexOf("CURRENT TIME") > -1
                                    || regex.IsMatch(Info.SheetData.UsedRange[rowCount, colCount].ToString()))
                                {
                                    dataRow[colCount - 1] = Info.SheetData.UsedRange[rowCount, colCount];
                                }
                                else
                                {
                                    throw new ArgumentException(string.Format("Invalid timestamp at row - {0}, col - {1}", rowCount, colCount));
                                }
                            }
                            else if (dataRow.Table.Columns[colCount - 1].DataType == typeof(System.Decimal))
                            {
                                dataRow[colCount - 1] = string.Format("{0:F2}", Info.SheetData.UsedRange[rowCount, colCount]);
                            }
                            else if (dataRow.Table.Columns[colCount - 1].DataType == typeof(System.DateTime)
                                && string.Equals(Info.SheetData.UsedRange[rowCount, colCount].ToString().ToUpper(), "CURRENT DATE"))
                            {
                                dataRow[colCount - 1] = DateTime.Parse("11/11/3333");
                            }
                            else if (dataRow.Table.Columns[colCount - 1].DataType == typeof(System.DateTime)
                            && string.Equals(Info.SheetData.UsedRange[rowCount, colCount].ToString().ToUpper(), "NULL"))
                            {
                                dataRow[colCount - 1] = DateTime.Parse("11/11/2222");
                            }
                            else
                            {
                                dataRow[colCount - 1] = Info.SheetData.UsedRange[rowCount, colCount].ToString().Trim();
                            }
                        }
                        else
                        {
                            if (dataRow.Table.Columns[colCount - 1].DataType == typeof(System.String))
                            {
                                dataRow[colCount - 1] = string.Empty;
                            }

                            if (dataRow.Table.Columns[colCount - 1].DataType == typeof(System.Decimal))
                            {
                                dataRow[colCount - 1] = 0.00;
                            }

                            if (dataRow.Table.Columns[colCount - 1].DataType == typeof(System.Int32))
                            {
                                dataRow[colCount - 1] = 0;
                            }

                            if (dataRow.Table.Columns[colCount - 1].DataType == typeof(System.DateTime))
                            {
                                dataRow[colCount - 1] = DateTime.Parse("11/11/1111");
                            }
                        }
                    }
                    object[] key = new object[table.PrimaryKey.Length];
                    int i = default(int);
                    
                    foreach(Data::DataColumn col in table.PrimaryKey)
                    {
                        key[i] = dataRow.ItemArray[col.Ordinal];
                        i++;
                    }

                    if (table.Rows.Contains(key))
                    {
                        Data::DataRow dr = table.Rows.Find(key);
                        if (string.Equals(dr.ItemArray[0].ToString().ToLower(), "delete"))
                        {
                            dr.Delete();
                        }
                    }
                    table.Rows.Add(dataRow);
                }
                catch(Exception ex)
                {
                    Info.Errors.Add(string.Format("Error in row - {0}, col - {1} - {2}", rowCount, colCount, ex.Message));
                }
            }
        }
            

        /// <summary>
        /// Sets the properties.
        /// </summary>
        /// <param name="colType">Type of the col.</param>
        /// <param name="column">The column.</param>
        /// <remarks></remarks>
        private static void SetProperties(string colType, ref Data.DataColumn column)
        {
            if (column.DataType == typeof(System.String))
            { 
                if (colType.ToUpper().IndexOf('(') > -1)
                {
                    column.MaxLength = Int32.Parse(colType.Split('(')[1].Replace(")", string.Empty).Trim());
                }
            }
        }

        /// <summary>
        /// Splits the length of the by.
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <param name="maxLength">Length of the max.</param>
        /// <returns></returns>
        public static IEnumerable<string> SplitByLength(this string str, int maxLength)
        {
            for (int index = 0; index < str.Length; index += maxLength)
            {
                yield return str.Substring(index, Math.Min(maxLength, str.Length - index));
            }
        }

        /// <summary>
        /// Gets the type of the data.
        /// </summary>
        /// <param name="ColType">Type of the col.</param>
        /// <returns></returns>
        private static Type GetDataType(string colType)
        {
            if (colType.ToUpper().IndexOf("CHAR") > -1 
                || colType.ToUpper().IndexOf("TIME") > -1 
                || colType.ToUpper().IndexOf("VARCHAR") > -1 
                || colType.ToUpper().IndexOf("VC") > -1)
            {
                return System.Type.GetType("System.String");
            }
            
            if (colType.ToUpper().IndexOf("INT") > -1)
            {
                return System.Type.GetType("System.Int32");
            }
                       
            if (colType.ToUpper().IndexOf("DATE") > -1 )
            {
                return System.Type.GetType("System.DateTime");
            }
            
            if (colType.ToUpper().IndexOf("DECIMAL") > -1)
            {       
                return System.Type.GetType("System.Decimal");
            }

            throw new ArgumentException (string.Format("Data type is not supported for {0}", colType));
        }
    }
}