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
    /// Representa una tarjeta de estudio que puede usarse como pregunta de quiz o flashcard
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Identificador único de la tarjeta
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID del tema al que pertenece esta tarjeta
        /// </summary>
        [Required(ErrorMessage = "El ID del tema es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del tema debe ser mayor a 0.")]
        public int TopicId { get; set; }

        /// <summary>
        /// Texto de la pregunta
        /// </summary>
        [Required(ErrorMessage = "El texto de la pregunta es obligatorio.")]
        [StringLength(1000, MinimumLength = 10,
            ErrorMessage = "La pregunta debe tener entre 10 y 1000 caracteres.")]
        public string QuestionText { get; set; } = string.Empty;

        /// <summary>
        /// Texto de la respuesta (usado en modo flashcard)
        /// </summary>
        [Required(ErrorMessage = "El texto de la respuesta es obligatorio.")]
        [StringLength(2000, MinimumLength = 3,
            ErrorMessage = "La respuesta debe tener entre 3 y 2000 caracteres.")]
        public string AnswerText { get; set; } = string.Empty;

        /// <summary>
        /// Tema al que pertenece la tarjeta (navegación)
        /// </summary>
        [ForeignKey(nameof(TopicId))]
        public virtual Topic? Topic { get; set; }

        /// <summary>
        /// Opciones de respuesta para modo quiz
        /// </summary>
        public virtual List<QuizOption> Options { get; set; } = new();

        /// <summary>
        /// Valida que el objeto Card sea válido
        /// </summary>
        public bool IsValid(out List<string> errors)
        {
            errors = new List<string>();

            if (TopicId <= 0)
                errors.Add("El ID del tema debe ser mayor a 0.");

            if (string.IsNullOrWhiteSpace(QuestionText))
                errors.Add("El texto de la pregunta no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(AnswerText))
                errors.Add("El texto de la respuesta no puede estar vacío.");

            if (QuestionText?.Length < 10)
                errors.Add("La pregunta debe tener al menos 10 caracteres.");

            if (QuestionText?.Length > 1000)
                errors.Add("La pregunta no puede exceder 1000 caracteres.");

            if (AnswerText?.Length < 3)
                errors.Add("La respuesta debe tener al menos 3 caracteres.");

            if (AnswerText?.Length > 2000)
                errors.Add("La respuesta no puede exceder 2000 caracteres.");

            // Validar opciones si existen
            if (Options != null && Options.Any())
            {
                if (Options.Count < 2)
                    errors.Add("Debe haber al menos 2 opciones de respuesta.");

                if (Options.Count > 6)
                    errors.Add("No puede haber más de 6 opciones de respuesta.");

                var correctOptions = Options.Count(o => o.IsCorrect);
                if (correctOptions == 0)
                    errors.Add("Debe haber al menos una opción correcta.");

                if (correctOptions > 1)
                    errors.Add("Solo puede haber una opción correcta.");

                // Validar cada opción
                for (int i = 0; i < Options.Count; i++)
                {
                    if (!Options[i].IsValid(out var optionErrors))
                    {
                        errors.Add($"Opción {i + 1}: {string.Join(", ", optionErrors)}");
                    }
                }
            }

            return errors.Count == 0;
        }

        /// <summary>
        /// Verifica si la tarjeta está lista para usarse en modo quiz
        /// </summary>
        public bool IsQuizReady()
        {
            return Options != null
                   && Options.Count >= 2
                   && Options.Any(o => o.IsCorrect);
        }

        /// <summary>
        /// Verifica si la tarjeta está lista para usarse en modo flashcard
        /// </summary>
        public bool IsFlashcardReady()
        {
            return !string.IsNullOrWhiteSpace(QuestionText)
                   && !string.IsNullOrWhiteSpace(AnswerText);
        }

        /// <summary>
        /// Representación en string del objeto
        /// </summary>
        public override string ToString()
        {
            // ✅ CORREGIDO
            if (string.IsNullOrWhiteSpace(QuestionText))
                return $"[Card {Id}] (sin pregunta)";

            var preview = QuestionText.Length > 50
                ? QuestionText.Substring(0, 50) + "..."
                : QuestionText;

            return $"[Card {Id}] {preview}";
        }
    }
}