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
        private Dictionary<int, string> numetcolor;
        private ImgTransform ImgTrans { get; set; }
        private Dictionary<string, RGB> usedcolor;
        private Dictionary<Case, int> numetcase ;
        private List<Case> cases ;
        /// <summary>
        /// constructor PDF
        /// </summary>
        /// <param name="imgT">ImgTransform to get the list of cases</param>
        /// <authors>Mattis Galoppin</authors>
        public ToPDF(ImgTransform imgT) 
        {
            //initialization
            this.ImgTrans = imgT;
            this.numetcolor = new Dictionary<int, string>();
            this.numetcase = new Dictionary<Case, int>();
        }
        
        public void generateINTofCOLOR()
        {
            usedcolor = ImgTrans.UsedColors;
            int i = 0;
            foreach( string a in usedcolor.Keys )
            {

                i++;
                numetcolor.Add(i,a);
                
                

            }
            
            
        }
        public void generateINTofCASE()
        {
           cases = ImgTrans.Cases;
            int i = 0;
            foreach(Case c in cases)
            {
                i++;
                numetcase[c] = i;
            }
        }
        /// <summary>
        /// write in a PDF the symbols of cases
        /// </summary>
        public void WriteInPDF()
        {
            string outputPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.pdf");

            PdfDocument document = new PdfDocument(); //new document

            PdfPage page = document.AddPage(); //new page
            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont font = new XFont("san serif", 12);

            foreach (Case c in ImgTrans.Cases) //Scrolls through boxes in the image
            {
                int number = numetcase[c]; //the number of the case
                gfx.DrawString(number.ToString(), font, XBrushes.Black, new XRect(10, 10, page.Width, page.Height), XStringFormat.TopLeft); //write this number
            }
            document.Save(outputPath);
        }
    }
}
