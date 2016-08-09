namespace RunboardAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class logindetail
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(15)]
        public string Username { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string Password { get; set; }
    }
}
