<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Services.aspx.cs" Inherits="RunBoard.Services" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id = "id2">
    <div class = "al">
     
     <h2> Threshold Options</h2>
     
  </div>
</div>
<div id= "drop">
 <%--<script type="text/javascript" language="javascript">
        function showAddress_Byamit() {
         var e = document.getElementById("Ttype");
         var country = e.options[e.selectedIndex].text;
     }s
</Script>--%>
    <asp:DropDownList ID="DropDownList1" runat="server" 
        DataSourceID="SqlDataSource3" DataTextField="Ttype" DataValueField="Ttype" 
        AutoPostBack="True" onselectedindexchanged="Page_Load" 
        ontextchanged="Page_Load">
       <asp:ListItem></asp:ListItem>
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:rundashboardConnectionString %>" 
        SelectCommand="SELECT DISTINCT [Ttype] FROM [limittable]">
    </asp:SqlDataSource>
</div>
    <div id ="Grid">
    <asp:SqlDataSource ID="Rundash" runat="server" 
        ConnectionString="<%$ ConnectionStrings:rundashboardConnectionString %>" 
        SelectCommand="SELECT DISTINCT Ttype, Failed_Threshold_Red, Unprocess_Threshold_Red, Failed_Threshold_Amber, Unprocess_Threshold_Amber FROM limittable WHERE (Ttype = @testVale)" 
        
            
            
            
            UpdateCommand="UPDATE limittable SET Failed_Threshold_Red = @Failed_Threshold_Red, Unprocess_Threshold_Red = @Unprocess_Threshold_Red, Failed_Threshold_Amber = @Failed_Threshold_Amber, Unprocess_Threshold_Amber = @Unprocess_Threshold_Amber WHERE (Ttype = @Ttype)">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList1" DefaultValue="CARD" 
                Name="testVale" PropertyName="SelectedValue" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="Failed_Threshold_Red" />
            <asp:Parameter Name="Unprocess_Threshold_Red" />
            <asp:Parameter Name="Failed_Threshold_Amber" />
            <asp:Parameter Name="Unprocess_Threshold_Amber" />
            <asp:Parameter Name="Ttype" />
        </UpdateParameters>
    </asp:SqlDataSource>
</div>



    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        AutoGenerateEditButton="True" DataKeyNames="Ttype" DataSourceID="Rundash" 
        BackColor="White" BorderColor="White" BorderStyle="Ridge" 
        BorderWidth="2px" Font-Bold="True" Font-Names="Arial" 
        style="margin-top: 0px" CellPadding="3" CellSpacing="1" GridLines="None" 
        Width="913px">
        <Columns>
            <asp:BoundField DataField="Ttype" HeaderText="Ttype" ReadOnly="True" 
                SortExpression="Ttype" />
            <asp:BoundField DataField="Failed_Threshold_Red" 
                HeaderText="Failed_Threshold_Red" SortExpression="Failed_Threshold_Red" />
            <asp:BoundField DataField="Unprocess_Threshold_Red" 
                HeaderText="Unprocess_Threshold_Red" SortExpression="Unprocess_Threshold_Red" />
            <asp:BoundField DataField="Failed_Threshold_Amber" 
                HeaderText="Failed_Threshold_Amber" SortExpression="Failed_Threshold_Amber" />
            <asp:BoundField DataField="Unprocess_Threshold_Amber" 
                HeaderText="Unprocess_Threshold_Amber" 
                SortExpression="Unprocess_Threshold_Amber" />
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
        <HeaderStyle BackColor="#33CC33" Font-Bold="True" ForeColor="#0033CC" />
        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#FFFF00" ForeColor="Black" />
        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="Black" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#594B9C" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#33276A" />
    </asp:GridView>

</asp:Content>
