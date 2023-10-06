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
        /// <summary>
        /// test the Grid Creation with an image 800*800
        /// </summary>
        [Fact]
        public void TestMakeGrid()
        {
            //the size of the image to test is 800 by 800

            transImg.Step = 5; //the step is initialized to 5
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
        }/// <summary>
        /// test function FillPixel, the coordonates of the pixels in the case number 3
        /// </summary>
        [Fact]
        public void TestFillPixel()
        {
            bool test = true;
            transImg.MakeGrid();// create the case
            transImg.FillPixels(); //create the pixels
            List<Pixel>PixelC3 = new List<Pixel>(); //list pixel of the case number 3
            int step = transImg.Step;
            
            PixelC3 = transImg.Cases[2].Pixels; //we test the third case
            
            foreach (Pixel p in PixelC3) //we check if the coordinate of Pixel in this case are okay
            {
                Coordinate coord = new Coordinate(0,0);
                if( (p.Coord.X < Nbstep(step,2)) || (p.Coord.X > Nbstep(step,3))) //check x
                {
                    test = false;
                }
                if ( (p.Coord.Y <0 ) || (p.Coord.Y > step)) //check y
                {
                    test = false;
                }
                Assert.True(test);
            }
        }
        /// <summary>
        /// give the result of the number of step
        /// </summary>
        /// <param name="nbFois">number of step</param>
        /// <param name="step">the step of the Grid</param>
        /// <returns>the step * nbFois</returns>
        public int Nbstep(int nbFois,int step)
        {
            step = nbFois * step;
            return step;

        }/// <summary>
        /// Test choiceStep with a img 800*800
        /// </summary>
        [Fact]
        public void TestChoiceStep()
        {
            transImg.ChoiceStep();
            Assert.Equal(10, transImg.Step); 
        }
    }
}
