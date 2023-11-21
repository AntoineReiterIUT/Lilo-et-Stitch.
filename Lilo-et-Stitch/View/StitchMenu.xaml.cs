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
    /// Main menu of the app
    /// </summary>
    public partial class StitchMenu : Window
    {
        #region attributes
        StitchTransform stitchTransform = new StitchTransform();
        StitchModels stitchModels = new StitchModels();
        #endregion

        #region operations
        /// <summary>
        /// Constructor of the main menu
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        public StitchMenu()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Button which can change the page to go to StitchTransform.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Mattis GALOPPIN</author>
        private void NewModel_Click(object sender, RoutedEventArgs e)
        {
            stitchTransform.Show();  //Display the new page
            this.Close();  //Close the the first one
        }


        /// <summary>
        /// Button which can change the page to go to StitchModels.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Mattis GALOPPIN</author>
        private void ModelServ_Click(object sender, RoutedEventArgs e)
        {
            stitchModels.Show(); //Display the new page 
            this.Close(); //Close the first one
        }
        #endregion
    }
}
