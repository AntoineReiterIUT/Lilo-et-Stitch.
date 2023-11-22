using Lilo_et_Stitch.Model.Transform;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace Lilo_et_Stitch.Model
{
    /// <summary>
    /// This class represents the image that will be transformed later on
    /// </summary>
    public class ImageStitch
    {
        #region attributes
        private Image ?image;
        #endregion


        #region properties

        /// <summary>
        /// The original image
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        public Image Image { get => image; set => image = value; }


        /// <summary>
        /// Width of the original image
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public int Width
        {
            get => image.Width;
        }


        /// <summary>
        /// Height of the original image
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public int Height
        {
            get => image.Height;
        }

        #endregion


        #region operations

        /// <summary>
        /// Transforms the image and rebuild the new image
        /// </summary>
        /// <param name="step">Step for each "case/pixel"</param>
        /// <param name="colorMax">Max amount of color to be used</param>
        /// <param name="gridColor">Color of the grid</param>
        /// <returns>The newly generated image</returns>
        /// <author>Maxime LAFFAYE</author>
        public GeneratedImage TransformImage(int step,int colorMax, System.Windows.Media.Color gridColor, Dictionary<string, System.Windows.Media.Color> colorPalette)
        {
            return new Transformer().TransformImage(step, colorMax, gridColor,this,colorPalette);
        }
        #endregion
    }
}
