// JScript File

var objAjaxResponse;
var objProposition;
var current;

$(document).ready(function (){
    ApplyChanges();
});

function DisableAllControls(sw)
{
    if (sw)
    {
        $('#divNext input').attr('disabled',true);
    }
    else
    {
        $('#divNext input').removeAttr('disabled');
    }
}

function ApplyChanges(step)
{
    if (typeof(step) == 'undefined')
    {
        ApplyChanges(1);
        return;
    }
   
    switch(step)
    {
        case 1:
            current = step;
            DisableAllControls(true);
            UpdateStatus(current,'starting');
            GenerateExcel_Proxy.Generate(successCallback,failureCallback);
            break;
        case 1.5: //1 failed
            UpdateStatus(current,'failed');
            $('#divStepMessage1').html(objAjaxResponse);
            break;
        case 1.9: //1 success
            UpdateStatus(current,'success');
            $('#divStepMessage1').html(' ');
            DisableAllControls(false);
            break;
    }
}

function UpdateStatus(item,status)
{
    var div,stat;
    div = '#divStep'+item;
    $(div).removeClass('starting loading failed success');
    $(div).addClass(status);  
}


var successCallback = function(data){
    objAjaxResponse = data.d;
    objProposition = objAjaxResponse;
    
    if (objAjaxResponse.length == 0)
        ApplyChanges(current + 0.9);
    else
    {
        ApplyChanges(current + 0.5);
    }
};


var failureCallback = function(data){
    var text = data.responseText;
    if (text.indexOf("Description:",0) > -1)
    {
        text = text.substr(text.indexOf("Description:",0),text.length - text.indexOf("Description:",0));
    }
    
    alert('Ajax Request failed.\n' + 'Status:' + data.status + '\nStatus Text:' + data.statusText + '\nResponse Text:' + text);
};
