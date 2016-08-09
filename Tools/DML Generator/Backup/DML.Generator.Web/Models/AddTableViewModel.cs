namespace DML.Generator.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    /// <summary>
    /// Table view model.
    /// </summary>
    public class TableViewModel
    {
        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        [Required(ErrorMessage="Please enter table name.")]
        [DisplayName("Exceed Table Name:")]
        public string TableName { get; set; }

        /// <summary>
        /// Gets or sets the name of the host DML.
        /// </summary>
        /// <value>
        /// The name of the host DML.
        /// </value>
        [Required(ErrorMessage = "Please enter host dml name.")]
        [DisplayName("Host DML Name:")]
        public string HostDMLName { get; set; }

        /// <summary>
        /// Gets or sets the name of the FEDML.
        /// </summary>
        /// <value>
        /// The name of the FEDML.
        /// </value>
        [Required(ErrorMessage = "Please enter FE dml name.")]
        [DisplayName("FE DML Name:")]
        public string FEDMLName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is table exist.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is table exist; otherwise, <c>false</c>.
        /// </value>
        public bool IsTableExist { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is grouped table.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is grouped table; otherwise, <c>false</c>.
        /// </value>
        public bool IsGroupedTable { get; set; }
    }
}