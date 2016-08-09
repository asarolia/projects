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
    public partial class cpldetail : System.Web.UI.Page
    {
        public String currentdate;

        // To read from web config 

        public static string cs = ConfigurationManager.ConnectionStrings["rundashboardConnectionString"].ConnectionString;

        SqlConnection myConnection = new SqlConnection(cs);


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(System.Web.HttpContext.Current.Session["flow"]) != "nflow")
            {
                Response.Redirect("Default.aspx");
            }
            //dtlv1.Text = Convert.ToString(System.Web.HttpContext.Current.Session["detail"]);
            dtlv3.Text = Convert.ToString(System.Web.HttpContext.Current.Session["date"]);

            // Read the date from the session

            currentdate = Convert.ToString(System.Web.HttpContext.Current.Session["date"]);

            //Get CPL1 Stage1 status 
            getCplStageOneStatus();

            //Get CPL1 Stage2 status 
            getCplStageTwoStatus();

            //Get CPL2 status
            getCplTwoStatus();
        }

        private void getCplTwoStatus()
        {
            opendb();
             int[] yValue = { 1 };
            // set for green flag 

            Chart3.Series["Series1"].Points.DataBindY(yValue);
            Chart3.Series["Series1"].Points[0].Color = Color.Green;

            

             int cntr = 0;
            
            //  SqlDataReader myReader = null;
            String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

            //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            try
            {

                objCommand.CommandType = CommandType.Text;
                objCommand.Parameters.AddWithValue("@ttype1", "CPLTWO");
                objCommand.Parameters.AddWithValue("@ddate", currentdate);
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
                        // Check for Amber flag
                        if ((myReader.GetString(0).Trim() == "CPLTWO") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                            & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                        {

                         //   cplflag = 2;
                            Chart3.Series["Series1"].Points.DataBindY(yValue);
                            Chart3.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        }


                        // Check for Red flag

                        if ((myReader.GetString(0).Trim() == "CPLTWO") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                        {
                           // cplflag = 0;
                            Chart3.Series["Series1"].Points.DataBindY(yValue);
                            Chart3.Series["Series1"].Points[0].Color = Color.Red;

                        }




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

           //  Check for Gray flag - means no data 


            if (cntr == 0)
            {
                //  cplflag = 3;
                Chart3.Series["Series1"].Points.DataBindY(yValue);
                Chart3.Series["Series1"].Points[0].Color = Color.Gray;
            }

          
            closedb();

            //// Need to develop logic for this

           

            //// set for default as gray 

            //Chart1.Series["Series1"].Points.DataBindY(yValue);
            //Chart1.Series["Series1"].Points[0].Color = Color.Gray;
        }

        private void getCplStageTwoStatus()
        {
            opendb();
            int[] yValue = { 1 };
            // set for green flag 

            Chart2.Series["Series1"].Points.DataBindY(yValue);
            Chart2.Series["Series1"].Points[0].Color = Color.Green;



            int cntr = 0;

            //  SqlDataReader myReader = null;
            String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

            //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            try
            {

                objCommand.CommandType = CommandType.Text;
                objCommand.Parameters.AddWithValue("@ttype1", "CPLSTGT");
                objCommand.Parameters.AddWithValue("@ddate", currentdate);
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
                        // Check for Amber flag
                        if ((myReader.GetString(0).Trim() == "CPLSTGT") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                            & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                        {

                            //   cplflag = 2;
                            Chart2.Series["Series1"].Points.DataBindY(yValue);
                            Chart2.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        }


                        // Check for Red flag

                        if ((myReader.GetString(0).Trim() == "CPLSTGT") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                        {
                            // cplflag = 0;
                            Chart2.Series["Series1"].Points.DataBindY(yValue);
                            Chart2.Series["Series1"].Points[0].Color = Color.Red;

                        }




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

            //  Check for Gray flag - means no data 


            if (cntr == 0)
            {
                //  cplflag = 3;
                Chart2.Series["Series1"].Points.DataBindY(yValue);
                Chart2.Series["Series1"].Points[0].Color = Color.Gray;
            }


            closedb();


        }

        private void getCplStageOneStatus()
        {
            opendb();
            int[] yValue = { 1 };
            // set for green flag 

            Chart1.Series["Series1"].Points.DataBindY(yValue);
            Chart1.Series["Series1"].Points[0].Color = Color.Green;



            int cntr = 0;

            //  SqlDataReader myReader = null;
            String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

            //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            try
            {

                objCommand.CommandType = CommandType.Text;
                objCommand.Parameters.AddWithValue("@ttype1", "CPLSTGO");
                objCommand.Parameters.AddWithValue("@ddate", currentdate);
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
                        // Check for Amber flag
                        if ((myReader.GetString(0).Trim() == "CPLSTGO") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                            & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                        {

                            //   cplflag = 2;
                            Chart1.Series["Series1"].Points.DataBindY(yValue);
                            Chart1.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        }


                        // Check for Red flag

                        if ((myReader.GetString(0).Trim() == "CPLSTGO") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                        {
                            // cplflag = 0;
                            Chart1.Series["Series1"].Points.DataBindY(yValue);
                            Chart1.Series["Series1"].Points[0].Color = Color.Red;

                        }




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

            //  Check for Gray flag - means no data 


            if (cntr == 0)
            {
                //  cplflag = 3;
                Chart1.Series["Series1"].Points.DataBindY(yValue);
                Chart1.Series["Series1"].Points[0].Color = Color.Gray;
            }


            closedb();


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("datepage.aspx");
        }

        protected void cpl1stg1_Click(object sender, EventArgs e)
        {
            Session["graph"] = "CPLSTGO";
            Session["date"] = dtlv3.Text;
            Response.Redirect("Graph.aspx");

        }

        protected void cpl1stg1detail_Click(object sender, EventArgs e)
        {
            Session["detail"] = "CPLSTGO";
            Session["date"] = dtlv3.Text;
            Response.Redirect("Detail.aspx");

        }

        protected void cpl1stg2_Click(object sender, EventArgs e)
        {
            Session["graph"] = "CPLSTGT";
            Session["date"] = dtlv3.Text;
            Response.Redirect("Graph.aspx");
        }

        protected void cpl1stg2detail_Click(object sender, EventArgs e)
        {
            Session["detail"] = "CPLSTGT";
            Session["date"] = dtlv3.Text;
            Response.Redirect("Detail.aspx");
        }

        protected void cpl2_Click(object sender, EventArgs e)
        {
            Session["graph"] = "CPLTWO";
            Session["date"] = dtlv3.Text;
            Response.Redirect("Graph.aspx");
        }

        protected void cpl2detail_Click(object sender, EventArgs e)
        {
            Session["detail"] = "CPLTWO";
            Session["date"] = dtlv3.Text;
            Response.Redirect("Detail.aspx");
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
       
    }
}