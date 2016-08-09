<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="RunBoard.Detail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<p> test linking to read the session data. You clicked <asp:Label ID="dtlv1" runat="server"></asp:Label> Detail</p>--%>
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
    <p> 
      <div id="VGrid">
        <asp:GridView ID="GridView1" runat="server" BackColor="White" 
            DataSourceID="SqlDataSource1" AutoGenerateColumns="False" 
            BorderStyle="Solid" HorizontalAlign="Center" style="margin-left: 0px" 
        Width="756px">
            
            <Columns>
                <asp:BoundField DataField="RecordDt" HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="RecordDt" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="Total" HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Total&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" SortExpression="Total" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle" />
                <asp:BoundField DataField="Processed" HeaderText="&nbsp;&nbsp;Processed&nbsp;&nbsp;" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="Processed" />
                <asp:BoundField DataField="Unprocessed" HeaderText="&nbsp;&nbsp;Unprocessed&nbsp;&nbsp;" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="Unprocessed" />
                <asp:BoundField DataField="Failed" HeaderText="&nbsp;&nbsp;Failed&nbsp;&nbsp;" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="Failed" />
                <asp:BoundField DataField="Failed_Threshold_Red" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    HeaderText="&nbsp;Failed_LT_Red&nbsp;" SortExpression="Failed_Threshold_Red" />
                <asp:BoundField DataField="Unprocess_Threshold_Red" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle" 
                    HeaderText="&nbsp;Unprocess_LT_Red&nbsp;" SortExpression="Unprocess_Threshold_Red" />
                <asp:BoundField DataField="Failed_Threshold_Amber" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle" 
                    HeaderText="&nbsp;Failed_LT_Amber&nbsp;" SortExpression="Failed_Threshold_Amber" />
                <asp:BoundField DataField="Unprocess_Threshold_Amber" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle" 
                    HeaderText="&nbsp;Unprocess_LT_Amber&nbsp;" 
                    SortExpression="Unprocess_Threshold_Amber" />
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
      </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:rundashboardConnectionString %>" 

            SelectCommand="SELECT datatable.RecordDt, datatable.Total, datatable.Processed, datatable.Unprocessed, datatable.Failed, limittable.Failed_Threshold_Red, limittable.Unprocess_Threshold_Red, limittable.Failed_Threshold_Amber, limittable.Unprocess_Threshold_Amber FROM datatable INNER JOIN limittable ON datatable.Type = limittable.Ttype AND datatable.Type = @Type AND datatable.RecordDt &lt;= @Date AND datatable.RecordDt &gt; DATEADD(dd, -20, @Date) ORDER BY datatable.RecordDt DESC" >
            <SelectParameters>
                <asp:SessionParameter Name="Type" SessionField="detail" />
                 <asp:SessionParameter Name="Date" SessionField="date" />
            </SelectParameters>
        </asp:SqlDataSource>
    </p>

     <p> &nbsp;</p>
      <p> &nbsp;</p>
       <p> &nbsp;</p>
        <p> &nbsp;</p>
</asp:Content>
