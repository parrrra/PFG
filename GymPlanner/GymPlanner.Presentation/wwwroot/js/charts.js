let charts = {};

window.renderLineChart = (canvasId, labels, data, chartTitle) => {
  const ctx = document.getElementById(canvasId);
  if (!ctx) return;

  if (charts[canvasId]) {
    charts[canvasId].destroy();
  }

  const today = new Date();
  const parsedLabels = labels.map((label) => new Date(label));
  const splitIndex = parsedLabels.findIndex((date) => date > today);

  charts[canvasId] = new Chart(ctx, {
    type: "line",
    data: {
      labels: labels,
      datasets: [
        {
          label: "Peso (kg)",
          data: data,
          borderColor: "blue",
          backgroundColor: "lightblue",
          tension: 0.3,
          pointRadius: 4,
          pointHoverRadius: 6,
          fill: false,
          segment: {
            borderColor: (ctx) => {
              const i = ctx.p0DataIndex;
              return i < splitIndex || splitIndex === -1 ? "blue" : "gray";
            },
            borderDash: (ctx) => {
              const i = ctx.p0DataIndex;
              return i < splitIndex || splitIndex === -1 ? [] : [5, 5];
            },
          },
        },
      ],
    },
    options: {
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        title: {
          display: true,
          text: chartTitle,
        },
        annotation: {
          annotations:
            splitIndex !== -1
              ? {
                  line1: {
                    type: "line",
                    xMin: labels[splitIndex],
                    xMax: labels[splitIndex],
                    borderColor: "red",
                    borderWidth: 2,
                    label: {
                      content: "Hoy",
                      enabled: true,
                      position: "start",
                      backgroundColor: "rgba(255, 0, 0, 0.8)",
                    },
                  },
                }
              : {},
        },
      },
      scales: {
        y: {
          beginAtZero: true,
          title: {
            display: true,
            text: "Peso (kg)",
          },
        },
        x: {
          title: {
            display: true,
            text: "Fecha",
          },
          ticks: {
            maxRotation: 45,
            autoSkip: true,
          },
        },
      },
    },
    plugins: [Chart.registry.getPlugin("annotation")],
  });
};

window.destroyChart = (canvasId) => {
  if (charts[canvasId]) {
    charts[canvasId].destroy();
    delete charts[canvasId];
  }
};

window.renderMultipleLineChart = (
  canvasId,
  labels,
  datasetsInfo,
  chartTitle
) => {
  const ctx = document.getElementById(canvasId);
  if (!ctx) return;

  if (charts[canvasId]) {
    charts[canvasId].destroy();
  }

  const today = new Date();
  const parsedLabels = labels.map((label) => new Date(label));
  const splitIndex = parsedLabels.findIndex((date) => date > today);

  const datasets = datasetsInfo.map((info, index) => {
    const color = getColor(index);

    return {
      label: info.label,
      data: labels.map((date) => {
        const i = info.dates.indexOf(date);
        return i !== -1 ? info.data[i] : null;
      }),
      borderColor: color,
      backgroundColor: color + "33",
      tension: 0.3,
      pointRadius: 4,
      pointHoverRadius: 6,
      fill: false,
      spanGaps: true,
      segment: {
        borderColor: (ctx) => {
          const i = ctx.p0DataIndex;
          return i < splitIndex || splitIndex === -1 ? color : "gray";
        },
        borderDash: (ctx) => {
          const i = ctx.p0DataIndex;
          return i < splitIndex || splitIndex === -1 ? [] : [5, 5];
        },
      },
    };
  });

  charts[canvasId] = new Chart(ctx, {
    type: "line",
    data: {
      labels: labels,
      datasets: datasets,
    },
    options: {
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        title: {
          display: true,
          text: chartTitle,
        },
        annotation: {
          annotations:
            splitIndex !== -1
              ? {
                  line1: {
                    type: "line",
                    xMin: labels[splitIndex],
                    xMax: labels[splitIndex],
                    borderColor: "red",
                    borderWidth: 2,
                    label: {
                      content: "Hoy",
                      enabled: true,
                      position: "start",
                      backgroundColor: "rgba(255, 0, 0, 0.8)",
                    },
                  },
                }
              : {},
        },
      },
      scales: {
        y: {
          beginAtZero: true,
          title: {
            display: true,
            text: "Peso (kg)",
          },
        },
        x: {
          title: {
            display: true,
            text: "Fecha",
          },
          ticks: {
            maxRotation: 45,
            autoSkip: true,
          },
        },
      },
    },
    plugins: [Chart.registry.getPlugin("annotation")],
  });
};

function getColor(index) {
  const colors = [
    "#007bff",
    "#28a745",
    "#ffc107",
    "#dc3545",
    "#6610f2",
    "#fd7e14",
    "#20c997",
    "#6f42c1",
    "#e83e8c",
    "#17a2b8",
  ];
  return colors[index % colors.length];
}
