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


namespace Lilo_et_Stitch.Data.PDF
{
    /// <summary>
    /// This class will handle the creation of the PDF
    /// </summary>
    /// <Author>Mattis GALOPPIN</Author>
    public class PDF_DAO
    {
        #region attributes
        private string path;
        private Dictionary<RGB, int> catalog;
        private ImgTransform imgTrans;
        private List<string> symbols;
        private Dictionary<RGB, string> catalogString;
        private Dictionary<RGB, string> catalogLegend;

        #endregion

        #region properties
        /// <summary>
        /// Property : read or edit the catalog
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public Dictionary<RGB, int> Catalog { get => catalog; set => catalog = value; }

        /// <summary>
        /// Property : read or edit the list of symbols
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public List<string> Symbols { get => symbols; set => symbols = value; }

        /// <summary>
        /// Property : read or edit the stringcatalog
        /// </summary>
        /// <author>Maxime LAFFAYE</author>
        public Dictionary<RGB, string> CatalogString { get => catalogString; set => catalogString = value; }

        #endregion


        #region operations
        /// <summary>
        /// Constructor of the PDF
        /// </summary>
        /// <param name="imgT">ImgTransform to get the list of cases</param>
        /// <param name="path">Path to save the file</param>
        /// <authors>Mattis GALOPPIN</authors>
        public PDF_DAO(ImgTransform imgT, string path)
        {
            //initialization
            this.path = path;
            imgTrans = imgT;
            Catalog = new Dictionary<RGB, int>();
            symbols = new List<string> { "☺", "♥", "♦", "♣", "♠", "•", "◘", "○", "☻", "*", "+", "-", "/", "|", "&", "~", "#", "`", "^", "@", "°", "[", "]", "=", "£", "¤", "µ", "$", "%", "§", "<", ">", "?", "!", ":", "(", ")", "{", "}", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", };
            CatalogString = new Dictionary<RGB, string>();
            catalogLegend = new Dictionary<RGB, string>();

            GenerateCatalog();
            GenerateSymbols();
            WriteInPDF();
        }


        /// <summary>
        /// Add a symbol for each colors
        /// </summary>
        /// <authors>Antoine REITER - Mattis GALOPPIN</authors>
        public void GenerateCatalog()
        {
            int i = 0;
            foreach (var item in imgTrans.UsedColors) //Loop through the list of used color
            {
                i++;
                Catalog.Add(item.Value, i); //Add a symbol and the color to the catalog
            }
        }


        /// <summary>
        /// Add the legend to the PDF
        /// </summary>
        /// <param name="document">The document to add the legend</param>
        /// <author>Antoine REITER</author>
        private void AddLegend(Document document)
        {
            //Add a page to the document
            List<Page> Pages = new List<Page>();
            Page page = document.Pages.Add();

            //Add a title to the page
            TextFragment text1;
            text1 = new TextFragment("symbol or integer -> color DMC "); //writes "symbol or integer -> color DMC" in the PDF

            //Sets the position of the title
            text1.IsInLineParagraph = false;
            text1.IsFirstParagraphInColumn = true;

            //Sets the font size of the title and add it to the page
            page.Paragraphs.Add(text1);
            text1 = new TextFragment("");
            page.Paragraphs.Add(text1);

            foreach (var item in imgTrans.UsedColors) //For all items in usedcolor
            {
                catalogLegend[item.Value] = item.Key;
            }

            Table table = new Table() //Creates a table that will be used to store the couleur and legend
            {
                Border = new BorderInfo(BorderSide.All, .5f, Color.Black), //Sets the border of the table

                DefaultCellBorder = new BorderInfo(BorderSide.All, .5f, Color.Black) //Sets the default border for cells
            };

            table.Margin.Left = 30;
            table.ColumnWidths = "10 10 10";

            //Creating the first row manually 
            table.Rows.Add();

            int CellCounter = -1;

            foreach (RGB a in catalogLegend.Keys)  //For each color in the catalogLegend
            {
                //We get the symbol, the color and the number
                string x = catalogLegend[a];
                string y = CatalogString[a];
                int z = Catalog[a];

                string information = "";
                if (CatalogString.ContainsKey(a)) //See if the rgb is in the catalog where we have the symbol
                    information = " " + y + " -> " + x;
                else if (Catalog.ContainsKey(a)) //See if the rgb is in the catalog where we have the number
                    information = " " + z + " -> " + x;

                else //If there's nothing found for a RGB it will print an error
                    information = " ERROR -> " + x;

                //Checking if the row isn't full if it is it will create a new row
                if (CellCounter >= 19)
                {
                    table.Rows.Add();
                    CellCounter = -1;
                }

                //Used to know wich row we are working on
                Row currentRow = table.Rows[table.Rows.Count - 1];
                //This add the symbol or the number in the cell
                var cell = currentRow.Cells.Add(information);
                cell.ColSpan = 6;

                //Add a view of the color next to the symbol or the number and there DMC color
                currentRow.Cells.Add("");
                currentRow.Cells[CellCounter + 2].BackgroundColor = Color.FromArgb(a.R, a.G, a.B);

                CellCounter += 2;
            }

            //And finally we add the table in the document
            page.Paragraphs.Add(table);
        }


        /// <summary>
        /// Write in a PDF the symbols of the generated cases
        /// </summary>
        /// <authors>Antoine REITER - Mattis GALOPPIN - Maxime LAFFAYE - Antoine SAKKALIS</authors>
        public void WriteInPDF()
        {
            string dataDir = path;

            //Initialize document object and properties
            Document document = new Document();
            document.PageInfo.Margin.Left = 5;
            document.PageInfo.Margin.Right = 0;
            document.PageInfo.IsLandscape = true;

            //Add the page to the document
            Page page = document.Pages.Add();

            //adding page 1
            GeneratePage1(document);

            //Creating the table
            Table table = new Table();

            //Setting the margin of the table
            table.ColumnWidths = "15 15 15";

            //Changing the color of the borders
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, .5f, Color.FromRgb(System.Drawing.Color.FromArgb(imgTrans.GridColor.A, imgTrans.GridColor.R, imgTrans.GridColor.G, imgTrans.GridColor.B)));

            //Calculating the amount of cells and rows
            int nbCol = Math.Max((int)Math.Ceiling((double)imgTrans.Img.Width / imgTrans.Step), 0);
            int nbRows = Math.Max((int)Math.Ceiling((double)imgTrans.Img.Height / imgTrans.Step), 0);

            //Adding rows and cells
            int CountCase = -1;
            int rows = 0;
            int columns = 0;
            for (rows = 0; rows < nbRows; rows++)
            {
                Row row = table.Rows.Add();
                for (columns = 0; columns < nbCol; columns++)
                {
                    CountCase++;
                    //Adding cells + symbols 
                    Case c = imgTrans.Cases[CountCase];
                    if (c.Opacity > 0) //if the case is not transparent
                    {
                        string carac = CatalogString[c.ColorDMC];
                        var newCell = row.Cells.Add(" " + carac.ToString());
                        //newCell.BackgroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.FromArgb(250,c.ColorDMC.R,c.ColorDMC.G,c.ColorDMC.B));
                        //For future use, ^
                    }
                    else //if the case is transparent
                    {
                        row.Cells.Add(" ");
                    }
                }
            }

            //Setting the size of the page according to the table, if the table is too small the page will stay at 60 60.
            document.Pages[2].SetPageSize(
                Math.Max(columns * 16 + (document.Pages[2].PageInfo.Margin.Left + document.Pages[2].PageInfo.Margin.Right), 60),
                Math.Max(rows * 11 + (document.Pages[2].PageInfo.Margin.Top + document.Pages[2].PageInfo.Margin.Bottom), 60));

            //Adding the table to the page
            document.Pages[2].Paragraphs.Add(table);

            AddLegend(document);

            document.Save(dataDir);
        }


        /// <summary>
        /// Generate the symbols for the PDF
        /// </summary>
        /// <authors>Mattis GALOPPIN</authors>
        public void GenerateSymbols()
        {
            int i = 0; //Index for char
            int j = 0;//Index for number
            CatalogString.Clear();
            foreach (var item in Catalog.Keys)
            {
                //If the symbols is not already in the catalogString and i exist in the list
                if (!CatalogString.ContainsValue(symbols[i]) && i < symbols.Count - 1)
                {
                    //We put a char
                    CatalogString.Add(item, symbols[i]);
                    i++;
                }
                else //We put a number
                {
                    CatalogString.Add(item, j.ToString());
                    j++;
                }
            }
        }


        /// <summary>
        /// Create the first page of the PDF
        /// </summary>
        /// <param name="document">the document</param>
        /// <author>Mattis Galoppin</author>
        public void GeneratePage1(Document document)
        {
            //Create the title
            TextFragment title = new TextFragment("Patron de point de croix");
            title.Position = new Position(250, 540);
            title.TextState.FontSize = 32;

            //Adding title to the page
            document.Pages.Add();
            document.Pages[1].Paragraphs.Add(title);

            //Create the image
            System.Drawing.Image img = imgTrans.NewImage;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png); //Save the image
                Stream imgStream = memoryStream;  //Load the image into stream
                document.Pages[1].AddImage(imgStream, new Aspose.Pdf.Rectangle(250, 1, 600, 600)); //Add image to page at the center
            }
        }
        #endregion
    }
}
