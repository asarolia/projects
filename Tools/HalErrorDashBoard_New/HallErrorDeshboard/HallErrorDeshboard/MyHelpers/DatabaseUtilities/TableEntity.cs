using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web;
using System.Xml.XPath;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace MyHelpers
{

    /// <summary>
    /// Summary description for TableEntity
    /// </summary>
    [Serializable]
    public class TableEntity
    {
        public string TableName;
        private string id;
        private string Condition, OrderBy, Region;
        private DataSet result;
        private Dictionary<string, string> ParameterList;
        private string SourceFile;
        public string DebugText;

        public string Id
        {
            get { return id; }
            private set { Id = value; }
        }

        public TableEntity()
        {


        }

        public TableEntity(XPathNavigator table, Dictionary<string, string> parameters)
        {
            ParameterList = parameters;

            id = table.GetAttribute("name", "");
            TableName = table.SelectSingleNode("./TableView").GetAttribute("id", "");
            Condition = ReplaceParameterString(table.SelectSingleNode("./*/Query_Where").Value);
            OrderBy = table.SelectSingleNode("./*/Query_OrderBy").Value;

            Region = ParameterList["%REGION%"];
        }

        private string ReplaceParameterString(string input)
        {
            foreach (string key in ParameterList.Keys)
            {
                input = input.Replace(key, ParameterList[key]);
            }
            return input;
        }


        public string Query()
        {
            string query;
            query = String.Format("select * from {0}.{1} A", Region, TableName);

            if (Condition.Length > 0)
                query = String.Format("{0} where {1}", query, Condition);

            if (OrderBy.Length > 0)
                query = String.Format("{0} order by {1}", query, OrderBy);

            return query;
        }

        public bool FetchData(IDBConnect dbobject)
        {
            result = dbobject.FetchData(TableName, Id, Region, Query());

            DebugText = String.Format("SQL=>{0}.<br/> \t\t => Returned {1} rows.", Query(), result.Tables[0].Rows.Count);

            if (result.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        public void FetchAndSaveData(IDBConnect dbobject, string SaveToPath)
        {
            FetchData(dbobject);
            Save(SaveToPath);
        }

        private void Save(string SaveToPath)
        {

            BinaryFormatter formatter = new BinaryFormatter();
            if (!File.Exists(SaveToPath))
                File.Create(SaveToPath).Close();
            Stream stream = File.OpenWrite(SaveToPath);

            formatter.Serialize(stream, this);
            stream.Close();

            //also save as xml
            SaveToPath = String.Format(@"{0}\{1}.xml", Path.GetDirectoryName(SaveToPath), Path.GetFileNameWithoutExtension(SaveToPath));
            if (!File.Exists(SaveToPath))
                File.Create(SaveToPath).Close();
            stream = File.OpenWrite(SaveToPath);

            result.WriteXml(stream);
            stream.Close();
        }

        public void Save()
        {
            if (SourceFile == null)
                throw new Exception("This object is not loaded from a file to save to.");

            Save(SourceFile);
        }

        public static TableEntity Retrieve(string LoadFromPath)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Stream stream = File.OpenRead(LoadFromPath);

            TableEntity ret = formatter.Deserialize(stream) as TableEntity;
            ret.SetSourceFile(LoadFromPath);

            stream.Close();
            return ret;
        }

        public void SetSourceFile(string filePath)
        {
            SourceFile = filePath;
        }

        public string GetXml()
        {
            return result.GetXml();
        }

        public DataSet GetResult()
        {
            return result;
        }

        public bool hasRows()
        {
            try
            {
                if (GetResult().Tables[0].Rows.Count > 0)
                    return true;
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public DataSet GetChanges()
        {
            return result.GetChanges();
        }

    }

}
