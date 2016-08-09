// JScript File

var objAjaxResponse;
var objProposition;

$(document).ready(function (){
    var PropositionListDropDown = $('#spanPropositionListDropDown select');
    
    PropositionListDropDown.change(function () {
        var scheme = $(this).val();
        $('#spanPropositionListDropDown input').val(scheme);
        GetPropositionDetails(scheme);
    });
    
    $('#spanEffectiveDate input').datepicker({
			changeMonth: true,
			changeYear: true
		});
    $('#spanExpirationDate input').datepicker({
			changeMonth: true,
			changeYear: true
		});
    
    $('#aCloneProposition').addClass('selected');
    $('#nCloneProposition').addClass('selected');
});

function GetPropositionDetails(scheme)
{
    if (scheme.length == 0)
    {
        $('#AjaxPropositionDetails').hide(1000);
        return;
    }
    
    $('#AjaxPropositionDetails').hide();
    $('#wAjaxPropositionDetails').show();
    CloneProposition_Proxy.GetPropositionDetails(scheme,successCallback, failureCallback);
     
}

var successCallback = function(data){
    objAjaxResponse = eval(data.d);
    objProposition = objAjaxResponse;
    
    $('#AjaxPropositionDetails').html(' ');
    $('#wAjaxPropositionDetails').hide();
    var template = $('#AjayPropositionDetailsTemplate').html();

    template = template.replace('{Name}',objProposition.Name);
    template = template.replace('{ProductCode}',objProposition.ProductCode);
    template = template.replace('{MasterCompanyNumber}',objProposition.MasterCompanyNumber);
    template = template.replace('{LOBCode}',objProposition.LOBCode);
    template = template.replace('{SchemeCode}',objProposition.SchemeCode);
    $('#SpanLiveSchemeCode input').val(objProposition.SchemeCode);
    
//    $(template).bindTo(objAjaxResponse, { fill: true, appendTo: '#AjaxPropositionDetails' });
    
    $('#AjaxPropositionDetails').html(template);
    $('#AjaxPropositionDetails').show(1000);
};

var failureCallback = function(data){
    var text = data.responseText;
    $('#wAjaxPropositionDetails').hide();

    if (text.indexOf("Description:",0) > -1)
    {
        text = text.substr(text.indexOf("Description:",0),text.length - text.indexOf("Description:",0));
    }
    
    alert('Ajax Request failed.\n' + 'Status:' + data.status + '\nStatus Text:' + data.statusText + '\nResponse Text:' + text);
};
