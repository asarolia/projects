namespace RunboardAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class batchdata_view
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string Jobname { get; set; }

        public DateTime? Start_Date { get; set; }

        public int? Start_Time { get; set; }

        public DateTime? Error_Date { get; set; }

        public int? Error_Time { get; set; }

        [StringLength(5)]
        public string Error_Code { get; set; }

        public DateTime? Restart_Date { get; set; }

        public int? Restart_Time { get; set; }

        public DateTime? End_Date { get; set; }

        public int? End_Time { get; set; }

        public int? Time_Lost { get; set; }

        [StringLength(60)]
        public string Comment { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string Job_Category { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string Job_Type { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime RecordDt { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Failed_Threshold_Red { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Unprocess_Threshold_Red { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Failed_Threshold_Amber { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Unprocess_Threshold_Amber { get; set; }
    }
}
