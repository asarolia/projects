<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Extract.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
    </div>    
    <table>
        <tr>
        <td>Release: 
            <asp:DropDownList ID="drRelease" runat="server" >
            <asp:ListItem Text="GI112"></asp:ListItem>
            <asp:ListItem Text="GI212"></asp:ListItem>
            <asp:ListItem Text="GI312"></asp:ListItem>
            <asp:ListItem Text="GI412"></asp:ListItem>
            </asp:DropDownList>
        </td>
            <td>
                <asp:Button ID = "btnExtract" Text = "Extract" runat="server" OnClick="btnExtract_Click" />
            </td>
        </tr>   
    </table>
    </form>
</body>
</html>
