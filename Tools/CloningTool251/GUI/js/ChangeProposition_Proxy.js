// JScript File
function ChangeProposition_Proxy() {}

ChangeProposition_Proxy.ChangeSchemeCode = function(successCallback, failureCallback){

    $.ajax({
      type: "POST",
      contentType: "application/json; charset=utf-8",
      url: "ChangeProposition.aspx/ChangeSchemeCode",
      data: '',
      success: function (data) { successCallback(data); },
      error: function (data) { failureCallback(data); }
    });


};

ChangeProposition_Proxy.ChangeProductCode = function(successCallback, failureCallback){

    $.ajax({
      type: "POST",
      contentType: "application/json; charset=utf-8",
      url: "ChangeProposition.aspx/ChangeProductCode",
      data: '',
      success: function (data) { successCallback(data); },
      error: function (data) { failureCallback(data); }
    });


};


ChangeProposition_Proxy.ChangeMasterCompanyNumber = function(successCallback, failureCallback){

    $.ajax({
      type: "POST",
      contentType: "application/json; charset=utf-8",
      url: "ChangeProposition.aspx/ChangeMasterCompanyNumber",
      data: '',
      success: function (data) { successCallback(data); },
      error: function (data) { failureCallback(data); }
    });


};


ChangeProposition_Proxy.ChangeEffectiveDate = function(successCallback, failureCallback){

    $.ajax({
      type: "POST",
      contentType: "application/json; charset=utf-8",
      url: "ChangeProposition.aspx/ChangeEffectiveDate",
      data: '',
      success: function (data) { successCallback(data); },
      error: function (data) { failureCallback(data); }
    });


};


ChangeProposition_Proxy.ChangeExpirationDate = function(successCallback, failureCallback){

    $.ajax({
      type: "POST",
      contentType: "application/json; charset=utf-8",
      url: "ChangeProposition.aspx/ChangeExpirationDate",
      data: '',
      success: function (data) { successCallback(data); },
      error: function (data) { failureCallback(data); }
    });


};
