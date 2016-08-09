using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using SignalR;
using SignalR.Hubs;
using SignalR.Infrastructure;

namespace DML.Generator.Web.Helper
{
    public static class DMLHelper
    {
        /// <summary>
        /// Get directory path.
        /// </summary>
        public static Func<string, string> GetDirectoryPath = clientIp => Path.Combine(ConfigurationManager.AppSettings.Get("FileUploadPath"), clientIp);

        /// <summary>
        /// Get file path.
        /// </summary>
        public static Func<string, string, string> GetFilePath = (path, fileName) => Path.Combine(path, fileName);

        /// <summary>
        /// Create client Directory.
        /// </summary>
        public static Action<string> CreateDirectory = (filePath) =>
        {
            if (Directory.Exists(filePath))
            {
                Directory.Delete(filePath, true);
            }
            Directory.CreateDirectory(filePath);
        };

        /// <summary>
        /// Brodcast the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void BrodCast(string connectionId, string message)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<Chat>();
            context.Clients.addMessage(connectionId, message);
        }
    }
}