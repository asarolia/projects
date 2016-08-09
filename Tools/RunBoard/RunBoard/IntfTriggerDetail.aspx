<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IntfTriggerDetail.aspx.cs" Inherits="RunBoard.IntfTriggerDetail" %>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<p> Date: <asp:TextBox ID="dtlv3" runat="server" Enabled="false"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;
      
            <asp:LinkButton ID="LinkButton1" runat="server" onclick="Button1_Click">&lt;&lt;</asp:LinkButton>
</p>
    <p> &nbsp;</p>
     <p> &nbsp;</p>
     <h1><b> <asp:Label ID="DetailLabel" runat="server"></asp:Label>  </b></h1>
      <p> &nbsp;</p>
      

            <div id="VGrid">
            
        <asp:GridView ID="GridView1" allowpaging="true"  runat="server" BackColor="White"  AutoGenerateColumns="False" BorderStyle="Solid" HorizontalAlign="Center" style="margin-left: 0px" Width="850px"> 
            <Columns>
                <asp:BoundField DataField="Interface" HeaderText="Interface Type" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle" SortExpression="Interface" > 
                </asp:BoundField>
                <asp:BoundField DataField="Count" HeaderText="Count" SortExpression="Count" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle" />
                <asp:BoundField DataField="MaxDate" HeaderText="To Date" SortExpression="MaxDate" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle" /> 
                <asp:BoundField DataField="MinDate" HeaderText="From Date" SortExpression="MinDate" ItemStyle-HorizontalAlign ="Center" ItemStyle-VerticalAlign ="Middle" />
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
        </asp:Content>