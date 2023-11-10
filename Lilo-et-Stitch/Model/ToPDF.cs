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
        /// constructor of the PDF
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
            //Adds a page to the document
            List<Page> Pages = new List<Page>();
            Page page = document.Pages.Add();


            TextFragment text1;
            text1 = new TextFragment("symbol or integer -> color DMC "); //writes "symbol or integer -> color DMC" in the PDF
            text1.IsInLineParagraph = false;
            text1.IsFirstParagraphInColumn = true;
            page.Paragraphs.Add(text1);
            text1 = new TextFragment("");
            page.Paragraphs.Add(text1);

            foreach (var item in ImgTrans.UsedColors) // for all items in usedcolor
            {
                this.catalogLegend[item.Value] = item.Key;
            }

            Table table = new Table() // Creates a table that will be used to store the culeur and legend
            {
                Border = new BorderInfo(BorderSide.All, .5f, Color.Black), // Sets the border of the table

                DefaultCellBorder = new BorderInfo(BorderSide.All, .5f, Color.Black) // Sets the default border for cells
            };
            table.ColumnWidths = "10 10 10";

            //Creating the first row manually 
            table.Rows.Add();

            int CellCounter = -1;

            foreach (RGB a in catalogLegend.Keys)  // for every rgb in the keys in catalogueLenged
            {
                // the string and the int  take the value of the differant catalog
                string x = catalogLegend[a];
                string y = catalogString[a];
                int z = catalog[a];


                string information = "";
                if (catalogString.ContainsKey(a)) // see if the RGB a is in the catalogtring where we have all the symbol
                    information = y + " -> " + x;
                else if (catalog.ContainsKey(a)) // see if the rgb is in the catalog where we have the int
                    information = z + " -> " + x;
                else // if there is nothing found for a RGB it will print error
                    information = " ERROR -> " + x;

                //checking if the row isn't full if it is it will create a new row
                if (CellCounter >= 19)
                {
                    table.Rows.Add();
                    CellCounter = -1;
                }

                // used to know wich row we are working on
                Row currentRow = table.Rows[table.Rows.Count - 1];
                // this add the symbol or the number in the cell
                var cell = currentRow.Cells.Add(information);
                cell.ColSpan = 6;

                //this add a view of the color next to the symbol or the number and there DMC color
                currentRow.Cells.Add("");
                currentRow.Cells[CellCounter + 2].BackgroundColor = Color.FromArgb(a.R, a.G, a.B);

                // we add 2 cell in the process so we make the cellcounter go up by 2
                CellCounter += 2;
            }
            // and finally we add the table in the document
            page.Paragraphs.Add(table);
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
            document.PageInfo.Margin.Right = 0;
            document.PageInfo.IsLandscape = true;
            // Adding the page
            Page page = document.Pages.Add();

            //adding page 1
            GeneratePage1(document);

            //Creating the table
            Aspose.Pdf.Table table = new Aspose.Pdf.Table();
            
            table.ColumnWidths = "15 15 15";

            //Changing the color of the borders
            table.DefaultCellBorder = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.FromArgb(this.ImgTrans.GridColor.A, this.ImgTrans.GridColor.R, this.ImgTrans.GridColor.G, this.ImgTrans.GridColor.B)));


            //Calculating the amount of cells and rows
            int nbCol = Math.Max((int)Math.Ceiling((double)this.ImgTrans.Img.Width / this.ImgTrans.Step), 0);
            int nbRows = Math.Max((int)Math.Ceiling((double)this.ImgTrans.Img.Height / this.ImgTrans.Step), 0);

            //adding rows + cells
            int CountCase = -1;
            int rows = 0;
            int columns = 0;
            for (rows = 0; rows < nbRows; rows++)
            {
                Aspose.Pdf.Row row = table.Rows.Add();
                for(columns = 0; columns < nbCol; columns++)
                {
                    CountCase++;
                    //Adding cells + symbols 
                    Case c = ImgTrans.Cases[CountCase];
                    if (c.Opacity > 0) //if the case is not transparent
                    {
                        string carac = catalogString[c.ColorDMC];
                        row.Cells.Add(carac.ToString());
                    }
                    else //if the case is transparent
                    {
                        row.Cells.Add(" ");
                    }
                }
            }

            //Setting the size of the page according to the table, if the table is too small the page will stay at 60 60.
            document.Pages[2].SetPageSize(
                Math.Max(columns * 16 + (document.Pages[2].PageInfo.Margin.Left + document.Pages[2].PageInfo.Margin.Right), 60), //842
                Math.Max(rows * 11 + (document.Pages[2].PageInfo.Margin.Top + document.Pages[2].PageInfo.Margin.Bottom), 60)); //595

            //Adding the table to the page
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
        public void GeneratePage1(Document document)
        {
            // Create a title
            TextFragment title = new TextFragment("Patron de point de croix");
            title.Position = new Position(250, 540);
            title.TextState.FontSize = 32;

            //adding title to the page
            document.Pages.Add();
            document.Pages[1].Paragraphs.Add(title);

            // Load image into stream
            System.Drawing.Image img = this.ImgTrans.NewImage;

            
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
