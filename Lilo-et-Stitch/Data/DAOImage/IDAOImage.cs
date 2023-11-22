using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

public interface IDAOImage
{

    /// <summary>
    /// Save the image in the file
    /// </summary>
    /// <param name="image">Image</param>
    /// /// <author>Antoine SAKKALIS</author>
    public void Save(Image image);

    /// <summary>
    /// Load the image from the file
    /// </summary>
    /// <returns>Image</returns>
    /// <author>Antoine SAKKALIS</author>
    public Image Load();
}