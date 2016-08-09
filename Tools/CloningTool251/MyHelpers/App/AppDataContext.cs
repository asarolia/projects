using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MyHelpers
{
    public class AppDataContext
    {
        public string Template { get; set; }
        public string Output { get; set; }
        public List<object> TemplateList { get; set; }
        public bool FlagError { get; set; }
        public bool TerminatedByUser { get; set; }
        public bool FileSaved { get; set; }
        public string Message { get; set; }

        public AppDataContext()
        {
            TemplateList = new List<object>();

            if (GetDirectoryName() == null)
                return;

            foreach (string file in Directory.GetFiles(GetDirectoryName()))
            {
                TemplateList.Add(new { Key = file, Text = Path.GetFileNameWithoutExtension(file) });
            }
        }

        private string GetDirectoryName()
        {
            if (Directory.Exists("Templates"))
                return "Templates";

            if (Directory.Exists(@"..\Templates"))
                return @"..\Templates";

            if (Directory.Exists(@"..\..\Templates"))
                return @"..\..\Templates";

            return null;
        }

        public void reset()
        {
            FlagError = false;
            TerminatedByUser = false;
            FileSaved = false;
            Message = "";
        }

    }
}
