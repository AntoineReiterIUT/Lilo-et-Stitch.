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
    interface IMainStitch
    {
        /// <summary>
        /// Will transform the image,
        /// </summary>
        /// <param name="step">Step of the "pixel/case"</param>
        /// <param name="colorMax">Max amount of color to be used </param>
        /// <param name="gridColor">Color of the grid</param>
        /// <returns>The transformed image | list of used colors</returns>
        /// <author>Maxime LAFFAYE</author>
        public (Image, Dictionary<string, RGB>) TransformImage(int step, int colorMax, System.Windows.Media.Color gridColor);


        /// <summary>
        /// Will create the PDF using the given path
        /// </summary>
        /// <param name="path">Path to save the file</param>
        /// <param name="editedImage">New image</param>
        /// <author>Maxime LAFFAYE</author>
        public void SavePDF(string path, System.Drawing.Image editedImage);
    }
}
