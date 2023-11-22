using Lilo_et_Stitch.Model.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lilo_et_Stitch.Model
{
    /// <summary>
    /// This class will represent the image that was transformed
    /// </summary>
    public class GeneratedImage
    {
        #region attributes
        private Dictionary<string, System.Windows.Media.Color> usedColor;
        private System.Windows.Media.Color gridColor;
        private int step;
        private List<Case> cases;
        private int width;
        private int height;
        #endregion


        #region  properties
        /// <summary>
        /// Property : Read or edit the list of used color
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public Dictionary<string, Color> UsedColor { get => usedColor; set => usedColor = value; }

        /// <summary>
        /// Property : Read or edit the grid's color
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public Color GridColor { get => gridColor; set => gridColor = value; }

        /// <summary>
        /// Property : Read or edit the list of cases
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public List<Case> Cases { get => cases; set => cases = value; }

        /// <summary>
        /// Property : Read or edit the step
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public int Step { get => step; set => step = value; }

        /// <summary>
        /// Width of the generated image
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public int Width { get => width; set => width = value; }

        /// <summary>
        /// Height of the generated image
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public int Height { get => height; set => height = value; }
        #endregion


        #region operations

        /// <summary>
        /// Constructor of the class. (Initialize the list + dictionnary
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public GeneratedImage()
        { 
            this.cases = new List<Case>();  
            this.usedColor = new Dictionary<string, Color>();
        }


        #endregion

    }
}
