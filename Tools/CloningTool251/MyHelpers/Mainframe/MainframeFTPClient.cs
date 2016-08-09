using System;
using System.Collections.Generic;
using System.Web;
using MyHelpers.Properties;

namespace MyHelpers
{
    /// <summary>
    /// Summary description for MainframeFTPClient
    /// </summary>
    public class MainframeFTPClient
    {
        private FtpClient _ftpClient;
        private string _errorMessage = "";

        public string Message
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }
        public List<string[]> MessageDoubleArray
        {
            get
            {
                char[] delimiter = { '\n' };
                char[] spacesplit = { ' ' };
                string[] array = Message.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                List<string[]> ret = new List<string[]>();
                foreach (string item in array)
                {
                    string[] cols = item.Split(spacesplit, StringSplitOptions.RemoveEmptyEntries);
                    ret.Add(cols);
                }
                return ret;
            }
        }

        public MainframeFTPClient()
        {
            _ftpClient = new FtpClient();
        }

        public bool Login(string User, string Password)
        {
            try
            {
                _ftpClient.Login(Resource.MainframeFTPServerURL, int.Parse(Resource.MainframeFTPServerPort) , User, Password);
            }
            catch (Exception e)
            {
                _errorMessage = e.Message;
                return false;
            }

            return true;
        }

        public bool SiteJES()
        {
            int response = _ftpClient.SendCommand("site filetype=JES");

            if (response == (int)FtpClient.FTPReturn.site_change_okay)
                return true;
            else
            {
                Message = _ftpClient.Message;
                return false;
            }

        }

        public bool Dir(string folder)
        {
            if (_ftpClient.SendCommand("CWD " + folder) != (int)FtpClient.FTPReturn.Change_directory)
            {
                Message = _ftpClient.Message;
                return false;
            }

            if (_ftpClient.Dir(folder))
            {
                Message = _ftpClient.FullResponse;
                return true;
            }
            else
            {
                Message = _ftpClient.Message;
                return false;
            }

        }

        public bool UploadFile(string LocalFile, string RemoteFile)
        {
            int reply = _ftpClient.UploadFile(LocalFile, RemoteFile, false);
            if (reply != (int)FtpClient.FTPReturn.file_transfer_success)
            {

                Message = _ftpClient.Message;
                return false;
            }
            Message = _ftpClient.FullResponse;
            return true;
        }

        public bool Close()
        {
            try
            {
                _ftpClient.Close();
            }
            catch (Exception e)
            {
                Message = e.Message;
                return false;
            }

            return true;
        }

        public string ExtractResult(string forJobId, string resultName, int OutsideBoundary)
        {
            List<string[]> result = MessageDoubleArray;
            int index = -1, jobidindex = 1;

            foreach (string[] item in result)
            {
                //first time find property name
                if (index < 0)
                {
                    for (index = 0; index < item.Length; index++)
                    {
                        if (item[index].Trim().Equals("JOBID"))
                            jobidindex = index;

                        if (item[index].Trim().Equals(resultName.Trim()))
                            break;
                    }
                    if (index >= item.Length)
                    {
                        index = index + OutsideBoundary - 1;
                    }
                }
                else if (index >= item.Length) { break; }
                else if (item[jobidindex].Trim() == forJobId.Trim()) { return item[index].Trim(); }

            }

            return "";
        }

        public bool DownloadFile(string fileName, string ToFile)
        {
            try
            {
                _ftpClient.DownloadFile(fileName, ToFile, true);
            }
            catch (Exception e)
            {
                Message = e.Message;
                return false;
            }
            Message = _ftpClient.Message;
            return true;
        }

        /// <summary>
        /// Get file from remote server
        /// </summary>
        /// <param name="RemoteFile"></param>
        /// <returns> returns local file path</returns>
        public string Get(string RemoteFile)
        {
            string TemporaryFile = null;
            try
            {
                TemporaryFile = new EmbeddedResource().PrependAssemblyPath(String.Format(Resource.DownloadFilepath,new Random().Next(10000).ToString()));
                _ftpClient.DownloadFile(RemoteFile, TemporaryFile, false);
            }
            catch (Exception e)
            {
                Message = e.Message;
                return null;
            }
            Message = _ftpClient.Message;
            return TemporaryFile;

        }

    }
}