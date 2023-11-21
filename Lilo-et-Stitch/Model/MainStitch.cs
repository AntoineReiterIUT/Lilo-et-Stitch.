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
    /// Class handling the main part of the model.
    /// </summary>
    public class MainStitch : IManager
    {
        #region attributes
        private ImageStitch _imageStitch;
        private Image img;
        #endregion


        #region operations
        public (Image, Dictionary<string,RGB>) TransformImage(int step,int colorMax, System.Windows.Media.Color gridColor)
        {
            return this._imageStitch.TransformImage(step,colorMax, gridColor);
        }


        public void SavePDF(string path, System.Drawing.Image editedImage)
        {
            this._imageStitch.SavePDF(path, editedImage);
        }


        public MainStitch(Image img)
        {
            this.img = img;
            this._imageStitch = new ImageStitch(img);
        }
        #endregion
    }
}
