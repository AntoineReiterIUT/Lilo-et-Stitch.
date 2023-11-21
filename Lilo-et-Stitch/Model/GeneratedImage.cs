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
        private List<Case> cases;
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
        public List<Case> Cases { get => cases; set => cases = value; }
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
