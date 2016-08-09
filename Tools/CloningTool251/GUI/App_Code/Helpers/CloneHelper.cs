//using System;
//using System.Data;
//using System.Configuration;
//using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
//using System.Collections.Generic;

///// <summary>
///// Summary description for CloneHelper
///// </summary>
//public class CloneHelper
//{
//    public CloneHelper()
//    {
//    }

//    static public IList<Category> GetPropositionList()
//    {
//        return DataProvider.GetPropositionList();
//    }

//    static public IList<Category> GetRegionsList()
//    {
//        List<Category> ret = new List<Category>();
//        ret.Add(new Category("CILXA1A", "CILXA1A - Live "));
//        ret.Add(new Category("CIUXA1A", "CIUXA1A - UAT "));
//        ret.Add(new Category("CIUXA2A", "CIUXA2A - 2 Systest "));
//        ret.Add(new Category("CIUXA3A", "CIUXA3A - 3 Systest "));
//        ret.Add(new Category("CIUXA4A", "CIUXA4A - 4 Systest "));
//        ret.Add(new Category("CITXA2A", "CITXA2A - 2 Dev"));
//        ret.Add(new Category("CITXA3A", "CITXA3A - 3 Dev"));
//        ret.Add(new Category("CITXA4A", "CITXA4A - 4 Dev"));
//        return ret;
//    }

//    static public PropositionDetails GetPropositionDetails(long instance, string Key)
//    {
//        return DataProvider.GetPropositionDetails(instance, Key);
//    }

//    static public string Login(long instance,string user,string password,string region,PropositionDetails input)
//    {
//        return DataProvider.Login(instance, user, password, region,input);
//    }

//    static public string StartDownload(string Events, PropositionDetails input)
//    {
//        LogUtility.LogUsage("Start Download", input);
//        return (DataProvider.StartDownload(input.Instance,Events,input));
//    }

//    //static public DownloadStatus DownloadProgress(PropositionDetails input)
//    //{
//    //    return DataProvider.DownloadProgress(input.Instance, input);
//    //}

//    static public string ChangeSchemeCode(PropositionDetails input)
//    {
//        return (DataProvider.ChangeSchemeCode(input.Instance, input));
//    }
//    static public string ChangeProductCode(PropositionDetails input)
//    {
//        return (DataProvider.ChangeProductCode(input.Instance, input));
//    }
//    static public string ChangeMasterCompanyNumber(PropositionDetails input)
//    {
//        return (DataProvider.ChangeMasterCompanyNumber(input.Instance, input));
//    }
//    static public string ChangeEffectiveDate(PropositionDetails input)
//    {
//        return (DataProvider.ChangeEffectiveDate(input.Instance, input));
//    }
//    static public string ChangeExpirationDate(PropositionDetails input)
//    {
//        return (DataProvider.ChangeExpirationDate(input.Instance, input));
//    }

//    static public string GenerateExcel(PropositionDetails input)
//    {
//        return DataProvider.GenerateExcel(input.Instance, input);
//    }
//    static public long StartSession()
//    {
//        return DataProvider.StartSession();
//    }

//}
