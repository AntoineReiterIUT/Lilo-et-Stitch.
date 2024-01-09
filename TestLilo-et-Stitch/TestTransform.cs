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
    /// This class will run tests to check that the Transformer class is working as intended
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
        /// Constructor of the test
        /// </summary>
        /// <author>Antoine SAKKALIS - Maxime LAFFAYE</author>
        public TestTransform()
        {
            //Create a new manager with an image, a new palette and a generated image
            this.creator = new ManagerCreator();
            creator.ManagerWithImage();
            creator.ManagerWithNewPalette();
            generatedImage = this.creator.ManagerWithGeneratedImage(this.stepForTests, this.maxColorForTests, gridColor);
        }


        /// <summary>
        /// Test the Grid Creation with a 8 x 3 image 
        /// </summary>
        /// <authors>Mattis GALOPPIN</authors>
        [Fact]
        public void TestMakeGrid()
        {
            //The number of cases in the generated image should be 2 as the image is 8x3 and the step is 4 (8/4 = 2)
            Assert.Equal(2, generatedImage.Cases.Count);
        }


        /// <summary>
        /// Tests the CaseAverage method to see if it calculates the correct average color
        /// </summary>
        /// <author>Antoine SAKKALIS - Maxime LAFFAYE</author>
        [Fact]
        public void TestCaseAverage()
        {
            //Create a new case with the size of the image
            Case testCase = new Case(new Coordinate(0, 0), new Coordinate(8, 3));

            //Calculate the average color of the case from the image
            Transformer transformer = new Transformer();
            transformer.CaseAverage(testCase, (Bitmap)creator.Manager.ImageStitch.Image);

            //Verify that the average color is correct from a manually calculated value
            Assert.Equal(Color.FromRgb(127, 223, 226), testCase.BaseColor);
        }


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
        /// Will test if the image is properly generated
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        [Fact]
        public void TestGeneratedImage()
        {
            Assert.NotNull(generatedImage);
        }
        

        /// <summary>
        /// Test function FillPixel, the coordinates of the pixels in the case number 3
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

        #endregion

    }
}