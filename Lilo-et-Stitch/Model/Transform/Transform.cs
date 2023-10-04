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
        private Image img;
        /// <summary>
        /// Property : read or edit the step value.
        /// </summary>
        public int Step { get { return step; } set {  step = value; } }
        /// <summary>
        /// Property : read or edit the list of cases value.
        /// </summary>
        public List<Case> Cases { get {  return cases; } set { cases = value; } }
        /// <summary>
        /// Property : read only the image value.
        /// </summary>
        public Image Img { get { return img; } }
        /// <summary>
        /// transforms the base image into a cross stitch
        /// </summary>
        /// <param name="img">Image of origin</param>
        /// <returns>return a list of new cases</returns>
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

            Case case1 = new Case(c1, c2);

            
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
        /// <summary>
        /// contructor of Transform
        /// </summary>
        public Transform(Image image)
        {
            //initialization
            this.cases = new List<Case>();
            this.step = 5;
            this.img = image;
        }
    }
}
