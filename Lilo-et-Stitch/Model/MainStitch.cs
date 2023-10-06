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
        private ImageStitch _imageStitch;
        private Image img;

        public (Image, Dictionary<string,RGB>) TransformImage()
        {
            return this._imageStitch.TransformImage();
        }

        public MainStitch(Image img)
        {
            this.img = img;
            this._imageStitch = new ImageStitch(img);
        }
    }
}
