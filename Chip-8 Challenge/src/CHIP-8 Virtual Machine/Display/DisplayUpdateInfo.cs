using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

namespace CHIP_8_Virtual_Machine;

public class DisplayUpdateInfo
{
    public bool[,] Pixels { get; }
    public int Width { get; }
    public int Height { get; }

    public DisplayUpdateInfo(bool[,] pixels, int width, int height)
    {
        Pixels = new bool[width, height];
        Array.Copy(pixels, Pixels, pixels.Length);

        Width = width;
        Height = height;
    }
}