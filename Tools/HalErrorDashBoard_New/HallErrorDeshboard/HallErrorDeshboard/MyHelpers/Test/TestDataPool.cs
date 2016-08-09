using System;
using System.Collections.Generic;
using System.Web;

namespace MyHelpers
{
    /// <summary>
    /// Summary description for TestDataPool
    /// </summary>
    public class TestDataPool
    {
        List<TestData> pool = new List<TestData>();

        public TestDataPool()
        {
        }

        public void Add(TestData testdata)
        {
            pool.Add(testdata);
        }

        public List<TestData> GetAllTestData()
        {
            return pool;
        }

    }
}