using GrainBroker.Domain.Models;
using GrainBroker.Services.Models;

namespace GrainBroker.Services.Interfaces
{
    public interface IImportService
    {

        /// <summary>
        /// Imports a CSV containing mulitple purchase orders 
        /// </summary>
        /// <param name="stream">The CSV stream</param>
        /// <param name="fileName">The name of the csv file</param>
        /// <returns>How many orders have been imported</returns>
        Task<int> ImportPurchaseOrderCsv(StreamReader stream, string fileName);

        /// <summary>
        /// Inserts a new import
        /// </summary>
        /// <param name="import">The import to insert</param>
        /// <returns>The inserted import</returns>
        Task<Import> Insert(Import import);

        /// <summary>
        /// Gets all imports ordered by latest to oldest
        /// </summary>
        /// <returns>all imports</returns>
        Task<IEnumerable<Import>> GetAll();
    }
}