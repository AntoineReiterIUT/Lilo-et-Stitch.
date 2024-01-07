using Lilo_et_Stitch.Data.DAO_GetParameters;
using Lilo_et_Stitch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Lilo_et_Stitch.View.PatternsViewer
{
    /// <summary>
    /// Interaction logic for WindowModelsStitch
    /// </summary>
    public partial class WindowModelsStitch : Window
    {
        #region attributes

        private int page = 0;
        private int maxPage = 0;
        private IManager manager;
        private TemplateUserModel patterns = new();
        Dictionary<int, Dictionary<string, BitmapImage>> images;
        private int selectedId = -1;
        private string patternSearchName = "";

        #endregion

        #region operations

        /// <summary>
        /// Constructor of the class WindowModelsStitch
        /// </summary>
        /// <param name="manager">The used manager</param>
        /// <author>Antoine SAKKALIS</author>
        public WindowModelsStitch(IManager manager)
        {
            //Set the manager
            this.manager = manager;
            InitializeComponent();

            //Display the first page
            ImageLoader();
        }


        /// <summary>
        /// This method will display the patterns
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        private async void ImageLoader()
        {
            try
            {
                //Get the patterns
                (int, Dictionary<int, Dictionary<string, BitmapImage>>) imageData = await manager.GetImages(page, patternSearchName);
                maxPage = imageData.Item1;

                //Update the max page number
                textMaxPages.Content = "/ " + maxPage.ToString();
                images = imageData.Item2;

                if (images.Count == 0)
                {
                    System.Windows.MessageBox.Show("Aucun patron trouvé...");
                }
                else //if there is at least one pattern
                {
                    //Clear the grid
                    controlModels.Items.Clear();

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
                            else //if the name is not too long
                            {
                                pattern.PatternName.Text = name;
                            }

                            //Set different row and column for each TemplateUserModel
                            Grid.SetRow(pattern, row);
                            Grid.SetColumn(pattern, column);

                            //Add the TemplateUserModel to the grid
                            controlModels.Items.Add(pattern);

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
                    textNumPage.Text = (page + 1).ToString();
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Erreur : " + e.Message);
            }
        }


        /// <summary>
        /// Will display the settings of the selected pattern
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        private async void Settings()
        {
            try
            {
                //Get the settings of the selected pattern
                Parameters settings = await manager.GetSettings(selectedId);


                //Display the settings of the selected pattern
                LabelName.Text = images[selectedId].Keys.First();
                LabelStep.Content = $"Taille des cases : {settings.Step}";
                LabelMaxColors.Content = $"Couleurs utilisées : {settings.NbColor}";
                LabelColorGrid.Content = $"Couleur grille : {settings.GridColor}";

                //apply the color of the grid to the rectangle
                RectangleColorGrid.Fill = new System.Windows.Media.SolidColorBrush(settings.ColorGridMedia);
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Erreur : " + e.Message);
            }
        }

        #endregion

        #region events

        /// <summary>
        /// This function will be called when the "HomeButton" is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Antoine SAKKALIS</author>
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            //Display the main menu
            WindowMenuStitch stitchMenu = new WindowMenuStitch();
            stitchMenu.Show();
            //Close the current window
            Close();
        }


        /// <summary>
        /// This function will be called when the "FirstButton" is clicked to go to the first page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Antoine SAKKALIS</author>
        private void Button_FirstClick(object sender, RoutedEventArgs e)
        {
            //Display the first page
            page = 0;
            ImageLoader();
        }


        /// <summary>
        /// This function will be called when the "NextButton" is clicked to go to the next page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Antoine SAKKALIS</author>
        private void Button_NextClick(object sender, RoutedEventArgs e)
        {
            if (page < maxPage - 1) //if we are not on the last page
            {
                //Display the next page
                page++;
                ImageLoader();
            }
        }


        /// <summary>
        /// This function will be called when the "PreviousButton" is clicked to go back to the previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Antoine SAKKALIS</author>
        private void Button_PreviousClick(object sender, RoutedEventArgs e)
        {
            if (page > 0) //if we are not on the first page
            {
                //Display the previous page
                page--;
                ImageLoader();
            }
        }


        /// <summary>
        /// This function will be called when the the "LastButton" is clicked to go to the last page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Antoine SAKKALIS</author>
        private void Button_LastClick(object sender, RoutedEventArgs e)
        {
            //Display the last page
            page = maxPage - 1;
            ImageLoader();
        }


        /// <summary>
        /// This function will be called when the "HomeBtn" is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <authors>Antoine SAKKALIS</authors>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //Display the main menu
            WindowMenuStitch stitchMenu = new();
            stitchMenu.Show();
            //Close the current window
            this.Close();
        }


        /// <summary>
        /// This function will be called when the "PatternsBtn" is clicked to display the selected pattern
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Antoine SAKKALIS</author>
        private void PatternsBtn_Click(object sender, RoutedEventArgs e)
        {
            //Set the selectedId by the id of the selected pattern button
            selectedId = (int)((System.Windows.Controls.Button)sender).Content;

            //Display the selected pattern
            SelectedPattern.Source = images[selectedId].Values.First();

            //Display the download button
            buttonDownload.Visibility = Visibility.Visible;

            //Display the settings of the selected pattern
            Settings();
        }


        /// <summary>
        /// This function will be called when the "Download" is clicked to download the selected pattern
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Antoine SAKKALIS</author>
        private void Button_DownloadClick(object sender, RoutedEventArgs e)
        {
            //Creating the dialog object
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();

            saveFileDialog.Filter = "Fichier PDF (*.pdf) |*.pdf";

            try
            {
                //Show the dialog and wait for the user to select a file
                DialogResult result = saveFileDialog.ShowDialog();

                string path = saveFileDialog.FileName;

                if (path != "") //If the user selected a path to save the pattern
                {
                    manager.GetPattern(LabelName.Text, path);
                    System.Windows.MessageBox.Show("Téléchargé avec succès!");
                }

            }
            catch (Exception excepiton)
            {
                System.Windows.MessageBox.Show("Erreur : " + excepiton.Message);
            }
        }


        /// <summary>
        /// This function will remove the placeholder of the searchbox when we click on it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Antoine SAKKALIS</author>
        private void textSearch_GotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            //Remove the placeholder
            this.textSearch.Text = "";
            this.textSearch.Foreground = new SolidColorBrush(Colors.Black);
        }


        /// <summary>
        /// This function will be directly called when we write in the search bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Antoine SAKKALIS</author>
        private void textSearch_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //If the user press enter or if the text is at least 2 characters long or if the text is empty
            if (e.Key == System.Windows.Input.Key.Enter | textSearch.Text.Length >= 2 | textSearch.Text.Length == 0)
            {
                //set the search value with the search bar content
                patternSearchName = textSearch.Text;
                //Display the first page of the search results
                page = 0;
                ImageLoader();
            }
        }


        /// <summary>
        /// This function will be called when we edit the page number directly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Antoine SAKKALIS</author>
        private void textNumPage_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter) //If the user press enter
            {
                try
                {
                    //Get the page number
                    int numPage = int.Parse(textNumPage.Text);

                    //If the page number is correct
                    if (numPage > 0 && numPage <= maxPage)
                    {
                        //Display the page
                        page = numPage - 1;
                        ImageLoader();
                    }
                    else if (numPage > maxPage) //if the page number is too high
                    {
                        //Display the last page
                        page = maxPage - 1;
                        ImageLoader();
                    }
                    else //if the page number is too low
                    {
                        //Display the first page
                        page = 0;
                        ImageLoader();
                    }
                }
                catch (Exception)
                {
                    //Display the actual page
                    textNumPage.Text = (page + 1).ToString();
                    System.Windows.MessageBox.Show("Veuillez entrer un nombre");
                }
            }
        }

        #endregion
    }
}
