
using System.Drawing;
using System.Windows.Media.Imaging;

public class DAOImage : IDAO_Image
{
    private Image image;

    /// <summary>
    /// Save the image in the file
    /// </summary>
    /// <param name="image">Image</param>
    public void Save(Image image)
    {
        image.Save("image.jpg");
    }

    ///
    /// <summary>
    /// Load the image from the file
    /// </summary>
    /// <returns>Image</returns>
    public Image Load()
    {
        return image.FromFile("image.jpg");
    }
}
