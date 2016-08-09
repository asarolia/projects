using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using MyHelpers.Properties;

namespace MyHelpers
{
    [Serializable]
    public class ExceedPolicy
    {
        private string _user, _password, _region, _policyNumber =  null;
        private List<string> _queries;
        private DataSet _result;

        public DataTableCollection Tables
        {
            get { return _result.Tables; }
            private set { }
        }

        public ExceedPolicy()
        {
        }

        public ExceedPolicy(string UserId, string Password, string Region)
        {
            _user = UserId;
            _password = Password;
            _region = Region;
        }

        public ExceedPolicy(string UserId, string Password, string Region, string PolicyNumber):this(UserId,Password,Region)
        {
            _policyNumber = PolicyNumber;
        }

        public void AddQuery(string WellFormatedQuery)
        {
            if (_queries == null)
                _queries = new List<string>();

            _queries.Add(WellFormatedQuery);
        }

        public void Execute()
        {
            IDBConnect shadow = DatabaseFactory.GetDataBase(null);
            shadow.AddOptions(IDBConnect.IDBConnectOptions.Region, _region);
            shadow.AddOptions(IDBConnect.IDBConnectOptions.User, _user);
            shadow.AddOptions(IDBConnect.IDBConnectOptions.Password, _password);

            if (_queries.Count > 1)
                throw new InvalidOperationException("ExceedPolicy: Download - multiple query processing is not handled.");

            if (Object.ReferenceEquals(_result,null))
                _result = new DataSet();

            _result.Merge(shadow.FetchData(GetTableName(_queries[0]),GetTableName(_queries[0]), _region, _queries[0]));

            _queries.RemoveAt(0);
        }

        private string GetTableName(string query)
        {
            string[] words = Regex.Split(query, @"\W+");
            if (words[0].ToLower().Equals("select"))
            {
                return words[3].Substring(words[3].IndexOf('.') + 1);
            }

            throw new InvalidOperationException("ExceedPolicy -> GetTableName -> invalid query '" + query + "'");
        }

        public static ExceedPolicy Download(string UserId, string Password, string Region, string[] tables)
        {
            ExceedPolicy policy = new ExceedPolicy(UserId, Password, Region);
            //foreach (string query in tables)
                policy.AddQuery(tables[0]);

            policy.Execute();

            return policy;
        }

        private RelataionalDatabaseConfig GetRelationConfig()
        {
            RelataionalDatabaseConfig ret = new RelataionalDatabaseConfig();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add(Resource.LiteralRegion,_region);

            if (!String.IsNullOrEmpty(_policyNumber))
                param.Add(Resource.LiteralPolicyNumber, _policyNumber);

            ret.SetParameter(param);

            return ret;
        }

        /// <summary>
        /// Download Exceed Policy and saves to temporary file
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Password"></param>
        /// <param name="Region"></param>
        /// <param name="PolicyNumber"></param>
        /// <param name="FileName"></param>
        /// <returns>ExceedPolicy Instance</returns>
        public static ExceedPolicy DownloadPolicyAndSave(string UserId, string Password, string Region, string PolicyNumber)
        {
            ExceedPolicy pol = DownloadPolicy(UserId, Password, Region, PolicyNumber);
            pol.Save();

            return pol;
        }

        public static ExceedPolicy DownloadPolicy(string UserId, string Password, string Region, string PolicyNumber)
        {
            ExceedPolicy policy = new ExceedPolicy(UserId, Password, Region,PolicyNumber);

            RelataionalDatabaseConfig relation = policy.GetRelationConfig();
            string tableName, query;

            while (relation.ReadNext(policy._result,out tableName,out query))
            {
                policy.AddQuery(query);
                policy.Execute();
            }
            return policy;
        }

        public static ExceedPolicy Retrieve(string FileName)
        {
            return Serialization.Deserialize(FileName) as ExceedPolicy;
        }

        public static string[] GetSavedPoliciesList()
        {
            string file = new EmbeddedResource().PrependAssemblyPath(String.Format(Resource.ExceedPolicySerialize, "*", "*", "*", "*"));
            string path = Path.GetDirectoryName(file);
            file = Path.GetFileName(file);

            return Directory.GetFiles(path,file,SearchOption.TopDirectoryOnly);
        }

        public void Save()
        {
            Serialization.Serialize(this, SerializePath());
        }

        public static string FormatColumn(DataTable table, int row, int col)
        {
            return DataSetHelper.WrapColumnValue(table.Columns[col].DataType, table.Rows[row][col]);
        }

        private string SerializePath()
        {
            string ret = String.Format(Resource.ExceedPolicySerialize, DateTime.Now.ToShortDateString().Replace('/', '-') + "-" + DateTime.Now.ToLongTimeString().Replace(':','-'), _user, _region, _policyNumber);
            return new EmbeddedResource().PrependAssemblyPath(ret);
        }

        public static List<ProductList> GetProductList()
        {
            List<ProductList> ret = new List<ProductList>();

            string line = "";

            StreamReader sr = new EmbeddedResource().GetStreamReader(Resource.ProductListFile);
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                ret.Add(new ProductList(line));
            }
            sr.Close();
            return ret;
        }

        public static List<ProductList> GetProductList(string ProductKey)
        {
            List<ProductList> list = GetProductList();
            List<ProductList> ret = new List<ProductList>();

            foreach (ProductList item in list)
            {
                if (item.key == ProductKey)
                {
                    ret.Add(item);
                    break;
                }

            }

            if (ret.Count == 0) return null;
            else
                return ret;
        }
    }
}
