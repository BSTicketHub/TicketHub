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