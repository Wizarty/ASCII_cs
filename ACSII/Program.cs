using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
namespace ASCII
{
    class Program
    {
        private const double W_OFFSET = 2;
        private const int MAX_WIDTH = 474;

        [STAThread]
        static void Main(string[] args)
        {
            var openFileDialog = new OpenFileDialog

            { 
                Filter = "Images | *.bmp; *.png; *.jpg; *.JPEG"
            };

            Console.WriteLine("Press to Start ...\n");

            while (true)
            {
                Console.ReadLine();

                if (openFileDialog.ShowDialog() != DialogResult.OK) 
                    continue;

                Console.Clear();

                var bitmap = new Bitmap(openFileDialog.FileName);
                bitmap = ResizeBitmap(bitmap);
                bitmap.ToGrayscale();

                var converter = new BitmapToASCII(bitmap);
                var rows = converter.Convert();

                foreach (var row in rows)
                    Console.WriteLine(row);

                File.WriteAllLines("image.txt", rows.Select(r => new string(r)));

                Console.SetCursorPosition(0, 0);
            }
        }

        private static Bitmap ResizeBitmap(Bitmap bitmap)
        {
          
            var newHeight = bitmap.Height / W_OFFSET * MAX_WIDTH / bitmap.Width;
            if (bitmap.Width > MAX_WIDTH || bitmap.Height > newHeight)
                bitmap = new Bitmap(bitmap, new Size(MAX_WIDTH, (int)newHeight));
            return bitmap;
        }
    }
}
