/// <reference path="_references.js" />
//alert(" main alert from custom JS");
var milliseconds = 1000;
var opacity = 0.5;

$(document).submit(function () {

    if (Modernizr.sessionstorage)
    {
        sessionStorage.setItem('sdate', $('#seldate').val());
        alert("selected date " + $('#seldate').val());
    }
    else {
        alert("Please upgrade your browser to run this application");
    }

});

$(document).ready(function () {

    if (Modernizr.sessionstorage) {
        var jdate = sessionStorage.getItem('sdate');
        if (jdate == null)
        {
         //  alert(" first load " + jdate);
         
            $('#seldate').html(Date.now().toLocaleDateString());
        }
      
    }
    else {
        alert("Please upgrade your browser to run this application");
    }

// hide not needed elements

    $('#hide1').html("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
    $('#hide2').html("&nbsp;&nbsp;&nbsp;");
    $('#hide3').html("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
    $('#hide4').html("&nbsp;&nbsp;&nbsp;");
    $('#hide5').html("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
    $('#hide6').html("&nbsp;&nbsp;&nbsp;");
    $('#hide7').html("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
    $('#hide8').html("&nbsp;&nbsp;&nbsp;");
    $('#hide9').html("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
    $('#hide10').html("&nbsp;&nbsp;&nbsp;");
    $('#hide11').html("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
    $('#hide12').html("&nbsp;&nbsp;&nbsp;");




  //   $('#btnShowMessage').click(displayTimeAsync);
    $('#btnShowMessage').click(showMessageAsync);
    $('#messageOk').click(hideMessageAsync);
   // alert("alert from custom JS");

});


function displayCoverAsync() {
    return $('#cover').fadeTo(milliseconds, opacity).promise();
}

//function showMessageContentAsync(message) {
//    $('#message').html(message);
//    $('#messageBox').show();
//    return $('#messageContent').slideDown(milliseconds).promise();
//}

function showMessageContentAsync() {
    $("#message").show();
    $('#messageBox').show();
    return $('#messageContent').slideDown(milliseconds).promise();
}

//function showMessageAsync(message) {
//    var coverPromise = displayCoverAsync();
//    var messagePromise = coverPromise.pipe(function () {
//        return showMessageContentAsync(message);
//    });
//    return messagePromise;
//}

function showMessageAsync() {
    var coverPromise = displayCoverAsync();
    var messagePromise = coverPromise.pipe(function () {
        // return showMessageContentAsync(message);
        return showMessageContentAsync();
    });
    return messagePromise;
}


function displayTimeAsync() {
     var message = 'The time is now ' + getTime();
      return showMessageAsync(message);

}

function getTime() {
    var dateTime = new Date();
    var hours = dateTime.getHours();
    var minutes = dateTime.getMinutes();
    return hours + ':' + (minutes < 10 ? '0' + minutes : minutes);
}

function hideMessageContentAsync(message) {
    var promise = $('#messageContent').slideUp(milliseconds).promise();
  //  var promise = $('#messageContent').addClass('hinge').promise();
    promise.done(function () { $('#messageBox').hide(); });
    return promise;
}

function hideCoverAsync() {
    return $('#cover').fadeOut(milliseconds).promise();
}
function hideMessageAsync() {
    var messagePromise = hideMessageContentAsync();
    var coverPromise = messagePromise.pipe(function () {
        return hideCoverAsync();
    });
    return coverPromise;
}
