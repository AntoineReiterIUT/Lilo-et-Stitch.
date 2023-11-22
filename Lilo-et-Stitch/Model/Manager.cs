using Lilo_et_Stitch.Data.DAOColorPalette;
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
    /// Class handling the main part of the model " The Manager "
    /// </summary>
    public class Manager : IManager
    {
        #region attributes
        private ImageStitch imageStitch = new ImageStitch();
        private GeneratedImage ?generatedImage;
        private IDAOColorPalette daoColorPalette = new DAOColorPalette();
        private IDAO_Image daoImage = new DAOImage();
        #endregion


        #region Properties

        /// <summary>
        /// Property : read or edit the generated image
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public ImageStitch ImageStitch { get => imageStitch; set => imageStitch = value; }


        /// <summary>
        /// Property : read or edit the generated image
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public GeneratedImage ?GeneratedImage { get => generatedImage; set => generatedImage = value; }

        #endregion


        #region operations
        public void ExportPDF(string path)
        {
            throw new NotImplementedException();
        }


        public void Import(Image img)
        {
            daoImage.Save(img);
        }


        public GeneratedImage TransformImage(int step, int colorMax, System.Windows.Media.Color gridColor)
        {
            this.imageStitch.Image = daoImage.Load();

            if(this.imageStitch != null)
            {
                generatedImage = imageStitch.TransformImage(step, colorMax, gridColor,daoColorPalette.GetPalette());
            }

            return generatedImage;
        }

        public void UpdatePalette(string path)
        {
            daoColorPalette.ChangePalette(path);
        }

        public void ResetPalette()
        {
            daoColorPalette.ResetPalette();
        }


        public Dictionary<string, System.Windows.Media.Color> GetPalette()
        {
            return daoColorPalette.GetPalette();
        }

        #endregion
    }
}
