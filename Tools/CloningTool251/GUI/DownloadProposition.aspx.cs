using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Net;
using System.IO;
using MyHelpers;
using System.Collections;

public partial class DownloadProposition : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //PropositionListDropDown.DataSource = CloneHelper.GetPropositionList();
        PropositionListDropDown.DataSource = ExceedPolicy.GetProductList();
        PropositionListDropDown.DataTextField = "Value";
        PropositionListDropDown.DataValueField = "Key";
        PropositionListDropDown.DataBind();

        //Region.DataSource = CloneHelper.GetRegionsList();
        Region.DataSource = DatabaseFactory.GetDBRegions();
        Region.DataTextField = "Value";
        Region.DataValueField = "Key";
        Region.DataBind();
        Region.SelectedIndex = Region.Items.Count -1;
    }

    [WebMethod]
    public static PropositionDetails GetPropositionDetails(string scheme)
    {
        List<ProductList> Proposition = ExceedPolicy.GetProductList(scheme);

        if (Object.ReferenceEquals(Proposition, null))
            return null;


        PropositionDetails propositionDetails;

        //long index = CloneHelper.StartSession();
        //propositionDetails = CloneHelper.GetPropositionDetails(index, scheme);
        propositionDetails = new PropositionDetails(Proposition[0]);

        //propositionDetails.Instance = index;
        propositionDetails.nSchemeCode = "";
        propositionDetails.nProductCode = "";
        propositionDetails.nMasterCompanyNumber = "";
        propositionDetails.nEffectiveDate = "";
        propositionDetails.nExpirationDate = "";

        MySession.SetPropositionDetails(propositionDetails);

        return propositionDetails;
    }

    [WebMethod]
    public static string VerifyLogin(string userid, string password, string region)
    {
        //PropositionDetails propositionDetails = MySession.GetPropositionDetails();
        //return CloneHelper.Login(propositionDetails.Instance, userid, password, region, propositionDetails);

        PropositionDetails propositionDetails = MySession.GetPropositionDetails();
        propositionDetails.SetUserCredentials(userid, password, region);

        MyParameters param = new MyParameters();
        param.Add(MyParameterOptions.User, propositionDetails.User);
        param.Add(MyParameterOptions.Password, propositionDetails.Password);
        param.Add(MyParameterOptions.Region, propositionDetails.Region);


        HelperSupportData helper = new HelperSupportData();
        if (!helper.Login(param))
            return helper.ErrorMessage;

        return "";

    }

    [WebMethod]
    public static ArrayList StartDownload(bool ProductInd, bool SchemeInd, bool MasterCompanyNumberInd)
    {
        ArrayList ret = new ArrayList();
        PropositionDetails propositionDetails = MySession.GetPropositionDetails();
        if (SchemeInd) { propositionDetails.nSchemeCode = "XXX"; }
        if (ProductInd) { propositionDetails.nProductCode = "XXX"; }
        if (MasterCompanyNumberInd) { propositionDetails.nMasterCompanyNumber = "XX"; }

        HelperSupportData helper = new HelperSupportData();
        if (!helper.StartDownloadAsync(propositionDetails.GetMyParameters(), null))
        {
            ret.Add(-1);
            ret.Add(helper.ErrorMessage);
            return ret;
        }
        else
        {
            propositionDetails.Instance = helper.Identifier;

            ret.Add(propositionDetails.Instance);
            ret.Add("");
            return ret;
        }
    }

    //[WebMethod]
    //public static string StartDownload(bool ProductInd, bool SchemeInd, bool MasterCompanyNumberInd)
    //{
    //    PropositionDetails propositionDetails = MySession.GetPropositionDetails();

    //    if (SchemeInd) { propositionDetails.nSchemeCode = "XXX"; }
    //    if (ProductInd) { propositionDetails.nProductCode = "XXX"; }
    //    if (MasterCompanyNumberInd) { propositionDetails.nMasterCompanyNumber = "XX"; }

    //    return CloneHelper.StartDownload(propositionDetails.WhatisChanged(), propositionDetails);
    //}

    //[WebMethod]
    //public static DownloadStatus DownloadProgress()
    //{
    //    PropositionDetails propositionDetails = MySession.GetPropositionDetails();
    //    return CloneHelper.DownloadProgress(propositionDetails);

    //}


    [WebMethod]
    public static DownloadStatus DownloadProgress(long identifier)
    {
        //PropositionDetails propositionDetails = MySession.GetPropositionDetails();

        DownloadStatus status = (DownloadStatus)ObjectPool.PopKeepOne(identifier);

        if (status == null)
        {
            throw new InvalidOperationException("Download status not found on the server.");
        }

        if (status.IsLastMessage())
            ObjectPool.Pop(identifier);

        return status;
        //return CloneHelper.DownloadProgress(propositionDetails);
    }

    //[WebMethod]
    //public static string Generate()
    //{
    //    PropositionDetails propositionDetails = MySession.GetPropositionDetails();
    //    return CloneHelper.GenerateExcel(propositionDetails);
    //}

    [WebMethod]
    public static string Generate()
    {
        PropositionDetails propositionDetails = MySession.GetPropositionDetails();
        //return CloneHelper.GenerateExcel(propositionDetails);

        HelperSupportData helper = new HelperSupportData();
        if (helper.GenerateExcel(propositionDetails.Instance, propositionDetails.GetMyParameters(),false))
        {
            propositionDetails.DownloadPath = Utilities.DecorateServerPath(helper.ExcelFileLocation);
            return "";
        }
        else
        {
            return helper.ErrorMessage;
        }
    }

    protected void DownloadExcel(object sender, EventArgs e)
    {
        PropositionDetails propositionDetails = MySession.GetPropositionDetails();
        WebClient client = new WebClient();
        byte[] buffer = client.DownloadData(MySession.GetPropositionDetails().DownloadPath);
        if (Path.GetExtension(MySession.GetPropositionDetails().DownloadPath) == ".zip")
            Response.ContentType = "application/x-zip-compressed";
        else
            Response.ContentType = "application/vnd.ms-excel";

        //Response.AppendHeader("Pragma", "Public");
        Response.AppendHeader("Content-disposition", "attachment; filename=" + GetFileName(MySession.GetPropositionDetails().SuggestedFileName(false)));
        Response.AppendHeader("content-length", buffer.Length.ToString());
        Response.BinaryWrite(buffer);
        Response.End();
    }

    private string GetFileName(string FilePath)
    {
        string ret = Path.GetFileName(FilePath);
        ret = ret.Replace(" ", "_");
        return ret;
    }

}