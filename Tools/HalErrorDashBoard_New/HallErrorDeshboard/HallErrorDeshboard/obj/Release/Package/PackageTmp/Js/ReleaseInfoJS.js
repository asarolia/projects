// JScript File for ReleaseInfo Page
var oTable;
var operation;//stores the operation value for template
var RELEASE_TAB=new Array();//Need to be declared for all the tables for which dynamic ajax template is genearted
var releaseScheme;
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
	    fnCancel(tableName);
	    
	}
    });
    
  $('#datatable tbody td img.view').live('click', function () {
					var nTr = this.parentNode.parentNode;
					var oData = oTable.fnGetData( nTr );
						fnPopulateTemplate('RELEASE_TAB',oData[2],'VIEW');
				} );
				
 $('#datatable tbody td img.edit').live('click', function () {
					var nTr = this.parentNode.parentNode;
					var oData = oTable.fnGetData( nTr );
						fnPopulateTemplate('RELEASE_TAB',oData[2],'UPDATE');
				} );
								
 $('#btnGetRelease').click(function() {
    var releaseYear=document.getElementById('DropDownRelYear').value;
     var releaseName=document.getElementById('DropDownRelName').value;   
     if(releaseYear=="DEFAULT")
        {
            alert("Select a Release Year from Dropdown");
            return false;
        }
     $('#loading').removeClass('hidden');
     releaseScheme={releaseName:releaseName,releaseYear:releaseYear};
     fnPopulateGrid(releaseScheme);
     $('#divGrid').removeClass('hidden');
       $('#loading').addClass('hidden');
      });
      
 });
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
              var response=eval(data);
              $('#divDetailsView').html('');
              $('#divDetailsView').append(response.html);
              $('#divDetailsView').show();
              RELEASE_TAB=response.controlIDs;
              fnValidate(tableName);
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
              success: function(data) {
              fnPopulateGrid(releaseScheme);
              alert('Record updated sucessfully');
              }
            });
    }
    function fnGetValues(tableName)
    {
        var controlIDs=new Array();
        var controlValues=new Array();
        controlIDs=RELEASE_TAB;
        for(var i=0; i<controlIDs.length ;i++)
        {
            controlValues[i]=document.getElementById(tableName + controlIDs[i]).value;
        }
        var operationScheme={tableName: tableName, data: controlValues};
        return JSON.stringify({ operationScheme : operationScheme });
    }    
    
    function fnValidate(tableName)
    {
        $("#"+tableName+"").validate();
    }

    function fnCancel(tableName)
    {
    $('#divDetailsView').html('');
    $('#divDetailsView').hide();
    }
    
    function fnPopulateGrid(releaseScheme)
    {
    var jsonData=JSON.stringify({ releaseScheme: releaseScheme });
     $.ajax({
      type: "POST",
      url: "WebService.asmx/PopulateReleaseGrid",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      data: jsonData,
      error: function (error){ 
      alert ('Error ' + (error.responseText));
      $('#loading').addClass('hidden');
      },
      success: function(data) {
      $('#divGrid').html('');
      $('#divGrid').append(data);
      
       //Changes for edit details of the row
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
      //Changes for view details of the row
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
				
				
				
       oTable=$("#datatable").dataTable({
					"aoColumnDefs": [
						{ "bSortable": false, "aTargets": [ 0 ] }
					],
					"aaSorting": [[2, 'asc']],
					"aoColumnDefs": [ 
						{ "bVisible": false, "aTargets": [2] }
					] 
					});
      }
      });
    }