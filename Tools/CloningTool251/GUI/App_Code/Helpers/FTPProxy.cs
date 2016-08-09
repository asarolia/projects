//using System;
//using System.Collections.Generic;
//using System.Web;
//using FTPService;

///// <summary>
///// Summary description for FTPProxy
///// </summary>
//public class FTPProxy
//{
//    public FTPProxy()
//    {
//        //
//        // TODO: Add constructor logic here
//        //
//    }

//    /// <summary>
//    /// Connect to FTP webservice to download a file
//    /// </summary>
//    /// <param name="User">User id</param>
//    /// <param name="Password">password</param>
//    /// <param name="PDS">Mainframe pds name</param>
//    /// <param name="Text">If error, then error text / else file location </param>
//    /// <returns>true = success / false = failed</returns>
//    static public bool Download(String User, String Password, String PDS, out String Text)
//    {
//        FtpService ftpservice = new FtpService();
//        Response response;

//        //call FTP proxy web service
//        try{
//            response = ftpservice.DownloadMainframeFile(User, Password, PDS);
//        }
//        catch {
//            Text = "Unable to connect to FTP Web service.";
//            return false;
//        }

       
//        if (response.ReturnStatus.ReturnCode == 0)  // if response is success
//        {
//            Text = response.ReturnStatus.ReturnText;
//            return true;
//        }

//        Text = response.ReturnStatus.ReturnText;
//        return false;
//    }


//}