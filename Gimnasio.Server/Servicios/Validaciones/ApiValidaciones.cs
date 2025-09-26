using Gimnasio.Server.Modelos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Gimnasio.Server.Servicios.Validaciones
{
    public static class ApiValidaciones
    {
        /// <summary>
        /// Valida que una entidad no sea null y que el ModelState sea válido.
        /// </summary>
        /// <typeparam name="T">Tipo de la entidad a validar</typeparam>
        /// <param name="entity">Entidad a validar</param>
        /// <param name="modelState">Estado del modelo</param>
        /// <returns>IActionResult con el error, o null si no hay errores</returns>
        public static IActionResult? ValidarEntidad<T>(T entity, ModelStateDictionary modelState)
            where T : class
        {
            if (entity == null)
            {
                return new BadRequestObjectResult(new { mensaje = "El objeto no puede ser nulo." });
            }


            //var errores = ModelState.Values
            //    .SelectMany(v => v.Errors)
            //    .Select(e => e.ErrorMessage)
            //    .ToList();
            if (!modelState.IsValid)
            {
                return new BadRequestObjectResult(new { mensaje = "Faltan datos obligatorios" });
            }



            return null; // null significa "sin errores"
        }

        /// <summary>
        /// Valida entidad con verificación de consistencia de ID para operaciones PUT.
        /// </summary>
        /// <typeparam name="T">Tipo de entidad que implementa IHasId</typeparam>
        /// <param name="urlId">ID que viene en la URL</param>
        /// <param name="entity">Entidad a validar</param>
        /// <param name="modelState">Estado del modelo</param>
        /// <returns>IActionResult con el error, o null si no hay errores</returns>
        public static IActionResult? ValidarEntidadConId<T>(int urlId, T entity, ModelStateDictionary modelState)
            where T : class, IHasId
        {
            // Primero las validaciones básicas
            var basicValidation = ValidarEntidad(entity, modelState);
            if (basicValidation != null) return basicValidation;

            // Validaciones específicas de ID
            if (entity.Id == 0)
            {
                return new BadRequestObjectResult(new { mensaje = "El Id del objeto es obligatorio." });
            }

            if (entity.Id != urlId)
            {
                return new BadRequestObjectResult(new { mensaje = "El Id del body debe coincidir con el Id de la URL." });
            }

            return null;
        }

        /// <summary>
        /// Valida ID para operaciones DELETE.
        /// </summary>
        /// <param name="id">ID a validar</param>
        /// <returns>IActionResult con el error, o null si no hay errores</returns>
        public static IActionResult? ValidarId(int id)
        {
            if (id <= 0)
            {
                return new BadRequestObjectResult(new { mensaje = "El Id debe ser mayor a 0." });
            }

            return null;
        }
    }
}
