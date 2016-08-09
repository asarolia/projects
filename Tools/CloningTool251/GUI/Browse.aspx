<%@ Page Title="Browse Module" Language="C#" MasterPageFile="~/MasterPages/BrowseMaster.master" AutoEventWireup="true" CodeFile="Browse.aspx.cs" Inherits="Browse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="js/Browse.js" type="text/javascript"> </script>
    <script src="js/Browse_Proxy.js" type="text/javascript"> </script>
    
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('#aBrowse').addClass('selected');
        });
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceholder" Runat="Server">
    <h2> Browse Mainframe Module</h2>
    <p class="subtitle">Enter user id, password & pds name to view the file</p> 
    <form id="Form1" runat="server" class="tabular">
        <div class="box">
            <p>
                <label id="Label1" for="UseridTextBox" runat="server">User ID <span class="required">*</span></label>
                <span id="spanUseridTextBox">
                    <asp:TextBox ID="UseridTextBox" size="10"  runat="server"></asp:TextBox>    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter User id" ControlToValidate="UseridTextBox" SetFocusOnError="true" />
                </span>
                <span id="spangetUserListButton">
                    <asp:Button CausesValidation="true" runat="server" id="getUserListButton" Text="Get List"/>
                </span>

            </p>
            <fieldset id="startDownload" class="collapsed">
                <legend id="startDownloadLegend" title="Click here to open / close additional input fields">Start new download</legend>
                <div id="startDownloadContent" style="display:none; ">
                    <p>
                        <label for="PasswordTextBox">Password<span class="required">*</span></label>
                        <span id="spanPasswordTextBox"><asp:TextBox TextMode="Password" ID="PasswordTextBox" runat="server"></asp:TextBox></span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Password" ControlToValidate="PasswordTextBox" SetFocusOnError="true" />
                    </p>

                    <p>
                        <label for="TextBoxModuleName">Member Name<span class="required">*</span></label>
                        <span id="spanTextBoxModuleName">
                        <asp:TextBox  runat="server" size="100" MaxLength="200" ID="TextBoxModuleName"></asp:TextBox>
                        </span>
                    </p>
                        <span id="spanButtonGetModule">
                            <asp:Button CausesValidation="true" runat="server" id="ButtonGetModule" Text="Download"/>
                        </span>
                </div>
            </fieldset>
         </div>

         <table cellpadding="0" cellspacing="0" width="100%" height="100%">
             <tr>
                 <td id="mydownloads" width="49%" align="left" valign="top">
                    <div>
                    <div class="box" style=" overflow:auto">
                        <h3>My Downloads <span id="mydownloadContent_wait" style=" display:none;" class="spaceditem loading">Refreshing list ...</span> </h3>
                        <div id="mydownloadContent" class="actionItem"></div>
                    </div>
                    </div>
                 </td>
                 <td width="1%">&nbsp;</td>
                 <td id="download" width="50%" valign="top">
                    <div>
                    <div class="box" style=" overflow:auto">
                        <h3>Over all Downloads <span id="downloadContent_wait" style=" display:none;" class=" spaceditem loading">Refreshing list ...</span> </h3>
                        <div id="downloadContent" class="actionItem"></div>
                    </div>
                    </div>
                 </td>
             </tr>
         </table>

        <%-- moduleListTemplate Template --%>
		        <div style="display:none;">
                    <div id="moduleListTemplate">
                        <div>
                            <span><a href="#">${title}</a></span><br />
                            <span>User Id: ${user} , Downloaded Time: ${downloadTime}</span>
                            <input type="hidden" id="hiddenNavigateTo" value="${link}" />
                        </div>
                    </div>

                </div>
        <%-- PropositionDetails Template --%>

    </form>
</asp:Content>

 