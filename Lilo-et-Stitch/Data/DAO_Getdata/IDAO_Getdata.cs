using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Lilo_et_Stitch.Data.DAO_Getdata
{
    /// <summary>
    /// This interface will be used as a link with the dao to get the datas
    /// </summary>
    public interface IDAO_Getdata
    {
        /// <summary>
        /// This method will return the images based on the given page number
        /// </summary>
        /// <param name="page"></param>
        /// <returns>A dictionnary that contains a dictionnary : ex IdParams : nameOfPattern , image as a bitmapImage (AKA a page)</returns>
        /// <author>Maxime LAFFAYE</author>
        public Dictionary<int, Dictionary<string, BitmapImage>> GetImages(int page);

    }
}
