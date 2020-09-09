var cacheData = {
    map: {},
    markers: [],
}

// Initialize and add the map
function initMap() {
    // The location of Uluru
    var uluru = {
        lat: 25.0417443,
        lng: 121.535194
    };
    // The map, centered at Uluru
    var map = new google.maps.Map(
        document.getElementById('mapApi'), {
            zoom: 15,
            center: uluru
        });
    // The marker, positioned at Uluru
    var marker = new google.maps.Marker({
        position: uluru,
        map: map
    });

    cacheData.map = map;
    // sendRequest();
}



function drawMap(features) {
    for (let i = 0; features.length > i; i++) {
        let location = {
            lat: features[i].geometry.coordinates[1],
            lng: features[i].geometry.coordinates[0]
        };
        let marker = new google.maps.Marker({
            position: location,
            map: cacheData.map
        });
        cacheData.markers.push(marker);
    }
}
