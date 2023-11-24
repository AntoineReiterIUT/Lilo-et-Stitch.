using Lilo_et_Stitch.Model.Transform;
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
    /// This class represents a template for each color used
    /// </summary>
    public partial class TemplateUsedColorColor : UserControl
    {
        #region operations 
        /// <summary>
        /// Constructor of the template
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public TemplateUsedColorColor()
        {       
            InitializeComponent();
        }

        /// <summary>
        /// Will set the values of the template.
        /// </summary>
        /// <param name="code">DMC Code</param>
        /// <param name="rgb">code RGB</param>
        /// <author>Maxime LAFFAYE</author>
        public void setValues(string code, Color color)
        {
            colorDisplay.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)color.R, (byte)color.G, (byte)color.B));
            colorDisplay.Width = 250;
            colorDisplay.Height = 80;
            colorDmc.Content = code;
        }
        #endregion
    }
}
