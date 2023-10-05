using Lilo_et_Stitch.Model.Transform;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TestLilo_et_Stitch
{
    public class TestTransform
    {
        public Image img;
        [Fact]
        public void TestMakeGrid()
        {

            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var file = Path.Combine(projectFolder, @"../TestImages/pierre-d-argent800.jpg");
            string cheminDestination = file;

            //we record an image
            img = Image.FromFile(cheminDestination);
            ImgTransform trans = new ImgTransform(img);

            //the size of the image to test is 800 by 800
            trans.MakeGrid();
            Assert.Equal(25600,trans.Cases.Count());

        }
        public void TestCaseAverage()
        {

        }
    }
}
