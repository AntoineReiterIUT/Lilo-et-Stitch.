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
        private Image image;
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
        /// Constructor of the class
        /// </summary>
        /// <param name="img">Image inserted by the user</param>
        /// <author>Antoine SAKKALIS</author>
        public ImageStitch(Image img)
        {
            image = img;
        }


        /// <summary>
        /// Will create the PDF using the given path
        /// </summary>
        /// <param name="path">Path to save the file</param>
        /// <param name="editedImage">New image</param>
        /// <author>Maxime LAFFAYE</author>
        public void SavePDF(string path, System.Drawing.Image editedImage)
        {
            ImgTransform transform = ImgTransform.Instance;
            transform.PDF(path,editedImage);
        }


        /// <summary>
        /// Transforms the image and rebuild the new image
        /// </summary>
        /// <param name="step">Step for each "case/pixel"</param>
        /// <param name="colorMax">Max amount of color to be used</param>
        /// <param name="gridColor">Color of the grid</param>
        /// <returns>New image | List of used colors</returns>
        /// <author>Maxime LAFFAYE</author>
        public (Image, Dictionary<string,RGB>) TransformImage(int step,int colorMax, System.Windows.Media.Color gridColor)
        {
            //Transform the image
            ImgTransform transform = ImgTransform.Instance;
            List<Case> cases = transform.TransformingIMG(prevImage,step,colorMax,gridColor);
            //Create the new image
            Bitmap bitNewImage = new Bitmap(prevImage);

            usedColors = transform.UsedColors;

            foreach (Case c in cases)
            {
                for (int i = c.Coord1.X; i < c.Coord2.X; i++) //Foreach pixel of the case's line
                {

                    for (int j= c.Coord1.Y; j < c.Coord2.Y; j++) //Foreach pixel of the case's column
                    {
                        if (c.Opacity > 0 ) //Setting the color of each pixel on the new image if he is not transparent
                            bitNewImage.SetPixel(i, j, Color.FromArgb(c.Opacity,c.ColorDMC.R,c.ColorDMC.G,c.ColorDMC.B));
                    }
                }

                //Creation of the grid (using the color from the parameters)
                for (int i = Math.Max(c.Coord1.X, 0); i < Math.Min(c.Coord2.X, bitNewImage.Width); i++) //Foreach pixel of the case's line
                {
                    //Draw the top line
                    bitNewImage.SetPixel(i, c.Coord1.Y, Color.FromArgb(gridColor.A,gridColor.R,gridColor.G,gridColor.B)); 
                }

                for (int j = Math.Max(c.Coord1.Y, 0); j < Math.Min(c.Coord2.Y, bitNewImage.Height); j++) //Foreach pixel of the case's column
                {
                    //Draw the left line
                    bitNewImage.SetPixel(c.Coord1.X, j, Color.FromArgb(gridColor.A, gridColor.R, gridColor.G, gridColor.B)); 
                }
            }
            return ((Image)bitNewImage, usedColors);
        }
        #endregion
    }
}
