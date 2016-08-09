using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.IO;

/// <summary>
/// Summary description for FileOperations
/// </summary>
public class FileOperations
{
	public FileOperations()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// Save a web page to local file
    /// </summary>
    /// <param name="WebURL"></param>
    /// <param name="User"></param>
    /// <param name="SaveToPath"></param>
    /// <returns></returns>
    public static bool SaveWebToLocal(String WebURL,String User,out String SaveToPath)
    {
        WebClient client = new WebClient();
        SaveToPath = Configuration.GetUserDirectory(User, StripPath(WebURL));

        try
        {
            client.DownloadFile(WebURL, SaveToPath);
        }
        catch(Exception e)
        {
            SaveToPath = "SaveWebToLocal failed with '" + e.Message +"'";
        }

        return true;
    }

    static public String StripPath(String FullFilePath)
    {
        return Path.GetFileName(FullFilePath);
    }

}