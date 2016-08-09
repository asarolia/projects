<%@ Page Language="C#" MasterPageFile="~/MasterPages/SiteMasterSidebar.master" AutoEventWireup="true" CodeFile="DownLoadData.aspx.cs" Inherits="DownLoadData" Title="Download Mainframe data" %>
<%@ PreviousPageType VirtualPath="~/CloneProposition.aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="js/DownloadData.js" language="javascript" type="text/javascript"> </script>
    <script src="js/DownloadData_Proxy.js" language="javascript" type="text/javascript"> </script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function(){
        $('#aCloneProposition').addClass('selected');
        $('#nDownloadData').addClass('selected');
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

    <h2>Download "<%=propositionDetails.Name %>" Proposition Data</h2>
    <p class="subtitle">Download proposition data from mainframe live region</p>
    <p class="required subtitle">* = Mandatory</p>
    
    <form id="Generic" runat="server" class="tabular">

        <h3>Mainframe credentials</h3>
        <div class="box">
            <p>
                <label for="UserTextBox">Login<span class="required">*</span></label>
                <span id="spanUserTextBox"><asp:TextBox ID="UserTextBox" MaxLength="10" runat="server"></asp:TextBox></span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Mainframe User ID" ControlToValidate="UserTextBox" SetFocusOnError="true" />
                
            </p>
            <p>
                <label for="PasswordTextBox">Password<span class="required">*</span></label>
                <span id="spanPasswordTextBox"><asp:TextBox TextMode="Password" ID="PasswordTextBox" runat="server"></asp:TextBox></span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Mainframe ID Password" ControlToValidate="PasswordTextBox" SetFocusOnError="true" />
            </p>
            <p>
                <label for="Region">Connect to region<span class="required">*</span></label>
                <span id="spanRegionTextBox">
                    <asp:DropDownList ID="Region" runat="server"></asp:DropDownList>
                </span>
            </p>
        </div>

        <span id="btnDownload">
            <asp:Button CausesValidation="true" ID="MyButton" Text="Start Download" runat="server" />
        </span>
        <span class="subtitle">This may take some time depending on number of tables and connectivity with Mainframe</span> 
        
        <h1>&nbsp;</h1>

        
        <div id="divStartProcess" class="actionItem" style="display:none;">
            <h3>Downloading data from Mainframe</h3> 
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
                            <li class="subItem">Progress : <div id="ProgressBar" ></div> </li>
                            <li class="subItem">Status : <span id="ProgressText"></span> </li>
                            <!--li class="subItem">Current table : <span id="CurrentTable"></span> </li-->
                            <li class="subItem scroll">Using SQL : <span id="CurrentSQL"></span> </li>
                        </ul>
                    </li>
                    <li id="divNext" style="display:none;">
                        <asp:Button ID="cmdNext"  PostBackUrl="~/ChangeProposition.aspx" Text="Continue" runat="server" />
                        <span class="footnote">Continue to Clone Proposition & apply changes</span>
                    </li>
                </ul>
            </div>
        </div>
  </form>      
    </div>
</asp:Content>

