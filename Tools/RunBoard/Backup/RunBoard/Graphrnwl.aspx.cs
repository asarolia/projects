using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RunBoard
{
    public partial class Graphrnwl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(System.Web.HttpContext.Current.Session["flow"]) != "nflow")
            {
                Response.Redirect("Default.aspx");
            }
            DateTime d1 = DateTime.Now;
            // DateTime d2 = new DateTime(d1.Year, d1.Month, d1.Day, 0, 0, 0);
            DateTime d3 = d1.Date.AddDays(-7);
            String s = d3.ToString("yyyy/MM/dd");
            Session["date1"] = s;

            //graphv1.Text = Convert.ToString(System.Web.HttpContext.Current.Session["graph"]);
            Label1.Text = Convert.ToString(System.Web.HttpContext.Current.Session["graph"]);
            graphv3.Text = Convert.ToString(System.Web.HttpContext.Current.Session["date"]);

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("datepage.aspx");
        }
    }
}