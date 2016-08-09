// JScript File
function Browse_Proxy() { }

Browse_Proxy.Download = function (userid, password, fileName, successCallback, failureCallback) {
    var jsonData = JSON.stringify({ User: userid ,Password: password ,FileName: fileName });

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Browse.aspx/Download",
        data: jsonData,
        success: function (data) { successCallback(data); },
        error: function (data) { failureCallback(data); }
    });

}

Browse_Proxy.GetMyList = function (userid, successCallback, failureCallback) {
    var jsonData = JSON.stringify({ User: userid });

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Browse.aspx/GetMyList",
        data: jsonData,
        success: function (data) { successCallback(data); },
        error: function (data) { failureCallback(data); }
    });

}

