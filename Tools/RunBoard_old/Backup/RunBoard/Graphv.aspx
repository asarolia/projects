<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Graphv.aspx.cs" Inherits="RunBoard.Graphv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%-- <p> Test linking to read the session data. You clicked <asp:Label ID="graphv1" runat="server"></asp:Label> Graph
</p>--%>
<p> Date: <asp:TextBox ID="graphv3" runat="server" Enabled="false"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <%--<asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="&lt;&lt;" 
        Width="82px" />--%>
        <asp:LinkButton ID="LinkButton1" runat="server" onclick="Button1_Click">&lt;&lt;</asp:LinkButton>
</p>

<%--Added for stacked chart --%>
  <%--   <div>
        <asp:Literal ID="lt1" runat="server"></asp:Literal>
    </div>  
    <div id="chart_div1">
    </div>   
--%>
    
     <p> 
        &nbsp;</p>
    <h1> Consolidated Processing View : <b> <asp:Label ID="graphv1" runat="server"></asp:Label>  </b></h1>
<p> 
        &nbsp;</p>
    
     <p> 
        &nbsp;</p>
    <p>
            <asp:CHART id="Chart4" runat="server" Height="296px" Width="620px" 
                BackColor="211, 223, 240" BorderlineDashStyle="Solid" 
                BackGradientStyle="TopBottom" BorderWidth="2px" BorderColor="#1A3B69" 
                DataSourceID="SqlDataSource1"> 
              <legends> 
                <asp:Legend TitleFont="Microsoft Sans Serif, 8pt, style=Bold" 
                      BackColor="Transparent" Font="Trebuchet MS, 8pt, style=Bold" 
                      Name="Default">
                    <Position Height="16.949152" Width="20" X="77" Y="5" />
                  </asp:Legend> 
              </legends>               
              <borderskin SkinStyle="Emboss"></borderskin> 
              <series> 
                <asp:Series Name="Series1" ChartType="Line" 
                      BorderColor="180, 26, 59, 105" Color="Green" XValueMember="RecordDt" 
                      YValueMembers="Processed" IsValueShownAsLabel="True" 
                      LegendText="Processed"></asp:Series> 
                <asp:Series Name="Series4" ChartType="Line" Color="Red" 
                      XValueMember="RecordDt" YValueMembers="Fail" IsValueShownAsLabel="True" 
                      LegendText="Fail"></asp:Series>
                  <asp:Series ChartArea="ChartArea4" ChartType="Line" 
                      Color="255, 128, 0" Legend="Default" Name="Series3" XValueMember="RecordDt" 
                      YValueMembers="Success" IsValueShownAsLabel="True" 
                      LegendText="Success">
                  </asp:Series>
     
              </series> 
          <chartareas> 
            <asp:ChartArea Name="ChartArea4" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="Transparent" BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom"> 
              <area3dstyle Rotation="10" Inclination="15" WallWidth="0" /> 
              <position Y="3" Height="92" Width="90" X="2"></position> 
              <axisy LineColor="64, 64, 64, 64" LabelAutoFitMaxFontSize="8" Title="Count" 
                    TitleFont="Microsoft Sans Serif, 12pt, style=Bold"> 
                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" /> 
                <MajorGrid LineColor="64, 64, 64, 64" /> 
                <ScaleView SizeType="Number" />
              </axisy> 
              <axisx LineColor="64, 64, 64, 64" LabelAutoFitMaxFontSize="8" Title="Date" 
                    TitleFont="Microsoft Sans Serif, 12pt, style=Bold" IntervalOffsetType="Days" IntervalType="Days" IsStartedFromZero="False"> 
                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" /> 
                <MajorGrid LineColor="64, 64, 64, 64" /> 
                <ScaleView Size="7" />
              </axisx> 
            </asp:ChartArea> 
           </chartareas> 
         </asp:CHART> 

    </p>

 <p> 
        &nbsp;</p>
    
     <p> 
        &nbsp;</p>
    <h1> Individual Processing View : <b><asp:Label ID="Label1" runat="server"></asp:Label> </b> </h1>
<p> 
        &nbsp;</p>
    
     <p> 
        &nbsp;</p>

    <p> 
        <asp:Chart ID="Chart1"  runat="server" Height="296px" Width="568px" 
                BackColor="211, 223, 240" BorderlineDashStyle="Solid" 
                BackGradientStyle="TopBottom" BorderWidth="2px" BorderColor="#1A3B69"  
            DataSourceID="SqlDataSource1">
            <legends> 
                <asp:Legend TitleFont="Microsoft Sans Serif, 8pt, style=Bold" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Enabled="False" Name="Default"></asp:Legend> 
              </legends>               
              <borderskin SkinStyle="Emboss"></borderskin> 
            <Series>
                <asp:Series Name="Series1" Color="Green" XValueMember="RecordDt" 
                    YValueMembers="Processed" IsValueShownAsLabel="True" ChartType="Line">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                    <AxisY Title="Processed Count" 
                        TitleFont="Microsoft Sans Serif, 12pt, style=Bold">
                        <ScaleView SizeType="Number" />
                    </AxisY>
                    <AxisX Title="Date" TitleFont="Microsoft Sans Serif, 12pt, style=Bold" 
                        IntervalOffsetType="Days" IntervalType="Days" IsStartedFromZero="False">
                        <ScaleView Size="7" />
                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:rundashboardConnectionString %>" 
            
            
            SelectCommand="SELECT RecordDt, SUM(Processed) AS Processed, SUM(Success) AS Success, SUM(Fail) AS Fail FROM FeedData WHERE Feed_Type = @Type AND RecordDt &lt;= @Date AND RecordDt &gt; DATEADD(dd, -7, @Date) GROUP BY RecordDt ORDER BY RecordDt DESC">
        <SelectParameters>
                <asp:SessionParameter Name="Type" SessionField="graph" />
                <asp:SessionParameter Name="Date" SessionField="date" />
            </SelectParameters>
        </asp:SqlDataSource>
</p>
    <p> 
        <asp:Chart ID="Chart2" runat="server" Height="296px" Width="558px" 
                BackColor="211, 223, 240" BorderlineDashStyle="Solid" 
                BackGradientStyle="TopBottom" BorderWidth="2px" BorderColor="#1A3B69" 
            DataSourceID="SqlDataSource1">
            <legends> 
                <asp:Legend TitleFont="Microsoft Sans Serif, 8pt, style=Bold" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Enabled="False" Name="Default"></asp:Legend> 
              </legends>               
              <borderskin SkinStyle="Emboss"></borderskin> 
            <Series>
                <asp:Series Color="255, 128, 0" Name="Series1" XValueMember="RecordDt" 
                    YValueMembers="Success" IsValueShownAsLabel="True" ChartType="Line">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                    <AxisY Title="Success Count" 
                        TitleFont="Microsoft Sans Serif, 12pt, style=Bold">
                        <ScaleView SizeType="Number" />
                    </AxisY>
                    <AxisX Title="Date" TitleFont="Microsoft Sans Serif, 12pt, style=Bold"
                       IntervalOffsetType="Days" IntervalType="Days" IsStartedFromZero="False">
                        <ScaleView Size="7" />
                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
</p>
    <p> 
        <asp:Chart ID="Chart3" runat="server" Height="296px" Width="556px" 
                BackColor="211, 223, 240" BorderlineDashStyle="Solid" 
                BackGradientStyle="TopBottom" BorderWidth="2px" BorderColor="#1A3B69" 
            DataSourceID="SqlDataSource1">
            <legends> 
                <asp:Legend TitleFont="Microsoft Sans Serif, 8pt, style=Bold" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="False" Enabled="False" Name="Default"></asp:Legend> 
              </legends>               
              <borderskin SkinStyle="Emboss"></borderskin> 
            <Series>
                <asp:Series Color="Red" Name="Series1" XValueMember="RecordDt" 
                    YValueMembers="Fail" IsValueShownAsLabel="True" ChartType="Line">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                    <AxisY Title="Failed Count" TitleFont="Microsoft Sans Serif, 12pt, style=Bold">
                       <ScaleView SizeType="Number" />
                    </AxisY>
                    <AxisX Title="Date" TitleFont="Microsoft Sans Serif, 12pt, style=Bold"
                     IntervalOffsetType="Days" IntervalType="Days" IsStartedFromZero="False">
                        <ScaleView Size="7" />
                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </p>
    <p> 
        &nbsp;</p>
    
   

</asp:Content>
