using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for BrowseHelper
/// </summary>
public class BrowseHelper
{
    public BrowseHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Download mainframe file and copy it in local
    /// </summary>
    /// <param name="User"></param>
    /// <param name="Password"></param>
    /// <param name="File"></param>
    /// <param name="Text"></param>
    /// <returns></returns>
    static public bool DownloadMainframeFile(String User, String Password, String File, out String Text)
    {
        //String WebURL;
        //if (!FTPProxy.Download(User, Password, File, out WebURL))
        //{
        //    Text = WebURL;
        //    return false;
        //}

        //if (!FileOperations.SaveWebToLocal(WebURL, User, out Text))
        //{
        //    Text = WebURL;
        //    return false;
        //}

        //UserAccess.AddDownload(User, new DownloadItem(User, File, BrowseHelper.ConvertFilePathToURL(Text)));

        ////add item to global list
        //UserAccess.AddDownload("", new DownloadItem(User, File, BrowseHelper.ConvertFilePathToURL(Text)));
        Text = "";
        return true;

    }



    static public String ConvertFilePathToURL(String FilePath)
    {
        return Configuration.DecorateServerPath(FilePath);
    }

    static public List<DownloadItem> GetMyList(string UserId)
    {
        return UserAccess.GetMyList(UserId);
    }
}