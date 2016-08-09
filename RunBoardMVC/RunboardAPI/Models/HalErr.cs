namespace RunboardAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HalErr")]
    public partial class HalErr
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(12)]
        public string Error_Ref { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Day_O_Count { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Day_T_Count { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Day_Th_Count { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(15)]
        public string Fail_Pgm_Name { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string Fail_Para_Name { get; set; }

        [Key]
        [Column(Order = 6)]
        public DateTime RecordDt { get; set; }

        public int? Day_Fo_Count { get; set; }

        public int? Day_Fi_Count { get; set; }
    }
}
