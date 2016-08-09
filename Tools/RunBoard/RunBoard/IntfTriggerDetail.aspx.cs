using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace RunBoard
{
    public partial class IntfTriggerDetail : System.Web.UI.Page
    {
        public string TriggerStatus;
        public static string cs = ConfigurationManager.ConnectionStrings["rundashboardConnectionString"].ConnectionString;
        SqlConnection myConnection = new SqlConnection(cs);
        protected void Page_Load(object sender, EventArgs e)
        {
            DetailLabel.Text = Convert.ToString(System.Web.HttpContext.Current.Session["detail"]);
            dtlv3.Text = Convert.ToString(System.Web.HttpContext.Current.Session["date"]);
            if (DetailLabel.Text.Trim() == "Unprocessed Triggers")
            {
                TriggerStatus = "U";
            }
            if (DetailLabel.Text.Trim() == "Errored Triggers")
            {
                TriggerStatus = "E";
            }
            if (DetailLabel.Text.Trim() == "Pending Print Triggers (Status P)")
            {
                TriggerStatus = "P";
            }

            opendb();
            SqlDataSource SqlDataSource1 = new SqlDataSource();
            this.Page.Controls.Add(SqlDataSource1);
            SqlDataSource1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["rundashboardConnectionString"].ConnectionString;
            SqlDataSource1.ID = "SqlDataSource1";
            SqlDataSource1.SelectCommand = "SELECT Interface, Count, MaxDate, MinDate FROM CommentTable Where RecordDt = '" + dtlv3.Text + "' and Type = '" + DetailLabel.Text + "' ORDER BY CommentTable.CommentRecordDt DESC";

            DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            int rowcount = (int)dv.Table.Rows.Count;
            if (rowcount > 0)
            {
                GridView1.DataSource = SqlDataSource1;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = SqlDataSource1;
                GridView1.DataBind();
                
            }
            closedb();

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("datepage.aspx");
        }
        private void opendb()
        {
            try
            {
                myConnection.Open();


            }
            catch
            {
                throw new Exception("Not able to open or connect to database");

            }
        }
        public void closedb()
        {
            try
            {
                myConnection.Close();


            }
            catch
            {
                throw new Exception("Not able to close database connection");

            }
        }
    }
}