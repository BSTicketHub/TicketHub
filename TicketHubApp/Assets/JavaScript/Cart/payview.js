var tbody = document.getElementById('ShowCartProduct');
var prouductImg = document.getElementById('productImg');
console.log(prouductImg)

var Title = document.getElementById('productTitle');
var unit = document.getElementById('unit');
var Price = document.getElementById('price');
var totalPrice = document.getElementById('totalPrice');
var ticketArea = document.getElementById('inCartTicket');
var kindOf = document.getElementById('kindOf');
var totalPay = document.getElementById('totalPay');


var arrays = [{
        img: "../Assets/Images/img/bar.jpg",
        Title: "正官庄",
        unit: "3",
        totalPrice: "400",
    },
    {
        img: "../Assets/Images/img/gardan.jpg",
        Title: "口宣品",
        unit: "7",
        totalPrice: "890",
    }
];



InitProduct();

function InitProduct() {

    for (item of arrays) {

        //照片區
        let img = item.img
        let addNewImgDiv = document.createElement('div');
        addNewImgDiv.classList.add('w-100', 'my-3');
        addNewImgDiv.style.backgroundImage = `url("${img}")`

        prouductImg.appendChild(addNewImgDiv);

        //標題
        let addNewTitleDiv = document.createElement('div');
        addNewTitleDiv.classList.add('w-100', 'my-3');
        addNewTitleDiv.innerHTML = item.Title;
        Title.appendChild(addNewTitleDiv);

        //張數
        let addNewUnitDiv = document.createElement('div');
        addNewUnitDiv.classList.add('w-100', 'my-3');
        addNewUnitDiv.innerHTML = item.unit;
        unit.appendChild(addNewUnitDiv);

        //單品總價
        let addNewTotalDiv = document.createElement('div');
        addNewTotalDiv.classList.add('w-100', 'my-3');
        addNewTotalDiv.innerHTML = item.totalPrice;
        totalPrice.append(addNewTotalDiv);

    }

    //顯示購入的種類數
    let addNewKindP = document.createElement('p');
    addNewKindP.classList.add('text-secondary');
    addNewKindP.innerText = "?件商品";
    kindOf.appendChild(addNewKindP);

    //顯示最後要付費的總金額
    let addNewTPayp = document.createElement('p');
    addNewTPayp.classList.add('text-Primary');
    addNewTPayp.innerText = "TWD 3000";
    totalPay.appendChild(addNewTPayp);

}