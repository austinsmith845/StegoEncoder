using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace StegoEncoder
{
    public class StegoImage
    {
        private Bitmap _image;
        public Bitmap Image
        {
            get { return _image; }
           
        }


        private string _messageToEncode;
        public string MessageToEncode
        {
            get { return _messageToEncode; }
        }

        public StegoImage(Bitmap img)
        {
            _image = img;
        }
        public StegoImage(Bitmap img, string message)
        {
            _image = img;
            _messageToEncode = message;
        }

        public bool EncodeImage(StegoEncoding encoding)
        {
            IEncodeCommand encoder = new StegoEncoderFactory().CreateEndcoder(encoding, _image, _messageToEncode);

            byte[] bytes = encoder.Execute();

            int k = 0;
            for(int i =0; i< _image.Height; i++)
            {
                for(int j = 0; j< _image.Width; j++)
                {
                    byte r = bytes[k++];
                    byte g = bytes[k++];
                    byte b = bytes[k++];
                    _image.SetPixel(j, i, Color.FromArgb(r, g, b));
                }
            }
                


          
            return true;
        }

        public string DecodeImage(StegoEncoding encoding, int key)
        {
            IEncodeCommand decoder = new StegoEncoderFactory().CreateDecoder(encoding, Image, key);
            byte[] bytes = decoder.Execute();
            int i = 0;
            char[] chars = new char[bytes.Length];
            foreach(byte b in bytes)
            {
                chars[i++] = (char)b;
            }

            return new string(chars);
        }

        public void WriteStegoImage(string fileName)
        {
            int pos = fileName.LastIndexOf(".");
            if(fileName.Substring(pos).ToLower() != ".png")
            {
                fileName = fileName.Substring(0, pos) + ".png";
                Console.WriteLine("Changed file type to .png to prevent loss of data due to lossy compression.");
            }

            _image.Save(fileName, ImageFormat.Png);
        }

        ~StegoImage()
        {
            _image.Dispose();
        }
    }
}
