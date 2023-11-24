using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Lilo_et_Stitch.Data.DAO_Getdata.DAO_Getdata;
using System.Windows.Media.Imaging;
using System.IO;
using Lilo_et_Stitch.Data.DAO_Getdata;
using System.Windows.Documents;

namespace Lilo_et_Stitch.Data.DAO_GetParameters
{
    
    public class DAO_GetParameters :IDAO_GetParameters
    {
        private Dictionary<int, Dictionary<string, BitmapImage>> dico = new Dictionary<int, Dictionary<string, BitmapImage>>();
        private HttpClient client = new HttpClient();
        private string address = "https://localhost:7199/User";
        private Parameters a = new Parameters();
        public async Task<Parameters> GetParameters(int ID_Param)
        {
            try
            {
                // Spécifiez l'URL de l'API pour recevoir les images
                string endPoint = $"{address}/GetParameters?={ID_Param}";

                // Effectuez la requête GET pour obtenir les images
                HttpResponseMessage response = await client.GetAsync(endPoint);

                // Vérifiez si la requête a réussi (code de statut 200 OK)
                if (response.IsSuccessStatusCode)
                {
                    // Lisez le contenu JSON de la réponse
                    string jsonContent = await response.Content.ReadAsStringAsync();

                    // Désérialisez le JSON en une liste d'objets ImageInfo
                    Parameters ParamList = JsonConvert.DeserializeObject<Parameters>(jsonContent);

                    // Convertissez la liste d'images en un dictionnaire
                    a =  ParamList;

                    // Utilisez le dictionnaire d'images comme nécessaire

                    return a;
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

       public Parameters GetParameters(Task<Parameters> param )
        {
            return param.Result;
        }

        void IDAO_GetParameters.GetParameters(int ID)
        {
            throw new NotImplementedException();
        }
    }
    
}
