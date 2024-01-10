using System.Collections.Generic;
using System.Windows.Media;

namespace Lilo_et_Stitch.Model.Transform
{
    /// <summary>
    /// Represent a case in the image
    /// </summary>
    public class Case
    {
        #region attributes

        private Coordinate coord1;
        private Coordinate coord2;

        private Color baseColor;
        private Color transformedColor;

        #endregion


        #region properties

        /// <summary>
        /// Property : read or edit the Coord1 value.
        /// </summary>
        /// <authors>Mattis GALOPPIN</authors>
        public Coordinate Coord1 { get => coord1; set => coord1 = value; }

        /// <summary>
        /// Property : read or edit the Coord2 value.
        /// </summary>
        ///<authors>Mattis GALOPPIN</authors>
        public Coordinate Coord2 { get => coord2; set => coord2 = value; }

        /// <summary>
        /// Property : read or edit the color value.
        /// </summary>
        /// <authors>Mattis GALOPPIN</authors>
        public Color BaseColor { get => baseColor; set => baseColor = value; }

        /// <summary>
        /// Property : read or edit the colorDMC value.
        /// </summary>
        /// <authors>Mattis GALOPPIN</authors>
        public Color TransformedColor { get => transformedColor; set => transformedColor = value; }

        #endregion


        #region operations

        /// <summary>
        /// Constructor of the case
        /// </summary>
        /// <param name="c1">Coordinate 1</param>
        /// <param name="c2">Coordinate 2</param>
        /// <authors>Mattis GALOPPIN</authors>
        public Case(Coordinate c1, Coordinate c2)
        {
            //Initialization
            baseColor = Color.FromRgb(0,0,0);
            transformedColor = Color.FromRgb(0,0,0);

            this.coord1 = c1;
            this.coord2 = c2;
        }

        #endregion
    }
}
