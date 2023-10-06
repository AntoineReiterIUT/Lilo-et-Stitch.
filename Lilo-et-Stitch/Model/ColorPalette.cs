using Lilo_et_Stitch.Model.Transform;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        /// <exception cref="FormatException">Thrown when there is a formatting error in the CSV file or during conversion.</exception>
        public void LoadColors()
        {
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var file = Path.Combine(projectFolder, @"../Data/InUsePalette.csv");

            FileInfo fileInfo = new FileInfo(file);
            if (fileInfo.Length == 0)
            {
                System.Windows.MessageBox.Show("La palette saisie est vide.");
            }
            else //when the color file is not empty
            {
                using (TextFieldParser sr = new TextFieldParser(file))
                {
                    while (!sr.EndOfData)
                    {
                        //Read a line
                        string line = sr.ReadLine();
                        string[] values = line.Split(';');

                        try
                        {
                            if (values.Length == 4) //Tests if there's 4 elements in the csv file line
                            {
                                RGB couleur = new RGB(int.Parse(values[1]), int.Parse(values[2]), int.Parse(values[3]));
                                dmcColors[values[0]] = couleur;
                            }
                            else
                            {
                                System.Windows.MessageBox.Show($"La ligne {dmcColors.Count() + 1} de votre fichier palette n'est pas au bon format, la couleur à cette ligne ne sera pas utilisée, corrigez cette ligne de votre palette (bon format = CodeCouleur;R;G;B) ou utilisez le bouton pour réinitiliser.");
                            }
                        }
                        catch (FormatException ex)
                        {
                            System.Windows.MessageBox.Show("Une erreur de format pour la palette de couleur s'est produite lors de la conversion à la ligne " + dmcColors.Count() + 1 + " de votre palette : " + ex.Message);
                        }

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
