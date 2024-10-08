﻿namespace CHIP_8_Virtual_Machine
{
    public class SystemFont
    {
        private Tribble _address = 0x050;
        private byte[] _bytes = new byte[]
        {
            0xF0, 0x90, 0x90, 0x90, 0xF0, // 0
            0x20, 0x60, 0x20, 0x20, 0x70, // 1
            0xF0, 0x10, 0xF0, 0x80, 0xF0, // 2
            0xF0, 0x10, 0xF0, 0x10, 0xF0, // 3
            0x90, 0x90, 0xF0, 0x10, 0x10, // 4
            0xF0, 0x80, 0xF0, 0x10, 0xF0, // 5
            0xF0, 0x80, 0xF0, 0x90, 0xF0, // 6
            0xF0, 0x10, 0x20, 0x40, 0x40, // 7
            0xF0, 0x90, 0xF0, 0x90, 0xF0, // 8
            0xF0, 0x90, 0xF0, 0x10, 0xF0, // 9
            0xF0, 0x90, 0xF0, 0x90, 0x90, // A
            0xE0, 0x90, 0xE0, 0x90, 0xE0, // B
            0xF0, 0x80, 0x80, 0x80, 0xF0, // C
            0xE0, 0x90, 0x90, 0x90, 0xE0, // D
            0xF0, 0x80, 0xF0, 0x80, 0xF0, // E
            0xF0, 0x80, 0xF0, 0x80, 0x80  // F
        };

        public byte[] this[char c]
        {
            get
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F'))
                {
                    int charIndex = c - '0';
                    return _bytes[charIndex..(charIndex + 5)];
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Character must be between 0 and F");
                }
            }
        }

        public Tribble AddressOf(char c)
        {
            if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F'))
            {
                int charIndex = c - '0';
                return _address + (charIndex * 5);
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Character must be between 0 and F, was {c}");
            }
        }

        public void InstallTo(RAM ram)
        {
            ram.SetBytes(_address, _bytes);
        }
    }
}