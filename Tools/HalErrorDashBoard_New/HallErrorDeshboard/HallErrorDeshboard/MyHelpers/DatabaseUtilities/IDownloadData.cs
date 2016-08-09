using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace MyHelpers
{
    public delegate void DownloadStatusChanged(Hashtable status);

    abstract class IDownloadData
    {
        public event DownloadStatusChanged StatusChanged;
        public DataSet Results;

        abstract public void SetIdentifier(long id);
        abstract public void SetContext(string Context);
        abstract public void SetParameters(MyParameters Param);
        abstract public void SetDBConnect(IDBConnect data);
        abstract public bool Download();
    }
}
