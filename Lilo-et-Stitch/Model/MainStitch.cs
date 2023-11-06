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
    /// Class handling the main part of the model.
    /// </summary>
    public class MainStitch : IMainStitch
    {
        #region attributes
        private ImageStitch _imageStitch;
        private Image img;
        #endregion

        #region operations
        public (Image, Dictionary<string,RGB>) TransformImage(int step,int colorMax)
        {
            return this._imageStitch.TransformImage(step,colorMax);
        }

        public MainStitch(Image img)
        {
            this.img = img;
            this._imageStitch = new ImageStitch(img);
        }
        #endregion
    }
}
