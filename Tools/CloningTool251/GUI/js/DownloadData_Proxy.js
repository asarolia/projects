// JScript File
function DownloadData_Proxy() {}

DownloadData_Proxy.GetPropositionDetails = function(scheme,successCallback, failureCallback){

    var jsonData = JSON.stringify({ scheme: scheme });

    $.ajax({
      type: "POST",
      contentType: "application/json; charset=utf-8",
      url: "DownloadData.aspx/GetPropositionDetails",
      data: jsonData,
      success: function (data) { successCallback(data); },
      error: function (data) { failureCallback(data); }
    });
 };

DownloadData_Proxy.VerifyLogin = function(userid,password,region,successCallback, failureCallback){

    var jsonData = JSON.stringify({ userid: userid ,password: password, region: region});

    $.ajax({
      type: "POST",
      contentType: "application/json; charset=utf-8",
      url: "DownloadData.aspx/VerifyLogin",
      data: jsonData,
      success: function (data) { successCallback(data); },
      error: function (data) { failureCallback(data); }
    });
};

DownloadData_Proxy.StartDownload = function(successCallback, failureCallback){

    $.ajax({
      type: "POST",
      contentType: "application/json; charset=utf-8",
      url: "DownloadData.aspx/StartDownload",
      data: "",
      success: function (data) { successCallback(data); },
      error: function (data) { failureCallback(data); }
    });
};

DownloadData_Proxy.DownloadProgress = function (Identifier, successCallback, failureCallback) {

    var jsonData = JSON.stringify({ identifier: Identifier});

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "DownloadData.aspx/DownloadProgress",
        data: jsonData,
        success: function (data) { successCallback(data); },
        error: function (data) { failureCallback(data); }
    });
};