using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace MyHelpers
{
    class DataSetToExcel
    {
        public string OutputFile = "";
        public string ErrorMessage = "";

        public DataSetToExcel()
        {
        }

        public bool CreateExcel(DataSet dataset, string TargetLocation)
        {
            try
            {
                IFormat formatter = new ExcelFormat();
                StringBuilder output = formatter.GenerateExcel(dataset);

                File.WriteAllText(TargetLocation, output.ToString());
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return false;
            }
        }
    }
}
