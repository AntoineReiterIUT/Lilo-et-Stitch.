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


        private Dictionary<int, Dictionary<string, BitmapImage>> dico = new Dictionary<int, Dictionary<string, BitmapImage>>();
        private HttpClient client = new HttpClient();
        private string address = "https://localhost:7199/GetData";

        #endregion



        #region operations
        public async void sendPage(int page)
        {          
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
                ConvertToImageDictionary(imageList);

            }
            catch (Exception ex)
            {
                throw new Exception("Erreur : " + ex.Message);
            }
        }

        private void ConvertToImageDictionary(List<Imageinfo> imageList)
        {
            // Parcourez chaque objet ImageInfo dans la liste
            foreach (var imageInfo in imageList)
            {
                // Vérifiez si le dictionnaire externe pour cet ID existe déjà
                if (!dico.ContainsKey(imageInfo.Id))
                {
                    dico[imageInfo.Id] = new Dictionary<string, BitmapImage>();
                }

                // Créez un MemoryStream à partir des bytes de l'image
                using (MemoryStream ms = new MemoryStream(imageInfo.Bytes))
                {
                    // Créez un objet BitmapImage à partir du MemoryStream
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = ms;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();

                    // Ajoutez l'image au dictionnaire interne avec le nom comme clé
                    dico[imageInfo.Id][imageInfo.Name] = bitmapImage;
                }
            }
        }


        public Dictionary<int, Dictionary<string, BitmapImage>> getdico(int page)
        {
            sendPage(page);
            return dico;
        }
        
        
        #endregion
    }
}

