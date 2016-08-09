// JScript File
function GenerateExcel_Proxy() {}

GenerateExcel_Proxy.Generate = function(successCallback, failureCallback){

    $.ajax({
      type: "POST",
      contentType: "application/json; charset=utf-8",
      url: "GenerateExcel.aspx/Generate",
      data: '',
      success: function (data) { successCallback(data); },
      error: function (data) { failureCallback(data); }
    });


};
