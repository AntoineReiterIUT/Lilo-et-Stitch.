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
        private string address = "https://localhost:7199/User";

        #endregion



        #region operations
        public async void sendPage(int page)
        {          
            try
            {
                // Spécifiez l'URL de l'API pour recevoir les images
                string endPoint = $"{address}/GetImages?page={page}";

                // Effectuez la requête GET pour obtenir les images
                HttpResponseMessage response = await client.GetAsync(endPoint);

                // Vérifiez si la requête a réussi (code de statut 200 OK)
                if (response.IsSuccessStatusCode)
                {
                    // Lisez le contenu JSON de la réponse
                    string jsonContent = await response.Content.ReadAsStringAsync();

                    // Désérialisez le JSON en une liste d'objets ImageInfo
                    List<Imageinfo> imageList = JsonConvert.DeserializeObject<List<Imageinfo>>(jsonContent);

                    // Convertissez la liste d'images en un dictionnaire
                    ConvertToImageDictionary(imageList);

                    // Utilisez le dictionnaire d'images comme nécessaire
                }
                else
                {
                    // Gérez les erreurs si la requête n'a pas réussi
                    Console.WriteLine("Erreur lors de la récupération des images. Code de statut : " + response.StatusCode);
                    throw new NotImplementedException(); // Ou lancez une exception appropriée selon vos besoins
                }
            }
            catch (Exception ex)
            {
                // Gérez les exceptions
                Console.WriteLine("Erreur : " + ex.Message);
                throw new NotImplementedException();  // Ou lancez une exception appropriée selon vos besoins
            }
        }

        private void ConvertToImageDictionary(List<Imageinfo> imageList)
        {
            // Parcourez chaque objet ImageInfo dans la liste
            foreach (var imageInfo in imageList)
            {
                // Vérifiez si le dictionnaire externe pour cet ID existe déjà
                if (!dico.ContainsKey(imageInfo.ID))
                {
                    dico[imageInfo.ID] = new Dictionary<string, BitmapImage>();
                }

                // Créez un MemoryStream à partir des bytes de l'image
                using (MemoryStream ms = new MemoryStream(imageInfo.ImageBytes))
                {
                    // Créez un objet BitmapImage à partir du MemoryStream
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = ms;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();

                    // Ajoutez l'image au dictionnaire interne avec le nom comme clé
                    dico[imageInfo.ID][imageInfo.Name] = bitmapImage;
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

