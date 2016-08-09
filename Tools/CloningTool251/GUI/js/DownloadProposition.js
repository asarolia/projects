// JScript File

var objAjaxResponse;
var objProposition;
var ProgressBarStarted;
var downloadid;

$(document).ready(function () {

    // screen build
    $('#aDownloadProposition').addClass('selected');
    $('#navigation').hide();
    $('#btnDownload input').attr('disabled', true);

    //proposition selection
    var PropositionListDropDown = $('#spanPropositionListDropDown select');
    PropositionListDropDown.change(function () {
        var scheme = $(this).val();
        $('#spanPropositionListDropDown input').val(scheme);
        GetPropositionDetails(scheme);
    });



    //download logic
    $('#btnDownload input').click(function () {
        if (!Page_IsValid)
            return false;

        StartDownload();
        return false;
    });

    //generate excel sheet


});

// proposition details fetch
function GetPropositionDetails(scheme)
{
    if (scheme.length == 0)
    {
        $('#AjaxPropositionDetails').hide(1000);
        return;
    }
    
    $('#AjaxPropositionDetails').hide();
    $('#wAjaxPropositionDetails').show();
    DownloadProposition_Proxy.GetPropositionDetails(scheme, successCallbackPropositionDetails, failureCallback);

}

//proposition details call back
var successCallbackPropositionDetails = function (data) {
    objAjaxResponse = eval(data.d);
    objProposition = objAjaxResponse;

    $('#AjaxPropositionDetails').html(' ');
    $('#wAjaxPropositionDetails').hide();
    var template = $('#AjayPropositionDetailsTemplate').html();

    template = template.replace('{Name}', objProposition.Name);
    template = template.replace('{ProductCode}', objProposition.ProductCode);
    template = template.replace('{MasterCompanyNumber}', objProposition.MasterCompanyNumber);
    template = template.replace('{LOBCode}', objProposition.LOBCode);
    template = template.replace('{SchemeCode}', objProposition.SchemeCode);
    $('#SpanLiveSchemeCode input').val(objProposition.SchemeCode);
    $('#AjaxPropositionDetails').html(template);
    $('#AjaxPropositionDetails').show(1000);
    $('#btnDownload input').attr('disabled', false);
};

var failureCallback = function(data){
    var text = data.responseText;

    if (text.indexOf("Description:",0) > -1)
    {
        text = text.substr(text.indexOf("Description:",0),text.length - text.indexOf("Description:",0));
    }
    
    alert('Ajax Request failed.\n' + 'Status:' + data.status + '\nStatus Text:' + data.statusText + '\nResponse Text:' + text);
};


//download proposition
function StartDownload() {
    $('#divStartProcess').show(250);
    $('#divNext').hide(100);
    initializeStatusArea();
    StartProcess();
}

function StartProcess(item) {
    if (typeof (item) == 'undefined') {
        DisableAllControls(true);
        StartProcess(1);
        return;
    }

    switch (item) {
        case 1:
            var userid = $('#spanUserTextBox input').val();
            var password = $('#spanPasswordTextBox input').val();
            var region = $('#spanRegionTextBox select').val();
            UpdateStatus(item, 'starting');
            DownloadProposition_Proxy.VerifyLogin(userid, password, region, successLogin, failureCallback);
            break;
        case 2:
            iterationCounter = 0;
            var si = $('#SpanSchemeIndicator input').is(':checked');
            var pi = $('#SpanProductIndicator input').is(':checked');
            var mi = $('#SpanMasterCompanyIndicator input').is(':checked');
            DownloadProposition_Proxy.StartDownload(pi,si,mi,successDownloadCallback, failureCallback);
            break;
        case 3:
            DownloadProposition_Proxy.DownloadProgress(downloadid, successProgress, failureCallback);
            break;
    }
}
function DisableAllControls(val) {
    if (val) {
        $('#spanUserTextBox input').attr("disabled", true);
        $('#spanPasswordTextBox input').attr("disabled", true);
        $('#btnDownload').attr("disabled", true);
        $('#divVerifyUserMessage').html(' ');
        $('#divDownloadingMessage').html(' ');
    }
    else {
        $('#spanUserTextBox input').removeAttr("disabled");
        $('#spanPasswordTextBox input').removeAttr("disabled");
        $('#btnDownload').removeAttr("disabled");
    }
}
function UpdateStatus(item, status) {
    var div, stat;

    //select item
    switch (item) {
        case 1: div = '#divVerifyUser';
            break;
        case 2: div = '#divDownloading';
            break;
    }

    $(div).removeClass('starting loading failed success');
    if (typeof (status) != 'undefined' && status.length > 0)
        $(div).addClass(status);
}
var successLogin = function (data) {
    objAjaxResponse = data.d;

    if (objAjaxResponse.length == 0) {
        UpdateStatus(1, 'success');
        UpdateStatus(2, 'starting');
        StartProcess(2);
    }
    else {
        UpdateStatus(1, 'failed');
        $('#divVerifyUserMessage').html(objAjaxResponse);
        DisableAllControls(false);
    }
}
var successDownloadCallback = function (data) {
    objAjaxResponse = data.d;

    if (objAjaxResponse.length && objAjaxResponse.length != 0) {
        if (objAjaxResponse[0] < 0){
            UpdateStatus(2, 'failed');
            $('#divDownloadingMessage').html(objAjaxResponse[1]);
            DisableAllControls(false);
        }
        else{
            downloadid= objAjaxResponse[0];
            initializeStatusArea();
            setTimeout("StartProcess(3);", 100);
        }
    }
    else {
        UpdateStatus(2, 'failed');
        $('#divDownloadingMessage').html(objAjaxResponse);
        DisableAllControls(false);
    }
}


function initializeStatusArea() {
    $('#divVerifyUserMessage').html('');
    $('#divDownloadingMessage').html('');
    $('#divDownloadInfo').show(250);
    $('#ProgressBar').hide();
    $('#CurrentTable').hide();
    $('#ProgressText').hide();
    $('#CurrentSQL').hide();
    iterationCounter = 0;
}
var successProgress = function (data) {
    objAjaxResponse = eval(data.d);

    //initialize
    $('#ProgressText').html('');

    //if service complete
    
    if (objAjaxResponse.Stage == 2 && objAjaxResponse.PercentageComplete == 100){
        $('#divDownloadingMessage').html('');
        UpdateStatus(2, 'success');
        $('#divDownloadInfo').hide(250);
        ProgressBarStarted = false;
        $('#divNext').show(100);
        setTimeout("ApplyChanges();", 3000);
    }
    //if service error
    else if (objAjaxResponse.ErrorFlag == true) {
        UpdateStatus(2, 'failed');
        $('#divDownloadingMessage').html(objAjaxResponse.ErrorMessage);
        if (ProgressBarStarted) {
            $('#ProgressBar').progressbar({ value: objAjaxResponse.PercentageComplete });
            //$('#CurrentTable').html(objAjaxResponse.Table);
            $('#ProgressText').html(objAjaxResponse.Description);
            $('#CurrentSQL').html(objAjaxResponse.SecondaryDescription);
        }
        DisableAllControls(false);
        ProgressBarStarted = false;
    } else {

        if (objAjaxResponse.PercentageComplete > 0) {
            if (!ProgressBarStarted) {
                $('#ProgressBar').show();
                $('#CurrentTable').show();
                $('#CurrentSQL').show();
                $('#ProgressText').show();
            }
            $('#ProgressBar').progressbar({ value: objAjaxResponse.PercentageComplete });
            //$('#CurrentTable').html(objAjaxResponse.Table);
            $('#ProgressText').html(objAjaxResponse.Description);
            $('#CurrentSQL').html(objAjaxResponse.SecondaryDescription);
            ProgressBarStarted = true;
        }
        else {
            $('#ProgressText').show();
            //$('#ProgressText').html('Awaiting download status ...' + iterationCounter++);
            $('#ProgressText').html('Awaiting download status ...');
        }

        setTimeout("StartProcess(3);", 100);
    }
}

var successProgressx = function (data) {
    objAjaxResponse = eval(data.d);

    //initialize
    $('#ProgressText').html('');

    //if service complete
    if (objAjaxResponse.isComplete) {
        $('#divDownloadingMessage').html(objAjaxResponse.Status);
        UpdateStatus(2, 'success');
        $('#divDownloadInfo').hide(250);
        ProgressBarStarted = false;
        $('#divNext').show(100);
        setTimeout("ApplyChanges();", 3000);
    }
    //if service error
    else if (objAjaxResponse.ErrorMessage && objAjaxResponse.ErrorMessage.length > 0) {
        UpdateStatus(2, 'failed');
        $('#divDownloadingMessage').html(objAjaxResponse.ErrorMessage);
        if (ProgressBarStarted) {
            $('#ProgressBar').progressbar({ value: objAjaxResponse.Percentage });
            $('#CurrentTable').html(objAjaxResponse.Table);
            $('#ProgressText').html('Received data for ' + objAjaxResponse.Current + ' of ' + objAjaxResponse.Maximum + ' tables.');
            $('#CurrentSQL').html(objAjaxResponse.SQL);
        }
        DisableAllControls(false);
        ProgressBarStarted = false;
    } else {

        if (objAjaxResponse.Percentage > 0) {
            if (!ProgressBarStarted) {
                $('#ProgressBar').show();
                $('#CurrentTable').show();
                $('#CurrentSQL').show();
                $('#ProgressText').show();
            }
            $('#ProgressBar').progressbar({ value: objAjaxResponse.Percentage });
            $('#CurrentTable').html(objAjaxResponse.Table);
            $('#ProgressText').html('Received data for ' + objAjaxResponse.Current + ' of ' + objAjaxResponse.Maximum + ' tables.');
            $('#CurrentSQL').html(objAjaxResponse.SQL);
            ProgressBarStarted = true;
        }
        else {
            $('#ProgressText').show();
            //$('#ProgressText').html('Awaiting download status ...' + iterationCounter++);
            $('#ProgressText').html('Awaiting download status ...');
        }

        setTimeout("StartProcess(3);", 100);
    }
}

function DisableAllControls(sw) {
    if (sw) {
        $('#divNext input').attr('disabled', true);
    }
    else {
        $('#divNext input').removeAttr('disabled');
    }
}

function ApplyChanges(step) {
    if (typeof (step) == 'undefined') {
        ApplyChanges(1);
        return;
    }

    switch (step) {
        case 1:
            current = step;
            DisableAllControls(true);
            UpdateStatusExcel(current, 'starting');
            DownloadProposition_Proxy.Generate(successCallbackGenerate, failureCallback);
            break;
        case 1.5: //1 failed
            UpdateStatusExcel(current, 'failed');
            $('#divStepMessage1').html(objAjaxResponse);
            break;
        case 1.9: //1 success
            UpdateStatusExcel(current, 'success');
            $('#divStepMessage1').html(' ');
            DisableAllControls(false);
            break;
    }
}

function UpdateStatusExcel(item, status) {
    var div, stat;
    div = '#divStep' + item;
    $(div).removeClass('starting loading failed success');
    $(div).addClass(status);
}


var successCallbackGenerate = function (data) {
    objAjaxResponse = data.d;
    objProposition = objAjaxResponse;

    if (objAjaxResponse.length == 0)
        ApplyChanges(current + 0.9);
    else {
        ApplyChanges(current + 0.5);
    }
};


