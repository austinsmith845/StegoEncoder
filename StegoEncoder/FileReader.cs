using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace StegoEncoder
{
    class FileReader
    {
        public Bitmap ReadImageFile(string path)
        {
            int count;
            if(!File.Exists(path))
            {
                throw new FileNotFoundException("The file specified was not found");
            }

            Bitmap map = new Bitmap(path);
            return map;

            
        }
    }
}
