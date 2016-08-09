﻿namespace DML.Generator.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    using DML.Generator.Domain;
    using DML.Generator.Domain.DML;
    using DML.Generator.Domain.Extensions;
    using DML.Generator.Web.Attributes;
    using DML.Generator.Web.Extension;
    using DML.Generator.Web.Helper;
    using DML.Generator.Web.Models;
    using Ionic.Zip;
    using System.Text;

    public class HomeController : BaseController
    {
        /// <summary>
        /// Files the upload.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FileUpload()
        {
            this.FileUploadViewModel = new FileUploadViewModel(Guid.NewGuid().ToString());
            return View(this.FileUploadViewModel);
        }

        /// <summary>
        /// Files the upload.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <returns></returns>
        [HttpPost]
        [Button(ButtonName="Upload")]
        public ActionResult FileUpload(HttpPostedFileBase excelFile)
        {
            string fileName = string.Empty;
            string path = string.Empty;
            string filePath = string.Empty;
            if (this.FileUploadViewModel != null)
            {
                this.FileUploadViewModel.HasErrors = false;
            }

            if (excelFile == null)
            {
                return View(this.FileUploadViewModel);
            }

            if(excelFile.FileName.ToLower().IndexOf(".xls") == -1)
            {
                TempData["message"] = "File type not supported";
                return View(this.FileUploadViewModel);
            }

            if (excelFile != null && excelFile.ContentLength > 0)
            {
                this.FileUploadViewModel.FileName = excelFile.FileName;
                fileName = Path.GetFileName(excelFile.FileName);
                path = DMLHelper.GetDirectoryPath(Request.UserHostAddress);
                DMLHelper.CreateDirectory(path);
                filePath = DMLHelper.GetFilePath(path, fileName);
                excelFile.SaveAs(filePath);
            }

            ExcelFactory excel = new ExcelFactory(filePath);
            this.FileUploadViewModel.DMLInfo = excel.GetSheetData();
            this.FileUploadViewModel.DMLInfo.ForEach(info=>
                {
                    if (!info.groupingIndicator && string.IsNullOrEmpty(info.HostDMLName))
                    {
                        this.FileUploadViewModel.HasErrors |= true;
                    }
                    else if (info.groupingIndicator && info.GroupHostDMLName.Count > 0 && info.GroupHostDMLName.Where(y => string.IsNullOrEmpty(y.Value)).Count() > 0)
                    {
                        this.FileUploadViewModel.HasErrors |= true;
                    }
                });

            return View(this.FileUploadViewModel); 
        }

        /// <summary>
        /// Creates the DML.
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateDML()
        {
            Contract.Requires(this.FileUploadViewModel != null, "File upload view model is null.");
            Contract.Requires(this.FileUploadViewModel.DMLInfo != null, "DML information is null.");
            Contract.Requires(this.FileUploadViewModel.DMLInfo.Count > 0, "DML information is null.");
            this.PopulateDataSet();
            Contract.Assert(this.FileUploadViewModel.DMLDataSet != null);
            this.GenerateDMLStrings();
            this.FileUploadViewModel.HasErrors = this.FileUploadViewModel.DMLInfo.FindAll(x => x.Errors.Count > 0 ).Count > 0;
            var data =
                new
                {
                    ProcessStatus = "Process Complete",
                    HasErrors = this.FileUploadViewModel.HasErrors
                };
            return Json(data);
        }

        /// <summary>
        /// Generates the DML strings.
        /// </summary>
        private void GenerateDMLStrings()
        {
            string HostFileName = string.Empty;
            string FEFileName = string.Empty;
            string SheetName = this.FileUploadViewModel.FileName;
            DMLHelper.CreateDirectory(Path.Combine(DMLHelper.GetDirectoryPath(Request.UserHostAddress), "HOST"));
            DMLHelper.CreateDirectory(Path.Combine(DMLHelper.GetDirectoryPath(Request.UserHostAddress), "FE"));
            foreach (DataTable dataTable in this.FileUploadViewModel.DMLDataSet.Tables)
            {
                List<DMLInfo> DMLInfo = this.FileUploadViewModel.DMLInfo.FindAll(x => string.Equals(x.SheetData.TableName, dataTable.TableName) && x.Errors.Count == 0);
                if (DMLInfo != null && DMLInfo.Count > 0)
                {
                    DMLInfo.ForEach(dmlInfo =>
                    {
                        if (!dmlInfo.groupingIndicator)
                        {
                            DMLHelper.BrodCast(this.FileUploadViewModel.ConnectionId, string.Format("#{0}:Generating DML.", dmlInfo.SheetData.Name));
                            Thread.Sleep(200);
                        }
                    });

                    DMLInfo firstInfo = this.FileUploadViewModel.DMLInfo.FirstOrDefault(x => string.Equals(x.SheetData.TableName, dataTable.TableName));

                    if (!firstInfo.groupingIndicator)
                    {
                        HostFileName = firstInfo.HostDMLName;
                        HostDML hostDML = new HostDML(dataTable, HostFileName, SheetName);
                        hostDML.HeaderInformation.WriteToFile("HOST", Request.UserHostAddress, HostFileName);
                        hostDML.DeleteStatement.WriteToFile("HOST", Request.UserHostAddress, HostFileName);
                        hostDML.InsertStatement.WriteToFile("HOST", Request.UserHostAddress, HostFileName);
                    }
                    else
                    {
                        foreach (KeyValuePair<string, string> keyVal in firstInfo.GroupHostDMLName)
                        {
                            DMLHelper.BrodCast(this.FileUploadViewModel.ConnectionId, string.Format("#{0}-{1}:Generating DML.", firstInfo.SheetData.Name, keyVal.Value));
                            Thread.Sleep(200);
                            DataTable keyDataTable = dataTable.Clone();
                            var keyRows = from myRow in dataTable.AsEnumerable()
                                          where string.Equals(myRow.Field<string>(firstInfo.GroupedOnCol), keyVal.Key)
                                          select myRow;

                            keyRows.CopyToDataTable(keyDataTable, LoadOption.OverwriteChanges);
                            HostDML hostDML = new HostDML(keyDataTable, keyVal.Value, SheetName);
                            hostDML.HeaderInformation.WriteToFile("HOST", Request.UserHostAddress, keyVal.Value);
                            hostDML.DeleteStatement.WriteToFile("HOST", Request.UserHostAddress, keyVal.Value);
                            hostDML.InsertStatement.WriteToFile("HOST", Request.UserHostAddress, keyVal.Value);
                            if (firstInfo.Errors.Count > 0)
                            {
                                StringBuilder errorMessage = new StringBuilder();
                                firstInfo.Errors.ForEach(error => { errorMessage.Append(error + "<br/>"); });
                                DMLHelper.BrodCast(this.FileUploadViewModel.ConnectionId, string.Format("#{0}-{2}:Error:{1}", firstInfo.SheetData.Name, errorMessage.ToString(), keyVal.Value));
                            }
                            else
                            {
                                DMLHelper.BrodCast(this.FileUploadViewModel.ConnectionId, string.Format("#{0}-{1}:Process Complete.", firstInfo.SheetData.Name, keyVal.Value));
                            }
                        }
                    }

                    FEFileName = firstInfo.FEDMLName;
                    FEDML FeDML = new FEDML(dataTable, HostFileName, SheetName);
                    FeDML.HeaderInformation.WriteToFile("FE", Request.UserHostAddress, FEFileName);
                    FeDML.DeleteStatement.WriteToFile("FE", Request.UserHostAddress, FEFileName);
                    FeDML.InsertStatement.WriteToFile("FE", Request.UserHostAddress, FEFileName);

                    DMLInfo.ForEach(dmlInfo =>
                    {
                        if (!dmlInfo.groupingIndicator)
                        {
                            if (dmlInfo.Errors.Count > 0)
                            {
                                StringBuilder errorMessage = new StringBuilder();
                                dmlInfo.Errors.ForEach(error => { errorMessage.Append(error + "<br/>"); });
                                DMLHelper.BrodCast(this.FileUploadViewModel.ConnectionId, string.Format("#{0}:Error:{1}", dmlInfo.SheetData.Name, errorMessage.ToString()));
                            }
                            else
                            {
                                DMLHelper.BrodCast(this.FileUploadViewModel.ConnectionId, string.Format("#{0}:Process Complete.", dmlInfo.SheetData.Name));
                            }

                            Thread.Sleep(200);
                        }
                    });
                }
            }
        }

        /// <summary>
        /// Downloads the DML.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public FileResult DownloadDML(string id)
        {
            string directoryPath = Path.Combine(DMLHelper.GetDirectoryPath(Request.UserHostAddress), id);
            string zipTo = directoryPath + ".zip";
            string downloadName = id + "DML.zip";
            
            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(directoryPath);
                zip.Save(zipTo);
            }

            return File(zipTo, "application/zip", downloadName);
        }

        /// <summary>
        /// Populates the data set.
        /// </summary>
        private void PopulateDataSet()
        {
            this.FileUploadViewModel.DMLDataSet = new DataSet();

            this.FileUploadViewModel.DMLInfo.ForEach(
                Info =>
                {
                    Info.Errors = new List<string>();
                    if (!this.FileUploadViewModel.DMLDataSet.Tables.Contains(Info.SheetData.TableName))
                    {
                        this.FileUploadViewModel.DMLDataSet.Tables.Add(Info.ToDataTable());
                    }
                    else
                    {
                        foreach (DataRow row in Info.ToDataTable().Rows)
                        {
                            this.FileUploadViewModel.DMLDataSet.Tables[Info.SheetData.TableName].ImportRow(row);
                        }
                    }

                    if (Info.Errors.Count > 0)
                    {
                        StringBuilder errorMessage = new StringBuilder();
                        Info.Errors.ForEach(error => { errorMessage.Append(error+"<br/>"); });
                        DMLHelper.BrodCast(this.FileUploadViewModel.ConnectionId, string.Format("#{0}:Error:{1}", Info.SheetData.Name, errorMessage.ToString()));
                    }
                    else
                    {
                        DMLHelper.BrodCast(this.FileUploadViewModel.ConnectionId, string.Format("#{0}:Dataset Processed.", Info.SheetData.Name));
                    }

                    Thread.Sleep(200);
                });
        }
    }
}
