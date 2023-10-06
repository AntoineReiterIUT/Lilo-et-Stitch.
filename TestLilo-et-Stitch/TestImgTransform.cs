using Lilo_et_Stitch.Model;
using Lilo_et_Stitch.Model.Transform;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TestLilo_et_Stitch
{
    public class TestImgTransform
    {
        private Image img;
        private ImgTransform transImg;

        [Fact]
        public void TestMakeGrid()
        {
            //the size of the image to test is 800 by 800
            transImg.MakeGrid();
            Assert.Equal(25600,transImg.Cases.Count());

        }
        public void TestCaseAverage()
        {

        }

        /// <summary>
        /// Tests the method to convert RGB colors to the nearest DMC value
        /// </summary>
        /// <author>Antoine Sakkalis</author>
        [Fact]
        public void TestToDMC()
        {
            transImg.Cases.Add(new Case(new Coordinate(1,2), new Coordinate(1, 2)));
            transImg.Cases.Add(new Case(new Coordinate(1, 2), new Coordinate(1, 2)));
            transImg.Cases.Add(new Case(new Coordinate(1, 2), new Coordinate(1, 2)));

            transImg.Cases[0].ColorRGB = new RGB(12, 34, 238);
            transImg.Cases[2].ColorRGB = new RGB(12, 34, 238);
            transImg.Cases[1].ColorRGB = new RGB(255, 0, 0);

            transImg.ToDMC();
            Assert.Equal(transImg.Cases[0].ColorDMC.B, new RGB(19, 71, 125).B);
            Assert.Equal(transImg.Cases[2].ColorDMC.B, new RGB(19, 71, 125).B);
            Assert.Equal(transImg.Cases[1].ColorDMC, new RGB(250, 50, 3));
        }
        public TestImgTransform()
        {
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var file = Path.Combine(projectFolder, @"../TestImages/pierre-d-argent800.jpg");
            string cheminDestination = file;

            //we record an image
            img = Image.FromFile(cheminDestination);
            transImg = new ImgTransform(img);
        }
    }
}
