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
        private Dictionary<RGB, RGB> colorMatches;
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

        /// <summary>
        /// Stores the converting occurences to reduce calculations
        /// </summary>
        public Dictionary<RGB, RGB> ColorMatches { get { return colorMatches; } set { colorMatches = value; } }
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
            FillPixels();
            CaseAverage();
            ToDMC();
            return this.Cases;
        }

        /// <summary>
        /// generates a box grid
        /// </summary>
        /// <author>MattisGALOPPIN - MaximeLAFFAYE</author>
        public void MakeGrid()
        {
            for (int y = 0; y < this.img.Height; y += this.step)
            {
                for (int x = 0; x < this.img.Width; x += this.step)
                {
                    int rightX = Math.Min(x + this.step, this.img.Width);
                    int bottomY = Math.Min(y + this.step, this.img.Height);

                    Coordinate c1 = new Coordinate(x, y);
                    Coordinate c2 = new Coordinate(rightX, bottomY);

                    Case case1 = new Case(c1, c2);
                    cases.Add(case1);
                }
            }
        }



        /// <summary>
        /// Will create and add the pixels to each case
        /// </summary>
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
                        Pixel nvPixel = new Pixel(new RGB(color.R, color.G, color.B), new Coordinate(i, j));
                        c.Pixels.Add(nvPixel);
                    }
                }
            }
        }


        /// <summary>
        /// Will find the avg color of the case (using each pixel's color)
        /// </summary>
        /// <author>MaximeLAFFAYE</author>
        public void CaseAverage()
        {

            foreach (Case tempCase in Cases)
            {
                int sumR = 0;
                int sumG = 0;
                int sumB = 0;

                int avgR = 0;
                int avgG = 0;
                int avgB = 0;
                foreach (Pixel pixel in tempCase.Pixels)
                {
                    sumR += pixel.ColorRGB.R;
                    sumG += pixel.ColorRGB.G;
                    sumB += pixel.ColorRGB.B;
                }
                avgR = sumR / tempCase.Pixels.Count;
                avgG = sumG / tempCase.Pixels.Count;
                avgB = sumB / tempCase.Pixels.Count;

                tempCase.ColorRGB = new RGB(avgR, avgG, avgB);
            }
        }
        /// <summary>
        /// change a RGB color in a DMC color existing 
        /// </summary>
        /// <author>Antoine Sakkalis</author>
        public void ToDMC()
        {
            ColorPalette colorPalette = new ColorPalette();
            colorPalette.LoadColors();
            ImageStitch.UsedColors.Clear();

            foreach (Case box in cases)
            {
                int bestDistance = 766; // Initialize bestDistance here
                RGB closestDMCColorRGB = null; // Initialize closestDMCColorRGB

                if (colorMatches.ContainsKey(box.ColorRGB))
                {
                    // If the color is already in the dictionary, use the mapped DMC color RGB value
                    closestDMCColorRGB = colorMatches[box.ColorRGB];
                }
                else
                {
                    foreach (string code in colorPalette.DMCColors.Keys)
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

                    // Store the closest DMC color RGB value in the colorMatches dictionary
                    colorMatches[box.ColorRGB] = closestDMCColorRGB;
                }

                // Get the DMC color code using the closest DMC color RGB value
                string closestDMCColorCode = colorPalette.DMCColors.FirstOrDefault(x => x.Value == closestDMCColorRGB).Key;

                box.ColorDMC = closestDMCColorRGB; // Set the ColorDMC of the case
                ImageStitch.UsedColors[closestDMCColorCode] = closestDMCColorRGB; // Register the used DMC color RGB value
            }
        }


        /// <summary>
        /// contructor of Transform
        /// </summary>
        public ImgTransform(Image image)
        {
            //initialization
            this.cases = new List<Case>();
            this.step = 15;
            this.img = image;
            colorMatches = new();
        }

        #endregion
    }
}
