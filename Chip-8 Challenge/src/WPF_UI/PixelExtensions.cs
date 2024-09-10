namespace Chip8.UI.Wpf
{
    public static class PixelExtensions
    {
        public static byte[] ToRGBA(this bool[,] pixels, int magnification, out (int Width, int Height) dimensions)
        {
            int width = pixels.GetLength(0) * magnification;
            int height = pixels.GetLength(1) * magnification;
            dimensions = (width, height);

            byte[] pixelBytes = new byte[width * height * 4];
            int pixelIndex = 0;

            // x left to right, add 4 bytes per pixel (RGBA), then next line
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte pixel = (byte)(pixels[x / magnification, y / magnification] ? 255 : 0);
                    pixelBytes[pixelIndex++] = pixel; // Red
                    pixelBytes[pixelIndex++] = pixel; // Green
                    pixelBytes[pixelIndex++] = pixel; // Blue
                    pixelBytes[pixelIndex++] = 255;   // Alpha (always 0xFF)
                }
            }

            return pixelBytes;
        }
    }
}
