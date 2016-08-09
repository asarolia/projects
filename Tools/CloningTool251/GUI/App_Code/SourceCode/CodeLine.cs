using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CodeLine
/// </summary>
public class CodeLine
{

    protected string _line = "", _line_tag="", _line_type="", _line_identifier="";
    protected int _line_level=0;

    public string Line
    {
        get { return _line; }
        set { _line = value; }
    }
    public string Tag
    {
        get { return _line_tag; }
        set { _line_tag = value; }
    }
    public string Type
    {
        get { return _line_type; }
        set { _line_type = value; }
    }
    public string Identifier
    {
        get { return _line_identifier; }
        set { _line_identifier = value; }
    }
    public int Level
    {
        get { return _line_level; }
        set { _line_level = value; }
    }

    public CodeLine()
    {
    }
    
    public CodeLine(string Line,string Tag,string Type, string Identifier, int Level)
    {
        this.Line = Line;
        this.Tag = Tag;
        this.Type = Type;
        this.Identifier = Identifier;
        this.Level = Level;
	}

    public void Reset()
    {
        Line = "";
        Tag = "";
        Type = "";
        Identifier = "";
        Level = 0;
    }

    public bool EqualType(CodeLine code)
    {
        return (this.Type == code.Type);
    }
    public bool EqualKind(CodeLine code)
    {
        return (this.Tag == code.Tag && this.Type == code.Type && this.Identifier == code.Identifier);
    }
    public int CompateLevel(CodeLine code)
    {
        return this.Level.CompareTo(code.Level);
    }
}