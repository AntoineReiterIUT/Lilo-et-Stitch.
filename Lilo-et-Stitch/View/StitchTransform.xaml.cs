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

namespace Lilo_et_Stitch.View
{
    /// <summary>
    /// Logique d'interaction pour StitchTransform.xaml
    /// </summary>
    public partial class StitchTransform : Window
    {

        #region attributes
        private System.Windows.Media.Color gridColor = System.Windows.Media.Color.FromRgb(0,0,0);
        private MainStitch mainstitch;
        #endregion

        #region operations

        /// <summary>
        /// Constructor
        /// </summary>
        public StitchTransform()
        {     
            InitializeComponent();
            ResetPalette_Click(null,null);
        }


        /// <summary>
        /// This function will be called when the "SaveBtn" is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>MaximeLAFFAYE</author>
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            //checking if the image has been transformed
            if (mainstitch != null)
            {
                //Creating the dialog object
                System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();

                saveFileDialog.Filter = "Fichier PDF (*.pdf) |*.pdf";

                try
                {
                    // Show the dialog and wait for the user to select a file
                    DialogResult result = saveFileDialog.ShowDialog();

                    string path = saveFileDialog.FileName;

                    if(path == "")
                    {
                        System.Windows.MessageBox.Show("Vous n'avez pas sélectioné d'emplacement!");
                    }
                    else
                    {
                        mainstitch.SavePDF(path);
                        System.Windows.MessageBox.Show("Exporté avec sucès!");
                    }

                }
                catch
                {
                    System.Windows.MessageBox.Show("Erreur lors de l'exportation en PDF.");
                }
                    
            }
        }


        /// <summary>
        /// This function will be called when the "PublishBtn" is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PublishBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }


        /// <summary>
        /// open the file browser and make the user select a file at the correct format
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <autor>Reiter Antoine</autor>
        private void SelectionnerIMG_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();

            // Filter files to only display PNG / JPG / GIF / BMP files
            openFileDialog.Filter = "Fichiers PNG (*.png)|*.png|Fichier JPG (*.jpg)|*.jpg|Fichier GIF (*.gif)|*.gif|Fichier BMP (*.bmp)|*.bmp";
            try
            {
                // Show the dialog and wait for the user to select a file
                DialogResult result = openFileDialog.ShowDialog();

                // Si l'utilisateur a sélectionné un fichier, importez l'image dans l'application
                if (result != null)
                {
                    // File path selected by the user
                    string cheminFichierSource = openFileDialog.FileName;
                    // Copy the file to an application directory
                    string cheminDestination = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                    File.Copy(cheminFichierSource, cheminDestination, true);
                    // Load the image into a WPF Image
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(cheminFichierSource);
                    bitmap.EndInit();
                    // Display the image in the Image control in the UI
                    imagePrev.Source = bitmap;
                    int i;
                    int y;
                    //initialization step with img

                    int stepIni = 35;

                    i = (int)bitmap.Height / stepIni; 
                    y = (int)bitmap.Width / stepIni;
                    i = (i + y) / 2;
                    ChangeStepSlider.Minimum = i;
                    ChangeStepSlider.Maximum = i*3;
                    ChangeStepSlider.Value = i*2;
                }
            }
            catch (Exception ex)
            {
                // Show a dialog box with the error message
                System.Windows.MessageBox.Show("Une erreur s'est produite : le fichier selectionné est soit incompatible, soit vous n'en avez pas selectionné");
            }
        }


        /// <summary>
        /// Will update the preview canva after opening the color picker dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>MaximeLAFFAYE</author>
        private void change_grid_color(object sender, RoutedEventArgs e)
        {
            ColorDialog clrDiag = new ColorDialog();
            if (clrDiag.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.gridColor = System.Windows.Media.Color.FromArgb(clrDiag.Color.A, clrDiag.Color.R, clrDiag.Color.G, clrDiag.Color.B);
                gridColorCanva.Background = new SolidColorBrush(gridColor);            
            }
        }


        /// <summary>
        /// uptdate the value of the slider in the Textbox
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e"></param>Object reference not set to an instance of an object.'
        /// <authors>Antoine Sakkalis</authors>
        private void MaxColorNumberChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            NumberOfColors.Text = NbColorsSlider.Value.ToString();
        }


        /// <summary>
        /// change la valeur de la textbox par celle du slider
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///<authors>Mattis Galoppin</authors>
        private void changeStepValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ChangeStepText.Text = ChangeStepSlider.Value.ToString(); 
        }


        /// <summary>
        /// This function will be called when the TransformBtn is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>MaximeLAFFAYE</author>
        private void TransformBtn_Click_1(object sender, RoutedEventArgs e)
        {
            //Creating the bitmap
            WriteableBitmap writeableBitmap;
            if (imagePrev.Source != null)
            {
                Bitmap bitmap;
                using (var stream = new MemoryStream())
                {
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)imagePrev.Source));
                    encoder.Save(stream);
                    bitmap = new Bitmap(stream);
                }
                this.mainstitch = new MainStitch(bitmap);

                //Starting the transformation of the image
                var (editedImage, colorFromImage) = mainstitch.TransformImage((int)(ChangeStepSlider.Value),(int)(NbColorsSlider.Value),gridColor);

                //clearing the list so we don't stack all colors
                usedColorList.Children.Clear(); 

                //adding the code - rgb color to the scrollViewer
                foreach (string code in colorFromImage.Keys)
                {
                    RGB rgb = colorFromImage[code];

                    UsedColorTemplate current = new UsedColorTemplate();
                    current.setValues(code, rgb);
                    
                    usedColorList.Children.Add(current);
                }

                //rebuilding the image
                BitmapImage bitmapImage = new BitmapImage();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    editedImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png); // Vous pouvez ajuster le format si nécessaire
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = memoryStream;
                    bitmapImage.EndInit();
                }
                writeableBitmap = new WriteableBitmap(bitmapImage);
                NewImg.Source = writeableBitmap;
            }
        }


        /// <summary>
        /// give a manual value to the slider 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <authors>Mattis Galoppin</authors>
        private void OnTextBoxKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter) //if enter is pressed
            {
            
            try   // try to give a new value to the slider
                {
                    ChangeStepSlider.Value = int.Parse(ChangeStepText.Text);
                }
            catch {}
            }
        }
        /// <summary>
        /// give a manual value to the slider
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextBoxKeyUpColor(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter) // Vous pouvez utiliser une condition pour une touche spécifique si nécessaire
            {
                try   // try to give a new value to the slider
                {
                    NbColorsSlider.Value = int.Parse(NumberOfColors.Text);
                }
                catch { }

                
            }
        }

        #region ToolBar's events

        /// <summary>
        /// This function will be called when the "HomeBtn" is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <authors>Mattis Galoppin</authors>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            StitchMenu stitchMenu = new StitchMenu();
            stitchMenu.Show(); // display new page 
            this.Close(); // close the firt one
        }

        /// <summary>
        /// Handles the palette click event, allowing the user to change the color palette by selecting a CSV file.
        /// </summary>
        /// <param name="sender">The sender object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        /// <author>Antoine Sakkalis</author>
        private void ChangePalette_Click(object sender, RoutedEventArgs e)
        {
            // Create a file dialog for selecting a CSV file
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string selectedFilePath = openFileDialog.FileName;

                    string csvData = File.ReadAllText(selectedFilePath);


                    // Write the CSV data to a new file in the specified directory
                    var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                    var file = Path.Combine(projectFolder, @"../Data/InUsePalette.csv");

                    FileInfo fileInfo = new FileInfo(file);
                    if (fileInfo.Length == 0)
                    {
                        System.Windows.MessageBox.Show("La palette saisie est vide, elle ne sera pas utilisée");
                    }
                    else
                    {
                        string targetFilePath = file.ToString();
                        File.WriteAllText(targetFilePath, csvData);
                        NbColorsSlider.Maximum = File.ReadAllLines(targetFilePath).Length;
                        NbColorsSlider.Value = File.ReadAllLines(targetFilePath).Length;
                    }
                }

                catch (Exception ex)
                {
                    // Show a dialog box with the error message
                    System.Windows.MessageBox.Show("Une erreur s'est produite : le fichier selectioné est soit incompatible, soit il est ouvert dans un autre fichier");
                }
            }
        }

        /// <summary>
        /// Handles the reset click event, allowing the user to reset the color palette.
        /// </summary>
        /// <param name="sender">The sender object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        /// <author>Antoine Sakkalis</author>
        private void ResetPalette_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                var file = Path.Combine(projectFolder, @"../Data/DMCBackup/DMCcolors.csv");
                string selectedFilePath = file.ToString(); //Path to the backup

                string csvData = File.ReadAllText(selectedFilePath);


                // Write the CSV backup data to the in use palette file
                file = Path.Combine(projectFolder, @"../Data/InUsePalette.csv");
                string targetFilePath = file.ToString();
                File.WriteAllText(targetFilePath, csvData);

                NbColorsSlider.Maximum = File.ReadAllLines(targetFilePath).Length;
                NbColorsSlider.Value = File.ReadAllLines(targetFilePath).Length;
            }

            catch (Exception ex)
            {
                // Show a dialog box with the error message
                System.Windows.MessageBox.Show("Une erreur s'est produite : le fichier selectioné est soit incompatible, soit il est ouvert dans un autre fichier");
            }
        }

        /// <summary>
        /// Will exit the application
        /// </summary>
        /// <param name="sender">The sender object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        /// <author>MaximeLAFFAYE</author>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        #endregion

        #endregion

    }
}

