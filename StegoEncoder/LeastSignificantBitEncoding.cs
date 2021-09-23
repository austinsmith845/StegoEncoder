using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace StegoEncoder
{
    /// <summary>
    /// This class will implement the least significat bit algorithm of steganography
    /// </summary>
    public class LeastSignificantBitEncoding : IEncodeCommand
    {
        private Bitmap _img;
        private string _message;
        private byte[] bytes;
        public LeastSignificantBitEncoding(Bitmap img, string message)
        {
            _img = img;
            bytes = GetBytes();
          
            if (message.Length * 8 > bytes.Length)
            {
                throw new ArgumentException("The inputted string is too long to be encoded in the specified image");
            }
            
            _message = message;

        }

        private byte[] GetBytes()
        {
            byte[] imageBytes = new byte[(_img.Height * _img.Width) * 3];

            int k = 0;
            for(int i = 0; i < _img.Height; i++ )
            {
                for(int j = 0; j < _img.Width; j++)
                {
                    Color col = _img.GetPixel(j, i);
                    imageBytes[k++] = col.R;
                    imageBytes[k++] = col.G;
                    imageBytes[k++] = col.B;
                }
            }
            
            return imageBytes;
        }
        public byte[] Execute()
        {
            int i = 0;
           
          
            foreach(char c in _message)
            {
                byte[] charBits = GetBits((byte)c);
                foreach(byte b in charBits)
                {
                    byte[] picBits = GetBits(bytes[i]);
                    picBits[picBits.Length - 1] = b;
                    bytes[i] = parseBits(picBits);
                    i++;
                }
            }
            return bytes;
            
        }

        private byte parseBits(byte[] bits)
        {
            byte sum = 0;
            int exponent = bits.Length - 1;
            foreach (byte b in bits)
            {
                sum += (byte)(b * (byte)Math.Pow(2, exponent));
                exponent -= 1;
            }
            return sum;
        }

        private byte[] GetBits(byte b)
        {
            Stack<byte> remainders = new Stack<byte>();
            while(b / 2.0 != 0)
            {
                remainders.Push((byte) (b % 2));
                b /= 2;
            }

            if(remainders.Count<8)
            {
                remainders.Push(0);
            }

            byte[] bits = new byte[remainders.Count];

            int i = 0;
            while(remainders.Count > 0)
            {
                bits[i++] = remainders.Pop();
            }

            return bits;
        }
    }
}
