using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lilo_et_Stitch.Model.Transform
{
    /// <summary>
    /// Represents a Pixel
    /// </summary>
    public class Pixel
    {

        #region attributes
        private RGB colorRGB;
        private int opacity;
        private Coordinate coord;
        #endregion


        #region properties
        /// <summary>
        /// Property : read or edit the colorRGB
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public RGB ColorRGB { set { colorRGB = value; } get { return this.colorRGB; } }


        /// <summary>
        /// Property : read or edit the opacity
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        public int Opacity { get => opacity; set => opacity = value; }


        /// <summary>
        /// Property : read or edit the coordinate
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public Coordinate Coord { set { coord = value; } get { return this.coord; } }
        #endregion


        #region operations
        /// <summary>
        /// Constructor of the pixel
        /// </summary>
        /// <param name="colorRGB">R,G,B</param>
        /// <param name="coord">X,Y</param>
        /// <author>Maxime LAFFAYE</author>
        public Pixel(RGB colorRGB, Coordinate coord, int opacity)
        {
            this.colorRGB = colorRGB;
            this.coord = coord;
            this.opacity = opacity;
        }
        #endregion
    }
}
