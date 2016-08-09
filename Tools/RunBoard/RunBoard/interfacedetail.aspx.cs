using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace RunBoard
{
    public partial class interfacedetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(System.Web.HttpContext.Current.Session["flow"]) != "nflow")
            {
                Response.Redirect("Default.aspx");
            }
            dtlv1.Text = Convert.ToString(System.Web.HttpContext.Current.Session["detail"]);

            dtlv3.Text = Convert.ToString(System.Web.HttpContext.Current.Session["date"]);
            
        }
       
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("datepage.aspx");
        }
        protected void grd_Pre1(object sender, EventArgs e)
        {
            GridViewRow gv = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableCell tc = new TableCell();
            tc.ColumnSpan = 4;
            tc.Text = "Unprocessed Triggers";
            tc.Attributes.Add("style", "text-align:center");
            gv.Cells.Add(tc);
            this.GridView1.Controls[0].Controls.AddAt(0, gv);
        }
        protected void grd_Pre2(object sender, EventArgs e)
        {
            GridViewRow gv = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableCell tc = new TableCell();
            tc.ColumnSpan = 4;
            tc.Text = "Errored Triggers";
            tc.Attributes.Add("style", "text-align:center");
            gv.Cells.Add(tc);
            this.GridView2.Controls[0].Controls.AddAt(0, gv);
        }
    }
}