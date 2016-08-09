using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;
using System.Runtime.Serialization;
using MyHelpers.Properties;
using System.IO;
using System.Data;

namespace MyHelpers
{
    class DownloadDataUsingConfig : IDownloadData
    {

        #region Private Variables
        
        List<string> EventList = new List<string>();
        Dictionary<string, string> ParameterList = new Dictionary<string, string>();
        string ConfigFilePath;
        MyParameters suppliedParameters;
        IDBConnect database;

        long Identifier;

        #endregion
        
        
        XPathDocument document;
        public List<TableEntity> TableList;
        private int CurrentPosition;
        private TableEntity CurrentTable;
        //private MySession CurrentSession;
        //private AutomatedTasks tasks;
    
        #region Public Functions

        public DownloadDataUsingConfig(string ConfigurationName)
        {
            try{
                document = new XPathDocument(new EmbeddedResource().GetStreamReader(String.Format(Resource.DownloadConfigQueryFile,ConfigurationName)));
            }
            catch(Exception e)
            {
                throw new InvalidOperationException(
                    String.Format("DownloadDataUsingConfig: configuration file could not be loaded for '{0}'. Failed with '{1}'",
                                ConfigurationName,e.Message)
                    );
            }
        }

        public override void SetDBConnect(IDBConnect data)
        {
            database = data;
        }

        public override void SetContext(string Context)
        {
            AddEvents(Context);
        }

        public override void SetIdentifier(long id)
        {
            Identifier = id;
        }

        public override void SetParameters(MyParameters param)
        {
            ParameterList = param.ToDictionary();
        }

        public override bool Download()
        {
        
            Prepare();
            FetchData();

            return true;
        }


        #endregion

        #region Private Functions
        private void AddEvents(string Events)
        {
            EventList = new List<string>();
            EventList.AddRange(Events.Split(','));
        }

        #endregion

        #region Private Functions

        private void AddParameter(string parameter, string value)
        {
            ParameterList[parameter] = value;
        }


        private string GetSetting(string name)
        {
            XPathNavigator navigator = document.CreateNavigator();
            XPathNodeIterator iterator = navigator.Select("Configuration/Settings/" + name);

            string ret= "";
            while (iterator.MoveNext())
            {
                ret = iterator.Current.Value;
            }
            return ret;
        }

        private string PrepareXPath()
        {
            string xpath = "";
            foreach (string item in EventList)
            {
                if (xpath.Length == 0)
                    xpath = String.Format(@"./TableView/Events/Event[@name='{0}'", item.Trim());
                else
                    xpath += String.Format(@" or @name='{0}'", item.Trim());
            }

            if (xpath.Length > 0)
                xpath += "]";


            return String.Format(@"Configuration/TableGroup/Table[{0}]", xpath);
        }

        private String Events()
        {
            string ret = "";
            foreach (String line in EventList)
            {
                if (ret.Length > 0)
                    ret += ",";
                ret += line;
            }
            return ret;
        }


        private void SaveTableList()
        {
            foreach (TableEntity item in TableList)
                item.Save();
        }

        private void Prepare()
        {
            Results = new DataSet();
            TableList = new List<TableEntity>();

            XPathNavigator navigator = document.CreateNavigator();
            XPathNodeIterator iterator = navigator.Select(PrepareXPath());
            foreach (XPathNavigator item in iterator)
            {
                TableEntity tab = new TableEntity(item,ParameterList);
                TableList.Add(tab);
            }

            if (TableList.Count == 0)
            {
                throw new InvalidDataException("Prepare SQL: Configuration Error. No table selected for '" + Events() +"' events.");
            }
        }


        private void LoadTableList(string FromFolder)
        {
            TableList = new List<TableEntity>();
            foreach (string file in Directory.GetFiles(FromFolder,"*.bin"))
            {
                TableList.Add(TableEntity.Retrieve(file));
            }
        }



        private void FetchData()
        {

            for (int i = 0; i < TableList.Count; i++)
            {
                CurrentPosition = i + 1;
                CurrentTable = TableList[i];

                //ObjectPool.Push(Identifier,new DownloadStatus(DownloadStatus.DownloadStatusStages.Processing,
                //                            (100*i)/TableList.Count,
                //                            String.Format("Downloading {0} of {1}. Table :{2}",i+1,TableList.Count,CurrentTable.Id),
                //                            ""));

                //CurrentTable.FetchAndSaveData(database, new EmbeddedResource().PrependAssemblyPath(String.Format(Resource.SupportDataDownload,Identifier.ToString(),CurrentTable.Id)));

                if (CurrentTable.FetchData(database))
                {
                    Results.Merge(CurrentTable.GetResult());
                }

                ObjectPool.Push(Identifier, new DownloadStatus(DownloadStatus.DownloadStatusStages.Processing,
                                            (100 * i) / TableList.Count,
                                            String.Format("Downloaded {0} of {1}. Table :{2}", i + 1, TableList.Count, CurrentTable.Id),
                                            CurrentTable.DebugText));

            }

            ObjectPool.Push(Identifier, new DownloadStatus(DownloadStatus.DownloadStatusStages.PostProcessing,
                                        100,
                                        "Download Complete",
                                        ""));

            //tasks = new AutomatedTasks(session, this);
            //LoadTableList(Config.DownloadDataPath(session));

            ////close the dialogue
            //CurrentStatus.PostCurrentStatus(this);
        }

        //private void FetchData(IDBConnect dbobject, MySession session)
        //{
        //    CurrentSession = session;

        //    Utilities.CleanUpDirectory(Config.DownloadDataPath(session));

        //    for (int i = 0; i < TableList.Count; i++)
        //    {
        //        CurrentPosition = i + 1;
        //        CurrentTable = TableList[i];

        //        if (i == (TableList.Count - 1))
        //            CurrentStatus.PostCurrentStatus(this, "Copying proposition data for editing..");
        //        else
        //            session.currentStatus = CurrentStatus.PostCurrentStatus(this);

        //        CurrentTable.FetchAndSaveData(dbobject, Config.DownloadDataPath(session, CurrentTable.Id));

        //        Debug.AppendLineToFile(CurrentTable.DebugText, Config.DownloadDataPath(session) + "\\QueryLog.txt");
        //        //Thread.Sleep(300);
        //    }

        //    tasks = new AutomatedTasks(session, this);
        //    //LoadTableList(tasks.CloneData());
        //    LoadTableList(Config.DownloadDataPath(session));

        //    //close the dialogue
        //    CurrentStatus.PostCurrentStatus(this);
        //}





        private int GetTotalNumber()
        {
            return TableList.Count;
        }

        private int GetCurrentPosition()
        {
            return CurrentPosition;
        }

        private TableEntity GetCurrentTable()
        {
            return CurrentTable;
        }

        //private long CurrentIndex()
        //{
        //    return CurrentSession.GetIndex();
        //}



        //private void MakeChange(string ChangeType)
        //{
        //    switch(ChangeType)
        //    {
        //        case "Scheme":
        //            tasks.ChangeScheme();
        //            SaveTableList();
        //            break;
        //        case "Product":
        //            tasks.ChangeProduct();
        //            SaveTableList();
        //            break;
        //        case "MasterCompanyNumber":
        //            tasks.ChangeMasterCompanyNumber();
        //            SaveTableList();
        //            break;
        //        case "EffectiveDate":
        //            tasks.ChangeEffectiveDate();
        //            SaveTableList();
        //            break;
        //        case "ExpirationDate":
        //            tasks.ChangeExpirationDate();
        //            SaveTableList();
        //            break;
        //        default:
        //            throw new Exception("Query Builder: Change Type not supported '" + ChangeType + "'.");
        //     }
        //}

        private DataSet GetDataSetByTableId(string Id)
        {
            foreach (TableEntity item in TableList)
            {
                if (item.Id == Id)
                    return item.GetResult();
            }
            return null;
        }

        private DataSet GetChanges()
        {
            DataSet dataset = new DataSet();

            foreach (TableEntity item in TableList)
            {
                if (item.GetChanges() != null)
                    dataset.Merge(item.GetChanges());
            }
            return dataset;
        }

        #endregion

    }
}
