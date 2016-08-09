using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace MyHelpers
{
    public class EmbeddedResource
    {
        static string _basePath = null;

        public EmbeddedResource()
        {

        }

        public static void SetBasePath(string path)
        {
            _basePath = path;
        }

        public string PrependAssemblyPath(string path)
        {
            string ret = path;
            if (path.StartsWith(@".\"))
                ret = CurrentFolder() + path.Substring(1);

            if (!Directory.Exists(Path.GetDirectoryName(ret)))
                Directory.CreateDirectory(Path.GetDirectoryName(ret));

            return ret;
        }

        public string CurrentFolder()
        {
            if (!String.IsNullOrEmpty(_basePath))
                return _basePath;

            Assembly assem = this.GetType().Assembly;
            return Path.GetDirectoryName(assem.Location);
        }

        public StreamReader GetStreamReader(string ResourceName)
        {
            Assembly assem = this.GetType().Assembly;
            try
            {
                Stream stream = assem.GetManifestResourceStream(ResourceName);
                StreamReader reader = new StreamReader(stream);
                return reader;
            }
            catch (Exception e)
            {
                throw new Exception("Error retrieving from Resources. Tried '"
                                            + ResourceName + "'\r\n" + e.ToString());
            }

        }
    }
}
