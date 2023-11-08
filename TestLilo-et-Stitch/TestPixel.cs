using Lilo_et_Stitch.Model.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLilo_et_Stitch
{
    /// <summary>
    /// Multiple tests will be ran to check if the class named "Pixel" works correctly.
    /// </summary>
    public class TestPixel
    {
        /// <summary>
        /// Will test if the Pixel is created properly.
        /// </summary>
        /// <author>MaximeLAFFAYE</author>
        [Fact]
        public void newPixel()
        {
            Pixel pixel1 = new Pixel(new RGB(255, 254, 253), new Coordinate(10, 15),1);

            Assert.Equal(10, pixel1.Coord.X) ;
            Assert.Equal(15, pixel1.Coord.Y) ;

            Assert.Equal(255,pixel1.ColorRGB.R) ;
            Assert.Equal(254, pixel1.ColorRGB.G);
            Assert.Equal(253, pixel1.ColorRGB.B);

        }
    }
}
