using Lilo_et_Stitch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lilo_et_Stitch.Data.DAO_PDF
{
    interface IPDF_DAO
    {
        /// <summary>
        /// Export the generated image to a PDF file
        /// </summary>
        /// <param name="path">The saving path</param>
        /// <param name="generatedImage">The generated image to export</param>
        /// <param name="imageStitch">The image stitch to export</param>
        void ExportPDF(string path, GeneratedImage generatedImage);
    }
}
