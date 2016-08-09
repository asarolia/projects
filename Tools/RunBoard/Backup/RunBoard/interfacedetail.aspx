<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="interfacedetail.aspx.cs" Inherits="RunBoard.interfacedetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style4
        {
            font-family: "Bookman Old Style";
            font-weight: bold;
            font-size: large;
        }
        .style6
        {
            font-family: "Bookman Old Style";
            font-size: large;
        }
    </style>
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
     
     <td colspan="21" align="center"><h2> Interface Trigger Details </h2>
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
     <td colspan="1"><span class="style4">Unprocessed Triggers</span>
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
     
     <td colspan="1">
             <asp:LinkButton ID="unptrgbutton" runat="server" onclick="unptrg_Click">Detail</asp:LinkButton>
      </td>
     <td colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
     
     <td colspan="1" class="style6"><strong>Errored Triggers</strong></td>
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
     
     <td colspan="1">
             <asp:LinkButton ID="errtrgbutton" runat="server" onclick="errtrg_Click">Detail</asp:LinkButton>
      </td>
     <td colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>

     <td colspan="1" class="style4">Pending Print Triggers (Status P)</td>
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
     
     
     <td colspan="1">
             <asp:LinkButton ID="prnttrgbutton" runat="server" onclick="prnttrg_Click">Detail</asp:LinkButton>
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
