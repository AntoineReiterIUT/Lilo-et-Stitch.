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
        private string nom;
        private int step;
        private int maxcolor;
        private Color colorgrid;

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        public int Step 
        { 
          get { return step; }
          set { step = value; }
        }
        public int Maxcolor 
        {
            get { return maxcolor; }
            set { maxcolor = value; } 
        }
        public Color Colorgrid 
        { 
            get { return colorgrid; }
            set { colorgrid = value; }
        }
    }
}
