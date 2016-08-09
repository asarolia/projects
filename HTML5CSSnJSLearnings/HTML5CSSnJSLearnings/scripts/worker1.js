function processTask(e)
{
    if (e.data == 'PROCESS')
    {
        var delay = 10000; //10 seconds

        setTimeout(function () {
            //code to be executed after 10 seconds
            self.postMessage("Command Success!");

        }, delay);

    } else
    {
        self.postMessage("WrongInput");
        self.close();

    }

}

self.addEventListener("message", processTask, false);
//var i = 0;

//function timedCount() {
//    i = i + 1;
//    postMessage(i);
//    setTimeout("timedCount()", 500);
//}

//timedCount();