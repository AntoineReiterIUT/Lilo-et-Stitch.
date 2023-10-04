using Lilo_et_Stitch.Model.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Lilo_et_Stitch.Model
{
    public class ImageStitch
    {
        #region attributes
        private Image prevImage;
        private Image newImage;
        private Dictionary<RGB,String> usedColors = new Dictionary<RGB,String>();
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
        public Dictionary<RGB, string> UsedColors { get => usedColors; set => usedColors = value; }

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
        public (Image,List<RGB>) TransformImage()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
