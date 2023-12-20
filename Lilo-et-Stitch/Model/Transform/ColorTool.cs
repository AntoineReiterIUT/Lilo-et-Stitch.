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
    class ColorTool
    {
        private Dictionary<System.Windows.Media.Color, System.Windows.Media.Color> colorMatches = new Dictionary<System.Windows.Media.Color, System.Windows.Media.Color>();


        /// <summary>
        /// Change a RGB color into an existing DMC color from the color of a case
        /// </summary>
        /// <param name="colorCase">Case to calculate the DMC color</param>
        /// <param name="useColorsToUse">Flag to determine whether to use a restricted set of colors</param>
        /// <returns>The closest DMC color</returns>
        /// <author>Antoine SAKKALIS</author>
        public System.Windows.Media.Color ToDMC(System.Windows.Media.Color colorCase, List<System.Windows.Media.Color> ColorsToUse)
        {
            int bestDistance = 766;

            System.Windows.Media.Color closestDMCColor = System.Windows.Media.Color.FromArgb(0, 0, 0, 0);

            closestDMCColor = ColorsToUse[0];

            if (colorMatches.ContainsKey(colorCase))
            {
                //If the color is already in the dictionary, use the mapped DMC color RGB value
                closestDMCColor = colorMatches[colorCase];
            }

            else
            {
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
                return closestDMCColor;
            }

            //Store the closest DMC color RGB value in the colorMatches dictionary
            colorMatches[colorCase] = closestDMCColor;

            return closestDMCColor;
        }
    }
}
