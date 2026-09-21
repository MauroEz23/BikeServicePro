using Microsoft.AspNetCore.Http;
using BikeServicePro.Models;

namespace BikeServicePro.Helpers
{
    /// <summary>
    /// Helper para verificar permisos basados en roles
    /// </summary>
    public static class AuthorizationHelper
    {
        /// <summary>
        /// Verifica si el usuario está autenticado
        /// </summary>
        public static bool IsAuthenticated(HttpContext httpContext)
        {
            return httpContext.Session.GetString("IsAuthenticated") == "true";
        }

        /// <summary>
        /// Obtiene el rol del usuario actual
        /// </summary>
        public static string? GetUserRole(HttpContext httpContext)
        {
            return httpContext.Session.GetString("Rol");
        }

        /// <summary>
        /// Verifica si el usuario es Administrador
        /// </summary>
        public static bool IsAdmin(HttpContext httpContext)
        {
            return GetUserRole(httpContext) == RolUsuario.Administrador.ToString();
        }

        /// <summary>
        /// Verifica si el usuario es Recepcionista
        /// </summary>
        public static bool IsRecepcionista(HttpContext httpContext)
        {
            return GetUserRole(httpContext) == RolUsuario.Recepcionista.ToString();
        }

        /// <summary>
        /// Verifica si el usuario es Mecánico
        /// </summary>
        public static bool IsMecanico(HttpContext httpContext)
        {
            return GetUserRole(httpContext) == RolUsuario.Mecanico.ToString();
        }

        /// <summary>
        /// Verifica si el usuario es Administrador o Recepcionista
        /// </summary>
        public static bool IsAdminOrRecepcionista(HttpContext httpContext)
        {
            var role = GetUserRole(httpContext);
            return role == RolUsuario.Administrador.ToString() || 
                   role == RolUsuario.Recepcionista.ToString();
        }

        /// <summary>
        /// Verifica si el usuario es Administrador o Mecánico
        /// </summary>
        public static bool IsAdminOrMecanico(HttpContext httpContext)
        {
            var role = GetUserRole(httpContext);
            return role == RolUsuario.Administrador.ToString() || 
                   role == RolUsuario.Mecanico.ToString();
        }

        /// <summary>
        /// Obtiene el ID del usuario actual
        /// </summary>
        public static int? GetUserId(HttpContext httpContext)
        {
            var userId = httpContext.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(userId) && int.TryParse(userId, out int id))
            {
                return id;
            }
            return null;
        }
    }
}