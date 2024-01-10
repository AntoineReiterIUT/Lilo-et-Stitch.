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
using System.Windows.Media;

namespace Lilo_et_Stitch.Data.DAO_GetParameters
{
    /// <summary>
    /// Get the parameters of the pattern
    /// </summary>
    public class DAO_GetParameters :IDAO_GetParameters
    {
        #region attributes

        private Dictionary<int, Dictionary<string, BitmapImage>> dico = new Dictionary<int, Dictionary<string, BitmapImage>>();
        private HttpClient client = new HttpClient();
        private string address = ConsAPI.ipAPI + "/GetData";

        #endregion

        #region opertations

        /// <summary>
        /// Get the parameters from the pattern
        /// </summary>
        /// <param name="id_Param">The ID of the concerned pattern</param>
        /// <returns>The settings of the pattern</returns>
        /// <exception cref="Exception">If something unattended happens with the request</exception>
        /// <author>Antoine REITER - Maxime LAFFAYE</author>
        public async Task<Parameters> GetParameters(int id_Param)
        {
            try
            {
                //Creating the endpoint request to get the settings
                string endPoint = $"{address}/GetSettings?Id={id_Param}";

                //Receiving the response status of the request from the server
                HttpResponseMessage response = await client.GetAsync(endPoint);

                //Verifying if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    //Receiving the content of the response
                    string jsonContent = await response.Content.ReadAsStringAsync();

                    //Deserializing the json containing the settings
                    Parameters settings = JsonConvert.DeserializeObject<Parameters>(jsonContent);

                    //Convert the deserialized string to a color and store it in the settings
                    settings.ColorGridMedia = (Color)ColorConverter.ConvertFromString("#" + settings.GridColor);

                    //Return the settings
                    return settings;

                }
                //If the request was not successful, throw an exception
                else
                {
                    throw new Exception("Server error while getting the settings");
                }
            }
            
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }

}
