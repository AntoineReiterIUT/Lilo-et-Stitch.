using Lilo_et_Stitch.Model;
using System.Windows.Media;

namespace TestLilo_et_Stitch
{
    /// <summary>
    /// This class will run tests on the "ColorPalette" class
    /// </summary>
    public class TestColorPalette
    {
        #region operations
        /// <summary>
        /// Tests the color palette loading method
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        [Fact]
        public void TestGetPalette()
        {
            ManagerCreator manager = new();
            manager.ManagerWithNewPalette();
            Manager paletteManager = manager.Manager;

            //Checking if the colors have been properly loaded
            Assert.Equal(paletteManager.GetPalette()["315"], Color.FromArgb(0,129, 73, 82));
            Assert.Equal(paletteManager.GetPalette()["3685"], Color.FromArgb(0, 136,21,49));
            Assert.Equal(paletteManager.GetPalette()["White"], Color.FromArgb(0, 252, 251, 248));
        }
        #endregion
    }
}
