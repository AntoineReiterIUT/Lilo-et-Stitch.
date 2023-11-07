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
        private Dictionary<Case, string> caseetnum;
        private ImgTransform ImgTrans { get; set; }

        /// <summary>
        /// constructor PDF
        /// </summary>
        /// <param name="imgT">ImgTransform to get the list of cases</param>
        /// <authors>Mattis Galoppin</authors>
        public ToPDF(ImgTransform imgT) 
        {
            this.ImgTrans = imgT;
        }
        
        public void GeneratePDF()
        {
            string outputPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.pdf");

            PdfDocument document = new PdfDocument();

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont font = new XFont("san serif",12);
            gfx.DrawString("hello world !",font,XBrushes.Black, new XRect(10,10,page.Width,page.Height),XStringFormat.TopLeft);
            
            document.Save(outputPath);
        }
        
        public void generateINTofCASE()
        {
            
        }
        public void WriteInPDF()
        {
            
        }
    }
}
