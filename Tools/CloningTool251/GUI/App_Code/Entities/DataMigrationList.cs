using System;
using System.Collections.Generic;
using System.Web;
using System.IO;

/// <summary>
/// Summary description for DataMigrationList
/// </summary>
public class DataMigrationList
{
    public string[] Parts;

    public DataMigrationList()
	{
	}

    public DataMigrationList(string fileName)
    {
        string file = Path.GetFileNameWithoutExtension(fileName);
        Parts = file.Split('.');
    }
}