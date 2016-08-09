<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HalDetail.aspx.cs" Inherits="RunBoard.HalDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<p> Date: <asp:TextBox ID="dtlv3" runat="server" Enabled="false"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <%--<asp:Button ID="Button1" runat="server" Text="&lt;&lt;" Width="87px" 
        onclick="Button1_Click" />--%>
    <asp:LinkButton ID="LinkButton1" runat="server" onclick="Button1_Click">&lt;&lt;</asp:LinkButton>
</p>
    <p> &nbsp;</p>
     <p> &nbsp;</p>
     <h1> Data View : <b> <asp:Label ID="dtlv1" runat="server"></asp:Label>  </b></h1>
    <p> &nbsp;</p>
      <div id="VGrid">
        <asp:GridView ID="GridView1" runat="server" BackColor="#FF9933" 
            DataSourceID="SqlDataSource1" AutoGenerateColumns="False" 
            BorderStyle="Solid" HorizontalAlign="Center" style="margin-left: 0px" 
        Width="756px">
           
            <Columns>
                
                <asp:BoundField DataField="Error_Ref" HeaderText="&nbsp;&nbsp;Error Ref&nbsp;&nbsp;" SortExpression="Error_Ref" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle" />
                <asp:BoundField DataField="Day_O_Count" HeaderText="&nbsp;&nbsp;Day 1&nbsp;&nbsp;" SortExpression="Day_O_Count" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle" />
                <asp:BoundField DataField="Day_T_Count" HeaderText="&nbsp;&nbsp;Day 2&nbsp;&nbsp;" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="Day_T_Count" />
                <asp:BoundField DataField="Day_Th_Count" HeaderText="&nbsp;&nbsp;Day 3&nbsp;&nbsp;" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="Day_Th_Count" />
                <asp:BoundField DataField="Fail_Pgm_Name" HeaderText="&nbsp;&nbsp;Program Name&nbsp;&nbsp;" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="Fail_Pgm_Name" />
                <asp:BoundField DataField="Fail_Para_Name" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    HeaderText="&nbsp;Para Name&nbsp;" SortExpression="Fail_Para_Name" />
                
            </Columns>
            <EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
            <HeaderStyle Font-Bold="True" BackColor="#33CC33" ForeColor="#0033CC" />
            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFF00" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
            <%--<EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderStyle Font-Bold="True" BackColor="#993300" ForeColor="White" />--%>
        </asp:GridView>
     
    <p> 
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:rundashboardConnectionString %>" 

            SelectCommand="SELECT Error_Ref, Day_O_Count, Day_T_Count, Day_Th_Count, Fail_Pgm_Name, Fail_Para_Name  FROM HalErr WHERE RecordDt = @Date " >
            <SelectParameters>               
                 <asp:SessionParameter Name="Date" SessionField="date" />
            </SelectParameters>
        </asp:SqlDataSource>
    </p>
    </div>

     <p> &nbsp;</p>
      <p> &nbsp;</p>
       <p> &nbsp;</p>
        <p> &nbsp;</p>
</asp:Content>
