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
            transImg.Cases[0].ColorRGB = new RGB(12, 34, 238);
            transImg.Cases[1].ColorRGB = new RGB(255, 0, 0);
            transImg.ToDMC();
            Assert.Equal(transImg.Cases[0].ColorDMC.B, new RGB(19, 71, 125).B);
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
        [Fact]
        public void TestFillPixel()
        {
            bool test = true;
            transImg.MakeGrid();// create the case
            transImg.FillPixels(); //create the pixels
            List<Pixel>PixelC3 = new List<Pixel>(); //list pixel of the case number 3

            PixelC3 = transImg.Cases[2].Pixels; //we test the third case

            foreach (Pixel p in PixelC3) //we check if the coordinate of Pixel in this case are okay
            {
                Coordinate coord = new Coordinate(0,0);
                if( (p.Coord.X < 10) || (p.Coord.X > 15)) //check x
                {
                    test = false;
                }
                if ( (p.Coord.Y <0 ) || (p.Coord.Y > 5)) //check y
                {
                    test = false;
                }
                Assert.True(test);
            }
        }
    }
}
