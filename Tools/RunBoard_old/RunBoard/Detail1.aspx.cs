using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace RunBoard
{
    public partial class Detail1 : System.Web.UI.Page
    {
        public String currentdate;
        public String detail;
        public int ftr;
        public int fta;
        public int utr;
        public int uta;
        public String s = "";
        public int cntr = 0;

        // To read from web config 

        public static string cs = ConfigurationManager.ConnectionStrings["rundashboardConnectionString"].ConnectionString;

        SqlConnection myConnection = new SqlConnection(cs);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(System.Web.HttpContext.Current.Session["flow"]) != "nflow")
            {
                Response.Redirect("Default.aspx");
            }
            dtlv1.Text = Convert.ToString(System.Web.HttpContext.Current.Session["detail"]);
            dtlv3.Text = Convert.ToString(System.Web.HttpContext.Current.Session["date"]);

            // Read from the session

            currentdate = Convert.ToString(System.Web.HttpContext.Current.Session["date"]);
            detail = Convert.ToString(System.Web.HttpContext.Current.Session["detail"]);
            
            CreateLabel();
        }

        private void CreateLabel()
        {
            opendb();

            
            //  SqlDataReader myReader = null;
            String sqlst = "SELECT * FROM limittable WHERE Ttype = @ttype1";

            
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            try
            {

                objCommand.CommandType = CommandType.Text;
                objCommand.Parameters.AddWithValue("@ttype1", detail);
  
                if (myConnection != null && myConnection.State == ConnectionState.Closed)
                {
                    objCommand.Connection.Open();

                }


                SqlDataReader myReader = objCommand.ExecuteReader();



                //}
                if (myReader.HasRows)
                {
                    

                    while (myReader.Read())
                    {
                        cntr = cntr + 1;
                        ftr = myReader.GetInt32(1);
                        utr = myReader.GetInt32(2);
                        fta = myReader.GetInt32(3);
                        uta = myReader.GetInt32(4);

                    }

                    myReader.Close();
                }
                else
                {
                    myReader.Close();
                }

                if (myConnection != null && myConnection.State == ConnectionState.Open)
                {
                    objCommand.Connection.Close();

                }


            }
            catch (SqlException SqlException)
            {

                Response.Write(SqlException.Message);
                throw SqlException;
            }

            if (cntr >= 1)
            {
                s = "Failed Threshold Red:" + ftr + "%";
                Label1.Text = s;
                s = "Failed Threshold Amber:" + fta + "%";
                Label2.Text = s;
                s = "Unprocess Threshold Red:" + utr + "%";
                Label3.Text = s;
                s = "Unprocess Threshold Amber:" + uta + "%";
                Label4.Text = s;
            }
            closedb();

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

        // function to close the database connection 
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("datepage.aspx");
        }
       
        
    }
}