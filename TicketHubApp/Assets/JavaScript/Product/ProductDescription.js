//中文主解


function initMap() {
    var coordinate;
    //這間餐廳在這裡 25.042848, 121.540344
    // The location of Uluru
    var taipei = {
        lat: 25.0392496,
        lng: 121.5397186
    };
    var map = new google.maps.Map(
        document.getElementById('mapApi'), {
            zoom: 17,
            center: taipei
    });
    // var marker = new google.maps.Marker({position: taipei, map: map});


    let addressRequest = new XMLHttpRequest();

    addressRequest.open('get', 'https://maps.googleapis.com/maps/api/geocode/json?address=320台灣桃園市中壢區春德路105號&key=AIzaSyDbOibXZPeCKpq5tbdQZD0B5By6Z3MkQHc&callback');

    addressRequest.send();
    addressRequest.onload = function () {
        let text = this.response;
        coordinate = JSON.parse(text).results[0].geometry.location;
        console.log(coordinate)
        map.setCenter(coordinate);
        let marker = new google.maps.Marker({
            position: coordinate,
            map: map
        })
    }
}
