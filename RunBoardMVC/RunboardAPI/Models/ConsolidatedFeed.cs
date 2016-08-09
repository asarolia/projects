using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RunboardAPI.Models
{
    public class ConsolidatedFeed
    {
        public  int ProcessedCount { get; set; }
        public int SuccessCount { get; set; }
        public int FailCount { get; set; }
        public int FTR { get; set; }
        public int FTA { get; set; }
    }
}