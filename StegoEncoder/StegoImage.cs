using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

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

        public StegoImage(Bitmap img, string message)
        {
            _image = img;
            _messageToEncode = message;
        }
        

    }
}
