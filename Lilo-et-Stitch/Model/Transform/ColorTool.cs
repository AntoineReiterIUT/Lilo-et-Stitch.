using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lilo_et_Stitch.Model.Transform
{
    /// <summary>
    /// Class with tools related to colors
    /// </summary>
    internal class ColorTool
    {
        #region attributes

        private Dictionary<System.Windows.Media.Color, System.Windows.Media.Color> colorMatches = new Dictionary<System.Windows.Media.Color, System.Windows.Media.Color>();

        #endregion


        #region operations

        /// <summary>
        /// Change an ARGB color into an existing DMC color from the color of a case
        /// </summary>
        /// <param name="colorCase">Color of the case to convert</param>
        /// <param name="useColorsToUse">List of colors to use for the DMC conversion</param>
        /// <returns>The closest DMC color</returns>
        /// <author>Antoine SAKKALIS</author>
        public System.Windows.Media.Color ToDMC(System.Windows.Media.Color colorCase, List<System.Windows.Media.Color> ColorsToUse)
        {
            int bestDistance = 766;

            //Default closest DMC color is the first one in the list
            System.Windows.Media.Color closestDMCColor = closestDMCColor = ColorsToUse[0];

            if (colorMatches.ContainsKey(colorCase))
            {
                //If the color is already in the dictionary, use the mapped DMC color RGB value
                closestDMCColor = colorMatches[colorCase];
            }

            else //If the color is not in the dictionary, calculate the closest DMC color
                foreach (System.Windows.Media.Color color in ColorsToUse)
                {

                    //Calculate the color distance
                    int distance = Math.Abs(color.R - colorCase.R)
                        + Math.Abs(color.G - colorCase.G)
                        + Math.Abs(color.B - colorCase.B);

                    if (distance < bestDistance) //If the distance is better than the previous one, use this color
                    {
                        bestDistance = distance;
                        closestDMCColor = color;
                    }
                }

            //Store the closest DMC color RGB value in the colorMatches dictionary
            colorMatches[colorCase] = closestDMCColor;

            return closestDMCColor;
        }


        /// <summary>
        /// Calculates the average color of a case
        /// </summary>
        /// <param name="caseToCheck"></param>
        /// <param name="sumR">The sum of red</param>
        /// <param name="sumG">The sum of green</param>
        /// <param name="sumB">The sum of blue</param>
        /// <param name="sumOpacity">The sum of opacity</param>
        /// <param name="pixelCount">The number of pixels in that case</param>
        /// <author>Antoine SAKKALIS</author>
        public System.Windows.Media.Color CalculateAverageColor(Case caseToCheck, int sumR, int sumG, int sumB, int sumOpacity, int pixelCount)
        {
            //Foreach color channel, calculate the average
            int avgR = sumR / pixelCount;
            int avgG = sumG / pixelCount;
            int avgB = sumB / pixelCount;
            int avgOpacity = sumOpacity / pixelCount;

            //Return the average color channels in ARGB
            return System.Windows.Media.Color.FromArgb((byte)avgOpacity, (byte)avgR, (byte)avgG, (byte)avgB);
        }

        #endregion
    }
}
