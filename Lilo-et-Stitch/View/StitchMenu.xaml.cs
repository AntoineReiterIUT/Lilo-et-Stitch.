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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Lilo_et_Stitch.View
{
    /// <summary>
    /// Logique d'interaction pour StitchMenu.xaml
    /// </summary>
    public partial class StitchMenu : Window
    {
        public StitchMenu()
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
        /// button which can change the page to go to StitchTransform.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewModel_Click(object sender, RoutedEventArgs e)
        {
            StitchTransform stitchTransform = new StitchTransform();

            stitchTransform.Show();  // display new page
            this.Close();  // close the firt one
        }
        /// <summary>
        /// button which can change the page to go to StitchModels.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModelServ_Click(object sender, RoutedEventArgs e)
        {
            StitchModels stitchModels = new StitchModels();
            stitchModels.Show(); // display new page 
            this.Close(); // close the firt one
        }
    }
}
