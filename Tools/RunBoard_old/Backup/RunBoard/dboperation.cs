using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;
using System.Configuration;


namespace RunBoard
{
    public class dboperation
    {
        //red = 0
        //green = 1
        //amber = 2
        //gray = 3
        public string ScreenDt { get; set; }

     //   public String currentdate = DateTime.Now.Date.ToLongDateString();

        public String currentdate = Convert.ToString(System.Web.HttpContext.Current.Session["date"]);
       

        //connection string for local DB
        //System.Data.SqlClient.SqlConnection myConnection = new System.Data.SqlClient.SqlConnection("user id=ASIAPAC\\asarolia;" +
        //                                       "password=Cscindia@0314;server=CSCINDAE707743\\SQLEXPRESS;" +
        //                                       "Trusted_Connection=yes;" +
        //                                       "database=rundashboard; " +
        //                                       "connection timeout=30; MultipleActiveResultSets=true");

        // Connection string for Aviva server 
        //System.Data.SqlClient.SqlConnection myConnection = new System.Data.SqlClient.SqlConnection("Server=SQLD005C.via.novonet;Database=Aviva_BNC_Earth;User Id=Earth_user;Password=ZYnS46t6We;MultipleActiveResultSets=true");

        // To read from web config 

        public static string cs = ConfigurationManager.ConnectionStrings["rundashboardConnectionString"].ConnectionString;

        SqlConnection myConnection = new SqlConnection(cs);

        public void StoreSession(String s)
        {
            System.Web.HttpContext.Current.Session["date"] = s;  

        }
       
        

      public void opendb()
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

    // Function to get the status for PRINT (PRINT/PRNS/PRNO)
      public int getPrintStatus()
      {
          // set for green flag 
          int printflag = 1;
          int cntr = 0;
        //  SqlDataReader myReader = null;
          String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";
          //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
          SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
          try
          {
          //    String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type = @ttype AND Record_Date = @ddate";
          //    string sqlConnString = string.Empty;
          //    sqlConnString = ConfigurationManager.ConnectionStrings["ConnectString"].ConnectionString;

          //    sqlConnString = sqlConnString.Replace("$USERID", "ASIAPAC\\asarolia");
          //    sqlConnString = sqlConnString.Replace("$PASSWORD", "Cscindia@0314");
          //    sqlConnString = sqlConnString.Replace("$SERVER", "CSCINDAE707743\\SQLEXPRESS");
          //    sqlConnString = sqlConnString.Replace("$DBNAME", "rundashboard");

          //    using (SqlConnection conn = new SqlConnection(sqlConnString))
          //    {
                  //using (SqlCommand objCommand = new SqlCommand(sqlst, myConnection))
                  //{
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

                                 printflag = 2;

                             }

                             // Check for Red flag

                             if ((myReader.GetString(0).Trim() == "PRINT") &
                                 ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                             {
                                 printflag = 0;

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
              
              Trace.TraceWarning(SqlException.Message);
              throw SqlException;
          }

          // Check for Gray flag - means no data 

          if (cntr == 0)
          {
              printflag = 3;
          }

          return printflag;

      }

      // Function to get the status for Oracle AR processing

      public int getOracleStatus()
      {
          // set for green flag 
          int oracleflag = 1;
          int cntr = 0;
          //  SqlDataReader myReader = null;
          String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";
          //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
          SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
          try
          {
              //    String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type = @ttype AND Record_Date = @ddate";
              //    string sqlConnString = string.Empty;
              //    sqlConnString = ConfigurationManager.ConnectionStrings["ConnectString"].ConnectionString;

              //    sqlConnString = sqlConnString.Replace("$USERID", "ASIAPAC\\asarolia");
              //    sqlConnString = sqlConnString.Replace("$PASSWORD", "Cscindia@0314");
              //    sqlConnString = sqlConnString.Replace("$SERVER", "CSCINDAE707743\\SQLEXPRESS");
              //    sqlConnString = sqlConnString.Replace("$DBNAME", "rundashboard");

              //    using (SqlConnection conn = new SqlConnection(sqlConnString))
              //    {
              //using (SqlCommand objCommand = new SqlCommand(sqlst, myConnection))
              //{
              objCommand.CommandType = CommandType.Text;
              objCommand.Parameters.AddWithValue("@ttype1", "ORACLE");
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
                      if ((myReader.GetString(0).Trim() == "ORACLE") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                          & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                      {

                          oracleflag = 2;

                      }


                      // Check for Red flag

                      if ((myReader.GetString(0).Trim() == "ORACLE") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                      {
                          oracleflag = 0;

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

              Trace.TraceWarning(SqlException.Message);
              throw SqlException;
          }

          // Check for Gray flag - means no data 

          if (cntr == 0)
          {
              oracleflag = 3;
          }

          return oracleflag;

      }

      // Function to get the status for CARD processing

      public int getCardStatus()
      {
          // set for green flag 
          int cardflag = 1;
          int cntr = 0;
          //  SqlDataReader myReader = null;
          String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

          //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
          SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
          try
          {
              //    String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type = @ttype AND Record_Date = @ddate";
              //    string sqlConnString = string.Empty;
              //    sqlConnString = ConfigurationManager.ConnectionStrings["ConnectString"].ConnectionString;

              //    sqlConnString = sqlConnString.Replace("$USERID", "ASIAPAC\\asarolia");
              //    sqlConnString = sqlConnString.Replace("$PASSWORD", "Cscindia@0314");
              //    sqlConnString = sqlConnString.Replace("$SERVER", "CSCINDAE707743\\SQLEXPRESS");
              //    sqlConnString = sqlConnString.Replace("$DBNAME", "rundashboard");

              //    using (SqlConnection conn = new SqlConnection(sqlConnString))
              //    {
              //using (SqlCommand objCommand = new SqlCommand(sqlst, myConnection))
              //{
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
                      // Check for Amber flag
                      if ((myReader.GetString(0).Trim() == "CARD") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                          & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                      {

                          cardflag = 2;

                      }


                      // Check for Red flag

                      if ((myReader.GetString(0).Trim() == "CARD") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                      {
                          cardflag = 0;

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

              Trace.TraceWarning(SqlException.Message);
              throw SqlException;
          }

          // Check for Gray flag - means no data 

          if (cntr == 0)
          {
              cardflag = 3;
          }

          return cardflag;

      }

      // Function to get the status for Renewal processing

      public int getRenewalStatus()
      {
          // set for green flag 
          int rnwlflag = 1;
          int cntr = 0;
          //  SqlDataReader myReader = null;
          String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

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
                      // Check for Amber flag
                      if ((myReader.GetString(0).Trim() == "RENEWAL") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                          & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                      {

                          rnwlflag = 2;

                      }


                      // Check for Red flag

                      if ((myReader.GetString(0).Trim() == "RENEWAL") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                      {
                          rnwlflag = 0;

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

              Trace.TraceWarning(SqlException.Message);
              throw SqlException;
          }

          // Check for Gray flag - means no data 

          if (cntr == 0)
          {
              rnwlflag = 3;
          }

          return rnwlflag;

      }

      // Function to get the status for Email processing

      public int getEmailStatus()
      {
          // set for green flag 
          int emailflag = 1;
          int cntr = 0;
          //  SqlDataReader myReader = null;
          String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

          //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
          SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
          try
          {
              //    String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type = @ttype AND Record_Date = @ddate";
              //    string sqlConnString = string.Empty;
              //    sqlConnString = ConfigurationManager.ConnectionStrings["ConnectString"].ConnectionString;

              //    sqlConnString = sqlConnString.Replace("$USERID", "ASIAPAC\\asarolia");
              //    sqlConnString = sqlConnString.Replace("$PASSWORD", "Cscindia@0314");
              //    sqlConnString = sqlConnString.Replace("$SERVER", "CSCINDAE707743\\SQLEXPRESS");
              //    sqlConnString = sqlConnString.Replace("$DBNAME", "rundashboard");

              //    using (SqlConnection conn = new SqlConnection(sqlConnString))
              //    {
              //using (SqlCommand objCommand = new SqlCommand(sqlst, myConnection))
              //{
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

                          emailflag = 2;

                      }


                      // Check for Red flag

                      if ((myReader.GetString(0).Trim() == "EMAIL") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                      {
                          emailflag = 0;

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

              Trace.TraceWarning(SqlException.Message);
              throw SqlException;
          }

          // Check for Gray flag - means no data 

          if (cntr == 0)
          {
              emailflag = 3;
          }

          return emailflag;

      }

      // Function to get the status for Triple Interface 

      public int getTIStatus()
      {
          // set for green flag 
          int tiflag = 1;
          int cntr = 0;
          //  SqlDataReader myReader = null;
          String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

          //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
          SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
          try
          {
              //    String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type = @ttype AND Record_Date = @ddate";
              //    string sqlConnString = string.Empty;
              //    sqlConnString = ConfigurationManager.ConnectionStrings["ConnectString"].ConnectionString;

              //    sqlConnString = sqlConnString.Replace("$USERID", "ASIAPAC\\asarolia");
              //    sqlConnString = sqlConnString.Replace("$PASSWORD", "Cscindia@0314");
              //    sqlConnString = sqlConnString.Replace("$SERVER", "CSCINDAE707743\\SQLEXPRESS");
              //    sqlConnString = sqlConnString.Replace("$DBNAME", "rundashboard");

              //    using (SqlConnection conn = new SqlConnection(sqlConnString))
              //    {
              //using (SqlCommand objCommand = new SqlCommand(sqlst, myConnection))
              //{
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

                          tiflag = 2;

                      }


                      // Check for Red flag

                      if ((myReader.GetString(0).Trim() == "TI") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                      {
                          tiflag = 0;

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

              Trace.TraceWarning(SqlException.Message);
              throw SqlException;
          }

          // Check for Gray flag - means no data 

          if (cntr == 0)
          {
              tiflag = 3;
          }

          return tiflag;

      }

  // Function to get the status for PITS

      public int getPitsStatus()
      {
          // set for green flag 
          int pitsflag = 1;
          int cntr = 0;
          //  SqlDataReader myReader = null;
          String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

          //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
          SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
          try
          {
              //    String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type = @ttype AND Record_Date = @ddate";
              //    string sqlConnString = string.Empty;
              //    sqlConnString = ConfigurationManager.ConnectionStrings["ConnectString"].ConnectionString;

              //    sqlConnString = sqlConnString.Replace("$USERID", "ASIAPAC\\asarolia");
              //    sqlConnString = sqlConnString.Replace("$PASSWORD", "Cscindia@0314");
              //    sqlConnString = sqlConnString.Replace("$SERVER", "CSCINDAE707743\\SQLEXPRESS");
              //    sqlConnString = sqlConnString.Replace("$DBNAME", "rundashboard");

              //    using (SqlConnection conn = new SqlConnection(sqlConnString))
              //    {
              //using (SqlCommand objCommand = new SqlCommand(sqlst, myConnection))
              //{
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

                          pitsflag = 2;

                      }


                      // Check for Red flag

                      if ((myReader.GetString(0).Trim() == "PITS") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                      {
                          pitsflag = 0;

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

              Trace.TraceWarning(SqlException.Message);
              throw SqlException;
          }

          // Check for Gray flag - means no data 

          if (cntr == 0)
          {
              pitsflag = 3;
          }

          return pitsflag;

      }

      // Function to get the status for WAMI

      public int getWamiStatus()
      {
          // set for green flag 
          int wamiflag = 1;
          int cntr = 0;
          //  SqlDataReader myReader = null;
          String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

          //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
          SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
          try
          {
              //    String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type = @ttype AND Record_Date = @ddate";
              //    string sqlConnString = string.Empty;
              //    sqlConnString = ConfigurationManager.ConnectionStrings["ConnectString"].ConnectionString;

              //    sqlConnString = sqlConnString.Replace("$USERID", "ASIAPAC\\asarolia");
              //    sqlConnString = sqlConnString.Replace("$PASSWORD", "Cscindia@0314");
              //    sqlConnString = sqlConnString.Replace("$SERVER", "CSCINDAE707743\\SQLEXPRESS");
              //    sqlConnString = sqlConnString.Replace("$DBNAME", "rundashboard");

              //    using (SqlConnection conn = new SqlConnection(sqlConnString))
              //    {
              //using (SqlCommand objCommand = new SqlCommand(sqlst, myConnection))
              //{
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

                          wamiflag = 2;

                      }


                      // Check for Red flag

                      if ((myReader.GetString(0).Trim() == "WAMI") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                      {
                          wamiflag = 0;

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

              Trace.TraceWarning(SqlException.Message);
              throw SqlException;
          }

          // Check for Gray flag - means no data 

          if (cntr == 0)
          {
              wamiflag = 3;
          }

          return wamiflag;

      }

    // Function to get the status for SEPS

      public int getSepsStatus()
      {
          // set for green flag 
          int sepsflag = 1;
          int cntr = 0;
          //  SqlDataReader myReader = null;
          String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

          //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
          SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
          try
          {
              //    String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type = @ttype AND Record_Date = @ddate";
              //    string sqlConnString = string.Empty;
              //    sqlConnString = ConfigurationManager.ConnectionStrings["ConnectString"].ConnectionString;

              //    sqlConnString = sqlConnString.Replace("$USERID", "ASIAPAC\\asarolia");
              //    sqlConnString = sqlConnString.Replace("$PASSWORD", "Cscindia@0314");
              //    sqlConnString = sqlConnString.Replace("$SERVER", "CSCINDAE707743\\SQLEXPRESS");
              //    sqlConnString = sqlConnString.Replace("$DBNAME", "rundashboard");

              //    using (SqlConnection conn = new SqlConnection(sqlConnString))
              //    {
              //using (SqlCommand objCommand = new SqlCommand(sqlst, myConnection))
              //{
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

                          sepsflag = 2;

                      }


                      // Check for Red flag

                      if ((myReader.GetString(0).Trim() == "SEPS") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                      {
                          sepsflag = 0;

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

              Trace.TraceWarning(SqlException.Message);
              throw SqlException;
          }

          // Check for Gray flag - means no data 

          if (cntr == 0)
          {
              sepsflag = 3;
          }

          return sepsflag;

      }

      // Function to get the status for GFIF

      public int getGfifStatus()
      {
          // set for green flag 
          int gfifflag = 1;
          int cntr = 0;
          //  SqlDataReader myReader = null;
          String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

          //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
          SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
          try
          {
              //    String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type = @ttype AND Record_Date = @ddate";
              //    string sqlConnString = string.Empty;
              //    sqlConnString = ConfigurationManager.ConnectionStrings["ConnectString"].ConnectionString;

              //    sqlConnString = sqlConnString.Replace("$USERID", "ASIAPAC\\asarolia");
              //    sqlConnString = sqlConnString.Replace("$PASSWORD", "Cscindia@0314");
              //    sqlConnString = sqlConnString.Replace("$SERVER", "CSCINDAE707743\\SQLEXPRESS");
              //    sqlConnString = sqlConnString.Replace("$DBNAME", "rundashboard");

              //    using (SqlConnection conn = new SqlConnection(sqlConnString))
              //    {
              //using (SqlCommand objCommand = new SqlCommand(sqlst, myConnection))
              //{
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

                          gfifflag = 2;

                      }


                      // Check for Red flag

                      if ((myReader.GetString(0).Trim() == "GFIF") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                      {
                          gfifflag = 0;

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

              Trace.TraceWarning(SqlException.Message);
              throw SqlException;
          }

          // Check for Gray flag - means no data 

          if (cntr == 0)
          {
              gfifflag = 3;
          }

          return gfifflag;

      }


      // Function to get the status for ANOL

      public int getAnolStatus()
      {
          // set for green flag 
          int anolflag = 1;
          int cntr = 0;
          //  SqlDataReader myReader = null;
          String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

          //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
          SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
          try
          {
              //    String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type = @ttype AND Record_Date = @ddate";
              //    string sqlConnString = string.Empty;
              //    sqlConnString = ConfigurationManager.ConnectionStrings["ConnectString"].ConnectionString;

              //    sqlConnString = sqlConnString.Replace("$USERID", "ASIAPAC\\asarolia");
              //    sqlConnString = sqlConnString.Replace("$PASSWORD", "Cscindia@0314");
              //    sqlConnString = sqlConnString.Replace("$SERVER", "CSCINDAE707743\\SQLEXPRESS");
              //    sqlConnString = sqlConnString.Replace("$DBNAME", "rundashboard");

              //    using (SqlConnection conn = new SqlConnection(sqlConnString))
              //    {
              //using (SqlCommand objCommand = new SqlCommand(sqlst, myConnection))
              //{
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

                          anolflag = 2;

                      }


                      // Check for Red flag

                      if ((myReader.GetString(0).Trim() == "ANOL") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                      {
                          anolflag = 0;

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

              Trace.TraceWarning(SqlException.Message);
              throw SqlException;
          }

          // Check for Gray flag - means no data 

          if (cntr == 0)
          {
              anolflag = 3;
          }

          return anolflag;

      }

      // Function to get the status for CPL

      public int getCplStatus()
      {
          // set for green flag 
          int cplflag = 1;
          int cntr = 0;
          //  SqlDataReader myReader = null;
          String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

          //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
          SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
          try
          {
              //    String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type = @ttype AND Record_Date = @ddate";
              //    string sqlConnString = string.Empty;
              //    sqlConnString = ConfigurationManager.ConnectionStrings["ConnectString"].ConnectionString;

              //    sqlConnString = sqlConnString.Replace("$USERID", "ASIAPAC\\asarolia");
              //    sqlConnString = sqlConnString.Replace("$PASSWORD", "Cscindia@0314");
              //    sqlConnString = sqlConnString.Replace("$SERVER", "CSCINDAE707743\\SQLEXPRESS");
              //    sqlConnString = sqlConnString.Replace("$DBNAME", "rundashboard");

              //    using (SqlConnection conn = new SqlConnection(sqlConnString))
              //    {
              //using (SqlCommand objCommand = new SqlCommand(sqlst, myConnection))
              //{
              objCommand.CommandType = CommandType.Text;
              objCommand.Parameters.AddWithValue("@ttype1", "CPL");
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
                      if ((myReader.GetString(0).Trim() == "CPL") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                          & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                      {

                          cplflag = 2;

                      }


                      // Check for Red flag

                      if ((myReader.GetString(0).Trim() == "CPL") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                      {
                          cplflag = 0;

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

              Trace.TraceWarning(SqlException.Message);
              throw SqlException;
          }

          // Check for Gray flag - means no data 

          if (cntr == 0)
          {
              cplflag = 3;
          }

          return cplflag;

      }

 // Function to get the status for Batch

      public int getBatchStatus()
      {
          // set for green flag 
          int batchflag = 1;
          int cntr = 0;
          //  SqlDataReader myReader = null;
          String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

          //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
          SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
          try
          {
              //    String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type = @ttype AND Record_Date = @ddate";
              //    string sqlConnString = string.Empty;
              //    sqlConnString = ConfigurationManager.ConnectionStrings["ConnectString"].ConnectionString;

              //    sqlConnString = sqlConnString.Replace("$USERID", "ASIAPAC\\asarolia");
              //    sqlConnString = sqlConnString.Replace("$PASSWORD", "Cscindia@0314");
              //    sqlConnString = sqlConnString.Replace("$SERVER", "CSCINDAE707743\\SQLEXPRESS");
              //    sqlConnString = sqlConnString.Replace("$DBNAME", "rundashboard");

              //    using (SqlConnection conn = new SqlConnection(sqlConnString))
              //    {
              //using (SqlCommand objCommand = new SqlCommand(sqlst, myConnection))
              //{
              objCommand.CommandType = CommandType.Text;
              objCommand.Parameters.AddWithValue("@ttype1", "BATCH");
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
                      if ((myReader.GetString(0).Trim() == "BATCH") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                          & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                      {

                          batchflag = 2;

                      }


                      // Check for Red flag

                      if ((myReader.GetString(0).Trim() == "BATCH") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                      {
                          batchflag = 0;

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

              Trace.TraceWarning(SqlException.Message);
              throw SqlException;
          }

          // Check for Gray flag - means no data 

          if (cntr == 0)
          {
              batchflag = 3;
          }

          return batchflag;

      }

 // Function to get the status for DATAFEED

      public int getDatafeedStatus()
      {
          // set for green flag 
          int dataflag = 1;
          int cntr = 0;
          //  SqlDataReader myReader = null;
          String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

          //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
          SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
          try
          {
              //    String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type = @ttype AND Record_Date = @ddate";
              //    string sqlConnString = string.Empty;
              //    sqlConnString = ConfigurationManager.ConnectionStrings["ConnectString"].ConnectionString;

              //    sqlConnString = sqlConnString.Replace("$USERID", "ASIAPAC\\asarolia");
              //    sqlConnString = sqlConnString.Replace("$PASSWORD", "Cscindia@0314");
              //    sqlConnString = sqlConnString.Replace("$SERVER", "CSCINDAE707743\\SQLEXPRESS");
              //    sqlConnString = sqlConnString.Replace("$DBNAME", "rundashboard");

              //    using (SqlConnection conn = new SqlConnection(sqlConnString))
              //    {
              //using (SqlCommand objCommand = new SqlCommand(sqlst, myConnection))
              //{
              objCommand.CommandType = CommandType.Text;
              objCommand.Parameters.AddWithValue("@ttype1", "DATAFEED");
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
                      if ((myReader.GetString(0).Trim() == "DATAFEED") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                          & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                      {

                          dataflag = 2;

                      }


                      // Check for Red flag

                      if ((myReader.GetString(0).Trim() == "DATAFEED") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                      {
                          dataflag = 0;

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

              Trace.TraceWarning(SqlException.Message);
              throw SqlException;
          }

          // Check for Gray flag - means no data 

          if (cntr == 0)
          {
              dataflag = 3;
          }

          return dataflag;

      }

      // Function to get the status for INTERFACE

      public int getInterfaceStatus()
      {
          // set for green flag 
          int interflag = 1;
          int cntr = 0;
          //  SqlDataReader myReader = null;
          String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

          //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
          SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
          try
          {
              //    String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type = @ttype AND Record_Date = @ddate";
              //    string sqlConnString = string.Empty;
              //    sqlConnString = ConfigurationManager.ConnectionStrings["ConnectString"].ConnectionString;

              //    sqlConnString = sqlConnString.Replace("$USERID", "ASIAPAC\\asarolia");
              //    sqlConnString = sqlConnString.Replace("$PASSWORD", "Cscindia@0314");
              //    sqlConnString = sqlConnString.Replace("$SERVER", "CSCINDAE707743\\SQLEXPRESS");
              //    sqlConnString = sqlConnString.Replace("$DBNAME", "rundashboard");

              //    using (SqlConnection conn = new SqlConnection(sqlConnString))
              //    {
              //using (SqlCommand objCommand = new SqlCommand(sqlst, myConnection))
              //{
              objCommand.CommandType = CommandType.Text;
              objCommand.Parameters.AddWithValue("@ttype1", "INTERFACE");
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
                      if ((myReader.GetString(0).Trim() == "INTERFACE") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                          & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                      {

                          interflag = 2;

                      }


                      // Check for Red flag

                      if ((myReader.GetString(0).Trim() == "INTERFACE") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                      {
                          interflag = 0;

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

              Trace.TraceWarning(SqlException.Message);
              throw SqlException;
          }

          // Check for Gray flag - means no data 

          if (cntr == 0)
          {
              interflag = 3;
          }

          return interflag;

      }

 // Function to get the status for HALERR

      public int getHalerrStatus()
      {
          // set for green flag 
          int halerrflag = 1;
          int cntr = 0;
          //  SqlDataReader myReader = null;
          String sqlst = "SELECT datatable.Type, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @ttype1 AND datatable.RecordDt = @ddate";

          //String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type IN ('PRNT','PRNS','PRNO')";// AND Record_Date = '@ddate'";
          SqlCommand objCommand = new SqlCommand(sqlst, myConnection);
          try
          {
              //    String sqlst = "SELECT * FROM dbo.datatable WHERE Trigger_Type = @ttype AND Record_Date = @ddate";
              //    string sqlConnString = string.Empty;
              //    sqlConnString = ConfigurationManager.ConnectionStrings["ConnectString"].ConnectionString;

              //    sqlConnString = sqlConnString.Replace("$USERID", "ASIAPAC\\asarolia");
              //    sqlConnString = sqlConnString.Replace("$PASSWORD", "Cscindia@0314");
              //    sqlConnString = sqlConnString.Replace("$SERVER", "CSCINDAE707743\\SQLEXPRESS");
              //    sqlConnString = sqlConnString.Replace("$DBNAME", "rundashboard");

              //    using (SqlConnection conn = new SqlConnection(sqlConnString))
              //    {
              //using (SqlCommand objCommand = new SqlCommand(sqlst, myConnection))
              //{
              objCommand.CommandType = CommandType.Text;
              objCommand.Parameters.AddWithValue("@ttype1", "HALERR");
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
                      if ((myReader.GetString(0).Trim() == "HALERR") & (((myReader.GetInt32(4) <= myReader.GetInt32(5)) & (myReader.GetInt32(3) <= myReader.GetInt32(6)))
                          & ((myReader.GetInt32(4) > myReader.GetInt32(7)) | (myReader.GetInt32(3) > myReader.GetInt32(8)))))
                      {

                          halerrflag = 2;

                      }


                      // Check for Red flag

                      if ((myReader.GetString(0).Trim() == "HALERR") & ((myReader.GetInt32(4) > myReader.GetInt32(5)) | (myReader.GetInt32(3) > myReader.GetInt32(6))))
                      {
                          halerrflag = 0;

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

              Trace.TraceWarning(SqlException.Message);
              throw SqlException;
          }

          // Check for Gray flag - means no data 

          if (cntr == 0)
          {
              halerrflag = 3;
          }

          return halerrflag;

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