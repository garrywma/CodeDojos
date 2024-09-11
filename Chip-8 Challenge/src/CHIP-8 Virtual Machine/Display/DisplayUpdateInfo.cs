namespace CHIP_8_Virtual_Machine;

public class DisplayUpdateInfo
{
    public bool[,] Pixels { get; }
    public int Width { get; }
    public int Height { get; }


    public DisplayUpdateInfo(bool[,] pixels, int width, int height)
    {
        Width = width;
        Height = height;

        Pixels = new bool[width, height];
        Array.Copy(pixels, Pixels, pixels.Length);
    }
}