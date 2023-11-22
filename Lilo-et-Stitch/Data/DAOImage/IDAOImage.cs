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
    /// <param name="path">Path to the image</param>
    /// /// <author>Antoine SAKKALIS</author>
    public void Save(string path);


    /// <summary>
    /// Load the image from the file
    /// <returns>Image</returns>
    /// <author>Antoine SAKKALIS</author>
    public Image LoadImage();


    /// <summary>
    /// Load a BitmapImage to display in the IHM
    /// </summary>
    /// <returns>A BitmapImage</returns>
    /// <author>Maxime LAFFAYE</author>
    public BitmapImage LoadBitmapImage();


}