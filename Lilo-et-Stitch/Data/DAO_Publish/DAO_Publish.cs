using Microsoft.VisualBasic.ApplicationServices;
using PdfSharp.BigGustave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using static System.Net.WebRequestMethods;

namespace Lilo_et_Stitch.Data.DAO_Publish
{
    /// <summary>
    /// This class will handle the connection with the API
    /// </summary>
    public class DAO_Publish : IDAO_Publish
    {
        #region attributes
        private HttpClient client = new HttpClient();
        private string address = "https://localhost:7199/User";
        #endregion


        #region operations
        public async Task CheckIfExist(string name)
        {
            try
            {
                string endPoint = string.Format($"{address}/CheckIfExist?name={name}");
                HttpResponseMessage response = await client.GetAsync(endPoint);
            }
            catch (HttpRequestException ex)
            {
                throw new FormatException("Une erreur HTTP s'est produite, veuillez vous connecter à internet.");
            }catch
            {
                throw new FormatException($"Un patron contient déjà ce nom : {name}");
            }

        }

        public async Task Publish(string name, int step, int maxColors, Color gridColor, string pathPdf, string pathPalette, BitmapImage transformedImage)
        {

            throw new NotFiniteNumberException();
        }
        #endregion
    }
}
