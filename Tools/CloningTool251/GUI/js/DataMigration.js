// JScript File
var ReponseList, flagToggleInactive = false,lastIndex = -1;

$(document).ready(function () {
    $.template("List", $("#divList").html());

    $("#ButtonRefresh").click(function () {
        RefreshList();
        return false;
    });

    setTimeout("RefreshList();", 1000);
});

function ToggleDisplay(index) {
    if (index < 0) {
        $(".SpanDisplay1").hide();
        $(".SpanDisplay2").hide();
    } else {
        $(".SpanDisplay1:eq(" + index + ")").toggle();
        $(".SpanDisplay2:eq(" + index + ")").toggle();
    }
}

function mouseIn() {
    if (flagToggleInactive) { return; }

    //hide all first
    setTimeout("ToggleDisplay(-1)", 0);

    var index = $(".ListRow").index(this);
    if (index > -1)
        setTimeout("ToggleDisplay(" + index + ")", 0);
}

function mouseInDropdown() { flagToggleInactive = true; }

function mouseOut() {

    if (flagToggleInactive) { return; }

    var index = $(".ListRow").index(this);
    if (index > -1)
        setTimeout("ToggleDisplay(" + index + ")", 0);
}
function mouseOutDropdown() { flagToggleInactive = false; }





function RefreshList(){
    DataMigration_Proxy.GetList(successGetListCallback,failureCallback);
}

function oddOrEven() {
    return ($.inArray(this.data, ReponseList) % 2) ? "odd" : "even";
}

function Upload(obj, input) {
    $("#HiddenSelectedFile input").val(input);

    var index = $(".UploadButton").index(obj);
    if (index > -1) {
        var value = $("#SpanSecondRegionList select:eq(" + index + ")").val();
        $("#HiddenTargetRegion input").val(value);
    }
    else {
        return false;
    }

    return true;
}




var successGetListCallback = function (data) {
    ReponseList = eval(data.d);

    $("#divListDisplay").empty();
    $.each(ReponseList, function (index, value) {
        $.tmpl("List", value).prependTo("#divListDisplay");
    }
    );
    //add event to tr
    $(".ListRow").mouseenter(mouseIn).mouseleave(mouseOut);

    //add dropdown select mouse events
    $("#SpanSecondRegionList").mouseenter(mouseInDropdown).mouseleave(mouseOutDropdown);
}



var failureCallback = function(data){
    var text = data.responseText;
    DisableAllControls(false);
    ResetProcess();
    if (text.indexOf("Description:",0) > -1)
    {
        text = text.substr(text.indexOf("Description:",0),text.length - text.indexOf("Description:",0));
    }
    
    alert('Ajax Request failed.\n' + 'Status:' + data.status + '\nStatus Text:' + data.statusText + '\nResponse Text:' + text);
};
