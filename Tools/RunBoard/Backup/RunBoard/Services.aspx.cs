using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RunBoard
{
    public partial class Services : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedIndex == 0)
            {
                GridView1.Visible = false;
            }
            else
            {
                GridView1.Visible = true;
            }
            
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("datepage.aspx");
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("datepage.aspx");
        }

    }
}