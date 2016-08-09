// JScript File

var objAjaxResponse;
var objProposition;
var current;

$(document).ready(function (){
    var ApplyChangesButton = $('#spanApplyRulesButton input');
    
    ApplyChangesButton.click(function () {
        ApplyChanges();
        return false;
    });    
});

function DisableAllControls(sw)
{
    if (sw)
    {
        $('#spanApplyRulesButton input').attr('disabled',true);
    }
    else
    {
        $('#spanApplyRulesButton input').removeAttr('disabled');
        $('#divNext').hide(1000);
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

            if (HasClass(current,'False'))
            {
                UpdateStatus(current,'ignored');
                ApplyChanges(current + 1);
            }
            else
                ChangeProposition_Proxy.ChangeSchemeCode(successCallback,failureCallback);
            break;
        case 1.5: //1 failed
            UpdateStatus(current,'failed');
            break;
        case 1.9: //1 success
            UpdateStatus(current,'success');
            ApplyChanges(current + 1);
            break;
        case 2:
            current = step;
            UpdateStatus(current,'starting');
            if (HasClass(current,'False'))
            {
                UpdateStatus(current,'ignored');
                ApplyChanges(current + 1);
            }
            else
                ChangeProposition_Proxy.ChangeProductCode(successCallback,failureCallback);
            break;
        case 2.5: //1 failed
            UpdateStatus(current,'failed');
            break;
        case 2.9: //1 success
            UpdateStatus(current,'success');
            ApplyChanges(current + 1);
            break;
        case 3:
            current = step;
            UpdateStatus(current,'starting');
            
            if (HasClass(current,'False'))
            {
                UpdateStatus(current,'ignored');
                ApplyChanges(current + 1);
            }
            else
                ChangeProposition_Proxy.ChangeMasterCompanyNumber(successCallback,failureCallback);
            break;
        case 3.5: //1 failed
            UpdateStatus(current,'failed');
            break;
        case 3.9: //1 success
            UpdateStatus(current,'success');
            ApplyChanges(current + 1);
            break;
        case 4:
            current = step;
            UpdateStatus(current,'starting');
            if (HasClass(current,'False'))
            {
                UpdateStatus(current,'ignored');
                ApplyChanges(current + 1);
            }
            else
                ChangeProposition_Proxy.ChangeEffectiveDate(successCallback,failureCallback);
            break;
        case 4.5: //1 failed
            UpdateStatus(current,'failed');
            break;
        case 4.9: //1 success
            UpdateStatus(current,'success');
            ApplyChanges(current + 1);
            break;
        case 5:
            current = step;
            UpdateStatus(current,'starting');
            if (HasClass(current,'False'))
            {
                UpdateStatus(current,'ignored');
                ShowNext();
            }
            else
                ChangeProposition_Proxy.ChangeExpirationDate(successCallback,failureCallback);
            break;
        case 5.5: //1 failed
            UpdateStatus(current,'failed');
            break;
        case 5.9: //1 success
            UpdateStatus(current,'success');
            ShowNext();
            break;
    }
}

function ShowNext()
{
        $('#divNext').show(1000);
}

function UpdateStatus(item,status)
{
    var div,stat,mes;
    div = '#divStep'+item;
    mes  = '#divStepMessage'+item;
    $(div).removeClass('starting loading failed success ignored');
    $(div).addClass(status); 
    
    $(mes).html(objAjaxResponse);
 
}

function HasClass(item,status)
{
    var div;
    div = '#divStep'+item;
    return $(div).hasClass(status);
}

var successCallback = function(data){
    objAjaxResponse = data.d;
    
    if (objAjaxResponse.length == 0)
        ApplyChanges(current + 0.9);
    else
        ApplyChanges(current + 0.5);
};


var failureCallback = function(data){
    var text = data.responseText;
    
    if (text.indexOf("Description:",0) > -1)
    {
        text = text.substr(text.indexOf("Description:",0),text.length - text.indexOf("Description:",0));
    }
    
    alert('Ajax Request failed.\n' + 'Status:' + data.status + '\nStatus Text:' + data.statusText + '\nResponse Text:' + text);
};
