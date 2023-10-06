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
    /// Logique d'interaction pour UsedColorTemplate.xaml
    /// </summary>
    public partial class UsedColorTemplate : UserControl
    {
        public UsedColorTemplate()
        {       
            InitializeComponent();
        }

        /// <summary>
        /// Will set the values of the template.
        /// </summary>
        /// <param name="code">DMC Code</param>
        /// <param name="rgb">code RGB</param>
        /// <author>MaximeLAFFAYE</author>
        public void setValues(string code, RGB rgb)
        {
            
            colorDisplay.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)rgb.R, (byte)rgb.G, (byte)rgb.B));
            //c.Width = 10;
            //c.Height = 10;
            colorDisplay.Width = 250;
            colorDisplay.Height = 80;
            colorDmc.Content = code;
        }

    }
}
