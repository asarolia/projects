using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;

namespace RunboardAPI.Models
{
    internal class EFInterceptor : IDbCommandInterceptor
    {
        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
          //  throw new NotImplementedException();
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
         //   throw new NotImplementedException();
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
          //  throw new NotImplementedException();
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            // Executes before fetching via EF

            //if (command.CommandText.Contains("FROM [dbo].[feeddata_view]"))
            //{
            //    //command.CommandText += " Where UnitsInstock > 0";
            //    command.CommandText = "SELECT SUM(Processed), SUM(Success), SUM(Fail), Failed_Threshold_Red, Failed_Threshold_Amber FROM feeddata_view WHERE RecordDt = '2015-07-28' GROUP BY Failed_Threshold_Red, Failed_Threshold_Amber";
            //}

            //  throw new NotImplementedException();
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
           // throw new NotImplementedException();
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
          //  throw new NotImplementedException();
        }
    }
}