function doprocess() {

    if (navigator.geolocation) {

        navigator.geolocation.getCurrentPosition(showlocation);

    } else
    {
        alert ("Geolocation API not supported on the browser. Please consider upgrade!")
    }

}

function showlocation(position) {

    var lat = position.coords.latitude;
    var lng = position.coords.longitude;

    alert("Your location: latitude=" + lat + " and longitude=" + lng);

    var img = new Image();
    img.src = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lng + "&zoom=13&size=300x300&sensor=false";

    var output = document.getElementById("geo");
    output.appendChild(img);
}