using System;
using System.Data;
using System.Configuration;
using System.IO;


/// <summary>
/// Summary description for LogUtility
/// </summary>
public class LogUtility
{
    static private string location = "Log/";
    static private string locationUser = "Log/User/";

	private LogUtility()
	{
	}

    static public void LogUsage(string subMessage,PropositionDetails propositionD)
    {
        Log(locationUser + propositionD.User + ".log",
            string.Format("{0} {1}:{2}",
            DateTime.Now.ToShortDateString(),
            DateTime.Now.ToShortTimeString(),
            subMessage + " '" + propositionD.Name + "'"));
    }

    static private void Log(string fileName,string Message)
    {
        if (!File.Exists(fileName))
        {
            if (!Directory.Exists(Path.GetDirectoryName(fileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            }
            (File.Create(fileName)).Close();
        }

        using (StreamWriter w = File.AppendText(fileName))
        {
            w.WriteLine(Message);
            w.Flush();
            w.Close();
        }

    }

}
