let showTicket = document.querySelectorAll('.show-ticket')
let ticketArea = document.querySelectorAll('.ticket-area');
for (let i = 0; i < ticketArea.length; i++) {
    showTicket[i].addEventListener('click', function () {
        ticketArea[i].classList.toggle('show');
        ticketArea[i].classList.toggle('border');
    })
}

function initMap() {
    //這間餐廳在這裡 25.042848, 121.540344
    // The location of Uluru
    var taipei = {
        lat: 25.044367,
        lng: 121.535168
    };
    var map = new google.maps.Map(
        document.getElementById('map'), {
            zoom: 14,
            center: taipei
        });
    // var marker = new google.maps.Marker({position: taipei, map: map});


    let addressRequest = new XMLHttpRequest()
    addressRequest.open('get', 'https://maps.googleapis.com/maps/api/geocode/json?address=復興南路一段279巷4號&key=AIzaSyCyO1vy0QaITVTahKLSJYfw_bgPxI7H7IU');
    addressRequest.send();
    addressRequest.onload = function () {
        let text = this.response;
        let coordinate = JSON.parse(text).results[0].geometry.location;
        let marker = new google.maps.Marker({
            position: coordinate,
            map: map
        })
    }
}