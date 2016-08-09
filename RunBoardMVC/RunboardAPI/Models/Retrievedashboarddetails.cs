using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RunboardAPI.Models
{
    public class Retrievedashboarddetails
    {
        public string Jobname { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public string STIME { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public string ETIME { get; set; }
        public string Comment { get; set; }
    }
}