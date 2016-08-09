function allowDrag(event) {
    //   event.preventDefault();
    event.dataTransfer.setData("Text", event.target.id);
}

function ondragover_handle(event) {
    event.preventDefault();
}

function ondrop_handle(event) {
    event.preventDefault();
    var info = event.dataTransfer.getData("Text");

    event.target.appendChild(document.getElementById(info));
}
