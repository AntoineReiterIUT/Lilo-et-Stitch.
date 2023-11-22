
using System.Drawing;
using System.Windows.Media.Imaging;

public class DAOImage : IDAOImage
{

    #region attributes
    private Image image;
    #endregion


    #region operations
    public void Save(Image image)
    {
        this.image = image;
    }

    
    public Image Load()
    {
        return image;
    }

    #endregion
}
