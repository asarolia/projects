using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

/// <summary>
/// Summary description for HTMLPrinter
/// </summary>
public class HTMLPrinter : AbstractPrinter
{
    CodeLine _previousCode;

	public HTMLPrinter() : base()
	{
	}

    public override string printHeader()
    {
        return "";
    }

    public override string printTrailer()
    {
        return ClosingLine().ToString();
    }

    public override string printLine(CodeLine Line)
    {
        StringBuilder str = new StringBuilder();

        if (_previousCode == null)
            str.Append(OpeningLine(Line));
        else if (!_previousCode.EqualKind(Line))
        {
            str.Append(ClosingLine());
            str.Append(OpeningLine(Line));
        }

        str.Append(Line.Line);

        _previousCode = Line;

        return str.ToString();
    }

    private StringBuilder OpeningLine(CodeLine Line)
    {
        StringBuilder str = new StringBuilder();
        str.Append("<div class='code-type-" + Line.Type + "'>");
        str.Append("<div class='code-tag-" + (Line.Tag.Trim().Length == 0 ?"none":Line.Tag.Trim()) + "'>");
        str.Append("<div class='code-line'>");
        return str;
    }

    private StringBuilder ClosingLine()
    {
        StringBuilder str = new StringBuilder();
        str.Append("</div>");
        str.Append("</div>");
        str.Append("</div>");
        return str;
    }
}