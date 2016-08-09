using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace MyHelpers
{
    class Compress
    {
        static public void ZipCompressFiles(string[] FileNames, string[] PsudoNames, string TargetFile)
        {

            //create zip output
            ZipOutputStream zipOut = new ZipOutputStream(File.Create(TargetFile));

            for (int i = 0; i < FileNames.Length; i++)
            {
                FileInfo fileInfo = new FileInfo(FileNames[i]);

                //create zip entry
                ZipEntry zipentry = new ZipEntry(PsudoNames[i]);

                //read input file
                FileStream filestream = File.OpenRead(FileNames[i]);
                byte[] buff = new byte[(int)filestream.Length];
                filestream.Read(buff, 0, buff.Length);
                filestream.Close();

                //prepare zip entry
                zipentry.DateTime = fileInfo.LastWriteTime;
                zipentry.Size = buff.Length;

                //write to zip file
                zipOut.PutNextEntry(zipentry);
                zipOut.Write(buff, 0, buff.Length);
            }

            zipOut.Finish();
            zipOut.Close();

        }

        static void ZipUnCompressFiles(String ZipFileName, string[] PsudoNames, string[] actualNames)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();

            for (int i = 0; i < PsudoNames.Length; i++)
            {
                map[PsudoNames[i]] = actualNames[i];
            }

            //create zip in stream
            ZipInputStream zipIn = new ZipInputStream(File.OpenRead(ZipFileName));

            ZipEntry entry;
            while ((entry = zipIn.GetNextEntry()) != null)
            {

                string filename = "", folder = "";

                if (map.ContainsKey(entry.Name))
                    filename = map[entry.Name];
                else
                    throw new InvalidOperationException("ZipUnCOmpressFiles: File name not found for '" + entry.Name + "'.");

                folder = filename.Substring(0, filename.LastIndexOf('\\'));

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                FileStream streamWriter = File.Create(filename);

                long size = entry.Size;
                byte[] data = new byte[size];
                while (true)
                {
                    size = zipIn.Read(data, 0, data.Length);
                    if (size > 0) streamWriter.Write(data, 0, (int)size);
                    else break;
                }
                streamWriter.Close();
            }

        }

        static void ZipCompressFolder(String FolderName, String TargetFolder, String TargetFileName)
        {

            if (!Directory.Exists(TargetFolder))
                Directory.CreateDirectory(TargetFolder);

            String targetFile = String.Format("{0}\\{1}", TargetFolder, TargetFileName);

            //create zip output
            ZipOutputStream zipOut = new ZipOutputStream(File.Create(targetFile));

            foreach (string file in Directory.GetFiles(FolderName))
            {
                FileInfo fileInfo = new FileInfo(file);

                //create zip entry
                ZipEntry zipentry = new ZipEntry(fileInfo.Name);

                //read input file
                FileStream filestream = File.OpenRead(file);
                byte[] buff = new byte[(int)filestream.Length];
                filestream.Read(buff, 0, buff.Length);
                filestream.Close();

                //prepare zip entry
                zipentry.DateTime = fileInfo.LastWriteTime;
                zipentry.Size = buff.Length;

                //write to zip file
                zipOut.PutNextEntry(zipentry);
                zipOut.Write(buff, 0, buff.Length);
            }

            zipOut.Finish();
            zipOut.Close();
        }



        static void ZipUnCompressFolder(String ZipFileName, String TargetFolder)
        {
            if (!Directory.Exists(TargetFolder))
                Directory.CreateDirectory(TargetFolder);

            //create zip in stream
            ZipInputStream zipIn = new ZipInputStream(File.OpenRead(ZipFileName));

            ZipEntry entry;
            while ((entry = zipIn.GetNextEntry()) != null)
            {
                FileStream streamWriter = File.Create(String.Format("{0}\\{1}", TargetFolder, entry.Name));

                long size = entry.Size;
                byte[] data = new byte[size];
                while (true)
                {
                    size = zipIn.Read(data, 0, data.Length);
                    if (size > 0) streamWriter.Write(data, 0, (int)size);
                    else break;
                }
                streamWriter.Close();
            }

        }

        //Unedited..
        static void ListZipContent(string sFile)
        {
            ZipFile zip = new ZipFile(File.OpenRead(sFile));
            foreach (ZipEntry entry in zip)
            {
                Console.WriteLine(entry.Name);
            }
        }

        /// <summary>
        /// Fast Compress folder
        /// </summary>
        /// <param name="FolderName"></param>
        /// <param name="TargetFolder"></param>
        /// <param name="TargetFileName"></param>
        static void FastZipCompressFolder(String FolderName, String TargetFolder, String TargetFileName)
        {
            if (!Directory.Exists(TargetFolder))
                Directory.CreateDirectory(TargetFolder);

            String targetFile = String.Format("{0}\\{1}", TargetFolder, TargetFileName);

            FastZip fZip = new FastZip();
            fZip.CreateZip(targetFile, FolderName, true, "");
        }

        /// <summary>
        /// Fast uncompress folder
        /// </summary>
        /// <param name="ZipFileName"></param>
        /// <param name="TargetFolder"></param>
        static void FastZipUnCompressFolder(String ZipFileName, String TargetFolder)
        {
            if (!Directory.Exists(TargetFolder))
                Directory.CreateDirectory(TargetFolder);

            FastZip fZip = new FastZip();
            fZip.ExtractZip(ZipFileName, TargetFolder, "");
        }

    }
}