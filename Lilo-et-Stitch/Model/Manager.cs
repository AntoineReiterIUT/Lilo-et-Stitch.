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
using Lilo_et_Stitch.Data.DAO_Publish;
using Lilo_et_Stitch.Data.DAO_Getdata;
using Lilo_et_Stitch.Data.DAO_GetParameters;

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
        private IDAO_PDF ?daoPDF;
        private IDAOImage daoImage = new DAOImage();
        private IDAO_Publish daoPublish = new DAO_Publish();
        DAO_Getdata dao_Getdata = new DAO_Getdata();
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
        
        ///<author>Maxime LAFFAYE</author>>
        public void ExportPDF(string path)
        {
            if (generatedImage != null)
            {
                daoPDF = new DAO_PDF();
                daoPDF.ExportPDF(path, generatedImage);
            }
            else
            {
                throw new Exception("Transformez une image avant d'exporter");
            }
        }

        ///<author>Maxime LAFFAYE</author>>
        public void Import(string path)
        {
            daoImage.Save(path);
        }

        ///<author>Maxime LAFFAYE</author>>
        public BitmapImage LoadBitmapImage()
        {
            return daoImage.LoadBitmapImage();
        }

        ///<author>Maxime LAFFAYE</author>>
        public Dictionary<int, Dictionary<string, BitmapImage>> GetImages(int page)
        {
            return dao_Getdata.GetImages(page);           
        }

        ///<author>Maxime LAFFAYE</author>>
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

        ///<author>Maxime LAFFAYE</author>>
        public void UpdatePalette(string path)
        {
            daoColorPalette.ChangePalette(path);
        }

        ///<author>Antoine SAKKALIS</author>>
        public void ResetPalette()
        {
            daoColorPalette.ResetPalette();
        }

        ///<author>Antoine SAKKALIS</author>>
        public Dictionary<string, System.Windows.Media.Color> GetPalette()
        {
            return daoColorPalette.GetPalette();
        }

        ///<author>Maxime LAFFAYE</author>>
        public async Task Publish(string name)
        {
            try
            {
                await daoPublish.CheckIfExist(name);
                daoPublish.Publish(name, generatedImage.Step, generatedImage.UsedColor.Count, generatedImage.GridColor, daoPDF.GetPath(), daoColorPalette.GetPath(), generatedImage.BitmapImage);

            }
            catch (Exception ex)
            {
                //Handle or log the exception as needed
                throw new Exception($"{ex.Message}");
            }
        }

        ///<author>Maxime LAFFAYE</author>>
        public async Task<Parameters> GetSettings(int id)
        {
            DAO_GetParameters dao_GetParameters = new DAO_GetParameters();
            return dao_GetParameters.GetParameters(id).Result;
        }

        ///<author>Maxime LAFFAYE</author>>
        public void GetPattern(string patternName, string path)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
