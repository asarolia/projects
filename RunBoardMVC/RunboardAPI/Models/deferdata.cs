namespace RunboardAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("deferdata")]
    public partial class deferdata
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string Time_Defer { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string Time_Run { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Delay { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime RecordDt { get; set; }
    }
}
