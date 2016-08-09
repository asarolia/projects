<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.master" AutoEventWireup="true" CodeFile="DownloadProposition.aspx.cs" Inherits="DownloadProposition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="js/Site.js" language="javascript" type="text/javascript"> </script>
    <script src="js/DownloadProposition.js" language="javascript" type="text/javascript"> </script>
    <script src="js/DownloadProposition_Proxy.js" language="javascript" type="text/javascript"> </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceholder" Runat="Server">
    <h2>Download Existing Proposition</h2>
    <p class="subtitle">Choose a proposition to download</p> 
    <p class="required subtitle">* = Mandatory</p>
    
    <form id="Form1" runat="server" class="tabular"> 
        <!-- mainframe details -->
        <h4>Mainframe credentials</h4>
        <div class="box">
            <p>
                <label for="UserTextBox">Login<span class="required">*</span></label>
                <span id="spanUserTextBox"><asp:TextBox ID="UserTextBox" MaxLength="10" runat="server"></asp:TextBox></span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Mainframe User ID" ControlToValidate="UserTextBox" SetFocusOnError="true" />
                
            </p>
            <p>
                <label for="PasswordTextBox">Password<span class="required">*</span></label>
                <span id="spanPasswordTextBox"><asp:TextBox TextMode="Password" ID="PasswordTextBox" runat="server"></asp:TextBox></span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Mainframe ID Password" ControlToValidate="PasswordTextBox" SetFocusOnError="true" />
            </p>
            <p>
                <label for="Region">Connect to region<span class="required">*</span></label>
                <span id="spanRegionTextBox">
                    <asp:DropDownList ID="Region" runat="server"></asp:DropDownList>
                </span>
            </p>
        </div>


        <!-- select proposition -->
        <h4>Download Preference</h4>
        <div class="box"> 
            <div  class="splitcontentleft">
            <p>
                <label id="Label1" for="PropositionListDropDown" runat="server">Download Proposition<span class="required"> *</span></label>
                <span id="spanPropositionListDropDown">
                    <asp:DropDownList ID="PropositionListDropDown" runat="server"></asp:DropDownList>
                    <asp:HiddenField ID="hPropositionListDropDown" runat="server" />
                    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select a proposition" ControlToValidate="PropositionListDropDown" SetFocusOnError="true" /><br />
                    
                </span> 
                <span id="SpanSchemeIndicator">
                    <label id="Label2" for="schemeCheckbox" runat="server">Include Scheme Data</label>
                    <asp:CheckBox runat="server" ID="schemeCheckbox" Checked="true" />
                </span>
                <br />
                <span id="SpanProductIndicator">
                    <label id="Label3" for="schemeCheckbox" runat="server">Include Product Data</label>
                    <asp:CheckBox runat="server" ID="CheckBox1" Checked="true"/>
                </span>
                <br />
                <span id="SpanMasterCompanyIndicator">
                    <label id="Label4" for="schemeCheckbox" runat="server">Include Master company Data</label>
                    <asp:CheckBox runat="server" ID="CheckBox2" Checked="true"/>
                </span>
                <br />
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
         
        <div class="box">
            <span id="btnDownload">
                <asp:Button CausesValidation="true" ID="MyButton" Text="Start Download" runat="server" />
            </span>
            <span class="subtitle">This may take some time depending on number of tables and connectivity with Mainframe</span> 
        </div>
        
        <!-- Download progress -->
        <div id="divStartProcess" class="actionItem" style="display:none;">
            <h4>Downloading data from Mainframe</h4> 
            <span class="subtitle">This may take a while</span> 
            <div class="box actionItem">
                <ul>
                    <li id="divVerifyUser">
                        Verifying Login Details 
                        <span class="required" id="divVerifyUserMessage"></span>
                    </li>
                    <li id="divDownloading">Downloading Mainframe DB2 Data
                        <span class="required" id="divDownloadingMessage"></span>
                        <ul id="divDownloadInfo">
                            <li class="subItem">Progress : <div id="ProgressBar"></div> </li>
                            <li class="subItem">Status : <span id="ProgressText"></span> </li>
                            <!--li class="subItem">Current table : <span id="CurrentTable"></span> </li-->
                            <li class="subItem scroll">Using SQL : <span id="CurrentSQL"></span> </li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>

        <!-- Generate Excel sheet -->
        <div id="divNext" style="display:none;">
        <h4>Excel Sheet Processing</h4>
        <div class="box actionItem">
            <ul>
                <li id="divStep1">Generating Proposition Data Sheet &nbsp;
                <span id="divStepMessage1" class="required">&nbsp;</span>
                </li>
                
            </ul>
            <div id="divStep2">
                <asp:Button runat="server" ID="NextButton" Text="Download" OnClick="DownloadExcel"/>
                <span class="subtitle"><span class="required">Please verify data thoroughly</span></span>               
                <ul>
                    <li> <b> SHM_CAMPAIGN </b> data is excluded from download due to volume of data</li>
                </ul>
            </div>
        </div>
        </div>

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

</asp:Content>

