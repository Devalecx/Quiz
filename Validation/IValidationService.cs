using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SoftwareEngineeringQuizApp.Validation
{
    /// <summary>
    /// Interfaz para el servicio de validación de objetos
    /// </summary>
    public interface IValidationService
    {
        /// <summary>
        /// Valida un objeto usando Data Annotations
        /// </summary>
        /// <typeparam name="T">Tipo del objeto a validar</typeparam>
        /// <param name="obj">Objeto a validar</param>
        /// <param name="errors">Lista de errores encontrados</param>
        /// <returns>True si el objeto es válido, False en caso contrario</returns>
        bool Validate<T>(T obj, out List<string> errors) where T : class;

        /// <summary>
        /// Valida un objeto y lanza una excepción si no es válido
        /// </summary>
        /// <typeparam name="T">Tipo del objeto a validar</typeparam>
        /// <param name="obj">Objeto a validar</param>
        /// <exception cref="ValidationException">Se lanza si el objeto no es válido</exception>
        void ValidateAndThrow<T>(T obj) where T : class;

        /// <summary>
        /// Obtiene todos los errores de validación de un objeto
        /// </summary>
        /// <typeparam name="T">Tipo del objeto a validar</typeparam>
        /// <param name="obj">Objeto a validar</param>
        /// <returns>Lista de errores encontrados</returns>
        List<ValidationResult> GetValidationErrors<T>(T obj) where T : class;
    }
}