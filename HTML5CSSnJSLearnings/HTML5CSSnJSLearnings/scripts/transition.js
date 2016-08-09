function onLoad() {

    
    var d = document.getElementById("divtrans");

    d.addEventListener("transitionend", onTransitionEnd, true);

}

function onTransitionEnd(e) {
    var p = document.getElementById("text");

    p.innerHTML = "Transition for "+ e.propertyName+" ended in "+e.elapsedTime+" seconds";

}