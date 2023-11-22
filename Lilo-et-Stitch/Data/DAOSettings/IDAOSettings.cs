using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lilo_et_Stitch.Data.DAOSettings
{
    /// <summary>
    /// DAO interface for the settings
    /// </summary>
    public interface IDAOSettings
    {
        /// <summary>
        /// Save the transformation settings
        /// </summary>
        /// <param name="step">The used step</param>
        /// <param name="colorMax">The maximum number of colors used</param>
        /// <param name="colorGrid">The color of used the grid</param>
        /// <author>Antoine SAKKALIS</author>
        void SaveSettings(int step,int colorMax,Color colorGrid);
    }
}
