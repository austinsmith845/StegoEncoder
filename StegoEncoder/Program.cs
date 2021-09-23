using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;

namespace StegoEncoder
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;
            string message = "";
            string answer = "";
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
            else {
                Console.WriteLine("Enter the path of the image file:");
                path = Console.ReadLine();

                Console.WriteLine("Would you like to hide text or read text (1 = hide, 2 = read)\n");
               
                do
                {
                    answer = Console.ReadLine();
                    if(answer != "1" && answer != "2")
                    {
                        Console.WriteLine("Invalid selection.\n");
                    }
                } while (answer != "1" && answer != "2");

                if (answer == "1")
                {
                    Console.WriteLine("Enter text to hide:");
                    message = Console.ReadLine();
                }
            }

            if (answer == "1")
            {
                FileReader reader = new FileReader();
                Bitmap image = reader.ReadImageFile(path);

                Console.WriteLine("Encoding text.....\n");

                StegoImage stegoImage = new StegoImage(image, message);
                stegoImage.EncodeImage(StegoEncoding.LSB);

                Console.WriteLine("Enter the name of the file to save (be sure to prepend the path onto the name):\n");
                string fileName = Console.ReadLine();

                stegoImage.WriteStegoImage(fileName);

                Console.WriteLine($"Operation Complete. Your key is:\t{stegoImage.MessageToEncode.Length}.\nYou will need this to decode.");
            }
            else
            {
                ReadTextFromStego(path);
            }
           
        }

        public static void ReadTextFromStego(string fileName)
        {
            StegoImage stego = new StegoImage(new Bitmap(fileName));

            Console.WriteLine("Enter you key:\n");
            int key;
            do
            {
                try
                {
                    key = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid Entry.\n");
                    key = -1;
                }
            } while (key == -1);

            Console.WriteLine("Decoding image....\nNote! if you did not specify the correct key then\nyou will not get the correct message!\n");
            string message = stego.DecodeImage(StegoEncoding.LSB, key);

            Console.WriteLine($"The hidden message is:\n{message}");
        }
    }
}
