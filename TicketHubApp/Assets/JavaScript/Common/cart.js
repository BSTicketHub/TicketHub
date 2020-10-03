UpdateCartData();

let showCart = document.getElementsByClassName("showCart")[0];
showCart.style.display = "none";
let open_display = document.getElementsByClassName("open")[0];


function shopCart() {
    showCart.style.display = "block";
}

function shopCartMove() {
    showCart.style.display = "none";
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
function addNewCart() {
    $(".addCart").click(function (e) {
        var id = `${$(this).parent().parent().parent().find("#chart_id").val()}`;
        var title = $(this).parent().parent().parent().find("#Title").text();
        var details = $(this).parent().parent().parent().find("#details").text();
        var discountPrice = Number($(this).parent().parent().parent().find("#DiscountPrice").text().replace("$", ""));
        var amount = Number($(this).parent().parent().parent().find("#cartcount").val());
        let imgSrc = $(this).parent().parent().parent().parent().find("img").attr("src");
        let cartItem = {
            id: id,
            title: title,
            details: details,
            price: discountPrice,
            amount: amount,
            img: `${imgSrc}`
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
    add();
    sub();
}

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
                    <div class="product-img img-bg" style="background-image: url(${item.img}); background-size: contain">
                    </div>
                </a>
                <div class="cart-detail">
                    <div class="product-detail ellipsis">
                        <h3 class="ellipsisw-200">
                            <a href="" id="puttitle" class="ellipsis">${item.title}</a>
                        </h3>
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

