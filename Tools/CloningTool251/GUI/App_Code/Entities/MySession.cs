using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for MySession
/// </summary>
public class MySession
{
	public MySession()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static PropositionDetails GetPropositionDetails()
    {
        return GetPropositionDetails(true);
    }

    public static PropositionDetails GetPropositionDetails(bool redirect)
    {
        PropositionDetails det = HttpContext.Current.Session["PropositionDetails"] as PropositionDetails;

        if (det == null & redirect)
            HttpContext.Current.Response.Redirect("~/Welcome.aspx?sessionExpired");

        return det;
    }
    public static void SetPropositionDetails(PropositionDetails propositiondetails)
    {
        HttpContext.Current.Session["PropositionDetails"] = propositiondetails;
    }

}
