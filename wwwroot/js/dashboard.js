// ============================================
// DASHBOARD.JS - Funcionalidad del dashboard
// ============================================

// Función para actualizar el dashboard con datos reales
async function loadDashboardData() {
    try {
        const response = await fetch('/Reportes/GetDashboardData');
        const data = await response.json();
        updateDashboardUI(data);
    } catch (error) {
        console.error('Error loading dashboard data:', error);
    }
}

function updateDashboardUI(data) {
    // Actualizar tarjetas de resumen
    document.getElementById('totalClientes').textContent = data.totalClientes || 0;
    document.getElementById('totalBicicletas').textContent = data.totalBicicletas || 0;
    document.getElementById('reparacionesActivas').textContent = data.reparacionesActivas || 0;
    document.getElementById('itemsStockBajo').textContent = data.itemsStockBajo || 0;
    
    // Actualizar gráficos si existen
    if (window.reparacionesChart) {
        window.reparacionesChart.data.datasets[0].data = data.reparacionesPorMes || [];
        window.reparacionesChart.update();
    }
}

// Cargar datos al iniciar
document.addEventListener('DOMContentLoaded', function() {
    loadDashboardData();
});