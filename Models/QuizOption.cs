using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareEngineeringQuizApp.Models
{
    /// <summary>
    /// Representa una opción de respuesta para una pregunta de quiz
    /// </summary>
    public class QuizOption
    {
        /// <summary>
        /// Identificador único de la opción
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID de la tarjeta a la que pertenece esta opción
        /// </summary>
        [Required(ErrorMessage = "El ID de la tarjeta es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de la tarjeta debe ser mayor a 0.")]
        public int CardId { get; set; }

        /// <summary>
        /// Texto de la opción de respuesta
        /// </summary>
        [Required(ErrorMessage = "El texto de la opción es obligatorio.")]
        [StringLength(500, MinimumLength = 1,
            ErrorMessage = "La opción debe tener entre 1 y 500 caracteres.")]
        public string OptionText { get; set; } = string.Empty;

        /// <summary>
        /// Indica si esta es la opción correcta
        /// </summary>
        public bool IsCorrect { get; set; }

        /// <summary>
        /// Tarjeta a la que pertenece la opción (navegación)
        /// </summary>
        [ForeignKey(nameof(CardId))]
        public virtual Card? Card { get; set; }

        /// <summary>
        /// Valida que el objeto QuizOption sea válido
        /// </summary>
        public bool IsValid(out List<string> errors)
        {
            errors = new List<string>();

            if (CardId <= 0)
                errors.Add("El ID de la tarjeta debe ser mayor a 0.");

            if (string.IsNullOrWhiteSpace(OptionText))
                errors.Add("El texto de la opción no puede estar vacío.");

            if (OptionText?.Length < 1)
                errors.Add("La opción debe tener al menos 1 caracter.");

            if (OptionText?.Length > 500)
                errors.Add("La opción no puede exceder 500 caracteres.");

            return errors.Count == 0;
        }

        /// <summary>
        /// Representación en string del objeto
        /// </summary>
        public override string ToString()
        {
            var correctMark = IsCorrect ? "✓" : "✗";
            return $"[Option {Id}] {correctMark} {OptionText}";
        }
    }
}