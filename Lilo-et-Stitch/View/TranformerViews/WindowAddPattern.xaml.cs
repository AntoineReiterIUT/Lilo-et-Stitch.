using Lilo_et_Stitch.Model;
using Microsoft.VisualBasic.ApplicationServices;
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

namespace Lilo_et_Stitch.View.TranformerViews
{
    /// <summary>
    /// Logique d'interaction pour WindowAddPattern.xaml
    /// </summary>
    public partial class WindowAddPattern : Window
    {
        #region Attributes

        private IManager manager;

        #endregion

        #region Operations

        /// <summary>
        /// The constructor of the WindowAddPattern
        /// </summary>
        /// <param name="manager">The used manager</param>
        public WindowAddPattern(IManager manager)
        {
            InitializeComponent();
            this.manager = manager;
        }

        /// <summary>
        /// Will call the publish of the manager with the choosed name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AjouterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                manager.Publish(PatternName.Text);
                MessageBox.Show("Le pattern a bien été ajouté");
                this.Close();
            }
            catch (Exception ex)
            {
                ReturnMessage.Text = ex.Message;
            }
        }

        #endregion
    }
}
