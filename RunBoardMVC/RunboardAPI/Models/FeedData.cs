namespace RunboardAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FeedData")]
    public partial class FeedData
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(15)]
        public string Feed_Type { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(40)]
        public string Feed_Name { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Processed { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Success { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Fail { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime RecordDt { get; set; }
    }
}
