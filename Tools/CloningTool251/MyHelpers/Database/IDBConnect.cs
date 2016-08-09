using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;

/// <summary>
/// Summary description for IDBConnect
/// </summary>
namespace MyHelpers
{
    abstract public class IDBConnect
    {
        public enum IDBConnectOptions
        {
            User,
            Password,
            Region
        }

        public IDBConnect()
        {
        }
        public string _message = "";
        public string ErrorMessage
        {
            get { return _message.Length > 0 ? _message : "<no message>"; }
            set { _message = value; }
        }

        abstract public DataSet FetchData(string TableName, string ViewName, string Region, string Query);
        abstract public bool DeleteData(string Region, DataSet dataset);
        abstract public bool InsertData(string Region, DataSet dataset);
        abstract public void SetOptions(string UserId, string Password, string Region);

        abstract public void SetServerURL(string URL);
        abstract public void AddOptions(IDBConnectOptions Key, String Value);
        abstract public bool TestConnection(string Region);
        abstract public bool Login();
    }
}
