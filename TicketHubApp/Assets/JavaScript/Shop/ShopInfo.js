var message = "@ViewBag.Message";
if (message) {
    window.alert(message)
}

var curCity = "@TempData["City"]";
var curDist = "@TempData["Dinstrict"]";

// 圖片讀取
function readURL(input) {
    if (input.files && input.files[0]) {
        var imageTagID = input.getAttribute("targetID");
        var reader = new FileReader();
        reader.onload = function (e) {
            var img = document.getElementById(imageTagID);
            img.setAttribute("src", e.target.result)
        }
        reader.readAsDataURL(input.files[0]);
    }
}

// 修改 / 編輯完成 button
var inputList = document.querySelectorAll("input, select, textarea");
for (let item of inputList) {
    item.setAttribute("disabled", "");
}

let modifyBtn = document.getElementById("modifyBtn");
let saveBtn = document.getElementById("saveBtn");

modifyBtn.addEventListener("click", function () {
    let input = document.querySelector("textarea");
    if (input.disabled == true) {
        for (let item of inputList) {
            item.removeAttribute("disabled", "");
        }
        saveBtn.classList.add("d-block");
        modifyBtn.innerText = "取消";
    } else {
        for (let item of inputList) {
            item.setAttribute("disabled", "");
        }
        saveBtn.classList.remove("d-block");
        modifyBtn.innerText = "修改";
    }
})


// 縣市 鄉鎮 selector
let url = "https://raw.githubusercontent.com/chenkai0709/FileStorage/master/taiwan_city-distinct";

window.onload = function () {
    fetchResource();
}

function fetchResource() {
    fetch(url)
        .then(response => response.text())
        .then(text => {
            // 成功...
            Data = JSON.parse(text);
            setForm(Data);
        })
        .catch(ex => {
            // 失敗...
            console.log(ex)
        });
}

function setForm(object) {
    let citySelect = document.getElementById("selectCity");
    filterRegion(object, citySelect,)
    citySelect.value = curCity;
    setDistinct(object, citySelect);
    citySelect.addEventListener('change', function () {
        setDistinct(object, citySelect);
    })
}

function setDistinct(object, citySelect) {
    let city = object.filter(x => x.name == citySelect.value)[0].districts;
    let distinctSelect = document.getElementById("selectDistinct");
    filterRegion(city, distinctSelect);
    distinctSelect.value = curDist;
}

function filterRegion(object, select) {
    select.innerHTML = "";
    let result = [];
    for (let i = 0, len = object.length; i < len; i++) {
        result += object[i].name;
        let option = document.createElement('option');
        option.innerHTML = object[i].name;
        option.value = object[i].name;
        select.appendChild(option)
    }
}