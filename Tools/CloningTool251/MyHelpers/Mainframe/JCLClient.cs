using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace MyHelpers
{
    /// <summary>
    /// Summary description for JCLClient
    /// </summary>
    public class JCLClient
    {

        private string _errorMessage = "";
        private MainframeFTPClient _client;
        public string JOBName = "";

        private string _user = "", _password = "";


        public JCLClient()
        {
            _client = new MainframeFTPClient();
        }

        public void SetCredentials(string UserId, string Password)
        {
            _user = UserId;
            _password = Password;
        }

        public bool isReady()
        {
            if (_user.Length == 0 || _password.Length == 0)
            {
                Message = "User Credentials missing.";
                return false;
            }

            //login into mainframe
            if (!_client.Login(_user, _password))
            {
                Message = _client.Message;
                return false;
            }


            //set transfer to JES
            if (!_client.SiteJES())
            {
                Message = _client.Message;
                return false;
            }


            return true;
        }

        public bool isJobComplete()
        {
            if (!JobStatus(JOBName))
                return false;

            if (Message == "OUTPUT")
            {
                Message = "";
                return true;
            }

            Message = "";
            return false;
        }

        public bool JobStatus(string JobNumber)
        {
            //check if client is ready
            if (!isReady())
                return false;

            //get jobs list
            if (!_client.Dir(JobNumber))
            {
                Message = _client.Message;
                return false;
            }

            //parse output to interpret current job status
            if (_client.ExtractResult(JobNumber, "JOBID", 0).Length == 0)
            {
                Message = "Job not found.";
                return false;
            }

            Message = _client.ExtractResult(JobNumber, "STATUS", 0);
            //Message = _client.ExtractResult(JobNumber, "", 3);
            _client.Close();
            return true;
        }

        public bool Submit(string JCLURL)
        {
            //check if client is ready
            if (!isReady())
                return false;

            //create a file with the input content
            if (JCLURL.Length == 0)
                return true;

            //migraiton fixes
            //string file = Metadata.TemporaryFolder + "\\JCL" + (new Random()).Next(9999).ToString() + ".jcl";
            string file = "\\JCL" + (new Random()).Next(9999).ToString() + ".jcl";
            try
            {
                using (WebClient clt = new WebClient())
                {
                    clt.DownloadFile(JCLURL, file);
                }
                //using (StreamWriter sr = File.CreateText(file))
                //    sr.Write(JCL);
            }
            catch (Exception e)
            {
                Message = e.Message;
                return false;
            }

            //submit jcl
            if (!_client.UploadFile(file, Path.GetFileNameWithoutExtension(file)))
            {
                Message = _client.Message;
                return false;
            }

            //parse job name
            Message = _client.Message;
            JOBName = _extractJobName(Message);

            _client.Close();

            if (JOBName.Length == 0)
                return false;
            else
                return true;
        }

        public string Message
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public void Close()
        {
            try
            {
                _client.Close();
            }
            catch { }
        }

        private string _extractJobName(string message)
        {
            Regex jobRegx = new Regex(@"(JOB[0-9]+)");
            return jobRegx.Match(message).Value;
        }

        public bool Spool(string number)
        {
            //check if client is ready
            if (!isReady())
                return false;

            string filename = String.Format("{0}.{1}", JOBName, number);
            //migration fixes
            //string fullfilename = String.Format("{0}\\{1}", Metadata.TemporaryFolder, filename);
            string fullfilename = String.Format("{0}\\{1}", "", filename);

            if (!_client.DownloadFile(filename, fullfilename))
            {
                Message = _client.Message;
                return false;
            }
            Message = fullfilename;
            return true;
        }
    }
}