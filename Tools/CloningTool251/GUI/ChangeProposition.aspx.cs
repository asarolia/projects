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
using System.Threading;
using System.Web.Services;
using MyHelpers;

public partial class ChangeProposition : System.Web.UI.Page
{
    public string SchemeCode;
    public string MasterCompanyNumber;
    public string ProductCode;
    public string EffectiveDate;
    public string ExpirationDate;
    public string SourceSelection;

    public PropositionDetails propositionDetails;


    protected void Page_Load(object sender, EventArgs e)
    {
        //this page is always called from ClientProposition.aspx
        //if (PreviousPage == null)
        //    Response.Redirect("~/CloneProposition.aspx");

        //if (PreviousPage != null & PreviousPage.IsCrossPagePostBack)
        //{
        //    SchemeCode = PreviousPage.SchemeCode;
        //    MasterCompanyNumber = PreviousPage.MasterCompanyNumber;
        //    ProductCode = PreviousPage.ProductCode;
        //    EffectiveDate = PreviousPage.EffectiveDate;
        //    ExpirationDate = PreviousPage.ExpirationDate;
        //    SourceSelection = PreviousPage.SourceSelection;

        propositionDetails = MySession.GetPropositionDetails();
        //}
    }

    [WebMethod(EnableSession=true)]
    public static string ChangeSchemeCode()
    {
        PropositionDetails propositionDetails = MySession.GetPropositionDetails();
        //return CloneHelper.ChangeSchemeCode(propositionDetails);
        HelperSupportData helper = new HelperSupportData();
        if (helper.ChangeScheme(propositionDetails.Instance, propositionDetails.GetMyParameters()))
            return "";
        else
            return helper.ErrorMessage;
    }

    [WebMethod]
    public static string ChangeProductCode()
    {
        PropositionDetails propositionDetails = MySession.GetPropositionDetails();
        //return CloneHelper.ChangeProductCode(propositionDetails);
        HelperSupportData helper = new HelperSupportData();
        if (helper.ChangeProduct(propositionDetails.Instance, propositionDetails.GetMyParameters()))
            return "";
        else
            return helper.ErrorMessage;
    }

    [WebMethod]
    public static string ChangeMasterCompanyNumber()
    {
        PropositionDetails propositionDetails = MySession.GetPropositionDetails();
        //return CloneHelper.ChangeMasterCompanyNumber(propositionDetails);
        HelperSupportData helper = new HelperSupportData();
        if (helper.ChangeMasterCompanyNumber(propositionDetails.Instance, propositionDetails.GetMyParameters()))
            return "";
        else
            return helper.ErrorMessage;
    }

    [WebMethod]
    public static string ChangeEffectiveDate()
    {
        PropositionDetails propositionDetails = MySession.GetPropositionDetails();
        //return CloneHelper.ChangeEffectiveDate(propositionDetails);
        HelperSupportData helper = new HelperSupportData();
        if (helper.ChangeEffectiveDate(propositionDetails.Instance, propositionDetails.GetMyParameters()))
            return "";
        else
            return helper.ErrorMessage;
    }


    [WebMethod]
    public static string ChangeExpirationDate()
    {
        PropositionDetails propositionDetails = MySession.GetPropositionDetails();
        //return CloneHelper.ChangeExpirationDate(propositionDetails);
        HelperSupportData helper = new HelperSupportData();
        if (helper.ChangeExpirationDate(propositionDetails.Instance, propositionDetails.GetMyParameters()))
            return "";
        else
            return helper.ErrorMessage;
    }
}
