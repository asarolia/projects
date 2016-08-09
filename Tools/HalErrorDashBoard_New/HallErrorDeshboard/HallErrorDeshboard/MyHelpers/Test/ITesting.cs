using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Data;

namespace MyHelpers
{
    /// <summary>
    /// Summary description for ITesting
    /// </summary>
    public abstract class ITesting
    {
        public string ResultText;

        public ITesting()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string ErrorText;

        public bool LoadPolicyData(DataSet Data, string Region)
        {
            try
            {
                PolicyDataHelper policyUtility = new PolicyDataHelper();
                policyUtility.write(Data, Region, PrepareDB(Region));
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("ITesting: LoadPolicyData - " + e.Message);
            }

            return true;
        }

        public bool ClearPolicyData(string PolicyNumber, string FromRegion)
        {
            try
            {
                PolicyDataHelper policyUtility = new PolicyDataHelper();
                policyUtility.delete(PolicyNumber, FromRegion, PrepareDB(FromRegion));
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("ITesting: ClearPolicyData - " + e.Message);
            }

            return true;
        }

        public DataSet ReadPolicyData(string PolicyNumber, string Region)
        {
            try
            {
                PolicyDataHelper policyUtility = new PolicyDataHelper();
                return policyUtility.read(PolicyNumber, Region, PrepareDB(Region));
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("ITesting: ReadPolicyData - " + e.Message);
            }

        }

        public IDBConnect PrepareDB(string Region)
        {
            IDBConnect dbobject = new DB2Shadow(); 
            //DatabaseFactory.GetInstance("DB2Shandow");
            dbobject.AddOptions(IDBConnect.IDBConnectOptions.Region, Region);

            //mock up
            dbobject.AddOptions(IDBConnect.IDBConnectOptions.User, "NALLAD");
            dbobject.AddOptions(IDBConnect.IDBConnectOptions.Password, "NU022011");

            //dbobject.SetConnectionString(ConfigurationManager.AppSettings.Get("db2shadowurl"), Region, "NALLAD", "NU022011");

            return dbobject;

        }

        public abstract void prepare(TestData Data);
        public abstract void execute(TestData Data);
        public abstract bool result();
        public abstract void test(TestDataPool DataPool);
    }
}