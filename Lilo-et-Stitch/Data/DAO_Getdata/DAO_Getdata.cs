using Aspose.Pdf.Operators;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace Lilo_et_Stitch.Data.DAO_Getdata
{
    /// <summary>
    /// DAO to get the data from the API
    /// </summary>
    public class DAO_Getdata : IDAO_Getdata
    {
        #region attributes

        private HttpClient client = new HttpClient();
        private string address = ConsAPI.ipAPI + "/GetData";

        #endregion


        #region operations

        /// <summary>
        /// This method will fill the dictionnary with images
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="patternName">Name of the patterns to look for</param>
        /// <exception cref="Exception">If something happens with the request</exception>
        /// <author>Antoine REITER - Maxime LAFFAYE</author>
        public async Task<(int, Dictionary<int, Dictionary<string, BitmapImage>>)> sendPage(int page, string patternName)
        {
            Dictionary<int, Dictionary<string, BitmapImage>> dico;
            int nbPages = 0;
            try
            {
                //API endpoints
                string endPointImages = $"{address}/GetImgBySearch";

                //If there is no pattern name, we use only the page
                if (patternName == "") endPointImages += $"?page={page}";
                else endPointImages += $"?name={patternName}&page={page}";

                //To get the number of pages
                string endPointNbPages = $"{address}/GetNumberPage?word={patternName}";

                //Getting the responses
                HttpResponseMessage responseImg = await client.GetAsync(endPointImages);
                HttpResponseMessage responseNbPages = await client.GetAsync(endPointNbPages);

                //Reading the json
                string jsonContentImg = await responseImg.Content.ReadAsStringAsync();
                string nbPageResult = await responseNbPages.Content.ReadAsStringAsync();

                //Deserializing the json into a list of ImageInfo
                List<Imageinfo> imageList = JsonConvert.DeserializeObject<List<Imageinfo>>(jsonContentImg);
                nbPages = JsonConvert.DeserializeObject<int>(nbPageResult);

                //Converting into a dictionnary
                dico = ConvertToImageDictionary(imageList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return (nbPages, dico);
        }


        /// <summary>
        /// Create a dictionnary that contains a dictionnary : ex IdParams : nameOfPattern , image as a bitmapImage (basically a page)
        /// </summary>
        /// <param name="imageList">The list of images to convert</param>
        /// <author>Antoine REITER - Maxime LAFFAYE</author>
        private Dictionary<int, Dictionary<string, BitmapImage>> ConvertToImageDictionary(List<Imageinfo> imageList)
        {
            Dictionary<int, Dictionary<string, BitmapImage>> dico = new Dictionary<int, Dictionary<string, BitmapImage>>();
            // dico.Clear();

            //For each image within the list of images
            foreach (var imageInfo in imageList)
            {
                //Checking if the dictionnary contains the key or not
                if (!dico.ContainsKey(imageInfo.Id_params))
                {
                    dico[imageInfo.Id_params] = new Dictionary<string, BitmapImage>();
                }

                //Creating the stream with the array of bytes
                using (MemoryStream ms = new MemoryStream(imageInfo.Bytes))
                {
                    // And now creating the bitmapImage
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = ms;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();

                    //Adding to the dictionnary
                    dico[imageInfo.Id_params][imageInfo.Name] = bitmapImage;
                }
            }

            //Returning the dictionnary
            return dico;
        }


        /// <summary>
        /// This method will get the PDF from the api and save it locally 
        /// </summary>
        /// <param name="patternName">Name of the pattern</param>
        /// <param name="path">Save path (with the name) of the rebuilt pdf</param>
        /// <returns>Nothing</returns>
        /// <exception cref="Exception">Exception if something happens with the request</exception>
        /// <author>Maxime LAFFAYE</author>
        public async Task SavePattern(string patternName, string path)
        {
            try
            {
                //API endpoint
                string endPoint = $"{address}/GetPDF?name={patternName}";

                //Getting the PDF
                HttpResponseMessage response = await client.GetAsync(endPoint);

                //Reading the json
                string jsonContent = await response.Content.ReadAsStringAsync();

                //Deserializing the json into a list of bytes
                byte[] pdfAsByt = JsonConvert.DeserializeObject<byte[]>(jsonContent);

                //Rebuilding the file
                if (pdfAsByt != null)
                {
                    //Building the path & rebuilding the file
                    File.WriteAllBytesAsync(path, pdfAsByt);
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// This method will return the images based on the given page number and the pattern name
        /// </summary>
        /// <param name="page">The page to return</param>
        /// <param name="patternName">The name of the pattern to look for</param>
        /// <returns>A dictionnary that contains a dictionnary : ex IdParams : nameOfPattern , image as a bitmapImage (AKA a page)</returns>
        /// <author>Maxime LAFFAYE</author>
        public async Task<(int, Dictionary<int, Dictionary<string, BitmapImage>>)> GetImages(int page, string patternName)
        {
            return await sendPage(page, patternName);
        }

        #endregion
    }
}

