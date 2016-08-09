using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for Instance
/// </summary>
public class Instance
{
    private long id;
	public Instance()
	{
        id = 0;
		//
		// TODO: Add constructor logic here
		//
	}

    public long Generate()
    {
        id = DateTime.Now.Ticks;
        return id;
    }

    public long Get()
    {
        return id;
    }

}
