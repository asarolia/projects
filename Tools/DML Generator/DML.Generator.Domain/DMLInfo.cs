using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Office.Interop.Excel;

namespace DML.Generator.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class DMLInfo
    {

        public DMLInfo(SheetData SheetData)
        {
            this.SheetData = SheetData;
            this.GroupHostDMLName = new HashSet<KeyValuePair<string, string>>();
            this.PopulateFEDMLName();
            this.PopulateHostDMLName();
            this.Errors = new List<string>();
        }

        /// <summary>
        /// Gets the name of the host DML.
        /// </summary>
        /// <value>
        /// The name of the host DML.
        /// </value>
        public string HostDMLName { get; private set; }

        /// <summary>
        /// Gets the name of the group host DML.
        /// </summary>
        /// <value>
        /// The name of the group host DML.
        /// </value>
        public HashSet<KeyValuePair<string, string>> GroupHostDMLName { get; private set; }

        /// <summary>
        /// Gets the grouped on col.
        /// </summary>
        public string GroupedOnCol { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [grouping indicator].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [grouping indicator]; otherwise, <c>false</c>.
        /// </value>
        public bool groupingIndicator { get; private set; }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public List<string> Errors { get; set; }

        /// <summary>
        /// Gets or sets the name of the FED ml.
        /// </summary>
        /// <value>
        /// The name of the FED ml.
        /// </value>
        public string FEDMLName { get; private set; }

        /// <summary>
        /// Gets the sheet data.
        /// </summary>
        public SheetData SheetData { get; private set; }

        /// <summary>
        /// Populates the name of the FEDML.
        /// </summary>
        private void PopulateFEDMLName()
        {
            XDocument doc = XDocument.Load(System.Configuration.ConfigurationManager.AppSettings.Get("Mapping"));
            XElement element = doc.Root.Elements(this.SheetData.TableName).FirstOrDefault();

            if (element != null && element.Attribute("FeDML") != null)
            {
                this.FEDMLName = element.Attribute("FeDML").Value;
            }
        }

        /// <summary>
        /// Populates the name of the host DML.
        /// </summary>
        private void PopulateHostDMLName()
        {
            XDocument doc = XDocument.Load(System.Configuration.ConfigurationManager.AppSettings.Get("Mapping"));
            XElement element = doc.Root.Elements(this.SheetData.TableName).FirstOrDefault();

            this.groupingIndicator = element != null && element.Attribute("grping") != null ? true : false;

            if (element != null && element.Attribute("HostCB") != null)
            {
                this.HostDMLName = element.Attribute("HostCB").Value;
                
            }
            else 
            {
                if (element != null && element.Attribute("col") != null)
                {
                    this.GroupedOnCol = element.Attribute("col").Value;

                    int colNumber = this.GetColumnNumberFromContent();
                    if (colNumber != 0)
                    {
                        for (int i = this.SheetData.StartRowIndex + 1; i < this.SheetData.EndRowIndex; i++)
                        {
                            this.GroupHostDMLName.Add(new KeyValuePair<string, string>(this.SheetData.UsedRange[i, colNumber].ToString(), this.GetHostDMLNameForGroup(this.SheetData.UsedRange[i, colNumber].ToString())));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the content of the column number from.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        private int GetColumnNumberFromContent()
        {
            if (this.SheetData.ColumnRowIndex > 0)
            {
                for (int colCount = 1; colCount < this.SheetData.UsedRange.GetUpperBound(1); colCount++)
                {
                    if (string.Equals(this.SheetData.UsedRange[this.SheetData.ColumnRowIndex, colCount].ToString(), this.GroupedOnCol))
                    {
                        return colCount;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Gets the host DML name for group.
        /// </summary>
        /// <param name="scheme">The scheme.</param>
        /// <returns></returns>
        private string GetHostDMLNameForGroup(string scheme)
        {
            XDocument doc = XDocument.Load(System.Configuration.ConfigurationManager.AppSettings.Get("Mapping"));
            XElement element = doc.Root.Elements(this.SheetData.TableName).FirstOrDefault();

            if (element != null && element.Attribute("grping") != null && string.Equals(element.Attribute("grping").Value.ToUpper(), "Y"))
            {
                XElement childElement = element.Elements("DML_grp_map").FirstOrDefault(x => x.Attribute("value") != null && string.Equals(x.Attribute("value").Value, scheme));
                if (childElement != null)
                {
                    return childElement.Attribute("HostCB").Value;
                }
            }

            return string.Empty;
        }
    }
}
