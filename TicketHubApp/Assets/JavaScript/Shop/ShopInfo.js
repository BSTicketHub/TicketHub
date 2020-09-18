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
for(let item of inputList) {
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