using Lilo_et_Stitch.Model.Transform;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace Lilo_et_Stitch.Model
{
    /// <summary>
    /// Iterface that will be used as a link between the view and the model.
    /// </summary>
    interface IManager
    {

        /// <summary>
        /// Import the path of the image to the model
        /// </summary>
        /// <param name="path">Path to the image</param>
        public void Import(string path);


        /// <summary>
        /// Load a BitmapImage to display in the IHM
        /// </summary>
        /// <returns>A BitmapImage</returns>
        /// <author>Maxime LAFFAYE</author>
        public BitmapImage LoadBitmapImage();


        /// <summary>
        /// Will reset the palette to it's original form
        /// </summary>
        public void ResetPalette();

        /// <summary>
        /// Gives the list of all the colors in the palette
        /// </summary>
        /// <returns>The Dictionnary of all color names and their values</returns>
        public Dictionary<string, System.Windows.Media.Color> GetPalette();

        /// <summary>
        /// Will update the palette with the given path
        /// </summary>
        /// <param name="path">Path to the new palette</param>
        public void UpdatePalette(string path);


        /// <summary>
        /// Transforms the image and creates the grid on it
        /// </summary>
        /// <param name="step">Step of the "pixel/case"</param>
        /// <param name="colorMax">Max amount of color to be used </param>
        /// <param name="gridColor">Color of the grid</param>
        /// <returns>The newly generated image</returns>
        /// <author>Maxime LAFFAYE</author>
        public GeneratedImage TransformImage(int step, int colorMax, System.Windows.Media.Color gridColor);


        /// <summary>
        /// Will create the PDF using the given path
        /// </summary>
        /// <param name="path">Path to save the file</param>
        /// <author>Maxime LAFFAYE</author>
        public void ExportPDF(string path);
    }
}
