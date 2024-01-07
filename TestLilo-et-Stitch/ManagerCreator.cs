using Lilo_et_Stitch.Model;


namespace TestLilo_et_Stitch
{
    /// <summary>
    /// Class responsible for creating the manager (Manager).
    /// </summary>
    internal class ManagerCreator
    {
        #region Attributes

        private Manager manager;
        private string projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

        #endregion

        #region Properties

        /// <summary>
        /// Property : gets the manager
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        public Manager Manager { get { return manager; } }

        #endregion

        #region Operations

        /// <summary>
        /// Initializes a new instance of the class
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        public ManagerCreator()
        {
            //Create a new manager
            manager = new Manager();
        }


        /// <summary>
        /// Creates a manager with a specified image.
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        public void ManagerWithImage()
        {
            //Path of the image file to import it
            string file = Path.Combine(projectFolder, @"../TestImages/ImageTest.png");
            manager.Import(file);
        }


        /// <summary>
        /// Creates a manager with a new palette specified.
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        public void ManagerWithNewPalette()
        {
            //Path of the file of the palette to load it
            string file = Path.Combine(projectFolder, @"../Data/InUsePalette.csv");
            manager.UpdatePalette(file);
        }


        /// <summary>
        /// Creates a manager with a generated image using the specified parameters
        /// </summary>
        /// <param name="step">The transformation step</param>
        /// <param name="colorMax">The maximum number of colors</param>
        /// <param name="gridColor">The color of the grid</param>
        /// <returns>The newly generated image</returns>
        /// <author>Antoine SAKKALIS</author>
        public GeneratedImage ManagerWithGeneratedImage(int step, int colorMax, System.Windows.Media.Color gridColor)
        {
            ManagerWithNewPalette();
            return manager.TransformImage(step, colorMax, gridColor);
        }

        #endregion
    }
}
