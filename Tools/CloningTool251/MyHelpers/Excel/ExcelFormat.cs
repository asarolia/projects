using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.Collections.Generic;
using System.IO;
using MyHelpers.Properties;


namespace MyHelpers
{

    /// <summary>
    /// Summary description for ExcelFormat
    /// </summary>
    public class ExcelFormat : IFormat
    {
        DataSet MasterSet;
        StringBuilder FileHeader;

        public ExcelFormat()
        {
            //throw new Exception("The method or operation is not implemented.");
            StreamReader reader = new EmbeddedResource().GetStreamReader(Resource.ExcelFormatterHeader);
            FileHeader = new StringBuilder();

            FileHeader.Append(reader.ReadToEnd());
            reader.Close();
        }

        public override StringBuilder GenerateExcel(DataSet dataset)
        {
            return FormatDataSet(dataset);
        }

        //public override void PreFormat()
        //{
        //    MasterSet = new DataSet();
        //}

        //public override string Format(DataSet input)
        //{
        //    try
        //    {
        //        DataTable dt = input.Tables[0];
        //        MasterSet.Tables.Add(dt.Copy());
        //    }
        //    catch (Exception e)
        //    {
        //        throw new InvalidDataException(e.Message + input.Tables[0].TableName);
        //    }
        //    return FormatDataSet(input);
        //}

        //public override string getExtension()
        //{
        //    return ".xls";
        //}

        //public override string PostFormat()
        //{
        //    return FormatDataSet(MasterSet);
        //}

        #region private members
        private StringBuilder FormatDataSet(DataSet CurrentDataSet)
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine(GetPageHeader());
            str.AppendLine(GetIndexData(CurrentDataSet));
            foreach (DataTable dt in CurrentDataSet.Tables)
            {
                str.AppendLine(FormatTable(dt));
            }
            str.AppendLine(GetPageTrailer());
            return str;
        }

        private string GetPageHeader()
        {
            return FileHeader.ToString();
        }

        private string GetIndexData(DataSet ds)
        {
            StringBuilder str = new StringBuilder();

            string CelDataEnds = "</Data></Cell>";

            str.AppendLine("<Worksheet ss:Name=\"Table Index\">");
            str.AppendLine("<Table ss:DefaultColumnWidth=\"180\" x:FullColumns=\"1\" x:FullRows=\"1\">");
            str.AppendLine("<Row><Cell ss:StyleID=\"TabComment\"><Data ss:Type=\"String\">");
            str.AppendLine("Table" + CelDataEnds + "<Cell ss:StyleID=\"TabComment\"><Data ss:Type=\"String\">" + "Comments" + CelDataEnds + "</Row>");

            foreach (DataTable dt in ds.Tables)
            {
                str.AppendLine("<Row><Cell ss:StyleID=\"Hyperlink\" ss:HRef= \"#'" + dt.TableName + "'!A1\"><Data ss:Type=\"String\">" + dt.TableName + "</Data></Cell></Row>");
            }
            str.AppendLine("</Table></Worksheet>");
            return str.ToString();
        }

        private string GetPageTrailer()
        {
            return "</Workbook>";
        }

        private string FormatTable(DataTable CurrentTable)
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine(GetHeader(CurrentTable));
            str.AppendLine(GetColumnData(CurrentTable));
            str.AppendLine(GetMiddleData(CurrentTable));
            str.AppendLine(GetTailer());
            return str.ToString();
        }


        private string GetHeader(DataTable CurrentTable)
        {
            StringBuilder str = new StringBuilder();

            String tableName = "", viewName = "";

            try
            {
                tableName = CurrentTable.ExtendedProperties["TableName"].ToString();
                viewName = CurrentTable.ExtendedProperties["ViewName"].ToString();
                if (viewName.Length == 0) viewName = tableName;
            }
            catch (Exception)
            {
            }

            str.AppendLine("<Worksheet ss:Name=\"" + viewName + "\">");
            str.AppendLine("<Table ss:DefaultColumnWidth=\"180\" x:FullColumns=\"1\" x:FullRows=\"1\">");
            str.AppendLine("<Column ss:AutoWidth=\"1\"/>");
            str.AppendLine("<Row><Cell/><Cell/><Cell/><Cell ss:StyleID=\"Hyperlink\" ss:HRef=\"#'Table Index'!A1\">");
            str.AppendLine("<Data ss:Type=\"String\">");
            str.AppendLine("Return to Table Index");
            str.AppendLine("</Data></Cell></Row>");
            str.AppendLine("<Row><Cell ss:StyleID=\"Bold\">");
            str.AppendLine("<Data ss:Type=\"String\">Exceed Table:</Data></Cell>");
            str.AppendLine("<Cell><Data ss:Type=\"String\">" + tableName + "</Data></Cell>");
            str.AppendLine("<Cell/><Cell ss:StyleID=\"Bold\"><Data ss:Type=\"String\">View:</Data></Cell>");
            str.AppendLine("<Cell><Data ss:Type=\"String\">" + viewName + "</Data></Cell>");
            str.AppendLine("</Row>");
            return str.ToString();
        }

        private string GetTailer()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("</Table>");
            str.AppendLine("</Worksheet>");
            return str.ToString();
        }

        private string GetColumnData(DataTable CurrentTable)
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine("<Row><Cell ss:StyleID=\"Bold\"><Data ss:Type=\"String\">Column Name:</Data></Cell>");
            foreach (DataColumn dataCol in CurrentTable.Columns)
            {
                str.AppendLine("<Cell ss:StyleID=\"ColName\"><Data ss:Type=\"String\">" + dataCol.ColumnName + "</Data></Cell>");
            }

            str.AppendLine("</Row>");
            str.AppendLine("<Row><Cell>");
            str.AppendLine("<Data ss:Type=\"String\">Type:</Data></Cell>");
            foreach (DataColumn dataCol in CurrentTable.Columns)
            {
                str.AppendLine("<Cell ss:StyleID=\"Type\"><Data ss:Type=\"String\">" + dataCol.ExtendedProperties["HostType"] + "</Data></Cell>");
            }
            str.AppendLine("</Row>");

            str.AppendLine("<Row>");
            str.AppendLine("<Cell><Data ss:Type=\"String\">Primary key:</Data></Cell>");

            List<DataColumn> primaryKey = new List<DataColumn>(CurrentTable.PrimaryKey);

            foreach (DataColumn dataCol in CurrentTable.Columns)
            {
                if (primaryKey.Count == 0 || primaryKey.Exists(delegate(DataColumn dc) { return dataCol.ColumnName == dc.ColumnName; }))
                {
                    str.AppendLine("<Cell ss:StyleID=\"Type\"><Data ss:Type=\"String\">Yes</Data></Cell>");
                }
                else
                {
                    str.AppendLine("<Cell ss:StyleID=\"Type\"/>");
                }
            }
            str.AppendLine("</Row>");
            return str.ToString();
        }


        private string GetMiddleData(DataTable CurrentTable)
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine("<Row>");
            str.AppendLine("<Cell ss:StyleID=\"StartEnd\"><Data ss:Type=\"String\">start</Data></Cell>");
            str.AppendLine("</Row>");
            foreach (DataRow row in CurrentTable.Rows)
            {
                str.AppendLine("<Row>");
                str.AppendLine("<Cell ss:StyleID=\"Add\"><Data ss:Type=\"String\">add</Data></Cell>");
                foreach (DataColumn col in CurrentTable.Columns)
                {
                    str.AppendLine("<Cell ss:StyleID=\"DataText\"><Data ss:Type=\"String\">" + row[col].ToString().Trim() + "</Data></Cell>");
                }
                str.AppendLine("</Row>");
            }
            str.AppendLine("<Row><Cell ss:StyleID=\"StartEnd\"><Data ss:Type=\"String\">end</Data></Cell></Row>");

            return str.ToString();
        }
        #endregion private members
    }
}