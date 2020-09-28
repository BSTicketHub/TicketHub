$(document).ready(function () {
    add();
    sub();
    UpdateCartData();
    UpdateCartData2();
    UpcartCounter();
    accordion();
});


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
    dots: false,
    loop: true,
    margin: 10,
    nav: true,
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


// 數量增加
function add() {
    $(".add_button").click(function () {
        var number = Number($(this).parent().parent().find("#cartcount").val())
        $(this).parent().parent().find("#cartcount").val(number + 1)
    });
}

// 數量减少
function sub() {
    $(".sub_button").click(function () {
        var number = Number($(this).parent().parent().find("#cartcount").val())
        if (number == 1) {

        } else {
            $(this).parent().parent().find("#cartcount").val(number - 1)
        }

    });
}

//刪除購買商品
function deleteCartItem(id) {
    console.log(id)
    console.log("delete")
    let cartData = JSON.parse(localStorage.getItem('Cart'))

    cartData = cartData.filter(x => x.id != id)

    localStorage.setItem('Cart', JSON.stringify(cartData))

    UpdateCartData();
    UpdateCartData2();
    UpcartCounter();
}

//購物車更新數量
function UpcartCounter() {
    let data = JSON.parse(localStorage.getItem('Cart'));
    let counter = 0;
    //console.log(data);
    // 顯示加總數
    for (let item of data) {
        let amount = +item.amount;
        counter = counter + amount;
        console.log(counter);
    }

    //console.log(counter);
    // document.getElementById("cartCounter").innerText = counter;
    document.getElementsByClassName("cartCounter")[0].innerText = counter;
    document.getElementsByClassName("cartCounter")[1].innerText = counter;
}

//最新推出Card 事件
$(".addCart").click(function (e) {
    var id = `${$(this).parent().parent().parent().find("#chart_id").val()}`;
    var title = $(this).parent().parent().parent().find("#Title").text();
    var details = $(this).parent().parent().parent().find("#details").text();
    var discountPrice = Number($(this).parent().parent().parent().find("#DiscountPrice").text().replace("$", ""));
    var amount = Number($(this).parent().parent().parent().find("#cartcount").val());

    //console.log(id)
    //console.log(title)
    //console.log(details)
    //console.log(discountPrice)
    //console.log(amount)

    let cartItem = {
        id: id,
        title: title,
        details: details,
        price: discountPrice,
        amount: amount
    }

    if (localStorage.getItem('Cart') == null) {
        localStorage.setItem('Cart', JSON.stringify([cartItem]))
    } else {
        let cart = JSON.parse(localStorage.getItem('Cart'))
        if (cart.filter(x => x.id == cartItem.id).length > 0) {
            cart[cart.indexOf(cart.filter(x => x.id == cartItem.id)[0])].amount += cartItem.amount
        } else {
            cart.push(cartItem);
        }
        localStorage.setItem('Cart', JSON.stringify(cart))
    }

    UpcartCounter();
    UpdateCartData()
})

//購物車顯示事件
function UpdateCartData() {
    var cart = document.querySelector(".cart")
    cart.innerHTML = ''

    if (localStorage.getItem('Cart') != null) {
        let cartData = JSON.parse(localStorage.getItem('Cart'))

        for (let item of cartData) {
            console.log(item);
            let carthtml = `
            <div class="cart-item">
                <a href="">
                    <div class="product-img img-bg">
                    </div>
                </a>
                <div class="cart-detail">
                    <div class="product-detail ellipsis">
                        <h3 class="ellipsisw-200">
                            <a href="" id="puttitle" class="ellipsis">${item.title}</a>
                        </h3>
                        <div class="product-option ellipsis">${item.details}</div>
                    </div>
                    <div class="text-tag"></div>
                    <div>數量 x <span class="text-tag putamout" id="putamout">${item.amount}</span></div>
                    <div class="product-pricing">
                        <h4>TWD <span id="putprice">${item.price}</span></h4>
                    </div>
                </div>
                <div class="product-action">
                    <button type="button" class="btn btn-light" onClick="deleteCartItem('${item.id}')">
                        <span class="iconify" data-icon="bi:trash" data-inline="false"></span>
                    </button>
                </div>
            </div>`

            cart.innerHTML += carthtml;
        }
    }

}


//首選星級餐廳、熱賣票劵Card 事件
$(".card_addToCart").click(function () {
    let card = $(this).parents(".product-detail");

    let id = `${card.find(".card_id").val()}`;
    let title = card.find(".card_title").text();
    let discountPrice = Number(card.find(".card_discountPrice").text().replace("$", ""));

    //key value值
    let cartItem = {
        id: id,
        title: title,
        price: discountPrice,
        amount: 1
    }

    if (localStorage.getItem('Cart') == null) {
        localStorage.setItem('Cart', JSON.stringify([cartItem]))
    } else {
        let cart = JSON.parse(localStorage.getItem('Cart'))
        if (cart.filter(x => x.id == cartItem.id).length > 0) {
            cart[cart.indexOf(cart.filter(x => x.id == cartItem.id)[0])].amount += 1
        } else {
            cart.push(cartItem);
        }
        localStorage.setItem('Cart', JSON.stringify(cart))
    }

    UpcartCounter();
    UpdateCartData2()
});

//購物車顯示事件
function UpdateCartData2() {
    var cart = document.querySelector(".cart")
    cart.innerHTML = ''

    if (localStorage.getItem('Cart') != null) {
        let cartData = JSON.parse(localStorage.getItem('Cart'))

        for (let item of cartData) {
            let carthtml = `
            <div class="cart-item">
                <a href="">
                    <div class="product-img img-bg">
                    </div>
                </a>
                <div class="cart-detail">
                    <div class="product-detail ellipsis">
                        <h3 class="ellipsisw-200">
                            <a href="" id="puttitle" class="ellipsis">${item.title}</a>
                        </h3>
                    </div>
                    <div>數量 x <span class="text-tag putamout" id="putamout">${item.amount}</span></div>
                    <div class="product-pricing">
                        <h4>TWD <span id="putprice">${item.price}</span></h4>
                    </div>
                </div>
                <div class="product-action">
                    <button type="button" class="btn btn-light" onClick="deleteCartItem('${item.id}')">
                        <span class="iconify" data-icon="bi:trash" data-inline="false"></span>
                    </button>
                </div>
            </div>`

            cart.innerHTML += carthtml
        }
    }

}



//手風琴
function accordion() {
    $("#faq").sticky({
        topSpacing: 100,
        bottomSpacing: 800,
        // zIndex: 1
    });
    $('body').scrollspy({
        target: '#faq'
    })
}

//搜尋
let search = document.querySelector('.search-content');
search.setAttribute('placeholder', '輸入地點開始美食之旅')