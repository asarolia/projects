using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for AbstractEditor
/// </summary>
public class AbstractEditor
{
    string _file;
    SourceCode _parsedCode,_currentCode;
    protected CodeLine _codeLine;

    public string ModuleName
    {
        get {
            return Path.GetFileNameWithoutExtension(_file);
        }
    }

	public AbstractEditor(string FileName)
	{
        _file = FileName;
	}

    public string RawOutput()
    {
        string ret;
        using (StreamReader sr = new StreamReader(_file))
        {
            ret = sr.ReadToEnd();
        }
        return ret;
    }

    public void BuildHierarchy()
    {
        using (StreamReader sr = new StreamReader(_file))
        {
            while (!sr.EndOfStream)
            {
                _codeLine = new CodeLine();
                _codeLine.Line = sr.ReadLine();

                _identifyLineProperties();
                _addCodeLine();
            }
        }
    }

    public string PrintOutput(AbstractPrinter Printer)
    {
        StringBuilder str = new StringBuilder();

        str.Append(Printer.printHeader());
        str.Append(_parsedCode.FormatOutput(Printer));
        str.Append(Printer.printTrailer());

        return str.ToString();
    }

    public string PrintOutputV2(AbstractPrinter Printer)
    {
        StringBuilder str = new StringBuilder();

        str.Append(Printer.printHeader());
        CodeLine[] lines = _parsedCode.GetCodeLines();

        foreach(CodeLine line in lines)
            str.AppendLine(Printer.printLine(line));

        str.Append(Printer.printTrailer());

        return str.ToString();
    }

    private void _addCodeLine()
    {
        if (_parsedCode == null)
        {
            _parsedCode = SourceCode.AddLine(new CodeLine("", "", "","", 0),null);
            _currentCode = _parsedCode;
        }

        _currentCode = SourceCode.AddLine(_codeLine,_currentCode);
    }

    protected virtual void _identifyLineProperties()
    {
    }

    public string[] TagList(bool MergeDuplicateTags)
    {
        return _parsedCode.TagList(MergeDuplicateTags);
    }
}