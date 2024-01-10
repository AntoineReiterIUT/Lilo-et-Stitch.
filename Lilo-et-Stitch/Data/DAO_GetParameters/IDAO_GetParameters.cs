using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lilo_et_Stitch.Data.DAO_GetParameters
{
    /// <summary>
    /// Interface for the DAO_GetParameters class
    /// </summary>
    internal interface IDAO_GetParameters
    {
        /// <summary>
        /// Get the parameters from the pattern
        /// </summary>
        /// <param name="id">The Id of the pattern</param>
        /// <returns>The parameters of the pattern</returns>
        /// <author>Antoine REITER - Maxime LAFFAYE</author>
        public Task<Parameters> GetParameters(int id);
    }
}
