using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace RunBoard
{
    public partial class feeddetail : System.Web.UI.Page
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

            //Get HSBC feed status 
            getHsbcStatus();

            //Get Barclays feed status 
            getBarclayStatus();

            //Get Santander feed status
            getSantStatus();
        }

        private void getSantStatus()
        {
            opendb();



            int[] yValue = { 1 };

            int feedcntr = 0;

            // set for default as green 

            Chart3.Series["Series1"].Points.DataBindY(yValue);
            Chart3.Series["Series1"].Points[0].Color = Color.Green;

            String sqlst = "SELECT SUM(FeedData.Processed), SUM(FeedData.Success), SUM(FeedData.Fail), limittable.Failed_Threshold_Red, limittable.Failed_Threshold_Amber FROM FeedData INNER JOIN limittable ON Feeddata.Feed_Type = limittable.Ttype AND Feeddata.Feed_Type = @ttype1 AND FeedData.RecordDt = @ddate GROUP BY limittable.Failed_Threshold_Red, limittable.Failed_Threshold_Amber ";

            //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            try
            {

                objCommand.CommandType = CommandType.Text;
                objCommand.Parameters.AddWithValue("@ttype1", "SANTANDER");
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
                        feedcntr = feedcntr + 1;
                        // Check for Amber flag
                        if ((myReader.GetInt32(2) >= myReader.GetInt32(4)) & (myReader.GetInt32(2) < myReader.GetInt32(3)))
                        {

                            // tiflag = 2;
                            Chart3.Series["Series1"].Points.DataBindY(yValue);
                            Chart3.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        }

                        // Check for Red flag
                        if (myReader.GetInt32(2) >= myReader.GetInt32(3))
                        {

                            // tiflag = 2;
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

            // Check for Gray flag - means no data 

            if (feedcntr == 0)
            {
                //tiflag = 3;
                Chart3.Series["Series1"].Points.DataBindY(yValue);
                Chart3.Series["Series1"].Points[0].Color = Color.Gray;
            }

            closedb();


        }

        private void getBarclayStatus()
        {
            opendb();

            

            int[] yValue = { 1 };

            int feedcntr = 0;

            // set for default as green 

            Chart2.Series["Series1"].Points.DataBindY(yValue);
            Chart2.Series["Series1"].Points[0].Color = Color.Green;

            String sqlst = "SELECT SUM(FeedData.Processed), SUM(FeedData.Success), SUM(FeedData.Fail), limittable.Failed_Threshold_Red, limittable.Failed_Threshold_Amber FROM FeedData INNER JOIN limittable ON Feeddata.Feed_Type = limittable.Ttype AND Feeddata.Feed_Type = @ttype1 AND FeedData.RecordDt = @ddate GROUP BY limittable.Failed_Threshold_Red, limittable.Failed_Threshold_Amber ";

            //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            try
            {

                objCommand.CommandType = CommandType.Text;
                objCommand.Parameters.AddWithValue("@ttype1", "BARCLAYS");
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
                        feedcntr = feedcntr + 1;
                        // Check for Amber flag
                        if ((myReader.GetInt32(2) >= myReader.GetInt32(4)) & (myReader.GetInt32(2) < myReader.GetInt32(3)))
                        {

                            // tiflag = 2;
                            Chart2.Series["Series1"].Points.DataBindY(yValue);
                            Chart2.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        }

                        // Check for Red flag
                        if (myReader.GetInt32(2) >= myReader.GetInt32(3))
                        {

                            // tiflag = 2;
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

            // Check for Gray flag - means no data 

            if (feedcntr == 0)
            {
                //tiflag = 3;
                Chart2.Series["Series1"].Points.DataBindY(yValue);
                Chart2.Series["Series1"].Points[0].Color = Color.Gray;
            }

            closedb();


        }

        private void getHsbcStatus()
        {
            opendb();

            // Need to develop logic for this

            int[] yValue = { 1 };

            int feedcntr = 0;

            // set for default as green

            Chart1.Series["Series1"].Points.DataBindY(yValue);
            Chart1.Series["Series1"].Points[0].Color = Color.Green;

            String sqlst = "SELECT SUM(FeedData.Processed), SUM(FeedData.Success), SUM(FeedData.Fail), limittable.Failed_Threshold_Red, limittable.Failed_Threshold_Amber FROM FeedData INNER JOIN limittable ON Feeddata.Feed_Type = limittable.Ttype AND Feeddata.Feed_Type = @ttype1 AND FeedData.RecordDt = @ddate GROUP BY limittable.Failed_Threshold_Red, limittable.Failed_Threshold_Amber ";

            //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            try
            {

                objCommand.CommandType = CommandType.Text;
                objCommand.Parameters.AddWithValue("@ttype1", "HSBC");
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
                        feedcntr = feedcntr + 1;
                        // Check for Amber flag
                        if ((myReader.GetInt32(2) >= myReader.GetInt32(4)) & (myReader.GetInt32(2) < myReader.GetInt32(3)))
                        {

                            // tiflag = 2;
                            Chart1.Series["Series1"].Points.DataBindY(yValue);
                            Chart1.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        }

                        // Check for Red flag
                        if (myReader.GetInt32(2) >= myReader.GetInt32(3))
                        {

                            // tiflag = 2;
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

            // Check for Gray flag - means no data 

            if (feedcntr == 0)
            {
                //tiflag = 3;
                Chart1.Series["Series1"].Points.DataBindY(yValue);
                Chart1.Series["Series1"].Points[0].Color = Color.Gray;
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


       
        protected void hsbc_Click(object sender, EventArgs e)
        {
            Session["graph"] = "HSBC";
            Session["date"] = dtlv3.Text;
            Response.Redirect("Graphv.aspx");

        }

        protected void hsbcdetail_Click(object sender, EventArgs e)
        {
            Session["detail"] = "HSBC";
            Session["date"] = dtlv3.Text;
            Response.Redirect("Detailv.aspx");
        }

        protected void barc_Click(object sender, EventArgs e)
        {
            Session["graph"] = "BARCLAYS";
            Session["date"] = dtlv3.Text;
            Response.Redirect("Graphv.aspx");

        }

        protected void barcdetail_Click(object sender, EventArgs e)
        {
            Session["detail"] = "BARCLAYS";
            Session["date"] = dtlv3.Text;
            Response.Redirect("Detailv.aspx");

        }

        protected void sant_Click(object sender, EventArgs e)
        {
            Session["graph"] = "SANTANDER";
            Session["date"] = dtlv3.Text;
            Response.Redirect("Graphv.aspx");

        }

        protected void santdetail_Click(object sender, EventArgs e)
        {
            Session["detail"] = "SANTANDER";
            Session["date"] = dtlv3.Text;
            Response.Redirect("Detailv.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("datepage.aspx");
        }
    }
}