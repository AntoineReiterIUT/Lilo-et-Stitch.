using Aspose.Pdf;
using Lilo_et_Stitch.Model.Transform;
using System;
using System.Collections.Generic;

namespace Lilo_et_Stitch.Model
{
    public class ToPDF
    {
        //attributies
        private Dictionary<RGB, int> catalog;
        private ImgTransform ImgTrans { get; set; }

        /// <summary>
        /// constructor PDF
        /// </summary>
        /// <param name="imgT">ImgTransform to get the list of cases</param>
        /// <authors>Mattis Galoppin</authors>
        /// 




        public ToPDF(ImgTransform imgT) 
        {
            //initialization
            this.ImgTrans = imgT;
            this.catalog = new Dictionary<RGB, int>();
            GenerateCatalog();
            WriteInPDF();
        }
       /// <summary>
       /// add a number for each colors
       /// </summary>
       /// <authors>Antoine Reither - Mattis Galoppin</authors>
        public void GenerateCatalog()
        {
            int i = 0;
            foreach(var item in ImgTrans.UsedColors) //run the list of used color
            {
                i++;
                this.catalog.Add(item.Value, i); //add a symbol and the color to the catalog
            }
        }
        /// <summary>
        /// write in a PDF the symbols of cases
        /// </summary>
        /// <authors>Antoine Reither - Mattis Galoppin</authors>
        
        public void WriteInPDF()
        {

            // The path to the documents directory.
            string dataDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HelloWorld_out.pdf");

            // Initialize document object
            Document document = new Document();
            // Add page
            Page page = document.Pages.Add();
            // Add text to new page
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Hello World!"));
            // Save updated PDF
            document.Save(dataDir + "HelloWorld_out.pdf");

            foreach (Case c in ImgTrans.Cases) //Scrolls through boxes in the image
            {
                int number = catalog[c.ColorRGB]; //the number of the case
                 //write this number
            }
            
        }
        
    }
}
