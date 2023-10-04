using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lilo_et_Stitch.Model.Transform
{
    /// <summary>
    /// This class represents a Coordinate.
    /// </summary>
    public class Coordinate
    {
        private int x;
        private int y;

        /// <summary>
        /// Property : read or edit the X value.
        /// </summary>
        public int X { get { return x; } set {  x = value; } }

        /// <summary>
        /// Property : read or edit the Y value.
        /// </summary>
        public int Y { get { return y; } set { y = value; } }


        /// <summary>
        /// Constructor of the coordinate.
        /// </summary>
        /// <param name="x">X of the coordinate.</param>
        /// <param name="y">Y of the coordinate.</param>
        /// <author>MaximeLAFFAYE</author>
        public Coordinate(int x, int y) 
        { 
            this.x = x;
            this.y = y;
        }
    }
}
