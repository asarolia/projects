// JScript File    
var oTable;
var odependencyTable;
var giRedraw = false;
var selectedRowId;//stores the value of selected row in main grid
var operation;//stores the opeartion value for dependency template
var MASTERERROR=new Array();//Need to be declared for all the tables for which dynamic ajax template is genearted
var DEPENDENCY_TAB=new Array();//Need to be declared for all the tables for which dynamic ajax template is genearted

     $(document).ready(function() {
        
     $.validator.setDefaults({
	submitHandler: function() { 
	    
	    var operationName =this.currentForm.className;
	    var tableName = this.currentForm.id; 
	    if(operationName=="UPDATE")
	    {
	    fnUpdate(tableName);
	    }
	    else if(operationName=="INSERT")
	    {
	    fnInsert(tableName);
	    }
	    if(tableName=="DEPENDENCY_TAB")
	    {
	    $('#editRight').hide();
	    fnRepopulateDGrid();
	    }
	    else if(tableName=="MASTERERROR")
	    {
	    fnSearchData();
	    }
	}
    });
            
     $('#datatable tbody td img.edit').live('click', function () {
					var nTr = this.parentNode.parentNode;
					var oData = oTable.fnGetData( nTr );
					operation = 'UPDATE'; //setting the oparation 
					fnEditDisplay(oTable, nTr);
//					var jsonData = JSON.stringify({ ID: oData[2] });
//    $.ajax({
//              type: "POST",
//              url: "WebService.asmx/CheckLock",
//              contentType: "application/json; charset=utf-8",
//              dataType: "json",
//              data: jsonData,       
//              error: function (error){
//              alert ('Error ' + (error.responseText));
//              },
//              success: function(data) {
//              bool=eval(data);
//              if(bool)
//              {
//              fnEditDisplay(oTable, nTr);
//              }
//              else
//              {
//              alert('Row is locked for editing');
//              }
//              }
//              });
						
				} );
				
    $('#datatable tbody td img.view').live('click', function () {
					var nTr = this.parentNode.parentNode;
					operation='VIEW';//setting the operation 
						fnViewDisplay(oTable, nTr);
				} );				
     
      $('#dependencytable tbody td img.editD').live('click', function () {
					var nTr = this.parentNode.parentNode;
				    fnEditDependencyDisplay(oTable, nTr);
				} );
				
    // fnLoadDrpDwnValues();//loads dropdown values for search
     
     $('#btnReset').click(function() {
     form1.reset();
     $("#divDataTable").html('');
     $('#divControl').hide();
     });
     
     
     $('#btnBack').click(function() {
     //fnReleaseLock();
     $.modal.impl.close();//closes the modal dialogue
     });
     
     $('#btnCancel').click(function() {
     //fnReleaseLock();
     $.modal.impl.close();//closes the modal dialogue
     });
     
     $('#btnOwned').click(function() {
     fnSearchData(true);
     $('#divControl').show();
     });
     
     
     $('#btnAddNew').click(function(){
	 fnPopulateTemplate('DEPENDENCY_TAB',0,'INSERT');
     });
     
     
     $("#btnSearch").click(function() {
     fnSearchData(false);
     $('#divControl').show();
     });  


    /* Add a click handler for the select row for dependency Grid */
				$('#btnDelete').click( function() {
					var anSelected = fnGetSelected( odependencyTable );
					if(anSelected.length > 0)
					{
					    var oData=odependencyTable.fnGetData(anSelected[0]);
					    fnDeleteDependency(oData[1]);//Selects the Master_ID value
					}
					else
					{
					    alert("Please select a row");					    
					}
				} );
});//End of documnet.ready function



function fnSearchData(flag)//Searchs data and add to main Grid on page
{
    $('#loading').removeClass('hidden');
    var fromdt = document.getElementById('txtfrom').value;
    var todt = document.getElementById('txtto').value;  
    var status =  document.getElementById('drpStatus').value;
     /*var status=document.getElementById('drpStatus').value;  
     var crelease=document.getElementById('drpCSCprpsd').value;  
     var lead=document.getElementById('drpLead').value; 
     var category= document.getElementById('drpCategory').value; 
     var release= document.getElementById('drpRelease').value; 
     var owner= document.getElementById('drpOwner').value; */
    var scheme = { fromdt: fromdt, todt: todt, status: status }; 
     var jsonData = JSON.stringify({ scheme: scheme });


     $.ajax({
         type: "POST",
         url: "DashBoard.aspx/PopulateData",
         contentType: "application/json; charset=utf-8",
         dataType: "json",
         data: jsonData,
         error: function (error) {
             $('#loading').addClass('hidden');
             alert('Error ' + (error.responseText));
         },
         success: function (data) {
             $("#divDataTable").html('');
             //alert(data.d);
             $("#divDataTable").append(data.d);            
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
             				nCloneTd.innerHTML = '<img class="edit" title="Edit Details" src="images/EditIcon.gif" />';
             				nCloneTd.className = "center";
             				
             				$('#datatable thead tr').each( function () {
             					this.insertBefore( nCloneTh, this.childNodes[0] );
             				} );
             				
             				$('#datatable tbody tr').each( function () {
             					this.insertBefore(  nCloneTd.cloneNode( true ), this.childNodes[0] );
             				} );
             				
             				
             				//Changes for view detaisl of the row
             				var nCloneTh = document.createElement( 'th' );
             				var nCloneTd = document.createElement( 'td' );
             				nCloneTd.innerHTML = '<img class="view" title="View Details" src="images/expand_blue.jpg" />';
             				nCloneTd.className = "center";
             				
             				$('#datatable thead tr').each( function () {
             					this.insertBefore( nCloneTh, this.childNodes[0] );
             				} );
             				
             				$('#datatable tbody tr').each( function () {
             					this.insertBefore(  nCloneTd.cloneNode( true ), this.childNodes[0] );
             				} );
             				

             /*
             * Initialse DataTables
             */
             oTable = $("#datatable").dataTable({
                 "aoColumnDefs": [
						{ "bSortable": false, "aTargets": [0] }
					],
                 "aoColumnDefs": [
						{ "bSortable": false, "aTargets": [1] }
					],
                 "aaSorting": [[2, 'asc']],
                 "aoColumnDefs": [
						{ "bVisible": false, "aTargets": [2] }
					]
             }
      );
             $('#loading').addClass('hidden');

         }
     })
}

/* Get the rows which are currently selected */
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

function fnPopulateDependencyGrid(master_ID)
{
var jsonData = JSON.stringify({ ID : master_ID });
    $.ajax({
      type: "POST",
      url: "WebService.asmx/PopulateDependencyGrid",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      data: jsonData,
      error: function (error){ 
      
      alert ('Error ' + (error.responseText));
      },
      success: function(data) {
      
      $("#rightGrid").html('');
      $("#rightGrid").append(data);
       /* Add a click handler to the rows - this could be used as a callback */
				$("#dependencytable tbody").click(function(event) {
					$(odependencyTable.fnSettings().aoData).each(function (){
						$(this.nTr).removeClass('row_selected');
					});
					$(event.target.parentNode).addClass('row_selected');
				});
				
				
				//Changes for adding Edit in the row
				var nCloneTh = document.createElement( 'th' );
				var nCloneTd = document.createElement( 'td' );
				if(operation=='UPDATE')
				{
				nCloneTd.innerHTML = '<img class="editD" title="Edit Details" src="images/EditIcon.gif" />';
				}
				else if(operation=='VIEW')
				{
				nCloneTd.innerHTML = '<img class="editD" title="View Details" src="images/expand_blue.jpg" />';
				}
				nCloneTd.className = "center";
				
				$('#dependencytable thead tr').each( function () {
					this.insertBefore( nCloneTh, this.childNodes[0] );
				} );
				
				$('#dependencytable tbody tr').each( function () {
					this.insertBefore(  nCloneTd.cloneNode( true ), this.childNodes[0] );
				} );
				
    /*
	* Initialse DataTables
	*/
      odependencyTable=$("#dependencytable").dataTable({
					"aoColumnDefs": [ 
						{ "bVisible": false, "aTargets": [1] }
					]
					}
      
      );
      }
      });  
}

function fnRepopulateDGrid()
{
	fnPopulateDependencyGrid(selectedRowId);
}

function fnGetEditTemplate(master_ID)
{

    var jsonData = JSON.stringify({ ID : master_ID });
    $.ajax({
      type: "POST",
      url: "WebService.asmx/GetExistingData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      data: jsonData,
      error: function (error){ 
      //
      alert ('Error ' + (error.responseText));
      },
      success: function(data) {
      
      var array=data;
      array=eval(data);
      
      switch (array[0]) {

    case 'lock':
    
        alert('Row is locked for editing');
    break;
        
    default:
    
        for(var i=0;i<array.length;i++)
          {
          if (array[i]!= undefined && document.getElementById(i+1).value != undefined)
            {
                document.getElementById(i+1).value=array[i];
            }
           
          }
          $('#divEditView').modal();
		$('#btnAddData').hide();
		$('#btnUpdateData').show();
    break;

        }

      }
      
      
      });  
}

function fnLoadDrpDwnValues()
{
    
    $.ajax({
      type: "POST",
      url: "WebService.asmx/GetDrpDwnData",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      data: "{}",
      error: function (error){ 
      
      alert ('Error ' + (error.responseText));
      },
      success: function(data) {
      
      var array=eval(data);
      
            var selectbox=document.getElementsByTagName('SELECT');
            for(var j=5;j<selectbox.length;j++)
            {
                for(var i=0;i<(array.length);i++)
                {
                    var box=selectbox.item(j);
                    if(box.name==array[i])
                    {
                        i++;
                        box.options[box.options.length] = new Option(array[i],array[i]);
                    }       
                }
            }
      }
      
      
      });  
}

function addOption(selectbox,text,value )
{
var optn = document.createElement("OPTION");
optn.text = text;
optn.value = value;
selectbox.options.add(optn);
}


//Called from jquery.simplemodal.js file
function fnReleaseLock()
{
     $.ajax({
      type: "POST",
      url: "WebService.asmx/DeleteLock",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      data: "{}",
      error: function (error){ 
      
      alert ('Error ' + (error.responseText));
      },
      success: function() {
      
      }
      })
   
}

function fnDeleteDependency(master_ID)
{
var jsonData = JSON.stringify({ ID : master_ID });
    $.ajax({
      type: "POST",
      url: "WebService.asmx/DeleteDependency",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      data: jsonData,
      error: function (error){ 
      
      alert ('Error ' + (error.responseText));
      },
      success: function() {
      fnRepopulateDGrid();
      }
      })
}


function fnEditDisplay(oTable, nTr)
{
    
    var oData = oTable.fnGetData( nTr );
					$('#divEditLeft').removeClass('divEditCenter');
					$('#divEditLeft').addClass('divEditLeft');
					$('.divEditRight').show();
					selectedRowId=oData[2];
					$('#dependencypnl').show();
					//fnPopulateDependencyGrid(selectedRowId);
					operation='UPDATE';
					fnPopulateTemplate('MASTERERROR',oData[2],'UPDATE');                    
}

function fnViewDisplay(oTable, nTr) {
    var oData = oTable.fnGetData(nTr);
    $('#divEditLeft').addClass('divEditLeft');
    $('.divEditRight').show();
    selectedRowId = oData[2];
    //fnPopulateDependencyGrid(selectedRowId);
    operation = 'VIEW';
    $('#dependencypnl').hide();
    fnPopulateTemplate('MASTERERROR', oData[2], 'VIEW');
}
//function fnGetDetailsTemplate(master_ID)
//{

//    var jsonData = JSON.stringify({ ID : master_ID });
//    $.ajax({
//      type: "POST",
//      url: "WebService.asmx/GetData",
//      contentType: "application/json; charset=utf-8",
//      dataType: "json",
//      data: jsonData,
//      error: function (error){ 
//      //
//      alert ('Error ' + (error.responseText));
//      },
//      success: function(data) {
//      
//      var array=data;
//      array=eval(data);
//        for(var i=0;i<array.length;i++)
//          {
//          if (array[i]!= undefined && document.getElementById(i+1).value != undefined)
//            {
//                document.getElementById(i+1).value=array[i];
//                $('#'+(i+1)).attr('disabled','disabled');
//            }
//          }
//      }
//      });  
//}

function fnEditDependencyDisplay(oTable, nTr)
{
     var oData = odependencyTable.fnGetData( nTr );
     fnPopulateTemplate("DEPENDENCY_TAB",oData[1],operation);				   
}

//Changes due to dynamic template starts here
    function fnInsert(tableName)
    {
        var jsonData=fnGetValues(tableName);
        $.ajax({
              type: "POST",
              url: "WebService.asmx/AddData",
              contentType: "application/json; charset=utf-8",
              dataType: "json",
              data: jsonData,
              error: function (error){   
              alert ('Error ' + (error.responseText));
              },
              success: function(data) {
              alert('Record inserted sucessfully');
              }
            });
    }
    
 
    function fnUpdate(tableName)
    {
        var jsonData=fnGetValues(tableName);
        $.ajax({
              type: "POST",
              url: "WebService.asmx/UpdateData",
              contentType: "application/json; charset=utf-8",
              dataType: "json",
              data: jsonData,
              error: function (error){   
              alert ('Error ' + (error.responseText));
              },
          success: function (data) {
              fnSearchData();
              alert('Record updated sucessfully');
          }              
      });
      
    }
    function fnGetValues(tableName)
    {
        var controlIDs=new Array();
        var controlValues=new Array();
        if(tableName=="DEPENDENCY_TAB")
        {
            controlIDs=DEPENDENCY_TAB;
        }
        else if(tableName=="MASTERERROR")
        {
            controlIDs=MASTERERROR;
        }
        for(var i=0; i<controlIDs.length ;i++)
        {
            if (controlIDs[i] == "statustext") {
                var str = controlValues[i] = document.getElementById(tableName + controlIDs[i]).value;
                controlValues[i] = str.substr(0, str.indexOf(" "));
            }
            else {
                controlValues[i] = document.getElementById(tableName + controlIDs[i]).value;
            }
        }
        var operationScheme={tableName: tableName, data: controlValues};
        return JSON.stringify({ operationScheme : operationScheme });
    }    
    
    function fnValidate(tableName)
    {
        $("#"+tableName+"").validate();
    }
    
    function fnPopulateTemplate(tableName, Id, operation)
    {
            var templateScheme = { tableName: tableName, Id: Id, operation: operation }; 
            var jsonData = JSON.stringify({ templateScheme: templateScheme });
            
            $.ajax({
              type: "POST",
              url: "WebService.asmx/PopulateTemplateView",
              contentType: "application/json; charset=utf-8",
              dataType: "json",
              data: jsonData,       
              error: function (error){
              alert ('Error ' + (error.responseText));
              },
              success: function(data) {
              var response=eval(data.d);
			  if(tableName=="MASTERERROR")
			  {
			    $('#divEditLeft').html('');
			    $('#divEditLeft').append(response.html);
			    MASTERERROR=response.controlIDs;
			    $('#divEditView').modal();
			  }
			  else if(tableName=="DEPENDENCY_TAB")
			  {
			    $('#editRight').html('');
			    $('#editRight').append(response.html);
			    $('#editRight').show();
			    DEPENDENCY_TAB=response.controlIDs;
			  }
			  if(operation=='INSERT')
			  {
			    document.getElementById('DEPENDENCY_TABLPR_NBR').value = document.getElementById('MASTERERRORLPR_NBR').value;//code to keep LPR number same for both tables
		        $('#DEPENDENCY_TABLPR_NBR').attr('disabled','disabled');
			  }
			  fnValidate(tableName);
              }
            });
    }
    
function fnCheckLock(ID)
{
    var bool=false;
    var jsonData = JSON.stringify({ ID: ID });
    $.ajax({
              type: "POST",
              url: "WebService.asmx/CheckLock",
              contentType: "application/json; charset=utf-8",
              dataType: "json",
              data: jsonData,       
              error: function (error){
              debugger;
              alert ('Error ' + (error.responseText));
              },
              success: function(data) {
              debugger;
              bool=eval(data);
              }
              });
              return bool;
}    

function fnCancel(tableName)
{
    if(tableName=="MASTERERROR")
    {
    //fnReleaseLock();
    $.modal.impl.close();
    }
    else if(tableName=="DEPENDENCY_TAB")
    {
    $('#editRight').html('');
    $('#editRight').hide();
    }
}