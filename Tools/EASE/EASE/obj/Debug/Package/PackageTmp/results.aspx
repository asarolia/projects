<%@ Page Title="EASE" Language="C#" AutoEventWireup="true" CodeBehind="results.aspx.cs" Inherits="EASE.results" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
--%>
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <title>EASE</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <!--[if IE]> 
    <link rel="stylesheet" type="text/css" href="~/Styles/ie.css" media="screen" />
    <![endif]-->
    <%--<asp:ContentPlaceHolder ID="HeadContent" runat="server">
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
           <td colspan="3"><p><asp:Label ID="label1" runat="server"></asp:Label></p></td>
           
           </tr>
           <tr>
           <td colspan="3"><asp:PlaceHolder ID="PH1"  runat="server">
                  </asp:PlaceHolder>
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
