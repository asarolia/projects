namespace DML.Generator.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    /// <summary>
    /// Add scheme view model.
    /// </summary>
    public class AddSchemeViewModel
    {
        /// <summary>
        /// Gets or sets the scheme id.
        /// </summary>
        /// <value>
        /// The scheme id.
        /// </value>
        [Required(ErrorMessage = "Please enter scheme number.")]
        [DisplayName("Exceed Scheme Number:")]
        public int SchemeId { get; set; }
    }
}