using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;

namespace StegoEncoder
{
    public class StegoEncoderFactory
    {
        public ICommand CreateEndcoder(StegoEncoding encoding, Bitmap img, string message)
        {
            switch(encoding)
            {
                case StegoEncoding.LSB: return new LeastSignificantBitEncoding(img, message); 
                default: return null;
            }
        }
    }
}
