using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace RunBoard
{
    public partial class Graphv : System.Web.UI.Page
    {
        StringBuilder str = new StringBuilder();

        // For local server 

        //SqlConnection conn = new SqlConnection("Data Source=CSCINDAE707743\\SQLEXPRESS;Initial Catalog=rundashboard;Integrated Security=True");

        // For aviva server 
        //SqlConnection conn = new SqlConnection("Data Source=SQLD005C.via.novonet;Initial Catalog=Aviva_BNC_Earth;User Id=Earth_user;Password=ZYnS46t6We;Integrated Security=True");

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

            graphv1.Text = Convert.ToString(System.Web.HttpContext.Current.Session["graph"]);

            //if (graphv1.Text == "CPLSTGO")
            //{
            //    graphv1.Text = "CPL Stage 1";
            //}
            //if (graphv1.Text == "CPLSTGT")
            //{
            //    graphv1.Text = "CPL Stage 2";
            //}
            //if (graphv1.Text == "CPLTWO")
            //{
            //    graphv1.Text = "CPL 2";
            //}
            Label1.Text = Convert.ToString(System.Web.HttpContext.Current.Session["graph"]);

            //if (Label1.Text == "CPLSTGO")
            //{
            //    Label1.Text = "CPL Stage 1";
            //}
            //if (Label1.Text == "CPLSTGT")
            //{
            //    Label1.Text = "CPL Stage 2";
            //}
            //if (Label1.Text == "CPLTWO")
            //{
            //    Label1.Text = "CPL 2";
            //}
            graphv3.Text = Convert.ToString(System.Web.HttpContext.Current.Session["date"]);



        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
                Response.Redirect("feeddetail.aspx");
            
        }

     
       
      
    }
}