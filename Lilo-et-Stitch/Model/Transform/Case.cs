using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lilo_et_Stitch.Model.Transform
{
    public class Case
    {
        //attributes
        private Coordinate coord1;
        private Coordinate coord2;

        private Color couleur;
        private List<Pixel> pixels;

        /// <summary>
        /// Property : read or edit the Coord1 value.
        /// </summary>
        public Coordinate Coord1 { get => coord1; set => coord1 = value; }
        /// <summary>
        /// Property : read or edit the Coord2 value.
        /// </summary>
        public Coordinate Coord2 { get => coord2; set => coord2 = value; }
        /// <summary>
        /// Property : read or edit the color value.
        /// </summary>
        public Color Couleur { get => couleur; set => couleur = value; }

        /// <summary>
        /// Constructor of the case
        /// </summary>
        /// <param name="c1">Coordinate 1</param>
        /// <param name="c2">Coordinate 2</param>
        public Case(Coordinate c1, Coordinate c2) 
        {
            // initialization
            pixels = new List<Pixel>();
            couleur = Color.FromArgb(0, 0, 0);

            this.coord1 = c1;
            this.coord2 = c2;
            
        }

    }
}
