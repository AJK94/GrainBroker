using GrainBroker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainBroker.Domain.Repository
{
    public interface IRepository<T>
    {    
        /// <summary>
        /// Inserts a new object of type T into the database 
        /// </summary>
        /// <param name="toInsert">The new object of type T</param>
        void Insert(T toInsert);

        /// <summary>
        /// Gets all objects of type T from the database
        /// </summary>
        /// <returns>An IEnumerable of objects of type T</returns>
        IEnumerable<T> GetAll();
    }
}