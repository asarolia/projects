using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for DownloadLocation
/// </summary>
public class DownloadLocation
{
	public DownloadLocation()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public bool Success = true;
    public String ErrorText = "";
    public String Location = "";

    public void SetError(String ErrorMessage)
    {
        ErrorText = ErrorMessage;
        Success = false;
    }

    public void SetDownloadLocation(String Text)
    {
        Success = true;
        ErrorText = "";

        Location = Text;
    }

}