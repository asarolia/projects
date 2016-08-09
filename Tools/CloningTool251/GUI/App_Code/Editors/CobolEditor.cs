using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CobolEditor
/// </summary>
public class CobolEditor : AbstractEditor
{
	public CobolEditor(string FileName):base(FileName)
	{
	}

    protected override void _identifyLineProperties()
    {

        setLineType(_codeLine);
        setLineLevel(_codeLine);
        setLineId(_codeLine);
        setLineTag(_codeLine);

    }

    private void setLineType(CodeLine Code)
    {
        if (Code.Line.Trim().Length == 0)
        {
            Code.Type = "emptyLine";
            return;
        }

        if (Code.Line.Substring(0, 1) == ")") // compiler section
        {
            Code.Type = "compiler";
            return;
        }

        if (Code.Line.Substring(6, 1) == "*") // cobol comment
        {
            Code.Type = "comment";
            return;
        }

        // check if it is starting at 9th character
        if (Code.Line.Substring(6, 1) != "")
        {
            if (Code.Line.ToLower().IndexOf(" division") > -1)
                Code.Type = "division";
            else if (Code.Line.ToLower().IndexOf(" section") > -1)
                Code.Type = "section";
            else if (Code.Line.ToLower().Substring(8, 2) == "01")
                Code.Type = "code";
            else if (Code.Line.ToLower().Substring(8, 2).Trim().Length != 0)
                Code.Type = "paragraph";
            else
                Code.Type = "undefined";
        }
        else
        {
            Code.Type = "code";
        }

    }
    private void setLineLevel(CodeLine Code)
    {
        switch (Code.Type)
        {
            case "division":
                Code.Level = 1;
                break;
            case "section":
                Code.Level = 2;
                break;
            case "code":
                Code.Level = 4;
                break;
            case "comment":
                Code.Level = 5;
                break;
            case "undefined":
                Code.Level = 5;
                break;
        }
    }

    private void setLineId(CodeLine Code)
    {
        Code.Identifier = Code.Type;
    }

    private void setLineTag(CodeLine Code)
    {
        if (Code.Type != "compiler")
            Code.Tag = Code.Line.Substring(0, 6).Trim();
    }

}