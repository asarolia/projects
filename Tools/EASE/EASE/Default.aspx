<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EASE._Default"  %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">--%>
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <title>EASE</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <!--[if IE]> 
    <link rel="stylesheet" type="text/css" href="~/Styles/ie.css" media="screen" />
    <![endif]-->
   <%-- <asp:ContentPlaceHolder ID="HeadContent" runat="server">
    </asp:ContentPlaceHolder>--%>
   
</head>
<body>
    <form id="Form1" runat="server">
    
    <div class="page">
        <div class="header">         
            
                <h1>
                    EASE - Exceed Automated Search Engine
                </h1>  
            
 
        </div>
        <div class="main"> 
        <table width="800px">
          <tr><td colspan="3">&nbsp;</td>
          </tr>
          <tr><td colspan="3">&nbsp;</td>
          </tr>
          <tr>
             <td colspan="1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
             <td colspan="1" align="center"><p>Enter What You Are Looking For!</p>
             </td>
             <td colspan="1">&nbsp;</td>
          </tr>
          <tr><td colspan="3">&nbsp;</td>
          </tr>
          <tr>
             <td colspan="1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
             <td colspan="1" align="center"><p><asp:TextBox ID="TextBox1" runat="server" Width="520px"></asp:TextBox></p></td>
             <td colspan="1" align="center"><p><asp:Button ID="Button1" runat="server" onclick="Search_Click" 
            style="margin-left: 0px" Width="109px" Text="Search" /></p></td>
          </tr>
          <tr><td>&nbsp;</td>
          </tr>
          <tr><td>
              <br />
              <br />
              <br />
              <br />
              <br />
              <br />
              <br />
              </td>
          </tr>
        
        </table>           
   
        </div>
      
    </div>
   
    </form>
</body>
</html>