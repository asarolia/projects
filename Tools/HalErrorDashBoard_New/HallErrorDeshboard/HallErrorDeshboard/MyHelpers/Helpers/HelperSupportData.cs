using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;
using MyHelpers.Properties;
using System.Data;
using System.IO;
using System.Threading;

namespace MyHelpers
{

    public class HelperSupportData
    {
        #region private variables
        
        string _message;
        bool Errorflag = false; 
        IDBConnect db;
        IDownloadData download;
        public long Identifier = 0;
        public DataSet Results;
        public string ExcelFileLocation;

        #endregion

        public string ErrorMessage
        {
            get
            {
                if (Errorflag)
                    return _message;
                return "";
            }
            set
            {
                Errorflag = true;
                _message = value;
            }
        }
        //public event void UpdateStatus(Dictionary<string, Object> Status);

        #region Public Functions
        public bool Login(MyParameters param)
        {
            GetDatabase(param);

            try
            {
                if (db.Login())
                    return true;
                else
                {
                    SetError("Login", db.ErrorMessage);
                    return false;
                }
            }
            catch (Exception e)
            {
                SetError("Login", e.Message);
                return false;
            }
        }

        public bool StartDownloadAsync(MyParameters param, DownloadStatusChanged statusFunction)
        {
            GetDatabase(param);

            download = DownloadFactory.GetInstance(null);

            download.SetContext(param.Get(MyParameterOptions.WhatIsChanged));
            download.SetParameters(param);
            download.SetDBConnect(db);

            Identifier = new Random().Next(10000, 99999);

            download.SetIdentifier(Identifier);

            //post status
            ObjectPool.Push(Identifier,new DownloadStatus(DownloadStatus.DownloadStatusStages.PreProcessing,0,"Starting download",""));

            if (statusFunction != null)
                download.StatusChanged += statusFunction;


            /*
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerAsync(download);
            */

            ThreadPool.QueueUserWorkItem(ThreadPoolCallBack,download);

            return true;
        }

        public void ThreadPoolCallBack(Object threadContext)
        {
            IDownloadData download = (IDownloadData)threadContext;
            try
            {
                download.Download();
                DataSetHelper.SaveTo(download.Results, new EmbeddedResource().PrependAssemblyPath(string.Format(Resource.SupportDataDownload, Identifier)));

                //download.Results.Dispose();
            }
            catch (Exception ex)
            {
                DownloadStatus status = (DownloadStatus)ObjectPool.PopKeepOne(Identifier);
                status.SetError(ex.Message);
                ObjectPool.Push(Identifier, status);
            }

        }

        void  worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                download.Download();
                DataSetHelper.SaveTo(download.Results, new EmbeddedResource().PrependAssemblyPath(string.Format(Resource.SupportDataDownload, Identifier)));

                //download.Results.Dispose();
            }
            catch (Exception ex)
            {
                DownloadStatus status = (DownloadStatus)ObjectPool.PopKeepOne(Identifier);
                status.SetError(ex.Message);
                ObjectPool.Push(Identifier,status);
            }
        }

        public bool ChangeScheme(long Identifier, MyParameters param)
        {
            try
            {
                File.Copy(new EmbeddedResource().PrependAssemblyPath(string.Format(Resource.SupportDataDownload, Identifier)), new EmbeddedResource().PrependAssemblyPath(string.Format(Resource.SupportDataUpdates, Identifier)), true);

                DataSet ds = DataSetHelper.LoadFrom(new EmbeddedResource().PrependAssemblyPath(string.Format(Resource.SupportDataUpdates, Identifier)));
                ApplyDatasetRules.ChangeScheme(ds, param);

                //ds.AcceptChanges();
                DataSetHelper.SaveTo(ds, new EmbeddedResource().PrependAssemblyPath(string.Format(Resource.SupportDataUpdates, Identifier)));
                return true;
            }
            catch (Exception e)
            {
                SetError("ChangeScheme", e.Message);
                return false;
            }
        }
        public bool ChangeProduct(long Identifier, MyParameters param)
        {
            try
            {
               

                DataSet ds = DataSetHelper.LoadFrom(new EmbeddedResource().PrependAssemblyPath(string.Format(Resource.SupportDataUpdates, Identifier)));
                ApplyDatasetRules.ChangeProduct(ds, param);
                //ds.AcceptChanges();
                DataSetHelper.SaveTo(ds, new EmbeddedResource().PrependAssemblyPath(string.Format(Resource.SupportDataUpdates, Identifier)));
                return true;
            }
            catch (Exception e)
            {
                SetError("ChangeProduct", e.Message);
                return false;
            }
        }

        public bool ChangeMasterCompanyNumber(long Identifier, MyParameters param)
        {
            try
            {
                DataSet ds = DataSetHelper.LoadFrom(new EmbeddedResource().PrependAssemblyPath(string.Format(Resource.SupportDataUpdates, Identifier)));
                ApplyDatasetRules.ChangeMasterCompanyNumber(ds, param);

                //ds.AcceptChanges();
                DataSetHelper.SaveTo(ds, new EmbeddedResource().PrependAssemblyPath(string.Format(Resource.SupportDataUpdates, Identifier)));
                return true;
            }
            catch (Exception e)
            {
                SetError("ChangeMasterCompanyNumber", e.Message);
                return false;
            }
        }
        public bool ChangeEffectiveDate(long Identifier, MyParameters param)
        {
            try
            {
                DataSet ds = DataSetHelper.LoadFrom(new EmbeddedResource().PrependAssemblyPath(string.Format(Resource.SupportDataUpdates, Identifier)));
                ApplyDatasetRules.ChangeEffectiveDate(ds, param);

                //ds.AcceptChanges();
                DataSetHelper.SaveTo(ds, new EmbeddedResource().PrependAssemblyPath(string.Format(Resource.SupportDataUpdates, Identifier)));
                return true;
            }
            catch (Exception e)
            {
                SetError("ChangeEffectiveDate", e.Message);
                return false;
            }
        }
        public bool ChangeExpirationDate(long Identifier, MyParameters param)
        {
            try
            {
                DataSet ds = DataSetHelper.LoadFrom(new EmbeddedResource().PrependAssemblyPath(string.Format(Resource.SupportDataUpdates, Identifier)));
                ApplyDatasetRules.ChangeExpirationDate(ds, param);

                //ds.AcceptChanges();
                DataSetHelper.SaveTo(ds, new EmbeddedResource().PrependAssemblyPath(string.Format(Resource.SupportDataUpdates, Identifier)));
                return true;
            }
            catch (Exception e)
            {
                SetError("ChangeExpirationDate", e.Message);
                return false;
            }
        }
        
        public bool GenerateExcel(long Identifier, MyParameters param,bool ChangesApplied)
        {
            try
            {
                DataSet ds = DataSetHelper.LoadFrom(new EmbeddedResource().PrependAssemblyPath(
                    (ChangesApplied)?
                    string.Format(Resource.SupportDataUpdates, Identifier):
                    string.Format(Resource.SupportDataDownload,Identifier)
                    ));
                DataSetToExcel excel = new DataSetToExcel();
                ExcelFileLocation = new EmbeddedResource().PrependAssemblyPath(String.Format(Resource.ExcelFileLocation, Identifier));
                excel.CreateExcel(ds, ExcelFileLocation);
                return true;
            }
            catch (Exception e)
            {
                SetError("GenerateExcel", e.Message);
                return false;
            }
        }

        public bool GenerateExcel(long Identifier, MyParameters param)
        {
            return GenerateExcel(Identifier, param, true);
        }

        #endregion

        #region Private functions

        

        private void SetError(string context, string message)
        {
            _message = String.Format("{0} function failed with '{1}' message", context, message);
            Errorflag = true;
        }

        private void GetDatabase(MyParameters param)
        {
            if (db != null)
                return;

            db = DatabaseFactory.GetDataBase(null);
            db.SetOptions(param.Get(MyParameterOptions.User),param.Get(MyParameterOptions.Password),param.Get(MyParameterOptions.Region));

            return ;
        }



        #endregion
    }
}
