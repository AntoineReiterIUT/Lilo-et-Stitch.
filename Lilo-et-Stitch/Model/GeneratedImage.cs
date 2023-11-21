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
        #endregion

        #region 
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
        #endregion
    }
}
