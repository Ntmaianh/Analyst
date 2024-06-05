var lineChartPercentForMajorStatistic; // Variable to store the pie chart object
var lineChartNumberForMajorStatistic;

function createLineChartPercent(semester, colorChoice, data) {
    var options = {
        type: 'line',
        data: {
            labels: semester,
            datasets: [
            {
                label: "Percent",
                data: data,
                borderWidth: 1,
                backgroundColor: colorChoice
            },
            ]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    }
    if (lineChartPercentForMajorStatistic) {
        lineChartPercentForMajorStatistic.data.labels = title;
       /* lineChartPercentForMajorStatistic.data.datasets[0].label = labelname;*/
        lineChartPercentForMajorStatistic.data.datasets[0].backgroundColor = colorChoice;
        lineChartPercentForMajorStatistic.data.datasets[0].data = percent;
        lineChartPercentForMajorStatistic.update();
    }
    else {
        lineChartPercentForMajorStatistic = new Chart(document.querySelector("#total-statistic-line-percent-chart"), options);
    }
}

function createLineChartNumber(semester, colorChoice, data) {
    var options = {
        type: 'line',
        data: {
            labels: semester,
            datasets: [{
                label: "Number",
                data: data,
                borderWidth: 1,
                backgroundColor: colorChoice
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    };

    if (lineChartNumberForMajorStatistic) {
        lineChartNumberForMajorStatistic.data.labels = title;
        /*chartPieForMajorStatistic.data.datasets[0].label = labelname;*/
        lineChartNumberForMajorStatistic.data.datasets[0].backgroundColor = colorChoice;
        lineChartNumberForMajorStatistic.data.datasets[0].data = number;
        lineChartNumberForMajorStatistic.update();
    }
    else {
        lineChartNumberForMajorStatistic = new Chart(document.querySelector(canvasId), options);
    }

}
