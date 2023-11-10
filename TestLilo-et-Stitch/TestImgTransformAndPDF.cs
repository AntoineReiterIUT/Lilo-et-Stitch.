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
    /// <summary>
    /// This class will run tests to check if the "ImgTransform" class is working as intented
    /// </summary>
    public class TestImgTransformAndPDF
    {
        #region attributes
        private Image img;
        private ImgTransform transImg;

        private int maxColorForTests = 20;
        private ToPDF pDFforTesting;
        private string pathToTest = @"../Data/testPDF.pdf";
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
            
            Assert.Equal(25600,transImg.Cases.Count());
        }


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


        /// <summary>
        /// Tests the method to convert RGB colors to the nearest DMC value
        /// </summary>
        /// <author>Antoine SAKKALIS</author>
        [Fact]
        public void TestToDMC()
        {
            //Clearing so we can test on fake cases
            transImg.Cases.Clear();

            
            transImg.Cases.Add(new Case(new Coordinate(1,2), new Coordinate(1, 2)));
            transImg.Cases.Add(new Case(new Coordinate(1, 2), new Coordinate(1, 2)));
            transImg.Cases.Add(new Case(new Coordinate(1, 2), new Coordinate(1, 2)));
            

            transImg.Cases[0].ColorRGB = new RGB(12, 34, 238);
            transImg.Cases[2].ColorRGB = new RGB(12, 34, 238);
            transImg.Cases[1].ColorRGB = new RGB(255, 0, 0);

            transImg.ToDMC(transImg.Cases[0]);
            transImg.ToDMC(transImg.Cases[2]);
            transImg.ToDMC(transImg.Cases[1]);
            

            Assert.Equal(transImg.Cases[0].ColorDMC.B, new RGB(19, 71, 165).B);
            Assert.Equal(transImg.Cases[2].ColorDMC.B, new RGB(19, 71, 165).B);
            Assert.Equal(transImg.Cases[1].ColorDMC, new RGB(161, 75, 81));
        }


        /// <summary>
        /// Constructor of the test
        /// </summary>
        /// <author>Antoine SAKKALIS - Maxime LAFFAYE</author>
        public TestImgTransformAndPDF()
        {
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var file = Path.Combine(projectFolder, @"../TestImages/pierre-d-argent800.jpg");
            string cheminDestination = file;

            //we record an image
            img = Image.FromFile(cheminDestination);
            transImg = ImgTransform.Instance;
            transImg.TransformingIMG(img,5,this.maxColorForTests,System.Windows.Media.Color.FromRgb(255, 0, 0));


            //Preparing for the PDF tests
            var filePDF = Path.Combine(projectFolder, this.pathToTest);
            transImg.NewImage = img;
            this.pDFforTesting = new ToPDF(transImg, filePDF);
        }


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
        #endregion
    }
}
