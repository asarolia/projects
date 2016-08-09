<%@ Page Language="C#" MasterPageFile="~/MasterPages/SiteMasterSidebar.master" AutoEventWireup="true" EnableViewState="false" CodeFile="ChangeProposition.aspx.cs" Inherits="ChangeProposition" Title="Change proposition" %>
<%@ PreviousPageType VirtualPath="~/DownLoadData.aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="js/ChangeProposition.js" type="text/javascript"> </script>
    <script src="js/ChangeProposition_Proxy.js" type="text/javascript"> </script>
    
    <script language="javascript" type="text/javascript">
        $(document).ready(function(){
        $('#aCloneProposition').addClass('selected');
        $('#nChangeProposition').addClass('selected');
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
    <h2>Apply Change to "<%=propositionDetails.Name%> " Proposition Data</h2>
    <p class="subtitle">Apply Predefined changes (Automatic Changes) or Manually amend proposition data - to create new proposition</p>
    
    <form runat="server" id="Generic">
    <h3>Automatic Changes</h3>
        <div class="box actionItem">
            <ul>
                <li id="divStep1" class="<%=propositionDetails.SchemeCodeChanged.ToString()%>">Change Scheme Code <%= " " + propositionDetails.SchemeCode + " => " + propositionDetails.nSchemeCode %>
                    <span class="required scroll" id="divStepMessage1"></span>
                </li>
                
                <li id="divStep2" class="<%=propositionDetails.ProductCodeChanged.ToString()%>">Change Product Code <%= " " + propositionDetails.ProductCode + " => " + propositionDetails.nProductCode %>
                    <span class="required scroll" id="divStepMessage2"></span>
                </li>

                <li id="divStep3" class="<%=propositionDetails.MasterCompanyNumberChanged.ToString()%>">Change Master Company number <%= " " + propositionDetails.MasterCompanyNumber + " => " + propositionDetails.nMasterCompanyNumber %>
                    <span class="required scroll" id="divStepMessage3"></span>
                </li>

                <li id="divStep4" class="<%=propositionDetails.EffectiveDateChanged.ToString()%>">Change Effective Date to <%= propositionDetails.nEffectiveDate %>
                    <span class="required scroll" id="divStepMessage4"></span>
                </li>

                <li id="divStep5" class="<%=propositionDetails.ExpirationDateChanged.ToString()%>">Change Expiration Date to <%= propositionDetails.nExpirationDate %>
                    <span class="required scroll" id="divStepMessage5"></span>
                </li>

            </ul>
            <span id="spanApplyRulesButton">
                <asp:Button runat="server" ID="ApplyRulesButton" Text="Apply Changes" />               
            </span>
        </div>
        <div class="box" id="divNext" style="display:none;">
            <span>
                <asp:Button runat="server" ID="NextButton" Text="Continue" PostBackUrl="~/GenerateExcel.aspx" />
                <span class="footnote">Continue to generate Support data Excel sheet</span>               
            </span>
        </div>
    </form>
</asp:Content>

