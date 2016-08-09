
$(document).ready(function () {

    //dozzy calculation of hights for source & side navigation bar
    SetHeightToDocument('#sourcecontent', $(window).height() - $('#header').height() - 10);
    SetHeightToDocument('#sidebar', $(window).height() - $('#header').height() - 10);

    //event handling for hiding / displaying hilight section
    $('#hilight-head').click(function () {
        $('#hilight').toggle('slow');
    });

    //handle hilight / de-hilight logic
    $('#hilight input').click(function () {
        var tag, flag;

        flag = this.checked;
        tag = $('label[for=' + this.id + ']').text();

        $.each(tag.split(','), function (index, value) { ToggleHighlight(value, flag); });
    });
});


//function toggle hilight based on flag
function ToggleHighlight(tag, flag) {
    tag = ".code-tag-" + tag;

    if (flag)
        $(tag).addClass("hilight");
    else
        $(tag).removeClass("hilight");
}

//set screen higt of elements to max the view window
function SetHeightToDocument(id,height) {
    $(id).height(height);
    $(id).css("overflow","auto");
}
