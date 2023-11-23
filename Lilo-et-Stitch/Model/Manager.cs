using Lilo_et_Stitch.Data.DAOColorPalette;
using Lilo_et_Stitch.Data.DAO_PDF;
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
        private GeneratedImage? generatedImage;
        private IDAOColorPalette daoColorPalette = new DAOColorPalette();
        private IDAOImage daoImage = new DAOImage();
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
        public GeneratedImage? GeneratedImage { get => generatedImage; set => generatedImage = value; }

        #endregion


        #region operations
        public void ExportPDF(string path)
        {
            if (generatedImage != null)
            {
                IDAO_PDF daoPDF = new DAO_PDF();
                daoPDF.ExportPDF(path, generatedImage);
            }
            else
            {
                throw new Exception("Transformez une image avant d'exporter");
            }

        }


        public void Import(string path)
        {
            daoImage.Save(path);
        }

        public BitmapImage LoadBitmapImage()
        {
            return daoImage.LoadBitmapImage();
        }

        public GeneratedImage TransformImage(int step, int colorMax, System.Windows.Media.Color gridColor)
        {
            //Load the image before transforming
            this.imageStitch.Image = daoImage.LoadImage();

            // If an image was already imported
            if(this.imageStitch != null)
            {
                //Do the transformation
                generatedImage = imageStitch.TransformImage(step, colorMax, gridColor,daoColorPalette.GetPalette());

                //Add the image in it's bitmapImage form because it'll be needed within the View
                generatedImage.BitmapImage = daoImage.BitmapToBitmapImage(generatedImage.Bitmap);
            }

            //Return the newly generated image
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

        public List<GeneratedImage> GetImages(int page) { 
            throw new NotImplementedException();
        }

        public void Publish(string name)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, Dictionary<string, BitmapImage>> GetImages()
        {
            throw new NotImplementedException();
        }

        public List<int> GetParameters(int id)
        {
            throw new NotImplementedException();
        }

        public void GetPattern(string patternName, string path)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
