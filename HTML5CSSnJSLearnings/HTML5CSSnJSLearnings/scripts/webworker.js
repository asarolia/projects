var wrkr;
var wrkrstatus;

function startWorker() {
    wrkrstatus = document.getElementById('statusmsg');
    // 1. check the support 

    if (typeof(Worker) != "undefined")
    {

        wrkr = new Worker("scripts/worker1.js");

        var msg = "PROCESS";
        //2 post the message to the worker

        wrkr.postMessage(msg);

        wrkrstatus.innerHTML = "Message sent to web worker - worker running";

        //3. add event handler for worker returned message 

        wrkr.addEventListener("message", processWorkerMessage, false);

    }
    else {
        wrkrstatus.innerHTML = "Web worker API is not supported on the browser";
    }
}

function processWorkerMessage(e)
{
    if (e.data == 'WrongInput')
    {

        document.getElementById('wrkrop').innerHTML = "Wrong command passed";
        wrkrstatus.innerHTML = "Web Worker self terminated";

    } else
    {
        document.getElementById('wrkrop').innerHTML = e.data;
        wrkrstatus.innerHTML = "Worker processed the task! Please stop the worker";

    }

   // document.getElementById('wrkrop').innerHTML = e.data;
}

function stopWorker() {

    wrkr.terminate();
    wrkrstatus.innerHTML = "Web worker terminated.";
}