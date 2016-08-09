// JScript File
function DataMigration_Proxy() {}

DataMigration_Proxy.GetList = function (successCallback, failureCallback) {

    //var jsonData = JSON.stringify({ scheme: scheme });

    $.ajax({
      type: "POST",
      contentType: "application/json; charset=utf-8",
      url: "DataMigration.aspx/GetList",
      data: "",
      success: function (data) { successCallback(data); },
      error: function (data) { failureCallback(data); }
    });
 };
