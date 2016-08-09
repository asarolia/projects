using System;
using System.Collections.Generic;
using System.Web;
using System.Threading;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;

namespace MyHelpers
{
    /// <summary>
    /// Summary description for CompositeJCLTasks
    /// </summary>
    public class CompositeJCLTasks
    {
        JCLClient client;
        const string FetchElementsURL = "EndeavorElementList.jcl";

        public CompositeJCLTasks()
        {
        }

        public string FetchEndeavorModules(string UserId, string Password, string Environment, string Type, string Stage)
        {
            client = new JCLClient();

            client.SetCredentials(UserId, Password);

            //create Parameter list
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("environment", (Environment.Length == 0) ? "*" : Environment);
            param.Add("type", (Type.Length == 0) ? "*" : Type);
            param.Add("stage", (Stage.Length == 0) ? "*" : Stage);

            //client.Submit(HttpContext.Current.Server.MapPath(FetchElementsURL));
            if (!client.Submit(PrepareJCL(FetchElementsURL, param)))
            {
                return "";
            }

            //wait till job is complete
            while (true)
            {
                if (client.isJobComplete())
                {
                    break;
                }
                Thread.Sleep(100);
            }

            //extract output file
            if (!client.Spool("4"))
            {
                return client.Message;
            }

            //Remove additinoal lines
            FilterFile(client.Message, new Regex(@"(&&ACTION ELEMENT)|(TYPE[ A-Z]+STAGE NUMBER)"));

            //Extract modules list
            List<ModuleEntry> res = ExtractModulesListFromFile(client.Message);

            //convert them to string
            string ret = "";
            foreach (ModuleEntry entry in res)
            {
                ret += entry.ToString() + "\n";
            }

            //return
            return ret;
        }

        private string PrefixJclLibrary(string file)
        {
            //HttpRequest req = HttpContext.Current.Request;
            //return String.Format("http://{0}{1}/{2}/{3}", req.ServerVariables["HTTP_HOST"], req.ApplicationPath, "JCLLib", file);
            //migration fixes
            return file;
        }

        private string PrepareJCL(string file, Dictionary<string, string> parameters)
        {
            string tempFile = "PREP" + (new Random()).Next(999).ToString();
            //migration fixes
            //StringBuilder str = new StringBuilder(File.ReadAllText(Metadata.StripServerPath(PrefixJclLibrary(file))));
            StringBuilder str = new StringBuilder(File.ReadAllText(PrefixJclLibrary(file)));

            foreach (string key in parameters.Keys)
            {
                str.Replace("%" + key.Trim().ToLower() + "%", parameters[key].Trim());
            }

            //File.WriteAllText(Metadata.StripServerPath(PrefixJclLibrary(tempFile)), str.ToString());
            File.WriteAllText(PrefixJclLibrary(tempFile), str.ToString());
            return PrefixJclLibrary(tempFile);
        }

        private void FilterFile(string file, Regex filter)
        {
            StringBuilder str = new StringBuilder();

            using (StreamReader sr = File.OpenText(file))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (filter.IsMatch(line))
                        str.AppendLine(line);
                }
            }

            File.WriteAllText(file, str.ToString());
        }

        private List<ModuleEntry> ExtractModulesListFromFile(string file)
        {
            //Regex Regtype = new Regex(@"^[ ]+TYPE ([A-Z])+[ ]+STAGE NUMBER([0-9])+");
            Regex Regtype = new Regex(@"TYPE[ A-Z]+STAGE NUMBER");
            Regex Regmodule = new Regex(@"&&ACTION ELEMENT");
            string type = "", name = "";
            int stage = 0, version = 0, level = 0;

            List<ModuleEntry> modules = new List<ModuleEntry>();
            char[] splitChar = { ' ' };

            using (StreamReader sr = File.OpenText(file))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    try
                    {
                        string[] splitLine = line.Split(splitChar, StringSplitOptions.RemoveEmptyEntries);
                        //ex.                     TYPE  BPGCOB    STAGE NUMBER 2 .                                                                                 
                        if (Regtype.IsMatch(line))
                        {
                            type = splitLine[1];
                            int.TryParse(splitLine[4], out stage);
                        }
                        //ex.                 &&ACTION ELEMENT   PCXDGFIF      VERSION 01   LEVEL 04  .                                                            
                        else if (Regmodule.IsMatch(line))
                        {
                            name = splitLine[2];
                            int.TryParse(splitLine[4], out version);
                            int.TryParse(splitLine[6], out level);
                            modules.Add(new ModuleEntry(name, type, stage, version, level));
                        }
                    }
                    catch { }
                }
            }

            return modules;
        }

    }
}