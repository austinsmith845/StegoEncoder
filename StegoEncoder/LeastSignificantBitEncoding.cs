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
    public class LeastSignificantBitEncoding : ICommand
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
            byte[] imageBytes;
            using (MemoryStream stream = new MemoryStream())
            {
                _img.Save(stream, ImageFormat.Png);
                imageBytes = stream.ToArray();
            }
            return imageBytes;
        }
        public void Execute()
        {
            int i = 0;
            foreach(char c in _message)
            {
                byte[] charBits = GetBits((byte)c);
                //Add in LSB insertion here
            }
            
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
