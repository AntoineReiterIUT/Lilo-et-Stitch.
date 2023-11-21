
using System.Drawing;
using System.Windows.Media.Imaging;

public class DAOImage : IDAO_Image
{

    #region attributes
    private Image image;
    #endregion


    #region operations
    public void Save(Image image)
    {
        image.Save("image.jpg");
    }

    
    public Image Load()
    {
        return image;
    }

    #endregion
}
