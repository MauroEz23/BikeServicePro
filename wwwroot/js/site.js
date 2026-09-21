// ============================================
// SITE.JS - Funcionalidad general
// ============================================

// Auto-cerrar alertas después de 5 segundos
document.addEventListener('DOMContentLoaded', function() {
    const alerts = document.querySelectorAll('.alert:not(.alert-permanent)');
    alerts.forEach(function(alert) {
        setTimeout(function() {
            const bsAlert = new bootstrap.Alert(alert);
            bsAlert.close();
        }, 5000);
    });
});

// Confirmación para acciones destructivas
document.addEventListener('click', function(e) {
    if (e.target.closest('.btn-danger')) {
        if (!confirm('¿Estás seguro de que deseas realizar esta acción?')) {
            e.preventDefault();
        }
    }
});

// ============================================
// FUNCIONES PARA DASHBOARD Y GRÁFICOS
// ============================================

function formatCurrency(value) {
    return new Intl.NumberFormat('es-CL', {
        style: 'currency',
        currency: 'CLP',
        minimumFractionDigits: 0
    }).format(value);
}

function getStatusColor(status) {
    const colors = {
        'Recibida': '#ffc107',
        'Diagnóstico': '#0dcaf0',
        'EsperandoRepuestos': '#dc3545',
        'EnReparacion': '#0d6efd',
        'Lista': '#198754',
        'Entregada': '#6c757d',
        'Cancelada': '#212529'
    };
    return colors[status] || '#6c757d';
}

function getPrioridadColor(prioridad) {
    const colors = {
        'Baja': '#198754',
        'Media': '#ffc107',
        'Alta': '#dc3545',
        'Urgente': '#dc3545'
    };
    return colors[prioridad] || '#6c757d';
}

// ============================================
// INICIALIZAR TOOLTIPS
// ============================================

document.addEventListener('DOMContentLoaded', function() {
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function(tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
});