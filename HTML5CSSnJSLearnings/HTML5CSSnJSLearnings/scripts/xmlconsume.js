/// <reference path="jquery-2.1.4.js" />

function displayData(e) {
    
    var reqobj = new XMLHttpRequest();
    var url = "xml/data.xml"
    reqobj.open("GET", url, false);

    reqobj.onreadystatechange = function () {

        if (reqobj.readyState == 4 && reqobj.status == 200) {

            //  alert(reqobj.responseXML);

            var xmldoc = reqobj.responseXML;

            var uprn = xmldoc.childNodes[0].getElementsByTagName("uprn")[0].textContent.toString();

            //alert(xmldoc.getElementByTagname("FloodReRiskResponse")[0].childNodes[1].nodevalue.toString());
            var str = "Flood re risk response for property : " +
                       "<br/>" +
                      "Unique property reference =" + uprn;

            document.getElementById("content").innerHTML = str;

            //document.writeln("Flood re risk response for property : ");
            //document.writeln("Unique property reference =" + uprn);


        } else {

            alert(reqobj.status);

        }

    }
    reqobj.send();
}

function displayJsonData(e) {

    var reqobj = new XMLHttpRequest();
    var url = "xml/jsondata.json"
    reqobj.open("GET", url, false);

    reqobj.onreadystatechange = function () {

        if (reqobj.readyState == 4 && reqobj.status == 200) {

            //  alert(reqobj.responseXML);

            var jsondoc = JSON.parse(reqobj.responseText);

            var uprn = jsondoc.FloodReRiskResponse.uprn;

            
            var str = "Flood re risk response for property : " +
                       "<br/>" +
                      "Unique property reference =" + uprn;

            document.getElementById("jsoncontent").innerHTML = str;

            var jsonstring = JSON.stringify(jsondoc, ["FloodReRiskResponse", "frid"])
            var index = jsonstring.search("frid") + 7;
            var data = "";
            for (var i = index ;i < jsonstring.length-3;i++)
            {
                data += jsonstring[i];

            }

            var op = "String after stringify method is = " + jsonstring +
                     "<br/>" +
                     "Flood Re unique ID : " + data;
         
            document.getElementById("stringifydata").innerHTML = op;

        } else {

            alert(reqobj.status);

        }

    }
    reqobj.send();
}

function displayEvalData(e) {

    var reqobj = new XMLHttpRequest();
    var url = "xml/jsondata.json"
    reqobj.open("GET", url, false);

    reqobj.onreadystatechange = function () {

        if (reqobj.readyState == 4 && reqobj.status == 200) {

            //  alert(reqobj.responseXML);

            var jsondoc = eval('('+reqobj.responseText+')');

            var uprn = jsondoc.FloodReRiskResponse.uprn;

            //alert(xmldoc.getElementByTagname("FloodReRiskResponse")[0].childNodes[1].nodevalue.toString());
            var str = "Flood re risk response for property : " +
                       "<br/>" +
                      "Unique property reference =" + uprn;

            document.getElementById("jsoncontent").innerHTML = str;

            //document.writeln("Flood re risk response for property : ");
            //document.writeln("Unique property reference =" + uprn);


        } else {

            alert(reqobj.status);

        }

    }
    reqobj.send();
}

function serializeData(e) {
    var formdata = $('#form').serialize;
    
    document.getElementById("serializedata").innerHTML = formdata;

}