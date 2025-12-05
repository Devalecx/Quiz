using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SoftwareEngineeringQuizApp.Validation
{
    /// <summary>
    /// Servicio centralizado para validación de objetos usando Data Annotations
    /// </summary>
    public class ValidationService : IValidationService
    {
        public bool Validate<T>(T obj, out List<string> errors) where T : class
        {
            errors = new List<string>();

            if (obj == null)
            {
                errors.Add("El objeto no puede ser nulo.");
                return false;
            }

            var validationResults = GetValidationErrors(obj);

            if (validationResults.Any())
            {
                errors.AddRange(validationResults.Select(vr => vr.ErrorMessage ?? "Error de validación desconocido"));
                return false;
            }

            return true;
        }

        public void ValidateAndThrow<T>(T obj) where T : class
        {
            if (obj == null)
            {
                throw new ValidationException("El objeto no puede ser nulo.");
            }

            var validationResults = GetValidationErrors(obj);

            if (validationResults.Any())
            {
                var errorMessages = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                throw new ValidationException($"Errores de validación: {errorMessages}");
            }
        }

        public List<ValidationResult> GetValidationErrors<T>(T obj) where T : class
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(obj);

            Validator.TryValidateObject(obj, validationContext, validationResults, validateAllProperties: true);

            return validationResults;
        }
    }
}