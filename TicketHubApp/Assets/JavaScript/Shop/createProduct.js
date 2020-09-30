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
    showTag(tagList, ticketTag);
}