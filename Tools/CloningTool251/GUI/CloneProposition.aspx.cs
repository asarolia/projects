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
using System.Threading;
using MyHelpers;
using System.Collections.Generic;

public partial class CloneProposition : System.Web.UI.Page
{
    public string SchemeCode
    {
        get
        {
            return SchemeTextBox.Text;
        }
    }
    public string MasterCompanyNumber
    {
        get
        {
            return MasterCompanyTextBox.Text;
        }
    }
    public string ProductCode
    {
        get
        {
            return ProductCodeTextBox.Text;
        }
    }
    public string EffectiveDate
    {
        get
        {
            return EffectiveDateTextBox.Text;
        }
    }
    public string ExpirationDate
    {
        get
        {
            return ExpirationDateTextBox.Text;
        }
    }
    public string SourceSelection
    {
        get
        {
            return hPropositionListDropDown.Value;
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        //PropositionListDropDown.DataSource = CloneHelper.GetPropositionList();
        PropositionListDropDown.DataSource = ExceedPolicy.GetProductList();
        PropositionListDropDown.DataTextField = "Value";
        PropositionListDropDown.DataValueField = "Key";
        PropositionListDropDown.DataBind();
    }

    [WebMethod]
    public static PropositionDetails GetPropositionDetails(string scheme)
    {
        List<ProductList> Proposition = ExceedPolicy.GetProductList(scheme);

        if (!Object.ReferenceEquals(Proposition, null))
            return new PropositionDetails(Proposition[0]);
        else
            return null;
    }
}
