<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BatchPDetail.aspx.cs" Inherits="RunBoard.BatchPDetail" %>
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
                
                <asp:BoundField DataField="Jobname" HeaderText="Job Name" SortExpression="Jobname" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle" />
                <asp:BoundField DataField="Start_Date" HeaderText="Start Date" SortExpression="Start_Date" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="STIME" HeaderText="Start Time" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="STIME" />
                <asp:BoundField DataField="Error_Date" HeaderText="Error Date" SortExpression="Error_Date" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="ERRTIME" HeaderText="Error Time" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="ERRTIME" />
                <asp:BoundField DataField="Error_Code" HeaderText="Error Code" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="Error_Code" />
                <asp:BoundField DataField="Restart_Date" HeaderText="Restart Date" SortExpression="Restart_Date" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="RSTIME" HeaderText="Restart Time" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="RSTIME" /> 
                <asp:BoundField DataField="End_Date" HeaderText="End Date" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="End_Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="ETIME" HeaderText="End Time" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="ETIME" />
                <asp:BoundField DataField="TLOST" HeaderText="Time Lost" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle"
                    SortExpression="TLOST" /> 
                
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
            
        </asp:GridView>
    </div>
     
    <p> 
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:rundashboardConnectionString %>" 

            SelectCommand="SELECT Jobname, Start_Date, (CONVERT(varchar(2), Start_Time/60)+':'+ CONVERT(varchar(2), Start_Time%60)) AS STIME, Error_Date,(CONVERT(varchar(2), Error_Time/60)+':'+ CONVERT(varchar(2), Error_Time%60)) AS ERRTIME, Error_Code, Restart_Date,(CONVERT(varchar(2), Restart_Time/60)+':'+ CONVERT(varchar(2), Restart_Time%60)) AS RSTIME, End_Date, (CONVERT(varchar(2), End_Time/60)+':'+ CONVERT(varchar(2), End_Time%60)) AS ETIME, (CONVERT(varchar(2), Time_Lost/60)+':'+ CONVERT(varchar(2), Time_Lost%60)) AS TLOST  FROM BatchData WHERE RecordDt &lt;= @Date AND RecordDt &gt; DATEADD(dd, -20, @Date) AND Job_Category = 'BATCHPERF' ORDER BY RecordDt DESC " >
            <SelectParameters>               
                 <asp:SessionParameter Name="Date" SessionField="date" />
            </SelectParameters>
        </asp:SqlDataSource>
    </p>
    

     <p> &nbsp;</p>
      <p> &nbsp;</p>
       <p> &nbsp;</p>
        <p> &nbsp;</p>
</asp:Content>
