using Lilo_et_Stitch.Model;
using Lilo_et_Stitch.Model.Transform;
using System;
using System.Linq;
using System.Security.Policy;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Color = System.Windows.Media.Color;
using Lilo_et_Stitch.View.PatternsViewer;
using Lilo_et_Stitch.View.TranformerViews;

namespace Lilo_et_Stitch.View
{
    /// <summary>
    /// Logique d'interaction pour StitchTransform.xaml
    /// </summary>
    public partial class WindowTransformStitch : Window
    {
        #region attributes

        private System.Windows.Media.Color gridColor = System.Windows.Media.Color.FromRgb(0,0,0);
        private IManager manager;
        private System.Drawing.Image editedImage;

        #endregion


        #region operations

        /// <summary>
        /// Constructor of the window
        /// </summary>
        /// <param name="manager">The manager used</param>
        /// <author>Maxime LAFFAYE</author>
        public WindowTransformStitch(IManager manager)
        {
            this.manager = manager;
            InitializeComponent();

            //Reset the used palette
            ResetPalette_Click(null, null);
        }


        /// <summary>
        /// This function will be called when the "SaveBtn" is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Maxime LAFFAYE</author>
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            //checking if the image has been transformed
            if (manager != null)
            {
                //Creating the dialog object
                System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();

                saveFileDialog.Filter = "Fichier PDF (*.pdf) |*.pdf";

                try
                {
                    // Show the dialog and wait for the user to select a file
                    DialogResult result = saveFileDialog.ShowDialog();

                    string path = saveFileDialog.FileName;

                    if (path != "")
                    {
                        manager.ExportPDF(path);
                        System.Windows.MessageBox.Show("Exporté avec sucès!");
                    }

                }
                catch (Exception excepiton)
                {
                    System.Windows.MessageBox.Show(excepiton.Message);
                }
                    
            }
        }


        /// <summary>
        /// This function will be called when the "PublishBtn" is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Antoine SAKKALIS</author>
        private void PublishBtn_Click(object sender, RoutedEventArgs e)
        {
            //Displaying the publish window
            WindowAddPattern addPage = new WindowAddPattern(manager);
            addPage.ShowDialog();
        }


        /// <summary>
        /// Open the file browser and make the user select a file at the correct format
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <autor>Antoine REITER</autor>
        private void SelectionnerIMG_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();

            //Filter files to only display PNG / JPG / GIF / BMP files
            openFileDialog.Filter = "Image Files(*.PNG;*.JPG;*.GIF;*.BMP)|*.PNG;*.JPG;*.GIF;*.BMP";

            try
            {
                //Show the dialog and wait for the user to select a file
                DialogResult result = openFileDialog.ShowDialog();

                //File path selected by the user
                string cheminFichierSource = openFileDialog.FileName;
                    
                //Import it to the model
                manager.Import(cheminFichierSource);
                //done importing

                //Load the bitmapImage
                BitmapImage bitmap = manager.LoadBitmapImage();
                imagePrev.Source = bitmap;


                //Initialization step with img
                int stepIni = 35;
                
                if(bitmap.Width > stepIni && bitmap.Height > 35) //Verify that the image is big enough
                {
                    //Calculate a pertinent range for the size of the cases
                    int i = (int)bitmap.Height / stepIni;
                    int y = (int)bitmap.Width / stepIni;
                    i = (i + y) / 2;
                    NbStepSlider.Minimum = i;
                    NbStepSlider.Maximum = i * 3;
                    NbStepSlider.Value = i * 2;
                }
                

            }
            catch (Exception ex)
            {
                //Show a dialog box with the error message
                System.Windows.MessageBox.Show("Erreur : le fichier selectionné est soit incompatible, soit vous n'en avez pas selectionné");
            }
        }


        /// <summary>
        /// Will update the preview canvas after opening the color picker dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Maxime LAFFAYE</author>
        private void change_grid_color(object sender, RoutedEventArgs e)
        {
            //Open the color picker dialog
            ColorDialog clrDiag = new ColorDialog();
            if (clrDiag.ShowDialog() == System.Windows.Forms.DialogResult.OK) //If the user clicked on OK
            {
                //Change the grid color for the transformation
                this.gridColor = System.Windows.Media.Color.FromArgb(clrDiag.Color.A, clrDiag.Color.R, clrDiag.Color.G, clrDiag.Color.B);
                //Change the preview color
                gridColorCanva.Background = new SolidColorBrush(gridColor);            
            }
        }


        /// <summary>
        /// Update the value of the slider in the Textbox
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e"></param>
        /// <authors>Antoine SAKKALIS</authors>
        private void MaxColorNumberChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            NumberOfColors.Text = NbColorsSlider.Value.ToString();
        }


        /// <summary>
        /// Update the text of the slider whenever the slider is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///<authors>Mattis GALOPPIN</authors>
        private void changeStepValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            NbStepText.Text = NbStepSlider.Value.ToString(); 
        }


        /// <summary>
        /// This function will be called when the TransformBtn is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Maxime LAFFAYE</author>
        private void TransformBtn_Click_1(object sender, RoutedEventArgs e)
        {            
            if (imagePrev.Source != null) //If an image has been imported
            {
                Bitmap bitmap;
                using (var stream = new MemoryStream()) //Convert the image to a bitmap
                {
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)imagePrev.Source));
                    encoder.Save(stream);
                    bitmap = new Bitmap(stream);
                }

                //Start the transformation of the image
                GeneratedImage generatedImage = manager.TransformImage((int)NbStepSlider.Value, (int)NbColorsSlider.Value, gridColor);

                //Clear the list so we don't stack all colors
                usedColorList.Children.Clear(); 

                //Add the code - rgb color to the scrollViewer
                foreach (string code in generatedImage.UsedColor.Keys)
                {
                    Color colorFound = generatedImage.UsedColor[code];

                    TemplateUsedColorColor current = new TemplateUsedColorColor();
                    current.setValues(code, colorFound);
                    
                    usedColorList.Children.Add(current);
                }

                //Rebuild the image
                BitmapImage bitmapImage = generatedImage.BitmapImage;

                NewImg.Source = bitmapImage;
            }
            
        }


        /// <summary>
        /// Give a manual value to the slider 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <authors>Antoine SAKKALIS</authors>
        private void OnTextBoxKeyUpStep(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (int.TryParse(NbStepText.Text, out int newValue))
            {
                //If parsing is successful, update the slider value
                NbStepSlider.Value = newValue;
            }
        }

        /// <summary>
        /// Give a manual value to the slider
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Antoine SAKKALIS</author>
        private void OnTextBoxKeyUpColor(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (int.TryParse(NumberOfColors.Text, out int newValue))
            {
                //If parsing is successful, update the slider value
                NbColorsSlider.Value = newValue;
            }
        }


        #region ToolBar's events

        /// <summary>
        /// This function will be called when the "HomeBtn" is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <authors>Mattis GALOPPIN</authors>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //Display new page 
            WindowMenuStitch stitchMenu = new ();
            stitchMenu.Show();

            //Close the first one
            this.Close(); 
        }


        /// <summary>
        /// Handles the palette click event, allowing the user to change the color palette by selecting a CSV file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Antoine SAKKALIS</author>
        private void ChangePalette_Click(object sender, RoutedEventArgs e)
        {
            //Create a file dialog for selecting a CSV file
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == true) //If the user clicked on OK
            {
                try
                {
                    //Update the palette
                    manager.UpdatePalette(openFileDialog.FileName);
                    //Update the slider to match the new palette
                    NbColorsSlider.Maximum = NbColorsSlider.Value = manager.GetPalette().Count();
                }

                catch (Exception ex)
                {
                    //Show a dialog box with the error message
                    System.Windows.MessageBox.Show("Une erreur s'est produite : le fichier selectioné est soit incompatible, soit il est ouvert dans un autre fichier");
                }
            }
        }


        /// <summary>
        /// Handles the reset click event, allowing the user to reset the color palette.
        /// </summary>
        /// <param name="sender">The sender object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        /// <author>Antoine SAKKALIS</author>
        private void ResetPalette_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Reset the palette
                manager.ResetPalette();
                //Update the slider to match the new palette
                NbColorsSlider.Maximum = NbColorsSlider.Value = manager.GetPalette().Count();
            }

            catch (Exception ex)
            {
                //Show a dialog box with the error message
                System.Windows.MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Will exit the application
        /// </summary>
        /// <param name="sender">The sender object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        /// <author>Maxime LAFFAYE</author>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }


        /// <summary>
        /// This method will remove the limitations for the size of the cases 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Antoine SAKKALIS</author>
        private void ManualCase_Click(object sender, RoutedEventArgs e)
        {
            NbStepSlider.Minimum = 1;
            NbStepSlider.Maximum = NbStepSlider.Maximum * NbStepSlider.Maximum;
        }

        #endregion

        #endregion

    }
}

