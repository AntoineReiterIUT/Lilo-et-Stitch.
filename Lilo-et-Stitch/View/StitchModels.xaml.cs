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
    public partial class StitchModels : Window
    {
        /// <summary>
        /// Window constructor
        /// </summary>
        public StitchModels()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Returns to the menu when clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            StitchMenu stitchMenu = new StitchMenu();
            stitchMenu.Show(); // display new page 
            this.Close(); // close the first one
        }
    }
}
