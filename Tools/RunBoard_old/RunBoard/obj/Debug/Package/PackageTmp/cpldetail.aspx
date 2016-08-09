<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cpldetail.aspx.cs" Inherits="RunBoard.cpldetail" %>
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
     
     <td colspan="21" align="center"><h2> Claims Purchase Ledger </h2>
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
     <td colspan="1"><h3><b>CPL1 Stage1</b> </h3>
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
             <asp:LinkButton ID="cpl1stg1Button1" runat="server" onclick="cpl1stg1_Click">Graph</asp:LinkButton></td>
     <td colspan="1" align ="center">&nbsp;|&nbsp;</td>
     <td colspan="1">
             <asp:LinkButton ID="cpl1stg1Button2" runat="server" onclick="cpl1stg1detail_Click">Detail</asp:LinkButton>
      </td>
     <td colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
     
     <td colspan="1"><h3><b>CPL1 Stage2</b></h3>
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
             <asp:LinkButton ID="cpl1stg2Button1" runat="server" onclick="cpl1stg2_Click">Graph</asp:LinkButton></td>
     <td colspan="1" align ="center">&nbsp;|&nbsp;</td>
     <td colspan="1">
             <asp:LinkButton ID="cpl1stg2Button2" runat="server" onclick="cpl1stg2detail_Click">Detail</asp:LinkButton>
      </td>
     <td colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

     <td colspan="1"><h3><b>CPL2/FRCF</b></h3>
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
             <asp:LinkButton ID="cpl2Button1" runat="server" onclick="cpl2_Click">Graph</asp:LinkButton></td>
     <td colspan="1" align ="center">&nbsp;|&nbsp;</td>
     <td colspan="1">
             <asp:LinkButton ID="cpl2Button2" runat="server" onclick="cpl2detail_Click">Detail</asp:LinkButton>
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
