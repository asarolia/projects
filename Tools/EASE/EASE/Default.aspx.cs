using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EASE
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // KMSearch obj = new KMSearch();

        }

        protected void Search_Click(object sender, EventArgs e)
        {
            Session["search"] = TextBox1.Text;
            Response.Redirect("results.aspx");
        }
    }
}
