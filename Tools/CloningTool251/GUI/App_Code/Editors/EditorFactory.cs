using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

/// <summary>
/// Summary description for EditorFactory
/// </summary>
public class EditorFactory
{
	public EditorFactory()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static AbstractEditor GetEditor(string FileName)
    {
        FileName = Configuration.GetLocalPath(FileName);

        switch (ModuleType(FileName))
        {
            case 1:
                return new CobolEditor(FileName);
        }

        return null;

    }

    private static int ModuleType(string FileName)
    {
        if (!File.Exists(FileName))
            throw new InvalidOperationException(String.Format("'{0}'File Does Not Exists!",FileName));

        return 1; //cobol
    }

}