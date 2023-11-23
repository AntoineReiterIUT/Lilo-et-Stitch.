using Microsoft.VisualBasic.ApplicationServices;
using PdfSharp.BigGustave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using static System.Net.WebRequestMethods;
using Aspose.Pdf.Operators;

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

            try
            {// Will execute all the requests we need to publish a pattern

                //Sending the parameters:
                SendParameters(name, step, maxColors, gridColor);

                //Sending the PDF:
                SendPDF(pathPdf,name);
            }
            catch (HttpRequestException ex)
            {
                throw new FormatException("Une erreur HTTP s'est produite, veuillez vous connecter à internet.");
            }
            catch
            {
                throw new FormatException($"Paramètres manquant!");
            }
        }


        /// <summary>
        /// Will send the parameters through the API
        /// </summary>
        /// <param name="name"></param>
        /// <param name="step"></param>
        /// <param name="maxColors"></param>
        /// <param name="gridColor"></param>
        /// <author>Maxime LAFFAYE</author>
        private async void SendParameters(string name, int step, int maxColors, Color gridColor)
        {
            //Removing # at the start of the HEX color
            string newColor = gridColor.ToString().Substring(1);

            //And now sending them
            string endPoint = string.Format($"{address}/SendParameters?name={name}&step={step}&maxColors={maxColors}&gridColor={newColor}");
            await client.GetAsync(endPoint);
        }


        private async void SendPDF(string path,string name)
        {
            try
            {
                //PDF into bytes
                byte[] pdfContent = System.IO.File.ReadAllBytes(path);

                //And now sending it
                string endPoint = string.Format($"{address}/AddPDF?name={name}");
                await client.PostAsJsonAsync(endPoint,pdfContent);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
