using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Data;

namespace MyHelpers
{
    /// <summary>
    /// Summary description for PolicyDataUtility
    /// </summary>
    public class PolicyDataHelper
    {
        #region properties
        public string ErrorText = "";
        private StringBuilder debugtext = new StringBuilder();
        private RelataionalDatabaseConfig queries;
        public DataSet LastResults;

        public string DebugText
        {
            get { return debugtext.ToString(); }
        }
        #endregion


        public PolicyDataHelper()
        {
            //migration fixes
            queries = new RelataionalDatabaseConfig(@"~/Default/PolicyRelationalDatabase.txt");
            //queries = new RelataionalDatabaseConfig(HttpContext.Current.Server.MapPath("~/Default/PolicyRelationalDatabase.txt"));
        }

        #region private functions

        #endregion //private

        #region public function
        /// <summary>
        /// read DB2 table for all policy tables and stores the data on the server
        /// </summary>
        /// <param name="PolicyNumber">Policy number</param>
        /// <param name="FromRegion">From Db2 region</param>
        /// <param name="dbobject">DB2 Object</param>
        /// <returns>dataset with results</returns>
        public DataSet read(string PolicyNumber, string FromRegion, IDBConnect dbobject)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add("PolicyNumber", PolicyNumber);
            map.Add("Region", FromRegion);

            queries.SetParameter(map);

            DataSet ds = new DataSet();
            string TableName, Query;
            try
            {
                while (queries.ReadNext(ds, out TableName, out Query))
                {
                    ds.Merge(dbobject.FetchData(TableName, TableName, FromRegion, Query));
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("PolicyDataUtility: read - " + e.Message);
            }

            LastResults = ds;
            return ds;
        }

        /// <summary>
        /// writes server policy information to DB2 tables
        /// </summary>
        /// <param name="PolicyNumber">Policy Number</param>
        /// <param name="ToRegion">To Db2 region</param>
        /// <param name="dbobject">DB2 Object</param>
        /// <returns>if success - true, else false</returns>
        public bool write(DataSet Data, string ToRegion, IDBConnect dbobject)
        {
            return dbobject.InsertData(ToRegion, Data);
        }

        /// <summary>
        /// removes policy from database
        /// </summary>
        /// <param name="PolicyNumber">Policy Number</param>
        /// <param name="FromRegion">From Db2 region</param>
        /// <param name="dbobject">DB2 Object</param>
        /// <returns>if success - true, else false</returns>
        public bool delete(string PolicyNumber, string FromRegion, IDBConnect dbobject)
        {
            DataSet ds = read(PolicyNumber, FromRegion, dbobject);
            return dbobject.DeleteData(FromRegion, ds);
        }
        #endregion

    }
}