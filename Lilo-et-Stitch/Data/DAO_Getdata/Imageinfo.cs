using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lilo_et_Stitch.Data.DAO_Getdata
{
    /// <summary>
    /// This class will be receiving the data from the getImages endpoint
    /// </summary>
    /// <author>Antoine REITER</author>
    public class Imageinfo
    {
        #region attributes
        private int id;
        private string name;
        private string path;
        private int id_params;
        private byte[] bytes;
        #endregion


        #region operations
        /// <summary>
        /// Property : Read or edit the id of the image within the bdd
        /// </summary>
        public int Id { get => id; set => id = value; }


        /// <summary>
        /// Property : Read or edit the name of the pattern in the bdd
        /// </summary>
        public string Name { get => name; set => name = value; }


        /// <summary>
        /// Property : Read or edit the path to the image
        /// </summary>
        public string Path { get => path; set => path = value; }


        /// <summary>
        /// Property : Read or edit the id of the settings within the bdd
        /// </summary>
        public int Id_params { get => id_params; set => id_params = value; }


        /// <summary>
        /// Property : Read or edit the image as bytes
        /// </summary>
        public byte[] Bytes { get => bytes; set => bytes = value; }

        #endregion

    }
}
