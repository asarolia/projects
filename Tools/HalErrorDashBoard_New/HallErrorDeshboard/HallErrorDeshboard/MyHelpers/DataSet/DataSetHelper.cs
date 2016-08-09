using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for DataSetHelper
/// </summary>
namespace MyHelpers
{
    public class DataSetHelper
    {
        private const string InserTemplate = "INSERT INTO %REGION%.<table> (<coma-separated-columns>) VALUES (<coma-separated-values>);";
        private const string DeleteTemplate = "DELETE %REGION%.<table> WHERE <primary-key-check>;";

        private DataSetHelper()
        {
        }

        #region public static functions

        /// <summary>
        /// returns List of unique row column values in "Data" for input table. Condition is additional check on dataset, not mandatory
        /// </summary>
        /// <param name="Data">Input Dataset</param>
        /// <param name="ForTable">Table Name</param>
        /// <param name="ForColumn">Column Name</param>
        /// <param name="Condition">Condition in column=value format</param>
        /// <returns></returns>
        public static List<string> GetColumnValues(DataSet Data, string ForTable, string ForColumn, string Condition)
        {
            List<string> values = new List<string>();

            foreach (DataTable dt in Data.Tables)
            {
                if (dt.TableName == ForTable)
                {
                    int index = -1;
                    try
                    {
                        foreach (DataColumn dc in dt.Columns)
                        {
                            index++;
                            if (dc.ColumnName == ForColumn)
                                break;
                        }

                        if (index == dt.Columns.Count)
                            throw new InvalidOperationException("RelationalDatabaseConfig: ReadColumns - table '" + ForTable + "' does not contain column '" + ForColumn + "' ");

                    }
                    catch (Exception e)
                    {
                        throw new InvalidOperationException("RelationalDatabaseConfig: ReadColumns - table '" + ForTable + "' & column '" + ForColumn + "' locate failed - " + e.Message);
                    }

                    foreach (DataRow dr in dt.Rows)
                    {

                        if (!VerifyCondition(dr, Condition))
                            continue;

                        if (!values.Contains(dr[index].ToString()))
                            values.Add(dr[index].ToString());
                    }
                }
            }

            return values;
        }

        public static List<String> CreateInsertSQL(DataSet Data, string Region)
        {
            List<string> queries = new List<string>();
            Dictionary<string, string> Map = new Dictionary<string, string>();
            Map["REGION"] = Region;

            foreach (DataTable dt in Data.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    queries.Add(CreateSQL(InserTemplate, dt, dr, Map));
                }
            }
            return queries;
        }

        public static List<String> CreateDeleteSQL(DataSet Data, string Region)
        {
            List<string> queries = new List<string>();
            Dictionary<string, string> Map = new Dictionary<string, string>();
            Map["REGION"] = Region;

            foreach (DataTable dt in Data.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    queries.Add(CreateSQL(DeleteTemplate, dt, dr, Map));
                }
            }
            return queries;
        }


        public static void SetColumnValue(DataRow Row, DataColumn Column, string value)
        {
            object result = null;
            Int32 int32;
            Int16 int16;
            float flt;
            Single single;

            switch (Column.DataType.Name.ToLower())
            {
                case "string":
                    if (value.Length > 0)
                        result = value;
                    else if (!Column.AllowDBNull)
                        result = value;
                    break;

                case "int32":
                    if (Int32.TryParse(value, out int32))
                        result = int32;
                    break;

                case "int16":
                    if (Int16.TryParse(value, out int16))
                        result = int16;
                    break;

                case "single":
                    if (Single.TryParse(value, out single))
                        result = single;
                    break;

                case "float":
                    if (float.TryParse(value, out flt))
                        result = flt;
                    break;
                default:
                    throw new InvalidOperationException("DataSetHelper: SetColumnValue - " + Row.Table.TableName + "(" + Column.ColumnName + ") data type inconsistant '" + Column.DataType.Name.ToLower() + "'.");
            }

            try
            {
                if (result == null && Column.AllowDBNull)
                    Row[Column] = DBNull.Value;
                else if (result != null)
                    Row[Column] = result;
                else
                    throw new InvalidOperationException("DataSetHelper: SetColumnValue - " + Row.Table.TableName + "(" + Column.ColumnName + ") value '" + value + "' is inconsistant with data type '" + Column.DataType.Name.ToLower() + "'.");
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("DataSetHelper: SetColumnValue - " + Row.Table.TableName + "(" + Column.ColumnName + ")  - " + e.Message);
            }
        }


        public static Type ConvertDB2Type(string ColumnType, string Length, string Decimals, out int MaxLength, out int MaxDecimals)
        {
            MaxLength = 0;

            if (!Int32.TryParse(Length, out MaxLength))
                throw new InvalidDataException("DataSetHelper: ConvertDB2Type - Invalid length " + Length);
            MaxDecimals = 0;

            switch (ColumnType.Trim().ToLower())
            {
                case "char":
                    return typeof(string);
                case "varchar":
                    return typeof(string);
                case "smallint":
                case "integer":
                    MaxLength = 0;
                    return typeof(Int32);
                case "timestmp":
                    MaxLength = 0;
                    return typeof(string);
                case "date":
                    MaxLength = 10;
                    return typeof(string);
                case "time":
                    MaxLength = 20;
                    return typeof(string);
                case "decimal":

                    if (!Int32.TryParse(Decimals, out MaxDecimals))
                        throw new InvalidDataException("DataSetHelper: ConvertDB2Type - Invalid decimal " + Decimals);
                    MaxLength = 0;

                    return typeof(float);
                default:
                    throw new InvalidOperationException("DataSetHelper: ConvertDB2Type - DB2 Column Type not understood - " + ColumnType);
            }
        }


        #endregion

        #region private static functions

        private static bool VerifyCondition(DataRow Row, string Condition)
        {
            return true;
        }

        private static string CreateSQL(string Template, DataTable Table, DataRow Row, Dictionary<string, string> CustomMap)
        {
            Dictionary<string, string> param = BuildParameters(Table, Row);
            string query = Template;

            foreach (string key in param.Keys)
            {
                query = query.Replace(string.Format("<{0}>", key.ToLower().Trim()), param[key]);
            }

            foreach (string key in CustomMap.Keys)
            {
                query = query.Replace(string.Format("%{0}%", key.ToUpper().Trim()), CustomMap[key]);
            }
            return query;
        }

        private static Dictionary<string, string> BuildParameters(DataTable Table, DataRow Row)
        {
            Dictionary<string, string> Map = new Dictionary<string, string>();
            BuildDataTableParameters(Table, Map);
            BuildRowParameters(Table, Row, Map);
            return Map;
        }

        private static void BuildDataTableParameters(DataTable Table, Dictionary<string, string> Map)
        {
            //Table Name
            Map["table"] = Table.TableName;

            //coma-separated-columns
            string str = "";
            foreach (DataColumn col in Table.Columns)
            {
                str = str + (str.Length > 0 ? ", " : "") + col.ColumnName;
            }

            Map["coma-separated-columns"] = str;
        }

        private static void BuildRowParameters(DataTable Table, DataRow Row, Dictionary<string, string> Map)
        {
            //coma-separated-values
            string str = "";
            foreach (DataColumn col in Table.Columns)
            {
                str = str + (str.Length > 0 ? ", " : "") + WrapColumnValue(col.DataType, Row[col]);
            }
            Map["coma-separated-values"] = str;

            //primary-key-check
            str = "";
            foreach (DataColumn col in Table.PrimaryKey)
            {
                str = str + (str.Length > 0 ? " AND " : "") + String.Format("{0} = {1}", col.ColumnName, WrapColumnValue(col.DataType, Row[col]));
            }
            Map["primary-key-check"] = str;
        }

        public static string WrapColumnValue(Type ColumnType, Object data)
        {
            if (DBNull.Value.Equals(data))
                return "null";

            switch (ColumnType.Name.ToLower())
            {
                case "string":
                case "datatime":
                    return "'" + data.ToString().TrimEnd() + "'";
                default:
                    return data.ToString();
            }

        }


        public static void UpdateColumn(DataTable table, string Column, string value)
        {
            DataColumn column = null;
            //find column index
            foreach (DataColumn col in table.Columns)
            {
                if (col.ColumnName == Column)
                {
                    column = col;
                    break;
                }
            }

            if (column == null)
                throw new InvalidOperationException("DataSetHelper: UpdateColumn : column '" + Column + "' not found.");

            foreach (DataRow row in table.Rows)
            {
                SetColumnValue(row, column, value);
            }
        }

        public static string ParseQuery(string Query, DataTable Table)
        {
           return ParseQuery(Query, Table, false);
        }

        public static string ParseQuery(string Query, DataTable Table,bool castDateAndTimeStamp)
        {
            if (!castDateAndTimeStamp)
                return Query;

            // check if select * is used
            string[] words = Regex.Split(Query, @"\W+", RegexOptions.IgnorePatternWhitespace);

            if (!words[1].Trim().Equals("from"))
                return Query;

            string selection = "";

            foreach (DataColumn col in Table.Columns)
            {
                if (col.ExtendedProperties.Contains("HostType"))
                {
                    if (selection.Length > 0)
                        selection += ", ";

                        selection += ApplyCast(col.ColumnName, col.ExtendedProperties["HostType"].ToString());

                }
                else
                    return Query;
            }

            int index = Query.IndexOf("From", StringComparison.OrdinalIgnoreCase) + 4;
            return String.Format("select {0} from {1}", selection, Query.Substring(index));
        }

        public static DataSet SaveTo(DataSet dataset, string TargetFile)
        {
            string tempFile = TargetFile + ".tmp.save";

            while (File.Exists(tempFile))
            {
                tempFile += ".save";
            }

            dataset.WriteXml(tempFile, XmlWriteMode.WriteSchema);

            File.Delete(TargetFile);
            File.Copy(tempFile, TargetFile, true);

            return dataset;
        }

        public static DataSet LoadFrom(string SourceFile)
        {
            string tempFile = SourceFile + ".tmp.load";
            File.Copy(SourceFile, tempFile, true);

            DataSet dataset = new DataSet();
            dataset.ReadXml(tempFile, XmlReadMode.Auto);

            return dataset;
        }
        public static DataSet SaveTox(DataSet dataset, string TargetFile)
        {
            dataset.WriteXml(TargetFile, XmlWriteMode.WriteSchema);
            return dataset;
        }

        public static DataSet LoadFromx(string SourceFile)
        {
            DataSet dataset = new DataSet();
            dataset.ReadXml(SourceFile, XmlReadMode.Auto);

            return dataset;
        }

        private static string ApplyCast(string ColumnName, string Type)
        {
            if (Type.ToLower().IndexOf("timestmp") > -1)
                return String.Format("CAST({0} AS CHAR(32))", ColumnName);
            else if (Type.ToLower().IndexOf("date") > -1)
                return String.Format("CAST({0} AS CHAR(10))", ColumnName);
            else if (Type.ToLower().IndexOf("time") > -1)
                return String.Format("CAST({0} AS CHAR(10))", ColumnName);
            else return ColumnName;
        }

        #endregion
    }
}