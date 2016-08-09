<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoadDataFromMF.aspx.cs" Inherits="LoadDataFromMF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Download</title>        
    <link href="css/core.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="bodyCenter">


<div id="header"></div>
    <div class="podHeight20 topGap" >
		<div class="pod podAlignedLink  middle sixColumnPod gradientPod" >
			<h2 class="podHeader podHeaderUnderline brandFont">Download data from Mainframe DB2</h2>
			
			<div class="podContent clearFix">
				<div class="aH3DataTable clearFix"><br /><br />
				
				 <table>
				 <tr>
				 <td>
				 <label for="company_name" style="color: #004fb6; font-size: 1.3em;">
                  M/F Username</label>
				 </td>
				 <td>
				 <input id="username" name="" type="text" runat="server" />                 
				 </td>
				 </tr>
				 <tr>
				 <td>
				 <label for="user_name" style="color: #004fb6; font-size: 1.3em;">Password</label></td>
				 <td>
		         <input id="password" name="" type="password" runat="server" style="width:99%" />
		        </td>
		        </tr>
		        <tr>
		        <td>
		         <label for="company_country_id" style="color: #004fb6; font-size: 1.3em;">
                  Region</label>
		        </td>
				<td>
				 <asp:DropDownList ID="Region" runat="server" Height="30px" Width="100%">
                 <asp:ListItem value="CIUXA1A" Text = "CIUXA1A - UAT"></asp:ListItem>
                 <asp:ListItem Value="CIUXA2A" Text="CIUXA2A - Systest"></asp:ListItem>
                  </asp:DropDownList>
				</td> 				 
				 </tr>
                 				 
				 </table>
				 <asp:Button ID="Submit" runat="server" Text="Download Data From DB2" 
                        onclick="Submit_Click"/>
                        <div id = "divLoading" runat="server" >
                        <!--<img src="images/loading-gif-animation.gif" alt="Loading" />-->
                        </div>
	              <p class="alignedLink">
                      <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/DashBoard.aspx">Go to Dashboard</asp:HyperLink></p>
				</div>
				</div>
				</div>
				</div>
    </div>
    </form>
</body>
</html>
