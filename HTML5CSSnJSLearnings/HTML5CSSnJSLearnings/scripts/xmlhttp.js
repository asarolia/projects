/// <reference path="_refrences.js" />

$(document).ready(function () 
{

    $("#btn1").on('click', handleButton1);


    function handleButton1() {  

         pwd = $("#txt1").val();

      //  alert(pwd);

        var obj = new XMLHttpRequest();
        obj.onreadystatechange = function () {

            if (obj.readyState == 4 && obj.status == 200) {

                if (pwd === 'sunrise' || pwd === 'sunset' || pwd === 'night')
                {

                    document.getElementById("output").innerHTML = obj.responseText;
                }
                else
                {

                    var jsonobj = JSON.parse(obj.responseText);

                    document.getElementById("output").innerHTML = "Your IP: " + jsonobj.ip;
                }
            }
            else {
              //  alert("error in fetching data");
            }
        };

       
        switch (pwd) {

            case "sunrise":
                obj.open("GET", "sunrise.html", true);
                break;
            case "sunset":
                obj.open("GET", "sunset.html", true);
                break;
            case "night":
                obj.open("GET", "night.html", true);
                break;
            default:
                // not working because url is not accessible from local
                //obj.open("GET", "http://10.201.204.192:9080/api/PasswordDigest/" + pwd, true);
                obj.open("GET", "http://ip.jsontest.com/", true);

                break;
        }


        obj.send();
    }


    


});


function onLoad() {

    var obj1 = new XMLHttpRequest();
    obj1.onreadystatechange = function () {

        if (obj1.readyState == 4 && obj1.status == 200) {
            document.getElementById("subsdiv").innerHTML = obj1.responseText;
        }
    };

    obj1.open("GET", "night.html", true);

    
    obj1.send();

}


function startLoadEvent(eid) {

    var obj2 = new XMLHttpRequest();
    obj2.onreadystatechange = function () {

        if (obj2.readyState == 4 && obj2.status == 200) {
            document.getElementById("subsdiv").innerHTML = obj2.responseText;
        }
    };

    switch (eid) {

        case "sunrise":
            obj2.open("GET", "sunrise.html", true);
            break;
        case "sunset":
            obj2.open("GET", "sunset.html", true);
            break;
        case "night":
            obj2.open("GET", "night.html", true);
            break;
    }


    obj2.send();


}