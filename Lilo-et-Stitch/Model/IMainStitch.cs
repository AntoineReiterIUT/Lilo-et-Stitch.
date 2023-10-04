using Lilo_et_Stitch.Model.Transform;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <returns>Image : New cross image. | Dictionnary<RGB,string> : Used color  </returns>
        public (Image, Dictionary<RGB,string>) TransformImage();

        /// <summary>
        /// Will import an image to the model.
        /// </summary>
        /// <param name="img">The image to be imported.</param>
        public void ImportImage(Image img);

        /// <summary>
        /// Will update the palette
        /// </summary>
        public void ChangePalette();
    }
}
