using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Lilo_et_Stitch.Data.PDF;

namespace Lilo_et_Stitch.Model.Transform
{
    /// <summary>
    /// This class will handle all the transformation process for the image
    /// </summary>
    public class ImgTransform
    {
        #region attributes
        private Dictionary<string, RGB> usedColors;
        private int step = 0;
        private int colorsStep;
        private List<Case> cases;
        private Image? img;
        private Dictionary<RGB, RGB> colorMatches;
        private ColorPalette? colorPalette;
        private System.Windows.Media.Color gridColor;
        private static ImgTransform? instance;
        private System.Drawing.Image newImage;
        #endregion


        #region properties
        /// <summary>
        /// Instance of our transformer
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public static ImgTransform Instance
        {
            get
            {
                if (instance == null) instance = new ImgTransform();
                return instance;
            }
        }


        /// <summary>
        /// Property : read or edit the step value
        /// </summary>
        /// <authors>Mattis GALOPPIN</authors>
        public int Step { get { return step; } set { step = value; } }


        /// <summary>
        /// Property : Read or edit the colorstep value
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public int ColorStep
        {
            get => colorsStep;
            set
            {
                if (colorPalette == null)
                {
                    colorPalette = new ColorPalette();
                    colorPalette.LoadColors();
                }
                this.colorsStep = colorPalette.DMCColors.Count / value;
            }
        }


        /// <summary>
        /// Property : read or edit the list of cases value.
        /// </summary>
        /// <authors>Mattis GALOPPIN</authors>
        public List<Case> Cases { get { return cases; } set { cases = value; } }


        /// <summary>
        /// Property : read only the image value.
        /// </summary>
        /// <authors>Mattis GALOPPIN</authors>
        public Image? Img
        {
            get { return img; }
            set { img = value; }
        }


        /// <summary>
        /// Stores the converting occurences to reduce calculations
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        public Dictionary<RGB, RGB> ColorMatches { get { return colorMatches; } set { colorMatches = value; } }


        /// <summary>
        /// Property : read or edit the Dictionary of usedColors
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public Dictionary<string, RGB> UsedColors { get => usedColors; set => usedColors = value; }


        /// <summary>
        /// Property : read or edit the color of the grid
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public System.Windows.Media.Color GridColor { get => gridColor; set => gridColor = value; }


        /// <summary>
        /// The new Image
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public System.Drawing.Image NewImage { get => newImage; set => newImage = value; }
        #endregion


        #region operations
        /// <summary>
        /// Will create the PDF using the given path
        /// </summary>
        /// <param name="path">Path to save the file</param>
        /// <param name="editedImage">New image</param>
        /// <author>Maxime LAFFAYE</author>
        public void PDF(string path, System.Drawing.Image editedImage)
        {
            this.NewImage = editedImage;
            new PDF_DAO(this, path);
        }


        /// <summary>
        /// Init the dictionaries/lists
        /// </summary>
        ///<author>Maxime LAFFAYE</author>
        private void InitValues()
        {
            this.colorMatches = new();
            this.cases = new List<Case>();
            this.usedColors = new Dictionary<string, RGB>();
        }


        /// <summary>
        /// Transforms the base image into a cross stitch
        /// </summary>
        /// <param name="img">Image of origin</param>
        /// <param name="gridColor">Color of the grid</param>
        /// <param name="image">Image to transform</param>
        /// <param name="maxColors">Maximum number of colors</param>
        /// <param name="step">Step of the grid</param>
        /// <returns>return a list of new cases</returns>
        /// <author>Maxime LAFFAYE</author>
        public List<Case> TransformingIMG(Image image, int step, int maxColors, System.Windows.Media.Color gridColor)
        {
            //Load the palette
            colorPalette = new ColorPalette();
            colorPalette.LoadColors();

            //Changing the class parameters
            this.img = image;
            this.step = step;
            this.colorsStep = colorPalette.DMCColors.Count / maxColors;
            this.GridColor = gridColor;
            this.InitValues();

            MakeGrid();
            FillPixels();

            return this.Cases;
        }


        /// <summary>
        /// Generates a box grid
        /// </summary>
        /// <author>Mattis GALOPPIN - Maxime LAFFAYE</author>
        public void MakeGrid()
        {
            for (int y = 0; y < this.img.Height; y += this.Step) //for each line
            {
                for (int x = 0; x < this.img.Width; x += this.Step) //for each column
                {
                    //Create the case
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
        /// <author>Maxime LAFFAYE</author>
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
                        Pixel nvPixel = new Pixel(new RGB(color.R, color.G, color.B), new Coordinate(i, j), color.A);
                        c.Pixels.Add(nvPixel);
                    }
                }
                CaseAverage(c);
                if (c.Opacity > 0)
                    ToDMC(c);
            }
        }


        /// <summary>
        /// Will find the avg color of the case given (using each pixel's color)
        /// </summary>
        /// <param name="caseToCheck">The case to calculate the average from</param>
        /// <author>Maxime LAFFAYE</author>
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

            //Average the colors and opacity of the cases
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
        /// Change a RGB color in an existing DMC color from the color of a case
        /// </summary>
        /// <param name="box">Case to calculate the DMC color</param>
        /// <author>Antoine SAKKALIS</author>
        public void ToDMC(Case box)
        {
            int bestDistance = 766;
            RGB closestDMCColorRGB = new(0, 0, 0);
            int steps = 1;

            if (colorMatches.ContainsKey(box.ColorRGB))
            {
                //If the color is already in the dictionary, use the mapped DMC color RGB value
                closestDMCColorRGB = colorMatches[box.ColorRGB];
            }
            else
            {
                foreach (string code in colorPalette.DMCColors.Keys)
                {
                    if (steps % colorsStep == 0) //Only check every X colors (to respect the maxColors parameter)
                    {
                        //Calculate the color distance
                        RGB colorDMC = colorPalette.DMCColors[code];
                        int distance = Math.Abs(colorDMC.R - box.ColorRGB.R)
                            + Math.Abs(colorDMC.G - box.ColorRGB.G)
                            + Math.Abs(colorDMC.B - box.ColorRGB.B);

                        if (distance < bestDistance) //If the distance is better than the previous one, use this color
                        {
                            bestDistance = distance;
                            closestDMCColorRGB = colorDMC;
                        }
                    }
                    steps++;
                }

                //Store the closest DMC color RGB value in the colorMatches dictionary
                colorMatches[box.ColorRGB] = closestDMCColorRGB;
            }

            //Get the DMC color code using the closest DMC color RGB value
            string closestDMCColorCode = colorPalette.DMCColors.FirstOrDefault(x => x.Value == closestDMCColorRGB).Key;

            //Set the ColorDMC of the case
            box.ColorDMC = closestDMCColorRGB;
            //Register the used DMC color RGB value
            usedColors[closestDMCColorCode] = closestDMCColorRGB; 
        }
        #endregion
    }
}
