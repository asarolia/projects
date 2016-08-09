<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/DataMigration.master" AutoEventWireup="true" CodeFile="DataMigration.aspx.cs" Inherits="DataMigration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="js/DataMigration.js" type="text/javascript"> </script>
    <script src="js/DataMigration_Proxy.js" type="text/javascript"> </script>
    
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('#aMigration').addClass('selected');
        });
    </script></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceholder" Runat="Server">
    <h2> Migrate Policy data from one region to another</h2>
    <span class="breadcrumb"> Migrate policy data from one region to another</span>

    <%if (Messages != null)  {%>
        <br />
        <div class="box">
        <%
            foreach (string message in Messages)
            {
                Response.Write(String.Format("{0} <br />", message));
            }
        
        %>
        </div>
    <%} %>
    
    <!-- New migration -->
    <h4> Migrate A New Policy</h4>
    <form id="MyForm" runat="server" class="tabular">
        <div class="box">
            <p> 
                <label id="Label2" for="UseridTextBox" runat="server">User ID <span class="required">*</span></label>
                <span id="spanUserIdTextBox">
                    <asp:TextBox ID="UserIdTextBox" size="10"  runat="server"></asp:TextBox>    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter User id" ControlToValidate="UserIdTextBox" SetFocusOnError="true" />
                </span>
            </p>
            <p> 
                <label id="Label3" for="PasswordTextBox" runat="server">Password <span class="required">*</span></label>
                <span id="spanPasswordTextBox">
                    <asp:TextBox ID="PasswordTextBox" size="10"  runat="server"></asp:TextBox>    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter Password" ControlToValidate="PasswordTextBox" SetFocusOnError="true" />
                </span>
            </p>
            <p>
                <label id="Label4" for="RegionList" runat="server">Region <span class="required">*</span></label>
                <span id="spanRegion">
                    <asp:DropDownList ID="RegionList" size="1"  runat="server"></asp:DropDownList>    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Select Region" ControlToValidate="RegionList" SetFocusOnError="true" />
                </span>
            </p>
            <p>
                <label id="Label5" for="PolicyNumber" runat="server">Policy Number <span class="required">*</span></label>
                <span id="span2">
                    <asp:TextBox ID="PolicyNumberTextBox" size="10"  runat="server"></asp:TextBox>    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Policy Number " ControlToValidate="PolicyNumberTextBox" SetFocusOnError="true" />
                </span>
            </p>
            <p>
                <asp:Button runat="server" ID="DownloadPolicy" Text="Download" 
                    onclick="DownloadPolicy_Click"/>
            </p>
        </div>


        <!-- Existing policies migration -->
        <h4> Download Policies <button id="ButtonRefresh">Refresh</button> </h4>
        <span id="HiddenSelectedFile">
            <asp:HiddenField ID="SelectedFile" Value="" runat="server" />
        </span>
        <span id="HiddenTargetRegion">
            <asp:HiddenField ID="TargetRegion" Value="" runat="server" />
        </span>
        <span class="Hello" id="Typical">Hello</span>
        <div  id="divListDisplay" >
        </div>
        
        <%-- List Template --%>
	    <div  id="divList" style="display:none;">
            <table width="100%" cellspacing="0" cellpadding="0" class="widgets">
                <tbody>
                    <tr class="${oddOrEven()} ListRow" id="ListRow">
                        <td width="80%">
                            <span class="headline">${Parts[0]} Number: <b>${Parts[4]}</b> </span>
                            <br />
                            <span> By User : <b>${Parts[2]}</b> On: ${Parts[1]} From: <b>${Parts[3]}</b> </span>
                        </td>
                        <td width="20%">
                            <span class="SpanDisplay1" style="display:none;">
                            Region: <span id="SpanSecondRegionList">
                                <asp:DropDownList id="SecondRegionList" runat="server"></asp:DropDownList>
                            </span>
                            </span>
                            &nbsp;
                        </td>
                        <td width="10%">
                            <span class="SpanDisplay2" style="display:none;">
                                <button id="UploadButton" class="UploadButton" onclick="return Upload(this,'${Parts.join('.')}');">Upload</button>
                            </span>
                            &nbsp;
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</asp:Content>

