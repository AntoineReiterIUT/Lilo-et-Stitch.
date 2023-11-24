using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lilo_et_Stitch.Data.DAO_Getdata
{
    internal class Imageinfo
    {
        private int _id;
        private string _name;
        private string _path;
        private int _ID_PARAM;
        private byte[] _ImageBytes;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public int ID_PARAM
        {
            get { return _ID_PARAM; }
            set { _ID_PARAM = value; }
        }

        public byte[] ImageBytes
        {
            get { return _ImageBytes; }
            set { _ImageBytes = value; }
        }
    }
}
