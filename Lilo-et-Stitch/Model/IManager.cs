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
        /// Import the image to the model
        /// </summary>
        /// <param name="img"></param>
        public void Import(Image img);

        /// <summary>
        /// Will reset the palette to it's original form
        /// </summary>
        public void ResetPalette();

        /// <summary>
        /// Will update the palette with the given path
        /// </summary>
        /// <param name="path">Path to the new palette</param>
        public void UpdatePalette(string path);
        0
        /// <summary>
        /// Transforms the image and creates the grid on it
        /// </summary>
        /// <param name="step">Step of the "pixel/case"</param>
        /// <param name="colorMax">Max amount of color to be used </param>
        /// <param name="gridColor">Color of the grid</param>
        /// <returns></returns>
        /// <author>Maxime LAFFAYE</author>
        public void TransformImage(int step, int colorMax, System.Windows.Media.Color gridColor);

        /// <summary>
        /// Will create the PDF using the given path
        /// </summary>
        /// <param name="path">Path to save the file</param>
        /// <author>Maxime LAFFAYE</author>
        public void ExportPDF(string path);
    }
}
