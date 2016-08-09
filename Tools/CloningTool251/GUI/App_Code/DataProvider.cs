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
//using System.Threading;
//using System.ComponentModel;

///// <summary>
///// Summary description for DataProvider
///// </summary>
//public class DataProvider
//{
//    static int counter = 0;
//    static bool debug = false;
//    static DBService.DBService service = new DBService.DBService();

//    public DataProvider()
//    {
//    }

//    static public IList<Category> GetPropositionList()
//    {
        
//        IList<Category> list = new List<Category>();
//        if (debug)
//        {
//            //hard code for time being
//            list.Add(new Category("", " Choose "));
//            list.Add(new Category("322", "322 - Mass Customisation Motor"));
//            list.Add(new Category("323", "323 - Mass Customisation Home"));
//            list.Add(new Category("324", "324 - Mass Customisation Travel"));
//        }
//        else
//        {
//            DBService.ProductList[] ret = service.GetProductList();
//            list.Add(new Category("", " Choose "));
//            foreach (DBService.ProductList p in ret)
//            {
//                list.Add(new Category(p.key,(p.key + " - "+ p.description)));
//            }
//        }

//        return list;
//    }

//    static public PropositionDetails GetPropositionDetails(long instance,string Key)
//    {
//        PropositionDetails det = new PropositionDetails();
//        //service.CookieContainer = new System.Net.CookieContainer();
//        if (debug)
//        {
//            if (Key.Length > 0)
//            {
//                det.Name = "Mass Customisation Motor";
//                det.ProductCode = "MMO";
//                det.SchemeCode = "322";
//                det.LOBCode = "APV";
//                det.MasterCompanyNumber = "02";
//            }
//            else
//            {
//                det.Name = "Error";
//                det.ProductCode = "Error";
//                det.MasterCompanyNumber = "Error";
//            }
//        }
//        else
//        {
//            DBService.ProductDetails ret = service.GetProductDetails(instance, Key);
//            det.Name = ret.Name;
//            det.ProductCode = ret.ProductCode;
//            det.SchemeCode = ret.SchemeCode;
//            det.LOBCode = ret.LOB_CD;
//            det.MasterCompanyNumber = ret.MastretCompanyNbr;
//            det.EffectiveDate = ret.Effective_DT;
//            det.ExpirationDate = ret.Expiration_DT;
//        }
//        return det;
//    }

//    static public long StartSession()
//    {
//        return service.StartSession("Default");
//    }

//    static public string Login(long instance,string user, string password,string region,PropositionDetails input)
//    {
//        DBService.Response ret;
//        if (debug)
//        {
//            Thread.Sleep(2000);
//            return "";
//        }
//        else
//        {
//            ret = service.Login(instance, user, password, region);
//            if (ret.ReturnStatus.ReturnCode != 0)
//                return ret.ReturnStatus.ReturnText;
//            else
//            {
//                input.SetUserCredentials(user, password, region);
//                return "";
//            }
//        }
//    }

//    static public string StartDownloadV2(long instance, string Events, PropositionDetails input)
//    {
//        if (debug)
//        {
//            counter = 0;
//            //return non space - in case error
//            return "";
//        }
//        else
//        {
//            DBService.Response ret; 
//            ret = service.DownloadData(instance, Events);

//            if (ret.ReturnStatus.ReturnCode == 0)
//                return "";
//            else
//                return ret.ReturnStatus.ReturnText;
//        }
//    }

//    static public string StartDownload(long instance, string Events, PropositionDetails input)
//    {
//        if (debug)
//        {
//            counter = 0;
//            //return non space - in case error
//            return "";
//        }
//        else
//        {
//            //string ret = service.DownloadData("SchemeCodeChanged,MasterCompanyNumberChanged,ProductCodeChanged");
//            //return ret;

//            BackgroundWorker background = new BackgroundWorker();
//            background.DoWork += new DoWorkEventHandler(background_DoWork);
//            background.RunWorkerAsync(input);

//            Thread.Sleep(1000);
//            return "";
//        }
//    }

//    static void background_DoWork(object sender, DoWorkEventArgs e)
//    {
//        PropositionDetails input = e.Argument as PropositionDetails;
//        service.DownloadDataAsync(input.Instance, input.WhatisChanged());
//    }

//    //static public DownloadStatus DownloadProgress(long instance,PropositionDetails input)
//    //{
//    //    DownloadStatus ret = new DownloadStatus();
//    //    if (debug)
//    //    {

//    //        ret.Maximum = 100;
//    //        counter += 3;
//    //        ret.Current = counter;
//    //        ret.Percentage = counter;
//    //        ret.Table = "SHM_PRODUCT_V";
//    //        ret.SQL = "Select * from SHM_PRODUCT_V where SCHEME_ID ='322' ";
//    //        ret.Status = "";
//    //        return ret;
//    //    }
//    //    else
//    //    {
//    //        DBService.CurrentStatus status = service.CurrentDownloadStatus(instance);

//    //        ret.ErrorMessage = status.ErrorMessage;

//    //        ret.Current= status.Current;
            
//    //        if (status.Total > 0)
//    //            ret.Percentage = (status.Current * 100) / status.Total;
//    //        else
//    //            ret.Percentage = 0;

//    //        ret.Maximum = status.Total;
//    //        ret.Table = status.CurrentTable;
//    //        ret.SQL = status.CurrentSQL;

//    //        if (status.ErrorMessage.Length != 0)
//    //            ret.isError = true;
//    //        else
//    //            ret.isError = false;

//    //        if (status.Current > 0 && status.Current == status.Total && ret.isError != true)
//    //            ret.isComplete = true;
//    //        else
//    //            ret.isError = false;

//    //        if (status.status != "OKAY")
//    //            ret.Status = status.status;
//    //        else
//    //            ret.Status = "";

//    //        ret.OtherStatus = status.OtherStatus;
//    //        return ret;
//    //    }

//    //}

//    static public string ChangeSchemeCode(long instance,PropositionDetails input)
//    {
//        DBService.Response response;
//        if (debug)
//            Thread.Sleep(2000);
//        else
//        {
//            response = service.ChangeSchemeCode(instance,input.nSchemeCode);

//            if (response.ReturnStatus.ReturnCode == 0)
//                return "";
//            else
//                return response.ReturnStatus.ReturnText;

//        }
//        //return non space - in case error
//        return "";
//    }
//    static public string ChangeProductCode(long instance,PropositionDetails input)
//    {
//        DBService.Response response;

//        if (debug)
//            Thread.Sleep(2000);
//        else
//        {
//            response = service.ChangeProductCode(instance,input.nProductCode);

//            if (response.ReturnStatus.ReturnCode == 0)
//                return "";
//            else
//                return response.ReturnStatus.ReturnText;
//        }
//        //return non space - in case error
//        return "";
//    }
//    static public string ChangeMasterCompanyNumber(long instance,PropositionDetails input)
//    {
//        if (debug)
//            Thread.Sleep(2000);
//        else
//        {
//            DBService.Response response;
//            response = service.ChangeMasterCompanyNumber(instance,input.nMasterCompanyNumber);

//            if (response.ReturnStatus.ReturnCode == 0)
//                return "";
//            else
//                return response.ReturnStatus.ReturnText;

//        }
//        //return non space - in case error
//        return "";
//    }
//    static public string ChangeEffectiveDate(long instance, PropositionDetails input)
//    {
//        if (debug)
//            Thread.Sleep(2000);
//        else
//        {
//            DBService.Response response;
//            response = service.ChangeEffectiveDate(instance,input.nEffectiveDate);

//            if (response.ReturnStatus.ReturnCode == 0)
//                return "";
//            else
//                return response.ReturnStatus.ReturnText;


//        }
//        //return non space - in case error
//        return "";
//    }
//    static public string ChangeExpirationDate(long instance, PropositionDetails input)
//    {
//        if (debug)
//            Thread.Sleep(2000);
//        else
//        {
//            DBService.Response response;
//            response = service.ChangeExpirationDate(instance, input.nExpirationDate);

//            if (response.ReturnStatus.ReturnCode == 0)
//                return "";
//            else
//                return response.ReturnStatus.ReturnText;


//        }
//        //return non space - in case error
//        return "";
//    }
//    static public string GenerateExcel(long instance, PropositionDetails input)
//    {
//        DBService.Response response;
//        if (debug)
//            Thread.Sleep(2000);
//        else
//        {
//            response = service.GenerateExcel(instance,input.SuggestedFileName(true));

//            if (response.ReturnStatus.ReturnCode == 0)
//            {
//                input.DownloadPath = response.ReturnStatus.ReturnText;
//                return "";
//            }
//            else
//                return response.ReturnStatus.ReturnText ;

//        }
//        //return non space - in case error
//        return "";
//    }

//}
