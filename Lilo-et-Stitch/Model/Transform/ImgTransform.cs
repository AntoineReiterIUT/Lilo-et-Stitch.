using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lilo_et_Stitch.Model.Transform
{
    public class ImgTransform
    {
        #region attributes
        private int step;
        private List<Case> cases;
        private Image img;
        #endregion


        #region properties
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
        #endregion


        #region operations

        /// <summary>
        /// transforms the base image into a cross stitch
        /// </summary>
        /// <param name="img">Image of origin</param>
        /// <returns>return a list of new cases</returns>
        /// <author>MaximeLAFFAYE</author>
        public List<Case> TransformingIMG(Image img)
        {
            MakeGrid();
            CaseAverage();
            ToDMC();
            return this.Cases;
        }

        /// <summary>
        /// generates a box grid
        /// </summary>
        public void MakeGrid()
        {
            Coordinate c1 = new Coordinate(0, 0);
            Coordinate c2 = new Coordinate(0, 0);

            Case case1 = new Case(c1, c2);

            while (c2.Y < this.img.Height) //we go through the columns
            {
                c2.Y = c2.Y + this.step;

                //we reset the coordinates of the line
                c1.X = 0; 
                c2.X = 0;

                while (c2.X < this.img.Width) //we go through  the lines
                {
                    c2.X = c2.X + this.step;

                    if  (c2.X > this.img.Width)
                    {
                        //we do nothing and we go out
                    }
                    else
                    {
                        case1 = new Case(c1, c2); //we create the case
                        cases.Add(case1); //put in the list of cases
                        c1.X = c2.X; //we shift point 1
                    }
                }
                c1.Y = c2.Y; //we shift point 1
            }
            
        }
        /// <summary>
        /// Will find the avg color of the case (using each pixel's color)
        /// </summary>
        /// <author>MaximeLAFFAYE</author>
        public void CaseAverage()
        {
            int sumR = 0;
            int sumG = 0;
            int sumB = 0;

            int avgR = 0;
            int avgG = 0;
            int avgB = 0;

            foreach (Case tempCase in Cases)
            {
                foreach (Pixel pixel in tempCase.Pixels)
                {
                    sumR += pixel.ColorRGB.R;
                    sumG += pixel.ColorRGB.G;
                    sumB += pixel.ColorRGB.B;
                }
                avgR = sumR / tempCase.Pixels.Count;
                avgG = sumG / tempCase.Pixels.Count;
                avgB = sumB / tempCase.Pixels.Count;

                tempCase.ColorRGB = new RGB(sumR, sumG, sumB);
            }
        }
        /// <summary>
        /// change a RGB color in a DMC color existing 
        /// </summary>
        public void ToDMC()
        {
            ColorPalette colorPalette = new ColorPalette();
            colorPalette.LoadColors();
            int sum;
            int bestsum = 765;
            string bestColor = "White";

           foreach (Case box in cases)
            {
                foreach (string code in colorPalette.DMCColors.Keys) //Trier les couleurs DMC ?
                {
                    RGB colorDMC = colorPalette.DMCColors[code];
                    sum = Math.Abs(colorDMC.R - box.ColorRGB.R);
                    sum += Math.Abs(colorDMC.G - box.ColorRGB.G);
                    sum += Math.Abs(colorDMC.B - box.ColorRGB.B);

                    if (sum < bestsum)
                    {
                        bestsum = sum;
                        bestColor = code;
                    }
                }
                box.ColorDMC = colorPalette.DMCColors[bestColor];

                //ImageStitch.UsedColors[colorPalette.DMCColors[bestColor]] = bestColor;
            }
        }
        /// <summary>
        /// contructor of Transform
        /// </summary>
        public ImgTransform(Image image)
        {
            //initialization
            this.cases = new List<Case>();
            this.step = 5;
            this.img = image;
        }

        #endregion
    }
}
