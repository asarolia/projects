// JScript File
function CloneProposition_Proxy() {}

CloneProposition_Proxy.GetPropositionDetails = function(scheme,successCallback, failureCallback){

    var jsonData = JSON.stringify({ scheme: scheme });

    $.ajax({
      type: "POST",
      contentType: "application/json; charset=utf-8",
      url: "CloneProposition.aspx/GetPropositionDetails",
      data: jsonData,
      success: function (data) { successCallback(data); },
      error: function (data) { failureCallback(data); }
    });


};


