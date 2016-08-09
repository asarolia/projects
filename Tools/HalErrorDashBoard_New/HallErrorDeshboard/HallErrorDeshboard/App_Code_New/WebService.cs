using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{
    ValidateXMLFileNew valXML = new ValidateXMLFileNew();
    XmlDocument xmlDoc;
    XmlNodeList xmlnd;
    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    public class ValidateXMLFileNew
    {
        XmlDocument xmlDoc;
        ArrayList arr;
        XmlNodeList xmlnd;
        string sMapPath;
        string sUserName;
        string sSQLErrorMessage;
        StringBuilder sErrorMessage;
        bool chckDuplicate;
        XmlDocument xmlTabSchema;
        XmlDocument xmlMatrixSchema;
        XmlDocument xmlDropDownSchema;
        public ValidateXMLFileNew()
        {
            ErrorMessage = new StringBuilder();
        }
        public string SPath
        {
            get
            {
                return sMapPath;
            }
            set
            {
                sMapPath = value;
            }
        }
        public string UserName
        {
            get
            {
                return sUserName;
            }
            set
            {
                sUserName = value;
            }
        }
        public string SQLErrorMessage
        {
            get
            {
                return sSQLErrorMessage;
            }
            set
            {
                sSQLErrorMessage = value;
            }
        }
        public StringBuilder ErrorMessage
        {
            get
            {
                return sErrorMessage;
            }
            set
            {
                sErrorMessage = value;
            }
        }
        public XmlDocument XmlTableSchema
        {
            get
            {
                return xmlTabSchema;
            }
            set
            {
                xmlTabSchema = value;
            }
        }
        public XmlDocument XmlMatrixSchema
        {
            get
            {
                return xmlMatrixSchema;
            }
            set
            {
                xmlMatrixSchema = value;
            }
        }
        public XmlDocument XmlDropDownSchema
        {
            get
            {
                return xmlDropDownSchema;
            }
            set
            {
                xmlDropDownSchema = value;
            }
        }
        public bool ChckDuplicate
        {
            get
            {
                return chckDuplicate;
            }
            set
            {
                chckDuplicate = value;
            }
        }
        public string GetSchemaAttribute(XmlNode xmlRow, string attribute)
        {
            string attributeValue = string.Empty;
            try
            {
                attributeValue = xmlRow.Attributes[attribute].Value.ToString();
            }
            catch (Exception e)
            {
            }
            return attributeValue;
        }

        public ArrayList GetSQLColumnOptions(string colName)
        {
            SqlDataReader sdr;
            SqlConnection sconn = new SqlConnection(ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString);
            string query="";
            if (string.Equals(colName, "statustext", StringComparison.OrdinalIgnoreCase))
            {
                query = "SELECT StatusText, Code from status";
            }
            /*else if (string.Equals(colName, "Release", StringComparison.OrdinalIgnoreCase))
            {
                query = "SELECT DISTINCT [RELEASE_NAME] FROM [dbo].[RELEASE_TAB]";
            }
            else
            {
                query = "SELECT * FROM support_tab WHERE FIELD_TYPE='" + colName + "' ";
            }*/
            SqlCommand sqlcmd = new SqlCommand(query, sconn);
            ArrayList drpDwnCollection = new ArrayList();
            try
            {
                sconn.Open();
                sdr = sqlcmd.ExecuteReader();
                if (sdr.HasRows)
                {

                    if (string.Equals(colName, "statustext", StringComparison.OrdinalIgnoreCase))
                    {
                        drpDwnCollection.Add("");
                        while (sdr.Read())
                        {
                            drpDwnCollection.Add(sdr["Code"].ToString() + " - " + sdr["StatusText"].ToString());
                        }
                    }
                  /*  else if (string.Equals(colName, "Release", StringComparison.OrdinalIgnoreCase))
                    {
                        drpDwnCollection.Add("");
                        drpDwnCollection.Add("Not assigned to a release");//Hard Coded value
                        while (sdr.Read())
                        {
                            drpDwnCollection.Add(sdr["RELEASE_NAME"].ToString());
                        }
                    }*/
                    else
                    {
                        while (sdr.Read())
                        {
                            drpDwnCollection.Add(sdr["FIELD_DESCRIPTION"].ToString());
                        }
                    }
                }
                sdr.Close();

            }
            catch (Exception e)
            {
                //Log.Write("ValidateXMLFileNew.ExecuteQuery - ApplicationException Occured while running sql: " + e.Message, Log.LOG_CATEGORY_AVIVADEFAULT);
            }
            finally
            {
                if (sqlcmd != null)
                    sqlcmd.Dispose();

                if (sconn != null)
                {
                    sconn.Close();
                    sconn.Dispose();
                }
            }
            return drpDwnCollection;
        }


        [WebMethod]
        public ResponseTemplate CreateAjaxTemplate(string tableName, string Id, string operation)
        {
            StringBuilder html = new StringBuilder();
            StringBuilder javaScript = new StringBuilder();
            //StringBuilder scriptRules=new StringBuilder();
            //StringBuilder scriptMessages=new StringBuilder();
            string tableWidth = "10px";
            switch (tableName.ToUpper())
            {
                case "MASTERERROR":
                    tableWidth = "10px";
                    break;
            }

            string formStart = "<form id='" + tableName + "' class='" + operation.ToUpper() + "'>";
            string tableStart = "<table cellspacing='0' rules='all' border='1' id='" + tableName + "_View' style='font-size:small;height:30px;border-collapse:collapse; width:" + tableWidth + ";'>";
            string tableEnd = "</table>";
            string rowStart = "<tr>";
            string rowEnd = "</tr>";
            string colStart = "<td>";
            string colEnd = "</td>";
            string readonlyhtml = "readonly='readonly' disabled='disabled'";
            string selectedhtml = "selected='selected'";
            DataTable dt = GetData(tableName, Id, operation);


            string[] controlIDs = null;

            html.Append(formStart);
            html.Append(tableStart);
            XmlNodeList xmlnd = XmlTableSchema.DocumentElement.ChildNodes;
            arr = new ArrayList();
            foreach (XmlNode xmlnde in xmlnd)
            {
                if (xmlnde.Attributes["TableName"].Value.ToString() == tableName.ToUpper())
                {
                    foreach (XmlNode xmlrow in xmlnde.ChildNodes)
                    {
                        try
                        {
                            #region GetAttribute Values
                            string sqlname = GetSchemaAttribute(xmlrow, "SqlColName");
                            string label = GetSchemaAttribute(xmlrow, "ExcelMatchColName");
                            string controlType = GetSchemaAttribute(xmlrow, "INPUT");
                            string autoSQL = GetSchemaAttribute(xmlrow, "Auto");
                            string maxlength = GetSchemaAttribute(xmlrow, "length");
                            string required = GetSchemaAttribute(xmlrow, "Required");
                            string validate = GetSchemaAttribute(xmlrow, "Validate");
                            string datatype = GetSchemaAttribute(xmlrow, "DataType");
                            string duplicate = GetSchemaAttribute(xmlrow, "DuplicateKey");
                            string unit = GetSchemaAttribute(xmlrow, "Unit");
                            string readonlyControl = GetSchemaAttribute(xmlrow, "ReadOnly");
                            #endregion

                            StringBuilder valdiationClass = new StringBuilder();
                            if (string.Equals(validate, "date", StringComparison.OrdinalIgnoreCase))
                            {
                                //unit = "yyyy-mm-dd";
                            }
                            //string control = "input";
                            //if (!string.Equals(maxlength,string.Empty)) 
                            //{
                            //    if (Convert.ToInt32(maxlength) >= 100)
                            //    {
                            //        control = "textarea rows=\"3\" cols=\"5\"";
                            //    }
                            //}

                            html.Append(rowStart);
                            html.Append(colStart);
                            html.Append("<label for='" + sqlname + "'>" + label + "</label>");
                            html.Append(colEnd);
                            html.Append(colStart);

                            switch (operation)
                            {
                                case "VIEW":
                                    if (sqlname == "StatusCd")
                                    {
                                        sqlname = "StatusText";
                                    }
                                    html.Append(dt.Rows[0][sqlname].ToString());
                                    break;

                                case "INSERT":

                                    if (required.Length > 0)
                                    {
                                        valdiationClass.Append("required");
                                    }
                                    if (string.Equals(validate, "date", StringComparison.OrdinalIgnoreCase))
                                    {
                                        valdiationClass.Append("date");
                                    }
                                    else//other validation rules can be added here
                                    {

                                    }
                                    if ((string.Equals(datatype, "numeric", StringComparison.OrdinalIgnoreCase)))
                                    {
                                        valdiationClass.Append("number");// use digits for fields not requiring decimals
                                    }
                                    if (string.Equals(datatype, "int", StringComparison.OrdinalIgnoreCase))
                                    {
                                        valdiationClass.Append("digits");
                                    }

                                    switch (controlType.ToUpper())
                                    {
                                        case "DROPDOWN":
                                            html.Append("<select class='" + valdiationClass + "' id='" + tableName + sqlname + "' name='" + label + "'>");
                                            ArrayList colOpt = GetSQLColumnOptions(sqlname);
                                            html.Append("<option selected='selected' value='' >--SELECT--</option>");
                                            for (int coloptCount = 0; coloptCount < colOpt.Count; coloptCount++)
                                            {
                                                html.Append("<option value='" + colOpt[coloptCount].ToString() + "'>" + colOpt[coloptCount].ToString() + "</option>");
                                            }
                                            html.Append("</select>");
                                            break;
                                        default:

                                            if (autoSQL.Length > 0)//disable controls
                                            {
                                                html.Append("<input id='" + tableName + sqlname + "' name='" + label + "' class='" + valdiationClass + "'  maxlength='" + maxlength + "' " + readonlyhtml + " />");
                                            }
                                            else
                                            {
                                                html.Append("<input id='" + tableName + sqlname + "' name='" + label + "' class='" + valdiationClass + "'  maxlength='" + maxlength + "' />");
                                            }
                                            break;
                                    }
                                    if (unit.Length > 0)
                                    {
                                        html.Append("&nbsp; " + unit);
                                    }
                                    break;

                                case "UPDATE":

                                    if (required.Length > 0)
                                    {
                                        valdiationClass.Append("required");
                                    }
                                    if (string.Equals(validate, "date", StringComparison.OrdinalIgnoreCase))
                                    {
                                        valdiationClass.Append("date");
                                    }
                                    else//incomplete
                                    {

                                    }
                                    if ((string.Equals(datatype, "numeric", StringComparison.OrdinalIgnoreCase)))
                                    {
                                        valdiationClass.Append("number");// use digits for fields not requiring decimals
                                    }
                                    if (string.Equals(datatype, "int", StringComparison.OrdinalIgnoreCase))
                                    {
                                        valdiationClass.Append("digits");// use digits for fields not requiring decimals
                                    }

                                    string value = dt.Rows[0][sqlname].ToString();
                                    switch (controlType.ToUpper())
                                    {
                                        case "DROPDOWN":
                                            html.Append("<select class='" + valdiationClass + "' id='" + tableName + sqlname + "' name='" + label + "'>");
                                            ArrayList colOpt = new ArrayList();
                                            colOpt = GetSQLColumnOptions(sqlname);
                                            for (int coloptCount = 0; coloptCount < colOpt.Count; coloptCount++)
                                            {
                                                if (string.Equals(value, colOpt[coloptCount].ToString(), StringComparison.OrdinalIgnoreCase))
                                                {
                                                    html.Append("<option " + selectedhtml + " value='" + colOpt[coloptCount].ToString() + "'>" + colOpt[coloptCount].ToString() + "</option>");
                                                }
                                                else
                                                {
                                                    html.Append("<option  value='" + colOpt[coloptCount].ToString() + "'>" + colOpt[coloptCount].ToString() + "</option>");
                                                }
                                            }
                                            html.Append("</select>");
                                            break;
                                        default:
                                            if (autoSQL.Length > 0 || readonlyControl.Length > 0)//disable controls
                                            {
                                                html.Append("<input id='" + tableName + sqlname + "' value='" + value + "' name='" + label + "' class='" + valdiationClass + "' maxlength='" + maxlength + "' " + readonlyhtml + " />");
                                            }
                                            else
                                            {
                                                html.Append("<input id='" + tableName + sqlname + "' value='" + value + "' name='" + label + "' class='" + valdiationClass + "' maxlength='" + maxlength + "' />");
                                            }
                                            break;
                                    }
                                    if (unit.Length > 0)
                                    {
                                        html.Append("&nbsp; " + unit);
                                    }
                                    break;
                            }

                            html.Append(colEnd);
                            html.Append(rowEnd);

                        }
                        catch
                        {

                        }
                    }

                    //Add buttons for submitting data
                    switch (operation)
                    {
                        case "VIEW":
                            controlIDs = new string[dt.Columns.Count];//Will contain control IDs 
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                controlIDs[i] = dt.Columns[i].ColumnName;
                            }
                            break;
                        case "INSERT":
                            controlIDs = new string[dt.Rows.Count];//Will contain control IDs 
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                controlIDs[i] = dt.Rows[i][0].ToString();
                            }
                            html.Append(rowStart);
                            html.Append(colStart);
                            html.Append("<input id='btn" + tableName + "Insert' type='submit' value='Insert' />");
                            html.Append("<input id='btn" + tableName + "Cancel' type='button' value='Cancel' onClick=\"fnCancel('" + tableName + "');\" />");
                            html.Append(colEnd);
                            html.Append(rowEnd);
                            break;
                        case "UPDATE":
                            controlIDs = new string[dt.Columns.Count];//Will contain control IDs 
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                controlIDs[i] = dt.Columns[i].ColumnName;
                            }
                            html.Append(rowStart);
                            html.Append(colStart);
                            html.Append("<input id='btn" + tableName + "Update' type='submit' value='Update' />");
                            html.Append("<input id='btn" + tableName + "Cancel' type='button' value='Cancel' onClick=\"fnCancel('" + tableName + "');\" />");
                            html.Append(colEnd);
                            html.Append(rowEnd);
                            break;
                    }


                }
            }
            html.Append(tableEnd);
            html.Append("</form>");
            ResponseTemplate response = new ResponseTemplate();
            response.html = html.ToString();
            response.controlIDs = controlIDs;
            response.tableName = tableName;
            return response;
        }
    }
    public class scheme
    {
        public scheme()
        { }
        private string _fromdt;
        public string fromdt
        {
            get { return _fromdt; }
            set { _fromdt = value; }
        }

        private string _todt;
        public string todt
        {
            get { return _todt; }
            set { _todt = value; }
        }


    }

        public class operationScheme
    {
        public operationScheme()
        { }
        public string[] data;
        public string tableName;
    }

        public class ResponseTemplate
        {
            public ResponseTemplate()
            { }
            public string html;
            public string tableName;
            public string[] controlIDs;
        }

        public class templateScheme
        {
            public templateScheme()
            { }
            public string tableName;
            public string Id;
            public string operation;
        }
        
        private static DataTable GetData(string tableName, string Id, string operation)
        {
            //Get data for particular ID
            SqlConnection sConn = null;
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            sConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString);
            try
            {
                sConn.Open();
                if (string.Equals(operation, "INSERT", StringComparison.OrdinalIgnoreCase))
                {

                    cmd.Connection = sConn;
                    cmd.CommandText = "SELECT * FROM " + tableName;
                    SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.KeyInfo);

                    //Retrieve column schema into a DataTable.
                    dt = myReader.GetSchemaTable();
                }
                else
                {
                    string query = "SELECT a.Guid, a.ReportDate, a.ErrorId, a.ErrorText, a.Source,a.Count, b.statustext, a.Comment FROM " + tableName + " a Inner Join status b on a.statuscd = b.Code WHERE GUID='" + Id + "'";
                    cmd = new SqlCommand(query, sConn);
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }

            }
            finally
            {
                sConn.Close();
            }
            return dt;
        }


        public string GetSchemaAttribute(XmlNode xmlRow, string attribute)
        {
            string attributeValue = string.Empty;
            try
            {
                attributeValue = xmlRow.Attributes[attribute].Value.ToString();
            }
            catch (Exception e)
            {
            }
            return attributeValue;
        }

        [WebMethod(EnableSession = true)]
        public ResponseTemplate PopulateTemplateView(templateScheme templateScheme)
        {
            //string user = Session["UserName"].ToString();           
            XmlDocument xmlSchema = new XmlDocument();
            xmlSchema.Load(Server.MapPath("TableSchemas/Table.xml"));
            valXML.XmlTableSchema = xmlSchema;
            ResponseTemplate response =valXML.CreateAjaxTemplate(templateScheme.tableName, templateScheme.Id, templateScheme.operation);
            /*if (string.Equals(templateScheme.tableName, "LPR_MASTER", StringComparison.OrdinalIgnoreCase))
            {
                CreateLock(templateScheme.tableName, templateScheme.Id, user);
            }*/
            return response;
        }

        [WebMethod(EnableSession = true)]
        public void UpdateData(operationScheme operationScheme)
        {
            //string user = Session["UserName"].ToString();
            XmlDocument xmlSchema = new XmlDocument();
            xmlSchema.Load(Server.MapPath("TableSchemas/Table.xml"));
            valXML.XmlTableSchema = xmlSchema;
            DataTable dt = BuildDataTable(operationScheme.data, operationScheme.tableName);
            System.Text.StringBuilder query = new System.Text.StringBuilder();
            query.Append(BuildUpdateQuery(dt, operationScheme.tableName, "GUID", true));
            if (!insertdata(query))
            {
                throw new Exception(valXML.SQLErrorMessage);
            }
        }
        public bool insertdata(StringBuilder query)
        {
            if (query.Length == 0)
            {
                valXML.SQLErrorMessage = "No data found to upload";
                return false;
            }
            //Insert for various tables goes here 
            string sSQL = string.Empty;
            string sRole = string.Empty;
            SqlCommand sCmd = null;
            SqlConnection sConn = null;
            SqlTransaction sqlTR = null;
            bool result = false;
            string beginTrans = " Declare @Err as int begin tran T1 ";
            string commitTrans = " if @Err<>0 Rollback Tran t1 else commit tran T1 ";
            try
            {
                valXML.SQLErrorMessage = "";
                sConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString);
                sConn.Open();
                sqlTR = sConn.BeginTransaction();
                //sCmd = new SqlCommand(beginTrans + query.ToString() + commitTrans, sConn, sqlTR);
                sCmd = new SqlCommand(query.ToString(), sConn, sqlTR);
                sCmd.Transaction = sqlTR;
                //sCmd.ExecuteReader();
                sCmd.ExecuteNonQuery();
                result = true;
                sqlTR.Commit();
            }
            catch (ApplicationException ee)
            {
                //AlertMsg(ee);
                //Log.Write("ValidateXMLFileNew.insertdata - ApplicationException Occured while running sql: " + ee.Message, Log.LOG_CATEGORY_AVIVAIMPORT);
                result = false;
                valXML.SQLErrorMessage = ee.Message;
                sqlTR.Rollback();
            }
            catch (SqlException eSql)
            {
                sqlTR.Rollback();
                switch (eSql.Number.ToString())
                {
                    case "2627":
                        //unique constraint failed
                        result = false;
                        valXML.SQLErrorMessage = "Uploaded report contain multiple records having same primary keys.<br/>Please Rectify and upload again!";
                        break;
                    case "8152":
                        //truncated
                        result = false;
                        valXML.SQLErrorMessage = "Uploaded report contain records having data which exceeds maximum column length.<br/>Please Rectify and upload again!";
                        break;
                    default:
                        result = false;
                        valXML.SQLErrorMessage = eSql.Message;
                        break;
                }
            }
            catch (Exception ee)
            {
                //AlertMsg(ee); 
                //Log.Write("ValidateXMLFileNew.insertdata - Exception Occured while running sql: " + ee.Message, Log.LOG_CATEGORY_AVIVAIMPORT);
                result = false;
                valXML.SQLErrorMessage = ee.Message;
                sqlTR.Rollback();
            }
            finally
            {
                if (sCmd != null)
                    sCmd.Dispose();

                if (sConn != null)
                {
                    sConn.Close();
                    sConn.Dispose();
                }
            }
            return result;

        }
        public DataTable BuildDataTable(string[] data, string tableName)//Order of data is same as that of columns in Database
        {
            DataTable dt = new DataTable();
            DataTable dtSchema = GetData(tableName, "", "INSERT");
            for (int i = 0; i < dtSchema.Rows.Count; i++)
            {
                dt.Columns.Add(dtSchema.Rows[i][0].ToString());
            }
            dt.Rows.Add(data);
            return dt;
        }
        public string BuildUpdateQuery(DataTable dt, string tableName, string idColumn, bool updateFlag)//updateFlag would check if autosql attributes like date and user id needs to be updated
        {
            if (dt == null)
            {
                return string.Empty;
            }
            StringBuilder queryList = new StringBuilder();
            StringBuilder sequence = new StringBuilder();
            XmlNodeList xmlnd = valXML.XmlTableSchema.DocumentElement.ChildNodes;
            string value = string.Empty;
            string id = string.Empty;
            DateTime dt1;
            for (int i = 0; i < dt.Rows.Count; i++)//For each row of DataTable
            {
                sequence.Remove(0, sequence.Length);//clear values
                foreach (XmlNode xmlnde in xmlnd)//For each column in XML Schema
                {
                    if (xmlnde.Attributes["TableName"].Value.ToString() == tableName.ToUpper())
                    {
                        foreach (XmlNode xmlrow in xmlnde.ChildNodes)
                        {
                            value = string.Empty;
                            try
                            {
                                string sqlname = GetSchemaAttribute(xmlrow, "SqlColName");
                                string autoSQL = GetSchemaAttribute(xmlrow, "Auto");
                                string datatype = GetSchemaAttribute(xmlrow, "DataType");
                                string duplicate = GetSchemaAttribute(xmlrow, "DuplicateKey");
                                string operation = GetSchemaAttribute(xmlrow, "Operation");
                                string validate = GetSchemaAttribute(xmlrow, "Validate");
                                try
                                {
                                    if (sqlname == "statustext")
                                    {
                                        sqlname = "statuscd";
                                    }
                                    value = dt.Rows[i][sqlname].ToString();
                                }
                                catch
                                {
                                    if (autoSQL.Length == 0)
                                    {
                                        continue;//if value is not present in Datatable donot update that column
                                    }
                                }

                                if (string.Equals(idColumn, sqlname, StringComparison.OrdinalIgnoreCase))
                                {
                                    if (datatype.Contains("int") || datatype.Contains("decimal") || datatype.Contains("numeric"))
                                    {
                                        id = value;
                                    }
                                    else
                                    {
                                        id = "'" + value + "'";
                                    }

                                    
                                }

                                if (datatype.Contains("datetime"))
                                {
                                    dt1 = Convert.ToDateTime(value);
                                    value = dt1.Year + "-" + dt1.Month + "-" + dt1.Day;

                                }
                                switch (autoSQL.ToUpper())
                                {
                                    case "SQL"://skips this column as SQL generated value
                                        break;
                                    case "USERID":
                                        switch (operation.ToUpper())
                                        {
                                            case "ADD":
                                                break;
                                            case "UPDATE":
                                                if (updateFlag)
                                                {
                                                    sequence.Append("[" + sqlname + "]=");
                                                    //sequence.Append("'" + user + "',");
                                                }
                                                break;
                                        }
                                        break;
                                    case "SQLDATE":
                                        switch (operation.ToUpper())
                                        {
                                            case "ADD":
                                                break;
                                            case "UPDATE":
                                                if (updateFlag)
                                                {
                                                    sequence.Append("[" + sqlname + "]=");
                                                    sequence.Append("convert(datetime, getdate(),120),");
                                                }
                                                break;
                                        }
                                        break;
                                    default://Case with Fields without AutoSql attribute
                                        if (value != null)
                                        {
                                            if (datatype.Contains("int") || datatype.Contains("decimal") || datatype.Contains("numeric"))
                                            {
                                                if (string.Equals(value, string.Empty))
                                                {
                                                    sequence.Append("[" + sqlname + "]=");
                                                    sequence.Append(0 + ",");
                                                }
                                                else
                                                {
                                                    sequence.Append("[" + sqlname + "]=");
                                                    sequence.Append(value + ",");
                                                }
                                            }
                                            else
                                            {
                                                sequence.Append("[" + sqlname + "]=");
                                                /*if (sqlname.ToUpper().Contains("DES") || sqlname.ToUpper().Contains("COM"))
                                                {
                                                    int length = Convert.ToInt32(GetAttributeValue(tableName + "_V", sqlname, "length", "SqlColName"));
                                                    if (value.Length > length)
                                                    {
                                                        value = value.Substring(0, length - 1);
                                                    }
                                                }*/
                                                if (string.Equals(validate, "date", StringComparison.OrdinalIgnoreCase))
                                                {
                                                  //  if (!isValidDate(value, out value))
                                                    {
                                                        throw new Exception("Date format is not correct for " + value + ", please enter in yyyy-mm-dd format");
                                                    }
                                                }
                                                value = value.Replace("'", "''");//to escape ' in SQL 
                                                sequence.Append("'" + value + "',");
                                            }
                                        }
                                        break;
                                }
                            }
                            catch
                            {
                            }
                        }
                        sequence.Remove(sequence.Length - 1, 1);//removing the last comma
                        queryList.Append("UPDATE [dbo]." + tableName + "_V SET " + sequence + " WHERE [" + idColumn + "]=" + id + ";");
                    }
                }
            }
            return queryList.ToString();
        }

        public string GetAttributeValue(string TableName, string ColumnName, string AttributeName, string AttributeType)
        {
            string abc = string.Empty;
            //xmlDoc = new XmlDocument();
            try
            {
                //xmlDoc.Load(SPath + @"\\TableSchemas\\TableSchemas.xml");            
                xmlnd = valXML.XmlTableSchema.DocumentElement.ChildNodes;
                foreach (XmlNode xmlnde in xmlnd)
                {
                    //XmlNodeList xmlnd1 = xmlDoc.DocumentElement.ChildNodes;
                    if (xmlnde.Attributes["Name"].Value.ToString() == TableName.ToUpper())
                    {
                        foreach (XmlNode xmlnde1 in xmlnde.ChildNodes)
                        {
                            if (xmlnde1.Attributes[AttributeType].Value.ToString() == ColumnName.ToUpper())
                            {
                                if (xmlnde1.Attributes[AttributeName] != null)
                                {
                                    try
                                    {
                                        abc = xmlnde1.Attributes[AttributeName].Value.ToString();
                                    }
                                    catch (Exception e)
                                    {
                                        abc = "";
                                    }
                                }
                                else
                                {
                                    abc = "";
                                }
                                break;
                            }
                        }
                    }
                }
            }
            catch (XmlException ee)
            {

            }
            catch (ArgumentException ee)
            {

            }
            return (abc);
        }

        public ArrayList GetSQLColumnOptions(string colName)
        {
            SqlDataReader sdr;
            SqlConnection sconn = new SqlConnection(ConfigurationManager.ConnectionStrings["NUConnectionString"].ConnectionString);
            string query;
            /*if (string.Equals(colName, "Primary_Owner", StringComparison.OrdinalIgnoreCase))
            {
                query = "SELECT DISTINCT USER_TABLE.FIRST_NAME, USER_TABLE.LAST_NAME FROM USER_TABLE JOIN USER_DETAILS_TABLE ON USER_TABLE.USER_ID=USER_DETAILS_TABLE.USER_ID WHERE USER_DETAILS_TABLE.USER_PROFILE LIKE '%L%' ORDER BY USER_TABLE.FIRST_NAME";
            }*/
            /* if (string.Equals(colName, "Release", StringComparison.OrdinalIgnoreCase))
            {
                query = "SELECT DISTINCT [RELEASE_NAME] FROM [dbo].[RELEASE_TAB]";
            }
            else
            {*/
            query = "SELECT * FROM status"; //WHERE FIELD_TYPE='" + colName + "' ";
            //}
            SqlCommand sqlcmd = new SqlCommand(query, sconn);
            ArrayList drpDwnCollection = new ArrayList();
            try
            {
                sconn.Open();
                sdr = sqlcmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        drpDwnCollection.Add(sdr["StatusText"].ToString());
                    }
                   /* if (string.Equals(colName, "Primary_Owner", StringComparison.OrdinalIgnoreCase))
                    {
                        drpDwnCollection.Add("");
                        while (sdr.Read())
                        {
                            drpDwnCollection.Add(sdr["FIRST_NAME"].ToString() + " " + sdr["LAST_NAME"].ToString());
                        }
                    }
                    else if (string.Equals(colName, "Release", StringComparison.OrdinalIgnoreCase))
                    {
                        drpDwnCollection.Add("");
                        drpDwnCollection.Add("Not assigned to a release");//Hard Coded value
                        while (sdr.Read())
                        {
                            drpDwnCollection.Add(sdr["RELEASE_NAME"].ToString());
                        }
                    }
                    else
                    {
                        while (sdr.Read())
                        {
                            drpDwnCollection.Add(sdr["FIELD_DESCRIPTION"].ToString());
                        }
                    }*/
                }
                sdr.Close();

            }
            catch (Exception e)
            {
                //Log.Write("ValidateXMLFileNew.ExecuteQuery - ApplicationException Occured while running sql: " + e.Message, Log.LOG_CATEGORY_AVIVADEFAULT);
            }
            finally
            {
                if (sqlcmd != null)
                    sqlcmd.Dispose();

                if (sconn != null)
                {
                    sconn.Close();
                    sconn.Dispose();
                }
            }
            return drpDwnCollection;
        }
   }
   