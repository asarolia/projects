<%@ Page Language="C#" MasterPageFile="~/MasterPages/SiteMaster.master" AutoEventWireup="true" EnableViewState="false" CodeFile="CloneProposition.aspx.cs" Inherits="CloneProposition" Title="Clone Existing Proposition" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/Site.js" language="javascript" type="text/javascript"> </script>
    <script src="js/CloneProposition.js" language="javascript" type="text/javascript"> </script>
    <script src="js/CloneProposition_Proxy.js" language="javascript" type="text/javascript"> </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceholder" Runat="Server">

    <h2>Clone Existing Proposition</h2>
    <p class="subtitle">Choose live proposition from list and clone data for new scheme/product/master company number</p> 
    <p class="required subtitle">* = Mandatory</p>
    
    <form id="Form1" runat="server" class="tabular"> 
        <div class="box"> 
            <div  class="splitcontentleft">
            <p>
                <label for="PropositionListDropDown" runat="server">Live proposition<span class="required"> *</span></label>
                <span id="spanPropositionListDropDown">
                    <asp:DropDownList ID="PropositionListDropDown" runat="server"></asp:DropDownList>
                    <asp:HiddenField ID="hPropositionListDropDown" runat="server" />
                    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select a proposition" ControlToValidate="PropositionListDropDown" SetFocusOnError="true" /><br />
                    
                </span> 
                <span id="SpanLiveSchemeCode" style="display:none;">
                    <asp:TextBox runat="server" ID="LiveSchemeCode" Value="" />
                </span>
            </p>
            </div>
            <div class="splitcontentright">
                <div id="wAjaxPropositionDetails" style="display:none;"><span class="loading">loading ...</span></div>
                <div id="Div1" class="proposition">
                    <div id="AjaxPropositionDetails" style="display:none;"></div>
                </div>
            </div>

            <div style="clear:both;"></div>
        </div>
        <h3>Clone to </h3>
        <span class="footnote" style="text-align:left;">Leave fields as <span class="required">blank</span> when value same as live.</span>       
         
        <div class="box">
        
            <div id="attributes" class="attributes">
            <div class="splitcontentleft">
            <p>
                
                <label for="SchemeTextBox" runat="server">New Scheme Code<span class="required">*</span></label>
                <asp:TextBox ID="SchemeTextBox" size="3" MaxLength="3" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Scheme code must be entered" ControlToValidate="SchemeTextBox" SetFocusOnError="true" /><br />
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="New Scheme code must not be same as live" ControlToValidate="SchemeTextBox" ControlToCompare="LiveSchemeCode" Type="String" Operator="NotEqual" SetFocusOnError="true" />
             </p>
             <p>
                <label id="Label1" for="ProductCodeTextBox" runat="server">New Product Code</label>
                <asp:TextBox ID="ProductCodeTextBox" size="3" MaxLength="3" runat="server"></asp:TextBox>             
             </p>
             <p>
                <label id="Label2" for="MasterCompanyTextBox" runat="server">Master Company Number</label>
                <asp:TextBox ID="MasterCompanyTextBox" size="2"  MaxLength="2" runat="server"></asp:TextBox>  
             </p>
             </div>
             <div class="splitcontentright">
             <p>
                <label id="Label3" for="EffectiveDateTextBox" runat="server">Effective from <span class="required">*</span></label>
                <span id="spanEffectiveDate">
                    <asp:TextBox ID="EffectiveDateTextBox" size="10"  runat="server"></asp:TextBox>    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Choose a Effective Date" ControlToValidate="EffectiveDateTextBox" SetFocusOnError="true" /><br />
                    <span class="footnote">(dd/mm/yyyy)</span>
                </span>
            </p>
            <p>
                <label id="Label4" for="ExpirationDateTextBox" runat="server">Expiration date<span class="required">*</span></label>
                <span id="spanExpirationDate">
                    <asp:TextBox ID="ExpirationDateTextBox" Text="31/12/9999" size="10"  runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Choose a Expiration Date" ControlToValidate="ExpirationDateTextBox" SetFocusOnError="true" /><br />                    
                    <span class="footnote">(dd/mm/yyyy)</span>
                </span>
            </p>
            </div>
            </div>
            <div style="clear:both;"></div>
            </div>
            <asp:Button runat="server" id="NextButton" PostBackUrl="~/DownLoadData.aspx" Text="Continue " />
            <span class="footnote">Continue & download data from Mainframe</span>
        </form>

        <%-- PropositionDetails Template --%>
		        <div style="display:none;">
                    <div id="AjayPropositionDetailsTemplate">
                        <table border="0" cellpadding="0" cellspacing="0">
                        <!--data-->
                            <tr class="center" >
                                <td colspan="2">{Name}</td>
                            </tr>
                            <tr>
                                <th>Scheme :</th>
                                <td>{SchemeCode}</td>
                            </tr>
                            <tr>
                                <th>Product :</th>
                                <td>{ProductCode}</td>
                            </tr>
                            <tr>
                                <th>Master Company Number :</th>
                                <td>{MasterCompanyNumber}</td>
                            </tr>
                            <tr>
                                <th>LOB :</th>
                                <td>{LOBCode}</td>
                            </tr>
                        <!--data-->
                        </table>
                    </div>
                </div>
        <%-- PropositionDetails Template --%>
    </div>
</asp:Content>


