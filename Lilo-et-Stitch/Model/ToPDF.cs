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
        private ImgTransform ImgTrans;

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
        /// Adds the legend to the PDF
        /// </summary>
        /// <author>MaximeLAFFAYE</author>
        private List<Page> AddLegend()
        {
            List<Page> Pages = new List<Page>();


            return Pages;
        }

        /// <summary>
        /// write in a PDF the symbols of cases
        /// </summary>
        /// <authors>Antoine Reither - Mattis Galoppin - MaximeLAFFAYE</authors>
        public void WriteInPDF()
        {

            // The path to the documents directory.
            string dataDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HelloWorld_out.pdf");

            // Initialize document object
            Document document = new Aspose.Pdf.Document();
            document.PageInfo.Margin.Left = 5;
            document.PageInfo.IsLandscape = true;
            // Adding the page
            Page page = document.Pages.Add();


            //Creating the table
            Aspose.Pdf.Table table = new Aspose.Pdf.Table()
            {
                // Set the table border color as LightGray
                Border = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Color.Black),
                // Set the border for table cells
                DefaultCellBorder = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Color.Black)
            };
            
            table.ColumnWidths = "10 10 10";

            //Calculating the amount of cells and rows
            int nbCol = Math.Max(this.ImgTrans.Img.Width / this.ImgTrans.Step,0);
            int nbRows = Math.Max(this.ImgTrans.Img.Height / this.ImgTrans.Step,0);
            

            //adding rows + cells
            int CountCase = -1;
            for (int i = 0; i < nbRows; i++)
            {
                Aspose.Pdf.Row row = table.Rows.Add();
                for(int j = 0; j < nbCol; j++)
                {
                    CountCase++;
                    //Adding cells + symbols 
                    Case c = ImgTrans.Cases[CountCase];
                    int number = catalog[c.ColorDMC];
                    row.Cells.Add(number.ToString());
                }
            }

            document.Pages[1].Paragraphs.Add(table);
            // Save updated PDF
            document.Save(dataDir);

            //Adding the legend
            List<Page> LegendPages = AddLegend();
            foreach (Page pageL  in LegendPages)
            {
                document.Pages.Add(pageL);
            }
            

            //https://reference.aspose.com/tutorials/pdf/net/programming-with-tables/add-image-in-a-table-cell/
        }
    }
}
