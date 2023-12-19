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
using SixLabors.ImageSharp.PixelFormats;
using System.Net;

namespace Lilo_et_Stitch.Data.DAO_Publish
{
    /// <summary>
    /// This class will handle the connection with the API
    /// </summary>
    public class DAO_Publish : IDAO_Publish
    {
        #region attributes
        private HttpClient client = new HttpClient();
        private string address = "https://localhost:7199/SendData";
        #endregion


        #region constructor
        /// <summary>
        /// Constructor of the DAO, will handle the certificate
        /// </summary>
        public DAO_Publish()
        {
            var spHandler = new HttpClientHandler();
            {
                spHandler.ServerCertificateCustomValidationCallback = (sender, ClientCertificateOption, chain, SslPolicyErrors) =>
                {
                    return true;
                };
                this.client = new HttpClient(spHandler);
            }
        }
        #endregion


        #region operations
        public async Task CheckIfExist(string name)
        {
            try
            {
                //API endpoint
                string endPoint = $"{address}/CheckIfExist?name={name}";

                //GetRequest and await it's response
                HttpResponseMessage response = await client.GetAsync(endPoint);


                //if it didn't succeed
                if (!response.IsSuccessStatusCode)
                {
                    // If it's a Bad Request
                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        throw new FormatException($"Un patron a déjà ce nom ! {name}");
                    }
                    else // for anything else
                    {
                        throw new FormatException($"Code non attendu: {response.StatusCode}");
                    }
                }
            }
            catch (HttpRequestException ex) // for network issues
            {
                throw new FormatException("Une erreur HTTP s'est produite, veuillez vous connecter à internet.");
            }
            catch (Exception ex) // anything else
            {
                throw new FormatException($"{ex.Message}");
            }
        }



        public async Task Publish(string name, int step, int maxColors, Color gridColor, string pathPdf, string pathPalette, BitmapImage transformedImage)
        {
            try
            {// Will execute all the requests we need to publish a pattern

                //Sending the parameters:
                SendParameters(name, step, maxColors, gridColor);

                //Sending the image
                SendImage(transformedImage, name);

                //Sending the PDF:
                SendPDF(pathPdf,name);

                //Sending the palette:
                SendPalette(pathPalette, name);    
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
            string newColor = gridColor.ToString().Substring(3);

            //And now sending them
            string endPoint = string.Format($"{address}/SendParameters?name={name}&step={step}&maxColors={maxColors}&gridColor={newColor}");
            await client.GetAsync(endPoint);
        }


        /// <summary>
        /// Will send the PDF with the name through the API
        /// </summary>
        /// <param name="path">Path to the PDF file</param>
        /// <param name="name">Name of the pattern</param>
        /// <author>Maxime LAFFAYE</author>
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


        /// <summary>
        /// Will send the Palette (csv) with the name through the API
        /// </summary>
        /// <param name="path">Path to the CSV file</param>
        /// <param name="name">Name of the pattern</param>
        /// <author>Maxime LAFFAYE</author>
        private async void SendPalette(string path,string name)
        {
            try
            {
                //CSV into bytes
                byte[] csvContent = System.IO.File.ReadAllBytes(path);

                //And now sending it
                string endPoint = string.Format($"{address}/AddCSV?name={name}");
                await client.PostAsJsonAsync(endPoint, csvContent);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        /// <summary>
        /// Will send the transformed image with the name through the API
        /// </summary>
        /// <param name="image"></param>
        /// <param name="name"></param>
        /// <author>Maxime LAFFAYE</author>
        private async void SendImage(BitmapImage image,string name)
        {
            try
            {
                //Image into bytes
                byte[] imageContent;

                //Encoding it
                using (MemoryStream stream = new MemoryStream())
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();

                    encoder.Frames.Add(BitmapFrame.Create(image));
                    encoder.Save(stream);

                    //Into an array
                    imageContent = stream.ToArray();
                }

                //And now sending it
                string endPoint = string.Format($"{address}/AddIMG?name={name}");
                await client.PostAsJsonAsync(endPoint, imageContent);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion
    }
}
