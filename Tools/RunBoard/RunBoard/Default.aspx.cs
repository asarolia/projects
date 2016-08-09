using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace RunBoard
{
    public partial class _Default : System.Web.UI.Page
    {

        public String currentdate;

        // calculate 2 week prior date from the system date 
        public static DateTime d1 = DateTime.Now;
       // public String cdate = d1.ToString("yyyyMMdd");
        //   System.Console.WriteLine(cdate);
        public static DateTime d2 = d1.AddDays(-7);
       // public String pdate = d2.ToString("yyyyMMdd");

        public int rnwl_failed_per = 0;
       // public int rnwl_failed_amber = 0;
        public int rnwl_unprocess_per = 0;
       // public int rnwl_unprocess_amber = 0;

        public int card_failed_per = 0;
       // public int card_failed_amber = 0;
        public int card_unprocess_per = 0;
       // public int card_unprocess_amber = 0;

        public int[] cValue = { 1 };
        public int cplcntr = 0;
        public int feedcntr = 0;
        public int[] fValue = { 1 };
        public Boolean batchflag = false;


        // To read from web config 

        public static string cs = ConfigurationManager.ConnectionStrings["rundashboardConnectionString"].ConnectionString;

        SqlConnection myConnection = new SqlConnection(cs);

        protected void Page_Load(object sender, EventArgs e)
        {
            CalendarExtender.StartDate = DateTime.Now.AddDays(-30);
            CalendarExtender.EndDate   = DateTime.Now; 
             Session["flow"] = "nflow";
          
                seldate1.Text = DateTime.Now.ToString("yyyy/MM/dd");

          
                Session["date"] = seldate1.Text;

                // Read the date from the session

                currentdate = Convert.ToString(System.Web.HttpContext.Current.Session["date"]);
             
            // get Print Status 
                getPrintStatus();
            // get PITS Status
                getPitsStatus();
            
            // get Oracle Status
                getOracleStatus();

            // get WAMI Status
                getWamiStatus();

            // get Card Status
                getCardStatus();

            // get SEPS Status
                getSepsStatus();

            // get Renewal Status
                getRenewalStatus();

            // get GFIF Status
                getGfifStatus();

            // get Email Status
                getEmailStatus();

            // get Anol Status
                getAnolStatus();

            // get TI Status
                getTIStatus();

            // get CPL Status
                getCplStatus();

            // Get Batch Status
                getBatchStatus();

            // Get datafeed Status
                getDatafeedStatus();

            // Get Interfaces Status
            //    getInterfaceStatus();
            //Get Batch performance status
                getBatchPerformance();

            // Get Halerr status
                getHalerrStatus();

                // Get interface trigger status
                getinterfacetriggerStatus();
            
            // Get MDM Status
                getMDMStatus();

            // Get Defer Status
                getDeferStatus();



        }

        private void getDeferStatus()
        {
            opendb();

            int[] yValue = { 1 };
            // set for green flag 

            Chart14.Series["Series1"].Points.DataBindY(yValue);
            Chart14.Series["Series1"].Points[0].Color = Color.Green;

            int cntr = 0;
            //  SqlDataReader myReader = null;
            String sqlst = "SELECT Delay FROM deferdata WHERE RecordDt = @ddate";

            //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            try
            {

                objCommand.CommandType = CommandType.Text;
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
                        if ((myReader.GetInt32(0) >= 3) & (myReader.GetInt32(0) < 5))
                        {

                            // tiflag = 2;
                            Chart14.Series["Series1"].Points.DataBindY(yValue);
                            Chart14.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        }


                        // Check for Red flag

                        if (myReader.GetInt32(0) > 5)
                        {
                            //   tiflag = 0;
                            Chart14.Series["Series1"].Points.DataBindY(yValue);
                            Chart14.Series["Series1"].Points[0].Color = Color.Red;

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

            if (cntr == 0)
            {
                //tiflag = 3;
                Chart14.Series["Series1"].Points.DataBindY(yValue);
                Chart14.Series["Series1"].Points[0].Color = Color.Gray;
            }
            else
            {
                Chart14.Series["Series1"].Points[0].ToolTip = "Click to View RAG staus details";
                Chart14.Series["Series1"].Points[0].Url = "Comments.aspx?detail=DEFER" + "&date=" + seldate1.Text;
            }

            closedb();


        }

        private void getMDMStatus()
        {
            opendb();

            int[] yValue = { 1 };
            // set for green flag 

            Chart17.Series["Series1"].Points.DataBindY(yValue);
            Chart17.Series["Series1"].Points[0].Color = Color.Green;

            int cntr = 0;
            //  SqlDataReader myReader = null;
            String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

            //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            try
            {

                objCommand.CommandType = CommandType.Text;
                objCommand.Parameters.AddWithValue("@ttype1", "MDM");
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
                        if ((myReader.GetString(0).Trim() == "MDM") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                            & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                        {

                            //  sepsflag = 2;
                            Chart17.Series["Series1"].Points.DataBindY(yValue);
                            Chart17.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        }


                        // Check for Red flag

                        if ((myReader.GetString(0).Trim() == "MDM") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                        {
                            // sepsflag = 0;
                            Chart17.Series["Series1"].Points.DataBindY(yValue);
                            Chart17.Series["Series1"].Points[0].Color = Color.Red;

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

            if (cntr == 0)
            {
                // sepsflag = 3;
                Chart17.Series["Series1"].Points.DataBindY(yValue);
                Chart17.Series["Series1"].Points[0].Color = Color.Gray;
            }
            else
            {
                Chart17.Series["Series1"].Points[0].ToolTip = "Click to View RAG staus details";
                Chart17.Series["Series1"].Points[0].Url = "Comments.aspx?detail=MDM" + "&date=" + seldate1.Text;
            }
            closedb();


        }

        private void getHalerrStatus()
        {


            //// need to implement the logic

            int[] yValue = { 1 };
            //// Currently defaulting to gray

            Chart16.Series["Series1"].Points.DataBindY(yValue);
            Chart16.Series["Series1"].Points[0].Color = Color.Blue;
            Chart16.Series["Series1"].Points[0].ToolTip = "Click to View RAG staus details";
            Chart16.Series["Series1"].Points[0].Url = "Comments.aspx?detail=HALERR" + "&date=" + seldate1.Text;

        }

        private void getinterfacetriggerStatus()
        {


            //// need to implement the logic

            int[] yValue = { 1 };
            //// Currently defaulting to gray

            Chart19.Series["Series1"].Points.DataBindY(yValue);
            Chart19.Series["Series1"].Points[0].Color = Color.Blue;
            Chart19.Series["Series1"].Points[0].ToolTip = "Click to View RAG staus details";
            Chart19.Series["Series1"].Points[0].Url = "Comments.aspx?detail=INTFTRGDTL" + "&date=" + seldate1.Text;

        }
        private void getBatchPerformance()
        {
            opendb();

            int[] yValue = { 1 };
            // set for green flag 

            Chart15.Series["Series1"].Points.DataBindY(yValue);
            Chart15.Series["Series1"].Points[0].Color = Color.Green;

            int cntr = 0;
            //  SqlDataReader myReader = null;
            String sqlst = "SELECT COUNT(*) AS CNT, limittable.Failed_Threshold_Red, limittable.Failed_Threshold_Amber FROM BatchData INNER JOIN limittable ON BatchData.Job_Category = limittable.Ttype AND BatchData.Job_Category = @cat AND RecordDt = @ddate GROUP BY limittable.Failed_Threshold_Red, limittable.Failed_Threshold_Amber";

            //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            try
            {

                objCommand.CommandType = CommandType.Text;
                objCommand.Parameters.AddWithValue("@cat", "BATCHPERF");
                objCommand.Parameters.AddWithValue("@ddate", currentdate);
                if (myConnection != null && myConnection.State == ConnectionState.Closed)
                {
                    objCommand.Connection.Open();

                }


                SqlDataReader myReader = objCommand.ExecuteReader();



                //}
                if (myReader.HasRows)
                {
                    //batchflag = true;
                    while (myReader.Read())
                    {
                        if (myReader.GetInt32(0) != 0)
                        {
                            cntr = cntr + 1;
                            // Check for Amber flag
                            if ((myReader.GetInt32(0) >= myReader.GetInt32(2)) & (myReader.GetInt32(0) < myReader.GetInt32(1)))
                            {

                                // tiflag = 2;
                                Chart15.Series["Series1"].Points.DataBindY(yValue);
                                Chart15.Series["Series1"].Points[0].Color = Color.DarkOrange;

                            }


                            // Check for Red flag

                            if (myReader.GetInt32(0) > myReader.GetInt32(1))
                            {
                                //   tiflag = 0;
                                Chart15.Series["Series1"].Points.DataBindY(yValue);
                                Chart15.Series["Series1"].Points[0].Color = Color.Red;

                            }


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

            Chart15.Series["Series1"].Points[0].ToolTip = "Click to View RAG staus details";
            Chart15.Series["Series1"].Points[0].Url = "Comments.aspx?detail=BATCHP" + "&date=" + seldate1.Text;
            
            // Check for Gray flag - means no data 

            /*if (cntr == 0)
            {
                 // tiflag = 3;
                Chart15.Series["Series1"].Points.DataBindY(yValue);
                Chart15.Series["Series1"].Points[0].Color = Color.Gray;
            }*/

            closedb();


            
        }

        private void getDatafeedStatus()
        {
            opendb();
            feedcntr = 0;

            
            // set for default green flag 

            Chart18.Series["Series1"].Points.DataBindY(fValue);
            Chart18.Series["Series1"].Points[0].Color = Color.Green;

            CheckHsbcAmber();
            CheckBarclayAmber();
            CheckSantAmber();

            CheckHsbcRed();
            CheckBarclayRed();
            CheckSantRed();

            // Check for Gray flag - means no data 

            if (feedcntr == 0)
            {
                //  cplflag = 3;
                Chart18.Series["Series1"].Points.DataBindY(fValue);
                Chart18.Series["Series1"].Points[0].Color = Color.Gray;
            }
            else
            {
                Chart18.Series["Series1"].Points[0].ToolTip = "Click to View RAG staus details";
                Chart18.Series["Series1"].Points[0].Url = "Comments.aspx?detail=DATAFEED" + "&date=" + seldate1.Text;
            }

            closedb();


        }

        private void CheckHsbcAmber()
        {
            //  SqlDataReader myReader = null;
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
                            Chart18.Series["Series1"].Points.DataBindY(fValue);
                            Chart18.Series["Series1"].Points[0].Color = Color.DarkOrange;

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
        }

        private void CheckBarclayAmber()
        {
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
                            Chart18.Series["Series1"].Points.DataBindY(fValue);
                            Chart18.Series["Series1"].Points[0].Color = Color.DarkOrange;

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
        }

        private void CheckSantAmber()
        {
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
                            Chart18.Series["Series1"].Points.DataBindY(fValue);
                            Chart18.Series["Series1"].Points[0].Color = Color.DarkOrange;

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
        }

        private void CheckHsbcRed()
        {
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
                        
                        // Check for Red flag

                        if (myReader.GetInt32(2) > myReader.GetInt32(3))
                        {
                            //   tiflag = 0;
                            Chart18.Series["Series1"].Points.DataBindY(fValue);
                            Chart18.Series["Series1"].Points[0].Color = Color.Red;

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
        }

        private void CheckBarclayRed()
        {
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
                       

                        // Check for Red flag

                        if (myReader.GetInt32(2) > myReader.GetInt32(3))
                        {
                            //   tiflag = 0;
                            Chart18.Series["Series1"].Points.DataBindY(fValue);
                            Chart18.Series["Series1"].Points[0].Color = Color.Red;

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
        }

        private void CheckSantRed()
        {
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
                        
                        // Check for Red flag

                        if (myReader.GetInt32(2) > myReader.GetInt32(3))
                        {
                            //   tiflag = 0;
                            Chart18.Series["Series1"].Points.DataBindY(fValue);
                            Chart18.Series["Series1"].Points[0].Color = Color.Red;

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
        }

        private void getBatchStatus()
        {
            opendb();

            int[] yValue = { 1 };
            // set for default green flag 

            Chart13.Series["Series1"].Points.DataBindY(yValue);
            Chart13.Series["Series1"].Points[0].Color = Color.Green;

            int cntr = 0;
            int pxacc050cntr = 0;
            int pxacc048cntr = 0;
            int pxacc050end = 0;
            int pxacc048end = 0;
            //  SqlDataReader myReader = null;
            String sqlst = "SELECT Jobname, End_Time  FROM BatchData WHERE Jobname IN ('PXACC048','PXACC050') AND RecordDt = @ddate" ;

            //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            try
            {

                objCommand.CommandType = CommandType.Text;
             //   objCommand.Parameters.AddWithValue("@name", "PXACC048");
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
                        // Check for Red flag. If PXACC048 has not ended that means status is red
                        if (myReader.GetString(0).Trim() == "PXACC048")
                        {
                            pxacc048cntr = 1;
                            if ((myReader.GetValue(1) is DBNull) || (myReader.GetInt32(1) == 0))
                            {
                                pxacc048end = 1;
                            }
                            
                        }


                        // Check for Amber flag. If PXACC048 has ended and PXACC050 is still running then colour is amber

                        if (myReader.GetString(0).Trim() == "PXACC050")
                        {
                            pxacc050cntr =  1;
                            if ((myReader.GetValue(1) is DBNull) || (myReader.GetInt32(1) == 0))
                            {
                                pxacc050end = 1;
                            }
   
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

            //if pxacc048 row exists and 
            if ((pxacc048cntr == 1) && (pxacc048end == 1))
            {
                Chart13.Series["Series1"].Points.DataBindY(yValue);
                Chart13.Series["Series1"].Points[0].Color = Color.Red;
            }
            if (((pxacc050cntr == 1) && (pxacc050end == 1)) && ((pxacc048cntr == 1) && (pxacc048end == 0)))
            {
                Chart13.Series["Series1"].Points.DataBindY(yValue);
                Chart13.Series["Series1"].Points[0].Color = Color.DarkOrange;
            }
            if ((pxacc050cntr == 1) && (pxacc048cntr == 1) && (pxacc048end == 0) && (pxacc050end == 0))
            {
                Chart13.Series["Series1"].Points.DataBindY(yValue);
                Chart13.Series["Series1"].Points[0].Color = Color.Green;
            }
            if (cntr == 0)
            {
                //tiflag = 3;
                Chart13.Series["Series1"].Points.DataBindY(yValue);
                Chart13.Series["Series1"].Points[0].Color = Color.Gray;
            }
            else
            {
                Chart13.Series["Series1"].Points[0].ToolTip = "Click to View RAG staus details";
                Chart13.Series["Series1"].Points[0].Url = "Comments.aspx?detail=BATCHS" + "&date=" + seldate1.Text;
            }

            closedb();




        }

        private void getCplStatus()
        {
            opendb();
            // int[] yValue = { 1 };
            // set for green flag 

            //Chart12.Series["Series1"].Points.DataBindY(yValue);
            //Chart12.Series["Series1"].Points[0].Color = Color.Green;

            Chart12.Series["Series1"].Points.DataBindY(cValue);
            Chart12.Series["Series1"].Points[0].Color = Color.Green;


            // int cntr = 0;
            cplcntr = 0;
            //  SqlDataReader myReader = null;
            //String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

            ////String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
            //SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            //try
            //{
               
            //    objCommand.CommandType = CommandType.Text;
            //    objCommand.Parameters.AddWithValue("@ttype1", "CPL");
            //    objCommand.Parameters.AddWithValue("@ddate", currentdate);
            //    if (myConnection != null && myConnection.State == ConnectionState.Closed)
            //    {
            //        objCommand.Connection.Open();

            //    }


            //    SqlDataReader myReader = objCommand.ExecuteReader();



            //    //}
            //    if (myReader.HasRows)
            //    {
            //        while (myReader.Read())
            //        {
            //            cntr = cntr + 1;
            //            // Check for Amber flag
            //            if ((myReader.GetString(0).Trim() == "CPL") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
            //                & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
            //            {

            //             //   cplflag = 2;
            //                Chart12.Series["Series1"].Points.DataBindY(yValue);
            //                Chart12.Series["Series1"].Points[0].Color = Color.DarkOrange;

            //            }


            //            // Check for Red flag

            //            if ((myReader.GetString(0).Trim() == "CPL") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
            //            {
            //               // cplflag = 0;
            //                Chart12.Series["Series1"].Points.DataBindY(yValue);
            //                Chart12.Series["Series1"].Points[0].Color = Color.Red;

            //            }




            //        }

            //        myReader.Close();
            //    }
            //    else
            //    {
            //        myReader.Close();
            //    }

            //    if (myConnection != null && myConnection.State == ConnectionState.Open)
            //    {
            //        objCommand.Connection.Close();

            //    }


            //}
            //catch (SqlException SqlException)
            //{

            //    Response.Write(SqlException.Message);
            //    throw SqlException;
            //}

            // Check for Gray flag - means no data 
           

            //if (cntr == 0)
            //{
            //    //  cplflag = 3;
            //    Chart12.Series["Series1"].Points.DataBindY(yValue);
            //    Chart12.Series["Series1"].Points[0].Color = Color.Gray;
            //}
            
            CheckCplStgoAmber();
            CheckCplStgtAmber();
            CheckCplTwoAmber();

            CheckCplStgoRed();
            CheckCplStgtRed();
            CheckCplTwoRed();

            // Check for Gray flag - means no data 

            if (cplcntr == 0)
            {
                //  cplflag = 3;
                Chart12.Series["Series1"].Points.DataBindY(cValue);
                Chart12.Series["Series1"].Points[0].Color = Color.Gray;
            }
            else
            {
                Chart12.Series["Series1"].Points[0].ToolTip = "Click to View RAG staus details";
                Chart12.Series["Series1"].Points[0].Url = "Comments.aspx?detail=CPL" + "&date=" + seldate1.Text;
            }


            closedb();
        }

        private void CheckCplTwoRed()
        {
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
                        cplcntr = cplcntr + 1;
                        // Check for CPL1 Stage1 Amber flag
                        //if ((myReader.GetString(0).Trim() == "CPLSTGO") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                        //    & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                        //{

                        //    //   cplflag = 2;
                        //    Chart12.Series["Series1"].Points.DataBindY(cValue);
                        //    Chart12.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        //}


                        // Check for Red flag

                        if ((myReader.GetString(0).Trim() == "CPLTWO") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                        {
                            // cplflag = 0;
                            Chart12.Series["Series1"].Points.DataBindY(cValue);
                            Chart12.Series["Series1"].Points[0].Color = Color.Red;

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
        }

        private void CheckCplStgtRed()
        {
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
                        cplcntr = cplcntr + 1;
                        // Check for CPL1 Stage1 Amber flag
                        //if ((myReader.GetString(0).Trim() == "CPLSTGO") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                        //    & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                        //{

                        //    //   cplflag = 2;
                        //    Chart12.Series["Series1"].Points.DataBindY(cValue);
                        //    Chart12.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        //}


                        // Check for Red flag

                        if ((myReader.GetString(0).Trim() == "CPLSTGT") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                        {
                            // cplflag = 0;
                            Chart12.Series["Series1"].Points.DataBindY(cValue);
                            Chart12.Series["Series1"].Points[0].Color = Color.Red;

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
        }

        private void CheckCplTwoAmber()
        {
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
                        cplcntr = cplcntr + 1;
                        // Check for CPL2 Amber flag
                        if ((myReader.GetString(0).Trim() == "CPLTWO") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                            & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                        {

                            //   cplflag = 2;
                            Chart12.Series["Series1"].Points.DataBindY(cValue);
                            Chart12.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        }


                        // Check for Red flag

                        //if ((myReader.GetString(0).Trim() == "CPLSTGO") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                        //{
                        //    // cplflag = 0;
                        //    Chart12.Series["Series1"].Points.DataBindY(cValue);
                        //    Chart12.Series["Series1"].Points[0].Color = Color.Red;

                        //}




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

        }

        private void CheckCplStgtAmber()
        {
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
                        cplcntr = cplcntr + 1;
                        // Check for CPL1 Stage2 Amber flag
                        if ((myReader.GetString(0).Trim() == "CPLSTGT") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                            & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                        {

                            //   cplflag = 2;
                            Chart12.Series["Series1"].Points.DataBindY(cValue);
                            Chart12.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        }


                        // Check for Red flag

                        //if ((myReader.GetString(0).Trim() == "CPLSTGO") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                        //{
                        //    // cplflag = 0;
                        //    Chart12.Series["Series1"].Points.DataBindY(cValue);
                        //    Chart12.Series["Series1"].Points[0].Color = Color.Red;

                        //}




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

        }

        private void CheckCplStgoRed()
        {
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
                        cplcntr = cplcntr + 1;
                        // Check for CPL1 Stage1 Amber flag
                        //if ((myReader.GetString(0).Trim() == "CPLSTGO") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                        //    & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                        //{

                        //    //   cplflag = 2;
                        //    Chart12.Series["Series1"].Points.DataBindY(cValue);
                        //    Chart12.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        //}


                        // Check for Red flag

                        if ((myReader.GetString(0).Trim() == "CPLSTGO") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                        {
                            // cplflag = 0;
                            Chart12.Series["Series1"].Points.DataBindY(cValue);
                            Chart12.Series["Series1"].Points[0].Color = Color.Red;

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

        }

        private void CheckCplStgoAmber()
        {
           
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
                        cplcntr = cplcntr + 1;
                        // Check for CPL1 Stage1 Amber flag
                        if ((myReader.GetString(0).Trim() == "CPLSTGO") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                            & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                        {

                            //   cplflag = 2;
                            Chart12.Series["Series1"].Points.DataBindY(cValue);
                            Chart12.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        }


                        // Check for Red flag

                        //if ((myReader.GetString(0).Trim() == "CPLSTGO") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                        //{
                        //    // cplflag = 0;
                        //    Chart12.Series["Series1"].Points.DataBindY(cValue);
                        //    Chart12.Series["Series1"].Points[0].Color = Color.Red;

                        //}




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

           
        }

        private void getTIStatus()
        {
            opendb();

            int[] yValue = { 1 };
            // set for green flag 

            Chart11.Series["Series1"].Points.DataBindY(yValue);
            Chart11.Series["Series1"].Points[0].Color = Color.Green;

            int cntr = 0;
            //  SqlDataReader myReader = null;
            String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

            //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            try
            {
                
                objCommand.CommandType = CommandType.Text;
                objCommand.Parameters.AddWithValue("@ttype1", "TI");
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
                        if ((myReader.GetString(0).Trim() == "TI") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                            & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                        {

                           // tiflag = 2;
                            Chart11.Series["Series1"].Points.DataBindY(yValue);
                            Chart11.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        }


                        // Check for Red flag

                        if ((myReader.GetString(0).Trim() == "TI") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                        {
                           //   tiflag = 0;
                            Chart11.Series["Series1"].Points.DataBindY(yValue);
                            Chart11.Series["Series1"].Points[0].Color = Color.Red;

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

            if (cntr == 0)
            {
                //tiflag = 3;
                Chart11.Series["Series1"].Points.DataBindY(yValue);
                Chart11.Series["Series1"].Points[0].Color = Color.Gray;
            }
            else
            {
                Chart11.Series["Series1"].Points[0].ToolTip = "Click to View RAG staus details";
                Chart11.Series["Series1"].Points[0].Url = "Comments.aspx?detail=TI" + "&date=" + seldate1.Text;
            }

            closedb();

        }

        private void getAnolStatus()
        {
            opendb();

            int[] yValue = { 1 };
            // set for green flag 

            Chart10.Series["Series1"].Points.DataBindY(yValue);
            Chart10.Series["Series1"].Points[0].Color = Color.Green;

            int cntr = 0;
            //  SqlDataReader myReader = null;
            String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

            //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            try
            {
                
                objCommand.CommandType = CommandType.Text;
                objCommand.Parameters.AddWithValue("@ttype1", "ANOL");
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
                        if ((myReader.GetString(0).Trim() == "ANOL") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                            & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                        {

                           // anolflag = 2;
                            Chart10.Series["Series1"].Points.DataBindY(yValue);
                            Chart10.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        }


                        // Check for Red flag

                        if ((myReader.GetString(0).Trim() == "ANOL") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                        {
                           // anolflag = 0;
                            Chart10.Series["Series1"].Points.DataBindY(yValue);
                            Chart10.Series["Series1"].Points[0].Color = Color.Red;

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

            if (cntr == 0)
            {
                //  anolflag = 3;
                Chart10.Series["Series1"].Points.DataBindY(yValue);
                Chart10.Series["Series1"].Points[0].Color = Color.Gray;
            }
            else
            {
                Chart10.Series["Series1"].Points[0].ToolTip = "Click to View RAG staus details";
                Chart10.Series["Series1"].Points[0].Url = "Comments.aspx?detail=ANOL" + "&date=" + seldate1.Text;
            }
            closedb();

        }

        private void getEmailStatus()
        {
            opendb();

            int[] yValue = { 1 };
            // set for green flag 

            Chart9.Series["Series1"].Points.DataBindY(yValue);
            Chart9.Series["Series1"].Points[0].Color = Color.Green;


            int cntr = 0;
            //  SqlDataReader myReader = null;
            String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

            //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            try
            {
                
                objCommand.CommandType = CommandType.Text;
                objCommand.Parameters.AddWithValue("@ttype1", "EMAIL");
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
                        if ((myReader.GetString(0).Trim() == "EMAIL") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                            & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                        {

                          //  emailflag = 2;

                            Chart9.Series["Series1"].Points.DataBindY(yValue);
                            Chart9.Series["Series1"].Points[0].Color = Color.DarkOrange;


                        }


                        // Check for Red flag

                        if ((myReader.GetString(0).Trim() == "EMAIL") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                        {
                           // emailflag = 0;

                            Chart9.Series["Series1"].Points.DataBindY(yValue);
                            Chart9.Series["Series1"].Points[0].Color = Color.Red;


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

            if (cntr == 0)
            {
                // emailflag = 3;
                Chart9.Series["Series1"].Points.DataBindY(yValue);
                Chart9.Series["Series1"].Points[0].Color = Color.Gray;

            }
            else
            {
                Chart9.Series["Series1"].Points[0].ToolTip = "Click to View RAG staus details";
                Chart9.Series["Series1"].Points[0].Url = "Comments.aspx?detail=EMAIL" + "&date=" + seldate1.Text;
            }

            closedb();

        }

        private void getGfifStatus()
        {
            opendb();

            int[] yValue = { 1 };
            // set for green flag 

            Chart8.Series["Series1"].Points.DataBindY(yValue);
            Chart8.Series["Series1"].Points[0].Color = Color.Green;

            int cntr = 0;
            //  SqlDataReader myReader = null;
            String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

            //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            try
            {
                
                objCommand.CommandType = CommandType.Text;
                objCommand.Parameters.AddWithValue("@ttype1", "GFIF");
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
                        if ((myReader.GetString(0).Trim() == "GFIF") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                            & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                        {

                           // gfifflag = 2;
                            Chart8.Series["Series1"].Points.DataBindY(yValue);
                            Chart8.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        }


                        // Check for Red flag

                        if ((myReader.GetString(0).Trim() == "GFIF") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                        {
                          //  gfifflag = 0;
                            Chart8.Series["Series1"].Points.DataBindY(yValue);
                            Chart8.Series["Series1"].Points[0].Color = Color.Red;

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

            if (cntr == 0)
            {
                // gfifflag = 3;
                Chart8.Series["Series1"].Points.DataBindY(yValue);
                Chart8.Series["Series1"].Points[0].Color = Color.Gray;
            }
            else
            {
                Chart8.Series["Series1"].Points[0].ToolTip = "Click to View RAG staus details";
                Chart8.Series["Series1"].Points[0].Url = "Comments.aspx?detail=GFIF" + "&date=" + seldate1.Text;
            }

            closedb();

        }

        private void getRenewalStatus()
        {
            opendb();

            int[] yValue = { 1 };
            // set for green flag 

            Chart7.Series["Series1"].Points.DataBindY(yValue);
            Chart7.Series["Series1"].Points[0].Color = Color.Green;

            int cntr = 0;
            //  SqlDataReader myReader = null;
            String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Failed, datatable.Unprocessed,  limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

            //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            try
            {

                objCommand.CommandType = CommandType.Text;
                objCommand.Parameters.AddWithValue("@ttype1", "RENEWAL");
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
                        //Decode percentage thresholds
                        rnwl_failed_per = Convert.ToInt32((myReader.GetInt32(4) * 100) / myReader.GetInt32(2));
                        rnwl_unprocess_per = Convert.ToInt32((myReader.GetInt32(3) * 100) / myReader.GetInt32(2));

                        // Check for Amber flag
                        if ((myReader.GetString(0).Trim() == "RENEWAL") & (((rnwl_failed_per <= myReader.GetInt32(5)) & (rnwl_unprocess_per <= myReader.GetInt32(6)))
                            & ((rnwl_failed_per >= myReader.GetInt32(7)) | (rnwl_unprocess_per >= myReader.GetInt32(8)))))
                        {

                           // rnwlflag = 2;
                            Chart7.Series["Series1"].Points.DataBindY(yValue);
                            Chart7.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        }


                        // Check for Red flag

                        if ((myReader.GetString(0).Trim() == "RENEWAL") & ((rnwl_failed_per > myReader.GetInt32(5)) | (rnwl_unprocess_per > myReader.GetInt32(6))))
                        {
                           // rnwlflag = 0;
                            Chart7.Series["Series1"].Points.DataBindY(yValue);
                            Chart7.Series["Series1"].Points[0].Color = Color.Red;

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

            if (cntr == 0)
            {
                //  rnwlflag = 3;
                Chart7.Series["Series1"].Points.DataBindY(yValue);
                Chart7.Series["Series1"].Points[0].Color = Color.Gray;
            }
            else

            {
                Chart7.Series["Series1"].Points[0].ToolTip = "Click to View RAG staus details";
                Chart7.Series["Series1"].Points[0].Url = "Comments.aspx?detail=RENEWAL" + "&date=" + seldate1.Text;
            }

            closedb();


        }

        private void getSepsStatus()
        {
            opendb();

            int[] yValue = { 1 };
            // set for green flag 

            Chart6.Series["Series1"].Points.DataBindY(yValue);
            Chart6.Series["Series1"].Points[0].Color = Color.Green;

            int cntr = 0;
            //  SqlDataReader myReader = null;
            String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

            //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            try
            {
               
                objCommand.CommandType = CommandType.Text;
                objCommand.Parameters.AddWithValue("@ttype1", "SEPS");
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
                        if ((myReader.GetString(0).Trim() == "SEPS") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                            & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                        {

                          //  sepsflag = 2;
                            Chart6.Series["Series1"].Points.DataBindY(yValue);
                            Chart6.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        }


                        // Check for Red flag

                        if ((myReader.GetString(0).Trim() == "SEPS") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                        {
                           // sepsflag = 0;
                            Chart6.Series["Series1"].Points.DataBindY(yValue);
                            Chart6.Series["Series1"].Points[0].Color = Color.Red;

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

            if (cntr == 0)
            {
                // sepsflag = 3;
                Chart6.Series["Series1"].Points.DataBindY(yValue);
                Chart6.Series["Series1"].Points[0].Color = Color.Gray;
            }
            else
            {
                Chart6.Series["Series1"].Points[0].ToolTip = "Click to View RAG staus details";
                Chart6.Series["Series1"].Points[0].Url = "Comments.aspx?detail=SEPS" + "&date=" + seldate1.Text;
            }
            closedb();

        }


        private void getCardStatus()
        {
            opendb();

            int[] yValue = { 1 };
            // set for green flag 

            Chart5.Series["Series1"].Points.DataBindY(yValue);
            Chart5.Series["Series1"].Points[0].Color = Color.Green;

            int cntr = 0;
            //  SqlDataReader myReader = null;
            String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

            //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            try
            {
                
                objCommand.CommandType = CommandType.Text;
                objCommand.Parameters.AddWithValue("@ttype1", "CARD");
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
                        //Decode percentage thresholds
                        card_failed_per = Convert.ToInt32((myReader.GetInt32(4)*100) / myReader.GetInt32(2));
                   //     card_unprocess_per = Convert.ToInt32((myReader.GetInt32(3)*100) / myReader.GetInt32(1));
                        
                        // Check for Amber flag
                        if ((myReader.GetString(0).Trim() == "CARD") & (((card_failed_per <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                            & ((card_failed_per > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                        {

                           // cardflag = 2;
                            Chart5.Series["Series1"].Points.DataBindY(yValue);
                            Chart5.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        }


                        // Check for Red flag

                        if ((myReader.GetString(0).Trim() == "CARD") & ((card_failed_per > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                        {
                            //cardflag = 0;
                            Chart5.Series["Series1"].Points.DataBindY(yValue);
                            Chart5.Series["Series1"].Points[0].Color = Color.Red;

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

            if (cntr == 0)
            {
                // cardflag = 3;
                Chart5.Series["Series1"].Points.DataBindY(yValue);
                Chart5.Series["Series1"].Points[0].Color = Color.Gray;
            }
            else
            {
                Chart5.Series["Series1"].Points[0].ToolTip = "Click to View RAG staus details";
                Chart5.Series["Series1"].Points[0].Url = "Comments.aspx?detail=CARD" + "&date=" + seldate1.Text;
            }
            closedb();

        }

        private void getWamiStatus()
        {
            opendb();
            int[] yValue = { 1 };
            // set for green flag 

            Chart4.Series["Series1"].Points.DataBindY(yValue);
            Chart4.Series["Series1"].Points[0].Color = Color.Green;

            int cntr = 0;
            //  SqlDataReader myReader = null;
            String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

            //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
            SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
            try
            {
                
                objCommand.CommandType = CommandType.Text;
                objCommand.Parameters.AddWithValue("@ttype1", "WAMI");
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
                        if ((myReader.GetString(0).Trim() == "WAMI") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                            & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                        {

                           // wamiflag = 2;

                            Chart4.Series["Series1"].Points.DataBindY(yValue);
                            Chart4.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        }


                        // Check for Red flag

                        if ((myReader.GetString(0).Trim() == "WAMI") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                        {
                           // wamiflag = 0;
                            Chart4.Series["Series1"].Points.DataBindY(yValue);
                            Chart4.Series["Series1"].Points[0].Color = Color.Red;

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

            if (cntr == 0)
            {
                //  wamiflag = 3;
                Chart4.Series["Series1"].Points.DataBindY(yValue);
                Chart4.Series["Series1"].Points[0].Color = Color.Gray;
            }
            else
            {
                Chart4.Series["Series1"].Points[0].ToolTip = "Click to View RAG staus details";
                Chart4.Series["Series1"].Points[0].Url = "Comments.aspx?detail=WAMI" + "&date=" + seldate1.Text;
            }

            closedb();
        }


        private void getOracleStatus()
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
                objCommand.Parameters.AddWithValue("@ttype1", "ORACLE");
                objCommand.Parameters.AddWithValue("@ddate", currentdate);
                if (myConnection != null && myConnection.State == ConnectionState.Closed)
                {
                    objCommand.Connection.Open();

                }


                SqlDataReader myReader = objCommand.ExecuteReader();



              
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        cntr = cntr + 1;
                        // Check for Amber flag
                        if ((myReader.GetString(0).Trim() == "ORACLE") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                            & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                        {

                           // oracleflag = 2;
                            Chart3.Series["Series1"].Points.DataBindY(yValue);
                            Chart3.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        }


                        // Check for Red flag

                        if ((myReader.GetString(0).Trim() == "ORACLE") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                        {
                          //  oracleflag = 0;
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

            if (cntr == 0)
            {
                // oracleflag = 3;
                Chart3.Series["Series1"].Points.DataBindY(yValue);
                Chart3.Series["Series1"].Points[0].Color = Color.Gray;
            }
            else
            {
                Chart3.Series["Series1"].Points[0].ToolTip = "Click to View RAG staus details";
                Chart3.Series["Series1"].Points[0].Url = "Comments.aspx?detail=ORACLE" + "&date=" + seldate1.Text;
            }

            closedb();

        }

        private void getPitsStatus()
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
                objCommand.Parameters.AddWithValue("@ttype1", "PITS");
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
                        if ((myReader.GetString(0).Trim() == "PITS") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                            & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                        {

                          //  pitsflag = 2;
                            Chart2.Series["Series1"].Points.DataBindY(yValue);
                            Chart2.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        }


                        // Check for Red flag

                        if ((myReader.GetString(0).Trim() == "PITS") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                        {
                           // pitsflag = 0;
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

            if (cntr == 0)
            {
                //pitsflag = 3;
                Chart2.Series["Series1"].Points.DataBindY(yValue);
                Chart2.Series["Series1"].Points[0].Color = Color.Gray;
            }
            else
            {
                Chart2.Series["Series1"].Pointprintstas[0].ToolTip = "Click to View RAG staus details";
                Chart2.Series["Series1"].Points[0].Url = "Comments.aspx?detail=PITS" + "&date=" + seldate1.Text;
            }
            closedb();

        }

        private void getPrintStatus()
        {
            opendb();

            currentdate = Convert.ToString(System.Web.HttpContext.Current.Session["date"]);
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
                objCommand.Parameters.AddWithValue("@ttype1", "PRINT");
                //objCommand.Parameters.AddWithValue("@ttype2", "PRNS");
                //objCommand.Parameters.AddWithValue("@ttype3", "PRNO");
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
                        if ((myReader.GetString(0).Trim() == "PRINT") &
                            (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                            & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                        {

                       //     printflag = 2;
                            
                            Chart1.Series["Series1"].Points.DataBindY(yValue);
                            Chart1.Series["Series1"].Points[0].Color = Color.DarkOrange;

                        }

                        // Check for Red flag

                        if ((myReader.GetString(0).Trim() == "PRINT") &
                            ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                        {
                          //  printflag = 0;
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
            Chart1.Series["Series1"].Points[0].ToolTip = "Click to View RAG staus details";
            Chart1.Series["Series1"].Points[0].Url = "Comments.aspx?detail=PRINT" + "&date=" + seldate1.Text;

            if (cntr == 0)
            {
                // printflag = 3;
                Chart1.Series["Series1"].Points.DataBindY(yValue);
                Chart1.Series["Series1"].Points[0].Color = Color.Gray;

            }
            else
            {
                Chart1.Series["Series1"].Points[0].ToolTip = "Click to View RAG staus details";
                Chart1.Series["Series1"].Points[0].Url = "Comments.aspx?detail=PRINT" + "&date=" + seldate1.Text;
                
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
       
        protected void graph_Click(object sender, EventArgs e)
        {
            Session["graph"] = "PRINT";
            Session["date"] = seldate1.Text;
            Response.Redirect("Graph.aspx");

        }

        protected void detail_Click(object sender, EventArgs e)
        {
            Session["detail"] = "PRINT";
            Session["date"] = seldate1.Text;
            Response.Redirect("Detail.aspx");

        }

        protected void batch_Click(object sender, EventArgs e)
        {
            Session["detail"] = "BATCH";
            Session["date"] = seldate1.Text;
            Response.Redirect("BatchDetail.aspx");

            

        }

        protected void datafeed_Click(object sender, EventArgs e)
        {
            Session["detail"] = "DATAFEED";
            Session["date"] = seldate1.Text;
            Response.Redirect("feeddetail.aspx");

        }

        protected void bp1_Click(object sender, EventArgs e)
        {
            Session["detail"] = "BATCHPERF";
            Session["date"] = seldate1.Text;
            Response.Redirect("BatchPDetail.aspx");

        }

        protected void halerr_Click(object sender, EventArgs e)
        {
            Session["detail"] = "HAL ERROR";
            Session["date"] = seldate1.Text;
            Response.Redirect("HalDetail.aspx");
        }

        protected void interfacetrigger_Click(object sender, EventArgs e)
        {
            Session["detail"] = "Interface Triggers";
            Session["date"] = seldate1.Text;
            Response.Redirect("interfacedetail.aspx");
        }

        protected void cpldetail_Click(object sender, EventArgs e)
        {
            Session["detail"] = "CPL";
            Session["date"] = seldate1.Text;
            Response.Redirect("cpldetail.aspx");

        }

        protected void cpl_Click(object sender, EventArgs e)
        {
            Session["graph"] = "CPL";
            Session["date"] = seldate1.Text;
            Response.Redirect("Graph.aspx");

        }

        protected void anoldetail_Click(object sender, EventArgs e)
        {
            Session["detail"] = "ANOL";
            Session["date"] = seldate1.Text;
            Response.Redirect("Detail.aspx");

        }

        protected void anol_Click(object sender, EventArgs e)
        {
            Session["graph"] = "ANOL";
            Session["date"] = seldate1.Text;
            Response.Redirect("Graph.aspx");
        }

        protected void emaildetail_Click(object sender, EventArgs e)
        {
            Session["detail"] = "EMAIL";
            Session["date"] = seldate1.Text;
            Response.Redirect("Detail.aspx");

        }

        protected void email_Click(object sender, EventArgs e)
        {
            Session["graph"] = "EMAIL";
            Session["date"] = seldate1.Text;
            Response.Redirect("Graph.aspx");

        }

        protected void gfifdetail_Click(object sender, EventArgs e)
        {
            Session["detail"] = "GFIF";
            Session["date"] = seldate1.Text;
            Response.Redirect("Detail.aspx");

        }

        protected void gfif_Click(object sender, EventArgs e)
        {
            Session["graph"] = "GFIF";
            Session["date"] = seldate1.Text;
            Response.Redirect("Graph.aspx");

        }

        protected void rnwldetail_Click(object sender, EventArgs e)
        {
            Session["detail"] = "RENEWAL";
            Session["date"] = seldate1.Text;
            Response.Redirect("Detail1.aspx");

        }

        protected void rnwl_Click(object sender, EventArgs e)
        {
            Session["graph"] = "RENEWAL";
            Session["date"] = seldate1.Text;
            Response.Redirect("Graphrnwl.aspx");

        }

        protected void sepsdetail_Click(object sender, EventArgs e)
        {
            Session["detail"] = "SEPS";
            Session["date"] = seldate1.Text;
            Response.Redirect("Detail.aspx");

        }

        protected void seps_Click(object sender, EventArgs e)
        {
            Session["graph"] = "SEPS";
            Session["date"] = seldate1.Text;
            Response.Redirect("Graph.aspx");

        }

        protected void carddetail_Click(object sender, EventArgs e)
        {
            Session["detail"] = "CARD";
            Session["date"] = seldate1.Text;
            Response.Redirect("Detail2.aspx");

        }

        protected void card_Click(object sender, EventArgs e)
        {
            Session["graph"] = "CARD";
            Session["date"] = seldate1.Text;
            Response.Redirect("Graph.aspx");

        }

        protected void wamidetail_Click(object sender, EventArgs e)
        {
            Session["detail"] = "WAMI";
            Session["date"] = seldate1.Text;
            Response.Redirect("Detail.aspx");

        }

        protected void wami_Click(object sender, EventArgs e)
        {
            Session["graph"] = "WAMI";
            Session["date"] = seldate1.Text;
            Response.Redirect("Graph.aspx");

        }

        protected void oracdetail_Click(object sender, EventArgs e)
        {
            Session["detail"] = "ORACLE";
            Session["date"] = seldate1.Text;
            Response.Redirect("Detail.aspx");
        }

        protected void orac_Click(object sender, EventArgs e)
        {
            Session["graph"] = "ORACLE";
            Session["date"] = seldate1.Text;
            Response.Redirect("Graph.aspx");
        }

        protected void pitsdetail_Click(object sender, EventArgs e)
        {
            Session["detail"] = "PITS";
            Session["date"] = seldate1.Text;
            Response.Redirect("Detail.aspx");

        }

        protected void pits_Click(object sender, EventArgs e)
        {
            Session["graph"] = "PITS";
            Session["date"] = seldate1.Text;
            Response.Redirect("Graph.aspx");

        }

        protected void ti_Click(object sender, EventArgs e)
        {
            Session["graph"] = "TI";
            Session["date"] = seldate1.Text;
            Response.Redirect("Graph.aspx");
        }

        protected void tidetail_Click(object sender, EventArgs e)
        {
            Session["detail"] = "TI";
            Session["date"] = seldate1.Text;
            Response.Redirect("Detail.aspx");

        }

        protected void refresh_Click(object sender, EventArgs e)
        {
           
            Session["date"] = Request.Form[seldate1.UniqueID];
          //  Session["date"] = seldate1.Text;
            Response.Redirect("datepage.aspx");
        }

        protected void mdm_Click(object sender, EventArgs e)
        {
            Session["graph"] = "MDM";
            Session["date"] = seldate1.Text;
            Response.Redirect("Graph.aspx");
        }

        protected void mdmdetail_Click(object sender, EventArgs e)
        {
            Session["detail"] = "MDM";
            Session["date"] = seldate1.Text;
            Response.Redirect("Detail.aspx");
        }

        protected void thresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void deferdetail_Click(object sender, EventArgs e)
        {
            Session["detail"] = "Defer Queue";
            Session["date"] = seldate1.Text;
            Response.Redirect("DeferDetail.aspx");

        }
        protected void Chart15_Load(object sender, EventArgs e)
        {

        }

        protected void seldate1_TextChanged(object sender, EventArgs e)
        {

        }

       
    }
}
