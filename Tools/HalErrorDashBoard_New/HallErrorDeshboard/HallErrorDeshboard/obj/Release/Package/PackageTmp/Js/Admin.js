// JScript File
var oTable;
var selectedRowId;//stores the value of selected row in main grid
var operation;//stores the opeartion value for dependency template
 $(document).ready(function() {
            $("table.#PnlUserCreation").hide();            
            $("table.#PnlAddCols").hide();
            $("table.#PnlRoleAssign").hide();
            $("table.#PhlUserDelete").hide();
            $("div.#DeleteLock").hide();            
            $("#btnAddColumn").hide();
            $("#btnUserSubmit").hide();
            $("#btnRoleSubmit").hide();
            $("#btnAddCol").hide();   
            $("table.#PnlRefreshSchema").hide();
            $("table.#PnlSupportTable").hide();
            
            $("[href=#UserCreate]").click(function() 
            {               
              document.getElementById("txtLoginName").value = "";
              document.getElementById("txtPass").value = "";
              document.getElementById("txtLstNme").value = "";
              document.getElementById("txtFstNme").value = "";
              document.getElementById("txtEmail").value = "";
              $("table.#PnlAddCols").hide();
              $("table.#PnlRoleAssign").hide();
              $("table.#PhlUserDelete").hide();
               $("table.#PnlRefreshSchema").hide();
               $("table.#PnlSupportTable").hide();
              $("div.#DeleteLock").hide();                            
              $("#btnUserSubmit").show();              
              $("#btnRoleSubmit").hide();              
              $("#btnAddCol").hide();                            
              $("table.#PnlUserCreation").show(500);
              $('table.#PnlUserCreation').append('<img id="loadining" src="../images/LoadingImg.gif" alt="loading"/>');
                $.ajax({
                  type: "POST",
                  url: "WebService.asmx/GetData",
                  data: "{}",
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",
                  success: function(msg) {
                    // Replace the div's content with the page method's return.
                    $('#loading').fadeOut(500,function(){                    
                    });
                  }
                });
            });
            $("[href=#AssignRole]").click(function() 
            { 
              $("table.#PnlAddCols").hide();              
              $("table.#PhlUserDelete").hide();
              $("table.#PnlUserCreation").hide();
              $("div.#DeleteLock").hide();       
              $("table.#PnlSupportTable").hide();
               $("table.#PnlRefreshSchema").hide();       
              $("table.#PnlRoleAssign").show(500);
              $("#btnUserSubmit").hide();
              $("#btnAddCol").hide();
              document.getElementById("drdRole").selectedIndex=0;
                //document.getElementById("drdAssQueue").selectedIndex=0;
                //document.getElementById("drdIncInProdReport").selectedIndex=0;
                document.getElementById("drdProfileforUser").selectedIndex=0;
                
                var jsonData = JSON.stringify({ ID : 2 });
                 $.ajax({
                    type: "POST",
                    url: "WebService.asmx/GetDrdValue",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: jsonData,     
                    success: function(data) 
                    {
                        fnAddtoUser(data);
                    },
                    error: AjaxFailed
                  });                    
                $("#btnRoleSubmit").show();
                  $("#LabelSucess").hide();                
            });
            $("[href=#AddCol]").click(function() 
            {                          
              $("table.#PhlUserDelete").hide();
              $("table.#PnlUserCreation").hide();
              $("table.#PnlRoleAssign").hide();
              $("div.#DeleteLock").hide();     
              $("table.#PnlSupportTable").hide();  
               $("table.#PnlRefreshSchema").hide();       
              $("table.#PnlAddCols").show(500);
              $("#btnAddCol").show();
              $("#btnUserSubmit").hide();
              $("#btnRoleSubmit").hide();
              $("#LabelSucess").hide();
               $("#Sys").hide();
                $("#SystemAction").hide();
                 $("#ColType").hide();
                $("#MaxLength").hide();
              ;
            });
            $("[href=#UserDelete]").click(function() 
            { 
              
              $("table.#PnlUserCreation").hide();
              $("table.#PnlRoleAssign").hide();
              $("table.#PnlAddCols").hide();
              $("table.#PnlSupportTable").hide();
              $("div.#DeleteLock").hide();       
               $("table.#PnlRefreshSchema").hide();       
              $("table.#PhlUserDelete").show(500);              
              var jsonData = JSON.stringify({});
              $.ajax({
                type: "POST",
                url: "WebService.asmx/GetUserForDelete",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: jsonData,     
                success: function(data) 
                {
                    PopulateDropdown(data);
                },
                error: AjaxFailed
               });                              
              $("#btnAddCol").hide();
              $("#btnUserSubmit").hide();
              $("#btnRoleSubmit").hide();
              $("#LabelSucess").hide();
              ;
            });
            $("[href=#DeleteLocks]").click(function() 
            { 
              $("table.#PnlUserCreation").hide();
              $("table.#PnlRoleAssign").hide();
              $("table.#PnlAddCols").hide();
              $("table.#PhlUserDelete").hide();
              $("table.#PnlSupportTable").hide();
               $("table.#PnlRefreshSchema").hide();
              $("div.#DeleteLock").show(500);
              $("#btnAddCol").hide();
              $("#btnUserSubmit").hide();
              $("#btnRoleSubmit").hide();               
              $("#LabelSucess").hide();
            });
            $("[href=#RefreshSchema]").click(function() 
            { 
              $("table.#PnlUserCreation").hide();
              $("table.#PnlRoleAssign").hide();
              $("table.#PnlAddCols").hide();
              $("table.#PnlSupportTable").hide();
              $("table.#PhlUserDelete").hide();
              $("div.#DeleteLock").hide();  
               $("table.#PnlRefreshSchema").show(500);            
              $("#btnAddCol").hide();
              $("#btnUserSubmit").hide();
              $("#btnRoleSubmit").hide();
              $("#LabelSucess").hide();
              ;
            });
            
            $("[href=#SupportTable]").click(function() 
            { 
              $("table.#PnlUserCreation").hide();
              $("table.#PnlRoleAssign").hide();
              $("table.#PnlAddCols").hide();
              $("table.#PnlSupportTable").show(500);
              $("table.#PhlUserDelete").hide();
              $("div.#DeleteLock").hide();  
               $("table.#PnlRefreshSchema").hide();            
              $("#btnAddCol").hide();
              $("#btnUserSubmit").hide();
              $("#btnRoleSubmit").hide();
              $("#LabelSucess").hide();
              ;
            });
            
        }); 
        
        $("#DeleteLocksLink").click(function() 
            { 
              $("table.#PnlUserCreation").hide();
              $("table.#PnlRoleAssign").hide();
              $("table.#PnlAddCols").hide();
              $("table.#PhlUserDelete").hide();
              $("table.#PnlSupportTable").hide();
              $("div.#DeleteLock").show(500);
              $("#btnAddCol").hide();
              $("#btnUserSubmit").hide();
              $("#btnRoleSubmit").hide();               
              $("#LabelSucess").hide();
              $.ajax({
                type: "Get",
                url: "GetListData.aspx?Anand=1",
                dataType: "text",
                data:{},
                success: function(msg) 
                {
                 DeleteLock(msg);
                },
                 error: AjaxFailed
              });
              ;
            });
        
        $(document).ready(function() {
          $("#DeleteLocksLink").click(function() 
            { 
              fnGetLockGridDetails();     
            });
         });

        
        $('#datatable tbody td img.Delete').live('click', function () {
					var nTr = this.parentNode.parentNode;
					operation='DELETE';//setting the operation 
					fnGetLockDetails(oTable, nTr);
		} );
		
		function fnGetLockGridDetails()
        {
             var jsonData = JSON.stringify({ ID : 2 });
             $.ajax({
                type: "POST",
                url: "WebService.asmx/PopulateLockGrid",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: jsonData,     
                success: function(data) 
                {
                 $("#DeleteLock").html('');
                 $("#DeleteLock").append(data);
                    /* Add a click handler to the rows - this could be used as a callback */
				    $("#datatable tbody").click(function(event) {
					    $(oTable.fnSettings().aoData).each(function (){
						    $(this.nTr).removeClass('row_selected');
					    });
					    $(event.target.parentNode).addClass('row_selected');
				    });
                 
                    //Changes for adding Edit in the row
				    var nCloneTh = document.createElement( 'th' );
				    var nCloneTd = document.createElement( 'td' );
				    nCloneTd.innerHTML = '<img class="Delete" title="Delete Lock" src="images/Delete.gif" />';
				    nCloneTd.className = "center";
    				
				    $('#datatable thead tr').each( function () {
					    this.insertBefore( nCloneTh, this.childNodes[0] );
				    } );
    				
				    $('#datatable tbody tr').each( function () {
					    this.insertBefore(  nCloneTd.cloneNode( true ), this.childNodes[0] );
				    } );
				    
				     oTable=$("#datatable").dataTable({
					    "aoColumnDefs": [
						    { "bSortable": false, "aTargets": [ 0 ] }],
					    "aoColumnDefs": [
						    { "bSortable": false, "aTargets": [ 1 ] }]
					});			    
				    
				    
                },
                 error: AjaxFailed
              });
        }	
		
		
		function fnGetLockDetails(oTable, nTr)
        {
             var oData = oTable.fnGetData( nTr );
             var oSelectedTable = oData[1];
             var oSelectedRowID = oData[2];
             var oSelectedIserID = oData[3];
             var flag=window.confirm("Are you sure you want to delete the Lock?")
             if(flag)
             {
             var jsonData = JSON.stringify({ TableID : oSelectedTable ,RowID : oSelectedRowID ,UserId : oSelectedIserID});
             $.ajax({
                type: "POST",
                url: "WebService.asmx/DeleteLockGridRow",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: jsonData,     
                success: function(data) 
                {
                fnGetLockGridDetails();
                },
                 error: AjaxFailed
              });
              }
              else
              return;
        }	
        
        function fnGetSelected( oTableLocal )
		{
			var aReturn = new Array();
			var aTrs = oTableLocal.fnGetNodes();
			
			for ( var i=0 ; i<aTrs.length ; i++ )
			{
				if ( $(aTrs[i]).hasClass('row_selected') )
				{
					aReturn.push( aTrs[i] );
				}
			}
			return aReturn;
		}
                   
        function ValidateUser()
        {   
            if(document.getElementById("txtLoginName").value == '' || document.getElementById("txtPass").value == '' || document.getElementById("txtLstNme").value == '' || document.getElementById("txtFstNme").value == '' || document.getElementById("txtEmail").value == '' || document.getElementById("drdProfile").selectedIndex == 0)
            {
                alert("Enter Mandatory(*) fields");
                return false;
            }                                                           
        }
        function ValidateRole()
        {            
            if(document.getElementById("drdUsers").selectedIndex == 0 || document.getElementById("drdRole").selectedIndex == 0 || document.getElementById("drdProfileforUser").selectedIndex == 0 )
            {
                alert("Enter Mandatory(*) fields");
                return false;
            }            
            var Role = document.getElementById("drdRole").value;
            //var AssQueue = document.getElementById("drdAssQueue").value;
            //var PRodReport = document.getElementById("drdIncInProdReport").value;
            var UserProfile = document.getElementById("drdProfileforUser").value;
            var UserID = document.getElementById("drdUsers").value;
            var jsonData = JSON.stringify({ Role : Role ,UserProfile : UserProfile ,UserID : UserID});
             $.ajax({
                type: "POST",
                url: "WebService.asmx/UserUpdate",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: jsonData,     
                success: function(data) 
                {            
                 PopulateDrd(data);
                },
                 error: AjaxFailed
              }); 
        }
      function GetDataFromSQL(object)
      {
      var xmlHttp;
      if (window.ActiveXObject)
        { 
        xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
        }
        else if (window.XMLHttpRequest)
        { 
        xmlHttp = new XMLHttpRequest();
        }

        var Source = object;
        switch(Source.id)
        {        
        case"UserCreate":
                
        break;
        case"AssignRole":
            
        break;
        case"AddColumn":
            
        break;
        case"DeleteUser":
            
        break;
        case"DeleteLocks":
            
        break;
        case"RefreshSchema":
            
        break;
        default:
            break;
        }
    }    



  
  function GetDropDownValues(object)
  { 
  
    if(object.selectedIndex==0)
    {
        alert("Please Select a User ID");
        document.getElementById("drdRole").selectedIndex=0;
//        document.getElementById("drdAssQueue").selectedIndex=0;
//        document.getElementById("drdIncInProdReport").selectedIndex=0;
        document.getElementById("drdProfileforUser").selectedIndex=0;
        return false;
    }
    else
    {
        var SelectedValue = object.value;
        var jsonData = JSON.stringify({ DrdValue : SelectedValue });
                 $.ajax({
                    type: "POST",
                    url: "WebService.asmx/GetDataForAdminDrd",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: jsonData,     
                    success: function(data)             
                    {
                      AjaxSucceeded(data)
                    },
                     error: AjaxFailed
                  });
                    }   
  }
  function AjaxSucceeded(result) 
  {
   //alert(result.d);
   //debugger;
//    if (window.ActiveXObject) 
//    {
//		//for IE
//		xmlDoc=new ActiveXObject("Microsoft.XMLDOM");
//		xmlDoc.async="false";
//		xmlDoc.loadXML(result);
//	} 
//	else if (document.implementation && document.implementation.createDocument) 
//	{
//		//for Mozila
//		parser=new DOMParser();
//		xmlDoc=parser.parseFromString(result,"text/xml");			
//	}	
var data=new Array();
data=eval(result);
	document.getElementById("drdRole").value=data[0];
    //xmlDoc.getElementsByTagName("anyType")[0].childNodes[0].data;
//    document.getElementById("drdAssQueue").value=   
//    xmlDoc.getElementsByTagName("anyType")[1].childNodes[0].data;
//    document.getElementById("drdIncInProdReport").value=
//    xmlDoc.getElementsByTagName("anyType")[2].childNodes[0].data;
    document.getElementById("drdProfileforUser").value=data[1];
    //xmlDoc.getElementsByTagName("anyType")[1].childNodes[0].data;
  }
          
  function AjaxFailed(result) 
  {
       alert(result.status + ' ' + result.statusText);
  }  
          
   function AddtoUser(msg)
   {
       
   if (window.ActiveXObject) 
    {
		//for IE
		xmlDoc=new ActiveXObject("Microsoft.XMLDOM");
		xmlDoc.async="false";
		xmlDoc.loadXML(msg);			
	} 
	else if (document.implementation && document.implementation.createDocument) 
	{
		//for Mozila
		parser=new DOMParser();
		xmlDoc=parser.parseFromString(msg,"text/xml");			
	}
	if(document.getElementById("drdUsers").options.length>1)
    {
        var i = document.getElementById("drdUsers").options.length;
        while(i>0)
        {
        document.getElementById("drdUsers").remove(i);
        i--;
        }
    }
	for(i=0;i<xmlDoc.getElementsByTagName("anyType").length;i++)		
	AddItem(xmlDoc.getElementsByTagName("anyType")[i].childNodes[0].data);
   }
   function AddItem(Text)
    {    
    var opt = document.createElement("option");    
    // Add an Option object to Drop Down/List Box
    document.getElementById("drdUsers").options.add(opt);        // Assign text and value to Option object
    opt.text = Text;
    opt.value = Text;
    } 
   function PopulateDrd(result)
   {
//   if (window.ActiveXObject) 
//    {
//		//for IE
//		xmlDoc=new ActiveXObject("Microsoft.XMLDOM");
//		xmlDoc.async="false";
//		xmlDoc.loadXML(result);			
//	} 
//	else if (document.implementation && document.implementation.createDocument) 
//	{
//		//for Mozila
//		parser=new DOMParser();
//		xmlDoc=parser.parseFromString(result,"text/xml");			
//	}
     alert("Role Updated SucessFully");
	
//	if(data[0]. && xmlDoc.getElementsByTagName("anyType")[1].childNodes[0].data)
//	{	    
//        alert("Role Updated SucessFully");
//	}
   }
   function DeleteLock(msg)
   {
    if (window.ActiveXObject) 
    {
		//for IE
		xmlDoc=new ActiveXObject("Microsoft.XMLDOM");
		xmlDoc.async="false";
		xmlDoc.loadXML(msg);			
	} 
	else if (document.implementation && document.implementation.createDocument) 
	{
		//for Mozila
		parser=new DOMParser();
		xmlDoc=parser.parseFromString(msg,"text/xml");			
	}	
	/*if(!xmlDoc.getElementsByTagName("anyType")[0].childNodes[0].data);
	{
	    alert("No Locks");
	}*/	
	    for(i=0;i<xmlDoc.getElementsByTagName("anyType").length;i++)		
	    {
	        
        }
        
			
   }
function  DeleteSelUser()
{      
    if(document.getElementById("drdUserID").selectedIndex==0)
    {
        alert("Please Select a User ID");        
        return false;
    }
    var SelectedValue =document.getElementById("drdUserID").value
    var jsonData = JSON.stringify({ UserName : SelectedValue });
     $.ajax({
        type: "POST",
        url: "WebService.asmx/DeleteUser",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: jsonData,     
        success: function(data)             
        {
            DeleteUser(data);
        },
         error: AjaxFailed
      });                              
}
   
     function RefreshSchema()
     {
        $.ajax({
        type: "POST",
        url: "WebService.asmx/RefreshSchema",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{}",     
        success: function(data)             
        {
            alert('Schema has been refreshed');
        },
         error: AjaxFailed
      });  
     }
     function Support()
     {
     if(document.getElementById("txtFieldtype").value=="")
     {
     alert("Please enter the mandatory fields");
     return;
     }
     var fieldType =document.getElementById("txtFieldtype").value
     var description =document.getElementById("txtFieldDes").value
     var jsonData = JSON.stringify({ fieldType : fieldType, description : description });
     $.ajax({
        type: "POST",
        url: "WebService.asmx/AddSupportData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: jsonData,     
        success: function(data)             
        {
            document.getElementById("txtFieldtype").value="";
            document.getElementById("txtFieldDes").value="";
            alert('Field added to Support Table');
        },
         error: AjaxFailed
      });
     }
   function DeleteUser(msg)
   {
//   if (window.ActiveXObject) 
//    {
//		//for IE
//		xmlDoc=new ActiveXObject("Microsoft.XMLDOM");
//		xmlDoc.async="false";
//		xmlDoc.loadXML(msg);			
//	} 
//	else if (document.implementation && document.implementation.createDocument) 
//	{
//		//for Mozila
//		parser=new DOMParser();
//		xmlDoc=parser.parseFromString(msg,"text/xml");			
//	}
    
    var data= eval(msg);
    if(data == true)
    {	    
        alert("User Deleted SucessFully");
    }
   GetUserForDelete();
}
   function GetUserForDelete()
   {    
    
      var jsonData = JSON.stringify({});
      $.ajax({
        type: "POST",
        url: "WebService.asmx/GetUserForDelete",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: jsonData,     
        success: function(data) 
        {
            PopulateDropdown(data);
        },
        error: AjaxFailed
       });          
    
   }
   
 
   function PopulateDropdown(result) 
  {
   //alert(result.d);
//    if (window.ActiveXObject) 
//    {
//		//for IE
//		xmlDoc=new ActiveXObject("Microsoft.XMLDOM");
//		xmlDoc.async="false";
//		xmlDoc.loadXML(result);			
//	} 
//	else if (document.implementation && document.implementation.createDocument) 
//	{
//		//for Mozila
//		parser=new DOMParser();
//		xmlDoc=parser.parseFromString(result,"text/xml");			
//	}
    var data=new Array();
    data = eval(result);

	if(document.getElementById("drdUserID").options.length>1)
    {
        var i = document.getElementById("drdUserID").options.length;
        while(i>0)
        {
        document.getElementById("drdUserID").remove(i);
        i--;
        }
    }
	for(i=0;i<data.length;i++)		
	    AddItemtoDelete(data[i]);
   }
   function AddItemtoDelete(Text)
    {    
    var opt = document.createElement("option");    
    // Add an Option object to Drop Down/List Box
    document.getElementById("drdUserID").options.add(opt);        // Assign text and value to Option object
    opt.text = Text;
    opt.value = Text;
   } 
  function ShowOptionsforDate()
  {
  if(document.getElementById("radioDateY").checked ==true)
  {
    $("#ColType").hide();
    $("#MaxLength").hide();
    
  }
  if(document.getElementById("radioDateN").checked ==true)
  {
    $("#ColType").show(100);
    $("#MaxLength").show(100);
  }
  if(document.getElementById("radioReadY").checked ==true)
  {
        $("#Sys").show(100);
    $("#SystemAction").hide();
    if(document.getElementById("radioSysY").checked ==true)
      {
        $("#SystemAction").show(100);
      }  
      if(document.getElementById("radioSysN").checked ==true)
      {
      $("#SystemAction").hide();
        
      }  
  }
  if(document.getElementById("radioReadN").checked ==true)
  {
    $("#Sys").hide();
    $("#SystemAction").hide();
  }
  
  }
  
 //Called on Click of Add Column Button On Admin Tab
 function AddColClick()
 {
    var ReportName = document.getElementById("drdReport1").value;
    var ColName="";
    var RadioDate="";
    var RadioRead ="";
    var RadioButtonIsUpSystem="";
    var SystemAction="";
    var RadioRead="";
    var ColType="";
    var MaxLength="";
    var PrimaryKey="";
    
    if(document.getElementById("txtColName").value =="" ||(document.getElementById("radioDateY").checked==false && document.getElementById("radioDateN").checked==false)||(document.getElementById("radioReadY").checked==false && document.getElementById("radioReadN").checked==false)||(document.getElementById("RadioBtnDuplicateY").checked==false&&document.getElementById("RadioBtnDuplicateN").checked==false))
    {    
        alert("Please Enter Mandatory Fields");
        return false;
    }    
    else
    {
        ColName = document.getElementById("txtColName").value;
        if(document.getElementById("RadioBtnDuplicateY").checked==true)
        {
            PrimaryKey = "Y";
        }
        else
        {
            PrimaryKey = "N";
        }
    }
    if(document.getElementById("radioDateY").checked == true)
    {
        RadioDate = "Y";
    }
    if(document.getElementById("radioDateN").checked == true)
    {
        RadioDate = "N";
        ColType = document.getElementById("drdDataType").value;
        MaxLength = document.getElementById("txtLength").value;
        
    }
    if(RadioDate=="N")
    {
        if(document.getElementById("drdDataType").value=="" || document.getElementById("txtLength").value=="")
        {
            alert("Please Select Column Data Type and Maximum Length");
        }    
    }    
    if(document.getElementById("radioReadY").checked == true)
    {
        RadioRead ="Y"
        if(document.getElementById("radioSysY").checked == true)
        {
            RadioButtonIsUpSystem ="Y";
            SystemAction = document.getElementById("DropDownAuto").value;
        }
        else
        {
            if(document.getElementById("radioSysY").checked == false && document.getElementById("radioSysN").checked==false)
            {
                alert("Please Select "+ document.getElementById("lblUpdate").innerText);
                return false;
            }
            RadioButtonIsUpSystem ="N";
            SystemAction = "";
        }
    }
    else
    {
        RadioRead ="N";
        RadioButtonIsUpSystem ="N";
        SystemAction = "";
    }
    var jsonData = JSON.stringify({ReportType:ReportName,ColName:ColName,RadioDate:RadioDate,ColType:ColType,MaxLength:MaxLength,RadioRead:RadioRead,RadioButtonIsUpSystem:RadioButtonIsUpSystem,SystemAction:SystemAction,PrimaryKey:PrimaryKey});
             $.ajax({
                type: "POST",
                url: "WebService.asmx/UpdateTable",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: jsonData,     
                success: function(data)             
                {
                //AddtoUser(msg);
                 alert(data);
                  document.getElementById("txtColName").value = "";
                  document.getElementById("txtLength").value = "";
                  document.getElementById("radioDateY").checked = false;
                  document.getElementById("radioDateN").checked = false;
                  document.getElementById("radioReadY").checked = false;
                  document.getElementById("radioReadN").checked = false;
                  document.getElementById("radioSysY").checked = false;
                  document.getElementById("radioSysN").checked = false;
                  document.getElementById("RadioBtnDuplicateY").checked = false;
                  document.getElementById("RadioBtnDuplicateN").checked = false;
                 
                },
                 error: AjaxFailed
              });                 
 }
 
 
 
 //Anand: Changes
 function fnAddtoUser(msg)
 {
       var data=new Array();
       data=eval(msg);
       var selectbox=document.getElementById('drdUsers');
        for(var i=0;i<(data.length);i++)
        {
            addOption(selectbox,data[i],data[i]);
        }
 }
 
 
 function addOption(selectbox,text,value )
{
    var optn = document.createElement("OPTION");
    optn.text = text;
    optn.value = value;
    selectbox.options.add(optn);
}
