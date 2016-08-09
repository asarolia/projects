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

            Processed.Text = "";
            Unprocessed.Text = "";
            Failed.Text = "";
            
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
            CreateLabel();
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

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CreateLabel()
        {
            Processed.Text = "";
            Unprocessed.Text = "";
            Failed.Text = "";

            if (dtlv1.Text == "PRINT")
            {
                Processed.Text = "Processed Count&nbsp&nbsp&nbsp&nbsp:&nbspTriggers with Status Code P or F ";
                Unprocessed.Text = "Unprocessed Count:&nbspTriggers with Status Code U ";
                Failed.Text = "Failed Count&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbspTriggers with Status Code E ";
            }
            if (dtlv1.Text == "ORACLE")
            {
                Processed.Text = "Processed Count&nbsp&nbsp&nbsp&nbsp:&nbspAR Messages with status as COM ";
                Unprocessed.Text = "Unprocessed Count:&nbspAR Messages with status as INF ";
                Failed.Text = "Failed Count&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbspAR Messages with status as FAL ";
            }
            if (dtlv1.Text == "EMAIL")
            {
                dtlv1.Text = "Cheeta Mail";
                Processed.Text = "Processed Count&nbsp&nbsp&nbsp&nbsp:&nbspTriggers with Status Code P ";
                Unprocessed.Text = "Unprocessed Count:&nbspTriggers with Status Code C ";
                Failed.Text = "Failed Count&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbspTriggers with Status Code E ";
            }
            if (dtlv1.Text == "TI")
            {
                Processed.Text = "Processed Count&nbsp&nbsp&nbsp&nbsp:&nbspTriggers with Status Code F ";
                Unprocessed.Text = "Unprocessed Count:&nbspTriggers with Status Code U ";
                Failed.Text = "Failed Count&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbspTriggers with Status Code E ";
            }
            if (dtlv1.Text == "MDM")
            {
                Processed.Text = "Processed Count&nbsp&nbsp&nbsp&nbsp:&nbspTriggers with Status Code F ";
                Unprocessed.Text = "Unprocessed Count:&nbspTriggers with Status Code U ";
                Failed.Text = "Failed Count&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbspTriggers with Status Code E ";
            }
            if (dtlv1.Text == "PITS")
            {
                Processed.Text = "Processed Count&nbsp&nbsp&nbsp&nbsp:&nbspTriggers with Status Code unequal to E or U ";
                Unprocessed.Text = "Unprocessed Count :&nbspTriggers with Status Code U ";
                Failed.Text = "Failed Count&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbspTriggers with Status Code E ";
            }
            if (dtlv1.Text == "WAMI")
            {
                Processed.Text = "Processed Count&nbsp&nbsp&nbsp&nbsp:&nbspTriggers with Status Code P or D ";
                Unprocessed.Text = "Unprocessed Count :&nbspTriggers with Status Code U ";
                Failed.Text = "Failed Count&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbspTriggers with Status Code E ";
            }
            if (dtlv1.Text == "GFIF")
            {
                Processed.Text = "Processed Count&nbsp&nbsp&nbsp&nbsp:&nbspTriggers with Status Code P ";
                Unprocessed.Text = "Unprocessed Count :&nbspTriggers with Status Code U ";
                Failed.Text = "Failed Count&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbspTriggers with Status Code E ";
            }
            if (dtlv1.Text == "ANOL")
            {
                Processed.Text = "Processed Count&nbsp&nbsp&nbsp&nbsp:&nbspTriggers with Status Code C ";
                Unprocessed.Text = "Unprocessed Count :&nbspTriggers with Status Code Spaces ";
                Failed.Text = "Failed Count&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbspTriggers with Status Code unequal to C or Spaces ";
            }
            if (dtlv1.Text == "CPL Stage 1")
            {
                Processed.Text = "Processed Count&nbsp&nbsp&nbsp&nbsp:&nbspTriggers with Status Code P ";
                Unprocessed.Text = "Unprocessed Count:&nbspTriggers with Status Code U ";
                Failed.Text = "Failed Count&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbspTriggers with Status Code E ";
            }
            if (dtlv1.Text == "CPL Stage 2")
            {
                Processed.Text = "Processed Count&nbsp&nbsp&nbsp&nbsp:&nbspTriggers with Status Code C ";
                Unprocessed.Text = "Unprocessed Count:&nbspTriggers with Status Code U ";
                Failed.Text = "Failed Count&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbspTriggers with Status Code E ";
            }
            if (dtlv1.Text == "CPL 2")
            {
                Processed.Text = "Processed Count&nbsp&nbsp&nbsp&nbsp:&nbspTriggers with Status Code S ";
                Unprocessed.Text = "Unprocessed Count:&nbspTriggers with Status Code U or R ";
                Failed.Text = "Failed Count&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbspTriggers with Status Code E ";
            }
        }

       
    }
}