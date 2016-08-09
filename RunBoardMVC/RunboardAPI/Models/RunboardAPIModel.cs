namespace RunboardAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Infrastructure.Interception;
    using System.Web.Http;

    public partial class RunboardAPIModel : DbContext
    {
        public RunboardAPIModel()
            : base("name=RunboardAPIModel")
        {
            DbInterception.Add(new EFInterceptor());
        }

        public virtual DbSet<CommentTable> CommentTables { get; set; }
        public virtual DbSet<limittable> limittables { get; set; }
        public virtual DbSet<BatchData> BatchDatas { get; set; }
        public virtual DbSet<datatable> datatables { get; set; }
        public virtual DbSet<deferdata> deferdatas { get; set; }
        public virtual DbSet<DeferObject> DeferObjects { get; set; }
        public virtual DbSet<FeedData> FeedDatas { get; set; }
        public virtual DbSet<HalErr> HalErrs { get; set; }
        public virtual DbSet<logindetail> logindetails { get; set; }
        public virtual DbSet<batchdata_view> batchdata_view { get; set; }
        public virtual DbSet<datatable_view> datatable_view { get; set; }
        public virtual DbSet<feeddata_view> feeddata_view { get; set; }
        public IHttpActionResult ConsolidatedFeed { get; internal set; }

        //   public virtual DbSet<ConsolidatedFeed> ConsolidatedFeed { get; internal set; }

        public virtual ObjectResult<RetrievedashboardStatus_Result> RetrievedashboardStatus(Nullable<System.DateTime> ddate)
        {
            var ddateParameter = ddate.HasValue ?
                new ObjectParameter("ddate", ddate) :
                new ObjectParameter("ddate", typeof(System.DateTime));

            //var ttypeParameter = ttype != null ?
            //    new ObjectParameter("ttype", ttype) :
            //    new ObjectParameter("ttype", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RetrievedashboardStatus_Result>("RetrievedashboardStatus", ddateParameter);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommentTable>()
                .Property(e => e.RecordDt)
                .IsFixedLength();

            modelBuilder.Entity<CommentTable>()
                .Property(e => e.Type)
                .IsFixedLength();

            modelBuilder.Entity<CommentTable>()
                .Property(e => e.RACFID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CommentTable>()
                .Property(e => e.CommentTitle)
                .IsUnicode(false);

            modelBuilder.Entity<CommentTable>()
                .Property(e => e.CommentText)
                .IsUnicode(false);

            modelBuilder.Entity<limittable>()
                .Property(e => e.Ttype)
                .IsFixedLength();

            modelBuilder.Entity<BatchData>()
                .Property(e => e.Jobname)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BatchData>()
                .Property(e => e.Error_Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BatchData>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<BatchData>()
                .Property(e => e.Job_Category)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BatchData>()
                .Property(e => e.Job_Type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<datatable>()
                .Property(e => e.Type)
                .IsFixedLength();

            modelBuilder.Entity<deferdata>()
                .Property(e => e.Time_Defer)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<deferdata>()
                .Property(e => e.Time_Run)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DeferObject>()
                .Property(e => e.Object)
                .IsFixedLength();

            modelBuilder.Entity<DeferObject>()
                .Property(e => e.Status)
                .IsFixedLength();

            modelBuilder.Entity<DeferObject>()
                .Property(e => e.Time)
                .IsFixedLength();

            modelBuilder.Entity<FeedData>()
                .Property(e => e.Feed_Type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<FeedData>()
                .Property(e => e.Feed_Name)
                .IsUnicode(false);

            modelBuilder.Entity<HalErr>()
                .Property(e => e.Error_Ref)
                .IsUnicode(false);

            modelBuilder.Entity<HalErr>()
                .Property(e => e.Fail_Pgm_Name)
                .IsUnicode(false);

            modelBuilder.Entity<HalErr>()
                .Property(e => e.Fail_Para_Name)
                .IsUnicode(false);

            modelBuilder.Entity<logindetail>()
                .Property(e => e.Username)
                .IsFixedLength();

            modelBuilder.Entity<logindetail>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<batchdata_view>()
                .Property(e => e.Jobname)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<batchdata_view>()
                .Property(e => e.Error_Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<batchdata_view>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<batchdata_view>()
                .Property(e => e.Job_Category)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<batchdata_view>()
                .Property(e => e.Job_Type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<datatable_view>()
                .Property(e => e.Type)
                .IsFixedLength();

            modelBuilder.Entity<feeddata_view>()
                .Property(e => e.Feed_Type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<feeddata_view>()
                .Property(e => e.Feed_Name)
                .IsUnicode(false);
        }
    }
}
