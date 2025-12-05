using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SoftwareEngineeringQuizApp.Extensions
{
    /// <summary>
    /// Métodos de extensión para facilitar la validación de objetos
    /// </summary>
    public static class ValidationExtensions
    {
        /// <summary>
        /// Valida un objeto usando Data Annotations
        /// </summary>
        public static bool IsValid<T>(this T obj, out List<string> errors) where T : class
        {
            errors = new List<string>();

            if (obj == null)
            {
                errors.Add("El objeto no puede ser nulo.");
                return false;
            }

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(obj);

            bool isValid = Validator.TryValidateObject(
                obj,
                validationContext,
                validationResults,
                validateAllProperties: true);

            if (!isValid)
            {
                errors.AddRange(validationResults.Select(vr =>
                    vr.ErrorMessage ?? "Error de validación desconocido"));
            }

            return isValid;
        }

        /// <summary>
        /// Valida un objeto y lanza una excepción si no es válido
        /// </summary>
        public static void ValidateAndThrow<T>(this T obj) where T : class
        {
            if (!obj.IsValid(out var errors))
            {
                throw new ValidationException(string.Join(", ", errors));
            }
        }

        /// <summary>
        /// Obtiene una descripción amigable de los errores de validación
        /// </summary>
        public static string GetValidationErrorsDescription<T>(this T obj) where T : class
        {
            if (obj.IsValid(out var errors))
            {
                return "El objeto es válido.";
            }

            return $"Errores de validación:\n- {string.Join("\n- ", errors)}";
        }

        /// <summary>
        /// Valida una colección de objetos
        /// </summary>
        public static bool ValidateAll<T>(this IEnumerable<T> items, out Dictionary<T, List<string>> errorsByItem)
            where T : class
        {
            errorsByItem = new Dictionary<T, List<string>>();
            bool allValid = true;

            foreach (var item in items)
            {
                if (!item.IsValid(out var errors))
                {
                    errorsByItem[item] = errors;
                    allValid = false;
                }
            }

            return allValid;
        }
    }
}