using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Lilo_et_Stitch.Model.Transform
{
    public class Transform
    {
        //attributes
        private int step;
        private List<Case> cases;
        /// <summary>
        /// Property : read or edit the step value.
        /// </summary>
        public int Step { get { return step; } set {  step = value; } }
        public List<Case> Cases { get {  return cases; } set { cases = value; } }
        /// <summary>
        /// transforms the base image into a cross stitch
        /// </summary>
        /// <param name="img">Image of origin</param>
        /// <returns></returns>
        public List<Case> TransformingIMG(Image img)
        {
            return new List<Case>();
        }

        /// <summary>
        /// generates a box grid
        /// </summary>
        public void MakeGrid()
        {
            Coordinate c1 = new Coordinate(0, 0);
            Coordinate c2 = new Coordinate(0, 0);
        }
        /// <summary>
        /// do the average of the color of all pixels in a case
        /// </summary>
        public void CaseAverage()
        {

        }
        /// <summary>
        /// change a RGB color in a DMC color existing
        /// </summary>
        public void ToDMC()
        {

        }
        public Transform()
        {
            this.cases = new List<Case>();
            this.step = 0;
        }
    }
}
