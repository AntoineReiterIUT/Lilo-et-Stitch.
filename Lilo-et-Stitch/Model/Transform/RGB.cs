using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lilo_et_Stitch.Model.Transform
{
    /// <summary>
    /// Represents a RGB color
    /// </summary>
    public class RGB
    {

        #region attributes
        private int r;
        private int g;
        private int b;
        #endregion


        #region properties
        /// <summary>
        /// Property : read or edit the Red value
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public int R
        {
            get { return r; }
            set {
                if (value < 0)
                {
                    r = 0;
                }else if (value > 255)
                {
                    r = 255;
                }
                else
                {
                    r = value;
                }
            }
        }


        /// <summary>
        /// Property : read or edit the Green value
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public int G
        {
            get { return g; }
            set
            {
                if (value < 0)
                {
                    g = 0;
                }
                else if (value > 255)
                {
                    g = 255;
                }
                else
                {
                    g = value;
                }
            }
        }


        /// <summary>
        /// Property : read or edit the Blue value.
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public int B
        {
            get { return b;}
            set {
                if (value < 0)
                {
                    b = 0;
                }
                else if (value > 255)
                {
                    b = 255;
                }
                else
                {
                    b = value;
                }
            }
        }
        #endregion


        #region operations
        /// <summary>
        /// Constructor of the RGB
        /// </summary>
        /// <param name="r">Red</param>
        /// <param name="g">Green</param>
        /// <param name="b">Blue</param>
        /// <author>Maxime LAFFAYE</author>
        public RGB(int r,int g,int b) 
        { 
            R = r;
            G = g;
            B = b;
        }


        /// <summary>
        /// Checks if two RGB color objects have the same values
        /// </summary>
        /// <param name="obj">The object to compare with the current RGB object</param>
        /// <returns> If the specified object is equal to the current RGB object</returns>
        /// <author>Maxime LAFFAYE</author>
        public override bool Equals(object? obj)
        {
            bool areEqual = false;

            if (obj is RGB other)
            {
                areEqual = r == other.r && g == other.g && b == other.b;
            }

            return areEqual;
        }


        /// <summary>
        /// Generate a hash code for the current object.
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        /// <returns>A hash code for the current object</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(R, G, B);
        }
        #endregion
    }
}
