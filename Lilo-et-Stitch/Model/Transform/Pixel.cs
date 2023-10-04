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
        private Coordinate coord;
        #endregion


        #region properties

        /// <summary>
        /// Property : read or edit the colorRGB
        /// </summary>
        public RGB ColorRGB { set { colorRGB = value; } get { return this.colorRGB; } }

        /// <summary>
        /// Property : read or edit the coordinate
        /// </summary>
        public Coordinate Coord { set { coord = value; } get { return this.coord; } }
        #endregion


        #region operations
        /// <summary>
        /// Constructor of the pixel
        /// </summary>
        /// <param name="colorRGB">R,G,B</param>
        /// <param name="coord">X,Y</param>
        /// <author>MaximeLAFFAYE</author>
        public Pixel(RGB colorRGB, Coordinate coord)
        {
            this.colorRGB = colorRGB;
            this.coord = coord;
        }
        #endregion
    }
}
