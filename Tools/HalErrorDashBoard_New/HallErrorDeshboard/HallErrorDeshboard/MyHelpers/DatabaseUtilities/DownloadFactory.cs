using System;
using System.Collections.Generic;
using System.Text;
using MyHelpers.Properties;

namespace MyHelpers
{
    class DownloadFactory
    {
        static public IDownloadData GetInstance(string Context)
        {
            IDownloadData download = new DownloadDataUsingConfig(Resource.DefaultConfiguration);
            return download;
        }
    }

}
