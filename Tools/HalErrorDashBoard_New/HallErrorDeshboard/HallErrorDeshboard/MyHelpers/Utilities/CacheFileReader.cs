using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace MyHelpers
{
    /// <summary>
    /// Summary description for CacheFileRead
    /// </summary>
    public class CacheFileReader
    {
        private static Dictionary<string, MemoryStream> dict = new Dictionary<string, MemoryStream>();

        public static StreamReader ReadFileAndCache(string FileName)
        {
            return getInstance(FileName, false);
        }

        public static StreamReader ReadFile(string FileName)
        {
            return getInstance(FileName, false);
        }

        private static StreamReader getInstance(string FileName, bool updateCache)
        {
            CacheFileReader cachefile = new CacheFileReader();
            MemoryStream m;

            if (!dict.ContainsKey(FileName) || dict[FileName] == null)
            {
                try
                {
                    m = LoadFileInMemory(FileName);
                    m.Position = 0;
                }
                catch (Exception)
                {
                    throw;
                }
                if (updateCache)
                    dict.Add(FileName, m);
            }
            else
            {
                m = dict[FileName];
            }

            return new StreamReader(m);
        }

        private static MemoryStream LoadFileInMemory(string FileName)
        {
            MemoryStream memory;

            try
            {
                FileStream f = File.OpenRead(FileName);
                memory = new MemoryStream((int)f.Length);
                StreamReader stream = new StreamReader(f);
                StreamWriter writer = new StreamWriter(memory);

                while (!stream.EndOfStream)
                {
                    string line = stream.ReadLine();
                    writer.WriteLine(line);
                }
                writer.Flush();
                stream.Close();
            }
            catch (Exception e)
            {
                throw;
            }
            return memory;

        }

        private CacheFileReader()
        {

        }

    }
}