// JScript File

var objAjaxResponse;
var objProposition;
var speed = 1000;
var iterationCounter = 0;
var maxiterationCounter = 30;
var ProgressBarStarted = false;
var DownloadIdentifier = -1;

$(document).ready(function (){
    
    var DownloadDataButton = $('#btnDownload input');
		
    DownloadDataButton.click(function () {
        if (!Page_IsValid)
            return false;
            
        StartProcess();
        return false;
    });
    
});

function DisableAllControls(val)
{
    if (val)
    {
        $('#spanUserTextBox input').attr("disabled", true);
        $('#spanPasswordTextBox input').attr("disabled", true);
        $('#btnDownload').attr("disabled", true);
        $('#divVerifyUserMessage').html(' ');
        $('#divDownloadingMessage').html(' ');
    }
    else
    {
        $('#spanUserTextBox input').removeAttr("disabled");
        $('#spanPasswordTextBox input').removeAttr("disabled");
        $('#btnDownload').removeAttr("disabled");
    }
}

function StartProcess(item)
{

    
    if (typeof(item) == 'undefined')
    {
       UpdateProgress('start');
       //DisableAllControls(true);
       StartProcess(1);
       return;
    }
    
    switch(item)
    {
        case 1:
            var userid = $('#spanUserTextBox input').val();
            var password = $('#spanPasswordTextBox input').val();
            var region = $('#spanRegionTextBox select').val();
            //UpdateStatus(item,'starting');
            UpdateProgress('login','starting');
            DownloadData_Proxy.VerifyLogin(userid,password,region,successLogin,failureCallback);
            break;
        case 2:
            iterationCounter = 0;
            //UpdateStatus(2,'starting');
            UpdateProgress('download','starting');
            DownloadData_Proxy.StartDownload(successDownloadCallback,failureCallback);
            break;
        case 3:
            UpdateProgress('progress','starting');
            DownloadData_Proxy.DownloadProgress(DownloadIdentifier,successProgress,failureCallback);
            break;
    }
    
        
        
}
function ResetProcess()
{
    UpdateStatus(1);
    UpdateStatus(2);
}

function UpdateStatus(item,status)
{
    var div,stat;
    
    //select item
    switch(item)
        {
        case 1: div = '#divVerifyUser';
            break;
        case 2: div = '#divDownloading';
            break;
        }
        
    $(div).removeClass('starting loading failed success');
    if (typeof(status) != 'undefined' && status.length > 0)
        $(div).addClass(status);  
}

var successLogin = function(data){
    objAjaxResponse = data.d;
    
    if (objAjaxResponse.length == 0)
    {
        //UpdateStatus(1,'success');
        
        UpdateProgress('login','success');
        StartProcess(2);
    }
    else
    {
        //UpdateStatus(1,'failed');
        UpdateProgress('login','failed',objAjaxResponse);
        //$('#divVerifyUserMessage').html(objAjaxResponse);
        //DisableAllControls(false);
        UpdateProgress('reset');
    }
}
var successDownloadCallback = function (data) {
    objAjaxResponse = data.d;
    var error = objAjaxResponse;

    if (objAjaxResponse.length && objAjaxResponse.length != 0) {
        
        if (objAjaxResponse[0] < 0){  //error
            error = objAjaxResponse[1];
        }else {
            DownloadIdentifier = objAjaxResponse[0];
            //UpdateProgress('download','starting');
            setTimeout("StartProcess(3);", 100);
        }

        return;
    }

    //UpdateStatus(2, 'failed');
    //$('#divDownloadingMessage').html(error);

    UpdateProgress('download','failed',error);
    
    //DisableAllControls(false);
    UpdateProgress('reset');
}

function UpdateProgress(state, flag, status, text1 , text2 , number1)
{
    switch(state)
    {
        case 'start': //starting a download
            DisableAllControls(true);
            UpdateStatus(1);
            UpdateStatus(2);

            $('#divStartProcess').show(3);
            $('#divDownloadInfo').show();
            $('#divNext').hide();

            $('#divDownloadingMessage').html('');
            $('#divVerifyUserMessage').html('');

            break;

        case 'login':  //login stage
            UpdateStatus(1,flag);

            if (typeof(status) != 'undefined')
                $('#divVerifyUserMessage').html(status);

            break;

        case 'download':  //download stage
            UpdateStatus(2,flag);

            $('#ProgressBar').hide();
            $('#CurrentTable').hide();
            $('#ProgressText').hide();
            $('#CurrentSQL').hide();

            if (typeof(status) != 'undefined')
                $('#divDownloadingMessage').html(status);

            break;

        case 'progress':  //while checking for progress
            UpdateStatus(2,flag);
            $('#divDownloadInfo').show();

            $('#ProgressBar').show();
            $('#CurrentTable').show();
            $('#ProgressText').show();
            $('#CurrentSQL').show();

            if (typeof(status) != 'undefined')
                $('#divDownloadingMessage').html(status);

            if (typeof(text1) != 'undefined')
                $('#ProgressText').html(text1);

            if (typeof(text2) != 'undefined')
                $('#CurrentSQL').html(text2);

            if (typeof(number1) != 'undefined')
                $('#ProgressBar').progressbar({ value: number1});

            break;

        case 'end':     // end download after success

            UpdateStatus(2, 'success');
            $('#divDownloadInfo').hide();
            $('#divNext').show();

            break;
            
        case 'reset':   //reset after fail
            DisableAllControls(false);
            $('#divNext').hide();
            break;
    }
}



var successProgress = function (data) {
    objAjaxResponse = eval(data.d);

    //initialize
    /*
    if (objAjaxResponse.Stage == 0) 
    {
    }
    if (objAjaxResponse.Stage == 1) 
    {
        $('#ProgressBar').show();
        $('#CurrentTable').show();
        $('#CurrentSQL').show();
        $('#ProgressText').show();
    }
    */
    if (objAjaxResponse.Stage == 1) 
    {
        UpdateProgress('progress','starting','',objAjaxResponse.Description, objAjaxResponse.SecondaryDescription, objAjaxResponse.PercentageComplete);
    }
    

    if (objAjaxResponse.Stage == 2) // post processing
    {
        if (objAjaxResponse.PercentageComplete = 100)
        {
            UpdateProgress('end');
        }
        return;
    }


    if (objAjaxResponse.ErrorFlag == true) // error
    {
        UpdateProgress('progress','failed', objAjaxResponse.ErrorMessage, objAjaxResponse.Description, objAjaxResponse.SecondaryDescription, objAjaxResponse.PercentageComplete);
        UpdateProgress('reset');
        return;
    }

    setTimeout("StartProcess(3);", 100);
}


var failureCallback = function(data){
    var text = data.responseText;
    //DisableAllControls(false);
    //ResetProcess();
    UpdateProgress('reset');
    if (text.indexOf("Description:",0) > -1)
    {
        text = text.substr(text.indexOf("Description:",0),text.length - text.indexOf("Description:",0));
    }
    
    alert('Ajax Request failed.\n' + 'Status:' + data.status + '\nStatus Text:' + data.statusText + '\nResponse Text:' + text);
};
