using Lilo_et_Stitch.Model.Transform;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;
using Lilo_et_Stitch.Data.DAO_GetParameters;

namespace Lilo_et_Stitch.Model
{
    /// <summary>
    /// Interface that will be used as a link between the view and the model.
    /// </summary>
    public interface IManager
    {
        /// <summary>
        /// Import the path of the image to the model
        /// </summary>
        /// <param name="path">Path to the image</param>
        /// <author>Maxime LAFFAYE</author>
        public void Import(string path);


        /// <summary>
        /// Will create the PDF using the given path
        /// </summary>
        /// <param name="path">Path to save the file</param>
        /// <exception cref="Exception">If the generatedImage doesn't exists</exception>
        /// <author>Maxime LAFFAYE</author>
        public void ExportPDF(string path);


        /// <summary>
        /// Load a BitmapImage to display in the HMI
        /// </summary>
        /// <returns>The BitmapImage to display</returns>
        /// <author>Maxime LAFFAYE</author>
        public BitmapImage LoadBitmapImage();


        /// <summary>
        /// Will update the palette from the given path
        /// </summary>
        /// <param name="path">Path to the new palette</param>
        /// <author>Maxime LAFFAYE</author>
        public void UpdatePalette(string path);


        /// <summary>
        /// Will reset the palette to the default one in the app files
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        public void ResetPalette();


        /// <summary>
        /// Gives the list of all the colors in the palette
        /// </summary>
        /// <returns>The dictionary of all color names and their values</returns>
        /// <author>Antoine SAKKALIS</author>
        public Dictionary<string, System.Windows.Media.Color> GetPalette();


        /// <summary>
        /// Transforms the image and creates the grid on it
        /// </summary>
        /// <param name="step">Step of the "pixel/case"</param>
        /// <param name="colorMax">Max amount of color to be used </param>
        /// <param name="gridColor">Color of the grid</param>
        /// <returns>The newly generated image</returns>
        /// <author>Maxime LAFFAYE</author>
        public GeneratedImage TransformImage(int step, int colorMax, System.Windows.Media.Color gridColor);


        /// <summary>
        /// Publish the pattern to the database
        /// </summary>
        /// <param name="name">The name of the pattern to save</param>
        /// <exception cref="Exception">Exception returned if the user didn't follow the steps or if there's a server error (folder already existing...)</exception>
        /// <author>Maxime LAFFAYE</author>
        public Task Publish(string name);


        /// <summary>
        /// Get the dictionary of up to 6 patterns
        /// </summary>
        /// <param name="page">The page to get the patterns from</param>
        /// <param name="patternName">The name of the pattern to search for</param>
        /// <returns>A dictionary with the ids, the names and the images themselves</returns>
        /// <author>Maxime LAFFAYE</author>
        Task<(int, Dictionary<int, Dictionary<string, BitmapImage>>)> GetImages(int page,string? patternName);


        /// <summary>
        /// Get the parameters of the pattern
        /// </summary>
        /// <param name="id">The Id of the pattern</param>
        /// <returns>An instance of Parameters containing all the values</returns>
        /// <author>Antoine SAKKALIS</author>
        public Task<Parameters> GetSettings(int id);


        /// <summary>
        /// Download the pattern from the database
        /// </summary>
        /// <param name="patternName">The name of the pattern to download</param>
        /// <param name="path">The path where the folder with the files will be saved</param>
        /// <author>Antoine SAKKALIS</author>
        public void GetPattern(string patternName,string path);
    }
}
