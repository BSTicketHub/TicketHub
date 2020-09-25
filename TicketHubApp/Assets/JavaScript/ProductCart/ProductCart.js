//import inner from "../../../Scripts/src/modifiers/inner";

var prouductImg = document.getElementById('productImg');
var Title = document.getElementById('productTitle');
var unit = document.getElementById('unit');
var Price = document.getElementById('price');
var totalPrice = document.getElementById('totalPrice');
var deleteCan = document.getElementById('deleteCan');
var ticketArea = document.getElementById('inCartTicket');
var DayOfOrder = document.getElementById('orderDay');
var checkbox = document.getElementById('checkbox');
var kindOf = document.getElementById('kindOf');
var totalPay = document.getElementById('totalPay');

var moneyBox = document.getElementsByClassName('moneyBox')[0];
var imgAndDetail = document.getElementById('imgAndDetail');
var accountCheck = document.getElementById('accountCheck');


var array = [{
    img: "../Assets/Images/img/bar.jpg",
    Title: "日月潭紅茶五吃",
    Intro: "台灣手路菜台灣手路菜台灣手路菜台灣手路菜台灣手路菜台灣手路菜台灣手路菜",
    originPrice: "原價: 200",
    discountPrice: "150",
    unit: "3",
    totalPrice: "450",
    deleteCan: "垃圾桶"
},
{
    img: "../Assets/Images/img/gardan.jpg",
    Title: "台中屋馬燒肉",
    Intro: "熟成紙木桶手作味噌",
    originPrice: "原價:200",
    discountPrice: "150",
    unit: "7",
    totalPrice: "490",
    deleteCan: "垃圾桶"
}
];


var arrays = [{
    imgs: "../Assets/Images/img/bar.jpg",
    Title: "日月潭紅茶五吃",
    originPrice: "原價: 200",
    discountPrice: "折扣價: 150",
    day: "2020/09/30",
    unit: "3",
    totalPrice: "450",
    
},
{
    imgs: "../Assets/Images/img/gardan.jpg",
    Title: "台中屋馬燒肉",
    discountPrice: "折扣價: 150",
    day: "2020/09/30",
    unit: "7",
    totalPrice: "490",
}
];


InitProduct();
ProductPay();

function InitProduct() {

    for (item of array) {

        //checkbox
        let checkArea = document.createElement('div');
        let addCheck = document.createElement('input');
        checkArea.classList.add('w-100', 'd-flex', 'align-items-center', 'justify-content-center');
        addCheck.setAttribute('type', 'checkbox');
        addCheck.classList.add('form-check-input', 'position-static');
        checkbox.appendChild(checkArea);
        checkArea.appendChild(addCheck);


        //照片區
        let img = item.img;
        let addNewImgDiv = document.createElement('div');
        addNewImgDiv.classList.add('w-100', 'my-3', 'rounded');
        addNewImgDiv.style.backgroundImage = `url("${img}")`

        prouductImg.appendChild(addNewImgDiv);

        //標題
        let addNewTitleDiv = document.createElement('div');
        let addNewTitle = document.createElement('p');
        let addNewIntro = document.createElement('p');
        //let priceArea = document.createElement('div');
        let originPrice = document.createElement('p');
        //let discountPrice = document.createElement('p');
        addNewTitleDiv.classList.add('w-100', 'my-3', 'text-left');
        addNewIntro.classList.add('w-100', 'fontsize', 'text-left', 'overflow-hidden');
        addNewIntro.style.height = "1.4rem";
        addNewTitle.classList.add('carttitle');
        //priceArea.classList.add('text-left', 'row', 'w-100');
        originPrice.classList.add('fontsize', 'text-muted', 'w-50', 'ml-3');
        //discountPrice.classList.add('carttitle', 'text-muted', 'w-30');
        addNewTitle.innerHTML = item.Title;
        addNewIntro.innerHTML = item.Intro;
        originPrice.innerHTML = item.originPrice;
        //discountPrice.innerHTML = item.discountPrice;
        addNewTitleDiv.appendChild(addNewTitle);
        addNewTitleDiv.appendChild(addNewIntro);
        //addNewTitleDiv.appendChild(priceArea);
        //priceArea.appendChild(originPrice);
        //priceArea.appendChild(discountPrice);
        Title.appendChild(addNewTitleDiv);

        //訂購日期改(單價)
        let discountDiv = document.createElement('div');
        discountDiv.classList.add('w-100', 'my-3', 'fontsize');
        discountDiv.innerHTML = item.discountPrice;
        orderDay.appendChild(discountDiv);

        //張數
        let addNewUnitDiv = document.createElement('div');
        addNewUnitDiv.classList.add('w-100', 'my-3', 'fontsize');
        addNewUnitDiv.innerHTML = item.unit;
        unit.appendChild(addNewUnitDiv);

        //總價
        let addNewTotalDiv = document.createElement('div');
        addNewTotalDiv.classList.add('w-100', 'my-3');
        addNewTotalDiv.innerHTML = item.totalPrice;
        totalPrice.appendChild(addNewTotalDiv);

        //刪除
        let addDelDiv = document.createElement('div');
        addDelDiv.classList.add('w-100');
        addDelDiv.innerHTML = item.deleteCan;
        deleteCan.appendChild(addDelDiv);

    }
}

function ProductPay() {

    for (items of arrays) {
        console.log('hey');

        let imgAndDetail = document.createElement('div');
        imgAndDetail.classList.add('text-center', 'd-flex', 'row')
        moneyBox.appendChild(imgAndDetail)
        //付款明細
        //照片
        let moneyImg = document.createElement('div')
        let imgs = items.imgs;
        moneyImg.classList.add('col-1', 'd-flex', 'flex-wrap', 'h-50', 'mx-3', 'rounded');
        moneyImg.style.backgroundImage = `url("${imgs}")`;
        imgAndDetail.appendChild(moneyImg);

        //明細
        let moneyDetail = document.createElement('div');
        moneyDetail.classList.add('w-100', 'col-10', 'd-flex', 'flex-wrap');
        
        //票名
        let addTkNameP = document.createElement('p');
        addTkNameP.classList.add('w-100', 'text-left', 'ticketName');
        addTkNameP.innerText = items.Title;
        moneyDetail.appendChild(addTkNameP);

        //購買日
        let addDayP = document.createElement('p');
        addDayP.classList.add('w-100', 'text-left', 'text-muted');
        addDayP.innerHTML = items.day;
        moneyDetail.appendChild(addDayP);

        //張數
        let addUnitP = document.createElement('p');
        addUnitP.classList.add('w-100', 'text-left', 'text-muted');
        addUnitP.innerHTML ="張數: "+ items.unit;
        moneyDetail.appendChild(addUnitP);
        imgAndDetail.appendChild(moneyDetail);

        //價錢
        let accountPriceDiv = document.createElement('div');
        accountPriceDiv.classList.add('row', 'd-flex', 'flex-wrap', 'w-100','justify-content-end');

        let accountCh = document.createElement('div');
        accountCh.classList.add( 'text-left', 'mr-3');

       
        let accountChP = document.createElement('p');
        accountChP.innerText = "總金額";
        accountCh.appendChild(accountChP);
        accountPriceDiv.appendChild(accountCh);

        let totalPrice = document.createElement('div');
        totalPrice.classList.add('w-40', 'text-left');
        let accountPriceP = document.createElement('p');
        accountPriceP.innerText = items.totalPrice;
        totalPrice.appendChild(accountPriceP);
        accountPriceDiv.appendChild(totalPrice);

        moneyDetail.appendChild(accountPriceDiv);

        


    }
    let kindOf = document.createElement('div');
    kindOf.classList.add('align-self-center', 'ml-2', 'mr-2');
    kindOf.innerText = "2件商品"
    accountCheck.appendChild(kindOf);

    let totalPay = document.createElement('div');
    totalPay.classList.add('align-self-center', 'ml-2', 'mr-2');
    totalPay.innerText = "940 元";
    accountCheck.appendChild(totalPay);

    let goPay = document.createElement('button');
    goPay.classList.add('btn', 'btn-primary');
    goPay.innerText = "確認結帳";
    accountCheck.appendChild(goPay);

}