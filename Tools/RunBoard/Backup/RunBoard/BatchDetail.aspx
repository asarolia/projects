<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BatchDetail.aspx.cs" Inherits="RunBoard.BatchDetail" %>
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
                
                <asp:BoundField DataField="Jobname" HeaderText="&nbsp;&nbsp;Job Name&nbsp;&nbsp;" SortExpression="Jobname" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle" />
                <asp:BoundField DataField="Start_Date" HeaderText="&nbsp;&nbsp;Start Date&nbsp;&nbsp;" SortExpression="Start_Date" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="STIME" HeaderText="&nbsp;&nbsp;Start Time&nbsp;&nbsp;" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="STIME" />
                <asp:BoundField DataField="End_Date" HeaderText="&nbsp;&nbsp;End Date&nbsp;&nbsp;" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="End_Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="ETIME" HeaderText="&nbsp;&nbsp;End Time&nbsp;&nbsp;" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="ETIME" />
                <asp:BoundField DataField="Comment" HeaderText="&nbsp;&nbsp;Description&nbsp;&nbsp;" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="Comment" />
                
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

            SelectCommand="SELECT Jobname, Start_Date, (CONVERT(varchar(2), Start_Time/60)+':'+ CONVERT(varchar(2), Start_Time%60)) AS STIME, End_Date, (CONVERT(varchar(2), End_Time/60)+':'+ CONVERT(varchar(2), End_Time%60)) AS ETIME, Comment  FROM BatchData WHERE RecordDt = @Date AND Jobname IN ('PXACC048','PXACC050')" >
            <SelectParameters>               
                 <asp:SessionParameter Name="Date" SessionField="date" />
            </SelectParameters>
        </asp:SqlDataSource>
    </p>
    </div>

     <p> &nbsp;</p>
      <p> &nbsp;</p>
      <p> 
         <asp:Label ID="Label1" runat="server" Font-Italic="True" ForeColor="#3366FF"></asp:Label>
    </p>
    <p> 
         <asp:Label ID="Label2" runat="server" Font-Italic="True" ForeColor="#3366FF"></asp:Label>
    </p>
    <p> 
         <asp:Label ID="Label3" runat="server" Font-Italic="True" ForeColor="#3366FF"></asp:Label>
    </p>
       <p> &nbsp;</p>
        <p> &nbsp;</p>
</asp:Content>
