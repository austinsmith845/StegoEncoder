using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;

namespace StegoEncoder
{
    public class StegoEncoderFactory
    {
        public IEncodeCommand CreateEndcoder(StegoEncoding encoding, Bitmap img, string message)
        {
            switch(encoding)
            {
                case StegoEncoding.LSB: return new LeastSignificantBitEncoding(img, message); 
                default: return null;
            }
        }

        public IEncodeCommand CreateDecoder(StegoEncoding encoding, Bitmap img, int key)
        {
            switch(encoding)
            {
                case StegoEncoding.LSB: return new LeastSignificantBitDecoder(img, key);
                default: return null;
            }

        }
    }
}
