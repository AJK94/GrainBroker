using GrainBroker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainBroker.Domain.Repository
{
    public interface ILocationRepository : IRepository<Location>
    {      

        /// <summary>
        /// Gets the object of type T with the location name of the param
        /// </summary>
        /// <param name="id">The id of the object</param>
        /// <returns>The object or null if none exists</returns>
        Task<Location?> GetByLocation(string name);
    }
}