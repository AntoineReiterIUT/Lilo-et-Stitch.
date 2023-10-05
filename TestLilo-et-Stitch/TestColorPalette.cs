using Lilo_et_Stitch.Model;
using Lilo_et_Stitch.Model.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLilo_et_Stitch
{
    public class TestColorPalette
    {
        /// <summary>
        /// Tests the color palette loading method
        /// </summary>
        [Fact]
        public void TestLoadColors()
        {
            ColorPalette testPalette = new();
            testPalette.LoadColors();

            Assert.Equal(testPalette.DMCColors["315"], new RGB(129, 73, 82));
            Assert.Equal(testPalette.DMCColors["3350"], new RGB(188, 67, 101));
            Assert.Equal(testPalette.DMCColors["White"], new RGB(252, 251, 248));

        }
    }
}
