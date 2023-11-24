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

    public class DAO_Getdata : IDAO_Getdata
    {
        #region attributes
        private HttpClient client = new HttpClient();
        private string address = "https://localhost:7199/GetData";
        //private Dictionary<int, Dictionary<string, BitmapImage>> dico = new Dictionary<int, Dictionary<string, BitmapImage>>();
        #endregion



        #region operations
        /// <summary>
        /// This method will fill the dictionnary with images
        /// </summary>
        /// <param name="page">Page number</param>
        /// <exception cref="Exception">If something happens with the request</exception>
        /// <author>Antoine REITER</author>
        public async Task<Dictionary<int, Dictionary<string, BitmapImage>>> sendPage(int page)
        {
            Dictionary<int, Dictionary<string, BitmapImage>> dico;
            try
            {
                // API endpoint
                string endPoint = $"{address}/GetImages?page={page}";

                // Getting the images
                HttpResponseMessage response = await client.GetAsync(endPoint);
            
                // Reading the json
                string jsonContent = await response.Content.ReadAsStringAsync();

                // Deserializing the json into a list of ImageInfo
                List<Imageinfo> imageList = JsonConvert.DeserializeObject<List<Imageinfo>>(jsonContent);

                // Converting into a dictionnary
                dico = ConvertToImageDictionary(imageList);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.Message);
            }
            return dico;
        }


        /// <summary>
        /// Create a dictionnary that contains a dictionnary : ex IdParams : nameOfPattern , image as a bitmapImage (basically a page)
        /// </summary>
        /// <param name="imageList"></param>
        private Dictionary<int, Dictionary<string, BitmapImage>> ConvertToImageDictionary(List<Imageinfo> imageList)
        {
            Dictionary<int, Dictionary<string, BitmapImage>> dico = new Dictionary<int, Dictionary<string, BitmapImage>>();
           // dico.Clear();

            // For each image within the list of images
            foreach (var imageInfo in imageList)
            {
                // Checking if the dictionnary contains the key or not
                if (!dico.ContainsKey(imageInfo.Id_params))
                {
                    dico[imageInfo.Id_params] = new Dictionary<string, BitmapImage>();
                }

                // Creating the stream with the array of bytes
                using (MemoryStream ms = new MemoryStream(imageInfo.Bytes))
                {
                    // And now creating the bitmapImage
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = ms;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();

                    // adding to the dictionnary
                    dico[imageInfo.Id_params][imageInfo.Name] = bitmapImage;
                }
            }
            //Returning the dictionnary
            return dico;
        }


        public async Task<Dictionary<int, Dictionary<string, BitmapImage>>> GetImages(int page)
        {
            return await sendPage(page);
        }
        
        
        #endregion
    }
}

