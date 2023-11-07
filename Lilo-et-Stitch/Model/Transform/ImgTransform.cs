using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Documents;
using Lilo_et_Stitch.Model;


namespace Lilo_et_Stitch.Model.Transform
{
    public class ImgTransform
    {
        #region attributes
        private Dictionary<string, RGB> usedColors = new Dictionary<string, RGB>();
        private int step = 0;
        private int colorsStep;
        private List<Case> cases;
        private Image img;
        private Dictionary<RGB, RGB> colorMatches;
        private ColorPalette colorPalette;
        #endregion

        #region properties
        /// <summary>
        /// Property : read or edit the step value.
        /// </summary>
        /// <authors>Mattis galoppin</authors>
        public int Step { get { return step; } set {  step = value; } }


        /// <summary>
        /// Property : read or edit the list of cases value.
        /// </summary>
        /// <authors>Mattis galoppin</authors>
        public List<Case> Cases { get {  return cases; } set { cases = value; } }


        /// <summary>
        /// Property : read only the image value.
        /// </summary>
        /// <authors>Mattis galoppin</authors>
        public Image Img { get { return img; } }
        #endregion
        /// <summary>
        /// Stores the converting occurences to reduce calculations
        /// </summary>
        public Dictionary<RGB, RGB> ColorMatches { get { return colorMatches; } set { colorMatches = value; } }


        /// <summary>
        /// Property : read or edit the Dictionary of usedColors
        /// </summary>
        public Dictionary<string, RGB> UsedColors { get => usedColors; set => usedColors = value; }
        #region operations


        /// <summary>
        /// transforms the base image into a cross stitch
        /// </summary>
        /// <param name="img">Image of origin</param>
        /// <returns>return a list of new cases</returns>
        /// <author>MaximeLAFFAYE</author>
        public List<Case> TransformingIMG(Image img)
        {
            //appel de la methode choice step si le step n'a pas de valeur positive
            if (this.step <= 0)
                { ChoiceStep(); }
                MakeGrid();
                FillPixels();
            ToPDF pdf = new ToPDF(this);
                return this.Cases;
        }


        /// <summary>
        /// generates a box grid
        /// </summary>
        /// <author>MattisGALOPPIN - MaximeLAFFAYE</author>
        public void MakeGrid()
        {
            for (int y = 0; y < this.img.Height; y += this.Step)
            {
                for (int x = 0; x < this.img.Width; x += this.Step)
                {
                    int rightX = Math.Min(x + this.Step, this.img.Width);
                    int bottomY = Math.Min(y + this.Step, this.img.Height);

                    Coordinate c1 = new Coordinate(x, y);
                    Coordinate c2 = new Coordinate(rightX, bottomY);

                    Case case1 = new Case(c1, c2);
                    cases.Add(case1);
                }
            }
        }


        /// <summary>
        /// Will create and add the pixels to each case after coloring each of them
        /// </summary>
        /// <author>MaximeLAFFAYE</author>
        public void FillPixels()
        {
            Bitmap bitNewImage = new Bitmap(img);
            foreach (Case c in this.cases)
            {
                
                for (int i = c.Coord1.X; i < c.Coord2.X; i++)
                {
                    for (int j = c.Coord1.Y; j < c.Coord2.Y; j++)
                    {
                        Color color = bitNewImage.GetPixel(i, j);
                        Pixel nvPixel = new Pixel(new RGB(color.R, color.G, color.B), new Coordinate(i, j),color.A);
                        c.Pixels.Add(nvPixel);
                    }
                }
                CaseAverage(c);
                ToDMC(c);
            }
        }


        /// <summary>
        /// Will find the avg color of the case given (using each pixel's color)
        /// </summary>
        /// <param name="caseToCheck"></param>
        /// <author>MaximeLAFFAYE</author>
        public void CaseAverage(Case caseToCheck)
        {
            int sumR = 0;
            int sumG = 0;
            int sumB = 0;
            int sumOpacity = 0;

            int avgR = 0;
            int avgG = 0;
            int avgB = 0;
            int avgOpacity = 0;

            //counting each color within the pixels
            foreach (Pixel pixel in caseToCheck.Pixels)
            {
                sumR += pixel.ColorRGB.R;
                sumG += pixel.ColorRGB.G;
                sumB += pixel.ColorRGB.B;
                sumOpacity += pixel.Opacity;
            }
            avgR = sumR / caseToCheck.Pixels.Count;
            avgG = sumG / caseToCheck.Pixels.Count;
            avgB = sumB / caseToCheck.Pixels.Count;
            avgOpacity = sumOpacity / caseToCheck.Pixels.Count;

            caseToCheck.ColorRGB = new RGB(avgR, avgG, avgB);
            caseToCheck.Opacity = avgOpacity;
        }


        /// <summary>
        /// change a RGB color in an existing DMC color
        /// </summary>
        /// <author>Antoine Sakkalis</author>
        public void ToDMC(Case box)
        {
            int bestDistance = 766; // Initialize bestDistance here
            RGB closestDMCColorRGB = null; // Initialize closestDMCColorRGB
            int steps = 0;

            if (colorMatches.ContainsKey(box.ColorRGB))
            {
                // If the color is already in the dictionary, use the mapped DMC color RGB value
                closestDMCColorRGB = colorMatches[box.ColorRGB];
            }
            else
            {                
                foreach (string code in colorPalette.DMCColors.Keys)
                {
                    //Verify if steps is divisible by colorsStep
                    if (steps % colorsStep == 0)
                    {
                        // Calculate the color distance
                        RGB colorDMC = colorPalette.DMCColors[code];
                        int distance = Math.Abs(colorDMC.R - box.ColorRGB.R)
                            + Math.Abs(colorDMC.G - box.ColorRGB.G)
                            + Math.Abs(colorDMC.B - box.ColorRGB.B);

                        if (distance < bestDistance)
                        {
                            bestDistance = distance;
                            closestDMCColorRGB = colorDMC;
                        }
                    }
                    steps++;
                }

                // Store the closest DMC color RGB value in the colorMatches dictionary
                colorMatches[box.ColorRGB] = closestDMCColorRGB;
            }

            // Get the DMC color code using the closest DMC color RGB value
            string closestDMCColorCode = colorPalette.DMCColors.FirstOrDefault(x => x.Value == closestDMCColorRGB).Key;
            if (closestDMCColorCode == null)
            {
                closestDMCColorCode = "";
            }
                

            box.ColorDMC = closestDMCColorRGB; // Set the ColorDMC of the case
            usedColors[closestDMCColorCode] = closestDMCColorRGB; // Register the used DMC color RGB value
        }


        /// <summary>
        /// choose the step according to the length and width of the image
        /// </summary>
        /// <authors>Mattis galoppin</authors>
        public void ChoiceStep()
        {

            int largeur = this.img.Width;
            int longeur = this.img.Height;
            int res = 0;
            this.step = 15; //initialization to 15

            
        }

            /*
            List<int> diviseurs = new List<int>(); //list of common divisors

            for (int i = 5; i < 21; i++) // we try to find a common divisor between 5 and 20
            {
                res = largeur % i;

                if (res == 0) // if width is divisible
                {
                    res = longeur % i;

                    if (res == 0) //if length is also divible
                    {
                        diviseurs.Add(i); //put in the commun list
                    }
                }
            }


            if (diviseurs.Count() != 0) //if  if the list is not empty
            {
                int mediane = diviseurs.Count() / 2;
                this.step = diviseurs[mediane];
            }

        }
            */

        /// <summary>
        /// contructor of Transform
        /// </summary>
        public ImgTransform(Image image,int step,int maxColors)
        {
            //Loading the palette
            colorPalette = new ColorPalette();
            colorPalette.LoadColors();

            //initialization
            this.cases = new List<Case>();
            this.img = image;
            this.step = step;
            this.colorsStep = colorPalette.DMCColors.Count / maxColors;
            colorMatches = new();

        }
        #endregion
    }

}
