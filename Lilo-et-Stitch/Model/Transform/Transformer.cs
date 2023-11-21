using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Lilo_et_Stitch.Data.PDF;

namespace Lilo_et_Stitch.Model.Transform
{
    /// <summary>
    /// This class will handle all the transformation process for the image
    /// </summary>
    public class Transformer
    {
        #region attributes

        #endregion



        #region operations

        /// <summary>
        /// Transforms the image and rebuild the new image
        /// </summary>
        /// <param name="step">Step for each "case/pixel"</param>
        /// <param name="colorMax">Max amount of color to be used</param>
        /// <param name="gridColor">Color of the grid</param>
        /// <returns>The image that was just generated</returns>
        /// <author>Maxime LAFFAYE</author>
        public GeneratedImage TransformImage(int step, int colorMax, System.Windows.Media.Color gridColor)
        {
            GeneratedImage newImg = new GeneratedImage();



            return newImg;

        }


        #endregion
    }
}
