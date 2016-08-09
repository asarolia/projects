using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RunboardAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;

    public class RundashboardDbContext: DbContext
    {
        public RundashboardDbContext()
            : base("name=RunDashboardEntities")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<CommentTable> CommentTables { get; set; }
        public virtual DbSet<limittable> limittables { get; set; }
        public virtual DbSet<datatable_view> datatable_view { get; set; }
        public virtual DbSet<feeddata_view> feeddata_view { get; set; }
        public virtual DbSet<BatchData> BatchDatas { get; set; }
        public virtual DbSet<datatable> datatables { get; set; }
        public virtual DbSet<deferdata> deferdatas { get; set; }
        public virtual DbSet<DeferObject> DeferObjects { get; set; }
        public virtual DbSet<FeedData> FeedDatas { get; set; }
        public virtual DbSet<HalErr> HalErrs { get; set; }
        public virtual DbSet<logindetail> logindetails { get; set; }
        public virtual DbSet<batchdata_view> batchdata_view { get; set; }

        //        public virtual ObjectResult<Retrievedashboarddetails_Result> Retrievedashboarddetails(Nullable<System.DateTime> ddate, string ttype)
        //public virtual ObjectResult<Retrievedashboarddetails_Result> Retrievedashboarddetails(Nullable<System.DateTime> ddate, string ttype)
        //{
        //    var ddateParameter = ddate.HasValue ?
        //        new ObjectParameter("ddate", ddate) :
        //        new ObjectParameter("ddate", typeof(System.DateTime));

        //    var ttypeParameter = ttype != null ?
        //        new ObjectParameter("ttype", ttype) :
        //        new ObjectParameter("ttype", typeof(string));

        //    return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Retrievedashboarddetails_Result>("Retrievedashboarddetails", ddateParameter, ttypeParameter);
        //}

        //public virtual ObjectResult<Retrievedashboardgraph_Result> Retrievedashboardgraph(Nullable<System.DateTime> ddate, string ttype)
        //{
        //    var ddateParameter = ddate.HasValue ?
        //        new ObjectParameter("ddate", ddate) :
        //        new ObjectParameter("ddate", typeof(System.DateTime));

        //    var ttypeParameter = ttype != null ?
        //        new ObjectParameter("ttype", ttype) :
        //        new ObjectParameter("ttype", typeof(string));

        //    return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Retrievedashboardgraph_Result>("Retrievedashboardgraph", ddateParameter, ttypeParameter);
        //}

        //public virtual ObjectResult<RetrievedashboardStatus_Result> RetrievedashboardStatus(Nullable<System.DateTime> ddate)
        //{
        //    var ddateParameter = ddate.HasValue ?
        //        new ObjectParameter("ddate", ddate) :
        //        new ObjectParameter("ddate", typeof(System.DateTime));

        //    return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RetrievedashboardStatus_Result>("RetrievedashboardStatus", ddateParameter);
        //}
    }
}