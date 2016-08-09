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
        public String s = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(System.Web.HttpContext.Current.Session["flow"]) != "nflow")
            {
                Response.Redirect("Default.aspx");
            }
          //  dtlv1.Text = Convert.ToString(System.Web.HttpContext.Current.Session["detail"]);
            dtlv1.Text = "Batch Performance";
            dtlv3.Text = Convert.ToString(System.Web.HttpContext.Current.Session["date"]);
            dtlv4.Text = dtlv3.Text;
            
            s = "RAG Status Red: When number of failures is greater or equal to 5 ";
            Label1.Text = s;
            s = "RAG Status Amber: When number of failure is between 1 and 4 inclusive ";
            Label2.Text = s;
            s = "RAG Status Green: When no batch fails ";
            Label3.Text = s;

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            Response.Redirect("datepage.aspx");

        }
    }
}