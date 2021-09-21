using System;
using System.Drawing;
using System.IO;

namespace StegoEncoder
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;
            string message;
            if(args.Length > 0)
            {
                if (args.Length < 2)
                {
                    Console.WriteLine("You must specify both the image and message using command line parameters");
                    return;
                }
                else
                {
                    path = args[0];
                    message = args[1];
                }
            }
            {
                Console.WriteLine("Enter the path of the image file to hide the text in:");
                path = Console.ReadLine();

                Console.WriteLine("Enter text to hide:");
                message = Console.ReadLine();
            }

            FileReader reader = new FileReader();
            Bitmap image = reader.ReadImageFile(path);

            StegoImage stegoImage = new StegoImage(image, message);
           
        }
    }
}
