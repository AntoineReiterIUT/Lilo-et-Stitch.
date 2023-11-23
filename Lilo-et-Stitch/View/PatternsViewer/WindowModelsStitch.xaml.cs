using Lilo_et_Stitch.Model;
using Lilo_et_Stitch.View.PatternsViewer;
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

namespace Lilo_et_Stitch.View
{
    /// <summary>
    /// Logique d'interaction pour StitchModels.xaml
    /// </summary>
    public partial class WindowModelsStitch : Window
    {

        #region attributes
        private int page = 0;
        private Manager manager;
        #endregion

        #region operations
        /// <summary>
        /// Window constructor
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        public WindowModelsStitch(Manager manager)
        {
            InitializeComponent();
            this.manager = manager;
            ImageLoader(null, null);
        }


        /// <summary>
        /// Returns to the menu when clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Antoine SAKKALIS</author>
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowMenuStitch stitchMenu = new ();
            stitchMenu.Show(); //Display the new page 
            this.Close(); //Close the first one
        }


        /// <summary>
        /// Returns to the menu when clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Antoine SAKKALIS</author>
        private void ImageLoader(object sender, RoutedEventArgs e)
        {
            var curPattern = Pattern1;

            //Contains the images to display (6 images) with their id, and for each id, the name of the pattern and the image
            Dictionary<int, Dictionary<string, BitmapImage>> images = manager.GetImages(page);

            for (int i = 0; i < 6; i++)
            {
                curPattern = (TemplateUserModel)FindName("Pattern" + i.ToString());
                //curPattern.PatternImage.Source = (images[i]);
                //curPattern.PatternName.Text = images[i];
            }

        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            page++;
            ImageLoader(null, null);
        }
    }
}
