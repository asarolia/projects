using RunboardAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;

namespace RunboardAPI.Controllers
{
    
    public class TestController : ApiController
    {

        private RunboardAPIModel db = new RunboardAPIModel();

        //running OK
        //public IQueryable<FeedData> Get()
        //{
        //    return db.FeedDatas;

        //}

        // stored proc not working
        //public IQueryable<RetrievedashboardStatus_Result> Get()
        //{
        //    return db.RetrievedashboardStatus_Result;
        //}

        // stored proc not working 
        //public String Get()
        //{


        //    using (db)
        //    {
        //        using (db.Database.Connection)
        //        {
        //            DbDataReader reader = null;

        //            DbCommand cmd = db.Database.Connection.CreateCommand();
        //            cmd.CommandText = "[dbo].[RetrievedashboardStatus]";
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("ddate", DateTime.Parse("2015-07-28")));
        //            try
        //            {
        //                db.Database.Connection.Open();
        //                reader = cmd.ExecuteReader();

        //                string str = null;
        //                if (reader.HasRows)
        //                {



        //                    while (reader.Read())
        //                    {
        //                        str = reader.GetInt32(13).ToString();
        //                    }

        //                    return str;
        //                }
        //                else
        //                {
        //                    return "No data found";
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                return e.ToString();

        //            }
        //            finally
        //            {
        //                db.Database.Connection.Close();
        //            }
        //        }
        //    }
        //}

        // working OK to return JSON
        public IHttpActionResult Get()
        {
            string query = "SELECT SUM(Processed) AS ProcessedCount, SUM(Success) AS SuccessCount, SUM(Fail) AS FailCount, Failed_Threshold_Red AS FTR, Failed_Threshold_Amber AS FTA FROM feeddata_view WHERE Feed_Type = 'HSBC' AND RecordDt = '2015-07-28' GROUP BY Failed_Threshold_Red, Failed_Threshold_Amber";

            try
            {
                // working OK to return JSON
                var data = db.Database.SqlQuery<ConsolidatedFeed>(query);
                return Json(data);
            }
            catch (Exception e)
            {
                return Json(e);
            }

        }

        //public HttpResponseMessage Get()
        //{
        //    // SQL version of the above LINQ code.
        //    string query = "SELECT SUM(Processed) AS ProcessedCount, SUM(Success) AS SuccessCount, SUM(Fail) AS FailCount, Failed_Threshold_Red AS FTR, Failed_Threshold_Amber AS FTA FROM feeddata_view WHERE Feed_Type = 'HSBC' AND RecordDt = '2015-07-28' GROUP BY Failed_Threshold_Red, Failed_Threshold_Amber For XML RAW,ELEMENTS";

        //    try
        //    {
        //        // test to return XML now 

        //        var data = db.Database.SqlQuery<ConsolidatedFeed>(query);

        //        return new HttpResponseMessage(){Content = new StringContent(data.ToXML(), Encoding.UTF8, "application/xml")};

        //       // return data;
        //    }
        //    catch (Exception e)
        //    {
        //         return new HttpResponseMessage() { Content = new StringContent("Failed in DB opertaion", Encoding.UTF8, "application/xml") };
        //   //     return ("Error occurred"+e);
        //    }

        //}

    }
}
