namespace RunboardAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DeferObject
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string Object { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1)]
        public string Status { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Count { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime RecordDt { get; set; }

        [StringLength(30)]
        public string Time { get; set; }
    }
}
