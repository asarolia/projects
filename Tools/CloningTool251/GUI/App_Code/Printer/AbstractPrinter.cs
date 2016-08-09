using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AbstractPrinter
/// </summary>
public class AbstractPrinter
{
	public AbstractPrinter()
	{
	}

    public virtual string printLine(CodeLine Line)
    {
        return "";
    }

    public virtual string printHeader()
    {
        return "";
    }

    public virtual string printTrailer()
    {
        return "";
    }

}