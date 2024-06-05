let generalBarChart;
let majorOverviewPolarChart;
let majorOverviewBarChart;

function clearChart(chart) {
    if (chart) {
        chart.destroy();
    }
}

function createGeneralStatisticBarChart(labels, datasets, colors, data1, data2, data3, data4) {
    const ctx = document.getElementById('general-statistic-bar');

    clearChart(generalBarChart); // Clear the previous chart

    var options = {
        series: [{
            name: datasets[0],
            data: data1
        }, {
            name: datasets[1],
            data: data2
        }, {
            name: datasets[2],
            data: data3
        }, {
            name: datasets[3],
            data: data4
        }],
        chart: {
            type: 'bar',
            height: '100%'
        },
        plotOptions: {
            bar: {
                horizontal: false,
                endingShape: 'rounded',
                dataLabels: {
                    position: 'top',
                },
            },
        },
        dataLabels: {
            enabled: true,
            offsetY: 15,
            style: {
                fontSize: '1 rem',
                colors: ['#fff']
            }
        },
        colors: colors,
        stroke: {
            show: true,
            width: 2,
            colors: ['transparent']
        },
        xaxis: {
            categories: labels,
        },
        yaxis: {
            title: {
                //text: '$ (thousands)'
            }
        },
        fill: {
            opacity: 1
        },
        tooltip: {
            y: {
                //formatter: function (val) {
                //    return "$ " + val + " thousands"
                //}
            }
        }
    };

    generalBarChart = new ApexCharts(ctx, options);
    generalBarChart.render();
}

function createMajorOverviewPolarChart(colors, dataLabels, data) {
    const ctx = document.getElementById('major-overview-polar');

    clearChart(majorOverviewPolarChart); // Clear the previous chart

    var options = {
        series: data,
        chart: {
            type: 'polarArea',
        },
        colors: colors,
        stroke: {
            colors: ['#fff']
        },
        fill: {
            opacity: 0.8
        },
        labels: dataLabels,
        responsive: [{
            breakpoint: 480,
            options: {
                chart: {
                    width: '200'
                },
                legend: {
                    position: 'bottom'
                }
            }
        }]
    };

    majorOverviewPolarChart = new ApexCharts(ctx, options);
    majorOverviewPolarChart.render();
}

function createMajorOverviewBarChart(colors, dataLabels, data) {
    const ctx = document.getElementById('major-overview-bar');

    clearChart(majorOverviewBarChart); // Clear the previous chart

    var options = {
        series: [{
            name: 'Số lượng',
            data: data
        }],
        chart: {
            type: 'bar',
            height: '100%'
        },
        colors: colors,
        labels: dataLabels,
        plotOptions: {
            bar: {
                borderRadius: 4,
                horizontal: true,
                distributed: true,
            }
        },
        dataLabels: {
            enabled: false
        }
    };

    majorOverviewBarChart = new ApexCharts(ctx, options);
    majorOverviewBarChart.render();
}