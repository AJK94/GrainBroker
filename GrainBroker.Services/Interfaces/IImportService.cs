using GrainBroker.Services.Models;

namespace GrainBroker.Services.Interfaces
{
    public interface IImportService
    {
        /// <summary>
        /// Imports a CSV containing mulitple purchase orders 
        /// </summary>
        /// <param name="stream">The CSV stream</param>
        /// <returns>How many orders  </returns>
        Task<int> ImportPurchaseOrderCsv(StreamReader stream);
    }
}