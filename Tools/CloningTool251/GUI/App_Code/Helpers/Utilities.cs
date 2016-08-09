using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

/// <summary>
/// Summary description for Utilities
/// </summary>
public class Utilities
{
    public Utilities()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    static public string DecorateServerPath(string Path)
    {
        string appPath = HttpContext.Current.Server.MapPath("~");
        string res;

        if (Path.IndexOf(appPath) > -1)
        {
            res = string.Format("{0}", Path.Replace(appPath, "").Replace("\\", "/"));

            HttpRequest req = HttpContext.Current.Request;
            res = String.Format("http://{0}{1}{2}", req.ServerVariables["HTTP_HOST"], req.ApplicationPath, res);

            return res;
        }
        else
            return null;
    }

    static public string DecorateServerPath(string Path, string VirtualDirectory)
    {
        if (!(VirtualDirectory.StartsWith("\\") || VirtualDirectory.StartsWith("/")))
        {
            return DecorateServerPath(Path);
        }

        string appPath = HttpContext.Current.Server.MapPath(VirtualDirectory);
        string res;

        if (Path.IndexOf(appPath) > -1)
        {
            res = string.Format("{0}", Path.Replace(appPath, "").Replace("\\", "/"));
            VirtualDirectory = VirtualDirectory.Replace("\\", "/");

            HttpRequest req = HttpContext.Current.Request;
            res = String.Format("http://{0}{1}{2}", req.ServerVariables["HTTP_HOST"], VirtualDirectory, res);

            return res;
        }
        else
            return null;
    }

    static public void CreateDirectory(string directory)
    {
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    static public void CleanUpDirectory(string Path)
    {
        if (Directory.Exists(Path))
        {
            Directory.Delete(Path, true);
        }
        CreateDirectory(Path);
    }

    static public void CopyDirectory(string source, string destination)
    {
        CleanUpDirectory(destination);

        foreach (string file in Directory.GetFiles(source))
        {
            File.Copy(file, destination + Path.GetFileName(file));
        }

    }

}
