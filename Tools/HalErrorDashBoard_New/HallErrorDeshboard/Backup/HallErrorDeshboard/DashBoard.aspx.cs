using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Data.Sql;
using System.Data.Objects;
using System.Linq;
using MyHelpers;
using System.Web.Services;
using System.Web.Script.Services;
using System.Text;
using System.Xml;

public partial class DashBoard : System.Web.UI.Page
{

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
        public int _status;
        public int status
        {
            get { return _status; }
            set { _status = value; }
        }

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

        public string fromdate;
        public string todate;

        protected void Page_Load(object sender, EventArgs e)
        {
            /*8System.Security.Principal.WindowsPrincipal p = System.Threading.Thread.CurrentPrincipal as System.Security.Principal.WindowsPrincipal;

            string strName = p.Identity.Name;

                
            Response.Write(WindowsIdentity.GetCurrent().Name.ToString()+ " "+ strName);        */

            if (!Page.IsPostBack)
            {
                //    BindGridView();
            }

        }
        /*protected void grdGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdGridView.EditIndex = e.NewEditIndex; // turn to edit mode        
            //((DropDownList)grdGridView.Rows[grdGridView.EditIndex].FindControl("drpErrorStatus")).SelectedValue 
            BindGridView();
        }
        protected void grdGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {        
            string id1 = ((HiddenField)grdGridView.Rows[e.RowIndex].Cells[1].FindControl("hidGuid")).Value;
            Guid id = new Guid(id1);
            string comment = ((TextBox)grdGridView.Rows[e.RowIndex].Cells[8].FindControl("txtErrorComment")).Text;
            string status = ((DropDownList)grdGridView.Rows[e.RowIndex].Cells[7].FindControl("drpErrorStatus")).SelectedValue;
            UpdateRecord(id, comment, status);
            BindGridView(); 
        }*/

        /*private void UpdateRecord(Guid id, string comment, string status)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[1].ToString();
            string sqlStatement = "UPDATE MasterError " +
                                 "SET Comment = @comment, statuscd = @status WHERE GUID = @Id";
        
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);
                cmd.Parameters.AddWithValue("@comment", comment);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.CommandType = CommandType.Text;            
                cmd.ExecuteNonQuery();
                grdGridView.EditIndex = -1;            
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Insert/Update Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                connection.Close();
            }

        }*/
        [WebMethod(EnableSession = true)]
        public static string PopulateData(scheme scheme)
        {
            string html = "";
            DashBoard db = new DashBoard();
            db.setfromtodate(scheme);
            DataTable dt = SearchData(scheme);
            if (dt != null)
            {
                html = BuildHTML(dt, false);
            }
            return html;
        }


        //[WebMethod(EnableSession = true)]
        //public ResponseTemplate PopulateTemplateView(templateScheme templateScheme)
        //{
        //    string user = Session["UserName"].ToString();
        //    XmlDocument xmlSchema = new XmlDocument();
        //    xmlSchema.Load(Server.MapPath("TableSchemas/Table.xml"));
        //    //ResponseTemplate response = CreateAjaxTemplate(templateScheme.tableName, templateScheme.Id, templateScheme.operation);
        //    return "abc";
        //}

        private static string BuildHTML(DataTable dt, bool flagForDependencyTable)
        {
            StringBuilder html = new StringBuilder();
            if (flagForDependencyTable)
            {
                html.Append("<table cellpadding='0' cellspacing='0' border='0' class='display' id='dependencytable'><thead><tr>");//change of id
            }
            else
            {
                html.Append("<table cellpadding='0' cellspacing='0' border='0' class='display' id='datatable'><thead><tr>");
            }
            for (int col = 0; col < dt.Columns.Count; col++)
            {
                html.Append("<th>" + dt.Columns[col].ColumnName + "</th>");
            }
            html.Append("</tr></thead>");
            html.Append("<tbody>");
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                html.Append("<tr>");
                for (int column = 0; column < dt.Columns.Count; column++)
                {
                    html.Append("<td>" + dt.Rows[row][column].ToString() + "</td>");
                }
                html.Append("</tr>");
            }
            html.Append("</tbody>");
            html.Append("</table>"); 
            return html.ToString();
        }


        public void setfromtodate(scheme scheme)
        {
            fromdate = scheme.fromdt;
            todate = scheme.todt;           
        }

        private static DataTable SearchData(scheme scheme)
        {
            SqlConnection sConn = null;
            string cmd1;
            sConn = new SqlConnection(ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString);
            sConn.Open();
            if (scheme.status == 10)
            {
                cmd1 = "select a.Guid, a.ReportDate, a.ErrorId, a.ErrorText, a.Source,a.Count,b.statustext as Status, a.Comment from mastererror a Inner Join	status b on a.statuscd = b.Code and ReportDate >= @fromdate and ReportDate <= @todate union select a.Guid, a.ReportDate, a.ErrorId, a.ErrorText, a.Source,a.Count,'No Status', a.Comment from mastererror a where a.statuscd is null and ReportDate >= @fromdate and ReportDate <= @todate order by a.Count desc";
                //cmd1 = "select DATEADD(dd, 0, DATEDIFF(dd, 0, a.ReportDate)) as ReportDate, a.ErrorId, a.ErrorText, a.Source,sum(a.Count),b.statustext as Status, a.Comment from mastererror a Inner Join	status b on a.statuscd = b.Code and DATEADD(dd, 0, DATEDIFF(dd, 0, a.ReportDate)) >= @fromdate and DATEADD(dd, 0, DATEDIFF(dd, 0, a.ReportDate)) <= @todate union select DATEADD(dd, 0, DATEDIFF(dd, 0, a.ReportDate)) as ReportDate, a.ErrorId, a.ErrorText, a.Source,sum(a.Count),'No Status' as Status, a.Comment from mastererror a where a.statuscd is null and DATEADD(dd, 0, DATEDIFF(dd, 0, a.ReportDate)) >= @fromdate and DATEADD(dd, 0, DATEDIFF(dd, 0, a.ReportDate)) <= @todate group by DATEADD(dd, 0, DATEDIFF(dd, 0, a.ReportDate)), a.ErrorId, a.ErrorText, a.Source,Status ,a.Comment order by a.ErrorText desc";                
            }
            else
            {
                cmd1 = "select a.Guid, a.ReportDate, a.ErrorId, a.ErrorText, a.Source,a.Count,b.statustext as Status, a.Comment from mastererror a Inner Join	status b on a.statuscd = b.Code and ReportDate >= @fromdate and ReportDate <= @todate where ReportDate >= @fromdate and ReportDate <= @todate and statuscd = @status order by a.Count desc";
                //cmd1 = "select a.Guid, DATEADD(dd, 0, DATEDIFF(dd, 0, a.ReportDate)) as ReportDate, a.ErrorId, a.ErrorText, a.Source,a.Count,b.statustext as Status, a.Comment from mastererror a Inner Join	status b on a.statuscd = b.Code and ReportDate >= @fromdate and ReportDate <= @todate where ReportDate >= @fromdate and ReportDate <= @todate and statuscd = @status group by ReportDate, a.ErrorId, a.ErrorText, a.Source order by a.Count desc";
            }
            DataTable dt = new DataTable();

            try
            {
                SqlCommand cmd = new SqlCommand(cmd1, sConn);
                cmd.CommandType = CommandType.Text;
                if (scheme.fromdt.Contains("null"))
                {
                    scheme.fromdt = string.Empty;
                }
                if (scheme.todt.Contains("null"))
                {
                    scheme.todt = string.Empty;
                }

                //cmd.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.Date, 80)).Value = scheme.fromdt;
                cmd.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.DateTime, 80)).Value = Convert.ToDateTime(scheme.fromdt);
                cmd.Parameters.Add(new SqlParameter("@todate", SqlDbType.DateTime, 100)).Value = Convert.ToDateTime(scheme.todt);
                if (scheme.status != 10)
                {
                    cmd.Parameters.Add(new SqlParameter("@status", SqlDbType.Int, 100)).Value = Convert.ToInt32(scheme.status);
                }
                cmd.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            finally
            {
                sConn.Close();
            }
            return dt;
        }

        /*private void BindGridView()
        {
            DataTable dt = new DataTable();        
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[1].ToString();
            try
            {
                connection.Open();
                string sqlStatement = "select a.Guid, a.ReportDate, a.ErrorId, a.ErrorText, a.Source,a.Count,a.Comment, a.statuscd, b.statustext from mastererror a Inner Join	status b on a.statuscd = b.Code and ReportDate >= '" + fromdate + "' and ReportDate <= '" + todate + "' union select a.Guid, a.ReportDate, a.ErrorId, a.ErrorText, a.Source,a.Count,a.Comment, a.statuscd, 'No Status' from mastererror a where a.statuscd is null and ReportDate >= '" + fromdate + "' and ReportDate <= '" + todate + "' order by a.Count desc";
                SqlCommand sqlCmd = new SqlCommand(sqlStatement, connection);                        
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);            
                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    grdGridView.DataSource = dt;
                    grdGridView.DataBind();
                }
                else
                {
                    grdGridView.DataSource = dt;
                    grdGridView.DataBind();
                    this.ClientScript.RegisterStartupScript(this.Page.GetType(), "ABC", "<script language=\"javascript\" type=\"text/javascript\" >alert( \"No Data found for these dates" + "\" );</script>", false);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Fetch Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                connection.Close();
            }
        }

        protected void grdGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdGridView.EditIndex = -1; 
            BindGridView(); 
        }
        protected void btnLoadGrid_Click(object sender, EventArgs e)
        {
            BindGridView();
        }
        protected void grdGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList drpTemp;
                //Label lblTemp;
                HiddenField hidTemp;
                drpTemp = ((DropDownList)e.Row.FindControl("drpErrorStatus"));
                hidTemp = ((HiddenField)e.Row.FindControl("hidErrorStatus"));
                //lblTemp = ((Label)e.Row.FindControl("lblErrorStatus"));
                if (drpTemp != null && hidTemp != null)
                    drpTemp.SelectedValue = hidTemp.Value;
           }
        }
        */
        //public string ReadErrorFromDB2()
        //{
        //    string serverurl = "http://172.20.192.251/DB2Shadow/DB2ShadowServer.asp";
        //    string sqlquery = "?SQLQuery=SELECT B.HELF_ERR_RFR_NBR AS ERROR_ID,B.HELF_ERR_CMT_TXT AS ERROR_TEXT, COUNT(*) AS ERROR_COUNT, 'HAL_ERR' AS ERROR_SOURCE, DATE(A.FAIL_TS) AS REPORT_DATE FROM HAL_ERR_LOG_DTL A, HAL_ERR_LOG_FAIL B WHERE DATE(A.FAIL_TS) > (CURRENT DATE - 2 DAYS) AND DATE(A.FAIL_TS) < CURRENT DATE  AND B.HELF_ERR_RFR_NBR = A.ERR_RFR_NBR GROUP BY B.HELF_ERR_RFR_NBR,B.HELF_ERR_CMT_TXT,DATE(A.FAIL_TS) ORDER BY ERROR_COUNT DESC";
        //    string url = serverurl + sqlquery + "&Connection=DSN=N200_DB2U_Temp;UID=biplavk;PWD=Mar2012;";

        //    WebRequest webRequest = HttpWebRequest.Create(url);
        //    webRequest.Method = "GET";
        //    //webRequest.UseDefaultCredentials = true;
        //    //webRequest.PreAuthenticate = true;
        //    webRequest.Proxy = null;
        //    //webRequest.Proxy = HttpWebRequest.GetSystemWebProxy();
        //    //webRequest.Credentials = new NetworkCredential("via\nallad", "Deva022011");
        //    string result;
        //    WebResponse webResponse = null;

        //    try
        //    {
        //        webResponse = webRequest.GetResponse();
        //        StreamReader strResponse = new StreamReader(webResponse.GetResponseStream(), System.Text.Encoding.ASCII);
        //        result = strResponse.ReadToEnd();
        //        webResponse.Close();
        //    }
        //    catch (WebException ex)
        //    {
        //        StreamReader strResponse = new StreamReader(ex.Response.GetResponseStream(), System.Text.Encoding.ASCII);
        //        result = strResponse.ReadToEnd();
        //        throw new InvalidOperationException("web method failed " + ex.Message + result);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new InvalidOperationException("web method failed " + ex.Message);
        //    }

        //    return result;
        //}

        //public void LoadErrorsToSQL(string result)
        //{

        //    List<string> Rows = new List<string>();
        //    Rows.AddRange(result.Split('\r'));
        //    Rows.RemoveAt(0);
        //    Rows.RemoveAt(Rows.Count - 1);

        //    string[] rows;
        //    DefectTrackerModel.DefectTrackerEntities defectTrackerEntities = new DefectTrackerModel.DefectTrackerEntities();
        //    ObjectSet<DefectTrackerModel.MasterError> test = defectTrackerEntities.CreateObjectSet<DefectTrackerModel.MasterError>();
        //    foreach (string row in Rows)
        //    {

        //        //DefectTrackerModel.MasterError MasterError = DefectTrackerModel.MasterError.CreateMasterError(Guid.NewGuid());
        //        DefectTrackerModel.MasterError MasterError = new DefectTrackerModel.MasterError();

        //        rows = row.Split('\t');
        //        MasterError.GUID = Guid.NewGuid();
        //        MasterError.ErrorId = rows[0];
        //        MasterError.ErrorText = rows[1];
        //        MasterError.Count = Convert.ToInt32(rows[2]);
        //        MasterError.Source = rows[3];
        //        MasterError.ReportDate = Convert.ToDateTime(rows[4]);

        //        //defectTrackerEntities.AddObject(MasterError.GetType().Name, MasterError);
        //        var find = test.FirstOrDefault(x => x.ErrorText == MasterError.ErrorText);
        //        if (find == null)
        //        {
        //            defectTrackerEntities.MasterErrors.AddObject(MasterError);
        //        }
        //    }
        //    defectTrackerEntities.SaveChanges();
        //}


    }
