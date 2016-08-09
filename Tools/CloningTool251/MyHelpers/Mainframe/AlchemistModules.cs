using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;

namespace MyHelpers
{
    /// <summary>
    /// Summary description for AlchemistModules
    /// </summary>
    public class AlchemistModules
    {
        private static AlchemistModules self;
        private string FileName, PDSName;
        private Dictionary<String, List<String>> ModuleList;
        private DateTime cacheDate;

        private AlchemistModules()
        {
            //migration fixes
            //metadata = new Metadata("alchemist");

            //PDSName = metadata.AlchemistLibrary;
            //FileName = metadata.AlchemistCache;
            cacheDate = new DateTime();

        }

        public static AlchemistModules getInstance(FtpClient ftpClient)
        {
            try
            {
                if (self == null)
                {
                    self = new AlchemistModules();
                }
                self.checkCache(ftpClient);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("AlchemistModules.getInstance " + e.Message);
            }

            return self;
        }

        private void checkCache(FtpClient ftpClient)
        {

            if (!isCacheUpToDate())
                RefreshCache(ftpClient);


        }

        public string[] FindModule(string moduleName)
        {
            moduleName = moduleName.Trim().ToUpper();

            if (moduleName.Contains("*"))
                return FindLikeModules(moduleName);
            else
                return FindExactModule(moduleName);
        }

        private string[] FindLikeModules(string moduleName)
        {
            List<string> modules = new List<string>();
            List<string> alchemModules = new List<string>();
            List<string> ret = new List<string>();
            String baseModuleName = moduleName.Replace('*', ' ').Trim();

            Regex startingWith = new Regex(@"^(" + baseModuleName + @")[A-Z0-9]*", RegexOptions.Compiled);
            Regex endingWith = new Regex(@"[A-Z0-9]*(" + baseModuleName + @")\z", RegexOptions.Compiled);
            Regex criteria;

            if (moduleName.EndsWith("*"))
                criteria = startingWith;
            else
                criteria = endingWith;

            alchemModules.AddRange(ModuleList.Keys);

            foreach (string alchemModule in alchemModules)
                if (criteria.IsMatch(alchemModule))
                    modules.Add(alchemModule);

            string[] result;

            foreach (string module in modules)
            {
                result = FindExactModule(module);
                if (result != null)
                    ret.AddRange(result);
            }

            result = new string[ret.Count];
            ret.CopyTo(result);
            return result;
        }

        private string[] FindExactModule(string moduleName)
        {
            string[] ret = null;
            try
            {
                if (ModuleList.ContainsKey(moduleName))
                {
                    ret = new string[ModuleList[moduleName].Count];
                    ModuleList[moduleName].CopyTo(ret);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("AlchemistModules.FindModule " + e.Message);
            }

            return ret;
        }

        /// <summary>
        /// Check if alchemist module cache is up to date.
        /// </summary>
        private bool isCacheUpToDate()
        {
            DateTime defaultTime = new DateTime();

            if (cacheDate.Equals(defaultTime))
            {
                //check if cache file exists
                //if (File.Exists(metadata.AlchemistCache))
                //migration fixes
                if (File.Exists("test"))
                {
                    //check if file is up to date
                    //FileInfo fileInfo = new FileInfo(metadata.AlchemistCache);
                    FileInfo fileInfo = new FileInfo("test");
                    cacheDate = fileInfo.LastWriteTime.Date;

                    if (cacheDate.Equals(DateTime.Now.Date))
                        loadContentToMemory();
                }
            }

            if (cacheDate.Equals(DateTime.Now.Date))
                return true;

            return false;

            ////check if cache file exists
            //if (File.Exists(metadata.AlchemistCache))
            //{
            //    //check if file is up to date
            //    FileInfo fileInfo = new FileInfo(metadata.AlchemistCache);
            //    DateTime lastModified = fileInfo.LastWriteTime.Date;
            //    DateTime currentDate = (new DateTime()).Date;

            //    if (lastModified.Equals(currentDate))
            //        return true;
            //}

            //return false;
        }

        /// <summary>
        /// Refresh cache by reading entry from host
        /// </summary>
        /// <param name="ftpClient"></param>
        private void RefreshCache(FtpClient ftpClient)
        {
            //if (metadata.AlchemistLibrary.Length == 0 || metadata.AlchemistCache.Length == 0)
            //    throw new InvalidOperationException("AlchemistRefreshCache: invalid configuration. Alchem lib: " + metadata.AlchemistLibrary + " OR Alchem cache: " + metadata.AlchemistCache + " are empty.");

            //metadata.LogDownloadInitiated(metadata.AlchemistLibrary);
            //ftpClient.DownloadFile(metadata.AlchemistLibrary, metadata.AlchemistCache, false);
            //metadata.LogDownloadComplete();

            loadContentToMemory();
        }

        private void loadContentToMemory()
        {
            StreamReader reader = new StreamReader(FileName);
            cacheDate = DateTime.Now.Date;

            ModuleList = new Dictionary<string, List<string>>();

            String line, moduleName, type, remark, PDS, entry;

            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();

                //module name
                if (line.Length > 8)
                    moduleName = line.Substring(0, 8).Trim();
                else
                    moduleName = line.Substring(0, line.Length).Trim();

                //type
                if (line.Length > 9)
                    type = line.Substring(8, 1).Trim();
                else
                    type = "L";

                //amendment history
                if (line.Length > 27)
                    remark = line.Substring(9, 18).Trim();
                else
                    remark = "";

                //pds
                if (line.Length > 62)
                    PDS = line.Substring(27, 35).Trim();
                else
                    if (line.Length > 27)
                        PDS = line.Substring(27, line.Length - 27).Trim();
                    else
                        PDS = String.Empty;

                if (moduleName.Length == 0 || PDS.Length == 0) continue;

                //entry = String.Format("{0},{1},{1}({0}),{2},{3}", moduleName, PDS,(type.Equals("D")?"":"ReadOnly"),remark);
                entry = String.Format("{0},{1},{1}({0}),{2},{3}", moduleName, PDS, (PDS.StartsWith("A") == true ? "ReadOnly" : "Worklib"), remark);

                if (ModuleList.ContainsKey(moduleName))
                {
                    ModuleList[moduleName].Add(entry);
                }
                else
                {
                    List<String> list = new List<string>();
                    list.Add(entry);
                    ModuleList.Add(moduleName, list);
                }
            }
        }

    }
}