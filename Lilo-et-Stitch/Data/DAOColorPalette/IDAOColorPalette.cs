using Aspose.Pdf.Facades;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lilo_et_Stitch.Data.DAOColorPalette
{
    /// <summary>
    /// This class will handle all the data access for the color palette
    /// </summary>
    public interface IDAOColorPalette
    {
        /// <summary>
        /// Change the palette used to transform the image
        /// </summary>
        /// <param name="csvPath">Path to the selected csv file</param>
        void ChangePalette(string csvPath);

        /// <summary>
        /// Reset the palette to the default DMC colors
        /// </summary>
        void ResetPalette();

        /// <summary>
        /// Gives the list of all the colors in the palette
        /// </summary>
        /// <returns>List of all the colors in the palette with their names</returns>
        Dictionary<string, Color> GetPalette();

        /// <summary>
        /// Get the path of where the palette is saved
        /// </summary>
        /// <returns>The path of where the palette is saved</returns>
        /// <author>Maxime LAFFAYE</author>
        public string GetPath();
    }
}
