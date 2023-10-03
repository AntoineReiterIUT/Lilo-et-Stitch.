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
        }

        private void NewModel_Click(object sender, RoutedEventArgs e)
        {
            StitchModels stitchModels = new StitchModels();

            stitchModels.Show();  // Affichez la nouvelle fenêtre
            this.Close();  // Fermez la fenêtre actuelle si nécessai
        }
    }
}
