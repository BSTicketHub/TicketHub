function initMap() {
    //這間餐廳在這裡 25.042848, 121.540344
    // The location of Uluru
    var taipei = {
        lat: 25.0392496,
        lng: 121.5397186
    };
    var map = new google.maps.Map(
        document.getElementById('mapApi'), {
        zoom: 14,
        center: taipei
    });
    // var marker = new google.maps.Marker({position: taipei, map: map});


    let addressRequest = new XMLHttpRequest()
    addressRequest.open('get', 'https://maps.googleapis.com/maps/api/geocode/json?latlng=25.0392496,121.5397186&key=AIzaSyDbOibXZPeCKpq5tbdQZD0B5By6Z3MkQHc&callback');

    addressRequest.send();
    addressRequest.onload = function () {
        console.log(this.response)
        let text = this.response;
        let coordinate = JSON.parse(text).results[0].geometry.location;
        let marker = new google.maps.Marker({
            position: coordinate,
            map: map
        })
    }
}
