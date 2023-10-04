using Lilo_et_Stitch.Model.Transform;
using System.Collections.Generic;

namespace Lilo_et_Stitch.Model
{
    /// <summary>
    /// This class represents the color pallet of DMC
    /// </summary>
    public class ColorPalette
    {
        #region attributes
        private List<RGB> dmcColors;
        private Dictionary<RGB, RGB> colorMatches;
        #endregion

        #region properties
        /// <summary>
        /// Stores the DMCs colors in a list with their RGB values
        /// </summary>
        public List<RGB> DMCColors { get { return dmcColors; } }

        /// <summary>
        /// Stores the converting occurences to reduce calculations
        /// </summary>
        public Dictionary<RGB, RGB> ColorMatches { get { return colorMatches; } set { colorMatches = value; } }
        #endregion

        #region operations
        /// <summary>
        /// Loads the DMC colors in the list
        /// </summary>
        public void LoadColors()
        {

        }

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public ColorPalette()
        {
            
        }
        #endregion
    }
}