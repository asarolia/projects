<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detailv.aspx.cs" Inherits="RunBoard.Detailv" %>
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
                
                <asp:BoundField DataField="Feed_Name" HeaderText="&nbsp;&nbsp;Feed Name&nbsp;&nbsp;" SortExpression="Feed_Name" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle" />
                <asp:BoundField DataField="Processed" HeaderText="&nbsp;&nbsp;Processed&nbsp;&nbsp;" SortExpression="Processed" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle" />
                <asp:BoundField DataField="Success" HeaderText="&nbsp;&nbsp;Success&nbsp;&nbsp;" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="Success" />
                <asp:BoundField DataField="Fail" HeaderText="&nbsp;&nbsp;Fail&nbsp;&nbsp;" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="Fail" />
                <asp:BoundField DataField="Failed_Threshold_Red" HeaderText="&nbsp;&nbsp;Failed Red&nbsp;&nbsp;" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="Failed_Threshold_Red" />
                <asp:BoundField DataField="Failed_Threshold_Amber" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    HeaderText="&nbsp;Failed Amber&nbsp;" SortExpression="Failed_Threshold_Red" />
                
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

            SelectCommand="SELECT FeedData.Feed_Name, FeedData.Processed, FeedData.Success, FeedData.Fail,limittable.Failed_Threshold_Red,limittable.Failed_Threshold_Amber  FROM FeedData INNER JOIN limittable ON FeedData.Feed_Type = limittable.Ttype AND FeedData.Feed_Type = @ftype AND RecordDt = @Date" >
            <SelectParameters>               
                 <asp:SessionParameter Name="Date" SessionField="date" />
                 <asp:SessionParameter Name="ftype" SessionField="detail" />
            </SelectParameters>
        </asp:SqlDataSource>
    </p>
    </div>

    <p> &nbsp;</p>
     <h1> Consolidated DataFeed View : <b> <asp:Label ID="Label1" runat="server"></asp:Label>  </b></h1>
    <p> &nbsp;</p>
     <div id="VGrid">
        <asp:GridView ID="GridView2" runat="server" BackColor="#FF9933" 
            DataSourceID="SqlDataSource2" AutoGenerateColumns="False" 
            BorderStyle="Solid" HorizontalAlign="Center" style="margin-left: 0px" 
        Width="756px">
        
            <Columns>
                
                <asp:BoundField DataField="RecordDt" HeaderText="&nbsp;&nbsp;Date&nbsp;&nbsp;" SortExpression="RecordDt" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle" DataFormatString="{0:yyyy-MM-dd}" />                 
                <asp:BoundField DataField="TP" HeaderText="&nbsp;&nbsp;Total Processed&nbsp;&nbsp;" SortExpression="TP" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle" />
                <asp:BoundField DataField="TS" HeaderText="&nbsp;&nbsp;Total Success&nbsp;&nbsp;" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="TS" />
                <asp:BoundField DataField="TF" HeaderText="&nbsp;&nbsp;Total Fail&nbsp;&nbsp;" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="TF" />
                
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
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:rundashboardConnectionString %>" 

            SelectCommand="SELECT RecordDt, SUM(Processed) AS TP, SUM(Success) AS TS, SUM(Fail) AS TF FROM FeedData WHERE Feed_Type = @ftype AND RecordDt &lt;= @Date AND RecordDt &gt; DATEADD(dd, -20, @Date) GROUP BY RecordDt ORDER BY RecordDt DESC" >
            <SelectParameters>               
                 <asp:SessionParameter Name="Date" SessionField="date" />
                 <asp:SessionParameter Name="ftype" SessionField="detail" />
            </SelectParameters>
        </asp:SqlDataSource>
    </p>
    </div>
     <p> &nbsp;</p>
      <p> &nbsp;</p>
       <p> &nbsp;</p>
        <p> &nbsp;</p>
</asp:Content>
