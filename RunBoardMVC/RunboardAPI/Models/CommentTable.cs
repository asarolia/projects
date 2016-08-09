namespace RunboardAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommentTable")]
    public partial class CommentTable
    {
        [StringLength(10)]
        public string RecordDt { get; set; }

        [StringLength(10)]
        public string Type { get; set; }

        [Key]
        public DateTime CommentRecordDt { get; set; }

        [StringLength(8)]
        public string RACFID { get; set; }

        [StringLength(50)]
        public string CommentTitle { get; set; }

        [StringLength(500)]
        public string CommentText { get; set; }
    }
}
