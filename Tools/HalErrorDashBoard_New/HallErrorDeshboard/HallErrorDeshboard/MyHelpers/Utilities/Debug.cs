using System;
using System.Collections.Generic;
using System.Web;
using System.IO;

namespace MyHelpers
{
    /// <summary>
    /// Summary description for Debug
    /// </summary>
    public class Debug
    {
        public Debug()
        {

        }

        public static void WriteLine(string Line)
        {
            System.Diagnostics.Debug.WriteLine(Line);
        }

        public static void WriteLineWithTime(string Line)
        {
            WriteLine(String.Format("{0}: {1}", DateTime.Now, Line));
        }

        public static void AppendLineToFile(string Line, string FileName)
        {
            File.AppendAllText(FileName, Line.EndsWith("\n") ? Line : Line + "\n");
        }

    }
}