using Lilo_et_Stitch.Model.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLilo_et_Stitch
{
    /// <summary>
    /// Multiple tests will be ran to check if the class named RGB works correctly.
    /// </summary>
    public class TestRGB
    {

        /// <summary>
        /// Will test if the RGB color is created properly.
        /// </summary>
        [Fact]
        public void newRGB()
        {
            RGB rgb1 = new RGB(1, 2, 3);

            Assert.Equal(1, rgb1.R);
            Assert.Equal(2, rgb1.G);
            Assert.Equal(3, rgb1.B);

            RGB rgb2 = new RGB(-1, 300, 255);

            Assert.Equal(0, rgb2.R);
            Assert.Equal(255, rgb2.G);
            Assert.Equal(255, rgb2.B);
        }
    }
}
