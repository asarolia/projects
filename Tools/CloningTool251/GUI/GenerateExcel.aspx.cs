using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Services;
using System.Threading;
using System.Net;
using System.IO;
using MyHelpers;

public partial class GenerateExcel : System.Web.UI.Page
{
    public PropositionDetails propositionDetails;
    protected void Page_Load(object sender, EventArgs e)
    {
        propositionDetails = MySession.GetPropositionDetails();
    }

    [WebMethod]
    public static string Generate()
    {
        PropositionDetails propositionDetails = MySession.GetPropositionDetails();
        //return CloneHelper.GenerateExcel(propositionDetails);

        HelperSupportData helper = new HelperSupportData();
        if (helper.GenerateExcel(propositionDetails.Instance, propositionDetails.GetMyParameters()))
        {
            propositionDetails.DownloadPath = Utilities.DecorateServerPath(helper.ExcelFileLocation);
            return "";
        }
        else
        {
            return helper.ErrorMessage;
        }
    }

    protected void  DownloadExcel(object sender, EventArgs e)
    {
        WebClient client = new WebClient();
        byte[] buffer = client.DownloadData(MySession.GetPropositionDetails().DownloadPath);
        if ( Path.GetExtension(MySession.GetPropositionDetails().DownloadPath) == ".zip")
            Response.ContentType = "application/x-zip-compressed";
        else
            Response.ContentType = "application/vnd.ms-excel";

        Response.AppendHeader("Content-disposition", "attachment; filename=" + GetFileName(MySession.GetPropositionDetails().SuggestedFileName(false)));
        Response.AppendHeader("content-length", buffer.Length.ToString());
        Response.BinaryWrite(buffer);
        Response.End();
    }

    private string GetFileName(string FilePath)
    {
        string ret = Path.GetFileName(FilePath);
        ret = ret.Replace(" ", "_");
        return ret;
    }
}
