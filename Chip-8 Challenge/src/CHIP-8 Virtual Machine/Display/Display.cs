﻿namespace CHIP_8_Virtual_Machine;

public record SpriteInfo(int X, int Y, bool[,] Pixels);

public class Display
{
    public const int DISPLAY_WIDTH = 64;
    public const int DISPLAY_HEIGHT = 32;

    private VM _vm;
    private bool[,] _pixels;

    public event EventHandler<bool[,]> OnDisplayUpdated;
    public event EventHandler<SpriteInfo> OnSpriteDisplayed;
    public event EventHandler OnDisplayCleared;

    public bool[,] Pixels => _pixels;

    public Display(VM vm)
    {
        _pixels = new bool[64, 32];
        _vm = vm;
    }

    public void Clear()
    {
        _pixels = new bool[64, 32];
        OnDisplayCleared?.Invoke(this, EventArgs.Empty);
        OnDisplayUpdated?.Invoke(this, _pixels);
    }

    public bool DisplayChar(int x, int y, char c)
    {
        byte[] sprite = _vm.SystemFont[c];
        return DisplaySprite(x, y, sprite);
    }

    public bool DisplaySprite(int x, int y, byte[] spriteBytes)
    {
        bool pixelErased = false;
        bool[,] spritePixels = new bool[8, spriteBytes.Length];

        try
        {
            for (int i = 0; i < spriteBytes.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    byte spriteByte = spriteBytes[i];
                    int localX = (x + (7 - j));
                    int localY = (y + i);

                    if (localX >= DISPLAY_WIDTH || localY >= DISPLAY_HEIGHT) continue;

                    bool currentPixelState = _pixels[localX, localY];
                    bool newPixelState = (spriteByte & (0x1 << j)) != 0; // checks if bit j is set

                    if (currentPixelState && newPixelState)
                    {
                        pixelErased = true;
                    }

                    _pixels[localX, localY] = currentPixelState ^ newPixelState;
                    spritePixels[(7 - j), i] = _pixels[localX, localY];
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }

        OnSpriteDisplayed?.Invoke(this, new SpriteInfo(x, y, spritePixels));
        OnDisplayUpdated?.Invoke(this, _pixels);

        return pixelErased;
    }
}
