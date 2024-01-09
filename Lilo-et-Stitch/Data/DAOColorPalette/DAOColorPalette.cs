using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace Lilo_et_Stitch.Data.DAOColorPalette
{
    /// <summary>
    /// This class will handle all the data access for the color palette
    /// </summary>
    public class DAOColorPalette : IDAOColorPalette
    {
        #region attributes

        private Dictionary<string, Color> palette;
        private string path;

        #endregion


        #region operations

        /// <summary>
        /// Constructor of the DAOColorPalette
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        public DAOColorPalette()
        {
            //Initialize the palette
            palette = new Dictionary<string, Color>();
        }


        /// <summary>
        /// Change the palette used to transform the image
        /// </summary>
        /// <param name="csvPath">Path to the selected csv file</param>
        /// <exception cref="Exception">Exception thrown if the color Palette is invalid</exception>
        /// <exception cref="FormatException">Exception thrown if the color Palette data (csv) format is invalid</exception>
        /// <author>Antoine SAKKALIS</author>
        public void ChangePalette(string csvPath)
        {
            this.path = csvPath;

            //Read the file
            FileInfo fileInfo = new FileInfo(csvPath);

            if (fileInfo.Length == 0) //when the color file is empty
            {
                throw new Exception("La palette saisie est vide, elle ne sera pas utilisée");
            }
            else //when the color file is not empty
            {
                //We clear the palette
                palette.Clear();
                using (TextFieldParser sr = new TextFieldParser(csvPath))
                {
                    //While the file is not empty
                    while (!sr.EndOfData)
                    {
                        //Read a line by splitting values with a ';'
                        string line = sr.ReadLine();
                        string[] values = line.Split(';');

                        try
                        {
                            if (values.Length == 4) //Tests if there's 4 elements in the csv file line
                            {
                                //Add the color to the list
                                Color couleur = new Color();
                                couleur.R = Byte.Parse(values[1]);
                                couleur.G = Byte.Parse(values[2]);
                                couleur.B = Byte.Parse(values[3]);

                                palette[values[0]] = couleur;
                            }
                            else
                            {
                                throw new FormatException($"La ligne {palette.Count() + 1} de votre fichier palette n'est pas au bon format, la couleur à cette ligne ne sera pas utilisée, corrigez cette ligne de votre palette (bon format = CodeCouleur;R;G;B) ou utilisez le bouton pour réinitiliser.");
                            }
                        }
                        catch (FormatException ex)
                        {
                            throw new Exception("Une erreur de format pour la palette de couleur s'est produite lors de la conversion à la ligne " + palette.Count() + 1 + " de votre palette : " + ex.Message);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Gives the list of all the colors in the palette
        /// </summary>
        /// <returns>Dictionary of all the colors in the palette with their names</returns>
        /// <author>Antoine SAKKALIS</author>
        public Dictionary<string, Color> GetPalette()
        {
            return palette;
        }


        /// <summary>
        /// Get the path of where the palette is saved
        /// </summary>
        /// <returns>The path of where the palette is saved</returns>
        /// <author>Maxime LAFFAYE</author>
        public string GetPath()
        {
            return this.path;
        }


        /// <summary>
        /// Reset the palette to the default DMC colors
        /// </summary>
        /// <exception cref="Exception">Exception thrown if the backup color Palette is invalid (modified app files)</exception>
        /// <author>Antoine SAKKALIS</author>
        public void ResetPalette()
        {
            try
            {
                //Get the path of the backup color palette
                string file = Path.Combine(Application.StartupPath, "Data", "DMCBackup", "DMCcolors.csv");

                //Change the palette to the backup palette
                ChangePalette(file.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("La palette de couleur est soit introuvable soit invalide (l'application a été altérée) : " + e.Message);
            }
        }

        #endregion

    }
}
