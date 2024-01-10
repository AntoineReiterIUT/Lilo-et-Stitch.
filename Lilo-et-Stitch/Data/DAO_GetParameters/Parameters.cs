using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace Lilo_et_Stitch.Data.DAO_GetParameters
{
    /// <summary>
    /// Class representing the parameters of a pattern
    /// </summary>
    public class Parameters
    {
        #region attributes

        private int id;
        private int step;
        private int maxcolor;
        private string colorgrid = "";
        private string palettePath = "";

        private Color colorGridMedia;

        #endregion


        #region properties

        /// <summary>
        /// Property : read or edit the id of the pattern
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public int Id
        {
            get => id;
            set => id = value;
        }

        /// <summary>
        /// Property : read or edit the step of the pattern
        /// </summary>
        /// <author>Antoine REITER</author>
        public int Step
        {
            get => step;
            set => step = value;
        }

        /// <summary>
        /// Property : read or edit the number of color of the pattern
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public int NbColor
        {
            get => maxcolor;
            set => maxcolor = value;
        }

        /// <summary>
        /// Property : read or edit the color of the grid of the pattern
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public string GridColor
        {
            get => colorgrid;
            set => colorgrid = value;
        }

        /// <summary>
        /// Property : read or edit the path of the palette of the pattern
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public string ColorPalette
        {
            get => palettePath;
            set => palettePath = value;
        }

        /// <summary>
        /// Property : read or edit the color of the grid of the pattern
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public Color ColorGridMedia
        {
            get => colorGridMedia;
            set => colorGridMedia = value;
        }

        #endregion

    }
}
