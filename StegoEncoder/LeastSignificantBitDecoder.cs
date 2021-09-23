using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;


namespace StegoEncoder
{
    public class LeastSignificantBitDecoder : IEncodeCommand
    {
     
            private Bitmap _img;
       
            private byte[] bytes;

            private int _key;

            public LeastSignificantBitDecoder(Bitmap img, int key)
            {
                _img = img;
            _key = key;
                bytes = GetBytes();


            }

        private byte[] GetBytes()
        {
            byte[] imageBytes = new byte[(_img.Height * _img.Width) * 3];

            int k = 0;
            for (int i = 0; i < _img.Height; i++)
            {
                for (int j = 0; j < _img.Width; j++)
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
                int j = 0;
                int k = 0;
                int letterCount = 0;
                byte[] message = new byte[_key];
                byte[] letterBits = new byte[8];
                int i = 1;
                while( j < message.Length)
                {
                    if(i == 8)//we have done a full character
                    {
                        letterCount = 0;
                        byte letter = parseBits(letterBits);
                        message[j++] = letter;
                        letterBits = new byte[8];
                        i = 0;
                    }
                    byte[] bits = GetBits(bytes[k++]);
                    letterBits[letterCount++] = bits[bits.Length - 1];
                    i++;

                }

                return message;

            }

            private byte parseBits(byte[] bits)
            {
                byte sum = 0;
                int exponent = bits.Length - 1; 
                foreach(byte b in bits)
                {
                    sum += (byte) (b *  (byte)Math.Pow(2, exponent));
                    exponent -= 1;
                }
                return sum;
            }

            private byte[] GetBits(byte b)
            {
                Stack<byte> remainders = new Stack<byte>();
                while (b / 2.0 != 0)
                {
                    remainders.Push((byte)(b % 2));
                    b /= 2;
                }

                if (remainders.Count < 8)
                {
                    remainders.Push(0);
                }

                byte[] bits = new byte[remainders.Count];

                int i = 0;
                while (remainders.Count > 0)
                {
                    bits[i++] = remainders.Pop();
                }

                return bits;
            }
        }
    }


