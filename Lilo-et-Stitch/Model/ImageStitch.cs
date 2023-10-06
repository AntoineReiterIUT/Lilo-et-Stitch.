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
        private static Dictionary<string,RGB> usedColors = new Dictionary<string,RGB>();
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

        /// <summary>
        /// Colors used to make the transformed image with his DMC code
        /// </summary>
        public static Dictionary<string, RGB> UsedColors { get => usedColors; set => usedColors = value; }
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
        /// Transforms the image of the user
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public (Image, Dictionary<string,RGB>) TransformImage()
        {
            ImgTransform transform = new ImgTransform(prevImage);
            List<Case> cases = new List<Case>();
            cases = transform.TransformingIMG(prevImage);
            List<RGB> listUsedColor = new List<RGB>();

            Bitmap bitNewImage = new Bitmap(prevImage);

            foreach (Case c in cases)
            {
                //0,0
                //5,5

                listUsedColor.Add(c.ColorDMC);
                for (int i = c.Coord1.X; i < c.Coord2.X; i++)
                {

                    for (int j= c.Coord1.Y; j < c.Coord2.Y; j++)
                    {
                        bitNewImage.SetPixel(i, j, Color.FromArgb(c.ColorDMC.R,c.ColorDMC.G,c.ColorDMC.B));

                    }
                }

                //DarkGrid
                for (int i = Math.Max(c.Coord1.X, 0); i <= Math.Min(c.Coord2.X, bitNewImage.Width - 1); i++)
                {
                    if (c.Coord1.Y >= 0 && c.Coord1.Y < bitNewImage.Height)
                    {
                        bitNewImage.SetPixel(i, c.Coord1.Y, Color.FromArgb(0, 0, 0));
                    }
                    if (c.Coord2.Y >= 0 && c.Coord2.Y < bitNewImage.Height)
                    {
                        bitNewImage.SetPixel(i, c.Coord2.Y, Color.FromArgb(0, 0, 0));
                    }
                }

                for (int j = Math.Max(c.Coord1.Y, 0); j <= Math.Min(c.Coord2.Y, bitNewImage.Height - 1); j++)
                {
                    if (c.Coord1.X >= 0 && c.Coord1.X < bitNewImage.Width)
                    {
                        bitNewImage.SetPixel(c.Coord1.X, j, Color.FromArgb(0, 0, 0));
                    }
                    if (c.Coord2.X >= 0 && c.Coord2.X < bitNewImage.Width)
                    {
                        bitNewImage.SetPixel(c.Coord2.X, j, Color.FromArgb(0, 0, 0));
                    }
                }


            }

            return ((Image)bitNewImage,usedColors);
        }

        #endregion
    }
}
