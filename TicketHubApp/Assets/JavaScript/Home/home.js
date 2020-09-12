$('.carousel-autoplay').owlCarousel({
    loop: true,
    margin: 10,
    nav: true,
    autoplay: true,
    responsive: {
        0: {
            items: 1,
            autoplayTimeout: 5000,
        },
    }
})
$('.carousel1').owlCarousel({
    loop: true,
    margin: 10,
    nav: true,
    dots: false,
    responsive: {
        0: {
            items: 1,
        },
    }
})
$('.carousel2').owlCarousel({
    dots: false,
    loop: true,
    margin: 10,
    nav: true,
    // autoplay: true,
    // autoplayTimeout: 4000,
    responsive: {
        0: {
            items: 2
        },
        600: {
            items: 2
        },
        1000: {
            items: 5
        }
    }
})
$('.carousel3').owlCarousel({
    loop: true,
    margin: 10,
    nav: true,
    autoplay: true,
    responsive: {
        0: {
            items: 1
        },
        600: {
            items: 3
        },
        1000: {
            items: 5
        }
    }
})

//滾輪到上方
const btnScrollTop = document.querySelector("#btnScroll");
btnScrollTop.addEventListener("click", function () {
    window.scrollTo({
        top: 0,
        left: 0,
        behavior: "smooth"
    });
});

//滾到上方隱藏
window.onscroll = function () {
    scrollFunction()
};

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        btnScrollTop.style.display = "block";
    } else {
        btnScrollTop.style.display = "none";
    }
}

//倒數
untilTsCaculateDD()

function untilTsCaculateDD() {
    let $obj = $('#ddCountdown__panel');
    let second_time = $obj.data('untilts');
    second_time = parseInt(second_time);
    let second = 0;
    let minute = 0;
    let hour = 0;
    if (second_time > 0) {
        second_time = second_time - 1;
        $obj.data('untilts', second_time);

        if (parseInt(second_time) > 60) {
            second = parseInt(second_time) % 60;
            minute = parseInt(second_time / 60);

            if (minute > 60) {
                minute = parseInt(second_time / 60) % 60;
                hour = parseInt(parseInt(second_time / 60) / 60);

                if (hour > 24) {
                    hour = parseInt(parseInt(second_time / 60) / 60);
                }
            }
        }
    }
    if (hour < 10) {
        hour = '0' + hour;
    }
    if (minute < 10) {
        minute = '0' + minute;
    }

    if (second < 10) {
        second = '0' + second;
    }

    let html = '<div class="timer text-center" id="ddCountdown__hour">' + hour + '</div>' +
        '<div class="text-center"> : </div>' +
        '<div class="timer text-center" id="ddCountdown__min">' + minute + '</div>' +
        '<div class="text-center"> : </div>' +
        '<div class="timer text-center" id="ddCountdown__second">' + second + '</div>';

    $('#ddCountdown__panel').html(html);
    setTimeout(untilTsCaculateDD, 1000);
}

//導航欄購物車
let showCart = document.getElementsByClassName("showCart")[0];
showCart.style.display = "none";
let open_display = document.getElementsByClassName("open")[0];


function shopCart() {
    showCart.style.display = "block";
}

function shopCartMove() {
    showCart.style.display = "none";
}

// 刪除購物車
function delete_item() {
    //var deleteBtn = document.getElementById("delete_btn");
    //var deleteContext = document.getElementById("cart_delete_item");
}
$("#delete_btn").click(function (e) {
    e.preventDefault();
    var myobj = document.getElementById("cart_delete_item");
    myobj.remove();
    $(".cart_delete_item").hide();
    $(".alert").hide(this);
});


// 數量增加
function add() {
    var txt = document.getElementById("cartcount");
    var a = txt.value;
    a++;
    txt.value = a;
}

// 數量减少
function sub() {
    var txt = document.getElementById("cartcount");
    var a = txt.value;
    if (a > 1) {
        a--;
        txt.value = a;
    } else {
        txt.value = 1;
    }
}