using System;
using System.IO;
using Avalonia.Media.Imaging;

namespace Mentor4U_Avalonia.Lib;

public static class ImageHelper
{
    public static Bitmap LoadFromDisk(Uri url)
    {
        var path = url.OriginalString;
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        return new Bitmap(stream);
    }
}
