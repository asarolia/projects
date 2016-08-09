using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.Globalization;

namespace RunBoard
{
    public partial class Comments : System.Web.UI.Page
    {

        public static string cs = ConfigurationManager.ConnectionStrings["rundashboardConnectionString"].ConnectionString;
        SqlConnection myConnection = new SqlConnection(cs);
        //public string convertmode;
        //public string convertinputtext;
        public string dtlv1;
        public string convertoutputtext;
        public string CommentTexOutput;
        protected void Page_Load(object sender, EventArgs e)
        {
            dtlv1 = Request.QueryString["detail"];
            commentheader.Text = convertheader('1', dtlv1.Trim());
            dtlv3.Text = Request.QueryString["date"];
            if (!IsPostBack)
            {
                loadcomments();
            }
            

            //opendb();
            //closedb();
        }

        private string getCommentText(String RagDate, String Type, String RacfId, String RecordDate)
        {
            opendb();  
            string sqlst = "Select CommentText from CommentTable where RecordDt = '" + RagDate + "' and Type = '" + Type + "' and RACFID = '" + RacfId + "' and CommentRecordDt = '" + RecordDate+"'";
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            SqlDataReader myReader = objCommand.ExecuteReader();
            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    CommentTexOutput = myReader.GetString(0).Trim();
                }
            }
            else
            {
               CommentTexOutput = "";
            }
            closedb();
            return CommentTexOutput;
        }
        private string convertheader(Char convertmode, string convertinputtext)
        {
          if (convertmode == '1') //Convert header text from type to detail text
         {
            if (convertinputtext == "PRINT")
            {
                convertoutputtext = "Print Status";
            }
            if (convertinputtext == "ORACLE")
            {
                convertoutputtext = "Oracle AR Processing";
            }
            if (convertinputtext == "CARD")
            {
                convertoutputtext = "Card Transactions";
            }
            if (convertinputtext == "RENEWAL")
            {
                convertoutputtext = "Renewals";
            }
            if (convertinputtext == "EMAIL")
            {
                convertoutputtext = "Email Processing";
            }
            if (convertinputtext == "TI")
            {
                convertoutputtext = "Triple Interface";
            }
//Added for GEOH-3046 Starts
            if (convertinputtext == "TSB")
            {
                convertoutputtext = "TSB";
            }
			if (convertinputtext == "BYS")
            {
                convertoutputtext = "BYS";
            }
//Added for GEOH-3046 Ends
            if (convertinputtext == "MDM")
            {
                convertoutputtext = "MDM";
            }
            if (convertinputtext == "DATAFEED")
            {
                convertoutputtext = "Datafeed";
            }
            if (convertinputtext == "PITS")
            {
                convertoutputtext = "PITS";
            }
            if (convertinputtext == "WAMI")
            {
                convertoutputtext = "WAMI";
            }
            if (convertinputtext == "SEPS")
            {
                convertoutputtext = "SEPS";
            }
            if (convertinputtext == "GFIF")
            {
                convertoutputtext = "GFIF";
            }
            if (convertinputtext == "ANOL")
            {
                convertoutputtext = "ANOL";
            }
            if (convertinputtext == "CPL")
            {
                convertoutputtext = "CPL";
            }
            if (convertinputtext == "BATCHS")
            {
                convertoutputtext = "Batch Status";
            }
            if (convertinputtext == "BATCHP")
            {
                convertoutputtext = "Batch Performance";
            }
            if (convertinputtext == "DEFER")
            {
                convertoutputtext = "Defer Queue";
            }
            if (convertinputtext == "HALERR")
            {
                convertoutputtext = "Hal Errors";
            }
            if (convertinputtext == "INTFTRGDTL")
            {
                convertoutputtext = "Interface Trigger Detail";
            }
         }
          if (convertmode == '2') //Convert header text from type to detail text
          {
              if (convertinputtext == "Interface Trigger Detail")
              {
                  convertoutputtext = "INTFTRGDTL";
              }
              if (convertinputtext == "Print Status")
              {
                  convertoutputtext = "PRINT";
              }
              if (convertinputtext == "Oracle AR Processing")
              {
                  convertoutputtext = "ORACLE";
              }
              if (convertinputtext == "Card Transactions")
              {
                  convertoutputtext = "CARD";
              }
              if (convertinputtext == "Renewals")
              {
                  convertoutputtext = "RENEWAL";
              }
              if (convertinputtext == "Email Processing")
              {
                  convertoutputtext = "EMAIL";
              }
              if (convertinputtext == "Triple Interface")
              {
                  convertoutputtext = "TI";
              }
//Added for GEOH-3046 Starts
			if (convertinputtext == "TSB")
              {
                  convertoutputtext = "TSB";
              }
//Added for GEOH-3046 Ends			  
              
              if (convertinputtext == "MDM")
              {
                  convertoutputtext = "MDM";
              }
              if (convertinputtext == "Datafeed")
              {
                  convertoutputtext = "DATAFEED";
              }
              if (convertinputtext == "PITS")
              {
                  convertoutputtext = "PITS";
              }
              if (convertinputtext == "WAMI")
              {
                  convertoutputtext = "WAMI";
              }
              if (convertinputtext == "SEPS")
              {
                  convertoutputtext = "SEPS";
              }
              if (convertinputtext == "GFIF")
              {
                  convertoutputtext = "GFIF";
              }
              if (convertinputtext == "ANOL")
              {
                  convertoutputtext = "ANOL";
              }
              if (convertinputtext == "CPL")
              {
                  convertoutputtext = "CPL";
              }
              if (convertinputtext == "Batch Status")
              {
                  convertoutputtext = "BATCH";
              }
              if (convertinputtext == "Batch Performance")
              {
                  convertoutputtext = "BATCHP";
              }
              if (convertinputtext == "Defer Queue")
              {
                  convertoutputtext = "DEFER";
              }
              if (convertinputtext == "Hal Errors")
              {
                  convertoutputtext = "HALERR";
              }
          }

            return convertoutputtext;
        }

        
        protected void loadcomments()
        {
            opendb();            
            SqlDataSource SqlDataSource1 = new SqlDataSource();
            this.Page.Controls.Add(SqlDataSource1);
            SqlDataSource1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["rundashboardConnectionString"].ConnectionString;
            SqlDataSource1.ID = "SqlDataSource1";
            
            SqlDataSource1.SelectCommand = "SELECT CommentText, CommentRecordDt, RACFID FROM CommentTable Where RecordDt = '" + dtlv3.Text + "' and Type = '" + dtlv1 + "' ORDER BY CommentTable.CommentRecordDt DESC";
            
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
                lblmsg.Text = "No comments found !!!";
            }
            closedb();
        }
        protected void lnkView_Click(object sender, EventArgs e)
        {
            LinkButton btnsubmit = sender as LinkButton;
            GridViewRow gRow = (GridViewRow)btnsubmit.NamingContainer;
            
            racfid_text_view.Text = gRow.Cells[3].Text.Trim();
            Label CommentTextView = (Label)gRow.FindControl("CommentText");
            CommentText_text_view.Text = CommentTextView.Text;
            CommentRecordDt_Text_view.Text = gRow.Cells[2].Text.Trim();

            //CommentText_text_view.Text = getCommentText(dtlv3.Text, dtlv1.Trim(), racfid_text_view.Text, CommentRecordDt_Text_view.Text);
            this.ModalPopupExtender4.Show();
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton btnsubmit = sender as LinkButton;
            GridViewRow gRow = (GridViewRow)btnsubmit.NamingContainer;
            racfid_text.Text = gRow.Cells[3].Text.Trim();
            Label CommentTextView = (Label)gRow.FindControl("CommentText");
            CommentText_text.Text = CommentTextView.Text;
            CommentRecordDt_Text.Text = gRow.Cells[2].Text.Trim();
            //CommentText_text.Text = getCommentText(dtlv3.Text, dtlv1.Trim(), racfid_text.Text, CommentRecordDt_Text.Text);
            
            this.ModalPopupExtender1.Show();
        }
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            LinkButton btnsubmit = sender as LinkButton;
            GridViewRow gRow = (GridViewRow)btnsubmit.NamingContainer;
            racfid_text_delete.Text = gRow.Cells[3].Text.Trim();
            Label CommentTextView = (Label)gRow.FindControl("CommentText");
            CommentText_text_delete.Text = CommentTextView.Text; 
            CommentRecordDt_Text_delete.Text = gRow.Cells[2].Text.Trim();
            //CommentText_text_delete.Text = getCommentText(dtlv3.Text, dtlv1.Trim(), racfid_text_delete.Text, CommentRecordDt_Text_delete.Text);
            this.ModalPopupExtender2.Show();
        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            LinkButton btnsubmit = sender as LinkButton;
            racfid_text_add.Text= "";
            //CommentTitle_text_add.Text = "";
            CommentText_text_add.Text ="";

            //GridViewRow gRow = (GridViewRow)btnsubmit.NamingContainer;
            this.ModalPopupExtender3.Show();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            opendb();
            String sqlst = "Insert into CommentTable (RecordDt,Type,CommentRecordDt,RACFID,CommentText ) Values ('" + dtlv3.Text + "','" + dtlv1 + "'," + "CURRENT_TIMESTAMP, @racfid_text_add, @CommentText_text_add ) ";
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            objCommand.Parameters.AddWithValue("@CommentText_text_add", CommentText_text_add.Text);
            objCommand.Parameters.AddWithValue("@racfid_text_add", racfid_text_add.Text);
            objCommand.ExecuteNonQuery();
            closedb();
            lblmsg.Text = "Comment Added...";
            loadcomments();

        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            opendb();
            String sqlst = "Delete from CommentTable Where RecordDt = '" + dtlv3.Text + "' and RACFID = '" + racfid_text_delete.Text + "' and CommentRecordDt = '" + CommentRecordDt_Text_delete.Text + "' and Type ='" +dtlv1+"'";
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            objCommand.ExecuteNonQuery();
            closedb();
            lblmsg.Text = "Comment Deleted...";
            loadcomments();

        }
        protected void btnModity_Click(object sender, EventArgs e)
        {
            opendb();
            //String sqlst = "update CommentTable set CommentText='" + CommentText_text.Text + "', RACFID='" + racfid_text.Text + "', CommentTitle ='"+CommentTitle_text.Text +"' Where RecordDt = '" + dtlv3.Text + "' and CommentRecordDt = '" + CommentRecordDt_Text.Text + "' and Type ='" + dtlv1 + "'";
            String sqlst = "update CommentTable set CommentText=@CommentText_text, RACFID=@racfid_text Where RecordDt = '" + dtlv3.Text + "' and CommentRecordDt = '" + CommentRecordDt_Text.Text + "' and Type ='" + dtlv1 + "'";
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            objCommand.Parameters.AddWithValue("@CommentText_text", CommentText_text.Text);
            objCommand.Parameters.AddWithValue("@racfid_text", racfid_text.Text);
            objCommand.ExecuteNonQuery();
            closedb();
            lblmsg.Text = "Comment Updated...";
            loadcomments();
            
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