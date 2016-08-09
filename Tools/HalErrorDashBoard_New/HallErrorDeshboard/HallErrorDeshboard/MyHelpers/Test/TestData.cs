using System;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;

namespace MyHelpers
{
    /// <summary>
    /// Summary description for TestData
    /// </summary>
    public class TestData
    {
        Dictionary<string, string> Arguments = new Dictionary<string, string>();

        public TestData()
        {
        }
        /// <summary>
        /// Add test data quick &amp; dirty
        /// </summary>
        /// <param name="Information">format 'key=>value key=>value</param>
        public TestData(string Information)
        {
            string[] keys = Regex.Split(Information, @"\W+");

            string first = "";

            foreach (string key in keys)
            {
                if (first.Length == 0)
                {
                    first = key;
                }
                else
                {
                    Add(first, key);
                    first = "";
                }
            }
        }

        public void Add(string KeyValue)
        {
            string[] array = Regex.Split(KeyValue, @"=>");
            Arguments.Add(array[0], array[1]);
        }

        public void Add(string Key, string Value)
        {
            Arguments[Key.ToLower()] = Value;
        }

        public string Get(string Key)
        {
            return Arguments[Key.ToLower()];
        }
        public bool Exists(string Key)
        {
            return Arguments.ContainsKey(Key.ToLower());
        }
    }
}