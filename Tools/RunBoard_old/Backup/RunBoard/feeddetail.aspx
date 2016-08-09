<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="feeddetail.aspx.cs" Inherits="RunBoard.feeddetail" %>
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

     
    <!-- Datafeed details table  -->
<table width="900">
  <tr>
     
     <td colspan="21" align="center"><h2> Corporate Partners Datafeed </h2>
     </td>
  </tr>
  <tr>
     <td>&nbsp;</td>
  </tr>
  <tr>
     <td>&nbsp;</td>
  </tr>
  <tr>
     <td>&nbsp;</td>
  </tr>
   <tr>
     <td colspan="1"><h3><b>HSBC</b> </h3>
     </td>
     <td colspan="1">
     <asp:Chart ID="Chart1" runat="server" Palette="None" Height="70px" 
            Width="70px">
            <Series>
                <asp:Series ChartType="Pie" Name="Series1">
                    <Points>
                        <asp:DataPoint YValues="1" />
                    </Points>
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
 
     </td> 
     <td colspan="1" align ="right">
             <asp:LinkButton ID="hsbcButton1" runat="server" onclick="hsbc_Click">Graph</asp:LinkButton></td>
     <td colspan="1" align ="center">&nbsp;|&nbsp;</td>
     <td colspan="1">
             <asp:LinkButton ID="hsbcButton2" runat="server" onclick="hsbcdetail_Click">Detail</asp:LinkButton>
      </td>
     <td colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
     
     <td colspan="1"><h3><b>Barclays</b></h3>
     </td>
     <td colspan="1">
     <asp:Chart ID="Chart2" runat="server" Palette="None" Height="70px" 
            Width="70px">
            <Series>
                <asp:Series ChartType="Pie" Name="Series1">
                    <Points>
                        <asp:DataPoint YValues="1" />
                    </Points>
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
 
     </td> 
     
     <td colspan="1" align ="right">
             <asp:LinkButton ID="barcButton1" runat="server" onclick="barc_Click">Graph</asp:LinkButton></td>
     <td colspan="1" align ="center">&nbsp;|&nbsp;</td>
     <td colspan="1">
             <asp:LinkButton ID="barcButton2" runat="server" onclick="barcdetail_Click">Detail</asp:LinkButton>
      </td>
     <td colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

     <td colspan="1"><h3><b>Santander</b></h3>
     </td>
     <td colspan="1">
     <asp:Chart ID="Chart3" runat="server" Palette="None" Height="70px" 
            Width="70px">
            <Series>
                <asp:Series ChartType="Pie" Name="Series1">
                    <Points>
                        <asp:DataPoint YValues="1" />
                    </Points>
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
 
     </td> 
     
     <td colspan="1" align ="right">
             <asp:LinkButton ID="SantButton1" runat="server" onclick="sant_Click">Graph</asp:LinkButton></td>
     <td colspan="1" align ="center">&nbsp;|&nbsp;</td>
     <td colspan="1">
             <asp:LinkButton ID="SantButton2" runat="server" onclick="santdetail_Click">Detail</asp:LinkButton>
      </td>
    </tr> 

</table>

    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />

</asp:Content>
