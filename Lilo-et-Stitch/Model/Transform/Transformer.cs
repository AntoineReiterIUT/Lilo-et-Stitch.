using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;

namespace Lilo_et_Stitch.Model.Transform
{
    ///<summary>
    ///This class will handle all the transformation process for the image
    ///</summary>
    public class Transformer
    {
        #region attributes

        private List<System.Windows.Media.Color> colorsToUse = new List<System.Windows.Media.Color>();
        private Dictionary<System.Windows.Media.Color, System.Windows.Media.Color> colorMatches = new Dictionary<System.Windows.Media.Color, System.Windows.Media.Color>();
        private Dictionary<string, System.Windows.Media.Color> colorPalette;
        private GeneratedImage newImage = new GeneratedImage();

        #endregion

        #region operations

        ///<summary>
        ///Transforms the image and rebuilds the new image
        ///</summary>
        ///<param name="step">Step for each "case/pixel"</param>
        ///<param name="colorMax">Max amount of color to be used</param>
        ///<param name="gridColor">Color of the grid</param>
        ///<returns>The image that was just generated</returns>
        ///<author>Maxime LAFFAYE</author>
        public GeneratedImage TransformImage(int step, int colorMax, System.Windows.Media.Color gridColor, ImageStitch imageStitch, Dictionary<string, System.Windows.Media.Color> colorPalette)
        {
            this.colorPalette = colorPalette;

            newImage.Width = imageStitch.Width;
            newImage.Height = imageStitch.Height;
            newImage.Step = step;
            newImage.GridColor = gridColor;

            //Creating the case, AKA the new pixels of our new image
            MakeGrid(newImage.Cases, step, imageStitch.Height, imageStitch.Width);
            FillPixels(newImage.Cases, imageStitch, colorMax);

            return newImage;
        }

        ///<summary>
        ///Will loop through all cases and get the average + the code DMC if necessary (if the opacity isn't 0)
        ///</summary>
        ///<param name="cases">List of cases</param>
        ///<param name="img">The ImageStitch</param>
        ///<param name="generatedImage">The newly generated image</param>
        ///<author>Maxime LAFFAYE</author>
        public void FillPixels(List<Case> cases, ImageStitch img, int colorMax)
        {
            Bitmap bitNewImage = new Bitmap(img.Image);
            foreach (Case c in cases)
                CaseAverage(c, bitNewImage);
            
            ColorAverage(cases, colorMax);

            foreach (Case c in cases)
                c.TransformedColor = ToDMC(c.BaseColor, true);

            RebuildImage(cases, bitNewImage, newImage);

            //Stocking as a bitmap and as an image
            newImage.Image = bitNewImage;
            newImage.Bitmap = bitNewImage;
        }

        ///<summary>
        ///Calculates the average color of a case
        ///</summary>
        ///<param name="cases">List of cases</param>
        ///<param name="maxColors">Maximum number of colors to be used</param>
        ///<author>Antoine SAKKALIS</author>
        public void ColorAverage(List<Case> cases, int maxColors)
        {
            Dictionary<System.Windows.Media.Color, int> colors = new Dictionary<System.Windows.Media.Color, int>();

            //Count the frequency of each color
            foreach (Case c in cases)
            {
                if (c.BaseColor.A >= 200)
                    if (colors.ContainsKey(c.BaseColor))
                        colors[c.BaseColor] = colors[c.BaseColor] + 1;
                    else
                        colors.Add(c.BaseColor, 1);
            }

            //Sort colors by frequency in descending order
            var sortedColors = colors.OrderByDescending(pair => pair.Value);

            List<System.Windows.Media.Color> colorCandidate = new List<System.Windows.Media.Color>();

            //Take the top maxColors colors
            foreach (var color in sortedColors)
            {
                colorCandidate.Add(color.Key);
            }

            //Use the colorsToUse list as needed
            foreach (System.Windows.Media.Color color in colorCandidate)
            {
                if (colorsToUse.Count < maxColors && !colorsToUse.Contains(ToDMC(color, false)))
                {
                    colorsToUse.Add(ToDMC(color, false));
                }
            }
        }

        ///<summary>
        ///Will draw the new pixels on the bitmap
        ///</summary>
        ///<param name="cases">List of cases (AKA the new pixels of the generated image)</param>
        ///<param name="img">New bitMap image that is being built</param>
        ///<param name="generatedImage">The newly generated image</param>
        ///<author>Maxime LAFFAYE</author>
        private void RebuildImage(List<Case> cases, Bitmap img, GeneratedImage generatedImage)
        {
            foreach (Case c in cases)
            {
                //the pixels/cases
                for (int i = c.Coord1.X; i < c.Coord2.X; i++) //Foreach pixel of the case's line
                {
                    for (int j = c.Coord1.Y; j < c.Coord2.Y; j++) //Foreach pixel of the case's column
                    {
                        img.SetPixel(i, j, Color.FromArgb(c.BaseColor.A, c.TransformedColor.R, c.TransformedColor.G, c.TransformedColor.B));
                    }
                }

                //Creation of the grid (using the color from the parameters)
                for (int i = Math.Max(c.Coord1.X, 0); i < Math.Min(c.Coord2.X, img.Width); i++) //Foreach pixel of the case's line
                {
                    //Draw the top line
                    img.SetPixel(i, c.Coord1.Y, Color.FromArgb(generatedImage.GridColor.A, generatedImage.GridColor.R, generatedImage.GridColor.G, generatedImage.GridColor.B));
                }

                for (int j = Math.Max(c.Coord1.Y, 0); j < Math.Min(c.Coord2.Y, img.Height); j++) //Foreach pixel of the case's column
                {
                    //Draw the left line
                    img.SetPixel(c.Coord1.X, j, Color.FromArgb(generatedImage.GridColor.A, generatedImage.GridColor.R, generatedImage.GridColor.G, generatedImage.GridColor.B));
                }
            }
        }

        ///<summary>
        ///Will find the avg color of the case given (using each pixel's color)
        ///</summary>
        ///<param name="caseToCheck">The case to calculate the average from</param>
        ///<param name="bitMap">The bitmap to get the colors from</param>
        ///<author>Maxime LAFFAYE</author>
        public void CaseAverage(Case caseToCheck, Bitmap bitMap)
        {
            int sumR = 0;
            int sumG = 0;
            int sumB = 0;
            int sumOpacity = 0;

            int avgR;
            int avgG;
            int avgB;
            int avgOpacity;

            //Average the colors and opacity of the cases
            int pixelCount = 0;
            for (int i = caseToCheck.Coord1.X; i < caseToCheck.Coord2.X; i++)
            {
                for (int j = caseToCheck.Coord1.Y; j < caseToCheck.Coord2.Y; j++)
                {
                    Color color = bitMap.GetPixel(i, j);
                    sumR += color.R;
                    sumG += color.G;
                    sumB += color.B;
                    sumOpacity += color.A;

                    pixelCount++;
                }
            }
            avgR = sumR / pixelCount;
            avgG = sumG / pixelCount;
            avgB = sumB / pixelCount;
            avgOpacity = sumOpacity / pixelCount;

            //Adds it to the case.BaseColor
            caseToCheck.BaseColor = System.Windows.Media.Color.FromArgb((byte)avgOpacity, (byte)avgR, (byte)avgG, (byte)avgB);
        }

        ///<summary>
        ///Generates a box grid
        ///</summary>
        ///<param name="cases">List of cases</param>
        ///<param name="step">The step of the image (the higher it is, the lower the amount of cases will be)</param>
        ///<param name="height">Height of the image</param>
        ///<param name="width">Width of the image</param>
        ///<author>Mattis GALOPPIN - Maxime LAFFAYE</author>
        public void MakeGrid(List<Case> cases, int step, int height, int width)
        {
            for (int y = 0; y < height; y += step) //for each line
            {
                for (int x = 0; x < width; x += step) //for each column
                {
                    //Create the case
                    int rightX = Math.Min(x + step, width);
                    int bottomY = Math.Min(y + step, height);

                    Coordinate c1 = new Coordinate(x, y);
                    Coordinate c2 = new Coordinate(rightX, bottomY);

                    Case case1 = new Case(c1, c2);
                    cases.Add(case1);
                }
            }
        }

        ///<summary>
        ///Change a RGB color into an existing DMC color from the color of a case
        ///</summary>
        ///<param name="colorCase">Case to calculate the DMC color</param>
        ///<param name="useColorsToUse">Flag to determine whether to use a restricted set of colors</param>
        ///<returns>The closest DMC color</returns>
        ///<author>Antoine SAKKALIS</author>
        public System.Windows.Media.Color ToDMC(System.Windows.Media.Color colorCase, bool useColorsToUse)
        {
            int bestDistance = 766;
            System.Windows.Media.Color closestDMCColor = System.Windows.Media.Color.FromArgb(0, 0, 0, 0);

            if (useColorsToUse)
            {
                closestDMCColor = colorsToUse[0];
            }

            if (colorMatches.ContainsKey(colorCase))
            {
                //If the color is already in the dictionary, use the mapped DMC color RGB value
                closestDMCColor = colorMatches[colorCase];
            }
            else
            {
                foreach (string code in colorPalette.Keys)
                {
                    if (useColorsToUse)
                    {
                        if (colorsToUse.Contains(colorPalette[code]))
                        {
                            //Calculate the color distance
                            System.Windows.Media.Color colorDMC = colorPalette[code];
                            int distance = Math.Abs(colorDMC.R - colorCase.R)
                                + Math.Abs(colorDMC.G - colorCase.G)
                                + Math.Abs(colorDMC.B - colorCase.B);

                            if (distance < bestDistance) //If the distance is better than the previous one, use this color
                            {
                                bestDistance = distance;
                                closestDMCColor = colorDMC;
                            }
                        }
                    }
                    else
                    {
                        //Calculate the color distance
                        System.Windows.Media.Color colorDMC = colorPalette[code];
                        int distance = Math.Abs(colorDMC.R - colorCase.R)
                            + Math.Abs(colorDMC.G - colorCase.G)
                            + Math.Abs(colorDMC.B - colorCase.B);

                        if (distance < bestDistance) //If the distance is better than the previous one, use this color
                        {
                            bestDistance = distance;
                            closestDMCColor = colorDMC;
                        }
                    }
                }

                //Store the closest DMC color RGB value in the colorMatches dictionary
                colorMatches[colorCase] = closestDMCColor;
            }

            //Get the DMC color code using the closest DMC color RGB value
            string closestDMCColorCode = colorPalette.FirstOrDefault(x => x.Value == closestDMCColor).Key;

            //Register the used DMC color RGB value
            newImage.UsedColor[closestDMCColorCode] = closestDMCColor;

            return closestDMCColor;
        }

        #endregion
    }
}
