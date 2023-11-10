using Lilo_et_Stitch.Model.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLilo_et_Stitch
{
    /// <summary>
    /// Multiple tests will be ran to check if the class named RGB works correctly
    /// </summary>
    public class TestRGB
    {
        #region operations
        /// <summary>
        /// Will test if the RGB color is created properly
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        [Fact]
        public void newRGB()
        {
            //Creation and test of the RGB color
            RGB rgb1 = new RGB(1, 2, 3);

            Assert.Equal(1, rgb1.R);
            Assert.Equal(2, rgb1.G);
            Assert.Equal(3, rgb1.B);

            //Test of the RGB color with a negative value
            RGB rgb2 = new RGB(-1, 300, 255);

            Assert.Equal(0, rgb2.R);
            Assert.Equal(255, rgb2.G);
            Assert.Equal(255, rgb2.B);
        }
        #endregion
    }
}
