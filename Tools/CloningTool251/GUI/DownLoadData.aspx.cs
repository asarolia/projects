using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Services;
using MyHelpers;

public partial class DownLoadData : System.Web.UI.Page
{
    private string schemecode, mastercompanynumber, productcode, effectivedate, expirationdate, sourceselection;
    public string SchemeCode
    {
        get { return schemecode; }
        set { schemecode = value; }
    }
    public string MasterCompanyNumber
    {
        get { return mastercompanynumber; }
        set { mastercompanynumber = value; }
    }    

    public string ProductCode
    {
        get { return productcode; }
        set { productcode = value; }
    }    

    public string EffectiveDate
    {
        get { return effectivedate; }
        set { effectivedate = value; }
    }
    public string ExpirationDate
    {
        get { return expirationdate; }
        set { expirationdate = value; }
    }
    public string SourceSelection
    {
        get { return sourceselection; }
        set { sourceselection = value; }
    }

    public PropositionDetails propositionDetails;

    protected void Page_Load(object sender, EventArgs e)
    {
        //this page is always called from ClientProposition.aspx
        if (PreviousPage == null)
            Response.Redirect("~/CloneProposition.aspx");

        if (PreviousPage != null & PreviousPage.IsCrossPagePostBack)
        {
            SourceSelection = PreviousPage.SourceSelection;
            //long index = CloneHelper.StartSession();
            long index = -1;
            //propositionDetails = CloneHelper.GetPropositionDetails(index, SourceSelection);
            propositionDetails = new PropositionDetails(ExceedPolicy.GetProductList(SourceSelection)[0]);

            propositionDetails.Instance = index;
            propositionDetails.nSchemeCode = PreviousPage.SchemeCode;
            propositionDetails.nProductCode = PreviousPage.ProductCode;
            propositionDetails.nMasterCompanyNumber = PreviousPage.MasterCompanyNumber;
            propositionDetails.nEffectiveDate = PreviousPage.EffectiveDate;
            propositionDetails.nExpirationDate = PreviousPage.ExpirationDate;

            MySession.SetPropositionDetails(propositionDetails);

            Region.DataSource = DatabaseFactory.GetDBRegions();
            Region.DataTextField = "Value";
            Region.DataValueField = "Key";
            Region.DataBind();
            Region.SelectedIndex = Region.Items.Count - 1;
        }
    }

    [WebMethod]
    public static string VerifyLogin(string userid, string password,string region)
    {


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
    public static ArrayList StartDownload()
    {
        ArrayList ret = new ArrayList();
        PropositionDetails propositionDetails = MySession.GetPropositionDetails();

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

}
