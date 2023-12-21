
using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

/// <summary>
/// DAO handling the image
/// </summary>
public class DAOImage : IDAOImage
{
    #region attributes

    private BitmapImage bitmapImage;
    private Image image;

    #endregion


    #region operations

    /// <summary>
    /// Save the image in the file
    /// </summary>
    /// <param name="path">Path to the image</param>
    /// <author>Antoine SAKKALIS</author>
    public void Save(string path)
    {
        //Copy the file to an application directory
        string cheminDestination = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
        File.Copy(path, cheminDestination, true);

        //Load the image into a BitmapImage
        bitmapImage = new BitmapImage();
        bitmapImage.BeginInit();
        bitmapImage.UriSource = new Uri(path);
        bitmapImage.EndInit();

        //Into a drawing image
        using (MemoryStream memoryStream = new MemoryStream())
        {
            //Save the image into a memory stream
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


    public BitmapImage BitmapToBitmapImage(Bitmap bitmap)
    {
        //Creation of the bitmapImage
        BitmapImage bitmapImage = new BitmapImage();

        //Convert it into a bitmapImage
        using (MemoryStream memoryStream = new MemoryStream())
        {
            bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            memoryStream.Seek(0, SeekOrigin.Begin);

            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = memoryStream;
            bitmapImage.EndInit();
        }

        //Return the converted
        return bitmapImage;
    }

    #endregion
}
