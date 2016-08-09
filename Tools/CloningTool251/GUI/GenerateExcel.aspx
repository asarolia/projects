<%@ Page Language="C#" MasterPageFile="~/MasterPages/SiteMasterSidebar.master" AutoEventWireup="true" CodeFile="GenerateExcel.aspx.cs" Inherits="GenerateExcel" Title="Download Excel Sheet" EnableViewState="false" %>
<%@ PreviousPageType VirtualPath="~/ChangeProposition.aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="js/GenerateExcel.js" type="text/javascript"> </script>
    <script src="js/GenerateExcel_Proxy.js" type="text/javascript"> </script>
    
    <script language="javascript" type="text/javascript">
        $(document).ready(function(){
        $('#aCloneProposition').addClass('selected');
        $('#nGenerateExcel').addClass('selected');
        });
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="sidebar" Runat="Server">
    <h3>Selected Proposition</h3>
    <div class="proposition">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr class="center" >
                <td colspan="2"><%=propositionDetails.Name %></td>
            </tr>
            <tr>
                <th>Scheme :</th>
                <td><%=propositionDetails.SchemeCode %></td>
            </tr>
            <tr>
                <th>Product :</th>
                <td><%=propositionDetails.ProductCode%></td>
            </tr>
            <tr>
                <th>Master Company Number :</th>
                <td><%=propositionDetails.MasterCompanyNumber%></td>
            </tr>
            <tr>
                <th>LOB :</th>
                <td><%=propositionDetails.LOBCode %></td>
            </tr>
        </table>
    </div>
    <div style="clear:both;"></div>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="bodyContentPlaceholder" Runat="Server">
    <h2>Generate Proposition Data in Excel Sheet</h2>
    <p class="subtitle">Generate Support Data in 'Automated DML Generation' format</p>
    
    <form runat="server" id="Generic">
    <h3>Excel Sheet Processing</h3>
        <div class="box actionItem">
            <ul>
                <li id="divStep1">Generating Proposition Data Sheet &nbsp;
                <span id="divStepMessage1" class="required">&nbsp;</span>
                </li>
                
            </ul>
            <div id="divNext">
                <asp:Button runat="server" ID="NextButton" Text="Download" OnClick="DownloadExcel"/>
                <span class="subtitle"><span class="required">Please verify data thoroughly</span></span>               
                <span class="footnote">Once required modifications are done, use 'Automated DML Generation' tool to generate this sheet.</span>               
            </div>
        </div>
        <br />
        <br />
        <br />
        <h4>Limitations in cloning</h4>
        <p class="subtitle">Following data needs to be changed manually by editing output excel sheet</p>
        <div class="box">
            <ul>
                <li> <b> SHM_CAMPAIGN </b> data is excluded from download due to volume of data</li>
                <li> <b> POL_CORRESPONDENCE </b> SCHEME_CD is not amended </li>
                <li> <b> HAL_UCT2-CORRESPONDENCE_KICKER </b> is not amended due to complexity</li>
                <li> <b> PRM_UCT9-CONTACT_EVENTS </b> is not amended due to complexity</li>
                <li> <b> And </b> may be more.. please review output Excel carefully. </li>
            </ul>
        </div>
        <h4>Thank you for using this tool</h4>
    </form>
</asp:Content>

