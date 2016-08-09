using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for DownloadStatus
/// </summary>
public class DownloadStatusx
{
    private int maximum, current,percentage;
    private string table, sql, status;
    public string ErrorMessage;
    public int Maximum 
    { 
        get {return maximum; }
        set { maximum = value; }
    }
    public int Percentage
    {
        get { return percentage; }
        set { percentage = value; }
    }
    public int Current
    { 
        get {return current; }
        set { current = value; }
    }
    public string Table
    { 
        get {return table; }
        set { table = value; }
    }
    public string SQL
    { 
        get {return sql; }
        set { sql = value; }
    }
    public string Status
    {
        get { return status; }
        set { status = value; }
    }
    private string otherstatus;
    public string OtherStatus
    {
        get { return otherstatus; }
        set { otherstatus = value; }
    }
    public bool isComplete;
    public bool isError;

    public DownloadStatusx()
	{
        status = "";
        otherstatus = "";
		//
		// TODO: Add constructor logic here
		//
	}
}
