using Lilo_et_Stitch.Data.DAO_GetParameters;
using Lilo_et_Stitch.Model;
using Lilo_et_Stitch.View.PatternsViewer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Lilo_et_Stitch.View
{
    /// <summary>
    /// Interaction logic for WindowModelsStitch
    /// </summary>
    public partial class WindowModelsStitch : Window
    {
        #region attributes

        private int page = 0;
        private Manager manager;
        private TemplateUserModel patterns = new();
        Dictionary<int, Dictionary<string, BitmapImage>> images;
        private int selectedId = -1;

        #endregion

        #region operations

        /// <summary>
        /// Constructor of the class WindowModelsStitch
        /// </summary>
        /// <param name="manager">The used manager</param>
        /// <author>Antoine SAKKALIS</author>
        public WindowModelsStitch(Manager manager)
        {
            this.manager = manager;
            InitializeComponent();
            ImageLoader();
        }


        /// <summary>
        /// This function will be called when the "HomeButton" is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Antoine SAKKALIS</author>
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowMenuStitch stitchMenu = new WindowMenuStitch();
            stitchMenu.Show();
            Close();
        }


        /// <summary>
        /// This function will be called when the "PatternsButton" is clicked
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        private async void ImageLoader()
        {
            try
            {
                images = await manager.GetImages(page);

                if (images.Count == 0)
                {
                    MessageBox.Show("Il n'y a pas de patrons à afficher sur cette page");
                    page--;
                }
                else
                {
                    //Clear the grid
                    test.Items.Clear();

                    //Display all the patterns
                    int row = 0;
                    int column = 0;
                    foreach (int id in images.Keys) //foreach id of the pattern
                    {
                        foreach (string name in images[id].Keys) //foreach name of the pattern per id
                        {
                            //Create a new TemplateUserModel
                            TemplateUserModel pattern = new TemplateUserModel();
                            //Set the properties of the TemplateUserModel
                            pattern.PatternButton.Click += PatternsBtn_Click;
                            pattern.PatternButton.Content = id;
                            pattern.PatternImage.Source = images[id][name];
                            if (name.Length > 30) //if the name is too long, we cut it
                            {
                                pattern.PatternName.Text = name.Substring(0, 30) + "...";
                            }
                            else
                            {
                                pattern.PatternName.Text = name;
                            }

                            //Set different row and column for each TemplateUserModel
                            Grid.SetRow(pattern, row);
                            Grid.SetColumn(pattern, column);

                            test.Items.Add(pattern);

                            //Update row and column for the next iteration
                            column++;
                            if (column >= 3)  //display in 3 columns
                            {
                                column = 0;
                                row++;
                            }
                        }
                    }
                    //Update the page number
                    numPage.Text = (page + 1).ToString();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        /// <summary>
        /// This function will be called when the "NumPage" is changed to set manually the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumPage_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (int.TryParse(numPage.Text, out int newPage))
            //{
            //    page = newPage;
            //    if (newPage > 0)
            //    {
            //        ImageLoader();
            //    }
            //}
        }


        /// <summary>
        /// This function will be called when the "NextButton" is clicked (to change page)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_NextClick(object sender, RoutedEventArgs e)
        {
            page++;
            ImageLoader();
        }


        /// <summary>
        /// This function will be called when the "PreviousButton" is clicked (to change page)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_PreviousClick(object sender, RoutedEventArgs e)
        {
            if (page > 0)
            {
                page--;
                ImageLoader();
            }
        }


        /// <summary>
        /// This function will be called when the "HomeBtn" is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <authors>Mattis GALOPPIN</authors>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            WindowMenuStitch stitchMenu = new();
            stitchMenu.Show(); //Display new page 
            this.Close(); //Close the firt one
        }

        /// <summary>
        /// This function will be called when the "PatternsBtn" is clicked to display the selected pattern
        /// </summary>
        private void PatternsBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedId = (int)((Button)sender).Content;
            SelectedPattern.Source = images[selectedId].Values.First();
            Settings();
        }

        /// <summary>
        /// This function will be called when the "Parameters" is clicked to display the parameters of the selected pattern
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        private async void Settings()
        {
            try
            {
                LabelName.Content = images[selectedId].Keys.First();
                Parameters settings = await manager.GetSettings(selectedId);
                LabelStep.Content = $"Taille des cases : {settings.Step}";
                LabelMaxColors.Content = $"Couleurs utilisées : {settings.NbColor}";
                LabelColorGrid.Content = $"Couleur grille : {settings.GridColor}";
                RectangleColorGrid.Fill = new System.Windows.Media.SolidColorBrush(settings.ColorGridMedia);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// This function will be called when the "Download" is clicked to download the selected pattern
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Antoine SAKKALIS</author>
        private void Button_DownloadClick(object sender, RoutedEventArgs e)
        {
            //if (selectedId != -1)
            //{
            //    manager.Import(selectedId);
            //    MessageBox.Show("Le patron a bien été importé");
            //}
            //else
            //{
            //    MessageBox.Show("Veuillez sélectionner un patron");
            //}
        }
        #endregion

    }
}
