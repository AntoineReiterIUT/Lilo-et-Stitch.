using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;




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
        /// This function will be called when the "InsertBtn" is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertBtn(object sender, RoutedEventArgs e)
        {




        }

        /// <summary>
        /// This function will be called when the "HomeBtn" is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {

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
        /// This function will be called when the TransformBtn is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TransformBtn_Click(object sender, RoutedEventArgs e)
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
                System.Windows.MessageBox.Show("Une erreur s'est produite : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }


}

