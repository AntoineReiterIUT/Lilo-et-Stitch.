using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lilo_et_Stitch.Model.Transform
{
    /// <summary>
    /// Represent a case in the 
    /// </summary>
    public class Case
    {
        //attributes
        private Coordinate coord1;
        private Coordinate coord2;

        private RGB colorRGB;
        private RGB colorDMC;
        private List<Pixel> pixels;

        /// <summary>
        /// Property : read or edit the Coord1 value.
        /// </summary>
        /// <authors>Mattis galoppin</authors>
        public Coordinate Coord1 { get => coord1; set => coord1 = value; }

        /// <summary>
        /// Property : read or edit the Coord2 value.
        /// </summary>
        ///<authors>Mattis galoppin</authors>
        public Coordinate Coord2 { get => coord2; set => coord2 = value; }

        /// <summary>
        /// Property : read or edit the color value.
        /// </summary>
        /// <authors>Mattis galoppin</authors>
        public RGB ColorRGB { get => colorRGB; set => colorRGB = value; }

        /// <summary>
        /// Property : read or edit the colorDMC value.
        /// </summary>
        /// <authors>Mattis galoppin</authors>
        public RGB ColorDMC { get => colorDMC; set => colorDMC = value; }

        /// <summary>
        /// Property : read or edit the list of Pixels
        /// </summary>
        public List<Pixel> Pixels
        {
            get => pixels;
            set => pixels = value;
        }

        /// <summary>
        /// Constructor of the case
        /// </summary>
        /// <param name="c1">Coordinate 1</param>
        /// <param name="c2">Coordinate 2</param>
        /// <authors>Mattis galoppin</authors>
        public Case(Coordinate c1, Coordinate c2) 
        {
            // initialization
            pixels = new List<Pixel>();
            RGB colorRGB = new RGB(0,0,0);
            RGB colorDMC = new RGB(0,0,0);

            this.coord1 = c1;
            this.coord2 = c2;
            
        }

    }
}
