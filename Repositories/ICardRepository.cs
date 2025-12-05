using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareEngineeringQuizApp.Models;

namespace SoftwareEngineeringQuizApp.Repositories
{
    /// <summary>
    /// Interfaz para el acceso a datos de Cards
    /// </summary>
    public interface ICardRepository
    {
        /// <summary>
        /// Obtiene todas las tarjetas de un tema específico
        /// </summary>
        Task<List<Card>> GetCardsByTopicAsync(int topicId);

        /// <summary>
        /// Obtiene una tarjeta específica por su ID, incluyendo sus opciones
        /// </summary>
        Task<Card?> GetCardByIdAsync(int cardId);

        /// <summary>
        /// Obtiene todas las tarjetas disponibles
        /// </summary>
        Task<List<Card>> GetAllCardsAsync();

        /// <summary>
        /// Cuenta cuántas tarjetas tiene un tema
        /// </summary>
        Task<int> CountCardsByTopicAsync(int topicId);
    }
}