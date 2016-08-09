using System;
using System.Collections.Generic;
using System.Text;

namespace MyHelpers
{
    public class DatabaseFactory
    {
        public static IDBConnect GetDataBase(string key)
        {
            return new DB2Shadow();
        }

        public static List<KeyValuePair<string,string>> GetDBRegions()
        {
            List<KeyValuePair<string,string>> ret = new List<KeyValuePair<string,string>>();

            ret.Add(new KeyValuePair<string,string>("CILXA1A", "CILXA1A - Live "));
            ret.Add(new KeyValuePair<string, string>("CIUXA1A", "CIUXA1A - UAT "));
            ret.Add(new KeyValuePair<string, string>("CIUXA2A", "CIUXA2A - 2 Systest "));
            ret.Add(new KeyValuePair<string, string>("CIUXA3A", "CIUXA3A - 3 Systest "));
            ret.Add(new KeyValuePair<string, string>("CIUXA4A", "CIUXA4A - 4 Systest "));
            ret.Add(new KeyValuePair<string, string>("CIDXA2A", "CIDXA2A - 2 Dev 2"));
            ret.Add(new KeyValuePair<string, string>("CIDXA3A", "CIDXA3A - 3 Dev 2"));
            ret.Add(new KeyValuePair<string, string>("CIDXA4A", "CIDXA4A - 4 Dev 2"));
            ret.Add(new KeyValuePair<string, string>("CITXA2A", "CITXA2A - 2 Dev"));
            ret.Add(new KeyValuePair<string, string>("CITXA3A", "CITXA3A - 3 Dev"));
            ret.Add(new KeyValuePair<string, string>("CITXA4A", "CITXA4A - 4 Dev"));

            return ret;
        }
    }
}
