using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareEngineeringQuizApp.Models;

namespace SoftwareEngineeringQuizApp.Repositories
{
    /// <summary>
    /// Interfaz para el acceso a datos de Topics
    /// </summary>
    public interface ITopicRepository
    {
        /// <summary>
        /// Obtiene todos los temas disponibles
        /// </summary>
        Task<List<Topic>> GetAllTopicsAsync();

        /// <summary>
        /// Obtiene un tema específico por su ID
        /// </summary>
        Task<Topic?> GetTopicByIdAsync(int topicId);

        /// <summary>
        /// Verifica si existen temas en la base de datos
        /// </summary>
        Task<bool> HasTopicsAsync();
    }
}