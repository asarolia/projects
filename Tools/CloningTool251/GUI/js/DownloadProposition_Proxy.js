// JScript File
function DownloadProposition_Proxy() { }

DownloadProposition_Proxy.GetPropositionDetails = function (scheme, successCallback, failureCallback) {

    var jsonData = JSON.stringify({ scheme: scheme });

    $.ajax({
      type: "POST",
      contentType: "application/json; charset=utf-8",
      url: "DownloadProposition.aspx/GetPropositionDetails",
      data: jsonData,
      success: function (data) { successCallback(data); },
      error: function (data) { failureCallback(data); }
    });
};

DownloadProposition_Proxy.VerifyLogin = function (userid, password, region, successCallback, failureCallback) {

    var jsonData = JSON.stringify({ userid: userid, password: password, region: region });

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "DownloadProposition.aspx/VerifyLogin",
        data: jsonData,
        success: function (data) { successCallback(data); },
        error: function (data) { failureCallback(data); }
    });
};

DownloadProposition_Proxy.StartDownload = function (ProductInd, SchemeInd, MasterCompanyNumberInd,successCallback, failureCallback) {

    var jsonData = JSON.stringify({ ProductInd: ProductInd, SchemeInd: SchemeInd, MasterCompanyNumberInd: MasterCompanyNumberInd });

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "DownloadProposition.aspx/StartDownload",
        data: jsonData,
        success: function (data) { successCallback(data); },
        error: function (data) { failureCallback(data); }
    });
};

DownloadProposition_Proxy.DownloadProgress = function (Identifier, successCallback, failureCallback) {
   var jsonData = JSON.stringify({ identifier: Identifier});

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "DownloadProposition.aspx/DownloadProgress",
        data: jsonData,
        success: function (data) { successCallback(data); },
        error: function (data) { failureCallback(data); }
    });
};

DownloadProposition_Proxy.Generate = function (successCallback, failureCallback) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "DownloadProposition.aspx/Generate",
        data: '',
        success: function (data) { successCallback(data); },
        error: function (data) { failureCallback(data); }
    });


};


