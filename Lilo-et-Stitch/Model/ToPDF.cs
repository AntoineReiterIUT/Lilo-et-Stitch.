using Lilo_et_Stitch.Model.Transform;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ToPDF(ImgTransform imgT) 
        {
            //initialization
            this.ImgTrans = imgT;
            this.catalog = new Dictionary<RGB, int>();
            GenerateCatalog();
           // WriteInPDF();
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
        /*
        public void WriteInPDF()
        {
            string outputPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.pdf");

            PdfDocument document = new PdfDocument(); //new document

            PdfPage page = document.AddPage(); //new page
            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont font = new XFont("Arial", 12);

            foreach (Case c in ImgTrans.Cases) //Scrolls through boxes in the image
            {
                int number = catalog[c.ColorRGB]; //the number of the case
                gfx.DrawString(number.ToString(), font, XBrushes.Black, new XRect(10, 10, page.Width, page.Height), XStringFormat.TopLeft); //write this number
            }
            document.Save(outputPath);
        }
        */
    }
}
