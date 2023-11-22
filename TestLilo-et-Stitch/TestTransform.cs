using Lilo_et_Stitch.Data.DAO_PDF;
using Lilo_et_Stitch.Model;
using Lilo_et_Stitch.Model.Transform;
using SixLabors.ImageSharp.Memory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Color = System.Windows.Media.Color;

namespace TestLilo_et_Stitch
{
    /// <summary>
    /// This class will run tests to check if the "Transformer is working as expected" class is working as intented
    /// </summary>
    public class TestTransform
    {

        #region attributes
        private ManagerCreator creator;
        private GeneratedImage? generatedImage;
        private int stepForTests = 4;
        private int maxColorForTests = 454;
        #endregion



        #region operations

        /// <summary>
        /// Test the Grid Creation with a 800 x 800 image 
        /// </summary>
        /// <authors>Mattis GALOPPIN</authors>
        [Fact]
        public void TestMakeGrid()
        {
            //the size of the image to test is 800 by 800

            Assert.Equal(25600, generatedImage.Cases.Count);
        }


        /*
       /// <summary>
       /// Test caseAverage with color pixels
       /// </summary>
       /// <author>Maxime LAFFAYE</author>
       [Fact]
       public void TestCaseAverage()
       {
           //Create a case with pixels of different colors
           Case testCase = new Case(new Coordinate(0, 0), new Coordinate(10, 10));
           testCase.Pixels.Add(new Pixel(new RGB(255, 0, 0), new Coordinate(0, 0),1));
           testCase.Pixels.Add(new Pixel(new RGB(0, 255, 0), new Coordinate(5, 5),1));
           testCase.Pixels.Add(new Pixel(new RGB(0, 0, 255), new Coordinate(10, 10),1));

           //Add the case to the list of cases in ImgTransform
           transImg.Cases.Add(testCase);

           //Call the CaseAverage method
           transImg.CaseAverage(testCase);

           //Check if the average color is correct
           RGB expectedAverageColor = new RGB(85, 85, 85);
           Assert.Equal(expectedAverageColor, testCase.ColorRGB);
       }
        
        */
        /// <summary>
        /// Tests the method to convert RGB colors to the nearest DMC value
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        [Fact]
        public void TestToDMC()
        {      
            //Check if the found color (written in the generated image) is the closest to the DMC one
            Assert.Equal(Color.FromArgb(0, 4, 196, 202), this.generatedImage.Cases[0].TransformedColor);
            Assert.Equal(Color.FromArgb(0, 252, 251, 248).B, this.generatedImage.Cases[1].TransformedColor.B);
        }


        /// <summary>
        /// Constructor of the test
        /// </summary>
        /// <author>Antoine SAKKALIS - Maxime LAFFAYE</author>

        public TestTransform()
        {
            this.creator = new ManagerCreator();
            creator.ManagerWithImage();
            creator.ManagerWithNewPalette();
            generatedImage = this.creator.ManagerWithGeneratedImage(this.stepForTests, this.maxColorForTests, System.Windows.Media.Color.FromRgb(255, 0, 0));
        }


        /// <summary>
        /// Will test if the image is properly generated
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        [Fact]
        public void TestGeneratedImage()
        {
            Assert.NotNull(generatedImage);
        }
        /*
        /// <summary>
        /// Test function FillPixel, the coordonates of the pixels in the case number 3
        /// </summary>
        /// <author>Mattis GALOPPIN</author>
        [Fact]
        public void TestFillPixel()
        {
            bool test = true;
            transImg.Step = 5;
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
        /// Give the result of the number of step
        /// </summary>
        /// <param name="nbFois">number of step</param>
        /// <param name="step">the step of the Grid</param>
        /// <returns>the step * nbFois</returns>
        /// <authors>Mattis GALOPPIN</authors>
        public int Nbstep(int nbFois,int step)
        {
            step = nbFois * step;
            return step;

        }
        */

        /*
        /// <summary>
        /// Testing if the Symbols of the PDF are generated properly.
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        [Fact]
        public void testSymbols()
        {
            bool equalResult = true;

            int countSymbols = 0;
            foreach (string catalogSymbol in pDFforTesting.CatalogString.Values)
            {
                if (catalogSymbol != pDFforTesting.Symbols[countSymbols]) equalResult = false;
                countSymbols++;
            }

            //Checking if the symbols are assigned correctly
            Assert.True(equalResult);

        }
        */
        #endregion

    }
}