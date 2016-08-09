using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;

namespace MyHelpers
{
    abstract public class IFormat
    {
        public IFormat()
        {
        }
        public IFormat(string input)
        {
        }
        //abstract public void PreFormat();
        //abstract public string Format(DataSet input);
        //abstract public string getExtension();
        //abstract public string PostFormat();

        abstract public StringBuilder GenerateExcel(DataSet dataset);

    }
}