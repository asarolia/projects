using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RunBoard
{
    public partial class BatchPDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(System.Web.HttpContext.Current.Session["flow"]) != "nflow")
            {
                Response.Redirect("Default.aspx");
            }
          //  dtlv1.Text = Convert.ToString(System.Web.HttpContext.Current.Session["detail"]);
            dtlv1.Text = "Batch Performance";
            dtlv3.Text = Convert.ToString(System.Web.HttpContext.Current.Session["date"]);

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            Response.Redirect("datepage.aspx");

        }
    }
}