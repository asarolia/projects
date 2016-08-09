using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

/// <summary>
/// Summary description for AbstractCode
/// </summary>
public class SourceCode
{
    private List<CodeLine> _lines;
    protected List<SourceCode> _children;
    protected SourceCode _parent;

    private int Level
    {
        get
        {
            if (_lines.Count > 0)
                return _lines[0].Level;
            return 0;
        }
    }
    private CodeLine Line
    {
        get
        {
            if (_lines.Count > 0)
                return _lines[0];
            return null;
        }
    }
    private string Type
    {
        get
        {
            if (_lines.Count > 0)
                return _lines[0].Type;
            return "";
        }
    }
    private string Identifier
    {
        get
        {
            if (_lines.Count > 0)
                return _lines[0].Identifier;
            return "";
        }
    }

    public SourceCode(CodeLine Line)
	{
        _children = new List<SourceCode>();
        _lines = new List<CodeLine>();
        _lines.Add(Line);
	}

    public void AddLine(CodeLine Line)
    {
        _lines.Add(Line);
    }

    public bool hasChildren()
    {
        if (_children == null)
            return false;
        else
            return true;
    }

    private void AddChild(SourceCode cd)
    {
        if (_children == null)
            _children = new List<SourceCode>();

        _children.Add(cd);
    }

    private void AddParent(SourceCode cd)
    {
        _parent = cd;
    }

    private SourceCode _lastChild()
    {
        if (hasChildren())
            return _children[_children.Count - 1];

        return null;
    }
    

    public SourceCode GetSuitableParent(int ForLevel)
    {
        SourceCode ret;

        ret = this;

        while (ret._parent != null)
        {
            if (ret.Level < ForLevel)
                return ret;

            ret = ret._parent;
        }

        return ret;
    }

    public void AddSibling(SourceCode sibling)
    {
        if (_parent == null)
            throw new InvalidOperationException("Code add sibling error.");

        _parent.AddChild(sibling);
        sibling.AddParent(_parent);
    }

    public static SourceCode AddLine(CodeLine codeLine, SourceCode CurrentLevel)
    {
        if (CurrentLevel == null)
            return new SourceCode(codeLine);

        if (CurrentLevel.Line.EqualKind(codeLine)) // add to same source code set
        {
            CurrentLevel.AddLine(codeLine);
            return CurrentLevel;
        }

        if(CurrentLevel.Line.CompateLevel(codeLine) == 0) // add as sibling
        {
            SourceCode ad = new SourceCode(codeLine);
            CurrentLevel.AddSibling(ad);
            return ad;
        }
        else if (CurrentLevel.Line.CompateLevel(codeLine) > 0) // add as child
        {
            SourceCode ad = new SourceCode(codeLine);
            ad.AddParent(CurrentLevel);
            CurrentLevel.AddChild(ad);
            return ad;
        }
        else if (CurrentLevel.Line.CompateLevel(codeLine) < 0) // add to parents
        {
            SourceCode ad = CurrentLevel.GetSuitableParent(codeLine.Level);
            SourceCode ad1 = new SourceCode(codeLine);
            ad.AddChild(ad1);
            ad1.AddParent(ad);
            return ad1;
        }

        return null;

    }


    public override string ToString()
    {
        StringBuilder str = new StringBuilder();

        _lines.ForEach(
                delegate(CodeLine line)
                {
                    str.AppendLine(line.Line);
                }
                );
        _children.ForEach(delegate(SourceCode item) { str.Append(item.ToString()); });
        return str.ToString();
    }

    public string FormatOutput(AbstractPrinter Printer)
    {
        StringBuilder str = new StringBuilder();

        _lines.ForEach(
                delegate(CodeLine line)
                {
                    if (Printer == null)
                        str.AppendLine(line.Line);
                    else
                        str.AppendLine(Printer.printLine(line));
                }
                );
        _children.ForEach(delegate(SourceCode item) { str.Append(item.FormatOutput(Printer)); });
        return str.ToString();
    }

    public CodeLine[] GetCodeLines()
    {
        List<CodeLine> lines = new List<CodeLine>();
        lines.AddRange(_lines);

        _children.ForEach(delegate(SourceCode item) { lines.AddRange(item.GetCodeLines()); });
        return lines.ToArray();
    }

    public string[] TagList(bool MergeDuplicateTags)
    {
        List<string> list = new List<string>();
        TagList(list);

        if (!MergeDuplicateTags)
        {
            list.Sort();
            return list.ToArray();
        }

        Dictionary<string,int> indexed = new Dictionary<string,int>();
        string[] tags = list.ToArray();
        list = new List<string>();

        foreach(string tag in tags)
        {
            int position=0;
            string tagPrefix= "";

            if (indexed.ContainsKey(tag.Substring(1)))
            {
                position = indexed[tag.Substring(1)];
                tagPrefix = list[position] + ",";
            }
            tagPrefix = tagPrefix + tag;

            if (position > 0)
            {
                list[position] = tagPrefix;
            }
            else
            {
                list.Add(tagPrefix);
                indexed.Add(tag.Substring(1), list.Count - 1);
            }
        }
        list.Sort();
        return list.ToArray();
    }

    public void TagList(List<string> list)
    {
        _lines.ForEach(
                delegate(CodeLine line)
                {
                    if (!list.Contains(line.Tag) && line.Tag.Length > 0)
                        list.Add(line.Tag);
                }
            );

        _children.ForEach(delegate(SourceCode item) { item.TagList(list); });
    }
}