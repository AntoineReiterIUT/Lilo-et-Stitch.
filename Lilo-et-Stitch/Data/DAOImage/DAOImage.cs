
using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

public class DAOImage : IDAOImage
{

    #region attributes
    private BitmapImage bitmapImage;
    private Image image;
    #endregion


    #region operations
    public void Save(string path)
    {
        //Copy the file to an application directory
        string cheminDestination = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

        //Load the image into a BitmapImage
        bitmapImage = new BitmapImage();
        bitmapImage.BeginInit();
        bitmapImage.UriSource = new Uri(path);
        bitmapImage.EndInit();

        //Into a drawing image
        using (MemoryStream memoryStream = new MemoryStream())
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            encoder.Save(memoryStream);

            this.image = System.Drawing.Image.FromStream(memoryStream);
        }
    }

    public Image LoadImage()
    {
        return image;
    }


    public BitmapImage LoadBitmapImage()
    {
        return bitmapImage;
    }
    #endregion
}
