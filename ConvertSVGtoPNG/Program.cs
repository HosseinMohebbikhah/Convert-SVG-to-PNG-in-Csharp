using Svg;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertSVGtoPNG
{
    class Program
    {
        static int i = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Please Select Your Folder...");
            Thread t = new Thread((ThreadStart)(() =>
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Multiselect = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        List<string> files = new List<string>();
                        files.AddRange(openFileDialog1.FileNames);
                        foreach (var item in files)
                        {
                            i++;
                            if (item.ToLower().Contains(".svg"))
                            {
                                try
                                {
                                    var svgDocument = SvgDocument.Open(item);
                                    var bitmap = svgDocument.Draw();
                                    bitmap.Save($@"PNG Folder\{Path.GetFileNameWithoutExtension(item)}.png", ImageFormat.Png);
                                }
                                catch
                                { }
                                Console.Clear();
                                Console.WriteLine($"OK. ({files.Count}/{string.Format("{0:0000}", i)})");
                                Application.DoEvents();
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("ERROR! Please check passphrase and do not attempt to edit cipher text");
                    }
                }
            }));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
            Console.WriteLine("End.");
        }
    }
}
