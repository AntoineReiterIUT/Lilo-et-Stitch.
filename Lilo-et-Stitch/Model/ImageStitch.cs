using Lilo_et_Stitch.Model.Transform;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lilo_et_Stitch.Model
{
    public class ImageStitch
    {
        #region attributes
        private Image prevImage;
        private Image newImage;
        private static Dictionary<string,RGB> usedColors = new Dictionary<string,RGB>();
        private bool changedPalette;
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

        /// <summary>
        /// Tells when the palet has been changed
        /// </summary>
        public bool ChangedPalette { get => changedPalette; set => changedPalette = value; }
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
        public (Image, Dictionary<RGB, String>) TransformImage()
        {
            ImgTransform transform = new ImgTransform(prevImage);
            List<Case> cases = new List<Case>();
            cases = transform.TransformingIMG(prevImage);
            List<RGB> listUsedColor = new List<RGB>();

            Bitmap bitNewImage = new Bitmap(prevImage);

            foreach(Case c in cases)
            {
                listUsedColor.Add(c.ColorDMC);
                for (int i = c.Coord1.X; i < c.Coord2.X; i++)
                {
                    for(int j= c.Coord1.Y; j < c.Coord2.Y; j++)
                    {
                        bitNewImage.SetPixel(i, j, Color.FromArgb(c.ColorDMC.R,c.ColorDMC.G,c.ColorDMC.B));
                    }
                }
            }

            return ((Image)bitNewImage,usedColors);
        }

        #endregion
    }
}
