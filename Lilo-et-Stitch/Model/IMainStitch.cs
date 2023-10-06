using Lilo_et_Stitch.Model.Transform;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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
        public (Image, Dictionary<string,RGB>) TransformImage();
    }
}
