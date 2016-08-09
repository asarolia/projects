String.prototype.repeat = function(num)
{
    return new Array(num + 1).join(this);
}

var socket;

function createSocket(host) {

    if (window.WebSocket) //check for browser support
        return new WebSocket(host);
    else if (window.MozWebSocket) //check for browser for support - Firefox
        return new MozWebSocket(host);
}

function init() {
    //var host = "ws://localhost:55555/echo"; //URL for websocket to connect to websocket server
    var host = "ws://echo.websocket.org/";
    try {
        //Task 1 - create and open the websocket connection 
        socket = createSocket(host);
        log('WebSocket - status ' + socket.readyState);

        //Task 2a -add event listener for opening connections
        socket.onopen = function (msg) {           
            log("Welcome - status " + this.readyState);
        };
        //Task 2b -add event listener for receiving messages via the websocket
        socket.onmessage = function (msg) {
            log("Received (" + msg.data.length + " bytes): " + msg.data);
        };
        //Task 2c -event listener for closing websocket connections
        socket.onclose = function (msg) {
            log("Disconnected - status " + this.readyState);
        };
    }
    catch (ex) {
        log(ex);
    }
    $("msg").focus();
}


function send() {
    var msg = document.getElementById('msg').value;
  
    try {
        //Task 3 -Use send() from WebSocket API to send messages via the websocket
        socket.send(msg);
        //Task 4 -update textbox with sent message and the number of bytes sent
       log('Sent (' + msg.length + " bytes): " + msg);
    } catch (ex) {
        log(ex);
    }
}
function quit() {
    log("Goodbye!");
    //Task 5 -use close() from the Websocket API to close the connection
    socket.close();
    socket = null;
}
/*
* Helper functions
*/
//Retrieves a DOM element with specified ID
function $(id) {
    return document.getElementById(id);
}
//Displays message text from websocket on the page
function log(msg) {
    $("log").innerHTML += "<br>" + msg;
}
function postData() {
        send();
   
}
