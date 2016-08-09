function startAnimation() {
    var p = document.getElementById("text");
    var rect = document.getElementById("rectAnim");
    var btn = document.getElementById("btn");

    // change the text

    p.innerHTML = "Animation started";

    // add the animation class to the rect division
    
    rect.classList.add("animate");

    // change the text of button

    btn.innerHTML = "Animation in process";

    // add the event handler for animation end 

    rect.addEventListener("animationend", function (e) {
        p.innerHTML = "Animation ended for "+e.animationName+" in "+e.elapsedTime+" seconds";
        btn.innerHTML = "Animation Complete";
    }, false);

}