using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace SoftwareEngineeringQuizApp.Models
{
    /// <summary>
    /// Representa un tema o categoría de estudio
    /// </summary>
    public class Topic
    {
        /// <summary>
        /// Identificador único del tema
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del tema (ej: "SQL", "Git", "QA")
        /// </summary>
        [Required(ErrorMessage = "El nombre del tema es obligatorio.")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-\.]+$",
            ErrorMessage = "El nombre solo puede contener letras, números, espacios, guiones y puntos.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripción detallada del tema
        /// </summary>
        [Required(ErrorMessage = "La descripción del tema es obligatoria.")]
        [StringLength(500, MinimumLength = 10,
            ErrorMessage = "La descripción debe tener entre 10 y 500 caracteres.")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Valida que el objeto Topic sea válido
        /// </summary>
        public bool IsValid(out List<string> errors)
        {
            errors = new List<string>();

            if (string.IsNullOrWhiteSpace(Name))
                errors.Add("El nombre no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(Description))
                errors.Add("La descripción no puede estar vacía.");

            if (Name?.Length < 3)
                errors.Add("El nombre debe tener al menos 3 caracteres.");

            if (Name?.Length > 100)
                errors.Add("El nombre no puede exceder 100 caracteres.");

            if (Description?.Length < 10)
                errors.Add("La descripción debe tener al menos 10 caracteres.");

            if (Description?.Length > 500)
                errors.Add("La descripción no puede exceder 500 caracteres.");

            return errors.Count == 0;
        }

        /// <summary>
        /// Representación en string del objeto
        /// </summary>
        public override string ToString()
        {
            return $"[Topic {Id}] {Name}";
        }
    }
}