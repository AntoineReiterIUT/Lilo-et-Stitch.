using Lilo_et_Stitch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lilo_et_Stitch.Data.DAO_PDF
{
    /// <summary>
    /// Interface for the DAO_PDF class
    /// </summary>
    interface IDAO_PDF
    {
        /// <summary>
        /// Export the generated image to a PDF file
        /// </summary>
        /// <param name="path">The saving path</param>
        /// <param name="generatedImage">The generated image to export</param>
        /// <author>Antoine SAKKALIS</author>
        void ExportPDF(string path, GeneratedImage generatedImage);


        /// <summary>
        /// Get the path of the pdf
        /// </summary>
        /// <returns>The path of the pdf</returns>
        /// <author>Maxime LAFFAYE</author>
        public string GetPath();
    }
}
