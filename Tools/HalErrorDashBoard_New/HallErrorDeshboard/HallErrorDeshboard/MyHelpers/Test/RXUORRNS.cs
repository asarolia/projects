using System;
using System.Collections.Generic;
using System.Web;
using System.Data;

namespace MyHelpers
{
    /// <summary>
    /// Summary description for RXUORRNS
    /// </summary>
    public class RXUORRNS : ITesting
    {
        public RXUORRNS()
        {
        }

        public override void prepare(TestData Data)
        {
            DataSet dataset = null;

            if (Data.Exists("from"))
            {
                dataset = ReadPolicyData(Data.Get("policy"), Data.Get("from"));
            }

            if (Data.Exists("to") && dataset != null)
            {
                ClearPolicyData(Data.Get("policy"), Data.Get("to"));

                LoadPolicyData(dataset, Data.Get("to"));
            }
        }

        public override void execute(TestData Data)
        {


        }

        public override bool result()
        {
            return true;
        }

        public override void test(TestDataPool DataPool)
        {
            try
            {
                foreach (TestData td in DataPool.GetAllTestData())
                {
                    prepare(td);
                    execute(td);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            result();
        }
    }
}