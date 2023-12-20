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
        /// <author>Antoine SAKKALIS</author>
        public void Import(string path);


        /// <summary>
        /// Load a BitmapImage to display in the IHM
        /// </summary>
        /// <returns>A BitmapImage</returns>
        /// <author>Maxime LAFFAYE</author>
        public BitmapImage LoadBitmapImage();


        /// <summary>
        /// Will reset the palette to it's original form
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
        /// Will update the palette with the given path
        /// </summary>
        /// <param name="path">Path to the new palette</param>
        /// <author>Antoine SAKKALIS</author>
        public void UpdatePalette(string path);


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
        /// Will create the PDF using the given path
        /// </summary>
        /// <param name="path">Path to save the file</param>
        /// <author>Maxime LAFFAYE</author>
        public void ExportPDF(string path);

        /// <summary>
        /// Publish the pattern to the database
        /// </summary>
        /// <param name="name">The name of the pattern to save</param>
        /// <author>Antoine SAKKALIS</author>
        public Task Publish(string name);

        /// <summary>
        /// Get the dictionary of up to 6 patterns
        /// </summary>
        /// <returns>A dictionary with the id, the name and the image itself</returns>
        /// <author>Antoine SAKKALIS</author>
        Task<(int, Dictionary<int, Dictionary<string, BitmapImage>>)> GetImages(int page,string? patternName);

        /// <summary>
        /// Get the parameters of the pattern
        /// </summary>
        /// <param name="id"></param>
        /// <returns>step,colorMax,colorGrid(hex)</returns>
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
