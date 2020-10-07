var close = true;
var release = false;

let selectOrder = document.getElementById("order");
selectOrder.addEventListener("change", function ordering() {
    fetchData(this.value, "ticketList", release, "issueTemplate");
});

let orderHistory = document.getElementById("orderHistory");
orderHistory.addEventListener("change", function ordering() {
    fetchData(this.value, "historyTicket", close, "historyTemplate");
});

fetchData(0, "ticketList", release, "issueTemplate");
fetchData(0, "historyTicket", close, "historyTemplate");

async function fetchData(orderValue, parentId, closed, template) {
    $.ajax({
        type: 'POST',
        url: 'getIssueApi',
        data: { order: orderValue, closed: closed },
        success: function (json) {
            json = JSON.parse(json);
            genTable(json, parentId, template);
        }
    });
}

function genTable(list, parentId, template) {
    let tbody = document.getElementById(parentId);
    tbody.innerHTML = "";
    var templateEle = document.getElementById(template);
    var td = templateEle.content.querySelectorAll("td");
    for (let item of list) {
        let tr = document.createElement("tr");
        if (item.ImgPath) {
            td[1].innerHTML = `<img src= ${item.ImgPath} />`;
        } else {
            td[1].innerHTML = "";
        }
        td[1].className += "p-0 align-middle";
        td[2].innerText = item.Title;
        td[3].innerText = item.Id;
        td[4].innerText = item.DiscountPrice;
        td[5].innerText = item.Status;
        td[6].innerHTML = item.ReleasedDate.replace(/T/, " ").slice(0, -3);
        td[7].innerText = item.SalesAmount;
        td[8].innerText = item.SalesPrice;
        var clone = document.importNode(templateEle.content, true);
        tr.appendChild(clone);
        tbody.appendChild(tr);
        tr.addEventListener("click", function () {
            let id = this.children[3].innerText;
            window.location.assign("IssueDetails" + "?id=" + id);
        });
        if (template == "issueTemplate") {
            let btn = tr.children[9].children[0];
            btn.value = item.Id;

            btn.addEventListener("click", function (e) {
                closeIssue(this.value)
                e.stopPropagation();
            });
        }
    }
}

function closeIssue(value) {
    $.ajax({
        type: 'POST',
        url: 'closeIssueApi',
        data: { Id: `${value}` },
        success: function (result) {
            if (result) {
                alert("下架成功");
                fetchData(0, "ticketList", release, "issueTemplate");
                fetchData(0, "historyTicket", close, "historyTemplate");
            } else {
                alert("下架失敗");
            }
        }
    });
}