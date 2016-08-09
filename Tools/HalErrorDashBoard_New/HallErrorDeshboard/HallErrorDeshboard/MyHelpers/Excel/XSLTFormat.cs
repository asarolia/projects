using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.IO;
using System.Text;

namespace MyHelpers
{
    public class XSLTFormat
    {
        string xsltFile = "";
        public XSLTFormat()
        {
        }
        public XSLTFormat(string TransformPath)
        {
            xsltFile = TransformPath;
        }

         public string Format(DataSet input)
        {
            XmlReader reader = null;
            XmlWriter output = null;
            StringBuilder outputXML = new StringBuilder();
            try
            {
                string inputXML = input.GetXmlSchema();
                XslCompiledTransform xslt = new XslCompiledTransform();
                XmlTextReader xsltinput = new XmlTextReader(xsltFile);

                output = XmlWriter.Create(outputXML);
                reader = XmlReader.Create(new MemoryStream(Encoding.ASCII.GetBytes(inputXML)));
                // reader = XmlReader.Create(inputXML);
                xslt.Load(xsltinput);
                xslt.Transform(reader, null, output);
            }
            finally
            {
                reader.Close();
                output.Close();
            }

            return outputXML.ToString();
        }

        public  string getExtension()
        {
            //return ".xls";
            return ".html";
        }

        public  void PreFormat()
        {
        }

        public  string PostFormat()
        {
            return "";
        }
    }
}