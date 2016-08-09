<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="DashBoard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="css/demo_table.css" rel="stylesheet" type="text/css" />
    <link href="css/demo_page.css" rel="stylesheet" type="text/css" />
    <link href="css/basic_ie.css" rel="stylesheet" type="text/css" />
    <link href="css/basic.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/core.css" rel="stylesheet" type="text/css" />
    
    <link rel="stylesheet" href="themes/base/jquery.ui.all.css"/>
    
	<script type="text/javascript" src="JS/jquery-1.4.4.js"></script>
	<script type="text/javascript" src="ui/jquery.ui.core.js"></script>
	<script type="text/javascript" src="ui/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="ui/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="JS/jquery.dataTables.js"></script>
    <script type="text/javascript" src="JS/jquery.json2.js"></script>    
    <script type="text/javascript" src="JS/JScript.js"></script>
    <script type="text/javascript" src="JS/jquery.simplemodal.js"></script>
    <script src="JS/jquery.validate.js" type="text/javascript"></script>
    <script src="JS/Date_script.js" type="text/javascript">
    <script type="text/javascript" src="JS/JScript.js"></script>    
    
	<link rel="stylesheet" href="demos.css"/>
	<script type="text/javascript">
	$(function() {
		var dates = $( "#txtfrom, #txtto" ).datepicker({
			defaultDate: "d",
			changeMonth: true,
			changeYear: true,
			numberOfMonths: 1,
			onSelect: function( selectedDate ) {
				var option = this.id == "txtfrom" ? "minDate" : "maxDate",
					instance = $( this ).data( "datepicker" ),
					date = $.datepicker.parseDate(
						instance.settings.dateFormat ||
						$.datepicker._defaults.dateFormat,
						selectedDate, instance.settings );
				dates.not( this ).datepicker( "option", option, date );
			}
		});
	});
	</script>
    
</head>
<body>
<div id="bodyCenter">
    <form id="form1" runat="server">


    <div id="header">
			
                <ul id="headerNav">
                    <!--<li><a title="Import" href="import.aspx">Import</a></li>
                    <li class="selected"><a title="Search" href="default.aspx">Search</a></li>
                    <li class="selectRight"><a  title="Data Extractor" href="dataExtractor.aspx">Data Extractor</a></li>
                    <li><a  title="Release" href="releaseTab.aspx">Release Info</a></li>
                    <li><a title="Release Tracker" href="releasetracker.aspx">Release Tracker</a></li>
                    <li><a  title="Admin" href="Admin.aspx">Admin</a></li>
                    <li class="right"><a  title="Team Management" href="team.aspx">Team</a></li>-->
                </ul>
		<div class="podHeight10">
		<div class="pod podAlignedLink left twelveColumnPod gradientPod">
			<h2 class="podHeader podHeaderUnderlineDashed brandFont">Hal Error Dashboard, Select dates to search logs</h2>
			<div class="podContent clearFix">
				<div class="aH3DataTable clearFix"><br /><br />
		<asp:Table ID="Table1"  runat="server" CssClass="table"  >
      <asp:TableRow >
         <asp:TableCell >
           From :
         </asp:TableCell>
        <asp:TableCell>
        <asp:TextBox ID="txtfrom" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       </asp:TableCell>
       <asp:TableCell>
                To:
       </asp:TableCell>
        <asp:TableCell>
        <asp:TextBox ID="txtto" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       </asp:TableCell>       
       <asp:TableCell>
                Status:
       </asp:TableCell>
        <asp:TableCell>
        <asp:DropDownList ID = "drpStatus" runat = "server">
        <asp:ListItem value = 10 text="All"></asp:ListItem>
        <asp:ListItem Value=0 Text="No Status"></asp:ListItem>
        <asp:ListItem Value=2 Text="Error Reported"></asp:ListItem>
        <asp:ListItem Value=3 Text="Investigating"></asp:ListItem>
        <asp:ListItem Value=4 Text="Fixed"></asp:ListItem>
        <asp:ListItem Value=5 Text="Rejected - Live Error"></asp:ListItem>
        <asp:ListItem Value=6 Text="Rejected - Negative Testing"></asp:ListItem>
        <asp:ListItem Value=7 Text="Rejected - Environment"></asp:ListItem>
        <asp:ListItem Value=8 Text="Rejected - Fixed in QC"></asp:ListItem>
        <asp:ListItem Value=9 Text="Rejected - Environment(Non-Exceed)"></asp:ListItem>
        </asp:DropDownList>
        
       </asp:TableCell>
       
       <asp:TableCell>
        
       </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow >
    
    </asp:TableRow>
    </asp:Table>
    <input id="btnSearch" type="button" value="Search" />
    <br /><br />
        
    
  </form>
         
            <div id="divDataTable">   
            </div>
     
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefectTrackerConnectionString3 %>"
            SelectCommand="SELECT * FROM [Status]"></asp:SqlDataSource>

                <div id="modalView" >
    <div id="divEditView" class="hidden" style = "width:50%; height : 50%;" >
        <div style="width:50%;">
        <input id="btnBack" type="button" value="BACK" class="backbutton"/>
        </div>
		<div class="clear"></div>
        <div class="pod eightColumn gradientPod divEditleft">
			<h2 class="podHeader podHeaderUnderlineDashed brandFont">Master Error</h2>
            <div id="divEditLeft">   
        </div>
        </div>
        
		<!-- preload the images -->
		<div style='display:none'>
			<img src="images/x.png" alt='' />
		</div>
        
        
        
        </div>    
    </div>
    <br /><br /><br /><br /><br /><br />
    
    </div>
    
    </div>
</body>
</html>
