using System;
using System.Collections.Generic;
using System.Text;

namespace MyHelpers
{
    public enum MyParameterOptions
    {
        User,
        Password,
        Region,
        MasterCompanyNumber,
        ProductCode,
        SchemeCode,
        EffectiveDate,
        ExpirationDate,
        LOBCode,
        Modified_MasterCompanyNumber,
        Modified_ProductCode,
        Modified_SchemeCode,
        Modified_EffectiveDate,
        Modified_ExpirationDate,
        WhatIsChanged
    }

    public class MyParameters
    {
        Dictionary<MyParameterOptions, Object> data;

        public MyParameters()
        {
            data = new Dictionary<MyParameterOptions, Object>();
        }

        public void Add(MyParameterOptions param, Object Value)
        {
            data[param] = Value;
        }

        public string Get(MyParameterOptions param)
        {
            if (!data.ContainsKey(param))
                throw new InvalidOperationException(String.Format("Param: '{0}' not found", param.ToString()));

            return data[param].ToString();
        }

        public string TryGet(MyParameterOptions param)
        {
            if (!data.ContainsKey(param))
                return "";

            return data[param].ToString();
        }

        public Object GetObject(MyParameterOptions param)
        {
            return data[param];
        }
        public Object TryGetObject(MyParameterOptions param)
        {
            if (!data.ContainsKey(param))
                return null;

            return data[param];
        }

        public void AddRange(MyParameterOptions[] param, Object[] Values)
        {
            int i = 0;
            foreach (MyParameterOptions p in param)
            {
                Add(p, Values[i]);
                i++;
            }
        }

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            string key = "";

            foreach (MyParameterOptions p in data.Keys)
            {
                key = "";
                switch (p)
                {
                    case (MyParameterOptions.EffectiveDate):
                        key = "%EFFECTIVE_DATE%";
                        break;
                    case (MyParameterOptions.ExpirationDate):
                        key = "%EXPIRATION_DATE%";
                        break;
                    case (MyParameterOptions.LOBCode):
                        key = "%LOB_CD%";
                        break;
                    case (MyParameterOptions.MasterCompanyNumber):
                        key = "%MASTER_COMPANY_NBR%";
                        break;
                    case (MyParameterOptions.Modified_MasterCompanyNumber):
                        key = "%NEW_MASTER_COMPANY_NBR%";
                        break;
                    case (MyParameterOptions.Modified_ProductCode):
                        key = "%NEW_PRODUCT_CD%";
                        break;
                    case (MyParameterOptions.Modified_SchemeCode):
                        key = "%NEW_SCHEME_ID%";
                        break;
                    case (MyParameterOptions.Modified_EffectiveDate):
                        key = "%NEW_EFFECTIVE_DATE%";
                        break;
                    case (MyParameterOptions.Modified_ExpirationDate):
                        key = "%NEW_EXPIRATION_DATE%";
                        break;

                    case (MyParameterOptions.Password):
                        break;
                    case (MyParameterOptions.User):
                        key = "%USER%";
                        break;
                    case (MyParameterOptions.ProductCode):
                        key = "%PRODUCT_CD%";
                        break;
                    case (MyParameterOptions.Region):
                        key = "%REGION%";
                        break;
                    case (MyParameterOptions.SchemeCode):
                        key = "%SCHEME_ID%";
                        break;
                    default:
                        break;
                }

                if (key.Length > 0)
                    ret.Add(key, data[p].ToString());
            }

            return ret;
        }


    }
}
