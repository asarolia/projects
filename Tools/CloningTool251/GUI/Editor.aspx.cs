using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Editor : System.Web.UI.Page
{
    private AbstractEditor _editor;

    protected void Page_Load(object sender, EventArgs e)
    {
        string fileName = Request.QueryString["file"];

        Logger logger = new Logger();

        //build code
         logger.LogTimeBetweenEvents("cobol hierarchy build");
        
        _editor = EditorFactory.GetEditor(fileName);
        _editor.BuildHierarchy();

        //set module name
        LabelModuleName.Text = _editor.ModuleName;

        //move code to output
        logger.LogTimeBetweenEvents("generate output html");
        //modulecode.InnerHtml = _editor.PrintOutput(new HTMLPrinter());
        modulecode.InnerHtml = _editor.PrintOutputV2(new HTMLPrinter());
        logger.EndEvent();

        //fill tag list
        logger.LogTimeBetweenEvents("fetch tag list");
        string[] items = _editor.TagList(true);
        logger.EndEvent();

        ListItemCollection listitems = new ListItemCollection();
        foreach(string item in items)
        {
            listitems.Add(new ListItem(item,"value"));
        }
        foreach(ListItem item in listitems)
            CheckboxListTag.Items.Add(item);

        _editor = null;

        performanceStat.InnerText = logger.Flush();
    }
}