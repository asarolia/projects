namespace DML.Generator.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using System.Configuration;
    
    /// <summary>
    /// Configuration factory class
    /// </summary>
    public static class ConfigurationFactory
    {
        /// <summary>
        /// Gets the path.
        /// </summary>
        private static string Path 
        {
            get 
            { 
                return ConfigurationManager.AppSettings.Get("Mapping"); 
            } 
        }

        /// <summary>
        /// Adds the exceed table.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="hostDMLName">Name of the host DML.</param>
        /// <param name="feDMLName">Name of the fe DML.</param>
        /// <returns></returns>
        public static string AddExceedTable(string tableName, string hostDMLName, string feDMLName)
        {
            XDocument Doc = XDocument.Load(Path);
            XElement element = Doc.Root.Elements(tableName).FirstOrDefault();

            if (element != null)
            {
                return "Table already exist.";
            }

            element = new XElement(tableName, new XAttribute("HostCB", hostDMLName), new XAttribute("FeDML", feDMLName));
            Doc.Root.Add(element);
            Doc.Save(Path);
            return "Table added successfully.";
        }

        /// <summary>
        /// Adds the exceed scheme.
        /// </summary>
        /// <param name="schemeId">The scheme id.</param>
        /// <returns></returns>
        public static string AddExceedScheme(int schemeId)
        {
            XDocument Doc = XDocument.Load(Path);
            List<XElement> element = Doc.Root.Elements().Where(x => x.Attribute("grping") != null && x.Attribute("col") != null && string.Equals(x.Attribute("grping").Value, "Y")
                && string.Equals(x.Attribute("col").Value, "SCHEME_ID")).ToList();

            List<XElement> filteredElements = new List<XElement>();
            element.ForEach(
                node=>
                {
                    List<XElement> DMLGrpMap = node.Elements("DML_grp_map")
                        .Where(y => y.Attribute("value") != null && string.Equals(y.Attribute("value").Value, schemeId.ToString())).ToList();
                    if (DMLGrpMap.Count > 0)
                    {
                        filteredElements.Add(node);
                    }
                }
                );

            if (filteredElements.Count > 0)
            {
                return "Scheme already exist.";
            }

            XElement newElement = new XElement("DML_grp_map", new XAttribute("value", schemeId), new XAttribute("HostCB", "SH000" + schemeId));
            
            element.ForEach(node =>
                { 
                    Doc.Root.Element(node.Name).Add(newElement);
                });
            
            Doc.Save(Path);
            return "Table added successfully.";
        }

        /// <summary>
        /// Gets the exceed table.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="HostDMLName">Name of the host DML.</param>
        /// <param name="FEDMLName">Name of the FEDML.</param>
        /// <returns></returns>
        public static bool GetExceedTable(string tableName, out string HostDMLName, out string FEDMLName, out bool isGroupedTable)
        {
            XDocument Doc = XDocument.Load(Path);
            XElement element = Doc.Root.Elements(tableName).FirstOrDefault();

            if (object.ReferenceEquals(element, null))
            {
                HostDMLName = FEDMLName = string.Empty;
                isGroupedTable = false;
                return !object.ReferenceEquals(element, null);
            }

            isGroupedTable = !object.ReferenceEquals(element.Attribute("grping"), null) && string.Equals(element.Attribute("grping").Value.ToUpper(), "Y");
            HostDMLName = !object.ReferenceEquals(element.Attribute("HostCB"), null) ? element.Attribute("HostCB").Value : string.Empty;
            FEDMLName = !object.ReferenceEquals(element.Attribute("FeDML"), null) ? element.Attribute("FeDML").Value : string.Empty;

            return !object.ReferenceEquals(element, null);
        }

        /// <summary>
        /// Edits the exceed table.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="hostDMLName">Name of the host DML.</param>
        /// <param name="FEDMLName">Name of the FEDML.</param>
        public static void EditExceedTable(string tableName, string hostDMLName, string FEDMLName, bool isGroupedTable)
        {
            XDocument Doc = XDocument.Load(Path);
            XElement element = Doc.Root.Elements(tableName).FirstOrDefault();
            foreach (XElement xElement in Doc.Root.Elements(tableName))
            {
                if(!object.ReferenceEquals(xElement, element))
                {
                    xElement.Remove();
                }
            }

            if (!object.ReferenceEquals(element, null))
            {
                if (!isGroupedTable && !string.IsNullOrEmpty(hostDMLName))
                {
                    if (!object.ReferenceEquals(element.Attribute("HostCB"), null))
                    {
                        element.SetAttributeValue("HostCB", hostDMLName);
                    }
                    else
                    {
                        element.Add(new XAttribute("HostCB", hostDMLName));
                    }
                }

                if (!string.IsNullOrEmpty(FEDMLName))
                {
                    if (!object.ReferenceEquals(element.Attribute("FeDML"), null))
                    {
                        element.SetAttributeValue("FeDML", FEDMLName);
                    }
                    else
                    {
                        element.Add(new XAttribute("FeDML", FEDMLName));
                    }
                }

                Doc.Save(Path);
            }
        }
    }
}
