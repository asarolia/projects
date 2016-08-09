<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Editor.aspx.cs" Inherits="Editor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Code Browser</title>
    <link rel="stylesheet" href="~/Images/StyleSheet.css" />
    <link rel="stylesheet"  href="Images/CodeBrowse.css" />
    <link rel="stylesheet" href="~/Images/jquery-ui-1.8.4.css" />
    <link rel="SHORTCUT ICON" href="../Images/logo.ico" />
    <script src='js/jquery/jquery-1.4.2.js' type="text/javascript"></script>
    <script language="javascript" src="js/editor.js" type="text/javascript"></script>

</head>
<body id="mainbody">
    <div id="header">
        <h1>Browsing module : <asp:Label runat="server" id="LabelModuleName"></asp:Label></h1>
    </div>

    <form id="Form1" runat="server">
        <div id="main">
            <div id="sidebar">
                <div>
                </div>
                <fieldset class="collapsible">
                    <legend id="hilight-head" class="expanded" style="cursor:pointer;"><h4>Highlight changes</h4></legend>
                    <div class="group-items-hi" id="hilight">
                            <asp:CheckBoxList AutoPostBack="false" CssClass="TagItems" ID="CheckboxListTag" runat="server"></asp:CheckBoxList>
                    </div>
                </fieldset>
                <!--div>
                    <h4 id="h1" class="expanded" style="cursor:pointer;">Highlight changes</h4>
                    <div class="group-items-hi" id="Div1">
                            <asp:CheckBoxList AutoPostBack="false" CssClass="TagItems" ID="CheckboxList1" runat="server"></asp:CheckBoxList>
                    </div>
                </div-->
            </div>
            <div id="content">
                <div id="sourcecontent">
                    <div runat="server" class="code-box" id="modulecode"> </div>
                </div>
            </div>
            <div id="performanceData" style=" white-space:pre;">
                <div id="performanceStat" runat="server"></div>
            </div>
        </div>
    </form>
</body>
</html>
