<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RunBoard.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%--<div id = "id2">
    <div class = "al">
     
     <h2> &nbsp;Login Screen </h2>
     
  </div>
</div>--%>
<div id = "id1" >
<br />
<br />
<asp:Label ID="Label2" runat="server"  Text="Username"></asp:Label>
&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txt1" runat="server"></asp:TextBox>
       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txt1" ErrorMessage="Enter username"></asp:RequiredFieldValidator>--%>
    <br />
    <br />
    <asp:Label ID="Label3" runat="server"  Text="Password" ></asp:Label>
&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txt2" runat="server" TextMode="Password"></asp:TextBox>
        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="txt2" ErrorMessage="Enter password"></asp:RequiredFieldValidator>--%>
    <br />
    <br />
&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btn3" runat="server" Text="Login" onclick="btn3_Click"/>
&nbsp;
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Cancel" />
    <br />
    <br />
    <asp:Label ID="lbl3" runat="server"></asp:Label>
</div>
<div id = "div2"></div>
</asp:Content>
