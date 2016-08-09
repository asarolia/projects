using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RunBoard
{
    public partial class BatchDetail : System.Web.UI.Page
    {
        public String s = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(System.Web.HttpContext.Current.Session["flow"]) != "nflow")
            {
                Response.Redirect("Default.aspx");
            }
            dtlv1.Text = Convert.ToString(System.Web.HttpContext.Current.Session["detail"]);

            dtlv3.Text = Convert.ToString(System.Web.HttpContext.Current.Session["date"]);
            s = "RAG Status Red: When PXACC048 Batch has not ended ";
            Label1.Text = s;
            s = "RAG Status Amber: When PXACC048 Batch has ended while PXACC050 is still running ";
            Label2.Text = s;
            s = "RAG Status Green: When both the batch jobs have ended successfully ";
            Label3.Text = s;

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            Response.Redirect("datepage.aspx");

        }
    }
}