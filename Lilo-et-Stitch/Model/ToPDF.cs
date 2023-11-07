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
    }
}
