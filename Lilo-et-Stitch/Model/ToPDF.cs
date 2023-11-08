using Aspose.Pdf;
using Aspose.Pdf.Text;
using Lilo_et_Stitch.Model.Transform;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection.Metadata;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using Color = Aspose.Pdf.Color;
using Document = Aspose.Pdf.Document;

namespace Lilo_et_Stitch.Model
{
    /// <summary>
    /// This class will handle the creation of the PDF
    /// </summary>
    public class ToPDF
    {
        #region attributes
        private string path;
        private Dictionary<RGB, int> catalog;
        private ImgTransform ImgTrans;
        private List<string> Symbols;
        private Dictionary<RGB, string> catalogString;
        private Dictionary<RGB, string> catalogLegend;
        #endregion


        #region operations
        /// <summary>
        /// constructor PDF
        /// </summary>
        /// <param name="imgT">ImgTransform to get the list of cases</param>
        /// <param name="path">Path to save the file</param>
        /// <authors>Mattis Galoppin</authors>

        public ToPDF(ImgTransform imgT,string path) 
        {
            //initialization
            this.path = path;
            this.ImgTrans = imgT;
            this.catalog = new Dictionary<RGB, int>();
            this.Symbols = new List<string> { "☺", "♥", "♦", "♣", "♠","•","◘","○","☻","*","+","-","/","|","&","~","#","`","^","@","°","[","]","=","£","¤","µ","$","%","§","<",">","?","!",":","(",")","{","}","A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z", };
            this.catalogString = new Dictionary<RGB, string>();
            this.catalogLegend = new Dictionary<RGB, string>();
            GenerateCatalog();
            GenerateSymbols();
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
        /// <author>antoine Reiter</author>
        private void AddLegend(Document document)
        {
            List<Page> Pages = new List<Page>();
            Page page = document.Pages.Add();
            int i = 1;
            TextFragment text1;
            text1 = new TextFragment("symbol or integer -> color DMC ");
            text1.IsInLineParagraph = false;
            text1.IsFirstParagraphInColumn = true;
            page.Paragraphs.Add(text1);



            foreach (var item in ImgTrans.UsedColors)
            {
                this.catalogLegend[item.Value] = item.Key;
            }
            foreach (RGB a in catalogLegend.Keys)
            {
               
               
                string x = catalogLegend[a];
                string y = catalogString[a];
                int z = catalog[a];

          


                if (catalogString.ContainsKey(a)){
                    text1 = new TextFragment(y + " -> " + x);
                    
                   
                    
                }
                else if (catalog.ContainsKey(a))
                {
                    text1 = new TextFragment(z + " -> " + x);

                    

                }
                else
                {
                    text1 = new TextFragment("ERROR -> " + x);

        


                }

                if (i==1)
                {
                    i=0;
                    text1.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Left;
                    text1.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Left;
                }

                else if (i == 2)
                {
                    i = 1;
                    text1.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Right;
                    text1.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;
                    text1.IsInLineParagraph = true;
                }

                else if (i==0)
                {
                    i=2;
                    text1.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;
                    text1.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Right;
                    text1.IsInLineParagraph = true;
                    i = 1;
                }
                
                
                
                text1.IsInLineParagraph = false;
                text1.IsKeptWithNext = true;
                page.Paragraphs.Add(text1);

            }
        }

        /// <summary>
        /// write in a PDF the symbols of cases
        /// </summary>
        /// <authors>Antoine Reither - Mattis Galoppin - MaximeLAFFAYE</authors>
        public void WriteInPDF()
        {

            // The path to the documents directory.
            //string dataDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HelloWorld_out.pdf");
            string dataDir = this.path;
            // Initialize document object
            Document document = new Aspose.Pdf.Document();
            document.PageInfo.Margin.Left = 5;
            document.PageInfo.IsLandscape = true;
            // Adding the page
            Page page = document.Pages.Add();

            //adding page 1
            GeneratePage1(document,dataDir);

            //Creating the table
            Aspose.Pdf.Table table = new Aspose.Pdf.Table()
            {
                // Set the table border color as LightGray
                Border = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Color.Black),
                // Set the border for table cells
                DefaultCellBorder = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Color.Black)
            };
            
            table.ColumnWidths = "15 15 15";

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
                    string carac = catalogString[c.ColorDMC];
                    row.Cells.Add(carac.ToString());
                }
            }
            
            document.Pages[2].Paragraphs.Add(table);
           
            

            //Adding the legend
            AddLegend(document);
            // Save updated PDF
            document.Save(dataDir);

            //https://reference.aspose.com/tutorials/pdf/net/programming-with-tables/add-image-in-a-table-cell/
        }
        /// <summary>
        /// genere les symbole dans un nouveau catalogue de caractères
        /// </summary>
        /// <authors>Mattis Galoppin</authors>
        public void GenerateSymbols()
        {
            int i= 0; //index for char
            int j= 0;//index for number
            catalogString.Clear();
            foreach (var item in this.catalog.Keys)
            {
                //if the symbols is not already in the catalogString and i exist in the list
                if ((!this.catalogString.ContainsValue(this.Symbols[i])&&(i < this.Symbols.Count - 1))) 
                {
                    //we put a char
                    this.catalogString.Add(item, this.Symbols[i]);
                    i++; 
                }
                else //we put a number
                {
                    this.catalogString.Add(item,j.ToString());
                    j++;
                }
            }
        }
        /// <summary>
        /// create the first page of the PDF
        /// </summary>
        /// <param name="document">the document</param>
        /// <author>Mattis Galoppin</author>
        public void GeneratePage1(Document document,string dataDir)
        {
            // Create a title
            TextFragment title = new TextFragment("Patron de point de croix");
            title.Position = new Position(250, 540);
            title.TextState.FontSize = 32;

            //adding title to the page
            document.Pages.Add();
            document.Pages[1].Paragraphs.Add(title);

            // Load image into stream
           System.Drawing.Image img = this.ImgTrans.Img;

            
            using (MemoryStream memoryStream = new MemoryStream())
            {
                img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png); //save the image
                Stream imgStream = memoryStream; 
                document.Pages[1].AddImage(imgStream, new Aspose.Pdf.Rectangle(250, 1, 600, 600)); //add image to page at the center

            }

         

           
            
     
        }

        #endregion
    }
}
