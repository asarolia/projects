using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading;

namespace MyHelpers
{
    /// <summary>
    /// Summary description for FtpClient
    /// </summary>
    public class FtpClient
    {
        Socket _clientSocket;
        string _reply = "", _previousCommand = "";
        int _returnCode = 0;
        Encoding ASCII = Encoding.ASCII;
        const int BLOCK_SIZE = 1000;
        string data = "";


        public FtpClient()
        {
            //
            // TODO: Add constructor logic here
            //

        }

        /// <summary>
        /// Login to Server
        /// </summary>
        /// <param name="ServerAddress"></param>
        /// <param name="ServerPort"></param>
        /// <param name="UserId"></param>
        /// <param name="Password"></param>

        public void Login(string ServerAddress, int ServerPort, string UserId, string Password)
        {
            //validate input
            if (ServerAddress.Length == 0 || ServerPort == 0 || UserId.Length == 0)
            {
                throw new ArgumentException("Login: Input is missing. ");
            }

            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipendpoint;
            try
            {
                if ((new Regex(@"^[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+")).IsMatch(ServerAddress))
                {
                    //IPAddress ipaddress = new IPAddress((new ASCIIEncoding()).GetBytes(ServerAddress.ToCharArray()));
                    IPAddress ipaddress = IPAddress.Parse(ServerAddress);
                    ipendpoint = new IPEndPoint(ipaddress, ServerPort);
                }
                else
                {
                    ipendpoint = new IPEndPoint(Dns.GetHostEntry(ServerAddress).AddressList[0], ServerPort);
                }
                //if (!Dns.GetHostEntry(ServerAddress).AddressList[0].ToString().Equals(ServerAddress))
                //    serverAddress = new IPEndPoint(Dns.GetHostEntry(ServerAddress).AddressList[1], ServerPort);
                //else
                //    serverAddress = new IPEndPoint(Dns.GetHostEntry(ServerAddress).AddressList[0], ServerPort);

            }
            catch (Exception e)
            {
                throw new IOException("Login: IP End point error." + e.Message);
            }

            //connect to server
            try
            {
                _clientSocket.Connect(ipendpoint);
                _readReply();
            }
            catch (Exception e)
            {
                throw new IOException("Login: Connection to server failed. " + e.Message);
            }

            if (_returnCode != (int)FTPReturn.Connect_Okay)
            {
                throw new IOException("Login: Invalid response from server. " + _reply.Substring(4));
            }

            //send user id
            _sendCommand("USER " + UserId);
            if (!(_returnCode == (int)FTPReturn.Password_required || _returnCode == (int)FTPReturn.Login_success))
            {
                _clientSocket.Close();
                _clientSocket = null;
                throw new InvalidOperationException("Login: 'LOGIN' failed. " + _reply.Substring(4));
            }

            //send password
            if (_returnCode == (int)FTPReturn.Password_required)
            {
                _sendCommand("Pass " + Password);

                if (!(_returnCode == (int)FTPReturn.Login_okay || _returnCode == (int)FTPReturn.Login_success))
                {
                    _clientSocket.Close();
                    _clientSocket = null;
                    throw new InvalidOperationException("Login: User id or password is invalid. " + _reply.Substring(4));
                }
            }
        }
        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns>
        /// 

        public bool Dir(string remoteDirectory)
        {
            //string[] response;
            try
            {
                SendStreamCommand("LIST ");
                //response = getFileList(remoteDirectory,true);
                //data = String.Join("\n", response);
                //SendCommand("DIR");
                return true;
            }
            catch (Exception e)
            {
                Message = e.Message;
                return false;
            }

        }

        public String[] getFileList(String remoteDirectory, bool TypeRawList)
        {
            if (remoteDirectory.Length > 0)
                _changeToDirectory(remoteDirectory, false);

            SendStreamCommand("NLST *");

            List<string> resp = new List<string>();
            string item;

            Char[] separator = { '\n' };
            string[] mess = data.Split(separator);

            for (int i = 0; i < mess.Length; i++)
            {
                item = mess[i];
                item.Replace('\n', ' ');
                item = item.Trim();

                if (item.Length > 0 && !item.StartsWith("@"))
                    resp.Add(item);
            }

            mess = new string[resp.Count];
            resp.CopyTo(mess);

            return mess;
        }

        public int SendStreamCommand(string command)
        {
            Socket cSocket = _createDataSocket();
            _sendCommand(command);

            if (_returnCode != (int)FTPReturn.Connection_Accepted && _returnCode != (int)FTPReturn.RETR_okay)
            {
                cSocket.Close();
                cSocket = null;
                throw new InvalidOperationException("SendStreamCommand: failed." + _reply.Substring(4));
            }

            int bytes;
            Byte[] buffer = new Byte[BLOCK_SIZE];

            data = "";

            //loop until data is started getting available
            cSocket.ReceiveTimeout = 0;
            while (true)
            {
                if (cSocket.Available > 0) break;
            }

            // read data
            while (true)
            {
                try
                {
                    bytes = cSocket.Receive(buffer, buffer.Length, SocketFlags.None);
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.WouldBlock ||
                          ex.SocketErrorCode == SocketError.IOPending ||
                          ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                    {
                        // socket buffer is probably empty, wait and try again
                        Thread.Sleep(30);
                        continue;
                    }
                    else
                        throw ex;  // any serious error occurr
                }

                if (bytes > 0)
                    data += ASCII.GetString(buffer, 0, bytes);
                else
                    break;

            }

            cSocket.Close();
            string temp = data;
            _readReply();

            data = temp;
            return _returnCode;
        }



        /// <summary>
        /// DownloadFile
        /// </summary>
        /// <param name="RemoteFileName"></param>
        /// <param name="LocalFileName"></param>
        public void DownloadFile(string RemoteFileName, string LocalFileName, bool BypassRemoteFileValidation)
        {

            string[] remoteFileInfo, localFileInfo;
            try
            {
                if (!BypassRemoteFileValidation)
                    remoteFileInfo = _parseFileName(RemoteFileName);
                else
                    remoteFileInfo = new string[2] { "", RemoteFileName };

                localFileInfo = _parseFileName(LocalFileName);
            }
            catch (InvalidOperationException)
            {
                throw;
            }

            if (!Directory.Exists(localFileInfo[0]))
                Directory.CreateDirectory(localFileInfo[0]);

            if (!BypassRemoteFileValidation)
                _changeToFileDirectory(RemoteFileName, false);

            Socket dataSocket = _createDataSocket();

            FileStream output = new FileStream(LocalFileName, FileMode.OpenOrCreate);

            _sendCommand("RETR " + remoteFileInfo[1]);

            if (_returnCode != (int)FTPReturn.Connection_Accepted && _returnCode != (int)FTPReturn.RETR_okay)
            {
                throw new InvalidOperationException("DownloadFile failed. " + "'" + RemoteFileName + "' " + _reply.Substring(4));
            }

            int bytes;
            Byte[] buffer = new Byte[BLOCK_SIZE];

            while (true)
            {
                bytes = dataSocket.Receive(buffer, buffer.Length, 0);
                output.Write(buffer, 0, bytes);

                if (bytes <= 0)
                {
                    break;
                }
            }
            dataSocket.Close();
            output.Close();
            _readReply();
        }

        public int UploadFile(string LocalFileName, string RemoteFileName, bool ChangeRemoteDirectory)
        {
            try
            {
                _uploadFile(LocalFileName, RemoteFileName, ChangeRemoteDirectory);
                return _returnCode;

            }
            catch (Exception e)
            {
                Message = e.Message;
                return 0;
            }
        }

        private void _uploadFile(string LocalFileName, string RemoteFileName, bool ChangeRemoteDirectory)
        {

            string[] remoteFileInfo, localFileInfo;

            localFileInfo = _parseFileName(LocalFileName);

            if (ChangeRemoteDirectory)
            {
                remoteFileInfo = _parseFileName(RemoteFileName);
                _changeToFileDirectory(RemoteFileName, true);
            }
            else
            {
                string[] temp = { "", RemoteFileName };
                remoteFileInfo = temp;
            }

            Socket dataSocket = _createDataSocket();
            _sendCommand("STOR " + remoteFileInfo[1]);

            if (!(_returnCode == 125 || _returnCode == 150))
            {
                throw new InvalidOperationException("UploadFile '" + RemoteFileName + "' failed. " + _reply.Substring(4));
            }


            // open input stream to read source file

            FileStream input;
            try
            {
                input = new FileStream(LocalFileName, FileMode.Open);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("UploadFile: unable to open local file '" + LocalFileName + "'. " + e.Message);
            }

            int bytes;
            Byte[] buffer = new Byte[BLOCK_SIZE];

            while ((bytes = input.Read(buffer, 0, buffer.Length)) > 0)
                dataSocket.Send(buffer, bytes, 0);

            input.Close();

            if (dataSocket.Connected)
            {
                dataSocket.Close();
            }

            _readReply();
            if (!(_returnCode == 226 || _returnCode == 250))
            {
                throw new IOException("UploadFile: " + _reply.Substring(4));
            }
        }

        public void Close()
        {
            if (_clientSocket != null)
            {
                _sendCommand("QUIT");
            }

            _clientSocket = null;
        }

        // private functions
        private Socket _createDataSocket()
        {

            _sendCommand("PASV");

            if (_returnCode != (int)FTPReturn.Passive_Mode)
            {
                throw new IOException("_createDataSocket: PASV failed. " + _reply.Substring(4));
            }

            int index1 = _reply.IndexOf('(');
            int index2 = _reply.IndexOf(')');
            string ipData = _reply.Substring(index1 + 1, index2 - index1 - 1);
            int[] parts = new int[6];

            int len = ipData.Length;
            int partCount = 0;
            string buf = "";

            for (int i = 0; i < len && partCount <= 6; i++)
            {

                char ch = Char.Parse(ipData.Substring(i, 1));
                if (Char.IsDigit(ch))
                    buf += ch;
                else if (ch != ',')
                {
                    throw new IOException("_createDataSocket: Malformed PASV reply. " + _reply);
                }

                if (ch == ',' || i + 1 == len)
                {

                    try
                    {
                        parts[partCount++] = Int32.Parse(buf);
                        buf = "";
                    }
                    catch (Exception)
                    {
                        throw new IOException("_createDataSocket: Malformed PASV reply. " + _reply);
                    }
                }
            }

            string ipAddress = parts[0] + "." + parts[1] + "." +
              parts[2] + "." + parts[3];

            int port = (parts[4] << 8) + parts[5];

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep;

            if (!Dns.GetHostEntry(ipAddress).AddressList[0].ToString().Equals(ipAddress))
                ep = new IPEndPoint(Dns.GetHostEntry(ipAddress).AddressList[1], port);
            else
                ep = new IPEndPoint(Dns.GetHostEntry(ipAddress).AddressList[0], port);

            try
            {
                s.Connect(ep);
            }
            catch (Exception)
            {
                throw new IOException("_createDataSocket: Can't connect to remote server." + ep.ToString());
            }

            return s;
        }

        private void _changeToFileDirectory(string RemoteFileName, bool createIfNeeded)
        {
            string[] inputInfo;
            try
            {
                inputInfo = _parseFileName(RemoteFileName);
                _changeToDirectory(inputInfo[0], createIfNeeded);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void _changeToDirectory(string RemoteDirectory, bool createIfNeeded)
        {
            string[] inputInfo;
            try
            {
                _moveToRemoteRootDirectory();
                _sendCommand("CWD " + RemoteDirectory);

                if (_returnCode == (int)FTPReturn.Change_directory_not_found && createIfNeeded)
                {
                    _sendCommand("MKD " + RemoteDirectory);
                    if (_returnCode != (int)FTPReturn.Change_directory)
                    {
                        throw new InvalidOperationException("_changeToDirectory: could not create '" + RemoteDirectory + "' directory. " + _reply.Substring(4));
                    }
                    _sendCommand("CWD " + RemoteDirectory);
                }

                if (_returnCode != (int)FTPReturn.Change_directory)
                {
                    throw new InvalidOperationException("_changeToDirectory: could not open '" + RemoteDirectory + "' directory. " + _reply.Substring(4));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void _moveToRemoteRootDirectory()
        {

            _sendCommand("CWD ");

            if (_returnCode != (int)FTPReturn.Change_directory)
            {
                throw new IOException("_moveToRemoteRootDirectory: CWD command failed." + _reply.Substring(4));
            }

            string currentDirectory;
            currentDirectory = _reply.Substring(4);
            currentDirectory = currentDirectory.Substring(currentDirectory.IndexOf("\"") + 1);
            currentDirectory = currentDirectory.Substring(0, currentDirectory.IndexOf("\""));

            //HttpContext.Current.Trace.Write("_moveToRemoteRootDirectory: current Directory =" + currentDirectory);

            int i = 0;
            //Reach to base directory
            while (true)
            {
                if (currentDirectory.Equals(".") || currentDirectory.Equals("/") || currentDirectory.Equals(""))
                    break;

                _sendCommand("CWD ..");
                if (_returnCode != (int)FTPReturn.Change_directory)
                {
                    throw new IOException("_moveToRemoteRootDirectory: CWD .. command failed. " + _reply.Substring(4));
                }
                currentDirectory = _reply.Substring(5);
                currentDirectory = currentDirectory.Substring(0, currentDirectory.IndexOf("\""));

                //HttpContext.Current.Trace.Write("_moveToRemoteRootDirectory: current Directory =" + currentDirectory);

                if (i++ > 100)
                {
                    throw new IOException("_moveToRemoteRootDirectory: Possible looping." + currentDirectory);
                }
            }
        }

        static public string[] _parseFileName(string FileName)
        {
            Regex MFPDS = new Regex(@"^([a-z#0-9]+\.)+([a-z#0-9]+)\([a-z0-9]+\)\z", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex MFSerial = new Regex(@"^([a-z#0-9]+\.)+([a-z#0-9]+)+(\.[a-z0-9]+)+\z", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Regex MSFolder = new Regex(@"^([\/]?[a-z0-9]+)+([\/][a-z0-9]+([.][a-z0-9]+)*)\z", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string[] result = new string[2];
            result[0] = string.Empty;
            result[1] = string.Empty;

            if (MFPDS.IsMatch(FileName))
            {
                if (FileName.IndexOf('(') > 0)
                {
                    result[0] = FileName.Substring(0, FileName.IndexOf('('));
                    result[1] = FileName.Substring(FileName.IndexOf('(') + 1, FileName.IndexOf(')') - FileName.IndexOf('(') - 1);
                }
            }
            else if (MFSerial.IsMatch(FileName))
            {
                if (FileName.LastIndexOf('.') > 0)
                {
                    result[0] = FileName.Substring(0, FileName.LastIndexOf('.'));
                    result[1] = FileName.Substring(FileName.LastIndexOf('.') + 1, FileName.Length - FileName.LastIndexOf('.') - 1);
                }
            }
            //        else if(MSFolder.IsMatch(FileName)){
            else
            {
                if (FileName.LastIndexOf('\\') > 0)
                {
                    result[0] = FileName.Substring(0, FileName.LastIndexOf('\\'));
                    result[1] = FileName.Substring(FileName.LastIndexOf('\\') + 1, FileName.Length - FileName.LastIndexOf('\\') - 1);
                }
            }

            if (result[0].Length < 1)
                throw new InvalidOperationException("Invalid file name '" + FileName + "'.  ");

            return result;
        }

        private void _sendCommand(String command)
        {
            _previousCommand = command;

            Byte[] senddata = ASCII.GetBytes((command + "\r\n").ToCharArray());
            _clientSocket.Send(senddata);
            _readReply();
        }

        private void _readReply()
        {

            try
            {
                data = "";
                _reply = _readLine();
            }
            catch (Exception e)
            {
                throw new Exception("\nreadReply: readLine failed after '" + _previousCommand + "' command." + e.Message);
            }

            try
            {
                _returnCode = int.Parse(_reply.Substring(0, 3));
            }
            catch (Exception e)
            {
                throw new Exception("\nreadReply: Parsing failed. " + e.Message);
            }
        }

        private string _readLine_not_working()
        {
            int bytes;
            Byte[] _buffer = new Byte[BLOCK_SIZE];

            _clientSocket.SendTimeout = 0;
            _clientSocket.ReceiveTimeout = 0;

            //wait until data is availabe
            while (true)
            {
                if (_clientSocket.Available > 0) break;
            }

            while (true)
            {
                //bytes = _clientSocket.Receive(_buffer, _buffer.Length, 0);
                try
                {
                    bytes = _clientSocket.Receive(_buffer, _buffer.Length, SocketFlags.None);
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.WouldBlock ||
                          ex.SocketErrorCode == SocketError.IOPending ||
                          ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                    {
                        // socket buffer is probably empty, wait and try again
                        //Thread.Sleep(30);
                        //wait until data is availabe
                        while (true)
                        {
                            if (_clientSocket.Available > 0) break;
                        }
                        continue;
                    }
                    else
                        throw ex;  // any serious error occurr
                }

                if (bytes > 0)
                    data += ASCII.GetString(_buffer, 0, bytes);
                else
                    break;
                //if (bytes < _buffer.Length && isDataReceiveComplete(data))
                //    break;
            }

            char[] separator = { '\n' };
            String[] broken_message = data.Split(separator);

            if (broken_message[broken_message.Length - 1].Trim().Length == 0)
                _reply = broken_message[broken_message.Length - 2].Trim();
            else
                _reply = broken_message[broken_message.Length - 1].Trim();
            return _reply;
        }

        private string _readLine()
        {
            int bytes;
            Byte[] _buffer = new Byte[BLOCK_SIZE];

            while (true)
            {
                bytes = _clientSocket.Receive(_buffer, _buffer.Length, 0);
                data += ASCII.GetString(_buffer, 0, bytes);
                if (bytes < _buffer.Length && isDataReceiveComplete(data))
                    break;
            }

            char[] separator = { '\n' };
            String[] broken_message = data.Split(separator);

            if (broken_message[broken_message.Length - 1].Trim().Length == 0)
                _reply = broken_message[broken_message.Length - 2].Trim();
            else
                _reply = broken_message[broken_message.Length - 1].Trim();
            return _reply;
        }

        private bool isDataReceiveComplete(string data)
        {
            char[] separator = { '\n' };
            String[] broken_message = data.Split(separator);
            string lastLine;

            if (broken_message[broken_message.Length - 1].Trim().Length == 0)
                lastLine = broken_message[broken_message.Length - 2].Trim();
            else
                lastLine = broken_message[broken_message.Length - 1].Trim();

            if (lastLine.Substring(3, 1) == " ")
                return true;
            else
                return false;
        }


        private string _readLineV1()
        {
            int bytes;
            Byte[] _buffer = new Byte[BLOCK_SIZE];

            while (true)
            {
                bytes = _clientSocket.Receive(_buffer, _buffer.Length, 0);
                data += ASCII.GetString(_buffer, 0, bytes);
                if (bytes < _buffer.Length)
                    break;
            }

            char[] separator = { '\n' };
            String[] broken_message = data.Split(separator);

            if (broken_message.Length > 2)
                _reply = broken_message[broken_message.Length - 2];
            else
                _reply = broken_message[0];

            if (!_reply.Substring(3, 1).Equals(" "))
                return _readLine();

            return _reply;
        }

        public long _getFileSize(string fileName)
        {

            _sendCommand("SIZE " + fileName);
            long size = 0;

            if (_returnCode == 213)
            {
                size = Int64.Parse(_reply.Substring(4));
            }
            else
            {
                throw new IOException("getFileSize: failed while retrieving size for " + fileName + ". " + _reply.Substring(4));
            }

            return size;

        }


        //return code enumarator
        public enum FTPReturn : int
        {
            RETR_okay = 125,
            Connection_Accepted = 150,
            mode_change = 200,
            Login_okay = 202,
            Connect_Okay = 220,
            Tranfer_okay = 226,
            Passive_Mode = 227,
            Login_success = 230,
            Change_directory = 250,
            Current_directory = 257,
            Password_required = 331,
            Change_directory_not_found = 550,
            site_change_okay = 200,
            file_transfer_success = 250,
            directory_listing_success = 250
        }

        public static FTPReturn ReturnCodes;

        public int SendCommand(string command)
        {
            try
            {
                _sendCommand(command);
                return _returnCode;
            }
            catch (Exception e)
            {
                _reply = e.Message;
                return 0;
            }
        }

        public string Message
        {
            get
            {
                return _reply;
            }
            set
            {
                _reply = value;
            }
        }

        public string FullResponse
        {
            get { return data; }
        }

    }
}