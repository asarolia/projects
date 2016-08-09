using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RunBoard
{
    public partial class Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(System.Web.HttpContext.Current.Session["flow"]) != "nflow")
            {
                Response.Redirect("Default.aspx");
            }
            dtlv1.Text = Convert.ToString(System.Web.HttpContext.Current.Session["detail"]);

            if(dtlv1.Text == "CPLSTGO")
            {
                dtlv1.Text = "CPL Stage 1";
            }
            if (dtlv1.Text == "CPLSTGT")
            {
                dtlv1.Text = "CPL Stage 2";
            }
            if (dtlv1.Text == "CPLTWO")
            {
                dtlv1.Text = "CPL 2";
            }

            dtlv3.Text = Convert.ToString(System.Web.HttpContext.Current.Session["date"]);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if ((dtlv1.Text == "CPL Stage 1") | (dtlv1.Text == "CPL Stage 2") | (dtlv1.Text == "CPL 2"))
            {
                Response.Redirect("cpldetail.aspx");
            }
            else
            {
                Response.Redirect("datepage.aspx");
            }
        }

       
    }
}