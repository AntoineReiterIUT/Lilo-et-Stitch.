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


namespace Lilo_et_Stitch.View
{
    /// <summary>
    /// Logique d'interaction pour StitchTransform.xaml
    /// </summary>
    public partial class StitchTransform : Window
    {
        
        public StitchTransform()
        {
            InitializeComponent();
            CenterWindow();


        }

        /// <summary>
        /// Will center the window
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        private void CenterWindow()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = Width;
            double windowHeight = Height;
            Left = (screenWidth - windowWidth) / 2;
            Top = (screenHeight - windowHeight) / 2;
        }
        /// <summary>
        /// This function will be called when the "HomeBtn" is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            StitchMenu stitchMenu = new StitchMenu();
            stitchMenu.Show(); // display new page 
            this.Close(); // close the firt one
        }

        /// <summary>
        /// This function will be called when the "SaveBtn" is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

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

                }
            }
            catch (Exception ex)
            {
                // Show a dialog box with the error message
                System.Windows.MessageBox.Show("Une erreur s'est produite : le fichier selectionné est soit incompatible, soit vous n'en avez pas selectionné");
            }
        }

        /// <summary>
        /// Handles the palette click event, allowing the user to change the color palette by selecting a CSV file.
        /// </summary>
        /// <param name="sender">The sender object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        /// <author>Antoine Sakkalis</author>
        private void UpdatePaletteBtn_Click(object sender, RoutedEventArgs e)
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
        /// uptdate the value of the slider in the Textbox
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e"></param>
        private void slider1_value_changeds(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtSlider1.Text = NbColorsSlider.Value.ToString();
        }



        /// <summary>
        /// This function will be called when the TransformBtn is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>MaximeLAFFAYE</author>
        private void TransformBtn_Click_1(object sender, RoutedEventArgs e)
        {

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
                MainStitch mainstitch = new MainStitch(bitmap);
                var (editedImage, colorFromImage) = mainstitch.TransformImage();

                usedColorList.Children.Clear();

                //
                foreach (string code in colorFromImage.Keys)
                {
                    RGB rgb = colorFromImage[code];

                    Canvas c = new Canvas();
                    c.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)rgb.R, (byte)rgb.G, (byte)rgb.B));
                    c.Width = 10;
                    c.Height = 10;

                    System.Windows.Controls.Label label = new System.Windows.Controls.Label();
                    label.Content = code;

                    
                    usedColorList.Children.Add(c);
                    usedColorList.Children.Add(label);
                }

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
        /// Handles the Reint click event, allowing the user to reset the color palette.
        /// </summary>
        /// <param name="sender">The sender object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        /// <author>Antoine Sakkalis</author>
        private void Button_ReintPalette(object sender, RoutedEventArgs e)
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
            }

            catch (Exception ex)
            {
                // Show a dialog box with the error message
                System.Windows.MessageBox.Show("Une erreur s'est produite : le fichier selectioné est soit incompatible, soit il est ouvert dans un autre fichier");
            }
        }
    }
}

