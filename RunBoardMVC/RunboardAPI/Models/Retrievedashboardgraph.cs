using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RunboardAPI.Models
{
    public class Retrievedashboardgraph
    {
        public string Type { get; set; }
        public int Total { get; set; }
        public int Processed { get; set; }
        public int Unprocessed { get; set; }
        public int Failed { get; set; }
        public System.DateTime RecordDt { get; set; }
        public int Failed_Threshold_Red { get; set; }
        public int Unprocess_Threshold_Red { get; set; }
        public int Failed_Threshold_Amber { get; set; }
        public int Unprocess_Threshold_Amber { get; set; }
    }
}