using Lilo_et_Stitch.Model.Transform;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Markup;
using System.Windows.Media;

namespace Lilo_et_Stitch.Model
{
    /// <summary>
    /// This class represents the color pallet of DMC
    /// </summary>
    public class ColorPalette
    {
        #region attributes
        private Dictionary<string, RGB> dmcColors;
        #endregion

        #region properties
        /// <summary>
        /// Stores the DMCs colors in a Dictionnary with their RGB values and DMC value code
        /// </summary>
        public Dictionary<string, RGB> DMCColors { get { return dmcColors; } }
        #endregion

        #region operations
        /// <summary>
        /// Loads the DMC colors in the list
        /// </summary>
        public void LoadColors()
        {
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var file = Path.Combine(projectFolder, @"../Data/InUsePalette.csv");
            using (TextFieldParser sr = new TextFieldParser(file))
            {
                // Lire les lignes du fichier
                while (!sr.EndOfData)
                {
                    // Lire une ligne du fichier
                    string line = sr.ReadLine();
                    // Séparer les valeurs par le point-virgule
                    string[] values = line.Split(';');
                    // Afficher les valeurs
                    foreach (string value in values)
                    {
                        RGB couleur = new RGB(int.Parse(values[1]), int.Parse(values[2]), int.Parse(values[3]));
                        dmcColors[values[0]] = couleur;

                    }
                }
            }
        }

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public ColorPalette()
        {
            dmcColors = new Dictionary<string, RGB>();
        }


        #endregion
    }
}
