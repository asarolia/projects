namespace RunboardAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("limittable")]
    public partial class limittable
    {
        [Key]
        [StringLength(10)]
        public string Ttype { get; set; }

        public int Failed_Threshold_Red { get; set; }

        public int Unprocess_Threshold_Red { get; set; }

        public int Failed_Threshold_Amber { get; set; }

        public int Unprocess_Threshold_Amber { get; set; }
    }
}
