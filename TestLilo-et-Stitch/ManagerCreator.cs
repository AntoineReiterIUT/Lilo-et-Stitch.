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
        /// Public property to access the manager (Manager)
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        public Manager Manager { get { return manager; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the class
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        public ManagerCreator()
        {
            manager = new Manager();
        }

        #endregion

        #region Methods

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
        /// <author>Antoine SAKKALIS</author>
        /// <returns>The newly generated image</returns>
        public GeneratedImage ManagerWithGeneratedImage(int step, int colorMax, System.Windows.Media.Color gridColor)
        {
            return manager.TransformImage(step, colorMax, gridColor);
        }

        #endregion
    }
}
