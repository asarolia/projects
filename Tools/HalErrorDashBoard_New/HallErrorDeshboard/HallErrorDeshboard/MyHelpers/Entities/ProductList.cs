using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Summary description for ProductList
/// </summary>
namespace MyHelpers
{
    public class ProductList
    {
        public string key;
        public string description;

        public string SchemeCode;
        public string ProductCode;
        public string MasterCompanyNbr;
        public string LOB_CD;
        public string Effective_DT;
        public string Expiration_DT;


        public string Key { get { return key; } set { key = value; } }
        public string Value { get { return key + " - " + description; } set { } }

        public ProductList(string Line)
        {
            string[] parts = Line.Split(',');

            if (parts.Length != 8)
                throw new InvalidDataException("Invalide Product List row:" + Line );

            key = parts[0];
            SchemeCode = parts[1];
            ProductCode = parts[2];
            MasterCompanyNbr = parts[3];
            LOB_CD = parts[4];
            Effective_DT = parts[5];
            Expiration_DT = parts[6];
            description = parts[7];
        }

    }
}