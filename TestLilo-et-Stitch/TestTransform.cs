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
        private System.Windows.Media.Color gridColor = System.Windows.Media.Color.FromRgb(255, 0, 0);
        #endregion



        #region operations

        /// <summary>
        /// Test the Grid Creation with a 800 x 800 image 
        /// </summary>
        /// <authors>Mattis GALOPPIN</authors>
        [Fact]
        public void TestMakeGrid()
        {
            //the size of the image to test is 8 by 3

            Assert.Equal(2, generatedImage.Cases.Count);
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
            generatedImage = this.creator.ManagerWithGeneratedImage(this.stepForTests, this.maxColorForTests,gridColor);
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
        

        /// <summary>
        /// Test function FillPixel, the coordonates of the pixels in the case number 3
        /// </summary>
        /// <author>Mattis GALOPPIN</author>
        [Fact]
        public void TestFillPixel()
        {
            bool testSuccess = true;

            foreach(Case c in generatedImage.Cases)
            {
                //Testing if the grid was correctly drown
                for (int i = Math.Max(c.Coord1.X, 0); i < Math.Min(c.Coord2.X, generatedImage.Width); i++) //Foreach pixel of the case's line
                {
                    //Checking if it has the expected color
                    System.Drawing.Color colorPixel = generatedImage.Bitmap.GetPixel(i, c.Coord1.Y);
                    if (!(colorPixel.R == gridColor.R && colorPixel.G == gridColor.G && colorPixel.B == gridColor.B)) testSuccess = false;
                }

                for (int j = Math.Max(c.Coord1.Y, 0); j < Math.Min(c.Coord2.Y, generatedImage.Height); j++) //Foreach pixel of the case's column
                {
                    //Checking if it has the expected color
                    System.Drawing.Color colorPixel = generatedImage.Bitmap.GetPixel(c.Coord1.X, j);
                    if (!(colorPixel.R == gridColor.R && colorPixel.G == gridColor.G && colorPixel.B == gridColor.B)) testSuccess = false;
                }
            }
            Assert.True(testSuccess);
        }

        /*
        
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

        #endregion

    }
}