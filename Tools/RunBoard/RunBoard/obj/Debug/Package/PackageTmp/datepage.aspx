<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="datepage.aspx.cs" Inherits="RunBoard.datepage" %>
<%@ register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <style type="text/css">
    .style5
    {
        font-family: "Bookman Old Style";
        font-weight: bold;
        font-size: large;
    }
    .style6
    {
        font-family: "Bookman Old Style";
        font-weight: bold;
    }
    .style7
    {
        font-size: large;
    }
    .style8
    {
        font-family: "Bookman Old Style";
        font-weight: bold;
        font-size: large;
        width: 128px;
    }
    .style9
    {
        width: 169px;
        height: 77px;
    }
    .style10
    {
            font-family: "Bookman Old Style";
            font-size: x-large;
            text-decoration: underline;
            height: 86px;
        }
    .style11
    {
        font-size: large;
        font-weight: bold;
        font-family: "Berlin Sans FB";
    }
    .style12
    {
            font-family: "Bookman Old Style";
            font-weight: bold;
            font-size: large;
            width: 128px;
            height: 82px;
        }
    .style13
    {
        height: 82px;
    }
    .style14
    {
        font-family: "Bookman Old Style";
        font-weight: bold;
        font-size: large;
        height: 82px;
    }
    .style15
    {
        height: 77px;
    }
    .style16
    {
        font-family: "Bookman Old Style";
        font-weight: bold;
        font-size: large;
        height: 77px;
    }
    .style17
    {
            font-family: "Bookman Old Style";
            font-weight: bold;
            font-size: large;
            width: 128px;
            height: 84px;
        }
    .style18
    {
        height: 84px;
    }
    .style19
    {
        font-family: "Bookman Old Style";
        font-weight: bold;
        font-size: large;
        height: 84px;
    }
    .style20
    {
        font-family: "Bookman Old Style";
        font-weight: bold;
        font-size: large;
        width: 128px;
        height: 83px;
    }
    .style21
    {
        height: 83px;
    }
        .style23
        {
            height: 82px;
            width: 59px;
        }
        .style24
        {
            height: 77px;
            width: 59px;
        }
        .style25
        {
            height: 84px;
            width: 59px;
        }
        .style26
        {
            height: 83px;
            width: 59px;
        }
        .style27
        {
            width: 59px;
        }
        .style28
        {
            width: 128px;
            height: 77px;
        }
        .style30
        {
            font-family: "Bookman Old Style";
            font-size: x-large;
            text-decoration: underline;
            height: 62px;
        }
        .style31
        {
            height: 62px;
        }
    </style>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  EnablePageMethods="true">
        </asp:ToolkitScriptManager>
<table width="900">
  <tr>
     <td colspan="1"><p><span class="style11">Date :</span>  <!--<asp:label ID="seldate" runat="server"/>-->
             <asp:TextBox ID="seldate1"  runat="server"/>
             <asp:Image ID="calImage1"  ImageUrl="~/Images/Calendar_scheduleHS.png" runat="server" style="height: 16px; width: 16px"/>
             <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="seldate1" PopupButtonID="calImage1" Format="yyyy/MM/dd" runat="server">
             </asp:CalendarExtender>
             &nbsp;&nbsp;<asp:LinkButton ID="refresh1" runat="server" onclick="refresh_Click">GO</asp:LinkButton>
             </p>
     </td>     
     <td colspan="1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     </td>
     <td colspan="1" align="right"><asp:LinkButton ID="threshold" runat="server" onclick="thresh_Click">Update Thresholds</asp:LinkButton> 
     </td>
  </tr>
</table>
<%--
 <p> Date : 
             <asp:TextBox ID="seldate1" runat="server"/>
             <asp:Image ID="calImage1"  ImageUrl="~/Images/Calendar_scheduleHS.png" runat="server"/>
             <asp:CalendarExtender ID="CalendarExtender1"  TargetControlID="seldate1" PopupButtonID="calImage1" Format="yyyy/MM/dd" runat="server">
             </asp:CalendarExtender>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:LinkButton ID="refresh1" runat="server" onclick="refresh_Click">GO</asp:LinkButton>


     
 </p>
--%>
<table width="900">
  <tr>
     
     <td colspan="12" align="center" class="style31">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span 
             class="style9"><span class="style10"><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Policy Area&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         </strong></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;&nbsp;&nbsp;</td>
     <td colspan="4" class="style31"></td> 
     <td colspan="12" align="center" class="style30"><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Claims Area&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</strong></td> 
  </tr>
  <tr>
     <td colspan="8" class="style12">Print Status</td>
     <td colspan="1" class="style23">
      &nbsp;&nbsp;
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
     <td colspan="1" class="style13" align="right"><%--<asp:Button ID="graph" runat="server" BackColor="White" Font-Underline="True" 
          Text="Graph" ForeColor="#0099FF" 
             onclick="graph_Click"></asp:Button>--%>
             <asp:LinkButton ID="graph1" runat="server" onclick="graph_Click">Graph</asp:LinkButton></td>
     <td colspan="1" class="style13" align ="center">&nbsp;|&nbsp;</td>
     <td colspan="1" class="style13"><%--<asp:Button ID="detail" runat="server" BackColor="White" Font-Underline="True" 
         Text="Detail" ForeColor="#0099FF" 
             onclick="detail_Click"></asp:Button>--%>
             <asp:LinkButton ID="detail1" runat="server" onclick="detail_Click">Detail</asp:LinkButton>
      </td>
     <td colspan="4" class="style13">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
     <td colspan="8" class="style14">PITS</td>
     <td colspan="1" class="style13">
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
     
     <td colspan="1" class="style13" align="right"><%--<asp:Button ID="pitsgraph" runat="server" BackColor="White" Font-Underline="True" 
         Text="Graph" ForeColor="#0099FF" 
             onclick="pits_Click"></asp:Button>--%>
             <asp:LinkButton ID="pitsgraph1" runat="server" onclick="pits_Click">Graph</asp:LinkButton></td>
     <td colspan="1" class="style13" align ="center">&nbsp;|&nbsp;</td>
     <td colspan="1" class="style13"><%--<asp:Button ID="pitsdetail" runat="server" BackColor="White" Font-Underline="True" 
         Text="Detail" ForeColor="#0099FF" 
             onclick="pitsdetail_Click"></asp:Button>--%>
             <asp:LinkButton ID="pitsdetail1" runat="server" onclick="pitsdetail_Click">Detail</asp:LinkButton>
      </td>
  </tr>

<!-- oracle-->
<tr>
     <td colspan="8" class="style28"><span class="style5">Oracle AR Processing</span>
     </td>
     <td colspan="1" class="style24">
     &nbsp;&nbsp;
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
     <td colspan="1" align ="right" class="style15"><%--<asp:Button ID="orac" runat="server" BackColor="White" Font-Underline="True" 
         Text="Graph" ForeColor="#0099FF" 
             onclick="orac_Click"></asp:Button>--%>
             <asp:LinkButton ID="orac1" runat="server" onclick="orac_Click">Graph</asp:LinkButton></td>
     <td colspan="1" align ="center" class="style15">&nbsp;|&nbsp;</td>
     <td colspan="1" class="style15"><%--<asp:Button ID="oracdetail" runat="server" BackColor="White" Font-Underline="True" 
         Text="Detail" ForeColor="#0099FF" 
             onclick="oracdetail_Click"></asp:Button>--%>
             <asp:LinkButton ID="oracdetail1" runat="server" onclick="oracdetail_Click">Detail</asp:LinkButton>
      </td>
     <td colspan="4" class="style15">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
     <td colspan="8" class="style16" >WAMI</td>
     <td colspan="1" class="style15">
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:Chart ID="Chart4" runat="server" Palette="None" Height="70px" 
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
     <td colspan="1" align ="right" class="style15"><%--<asp:Button ID="wami" runat="server" BackColor="White" Font-Underline="True" 
         Text="Graph" ForeColor="#0099FF" 
             onclick="wami_Click"></asp:Button>--%>
             <asp:LinkButton ID="wami1" runat="server" onclick="wami_Click">Graph</asp:LinkButton></td>
     <td colspan="1" align ="center" class="style15">&nbsp;|&nbsp;</td>
     <td colspan="1" class="style15"><%--<asp:Button ID="wamidetail" runat="server" BackColor="White" Font-Underline="True" 
         Text="Detail" ForeColor="#0099FF" 
             onclick="wamidetail_Click"></asp:Button>--%>
             <asp:LinkButton ID="wamidetail1" runat="server" onclick="wamidetail_Click">Detail</asp:LinkButton>
      </td>
  </tr>

<!-- Card row -->
<tr>
     <td colspan="8" class="style17">Card Transactions</td>
     <td colspan="1" class="style25">
     &nbsp;&nbsp;
     <asp:Chart ID="Chart5" runat="server" Palette="None" Height="70px" 
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
     <td colspan="1" align ="right" class="style18"><%--<asp:Button ID="card" runat="server" BackColor="White" Font-Underline="True" 
         Text="Graph" ForeColor="#0099FF" 
             onclick="card_Click"></asp:Button>--%>
             <asp:LinkButton ID="card1" runat="server" onclick="card_Click">Graph</asp:LinkButton></td>
     <td colspan="1" align ="center" class="style18">&nbsp;|&nbsp;</td>
     <td colspan="1" class="style18"><%--<asp:Button ID="carddetail" runat="server" BackColor="White" Font-Underline="True" 
         Text="Detail" ForeColor="#0099FF" 
             onclick="carddetail_Click"></asp:Button>--%>
             <asp:LinkButton ID="carddetail1" runat="server" onclick="carddetail_Click">Detail</asp:LinkButton>
      </td>
     <td colspan="4" class="style18">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
     <td colspan="8" class="style19">SEPS</td>
     <td colspan="1" class="style18">
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:Chart ID="Chart6" runat="server" Palette="None" Height="70px" 
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
     <td colspan="1" align ="right" class="style18"><%--<asp:Button ID="seps" runat="server" BackColor="White" Font-Underline="True" 
         Text="Graph" ForeColor="#0099FF" 
             onclick="seps_Click"></asp:Button>--%>
             <asp:LinkButton ID="seps1" runat="server" onclick="seps_Click">Graph</asp:LinkButton></td>
     <td colspan="1" align ="center" class="style18">&nbsp;|&nbsp;</td>
     <td colspan="1" class="style18"><%--<asp:Button ID="sepsdetail" runat="server" BackColor="White" Font-Underline="True" 
         Text="Detail" ForeColor="#0099FF" 
             onclick="sepsdetail_Click"></asp:Button>--%>
             <asp:LinkButton ID="sepsdetail1" runat="server" onclick="sepsdetail_Click">Detail</asp:LinkButton>
      </td>
  </tr>

<!-- Renewals row -->
<tr>
     <td colspan="8" class="style12">Renewals</td>
     <td colspan="1" class="style23">
     &nbsp;&nbsp;
     <asp:Chart ID="Chart7" runat="server" Palette="None" Height="70px" 
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
     <td colspan="1" align ="right" class="style13"><%--<asp:Button ID="rnwl" runat="server" BackColor="White" Font-Underline="True" 
         Text="Graph" ForeColor="#0099FF" 
             onclick="rnwl_Click"></asp:Button>--%>
             <asp:LinkButton ID="rnwl1" runat="server" onclick="rnwl_Click">Graph</asp:LinkButton></td>
     <td colspan="1" align ="center" class="style13">&nbsp;|&nbsp;</td>
     <td colspan="1" class="style13"><%--<asp:Button ID="rnwldetail" runat="server" BackColor="White" Font-Underline="True" 
         Text="Detail" ForeColor="#0099FF" 
             onclick="rnwldetail_Click"></asp:Button>--%>
             <asp:LinkButton ID="rnwldetail1" runat="server" onclick="rnwldetail_Click">Detail</asp:LinkButton>
      </td>
     <td colspan="4" class="style13">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
     <td colspan="8" class="style14">GFIF</td>
     <td colspan="1" class="style13">
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:Chart ID="Chart8" runat="server" Palette="None" Height="70px" 
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
     <td colspan="1" align ="right" class="style13"><%--<asp:Button ID="gfif" runat="server" BackColor="White" Font-Underline="True" 
         Text="Graph" ForeColor="#0099FF" 
             onclick="gfif_Click"></asp:Button>--%>
             <asp:LinkButton ID="gfif1" runat="server" onclick="gfif_Click">Graph</asp:LinkButton></td>
     <td colspan="1" align ="center" class="style13">&nbsp;|&nbsp;</td>
     <td colspan="1" class="style13"><%--<asp:Button ID="gfifdetail" runat="server" BackColor="White" Font-Underline="True" 
         Text="Detail" ForeColor="#0099FF" 
             onclick="gfifdetail_Click"></asp:Button>--%>
             <asp:LinkButton ID="gfifdetail1" runat="server" onclick="gfifdetail_Click">Detail</asp:LinkButton>
      </td>
  </tr>

  <!-- Email row -->
<tr>
     <td colspan="8" class="style17">Email Processing</td>
     <td colspan="1" class="style25">
     &nbsp;&nbsp;
     <asp:Chart ID="Chart9" runat="server" Palette="None" Height="70px" 
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
     <td colspan="1" align ="right" class="style18"><%--<asp:Button ID="email" runat="server" BackColor="White" Font-Underline="True" 
         Text="Graph" ForeColor="#0099FF" 
             onclick="email_Click"></asp:Button>--%>
             <asp:LinkButton ID="email1" runat="server" onclick="email_Click">Graph</asp:LinkButton></td>
     <td colspan="1" align ="center" class="style18">&nbsp;|&nbsp;</td>
     <td colspan="1" class="style18"><%--<asp:Button ID="emaildetail" runat="server" BackColor="White" Font-Underline="True" 
         Text="Detail" ForeColor="#0099FF" 
             onclick="emaildetail_Click"></asp:Button>--%>
             <asp:LinkButton ID="emaildetail1" runat="server" onclick="emaildetail_Click">Detail</asp:LinkButton>
      </td>
     <td colspan="4" class="style18">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
     <td colspan="8" class="style19">ANOL</td>
     <td colspan="1" class="style18">
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:Chart ID="Chart10" runat="server" Palette="None" Height="70px" 
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
     <td colspan="1" align ="right" class="style18"><%--<asp:Button ID="anol" runat="server" BackColor="White" Font-Underline="True" 
         Text="Graph" ForeColor="#0099FF" 
             onclick="anol_Click"></asp:Button>--%>
             <asp:LinkButton ID="anol1" runat="server" onclick="anol_Click">Graph</asp:LinkButton></td>
     <td colspan="1" align ="center" class="style18">&nbsp;|&nbsp;</td>
     <td colspan="1" class="style18"><%--<asp:Button ID="anoldetail" runat="server" BackColor="White" Font-Underline="True" 
         Text="Detail" ForeColor="#0099FF" 
             onclick="anoldetail_Click"></asp:Button>--%>
             <asp:LinkButton ID="anoldetail1" runat="server" onclick="anoldetail_Click">Detail</asp:LinkButton>
      </td>
  </tr>

   <!-- cpl row -->
<tr>
     <td colspan="8" class="style12">Triple Interface</td>
     <td colspan="1" class="style23">
     &nbsp;&nbsp;
     <asp:Chart ID="Chart11" runat="server" Palette="None" Height="70px" 
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
     <td colspan="1" align ="right" class="style13"><%--<asp:Button ID="Button1" runat="server" BackColor="White" Font-Underline="True" 
         Text="Graph" ForeColor="#0099FF" 
             onclick="ti_Click"></asp:Button>--%>
             <asp:LinkButton ID="LinkButton1" runat="server" onclick="ti_Click">Graph</asp:LinkButton></td>
     <td colspan="1" align ="center" class="style13">&nbsp;|&nbsp;</td>
     <td colspan="1" class="style13"><%--<asp:Button ID="Button2" runat="server" BackColor="White" Font-Underline="True" 
         Text="Detail" ForeColor="#0099FF" 
             onclick="tidetail_Click"></asp:Button>--%>
             <asp:LinkButton ID="LinkButton2" runat="server" onclick="tidetail_Click">Detail</asp:LinkButton>
      </td>
     <td colspan="4" class="style13">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
     <td colspan="8" class="style14">CPL</td>
     <td colspan="1" class="style13">
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:Chart ID="Chart12" runat="server" Palette="None" Height="70px" 
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
     <td colspan="1" align ="right" class="style13"><%--<asp:Button ID="cpl" runat="server" BackColor="White" Font-Underline="True" 
         Text="Graph" ForeColor="#0099FF" 
             onclick="cpl_Click"></asp:Button>--%>
<!-- Comment out for different CPL processing -->
             <%--<asp:LinkButton ID="cpl1" runat="server" onclick="cpl_Click">Graph</asp:LinkButton>--%></td>
     <td colspan="1" align ="center" class="style13"><%--&nbsp;|&nbsp;--%></td>
     <td colspan="1" class="style13"><%--<asp:Button ID="cpldetail" runat="server" BackColor="White" Font-Underline="True" 
         Text="Detail" ForeColor="#0099FF" 
             onclick="cpldetail_Click"></asp:Button>--%>
             <asp:LinkButton ID="cpldetail1" runat="server" onclick="cpldetail_Click">Detail</asp:LinkButton>
      </td>
  </tr>

  <tr>
     <td colspan="8" class="style20">MDM</td>
     <td colspan="1" class="style26">
     &nbsp;&nbsp;
     <asp:Chart ID="Chart17" runat="server" Palette="None" Height="70px" 
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
     <td colspan="1" align ="right" class="style21">
             <asp:LinkButton ID="mdmButton1" runat="server" onclick="mdm_Click">Graph</asp:LinkButton></td>
     <td colspan="1" align ="center" class="style21">&nbsp;|&nbsp;</td>
     <td colspan="1" class="style21"><%--<asp:Button ID="Button2" runat="server" BackColor="White" Font-Underline="True" 
         Text="Detail" ForeColor="#0099FF" 
             onclick="tidetail_Click"></asp:Button>--%>
             <asp:LinkButton ID="mdmButton2" runat="server" onclick="mdmdetail_Click">Detail</asp:LinkButton>
      </td>
    
     
  </tr>
  <tr>
     <td colspan="8" class="style8">Data Feed</td>
     <td colspan="1" class="style27">
     &nbsp;&nbsp;
     <asp:Chart ID="Chart18" runat="server" Palette="None" Height="70px" 
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
             <%--<asp:LinkButton ID="LinkButton3" runat="server" onclick="mdm_Click">Graph</asp:LinkButton>--%></td>
     <td colspan="1" align ="center"><%--&nbsp;|&nbsp;--%></td>
     <td colspan="1"><%--<asp:Button ID="Button2" runat="server" BackColor="White" Font-Underline="True" 
         Text="Detail" ForeColor="#0099FF" 
             onclick="tidetail_Click"></asp:Button>--%>
             <asp:LinkButton ID="datafeed1" runat="server" onclick="datafeed_Click">Detail</asp:LinkButton>
      </td>
    
     
  </tr>

</table>
<!-- generic table  -->
<table width="900">
  <tr>
     
     <td colspan="22" align="center"><h2> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Generic&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h2>
     </td>
  </tr>
   <tr>
     <td colspan="1"><span class="style6"><span class="style7">Batch Status </span> 
         <br class="style7" /> <span class="style7">At 07:00</span></span>
     </td>
     <td colspan="1">
     <asp:Chart ID="Chart13" runat="server" Palette="None" Height="70px" 
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
     <td colspan="1"><%--<asp:Button ID="batch" runat="server" BackColor="White" Font-Underline="True" 
         Text="Detail" ForeColor="#0099FF" 
             onclick="batch_Click"></asp:Button>--%>
             <asp:LinkButton ID="batch1" runat="server" onclick="batch_Click">Detail</asp:LinkButton></td>
     <td colspan="3"></td>
     <td colspan="1" class="style6"><span class="style7">Batch </span> 
         <br class="style7" /><span class="style7">Performance</span></td>
     <td colspan="1">
     <asp:Chart ID="Chart15" runat="server" Palette="None" Height="70px" 
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
             <asp:LinkButton ID="bp1" runat="server" onclick="bp1_Click">Detail</asp:LinkButton></td>
     <td colspan="3"></td>
     <td colspan="1" class="style5">Defer Queue</td>
     <td colspan="1">
     <asp:Chart ID="Chart14" runat="server" Palette="None" Height="70px" 
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
     
     <td colspan="1"><%--<asp:Button ID="datafeed" runat="server" BackColor="White" Font-Underline="True" 
         Text="Detail" ForeColor="#0099FF" 
             onclick="datafeed_Click"></asp:Button>--%>
             <asp:LinkButton ID="defer1" runat="server" onclick="defer_Click">Detail</asp:LinkButton></td>
     <td colspan="3"></td>
     <td colspan="1" class="style5">Hal_Errors</td>
     <td colspan="1">
     <asp:Chart ID="Chart16" runat="server" Palette="None" Height="70px" 
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
     
     <td colspan="1"><%--<asp:Button ID="halerr" runat="server" BackColor="White" Font-Underline="True" 
         Text="Detail" ForeColor="#0099FF" 
             onclick="halerr_Click"></asp:Button>--%>
             <asp:LinkButton ID="halerr1" runat="server" onclick="halerr_Click">Detail</asp:LinkButton></td>
  </tr>
  <%--<tr>
  <td>
  <asp:HiddenField runat="server" ID="hddate" Value="" />
  </td>
  </tr>--%>

</table>
</asp:Content>