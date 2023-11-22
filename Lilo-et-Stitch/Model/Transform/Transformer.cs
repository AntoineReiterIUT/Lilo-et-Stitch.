using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;


namespace Lilo_et_Stitch.Model.Transform
{
    /// <summary>
    /// This class will handle all the transformation process for the image
    /// </summary>
    public class Transformer
    {
        #region attributes
        private Dictionary<System.Windows.Media.Color, System.Windows.Media.Color> colorMatches = new Dictionary<System.Windows.Media.Color, System.Windows.Media.Color>();
        private Dictionary<string, System.Windows.Media.Color> colorPalette;
        private int colorsStep;
        private GeneratedImage newImage = new GeneratedImage();
        #endregion


        #region operations

        /// <summary>
        /// Transforms the image and rebuild the new image
        /// </summary>
        /// <param name="step">Step for each "case/pixel"</param>
        /// <param name="colorMax">Max amount of color to be used</param>
        /// <param name="gridColor">Color of the grid</param>
        /// <returns>The image that was just generated</returns>
        /// <author>Maxime LAFFAYE</author>
        public GeneratedImage TransformImage(int step, int colorMax, System.Windows.Media.Color gridColor, ImageStitch imageStitch,Dictionary<string, System.Windows.Media.Color> colorPalette)
        {
            this.colorPalette = colorPalette;
            this.colorsStep = colorPalette.Count / colorMax;

            newImage.Width = imageStitch.Width;
            newImage.Height = imageStitch.Height;
            newImage.Step = step;
            newImage.GridColor = gridColor;


            // Creating the case, AKA the new pixels of our new image
            MakeGrid(newImage.Cases,step,imageStitch.Height,imageStitch.Width);
            FillPixels(newImage.Cases, imageStitch);

            return newImage;
        }


        /// <summary>
        /// Will loop through all cases and get the average + the code DMC if necessary (if the opacity isn't 0)
        /// </summary>
        /// <param name="cases">List of cases</param>
        /// <param name="img">The ImageStitch</param>
        /// <param name="generatedImage">The newly generated image</param>
        /// /// <author>Maxime LAFFAYE</author>
        public void FillPixels(List<Case> cases, ImageStitch img)
        {
            Bitmap bitNewImage = new Bitmap(img.Image);
            foreach (Case c in cases)
            {            
                CaseAverage(c, bitNewImage);    
                ToDMC(c);              
            }
            RebuildImage(cases,bitNewImage, newImage);

            //Stocking as a bitmap and as an image
            newImage.Image = bitNewImage;
            newImage.Bitmap = bitNewImage;
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
                // the pixels/cases
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


        /// <summary>
        /// Will find the avg color of the case given (using each pixel's color)
        /// </summary>
        /// <param name="caseToCheck">The case to calculate the average from</param>
        /// <param name="bitMap">The bitmap to get the colors from</param>
        /// <author>Maxime LAFFAYE</author>
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


        /// <summary>
        /// Generates a box grid
        /// </summary>
        /// <param name="cases">List of cases</param>
        /// <param name="step">The step of the image (the higher it is, the lower the amount of cases will be )</param>
        /// <param name="height"></param>
        /// <param name="width"></param>
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
        /// Change a RGB color in an existing DMC color from the color of a case
        /// </summary>
        /// <param name="box">Case to calculate the DMC color</param>
        /// <author>Antoine SAKKALIS</author>
        public void ToDMC(Case box)
        {
            int bestDistance = 766;
            System.Windows.Media.Color closestDMCColor = System.Windows.Media.Color.FromArgb(0,0,0,0);
            int steps = 1;

            if (colorMatches.ContainsKey(box.BaseColor))
            {
                //If the color is already in the dictionary, use the mapped DMC color RGB value
                closestDMCColor = colorMatches[box.BaseColor];
            }
            else
            {
                foreach (string code in colorPalette.Keys)
                {
                    if (steps % colorsStep == 0) //Only check every X colors (to respect the maxColors parameter)
                    {
                        //Calculate the color distance
                        System.Windows.Media.Color colorDMC = colorPalette[code];
                        int distance = Math.Abs(colorDMC.R - box.BaseColor.R)
                            + Math.Abs(colorDMC.G - box.BaseColor.G)
                            + Math.Abs(colorDMC.B - box.BaseColor.B);

                        if (distance < bestDistance) //If the distance is better than the previous one, use this color
                        {
                            bestDistance = distance;
                            closestDMCColor = colorDMC;
                        }
                    }
                    steps++;
                }

                //Store the closest DMC color RGB value in the colorMatches dictionary
                colorMatches[box.BaseColor] = closestDMCColor;
            }

            //Get the DMC color code using the closest DMC color RGB value
            string closestDMCColorCode = colorPalette.FirstOrDefault(x => x.Value == closestDMCColor).Key;

            //Set the ColorDMC of the case
            box.TransformedColor = closestDMCColor;
            //Register the used DMC color RGB value
            newImage.UsedColor[closestDMCColorCode] = closestDMCColor;
        }
        


        #endregion
    }
}
