using Lilo_et_Stitch.Model.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Lilo_et_Stitch.Model
{
    /// <summary>
    /// Class handling the main part of the model.
    /// </summary>
    public class MainStitch : IMainStitch
    {
        private ImageStitch _imageStitch;

        public void ChangePalette()
        {
            throw new NotImplementedException();
        }

        public void ImportImage(Image img)
        {
            throw new NotImplementedException();
        }

        public (Image, Dictionary<RGB, string>) TransformImage()
        {
            throw new NotImplementedException();
        }

        public MainStitch()
        {
            this._imageStitch = new ImageStitch();
        }
    }
}
