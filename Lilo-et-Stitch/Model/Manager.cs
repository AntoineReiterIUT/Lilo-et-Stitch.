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
        private IDAO_Getdata dao_Getdata = new DAO_Getdata();

        #endregion


        #region Properties

        /// <summary>
        /// Property : Read or edit the generated image
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public ImageStitch ImageStitch { get => imageStitch; set => imageStitch = value; }


        /// <summary>
        /// Property : Read or edit the generated image
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public GeneratedImage? GeneratedImage { get => generatedImage; set => generatedImage = value; }

        #endregion


        #region operations

        /// <summary>
        /// Import the path of the image to the model
        /// </summary>
        /// <param name="path">Path to the image</param>
        /// <author>Maxime LAFFAYE</author>>
        public void Import(string path)
        {
            //Import the image in the database
            daoImage.Save(path);
        }


        /// <summary>
        /// Will create the PDF using the given path
        /// </summary>
        /// <param name="path">Path to save the file</param>
        /// <exception cref="Exception">If the generatedImage doesn't exists</exception>
        /// <author>Maxime LAFFAYE</author>
        public void ExportPDF(string path)
        {
            if (generatedImage != null)
            {
                //Export the PDF
                daoPDF = new DAO_PDF();
                daoPDF.ExportPDF(path, generatedImage);
            }
            else //If the generatedImage doesn't exists
            {
                throw new Exception("Transformez une image avant d'exporter");
            }
        }


        /// <summary>
        /// Load a BitmapImage to display in the HMI
        /// </summary>
        /// <returns>The BitmapImage to display</returns>
        /// <author>Maxime LAFFAYE</author>
        public BitmapImage LoadBitmapImage()
        {
            return daoImage.LoadBitmapImage();
        }


        /// <summary>
        /// Will update the palette from the given path
        /// </summary>
        /// <param name="path">Path to the new palette</param>
        /// <author>Maxime LAFFAYE</author>
        public void UpdatePalette(string path)
        {
            //Update the palette in the database
            daoColorPalette.ChangePalette(path);
        }


        /// <summary>
        /// Will reset the palette to the default one from the app files
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        public void ResetPalette()
        {
            //Reset the palette
            daoColorPalette.ResetPalette();
        }


        /// <summary>
        /// Gives the list of all the colors in the palette
        /// </summary>
        /// <returns>The dictionary of all color names and their values</returns>
        /// <author>Antoine SAKKALIS</author>
        public Dictionary<string, System.Windows.Media.Color> GetPalette()
        {
            //Get the palette
            return daoColorPalette.GetPalette();
        }


        /// <summary>
        /// Transforms the image and creates the grid on it
        /// </summary>
        /// <param name="step">Step of the "pixel/case"</param>
        /// <param name="colorMax">Max amount of color to be used </param>
        /// <param name="gridColor">Color of the grid</param>
        /// <returns>The newly generated image</returns>
        /// <author>Maxime LAFFAYE</author>
        public GeneratedImage TransformImage(int step, int colorMax, System.Windows.Media.Color gridColor)
        {
            //Load the image before transforming
            this.imageStitch.Image = daoImage.LoadImage();

            // If an image was already imported
            if (this.imageStitch != null)
            {
                //Do the transformation
                generatedImage = imageStitch.TransformImage(step, colorMax, gridColor, daoColorPalette.GetPalette());

                //Add the image in it's bitmapImage form because it'll be needed within the View
                generatedImage.BitmapImage = daoImage.BitmapToBitmapImage(generatedImage.Bitmap);
            }

            //Return the newly generated image
            return generatedImage;
        }


        /// <summary>
        /// Publish the pattern to the database
        /// </summary>
        /// <param name="name">The name of the pattern to save</param>
        /// <exception cref="Exception">Exception returned if the user didn't follow the steps or if there's a server error (folder already existing...)</exception>
        /// <author>Maxime LAFFAYE</author>
        public async Task Publish(string name)
        {
            if (generatedImage == null)
            {
                throw new Exception("Transformez une image avant de la publier");
            }
            if (daoPDF == null)
            {
                throw new Exception("Exportez une image avant de la publier");
            }

            try
            {
                //Check if the pattern already exists
                await daoPublish.CheckIfExist(name);
                //If it doesn't exists (exception unraised), publish it
                await daoPublish.Publish(name, generatedImage.Step, generatedImage.UsedColor.Count, generatedImage.GridColor, daoPDF.GetPath(), daoColorPalette.GetPath(), generatedImage.BitmapImage);
            }
            catch (Exception ex)
            {
                //Handle or log the exception as needed
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Get the dictionary of up to 6 patterns
        /// </summary>
        /// <param name="page">The page to get the patterns from</param>
        /// <param name="patternName">The name of the pattern to search for</param>
        /// <returns>A dictionary with the ids, the names and the images themselves</returns>
        /// <author>Maxime LAFFAYE</author>
        public async Task<(int, Dictionary<int, Dictionary<string, BitmapImage>>)> GetImages(int page, string? patternName)
        {
            //Get the images from the database
            return await dao_Getdata.GetImages(page,patternName);
        }


        /// <summary>
        /// Get the parameters of the pattern
        /// </summary>
        /// <param name="id">The Id of the pattern</param>
        /// <returns>An instance of Parameters containing all the values</returns>
        ///<author>Maxime LAFFAYE</author>>
        public async Task<Parameters> GetSettings(int id)
        {
            //Get the parameters from the database
            DAO_GetParameters dao_GetParameters = new DAO_GetParameters();
            return await dao_GetParameters.GetParameters(id);
        }


        /// <summary>
        /// Download the pattern from the database
        /// </summary>
        /// <param name="patternName">The name of the pattern to download</param>
        /// <param name="path">The path where the folder with the files will be saved</param>
        ///<author>Maxime LAFFAYE</author>
        public void GetPattern(string patternName, string path)
        {
            //Get the pattern from the database
            dao_Getdata.SavePattern(patternName, path);
        }

        #endregion
    }
}
