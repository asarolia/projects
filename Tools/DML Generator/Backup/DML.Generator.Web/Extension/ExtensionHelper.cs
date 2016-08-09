namespace DML.Generator.Web.Extension
{
    using System.IO;
    using DML.Generator.Web.Helper;
    using System.Collections.Generic;
    using System;

    /// <summary>
    /// Extension method class
    /// </summary>
    /// <remarks></remarks>
    public static class ExtensionHelper
    {
        /// <summary>
        /// Writes to file.
        /// </summary>
        /// <param name="DMLString">The DML string.</param>
        /// <param name="type">The type.</param>
        /// <param name="clientIP">The client IP.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <remarks></remarks>
        public static void WriteToFile(this string DMLString, string type, string clientIP, string fileName)
        {
            string path = DMLHelper.GetDirectoryPath(clientIP);
            string directoryPath = string.Format("{0}/{1}", path, type);
            string filePath = DMLHelper.GetFilePath(directoryPath, fileName + ".txt");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            using (StreamWriter writer = File.Exists(filePath) ? new StreamWriter(filePath, true) : new StreamWriter(filePath, false))
            {
                writer.WriteLine(DMLString);
                writer.Close();
            }
        }
    }
}