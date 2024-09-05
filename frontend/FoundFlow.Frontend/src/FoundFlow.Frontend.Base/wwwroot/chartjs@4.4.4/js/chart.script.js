window.RunningDashChart = (canvasId, labels, data) => {
    const ctx = document.getElementById(canvasId);

    new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: '',
                data: data,
                borderColor: '#4e7fe8',
                tension: 0.1,
                formatter: (value) => {
                    return value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
                }
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        callback: (value) => {
                            return value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
                        }
                    }
                }
            },
            elements: {
              point: {
                  radius: 6,
                  backgroundColor: '#4e7fe8'
              }  
            },
            plugins: {
                legend: {
                    display: false
                },
                tooltip: {
                    callbacks: {
                        label: (context) => {
                            let label = context.dataset.label || '';
                            if (label) {
                                label += ': ';
                            }
                            if (context.parsed.y !== null) {
                                label += context.parsed.y.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
                            }
                            return label;
                        }
                    }
                }
            }
        }
    });
}