// 今日訂單數 & 今日銷售金額 & 今日票券數
let counters = document.querySelectorAll('.counter');
const speed = 200; // The lower the slower

counters.forEach(counter => {
    const updateCount = () => {
        const target = +counter.getAttribute('data-target');
        const count = +counter.innerText;

        // Lower inc to slow and higher to slow
        const inc = target / speed;

        // Check if target is reached
        if (count < target) {
            // Add inc to count and output in counter
            counter.innerText = count + inc;
            // Call function every ms
            setTimeout(updateCount, 1);
        } else {
            counter.innerText = target;
        }
    };

    updateCount();
});


// 造訪人數圖表 & 銷售金額圖表
var curDateTime = new Date()
var curYear = curDateTime.getFullYear();
var curMonth = curDateTime.getMonth();
var curDate = curDateTime.getDate();
function setLabel(mode) {
    let Labels = [];
    if (mode == 'month') {
        for (let i = 11; i >= 0; i--) {
            let tempDateTime = new Date(curYear, curMonth - i);
            let tempYear = tempDateTime.getFullYear();
            let tempMonth = tempDateTime.getMonth();
            Labels.push(`${tempYear}-${tempMonth + 1}`);
        }
    } else if (mode == 'week') {
        for (let i = 6; i >= 0; i--) {
            let tempDateTime = new Date(curYear, curMonth, curDate - i);
            let tempYear = tempDateTime.getFullYear();
            let tempMonth = tempDateTime.getMonth();
            let tempDate = tempDateTime.getDate();
            Labels.push(`${tempYear}-${tempMonth}-${tempDate}`);
        }
    }
    return Labels;
}

var visitCanvas = document.getElementById('visitCount').getContext('2d');
var moneyCanvas = document.getElementById('moneyCount').getContext('2d');
var visitChart = createChart(visitCanvas, 'gray', '#26bcc7', null, [0, 0, 0, 0, 0, 0, 0]);
var moneyChart = createChart(moneyCanvas, 'gray', 'rgb(255, 99, 132)', null, [0, 0, 0, 0, 0, 0, 0]);

function createChart(Element, dotColor, lineColor, labels, data) {
    let title = (Element == visitCanvas) ? "顧客人次" : "總金額";
    let chart = new Chart(Element, {
        // The type of chart we want to create
        type: 'line',
        // The data for our dataset
        data: {
            labels: labels,
            datasets: [{
                label: title,
                backgroundColor: dotColor,
                borderColor: lineColor,
                data: data,
                fill: false
            }]
        },
        // Configuration options go here
        options: {
            scales: {
                xAxes: [{
                    ticks: {
                        fontSize: 8
                    }
                }]
            }
        }
    });
    return chart;
}

// 圖表 button switch
let weekVisitBtn = document.getElementById("weekVisit");
let monthVisitBtn = document.getElementById("monthVisit");
let weekMoneyBtn = document.getElementById("weekMoney");
let monthMoneyBtn = document.getElementById("monthMoney");

weekVisitBtn.addEventListener('click', function () { switchWeekMonth(this, monthVisitBtn, "-30px", visitChart, 'week') });
monthVisitBtn.addEventListener('click', function () { switchWeekMonth(this, weekVisitBtn, "30px", visitChart, 'month') });
weekMoneyBtn.addEventListener('click', function () { switchWeekMonth(this, monthMoneyBtn, "-30px", moneyChart, 'week') });
monthMoneyBtn.addEventListener('click', function () { switchWeekMonth(this, weekMoneyBtn, "30px", moneyChart, 'month') });


function switchWeekMonth(switchNode, brotherNode, position, chart, mode) {
    switchNode.style.color = "#fff";
    switchNode.style.backgroundPosition = "0px 0px";
    brotherNode.style.backgroundColor = "#fff";
    brotherNode.style.color = "#4da9db";
    brotherNode.style.backgroundPosition = position;
    let Labels = setLabel(mode);
    chart.data.labels = Labels;
    fetchChart(Labels, chart);
}

// 初始化
switchWeekMonth(weekVisitBtn, monthVisitBtn, "-30px", visitChart, 'week');
switchWeekMonth(weekMoneyBtn, monthMoneyBtn, "-30px", moneyChart, 'week');

function fetchChart(data, chart) {
    $.ajax({
        type: 'POST',
        url: 'chartApi',
        data: { Labels: data, Type: chart.id },
        success: function (json) {
            chart.data.datasets[0].data = json;
            chart.update();
        }
    });
}




// sales report
let search = document.getElementById("search");
let strRepo = document.getElementById("startDate").value;
let endRepo = document.getElementById("endDate").value;

function updateReport(json) {
    document.getElementById("TotalSales").innerText = parseFloat(json[0]);
    document.getElementById("TotalAmount").innerText = parseInt(json[1], 10);
    document.getElementById("TotalCutsom").innerText = parseInt(json[2], 10);
}

search.addEventListener("click", () => { fetchData(strRepo, endRepo, updateReport) });

// today's sales and customer
// convert JavaScript date format to c# datetime
Date.prototype.Format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份
        "d+": this.getDate(), //日
        "H+": this.getHours(), //小時
        "m+": this.getMinutes(), //分
        "s+": this.getSeconds(), //秒
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度
        "S": this.getMilliseconds() //毫秒
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
}
var timeNow = new Date().Format("yyyy-MM-dd");

function todaysReport(json) {
    document.getElementById("todaySales").setAttribute("data-target", parseInt(json[0]));
    document.getElementById("todayAmount").setAttribute("data-target", parseInt(json[1]));
}

fetchData(timeNow, timeNow, todaysReport);


// ajax get report function, input: start date/ end date/ success function(data)
async function fetchData(startDate, endDate, updateData) {
    $.ajax({
        type: 'POST',
        url: 'getReportApi',
        data: { duration: [startDate, endDate] },
        success: function (json) {
            updateData(json);
        }
    });
}



// top 5
fetchTop5Issue();
fetchTop5Customer();
function top5Table(json, tbody) {
    for (var item of json) {
        let tr = document.createElement("tr");
        let td_t = document.createElement("td");
        let td_a = document.createElement("td");
        let td_s = document.createElement("td");
        td_t.innerText = item.Name;
        td_a.innerText = item.IssueAmount;
        td_s.innerText = item.TotalRevenue;
        tr.append(td_t, td_a, td_s);
        tbody.append(tr);
    }
}
async function fetchTop5Issue() {
    $.ajax({
        type: 'POST',
        url: 'TopIssueApi',
        success: function (json) {
            let tbody = document.getElementById("top5Issue");
            top5Table(json, tbody);
        }
    });
}
async function fetchTop5Customer() {
    $.ajax({
        type: 'POST',
        url: 'TopCustomerApi',
        success: function (json) {
            let tbody = document.getElementById("top5Custom");
            top5Table(json, tbody);
        }
    });
}