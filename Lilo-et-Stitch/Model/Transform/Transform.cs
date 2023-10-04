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

            while (c2.Y < this.img.Height) //traversal of lines
            {
                c2.Y = c2.Y + this.step;

                while (c2.X < this.img.Width) //we go through the columns
                {
                    c2.X = c2.X + this.step;

                    if  (c2.X > this.img.Width)
                    {
                        //we do nothing
                    }
                    else
                    {
                        case1 = new Case(c1, c2); //put in the list of cases
                        c1.X = c2.X; //we shift point 1
                    }
                }
                c1.Y = c2.Y; //we shift point 1
            }
            
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
