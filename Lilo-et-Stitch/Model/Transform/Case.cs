using System.Collections.Generic;

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

        private RGB colorRGB;
        private RGB colorDMC;
        private int opacity;
        private List<Pixel> pixels;
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
        public RGB ColorRGB { get => colorRGB; set => colorRGB = value; }

        /// <summary>
        /// Property : read or edit the colorDMC value.
        /// </summary>
        /// <authors>Mattis GALOPPIN</authors>
        public RGB ColorDMC { get => colorDMC; set => colorDMC = value; }

        /// <summary>
        /// Property : read or edit the opacity value.
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        public int Opacity { get => opacity; set => opacity = value; }

        /// <summary>
        /// Property : read or edit the list of Pixels
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public List<Pixel> Pixels
        {
            get => pixels;
            set => pixels = value;
        }
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
            pixels = new List<Pixel>();
            RGB colorRGB = new RGB(0, 0, 0);
            RGB colorDMC = new RGB(0, 0, 0);

            this.coord1 = c1;
            this.coord2 = c2;
        }
        #endregion
    }
}
