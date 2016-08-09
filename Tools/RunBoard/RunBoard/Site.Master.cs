using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RunBoard
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //seldate1.Text = DateTime.Now.ToLongDateString();
            //Session.Add("date", seldate.Text);
            if (Session["username"] != null)
            {
                //    Label1.Text = "Welcome " + Session["username"].ToString();
            }
            else
            {
                //    Label1.Text = "";
                LinkButton1.Text = "";
            }
            
        }
        /*
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            seldate.Text = Calendar1.SelectedDate.ToLongDateString();
        }
        */
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session["username"] = null;
            Response.Redirect("Datepage.aspx");
        }  
    }
}
