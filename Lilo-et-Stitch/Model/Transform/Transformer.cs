using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media;

namespace Lilo_et_Stitch.Model.Transform
{
    /// <summary>
    /// This class will handle all the transformation process for the image
    /// </summary>
    public class Transformer
    {
        #region attributes

        private List<System.Windows.Media.Color> colorsToUse = new List<System.Windows.Media.Color>();
        private Dictionary<string, System.Windows.Media.Color> colorPalette;
        private GeneratedImage newImage = new GeneratedImage();
        private Dictionary<System.Windows.Media.Color, int> colors = new Dictionary<System.Windows.Media.Color, int>();
        ColorTool colorTool = new ColorTool();

        #endregion

        #region operations

        /// <summary>
        /// Transforms the image and rebuilds the new image
        /// </summary>
        /// <param name="step">Step for each "case/pixel"</param>
        /// <param name="colorMax">Max amount of color to be used</param>
        /// <param name="gridColor">Color of the grid</param>
        /// <returns>The image that was just generated</returns>
        /// <author>Maxime LAFFAYE</author>
        public GeneratedImage TransformImage(int step, int colorMax, System.Windows.Media.Color gridColor, ImageStitch imageStitch, Dictionary<string, System.Windows.Media.Color> colorPalette)
        {
            this.colorPalette = colorPalette;

            newImage.Width = imageStitch.Width;
            newImage.Height = imageStitch.Height;
            newImage.Step = step;
            newImage.GridColor = gridColor;

            //Create the grid
            MakeGrid(newImage.Cases, step, imageStitch.Height, imageStitch.Width);
            //Then fill the pixels of the new image
            FillPixels(newImage.Cases, imageStitch, colorMax);

            //Return the new image
            return newImage;
        }


        /// <summary>
        /// Will fill the pixels of the new image
        /// </summary>
        /// <param name="cases">List of cases</param>
        /// <param name="img">The ImageStitch</param>
        /// <param name="generatedImage">The newly generated image</param>
        /// <author>Maxime LAFFAYE</author>
        public void FillPixels(List<Case> cases, ImageStitch img, int colorMax)
        {
            Bitmap bitNewImage = new Bitmap(img.Image);

            //For each case, calculate the average color
            foreach (Case c in cases)
                CaseAverage(c, bitNewImage, colorMax);

            //Then, select the most used colors to have the best result with less colors
            UpdateColorsToUse(colors, colorMax);

            //for each case, transform the color to the closest DMC color
            foreach (Case c in cases)
            {
                c.TransformedColor = colorTool.ToDMC(c.BaseColor, colorsToUse);

                //Get the DMC color code using the closest DMC color RGB value
                string closestDMCColorCode = colorPalette.FirstOrDefault(x => x.Value == c.TransformedColor).Key;

                //Register the used DMC color RGB value
                newImage.UsedColor[closestDMCColorCode] = c.TransformedColor;
            }

            //Rebuild the image
            RebuildImage(cases, bitNewImage, newImage);

            //Stocking as a bitmap and as an image
            newImage.Image = bitNewImage;
            newImage.Bitmap = bitNewImage;
        }


        /// <summary>
        /// Will find the avg color of the case given (using each pixel's color)
        /// </summary>
        /// <param name="caseToCheck">The case to calculate the average from</param>
        /// <param name="bitMap">The bitmap to get the colors from</param>
        /// <author>Maxime LAFFAYE</author>
        public void CaseAverage(Case caseToCheck, Bitmap bitMap, int maxColors)
        {
            int pixelCount = 0;
            int sumR = 0, sumG = 0, sumB = 0, sumOpacity = 0;

            //for each pixel of the case
            for (int i = caseToCheck.Coord1.X; i < caseToCheck.Coord2.X; i++)
            {
                for (int j = caseToCheck.Coord1.Y; j < caseToCheck.Coord2.Y; j++)
                {
                    //Get the color of the pixel
                    System.Drawing.Color DrawingColor = bitMap.GetPixel(i, j);
                    System.Windows.Media.Color color = System.Windows.Media.Color.FromArgb(DrawingColor.A, DrawingColor.R, DrawingColor.G, DrawingColor.B);

                    //Add the color to the sum
                    sumR += color.R;
                    sumG += color.G;
                    sumB += color.B;
                    sumOpacity += color.A;

                    pixelCount++;

                    //If the color is almost visible, add it to the list of colors
                    if (color.A >= 200)
                    {
                        if (colors.ContainsKey(color))
                            colors[color] = colors[color] + 1;
                        else
                            colors.Add(color, 1);
                    }
                }
            }

            //Calculate the average color of the case
            caseToCheck.BaseColor = colorTool.CalculateAverageColor(caseToCheck, sumR, sumG, sumB, sumOpacity, pixelCount);
        }


        /// <summary>
        /// Updates the list of colors to use
        /// </summary>
        /// <param name="colors">Dictionary of colors used and their number of occurrences</param>
        /// <param name="maxColors">Maximum number of colors to be used</param>
        /// <author>Antoine SAKKALIS</author>
        private void UpdateColorsToUse(Dictionary<System.Windows.Media.Color, int> colors, int maxColors)
        {
            //Sort the colors by the number of times they are used and put them in an array
            System.Windows.Media.Color[] sortedColors = colors
                .OrderByDescending(x => x.Value)
                .Select(x => x.Key)
                .ToArray();

            //Add the colors to the list of colors to use
            foreach (System.Windows.Media.Color color in sortedColors)
            {
                //If the color is not already in the list and the list is not full, add it
                if (colorsToUse.Count < maxColors && !colorsToUse.Contains(colorTool.ToDMC(color, colorPalette.Values.ToList())))
                {
                    colorsToUse.Add(colorTool.ToDMC(color, colorPalette.Values.ToList()));
                }
            }
        }


        /// <summary>
        /// Generates a box grid
        /// </summary>
        /// <param name="cases">List of cases</param>
        /// <param name="step">The step of the image (the higher it is, the lower the amount of cases will be)</param>
        /// <param name="height">Height of the image</param>
        /// <param name="width">Width of the image</param>
        /// <author>Mattis GALOPPIN - Maxime LAFFAYE</author>
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


        /// <summary>
        /// Will draw the new pixels on the bitmap
        /// </summary>
        /// <param name="cases">List of cases (AKA the new pixels of the generated image)</param>
        /// <param name="img">New bitMap image that is being built</param>
        /// <param name="generatedImage">The newly generated image</param>
        /// <author>Maxime LAFFAYE</author>
        private void RebuildImage(List<Case> cases, Bitmap img, GeneratedImage generatedImage)
        {
            foreach (Case c in cases)
            {
                //the pixels/cases
                for (int i = c.Coord1.X; i < c.Coord2.X; i++) //Foreach pixel of the case's line
                {
                    for (int j = c.Coord1.Y; j < c.Coord2.Y; j++) //Foreach pixel of the case's column
                    {
                        img.SetPixel(i, j, System.Drawing.Color.FromArgb(c.BaseColor.A, c.TransformedColor.R, c.TransformedColor.G, c.TransformedColor.B));
                    }
                }

                //Creation of the grid (using the color from the parameters)
                for (int i = Math.Max(c.Coord1.X, 0); i < Math.Min(c.Coord2.X, img.Width); i++) //Foreach pixel of the case's line
                {
                    //Draw the top line
                    img.SetPixel(i, c.Coord1.Y, System.Drawing.Color.FromArgb(generatedImage.GridColor.A, generatedImage.GridColor.R, generatedImage.GridColor.G, generatedImage.GridColor.B));
                }

                for (int j = Math.Max(c.Coord1.Y, 0); j < Math.Min(c.Coord2.Y, img.Height); j++) //Foreach pixel of the case's column
                {
                    //Draw the left line
                    img.SetPixel(c.Coord1.X, j, System.Drawing.Color.FromArgb(generatedImage.GridColor.A, generatedImage.GridColor.R, generatedImage.GridColor.G, generatedImage.GridColor.B));
                }
            }
        }

        #endregion
    }
}
