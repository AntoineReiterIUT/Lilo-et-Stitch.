using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Lilo_et_Stitch.Data.DAO_Publish
{
    /// <summary>
    /// Interface for the DAO_Publish class
    /// </summary>
    public interface IDAO_Publish
    {

        /// <summary>
        /// Will check if the name of the pattern doesn't exist already
        /// </summary>
        /// <param name="name">Name of the directory to be checked</param>
        /// <returns>Result of the request</returns>
        /// <author>Maxime LAFFAYE</author>
        public Task CheckIfExist(string name);


        /// <summary>
        /// Will publish the PDF
        /// </summary>
        /// <param name="name">Name of the pattern</param>
        /// <param name="step">Step for the cases</param>
        /// <param name="maxColors">Max amount of colors to be used</param>
        /// <param name="gridColor">Color of the grid</param>
        /// <param name="pathPdf">Path to the pdf</param>
        /// <param name="pathPalette">Path to the used palette</param>
        /// <param name="transformedImage">The transformedImage as a bitmapImage</param>
        /// <returns>Result of the request</returns>
        /// <author>Maxime LAFFAYE</author>
        public Task Publish(string name, int step, int maxColors, System.Windows.Media.Color gridColor, string pathPdf, string pathPalette, BitmapImage transformedImage);

    }
}
