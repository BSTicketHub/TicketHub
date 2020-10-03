// set upload Image
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
// 預覽功能
function showModal() {
    let upLoadImage = document.getElementById("preview_progressbarTW_img").getAttribute("src");
    let textTitle = document.getElementById("textTitle").value;
    let memo = document.getElementById("memo").value;
    let originalPrice = document.getElementById("originalPrice").value;
    let salePrice = document.getElementById("salePrice").value;

    let ticketImg = document.querySelector(".ticketImg");
    let title = document.querySelector(".issue h3");
    let description = document.querySelector(".issue p.description");
    let ticketListOldPrice = document.querySelector(".ticketListOldPrice");
    let ticketListSalesPrice = document.querySelector(".ticketListSalesPrice");

    ticketImg.setAttribute('style', `background-image: url(${upLoadImage})`);
    title.innerText = textTitle;
    description.innerText = memo;
    ticketListOldPrice.innerText = originalPrice;
    ticketListSalesPrice.innerText = salePrice;

    let ticketTag = document.querySelector(".ticket .showTag");
    showTag(ticketTag);
}

function showTag(parent) {
    parent.innerHTML = null;
    tagList.forEach(function (value) {
        if (value != "") {
            let span = document.createElement("span");
            span.setAttribute("style", "height: 1.8rem")
            span.innerText = value;
            parent.append(span);
            span.addEventListener("click", function () {
                console.log(tagList);
                tagList.splice(tagList.indexOf(value), 1);
                console.log(tagList);
                setTagStringtoForm();
                parent.removeChild(span);
            })
        }
    })
    compileTag();
}

function fetchTag() {
    var color = $("input[type='checkbox']:checked").val();
    if (color != null) {
        let string = color + $("#tagInput").val()
        var showtag = document.getElementById("showTag");
        tagList.push(string);
        showTag(showtag);
        setTagStringtoForm();
    } else {
        $('#tagMessage').text(" X 請選擇顏色")
    }
}

function setColor() {
    $("input[type='checkbox']").each(function (item) {
        $(this).click(function (e) {
            $("input[type='checkbox']").each(function (input) {
                $(this).prop('checked', false);
            })
            $(this).prop('checked', true);
        })
    });

}

function setTagStringtoForm() {
    tagString = tagList.join(" ");
    $('#tagString').val(tagString);
}

window.onload = function(){
    setColor();
}

