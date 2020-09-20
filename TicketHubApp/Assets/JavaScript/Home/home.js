$(document).ready(function () {
    add();
    sub();
    cart_click();
    cart_click2();
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

//購物車事件
function cart_click() {
    $(".addCart").click(function () {
        var data = {}
        data.id = Number($(this).parent().parent().parent().find("#chart_id").val());
        data.name = $(this).parent().parent().parent().find("#Title").text();
        data.details = $(this).parent().parent().parent().find("#details").text();
        data.price = Number($(this).parent().parent().parent().find("#DiscountPrice").text().replace("$", ""));
        data.amount = $(this).parent().parent().parent().find("#cartcount").val();

        cartLS.add(data)
        alert("123")
    });
    //Local Storage 渲染
    function renderCart(items) {

        const cart = document.querySelector(".cart")
        const deleteInfo = document.querySelector(".deleteInfo")

        cart.innerHTML = items.map((item) => `<div class="cart-item">
    <a href="">
        <div class="product-img img-bg">
        </div>
    </a>
    <div class="cart-detail">
        <div class="product-detail">
            <h3>
                <a href="" id="puttitle">${item.name}</a>
            </h3>
            <div class="product-option">${item.details}</div>
        </div>
        <div class="text-tag">2020-09-12 11:00</div>
        <div>數量 x <span class="text-tag putamout" id="putamout">${item.amount}</span></div>
        <div class="product-pricing">
            <h4>TWD <span id="putprice">${item.price}</span></h4>
        </div>
    </div>
    <div class="product-action">
        <button type="button" class="btn btn-light" data-toggle="modal"
            data-target="#CartModal">
            <span class="iconify" data-icon="bi:trash" data-inline="false"></span>
        </button>
    </div>
</div>`).join("")

        deleteInfo.innerHTML = items.map((item) => ` <div class="cart-item">
    <a href="">
        <div class="product-img img-bg">
        </div>
    </a>
    <div class="cart-detail">
        <div class="product-detail">
            <h3>
                <a href="" id="puttitle">${item.name}</a>
            </h3>
            <div class="product-option">${item.details}</div>
        </div>
        <div class="text-tag">2020-09-12 11:00</div>
        <div>人數 x <span class="text-tag putamout" id="putamout">${item.amount}</span></div>
        <div class="product-pricing">
            <h4>TWD <span id="putprice">${item.price}</span></h4>
        </div>
    </div>
</div>
<div class="modal-footer">
    <h5 class="alert alert-danger mb-0 mr-auto">您確定要刪除所選商品嗎？</h5>
    <button type="button" class="btn btn-secondary" data-dismiss="modal">取消</button>
    <button type="button" class="btn btn-danger" id="delete_btn" onClick="cartLS.remove(${item.id})"
        >刪除</button>
</div><hr>`).join("")

    }
    renderCart(cartLS.list())
    cartLS.onChange(renderCart)
    
}



function cart_click2() {
    $(".addCart2").click(function () {
        var data = {}
        data.id = Number($(this).parent().parent().find("#chart_id").val());
        data.name = $(this).parent().parent().find("#Title").text();
        data.details = $(this).parent().parent().parent().find("#details").text();
        data.price = Number($(this).parent().parent().parent().find("#DiscountPrice").text().replace("$", ""));
        data.quantity = $(this).find("#addCart2");
        console.dir(data.amount);
        cartLS.add(data)

        //Local Storage 渲染
        function renderCart2(items) {

            const cart = document.querySelector(".cart")
            const deleteInfo = document.querySelector(".deleteInfo")

            cart.innerHTML = items.map((item) => `<div class="cart-item">
            <a href="">
                <div class="product-img img-bg">
                </div>
            </a>
            <div class="cart-detail">
                <div class="product-detail">
                    <h3>
                        <a href="" id="puttitle">${item.name}</a>
                    </h3>
                    <div class="product-option">${item.details}</div>
                </div>
                <div class="text-tag">2020-09-12 11:00</div>
                <div>數量 x <span class="text-tag putamout" id="putamout">${item.quantity}</span></div>
                <div class="product-pricing">
                    <h4>TWD <span id="putprice">${item.price}</span></h4>
                </div>
            </div>
            <div class="product-action">
                <button type="button" class="btn btn-light" data-toggle="modal"
                    data-target="#CartModal">
                    <span class="iconify" data-icon="bi:trash" data-inline="false"></span>
                </button>
            </div>
        </div>`).join("")

            deleteInfo.innerHTML = items.map((item) => ` <div class="cart-item">
            <a href="">
                <div class="product-img img-bg">
                </div>
            </a>
            <div class="cart-detail">
                <div class="product-detail">
                    <h3>
                        <a href="" id="puttitle">${item.name}</a>
                    </h3>
                    <div class="product-option">${item.details}</div>
                </div>
                <div class="text-tag">2020-09-12 11:00</div>
                <div>人數 x <span class="text-tag putamout" id="putamout">${item.quantity}</span></div>
                <div class="product-pricing">
                    <h4>TWD <span id="putprice">${item.price}</span></h4>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <h5 class="alert alert-danger mb-0 mr-auto">您確定要刪除所選商品嗎？</h5>
            <button type="button" class="btn btn-secondary" data-dismiss="modal">取消</button>
            <button type="button" class="btn btn-danger" id="delete_btn" onClick="cartLS.remove(${item.id})"
                >刪除</button>
        </div><hr>`).join("")

        }
        renderCart2(cartLS.list())
        cartLS.onChange(renderCart2)

    });
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