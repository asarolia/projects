using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for DownloadItem
/// </summary>
public class DownloadItem
{
    public string user, downloadTime, title, link;
    public string editLink
    {
        get
        {
            return Configuration.DecorateEditorPath(link);
        }
        private set
        {
        }
    }

	public DownloadItem(string UserId, string MainframePDS, string LocalPath)
	{
        user = UserId;
        title = MainframePDS;
        link = LocalPath;
        downloadTime = DateTime.Now.ToString();
	}
}