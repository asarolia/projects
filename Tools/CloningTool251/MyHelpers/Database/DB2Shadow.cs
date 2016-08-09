using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using MyHelpers.Properties;

namespace MyHelpers
{
    public class DB2Shadow : IDBConnect
    {
        #region members

        private string ConnectionString;
        private string URL = "";
        private const string RestOfURL = "?Connection=%DSN%;UID=%USER%;PWD=%PASSWORD%;";
        private readonly Object SyncRoot = new Object();

        private Dictionary<IDBConnectOptions, string> Options;

        #endregion members

        public DB2Shadow()
        {
            Options = new Dictionary<IDBConnectOptions, string>();
            SetServerURL(Resource.ShadowUrl);
        }

        #region public function

        override public void SetServerURL(string URL)
        {
            this.URL = URL;
        }

        public override void AddOptions(IDBConnectOptions Key, string Value)
        {
            Options[Key] = Value;
        }

        public override void SetOptions(string UserId, string Password, string Region)
        {
            AddOptions(IDBConnectOptions.User, UserId);
            AddOptions(IDBConnectOptions.Password, Password);
            AddOptions(IDBConnectOptions.Region, Region);
        }

        override public bool TestConnection(string Region)
        {
            SetRegion(Region);
            PrepareConnectionString();
            string strQuery = "SELECT SCHEME_ID FROM " + Region.Trim() + ".SHM_AGENCY WITH UR FETCH FIRST ROW ONLY;";
            try
            {
                string result = runQuery(strQuery);
                if (result == null)
                    return false;
                else
                    return true;
            }
            catch (Exception e)
            {
                this.ErrorMessage = e.Message;
                return false;
            }
        }

        public override bool Login()
        {
            return TestConnection(Options[IDBConnectOptions.Region]);
        }

        override public DataSet FetchData(string TableName, string ViewName, string Region, string query)
        {
            SetRegion(Region);
            PrepareConnectionString();

            DataSet ds = new DataSet();
            try
            {

                ds.Tables.Add(FetchTableDetails(TableName, ViewName, Region));
                PopulateDataTable(ds.Tables[ViewName], runQuery(DataSetHelper.ParseQuery(query, ds.Tables[ViewName])));

                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override bool InsertData(string Region, DataSet dataset)
        {
            //List<string> queries = DataSetHelper.CreateInsertSQL(dataset, Region);
            ////Debug.AppendLineToFile(String.Join("\n", queries.ToArray()), DebugFileName());
            //SetRegion(Region);
            //PrepareConnectionString();

            //foreach (string query in queries)
            //{
            //    runQuery(query);
            //}
            return true;
        }

        public override bool DeleteData(string Region, DataSet dataset)
        {
            //List<string> queries = DataSetHelper.CreateDeleteSQL(dataset, Region);

            ////Debug.AppendLineToFile(String.Join("\n", queries.ToArray()), DebugFileName());
            //SetRegion(Region);
            //PrepareConnectionString();

            //foreach (string query in queries)
            //{
            //    runQuery(query);
            //}
            return true;
        }


        #endregion

        #region private functions

        private void SetConnectionString(string connection, string Region, string User, string Password)
        {
            URL = connection;
            Options[IDBConnectOptions.Region] = Region;
            Options[IDBConnectOptions.User] = User;
            Options[IDBConnectOptions.Password] = Password;
        }

        private DataTable FetchTableDetails(string TableName, string ViewName, string Region)
        {
            DataTable table = new DataTable();

            StringBuilder query = new StringBuilder("SELECT COLNO,NAME, COLTYPE, LENGTH ,SCALE,KEYSEQ,NULLS FROM SYSIBM.SYSCOLUMNS");
            query.Append(" WHERE TBNAME = '" + TableName + "' AND TBCREATOR = '" + Region + "'");
            query.Append(" ORDER BY COLNO WITH UR FETCH FIRST 200 ROWS ONLY;");
            string QueryResult = runQuery(query.ToString());
            try
            {
                table = CreateDataTableTemplate(TableName, ViewName, QueryResult);
            }
            catch
            {
                throw;
            }
            return table;
        }

        private DataTable CreateDataTableTemplate(string TableName, string ViewName, string result)
        {
            if (result.Split('\r').Length < 1)
                throw new InvalidOperationException("Invalid input in CreateDatasetTemplate");

            List<string> Rows = new List<string>();
            Rows.AddRange(result.Split('\r'));
            Rows.RemoveAt(0);
            Rows.RemoveAt(Rows.Count - 1);

            DataTable table = new DataTable();
            table.TableName = ViewName;
            table.ExtendedProperties.Add("TableName", TableName);
            table.ExtendedProperties.Add("ViewName", ViewName);

            List<DataColumn> primaryKey = new List<DataColumn>();
            int length, scale;
            string[] rows;

            foreach (string row in Rows)
            {
                rows = row.Split('\t');

                DataColumn dc = new DataColumn(rows[1], DataSetHelper.ConvertDB2Type(rows[2], rows[3], rows[4], out length, out scale));

                if (length != 0)
                {
                    if (scale == 0)
                    {
                        dc.ExtendedProperties.Add("HostType", String.Format("{0}({1})", rows[2].Trim(), rows[3].Trim()));
                        dc.MaxLength = length;
                    }
                    else
                        dc.ExtendedProperties.Add("HostType", String.Format("{0}({1},{2})", rows[2].Trim(), rows[3].Trim(), rows[4].Trim()));
                }
                else
                {
                    dc.ExtendedProperties.Add("HostType", rows[2].Trim());
                }

                if (rows[6] == "Y")
                    dc.AllowDBNull = true;
                else
                    dc.AllowDBNull = false;

                string key = row.Split('\t')[5];

                Int16 keyInt;
                bool Conversion = Int16.TryParse(key, out keyInt);

                if (key.Length != 0 && Conversion && keyInt > 0)
                    primaryKey.Add(dc);

                table.Columns.Add(dc);
            }
            if (primaryKey.Count > 0)
                table.PrimaryKey = primaryKey.ToArray();

            return table;
        }

        private void PopulateDataTable(DataTable Table, string result)
        {
            List<string> Rows = new List<string>();
            Rows.AddRange(result.Split('\r'));
            Rows.RemoveAt(Rows.Count - 1);

            if (Rows.Count < 2)
                return;

            Rows.RemoveAt(0);

            foreach (string row in Rows)
            {
                AddTableRow(Table, row);
            }
        }


        private void AddTableRow(DataTable Table, string RowText)
        {
            List<string> RowValue = new List<string>();
            RowValue.AddRange(RowText.Split('\t'));

            DataRow Row = Table.NewRow();

            for (int i = 0; i < Table.Columns.Count; i++)
            {
                DataSetHelper.SetColumnValue(Row, Table.Columns[i], RowValue[i]);
            }

            Table.Rows.Add(Row);
        }

        private string runQuery(string query)
        {
            Random random = new Random();

            string url = ConnectionString + "&SQLQuery=" + query.Replace(" ", "%20") + "&Random=" + random.Next().ToString();
           // string url = ConnectionString + "&SQLQuery=" + query + "&Random=" + random.Next().ToString();

            //Debug.AppendLineToFile(String.Format("Connection String: {0}", url), DebugFileName());

            WebRequest webRequest = HttpWebRequest.Create(url);
            webRequest.Method = "GET";
            //webRequest.UseDefaultCredentials = true;
            //webRequest.PreAuthenticate = true;
            webRequest.Proxy = null;
            //webRequest.Proxy = HttpWebRequest.GetSystemWebProxy();
            //webRequest.Credentials = new NetworkCredential("via\nallad", "Deva022011");
            string result;
            WebResponse webResponse = null;

            try
            {
                webResponse = webRequest.GetResponse();
                StreamReader strResponse = new StreamReader(webResponse.GetResponseStream(), System.Text.Encoding.ASCII);
                result = strResponse.ReadToEnd();
                webResponse.Close();
            }
            catch (WebException e)
            {
                StreamReader strResponse = new StreamReader(e.Response.GetResponseStream(), System.Text.Encoding.ASCII);
                result = strResponse.ReadToEnd();
                throw new InvalidOperationException("web method failed " + e.Message + result);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("web method failed " + e.Message);
            }

            //Debug.AppendLineToFile(String.Format("Response: \n{0}\n\n\n\n\n", result), DebugFileName());

            return result;
        }

        private string GetDSN(string Region)
        {
            string DSN;

            switch (Region.Substring(0, 3))
            {
                case "CIT":
                case "CID":
                    DSN = "DSN=N200_DB2T_Temp";
                    break;
                case "CIU":
                    DSN = "DSN=N200_DB2U_Temp";
                    break;
                case "CIL":
                    DSN = "DSN=N200_DB2P";
                    break;
                default:
                    throw new InvalidOperationException("Invalid region selected:" + Region);
            }
            return DSN;

        }

        private void PrepareConnectionString()
        {
            if (URL.Length == 0)
                throw new InvalidDataException("DB2Shadow:PrepareConnectionString - URL not set.");

            if (!Options.ContainsKey(IDBConnectOptions.Region))
                throw new InvalidDataException("DB2Shadow:PrepareConnectionString - Region not set.");

            if (!Options.ContainsKey(IDBConnectOptions.User))
                throw new InvalidDataException("DB2Shadow:PrepareConnectionString - User not set.");

            if (!Options.ContainsKey(IDBConnectOptions.Password))
                throw new InvalidDataException("DB2Shadow:PrepareConnectionString - Password not set.");

            ConnectionString = URL + RestOfURL;
            ConnectionString = ConnectionString.Replace("%DSN%", GetDSN(Options[IDBConnectOptions.Region]));
            ConnectionString = ConnectionString.Replace("%USER%", Options[IDBConnectOptions.User]);
            ConnectionString = ConnectionString.Replace("%PASSWORD%", Options[IDBConnectOptions.Password]);
        }

        private void SetRegion(string Region)
        {
            Options[IDBConnectOptions.Region] = Region;
        }
        #endregion private functions

    }
}
