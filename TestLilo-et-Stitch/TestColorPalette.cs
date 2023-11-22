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
        public void TestLoadColors()
        {
            ManagerCreator manager = new();
            manager.ManagerWithNewPalette();
            Manager paletteManager = manager.Manager;

            /*
            //Checking if the colors have been properly loaded
            Assert.Equal(paletteManager.GetPalette()["315"], Color.FromRgb(129, 79, 1));
        }
        #endregion
    }
}
