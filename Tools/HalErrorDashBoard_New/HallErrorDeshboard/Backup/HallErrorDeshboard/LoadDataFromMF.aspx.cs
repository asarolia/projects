using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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
using System.Data.Sql;
using System.Data.Objects;
using System.Linq;
using System.Globalization;
using System.ComponentModel; 


public partial class LoadDataFromMF : System.Web.UI.Page
{
    #region members

    private string ConnectionString;
    private string URL = "";
    private const string RestOfURL = "&Connection=%DSN%;UID=%USER%;PWD=%PASSWORD%;";
    
    #endregion members

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        string result;
        result = ReadErrorFromDB2();
        LoadErrorsToSQL(result);        
    }
    public string ReadErrorFromDB2()
    {
        URL = "http://172.20.192.251/DB2Shadow/DB2ShadowServer.asp";
        string sqlquery = "?SQLQuery=SELECT B.HELF_ERR_RFR_NBR AS ERROR_ID,B.HELF_ERR_CMT_TXT AS ERROR_TEXT, COUNT(*) AS ERROR_COUNT, 'HAL_ERR' AS ERROR_SOURCE, DATE(A.FAIL_TS) AS REPORT_DATE FROM %Region%.HAL_ERR_LOG_DTL A, %Region%.HAL_ERR_LOG_FAIL B WHERE DATE(A.FAIL_TS) > (CURRENT DATE - 2 DAYS) AND DATE(A.FAIL_TS) < CURRENT DATE  AND B.HELF_ERR_RFR_NBR = A.ERR_RFR_NBR GROUP BY B.HELF_ERR_RFR_NBR,B.HELF_ERR_CMT_TXT,DATE(A.FAIL_TS) ORDER BY ERROR_COUNT DESC";
        //string url = serverurl + sqlquery + "&Connection=DSN=N200_DB2U_Temp;UID=biplavk;PWD=Mar2012;";
        ConnectionString = URL + sqlquery + RestOfURL;
        ConnectionString = ConnectionString.Replace("%DSN%", GetDSN(Region.Text));
        ConnectionString = ConnectionString.Replace("%USER%",username.Value);
        ConnectionString = ConnectionString.Replace("%PASSWORD%", password.Value);
        ConnectionString = ConnectionString.Replace("%Region%", Region.Text);

        WebRequest webRequest = HttpWebRequest.Create(ConnectionString);
        webRequest.Method = "GET";
        //webRequest.UseDefaultCredentials = true;
        //webRequest.PreAuthenticate = true;
        webRequest.Proxy = null;
        //webRequest.Proxy = HttpWebRequest.GetSystemWebProxy();
        //webRequest.Credentials = new NetworkCredential("via\nallad", "Deva022011");
        string result;
        WebResponse webResponse = null;

        try
        {
            webResponse = webRequest.GetResponse();
            StreamReader strResponse = new StreamReader(webResponse.GetResponseStream(), System.Text.Encoding.ASCII);
            result = strResponse.ReadToEnd();
            webResponse.Close();
            this.ClientScript.RegisterStartupScript(this.Page.GetType(), "Alert", "window.alert('Success: Dwonload from DB2 Completed');", true);
        }
        catch (WebException ex)
        {
            StreamReader strResponse = new StreamReader(ex.Response.GetResponseStream(), System.Text.Encoding.ASCII);
            result = strResponse.ReadToEnd();
            this.ClientScript.RegisterStartupScript(this.Page.GetType(), "Alert", "window.alert('Error: " + ex.Message + "');", true);
            return result;
            //throw new InvalidOperationException("web method failed " + ex.Message + result);
            
        }
        catch (Exception ex)
        {          
            throw new InvalidOperationException("web method failed " + ex.Message);           
            
        }

        return result;
    }
    public void LoadErrorsToSQL(string result)
    {

        List<string> Rows = new List<string>();
        Rows.AddRange(result.Split('\r'));
        Rows.RemoveAt(0);
        Rows.RemoveAt(Rows.Count - 1);

        string[] rows;
        DefectTrackerModel.DefectTrackerEntities defectTrackerEntities = new DefectTrackerModel.DefectTrackerEntities();
        ObjectSet<DefectTrackerModel.MasterError> test = defectTrackerEntities.CreateObjectSet<DefectTrackerModel.MasterError>();
        
        foreach (string row in Rows)
        {

            DefectTrackerModel.MasterError MasterError = new DefectTrackerModel.MasterError();
            
            rows = row.Split('\t');
            MasterError.GUID = Guid.NewGuid();
            MasterError.ErrorId = rows[0];
            MasterError.ErrorText = rows[1];
            MasterError.Count = Convert.ToInt32(rows[2]);
            MasterError.Source = rows[3];
            //MasterError.ReportDate = rows[4];  //Commented Raj

            string strDate = rows[4];

            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "dd/MM/yyyy";
            dtfi.DateSeparator = "/";
            DateTime objDate = Convert.ToDateTime(strDate, dtfi);
            MasterError.ReportDate = objDate;                        
            
            //defectTrackerEntities.AddObject(MasterError.GetType().Name, MasterError);
            var find = test.FirstOrDefault(x => ((x.ErrorText).Trim() == (MasterError.ErrorText).Trim() && (x.StatusCd >= 4 || x.StatusCd == 0)));

            if (find == null)
            {
                if (MasterError.StatusCd == null)
                {
                    MasterError.StatusCd = 0;
                }
                defectTrackerEntities.MasterErrors.AddObject(MasterError);

            }
        }
        defectTrackerEntities.SaveChanges();
    }

    private string GetDSN(string Region)
    {
        string DSN;

        switch (Region.Substring(0, 3))
        {
            case "CIT":
            case "CID":
                DSN = "DSN=N200_DB2T_Temp";
                break;
            case "CIU":
                DSN = "DSN=N200_DB2U_Temp";
                break;
            case "CIL":
                DSN = "DSN=N200_DB2P";
                break;
            default:
                throw new InvalidOperationException("Invalid region selected:" + Region);
        }
        return DSN;

    }   
}