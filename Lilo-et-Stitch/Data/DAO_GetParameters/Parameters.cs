using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace Lilo_et_Stitch.Data.DAO_GetParameters
{
    public class Parameters
    {
        private int id;
        private int step;
        private int maxcolor;
        private string colorgrid = "";
        private string palettePath = "";

        private Color colorGridMedia;


        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int Step 
        { 
          get { return step; }
          set { step = value; }
        }
        public int NbColor 
        {
            get { return maxcolor; }
            set { maxcolor = value; } 
        }
        public string GridColor 
        { 
            get { return colorgrid; }
            set { colorgrid = value ; }
        }

        public string ColorPalette { get => palettePath; set => palettePath = value; }

        public Color ColorGridMedia
        {
            get { return colorGridMedia;}
            set {  colorGridMedia = value ; }
        }

        
    }
}
