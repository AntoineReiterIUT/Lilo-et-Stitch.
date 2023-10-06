using Lilo_et_Stitch.Model;
using Lilo_et_Stitch.Model.Transform;
using System;
using System.Drawing;
using System.IO;
using System.Security.Policy;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Lilo_et_Stitch.View
{
    /// <summary>
    /// Logique d'interaction pour StitchTransform.xaml
    /// </summary>
    public partial class StitchTransform : Window
    {
        int i = 0;
        public StitchTransform()
        {
            InitializeComponent();
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
        /// This function will be called when the "UpdatePaletteBtn_Click" is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdatePaletteBtn_Click(object sender, RoutedEventArgs e)
        {

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

        private void SelectionnerIMG_Click(object sender, RoutedEventArgs e)
        {

            // Créez un OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Filtrez les fichiers pour n'afficher que les fichiers PNG
            openFileDialog.Filter = "Fichiers PNG (*.png)|*.png|Fichier JPG (*.jpg)|*.jpg|Fichier GIF (*.gif)|*.gif|Fichier BMP (*.bmp)|*.bmp";
            try
            {
                // Affichez la boîte de dialogue et attendez que l'utilisateur sélectionne un fichier
                DialogResult result = openFileDialog.ShowDialog();

                // Si l'utilisateur a sélectionné un fichier, importez l'image dans l'application
                if (result != null)
                {
                    // Chemin du fichier sélectionné par l'utilisateur
                    string cheminFichierSource = openFileDialog.FileName;
                    // Copiez le fichier dans un répertoire de votre application
                    string cheminDestination = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                    File.Copy(cheminFichierSource, cheminDestination, true);
                    // Chargez l'image dans une Image WPF
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(cheminFichierSource);
                    bitmap.EndInit();

                    // Affichez l'image dans votre contrôle Image dans votre interface utilisateur
                    imagePrev.Source = bitmap;
                }
            }
            catch (Exception ex)
            {
                // Show a dialog box with the error message
                System.Windows.MessageBox.Show("Une erreur s'est produite : le fichier selectioné est soit incompatible, soit vous n'en avez pas selectionné");
            }
        }

        private void UpdatePaletteBtn_Click_1(object sender, RoutedEventArgs e)
        {

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

        private WriteableBitmap writeableBitmap;

        /// <summary>
        /// This function will be called when the TransformBtn is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>MaximeLAFFAYE</author>
        private void TransformBtn_Click_1(object sender, RoutedEventArgs e)
        {
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
                var (resultatImage, resultatDictionnaire) = mainstitch.TransformImage();
                BitmapImage bitmapImage = new BitmapImage();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    resultatImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png); // Vous pouvez ajuster le format si nécessaire
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = memoryStream;
                    bitmapImage.EndInit();
                }

                // Créer un WriteableBitmap à partir du BitmapImage
                WriteableBitmap writeableBitmap = new WriteableBitmap(bitmapImage);

                // Afficher l'image dans NewImg
                NewImg.Source = writeableBitmap;

            }
        }


    }


}

