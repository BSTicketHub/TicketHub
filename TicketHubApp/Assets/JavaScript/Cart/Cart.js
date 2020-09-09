var tbody = document.getElementById('ShowCartProduct');
var prouductImg = document.getElementById('productImg');
console.log(prouductImg)

var Title = document.getElementById('productTitle');
var unit = document.getElementById('unit');
var Price = document.getElementById('price');
var totalPrice = document.getElementById('totalPrice');
var deleteCan = document.getElementById('deleteCan');
var ticketArea = document.getElementById('inCartTicket');

var array = [{
        img: "../Assets/Images/img/bar.jpg",
        Title: "2",
        unit: "3",
        totalPrice: "4",
        deleteCan: "9"
    },
    {
        img: "../Assets/Images/img/gardan.jpg",
        Title: "口宣品",
        unit: "7",
        totalPrice: "890",
        deleteCan: "0"
    }
];



InitProduct();

function InitProduct() {


    for (item of array) {

        //照片區
        let img = item.img
        let addNewImgDiv = document.createElement('div');
        addNewImgDiv.classList.add('w-100' , 'my-3');
        addNewImgDiv.style.backgroundImage = `url("${img}")`

        prouductImg.appendChild(addNewImgDiv);

        //標題
        let addNewTitleDiv = document.createElement('div');
        addNewTitleDiv.classList.add('w-100','my-3');
        addNewTitleDiv.innerHTML = item.Title;
        Title.appendChild(addNewTitleDiv);

        //張數
        let addNewUnitDiv = document.createElement('div');
        addNewUnitDiv.classList.add('w-100','my-3');
        addNewUnitDiv.innerHTML = item.unit;
        unit.appendChild(addNewUnitDiv);

        //總價
        let addNewTotalDiv = document.createElement('div');
        addNewTotalDiv.classList.add('w-100','my-3');
        addNewTotalDiv.innerHTML = item.totalPrice;
        totalPrice.append(addNewTotalDiv);

        //刪除
        let addDelDiv = document.createElement('div');
        addDelDiv.classList.add('w-100');
        // backgroundImage= `url("${deleteCan}")`
        addDelDiv.innerHTML = item.deleteCan;
        deleteCan.appendChild(addDelDiv);
    }
}