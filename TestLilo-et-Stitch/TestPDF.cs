using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Pdf.Operators;
using System.Windows.Markup;
using Lilo_et_Stitch.Model.Transform;
using Microsoft.VisualBasic.ApplicationServices;
using Lilo_et_Stitch.Data.DAO_PDF;

namespace TestLilo_et_Stitch
{
    /// <summary>
    /// Class to test the PDF generation
    /// </summary>
    public class TestPDF
    {
        #region operations

        /// <summary>
        /// Testing if the Symbols of the PDF are generated properly.
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        [Fact]
        public void testSymbols()
        {
            //Creating a manager with an image
            bool equalResult = true;
            ManagerCreator creator = new ManagerCreator();
            creator.ManagerWithImage();
            //Generating the image
            creator.ManagerWithGeneratedImage(4,454, System.Windows.Media.Color.FromRgb(0, 0, 0));
            //Creating a PDF
            DAO_PDF pDFforTesting = new ();
            //Adding the path and the generated image to the PDF
            pDFforTesting.ExportPDF(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"../Data/testPDF.png"), creator.Manager.GeneratedImage);

            //Checking if the symbols are assigned correctly
            int countSymbols = 0;
            foreach (string catalogSymbol in pDFforTesting.CatalogString.Values)
            {
                if (catalogSymbol != pDFforTesting.Symbols[countSymbols]) equalResult = false; //Verify each symbol one by one
                countSymbols++;
            }

            //Asserting the result
            Assert.True(equalResult);
        }

        #endregion
    }
}
