using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RunBoardMVC.Models
{
    public class limittable
    {
        public string Ttype { get; set; }
        public int Failed_Threshold_Red { get; set; }
        public int Unprocess_Threshold_Red { get; set; }
        public int Failed_Threshold_Amber { get; set; }
        public int Unprocess_Threshold_Amber { get; set; }
    }
}