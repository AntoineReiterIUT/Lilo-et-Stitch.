using Lilo_et_Stitch.Model.Transform;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Lilo_et_Stitch.Model
{
    /// <summary>
    /// This class will be used as a link with the mainStitch and the imgTransform.
    /// It will color the pixels.
    /// </summary>
    /// <author>MaximeLAFFAYE</author>
    public class ImageStitch
    {
        #region attributes
        private Image prevImage;
        private Image newImage;
        private Dictionary<string,RGB> usedColors = new Dictionary<string,RGB>();
        #endregion

        #region properties
        /// <summary>
        /// Image picked by the user
        /// </summary>
        public Image PrevImage { get => prevImage; set => prevImage = value; }

        /// <summary>
        /// Image transformed to show
        /// </summary>
        public Image NewImage { get => newImage; set => newImage = value; }
        #endregion

        #region operations
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="img">Image inserted by the user</param>
        public ImageStitch(Image img)
        {
            prevImage = img;
        }



        /// <summary>
        /// Transforms the image
        /// </summary>
        /// <param name="step">Step for each "case/pixel"</param>
        /// <param name="colorMax">Max amount of color to be used</param>
        /// <param name="gridColor">Color of the grid</param>
        /// <returns></returns>
        /// <author>MaximeLAFFAYE</author>
        public (Image, Dictionary<string,RGB>) TransformImage(int step,int colorMax, System.Windows.Media.Color gridColor)
        {
            ImgTransform transform = new ImgTransform(prevImage,step,colorMax);
            List<Case> cases = new List<Case>();
            cases = transform.TransformingIMG(prevImage);
            usedColors = transform.UsedColors;
            Bitmap bitNewImage = new Bitmap(prevImage);

            foreach (Case c in cases)
            {
                for (int i = c.Coord1.X; i < c.Coord2.X; i++)
                {

                    for (int j= c.Coord1.Y; j < c.Coord2.Y; j++)
                    {
                        //Setting the color of each pixel on the new image
                        bitNewImage.SetPixel(i, j, Color.FromArgb(c.ColorDMC.R,c.ColorDMC.G,c.ColorDMC.B));
                    }
                }

                //Creation of the grid (using the color from the parameters)
                for (int i = Math.Max(c.Coord1.X, 0); i <= Math.Min(c.Coord2.X, bitNewImage.Width - 1); i++)
                {
                    if (c.Coord1.Y >= 0 && c.Coord1.Y < bitNewImage.Height)
                    {
                        bitNewImage.SetPixel(i, c.Coord1.Y, Color.FromArgb(gridColor.A,gridColor.R,gridColor.G,gridColor.B));
                    }
                    if (c.Coord2.Y >= 0 && c.Coord2.Y < bitNewImage.Height)
                    {
                        bitNewImage.SetPixel(i, c.Coord2.Y, Color.FromArgb(gridColor.A, gridColor.R, gridColor.G, gridColor.B));
                    }
                }

                for (int j = Math.Max(c.Coord1.Y, 0); j <= Math.Min(c.Coord2.Y, bitNewImage.Height - 1); j++)
                {
                    if (c.Coord1.X >= 0 && c.Coord1.X < bitNewImage.Width)
                    {
                        bitNewImage.SetPixel(c.Coord1.X, j, Color.FromArgb(gridColor.A, gridColor.R, gridColor.G, gridColor.B));
                    }
                    if (c.Coord2.X >= 0 && c.Coord2.X < bitNewImage.Width)
                    {
                        bitNewImage.SetPixel(c.Coord2.X, j, Color.FromArgb(gridColor.A, gridColor.R, gridColor.G, gridColor.B));
                    }
                }


            }

            return ((Image)bitNewImage, usedColors);
        }
        #endregion
    }
}
