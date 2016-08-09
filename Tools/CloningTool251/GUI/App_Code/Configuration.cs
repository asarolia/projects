using System;
using System.Collections.Generic;
using System.Web;
using System.IO;

/// <summary>
/// Summary description for Configuration
/// </summary>
public class Configuration
{
	public Configuration()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //Root for all 
    private static string GetRoot()
    {
        return System.Web.HttpContext.Current.Server.MapPath(".");
    }

    //Root for temporary data
    private static string GetTemporaryDataRoot()
    {
        return CreateDir(ConcatPath( GetRoot() ,"TemporaryData"));
    }

    /// <summary>
    /// Utility to Create a directory if not exists already
    /// </summary>
    /// <param name="Dir"></param>
    /// <returns></returns>
    private static string CreateDir(string Dir)
    {
        if (!Directory.Exists(Dir))
            Directory.CreateDirectory(Dir);
        return Dir;
    }

    /// <summary>
    /// Utility to Create a directory if not exists already
    /// </summary>
    /// <param name="Dir"></param>
    /// <returns></returns>
    public static string ConcatPath(string First, string Second)
    {
        if (!First.EndsWith("\\"))
            return String.Format("{0}\\{1}", First, Second);
        else
            return First + Second;
    }

    static public String GetUserDirectory(String ForUser)
    {
        return CreateDir(ConcatPath(GetTemporaryDataRoot(), ForUser));
    }

    static public String GetUserDirectory(String ForUser,String File)
    {
        return ConcatPath(GetUserDirectory(ForUser), File);
    }

    static public String DecorateServerPath(String FilePath)
    {
        string appPath = HttpContext.Current.Server.MapPath("~");
        string res;

        if (FilePath.IndexOf(appPath) > -1)
        {
            res = string.Format("{0}", FilePath.Replace(appPath, "").Replace("\\", "/"));

            HttpRequest req = HttpContext.Current.Request;
            res = String.Format("http://{0}{1}{2}", req.ServerVariables["HTTP_HOST"], req.ApplicationPath, res);

            return res;
        }
        else
            return FilePath;

    }

    static public string DirectoryUserProfile(string User)
    {
        return CreateDir(ConcatPath(GetUserDirectory(User), "Profile"));
    }

    static public string GetLocalPath(string url)
    {
        HttpRequest req = HttpContext.Current.Request;
        string appPath = String.Format("http://{0}{1}/", req.ServerVariables["HTTP_HOST"], req.ApplicationPath);
        string res;

        if (url.IndexOf(appPath) > -1)
        {
            res = string.Format(".\\{0}", url.Replace(appPath, "").Replace("/", "\\"));
            return res;
        }
        else if (url.StartsWith("."))
        {
            string appFolder = HttpContext.Current.Server.MapPath("~");
            res = appFolder + url.Substring(1);
            return res;
        }
        else
            return url;
            

    }

    static public string DecorateEditorPath(string link)
    {
        HttpRequest req = HttpContext.Current.Request;
        return String.Format("{0}?file={1}",String.Format("http://{0}{1}/Editor.aspx", req.ServerVariables["HTTP_HOST"], req.ApplicationPath),Configuration.GetLocalPath(link));
    }

}