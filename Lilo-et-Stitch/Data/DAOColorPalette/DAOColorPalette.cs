using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        #endregion

        #region operations

        /// <author>Antoine SAKKALIS</author>
        public void ChangePalette(string csvPath)
        {
            //Read the file
            FileInfo fileInfo = new FileInfo(csvPath);

            if (fileInfo.Length == 0) //when the color file is empty
            {
                throw new Exception("La palette saisie est vide, elle ne sera pas utilisée");
            }
            else //when the color file is not empty
            {
                using (TextFieldParser sr = new TextFieldParser(csvPath))
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

        /// <author>Antoine SAKKALIS</author>
        public Dictionary<string, Color> GetPalette()
        {
            return palette;
        }


        ///<author>Antoine SAKKALIS</author>
        public void ResetPalette()
        {
            try
            {
                var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                var file = Path.Combine(projectFolder, @"../Data/DMCBackup/DMCcolors.csv");

                ChangePalette(file.ToString());
            }
            catch
            {
                throw new Exception("Une erreur s'est produite lors de la réinitialisation de la palette de couleur");
            }
        }
        #endregion

    }
}
