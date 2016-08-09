using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using MyHelpers;

public partial class Browse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    static public DownloadLocation Download(string User, string Password, string FileName)
    {
        DownloadLocation ret = new DownloadLocation();

        //if (BrowseHelper.DownloadMainframeFile(User, Password, FileName, out StatusText))
        //    ret.SetDownloadLocation(BrowseHelper.ConvertFilePathToURL(StatusText));
        //else
        //    ret.SetError(StatusText);

        MainframeFTPClient client = new MainframeFTPClient();
        client.Login(User, Password);
        string outputFile = client.Get(FileName);

        if (String.IsNullOrEmpty(outputFile))
        {
            ret.SetError(client.Message);
        }
        else
        {
            ret.SetDownloadLocation(BrowseHelper.ConvertFilePathToURL(outputFile));
        }
        return ret;
    }

    [WebMethod]
    static public List<DownloadItem> GetMyList(string User)
    {
        return BrowseHelper.GetMyList(User);
    }


}