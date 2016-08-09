function Slide(id)
{
    var divId = '#' + id;
    if ($(divId).html()== '')
    {
        $(divId).hide();
    }
    else
    {
        $(divId).show();
    }
        
}