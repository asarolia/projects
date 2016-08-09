
// JScript File

var objAjaxResponse;

$(document).ready(function () {

    //handling download button
    var DownloadDataButton = $('#spanButtonGetModule input');
    DownloadDataButton.click(function () {
        if (!Page_IsValid)
            return false;

        DisableAllControls(true);

        Download();
        return false;
    });

    //expand and collapse section
    $('#startDownloadLegend').click(function () {
        if ($('#startDownload').hasClass('collapsed')) {
            $('#startDownloadContent').toggle(500);
            $('#startDownload').removeClass('collapsed');
            $('#startDownload').addClass('collapsible');
        } else {
            $('#startDownloadContent').toggle(500);
            $('#startDownload').removeClass('collapsible');
            $('#startDownload').addClass('collapsed');
        }

    });

    //get user list

    //handling download button
    var GetListButton = $('#spangetUserListButton input');
    GetListButton.click(function () {

        GetMyList();
        return false;
    });

    setTimeout("GetGlobalList();", 1000);

    $.template("DownloadItem", $('#moduleListTemplate').html());
});



function DisableAllControls(val) {
    $('#spanButtonGetModule input').attr("disabled", val);
}

function GetMyList() {
    var user = $('#spanUseridTextBox input').val();
    $('#mydownloadsContent_wait').show();
    Browse_Proxy.GetMyList(user, success_GetMyList, failure);

}

function GetGlobalList() {
    $('#downloadContent_wait').show();
    Browse_Proxy.GetMyList("", success_GetGlobalList , failure);
}

function Download() {
    var user = $('#spanUseridTextBox input').val();
    var password = $('#spanPasswordTextBox input').val();
    var File = $('#spanTextBoxModuleName input').val();
    
    Browse_Proxy.Download(user, password, File, success, failure);

}

var openModule = function () {
    var tmplItem = $.tmplItem(this)
    var url = tmplItem.data.editLink;

    window.open(url);
}

function fillScreen(areaname,templatename,data,callback)
{
    $(areaname).empty();
    $.each(data, function (index, value) {
        $.tmpl(templatename, value).css("cursor", "pointer").click(callback).prependTo(areaname);
    });
}

var success_GetMyList = function (data) {
    objAjaxResponse = data.d;

    $('#mydownloadsContent_wait').hide();
    $('#mydownloadContent').empty();

    fillScreen("#mydownloadContent", "DownloadItem", objAjaxResponse, openModule);
}

var success_GetGlobalList = function (data) {
    objAjaxResponse = data.d;
    $('#downloadContent_wait').hide();

    fillScreen("#downloadContent", "DownloadItem", objAjaxResponse, openModule);
    setTimeout("GetGlobalList();", 5000);
}

var success = function (data) {
    objAjaxResponse = data.d;

    if (objAjaxResponse.Success) {
        alert('Success:' + objAjaxResponse.Location);
        GetMyList();
    }
    else {
        alert('ErrorText:' + objAjaxResponse.ErrorText);
    }
    DisableAllControls(false);
}

var failure = function (data) {
    var text = data.responseText;

    if (text.indexOf("Description:", 0) > -1) {
        text = text.substr(text.indexOf("Description:", 0), text.length - text.indexOf("Description:", 0));
    }

    alert('Ajax Request failed.\n' + 'Status:' + data.status + '\nStatus Text:' + data.statusText + '\nResponse Text:' + text);
    DisableAllControls(false);
};

