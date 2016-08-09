﻿using System.Collections.Generic;
using DML.Generator.Domain;
using System.Data;
using System;

namespace DML.Generator.Web.Models
{
    /// <summary>
    /// File upload view model
    /// </summary>
    public class FileUploadViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileUploadViewModel"/> class.
        /// </summary>
        /// <param name="connectionId">The connection id.</param>
        public FileUploadViewModel(string connectionId)
        {
            this.ConnectionId = connectionId;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has errors.
        /// </summary>
        /// <value><c>true</c> if this instance has errors; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        public bool HasErrors { get; set; }

        /// <summary>
        /// Gets or sets the file data.
        /// </summary>
        /// <value>
        /// The file data.
        /// </value>
        public byte FileData { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the type of the file MIME.
        /// </summary>
        /// <value>
        /// The type of the file MIME.
        /// </value>
        public string FileMimeType { get; set; }

        /// <summary>
        /// Gets the connection id.
        /// </summary>
        public string ConnectionId { get; private set; }

        /// <summary>
        /// Gets or sets the sheet information.
        /// </summary>
        /// <value>
        /// The sheet information.
        /// </value>
        public List<DMLInfo> DMLInfo { get; set; }

        /// <summary>
        /// Gets or sets the data set.
        /// </summary>
        /// <value>
        /// The data set.
        /// </value>
        public DataSet DMLDataSet { get; set; }
    }
}