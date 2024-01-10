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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Lilo_et_Stitch.View
{
    /// <summary>
    /// Window for the main menu of the Lilo & Stitch application
    /// </summary>
    public partial class WindowMenuStitch : Window
    {
        #region attributes

        private WindowTransformStitch stitchTransform;
        private WindowModelsStitch stitchModels;
        private IManager manager = new Manager();

        #endregion


        #region operations

        /// <summary>
        /// Constructor of the main menu
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        public WindowMenuStitch()
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
            //Display the new page
            stitchTransform = new WindowTransformStitch(manager); 
            stitchTransform.Show();

            //Close the the first one
            this.Close();  
        }


        /// <summary>
        /// Button which can change the page to go to StitchModels.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Mattis GALOPPIN</author>
        private void ModelServ_Click(object sender, RoutedEventArgs e)
        {
            //Display the new page 
            stitchModels = new WindowModelsStitch(manager); 
            stitchModels.Show();

            //Close the first one
            this.Close(); 
        }

        #endregion
    }
}
